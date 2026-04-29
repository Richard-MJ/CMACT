import CoreNFC

@available(iOS 13.0, *)
class EmvCardReader {

    private let iso7816Tag: NFCISO7816Tag // The ISO 7816 tag for communication

    private let AID_VISA = "A0000000031010" // Visa Application Identifier
    private let PDOL_TAG_TTQ = "9F66" // Terminal Transaction Qualifiers
    private let PDOL_TAG_AMOUNT_AUTH = "9F02" // Amount, Authorized (Numeric)
    private let PDOL_TAG_AMOUNT_OTHER = "9F03" // Amount, Other (Numeric)
    private let PDOL_TAG_COUNTRY_CODE = "9F1A" // Terminal Country Code
    private let PDOL_TAG_TVR = "95" // Terminal Verification Results
    private let PDOL_TAG_CURRENCY_CODE = "5F2A" // Transaction Currency Code
    private let PDOL_TAG_DATE = "9A" // Transaction Date
    private let PDOL_TAG_TRANSACTION_TYPE = "9C" // Transaction Type
    private let PDOL_TAG_UNPREDICTABLE_NUMBER = "9F37" // Unpredictable Number

    init(iso7816Tag: NFCISO7816Tag) {
        self.iso7816Tag = iso7816Tag
    }

    func readEmvCard() async throws -> [String: String]? { // Marked as async and throws
        var cardData = [String: String]()

        let selectApdu = buildSelectAidApdu(aid: AID_VISA)
        let selectResponse: Data
        do {
            selectResponse = try await sendCommand(apdu: selectApdu, commandName: "SELECT AID")
        } catch {
            throw error // Re-throw the error to be handled by the caller
        }

        guard isSuccess(response: selectResponse) else {
            return nil
        }

        let pdolHex: String? = {
            guard let tlv1 = parseTlv(hexData: bytesToHex(selectResponse), context: "SELECT AID Response (Outer)"),
                  let fci = tlv1["6F"].flatMap({ parseTlv(hexData: $0, context: "FCI Template (6F)") }),
                  let a5 = fci["A5"].flatMap({ parseTlv(hexData: $0, context: "Proprietary Template (A5)") }) else {
                return nil
            }
            return a5["9F38"]
        }()

        let gpoApdu = buildGpoApdu(pdolHex: pdolHex)
        let gpoResponse: Data
        do {
            gpoResponse = try await sendCommand(apdu: gpoApdu, commandName: "GET PROCESSING OPTIONS")
        } catch {
            throw error // Re-throw the error
        }

        guard isSuccess(response: gpoResponse) else {
            return nil
        }

        let gpoHex = bytesToHex(gpoResponse)
        guard let tlvMainDict = parseTlv(hexData: gpoHex, context: "GPO Response (Main)") else {
            return nil
        }

        let template77: [String: String]
        if let parsedTemplate77 = tlvMainDict["77"].flatMap({ parseTlv(hexData: $0, context: "Template 77") }) {
            template77 = parsedTemplate77
        } else {
            template77 = tlvMainDict
        }

        if let track2 = template77["57"] {
            let parts = track2.split(separator: "D")
            if parts.count >= 2 {
                let pan = String(parts[0])
                let expiryData = String(parts[1])
                if expiryData.count >= 4 {
                    let expYY = String(expiryData.prefix(2))
                    let expMM = String(expiryData[expiryData.index(expiryData.startIndex, offsetBy: 2)..<expiryData.index(expiryData.startIndex, offsetBy: 4)])
                    cardData["PAN"] = pan
                    cardData["EXPIRY_DATE"] = "\(expMM)/\(expYY)"
                } else {
                    return nil
                }
            } else {
                return nil
            }
        } else {
            return nil // Track 2 data not found
        }

        return cardData
    }

    private func sendCommand(apdu: Data, commandName: String) async throws -> Data { // Marked as async and throws
        do {
            let (response, sw1, sw2) = try await iso7816Tag.sendCommand(apdu: NFCISO7816APDU(data: apdu)!)
            // Combine response data with SW1 and SW2 status bytes
            var fullResponse = response
            fullResponse.append(sw1)
            fullResponse.append(sw2)
            return fullResponse
        } catch let error as NFCReaderError {
            throw error
        } catch {
            throw error
        }
    }

    private func isSuccess(response: Data) -> Bool {
        guard response.count >= 2 else {
            return false
        }
        let sw1 = response[response.count - 2]
        let sw2 = response[response.count - 1]
        let success = (sw1 == 0x90 && sw2 == 0x00)
        return success
    }

    private func hexToBytes(hex: String) -> Data {
        var data = Data(capacity: hex.count / 2)
        var index = hex.startIndex
        while index < hex.endIndex {
            let nextIndex = hex.index(index, offsetBy: 2)
            if let byte = UInt8(hex[index..<nextIndex], radix: 16) {
                data.append(byte)
            } else {
                // Handle error, e.g., return empty data or throw
            }
            index = nextIndex
        }
        return data
    }

    private func bytesToHex(_ bytes: Data) -> String {
        return bytes.map { String(format: "%02X", $0) }.joined()
    }

    private func parseTlv(hexData: String, context: String = "Unknown") -> [String: String]? {
        var tlvMap = [String: String]()
        var currentIndex = hexData.startIndex

        while currentIndex < hexData.endIndex {
            var tagBytesCount = 0
            var tagEndIndex = currentIndex
            var tagHex = ""

            guard let firstTagByteRange = hexData.range(of: hexData[currentIndex..<hexData.endIndex].prefix(2)),
                  let firstTagByte = UInt8(hexData[firstTagByteRange], radix: 16) else {
                return nil
            }
            tagEndIndex = hexData.index(currentIndex, offsetBy: 2)
            tagHex = String(hexData[currentIndex..<tagEndIndex])
            tagBytesCount += 1

            if (firstTagByte & 0x1F) == 0x1F { // Multi-byte tag
                while tagEndIndex < hexData.endIndex {
                    guard let nextTagByteRange = hexData.range(of: hexData[tagEndIndex..<hexData.endIndex].prefix(2)),
                          let nextTagByte = UInt8(hexData[nextTagByteRange], radix: 16) else {
                        return nil
                    }
                    tagEndIndex = hexData.index(tagEndIndex, offsetBy: 2)
                    let startOfLastByte = hexData.index(tagEndIndex, offsetBy: -2)
                    tagHex += String(hexData[startOfLastByte..<tagEndIndex]) // Append the new byte
                    tagBytesCount += 1
                    if (nextTagByte & 0x80) == 0 { break } // Last byte of multi-byte tag
                }
            }
            let tag = tagHex.uppercased()
            currentIndex = tagEndIndex
            let tagStartIndex = hexData.index(currentIndex, offsetBy: -tagHex.count)

            guard currentIndex < hexData.endIndex else {
                return nil
            }
            guard let lengthByteRange = hexData.range(of: hexData[currentIndex..<hexData.endIndex].prefix(2)),
                  var lengthByte = UInt8(hexData[lengthByteRange], radix: 16) else {
                return nil
            }
            currentIndex = hexData.index(currentIndex, offsetBy: 2)

            var valueLength: Int
            if (lengthByte & 0x80) == 0x80 { // Length is encoded in multiple bytes
                let numLengthBytes = Int(lengthByte & 0x7F)
                guard numLengthBytes > 0 else {
                    return nil
                }
                guard hexData.endIndex.encodedOffset - currentIndex.encodedOffset >= numLengthBytes * 2 else {
                    return nil
                }
                let actualLengthHex = String(hexData[currentIndex..<hexData.index(currentIndex, offsetBy: numLengthBytes * 2)])
                guard let parsedLength = Int(actualLengthHex, radix: 16) else {
                    return nil
                }
                valueLength = parsedLength
                currentIndex = hexData.index(currentIndex, offsetBy: numLengthBytes * 2)
            } else {
                valueLength = Int(lengthByte)
            }

            guard hexData.endIndex.encodedOffset - currentIndex.encodedOffset >= valueLength * 2 else {
                return nil
            }
            let valueEndIndex = hexData.index(currentIndex, offsetBy: valueLength * 2)
            let value = String(hexData[currentIndex..<valueEndIndex]).uppercased()
            currentIndex = valueEndIndex

            tlvMap[tag] = value
        }
        return tlvMap
    }

    /// Construye un comando APDU para seleccionar una aplicación (AID) en la tarjeta.
    ///
    /// El comando SELECT (CLA: 00, INS: A4) se utiliza para seleccionar una aplicación específica.
    /// Formato: [00 A4 04 00 Lc AID 00]
    /// - CLA (00): Instrucción estándar.
    /// - INS (A4): Comando SELECT.
    /// - P1 (04): Seleccionar por nombre (AID).
    /// - P2 (00): Primer o único resultado.
    /// - Lc: Longitud del AID.
    /// - AID: Identificador de la aplicación.
    /// - Le (00): Esperar respuesta completa.
    ///
    /// - Parameter aid: El Application Identifier (AID) como una cadena hexadecimal.
    /// - Returns: Un objeto `Data` que representa el APDU completo.
    private func buildSelectAidApdu(aid: String) -> Data {
        let aidBytes = hexToBytes(hex: aid)
        let aidLength = UInt8(aidBytes.count)

        var apdu = Data()
        apdu.append(0x00) // CLA
        apdu.append(0xA4) // INS
        apdu.append(0x04) // P1
        apdu.append(0x00) // P2
        apdu.append(aidLength) // Lc
        apdu.append(aidBytes) // Datos (AID)
        apdu.append(0x00) // Le
        return apdu
    }

    /// Construye el comando APDU para el GPO (Get Processing Options).
    ///
    /// Este comando inicia el procesamiento de la transacción. Utiliza el Tag 83 para encapsular
    /// los datos del PDOL, que son información que el terminal proporciona a la tarjeta.
    ///
    /// Formato: [80 A8 00 00 Lc 83 XX ...]
    /// - Si no se recibe un PDOL, se envía un campo de datos "8300" (GPO sin PDOL).
    ///
    /// - Parameter pdolHex: La cadena hexadecimal opcional que representa los tags y longitudes del PDOL.
    /// - Returns: Un objeto `Data` que representa el APDU de GPO.
    private func buildGpoApdu(pdolHex: String?) -> Data {
        let commandHeader = "80A80000"
        var dataField = "83" // Tag para el PDOL en el GPO
        var pdolValueHex = ""

        if let pdol = pdolHex, !pdol.isEmpty {
            var currentIndex = pdol.startIndex

            while currentIndex < pdol.endIndex {
                var tagEndIndex = pdol.index(currentIndex, offsetBy: 2)
                var tagHex = String(pdol[currentIndex..<tagEndIndex])

                if (Int(tagHex, radix: 16)! & 0x1F) == 0x1F {
                    guard pdol.index(tagEndIndex, offsetBy: 2) <= pdol.endIndex else {
                        break
                    }
                    tagEndIndex = pdol.index(tagEndIndex, offsetBy: 2)
                    tagHex = String(pdol[currentIndex..<tagEndIndex])
                }
                currentIndex = tagEndIndex

                // Extract Length
                guard pdol.index(currentIndex, offsetBy: 2) <= pdol.endIndex else {
                    break
                }
                let lengthEndIndex = pdol.index(currentIndex, offsetBy: 2)
                let len = Int(String(pdol[currentIndex..<lengthEndIndex]), radix: 16)!
                currentIndex = lengthEndIndex

                // Generate simulated data based on the tag
                let simulatedData: String
                switch tagHex.uppercased() {
                    case PDOL_TAG_TTQ: simulatedData = "20000000" // Terminal Transaction Qualifiers
                    case PDOL_TAG_AMOUNT_AUTH: simulatedData = String(repeating: "0", count: len * 2) // Amount, Authorized (Numeric)
                    case PDOL_TAG_AMOUNT_OTHER: simulatedData = String(repeating: "0", count: len * 2) // Amount, Other (Numeric)
                    case PDOL_TAG_COUNTRY_CODE: simulatedData = "0604" // Perú (ISO 3166-1 numeric for Peru)
                    case PDOL_TAG_TVR: simulatedData = String(repeating: "0", count: len * 2) // Terminal Verification Results
                    case PDOL_TAG_CURRENCY_CODE: simulatedData = "0604" // PEN (ISO 4217 numeric for Peruvian Sol)
                    case PDOL_TAG_DATE:
                        let formatter = DateFormatter()
                        formatter.dateFormat = "yyMMdd"
                        simulatedData = formatter.string(from: Date())
                    case PDOL_TAG_TRANSACTION_TYPE: simulatedData = "00" // Purchase
                    case PDOL_TAG_UNPREDICTABLE_NUMBER:
                        // Generate a random 4-byte (8 hex char) unpredictable number
                        simulatedData = String(format: "%08X", Int.random(in: 0...Int.max))
                    default:
                        simulatedData = String(repeating: "0", count: len * 2)
                }
                pdolValueHex += simulatedData.prefix(len * 2)
            }
        } else {
            pdolValueHex = ""
        }

        dataField += String(format: "%02X", pdolValueHex.count / 2) + pdolValueHex

        let dataLength = String(format: "%02X", dataField.count / 2)

        let finalApduHex = commandHeader + dataLength + dataField
        return hexToBytes(hex: finalApduHex)
    }
}
