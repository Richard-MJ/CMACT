package com.cajatacna.emv_nfc_plugin

import android.nfc.tech.IsoDep
import java.nio.ByteBuffer
import java.util.*
import kotlin.collections.LinkedHashMap
import java.time.format.DateTimeFormatter
import java.time.LocalDate
import kotlin.random.Random

class EmvCardReader(private val isoDep: IsoDep) {

    private val HEX_ARRAY = "0123456789ABCDEF".toCharArray()
    private val AID_VISA = "A0000000031010"
    private val PDOL_TAG_TTQ = "9F66" // Terminal Transaction Qualifiers
    private val PDOL_TAG_AMOUNT_AUTH = "9F02" // Amount, Authorized (Numeric)
    private val PDOL_TAG_AMOUNT_OTHER = "9F03" // Amount, Other (Numeric)
    private val PDOL_TAG_COUNTRY_CODE = "9F1A" // Terminal Country Code
    private val PDOL_TAG_TVR = "95" // Terminal Verification Results
    private val PDOL_TAG_CURRENCY_CODE = "5F2A" // Transaction Currency Code
    private val PDOL_TAG_DATE = "9A" // Transaction Date
    private val PDOL_TAG_TRANSACTION_TYPE = "9C" // Transaction Type
    private val PDOL_TAG_UNPREDICTABLE_NUMBER = "9F37" // Unpredictable Number

    fun readEmvCard(): Map<String, String>? {
        val cardData = LinkedHashMap<String, String>()

        try {
            val selectApdu = buildSelectAidApdu(AID_VISA)
            val selectResponse = isoDep.transceive(selectApdu)
            if (!isSuccess(selectResponse))
                return null

            val pdol = runCatching {
                val tlv1 = parseTlv(bytesToHex(selectResponse))
                val fci = tlv1["6F"]?.let { parseTlv(it) }
                val a5 = fci?.get("A5")?.let { parseTlv(it) }
                a5?.get("9F38")
            }.getOrNull()

            val gpoApdu = buildGpoApdu(pdol)
            val gpoResponse = isoDep.transceive(gpoApdu)
            if (!isSuccess(gpoResponse))
                return null

            val gpoHex = bytesToHex(gpoResponse)
            val tlvMain = parseTlv(gpoHex)
            val template77 = tlvMain["77"]?.let { parseTlv(it) } ?: tlvMain

            template77["57"]?.let { track2 ->
                val parts = track2.split("D")
                if (parts.size >= 2) {
                    val pan = parts[0]
                    val expYY = parts[1].substring(0, 2)
                    val expMM = parts[1].substring(2, 4)
                    cardData["PAN"] = pan
                    cardData["EXPIRY_DATE"] = "$expMM/$expYY"
                }
            } ?: run {
                return null
            }

            return cardData
        } catch (e: Exception) {
            return null
        }
    }

    /**
     * Construye un comando APDU para seleccionar una aplicación (AID) en la tarjeta.
     *
     * El comando SELECT (CLA: 00, INS: A4) se utiliza para seleccionar una aplicación
     * específica dentro de una tarjeta inteligente EMV. Este método arma el APDU en formato:
     * [00 A4 04 00 Lc AID 00], donde:
     * - CLA = 00 (instrucción estándar)
     * - INS = A4 (comando SELECT)
     * - P1 = 04 (seleccionar por nombre)
     * - P2 = 00 (primer o único resultado)
     * - Lc = longitud del AID
     * - AID = identificador de la aplicación en formato hexadecimal
     * - Le = 00 (esperar respuesta completa)
     *
     * @param aid AID (Application Identifier) como cadena hexadecimal (por ejemplo, "A0000000031010" para Visa).
     * @return Arreglo de bytes representando el APDU listo para ser enviado mediante NFC a la tarjeta.
     */
    private fun buildSelectAidApdu(aid: String): ByteArray {
        val aidBytes = hexToBytes(aid)
        val aidLength = aidBytes.size
        val apdu = ByteBuffer.allocate(6 + aidLength)
        apdu.put(0x00.toByte())
        apdu.put(0xA4.toByte())
        apdu.put(0x04.toByte())
        apdu.put(0x00.toByte())
        apdu.put(aidLength.toByte())
        apdu.put(aidBytes)
        apdu.put(0x00.toByte())
        return apdu.array()
    }

    /**
     * Construye el comando APDU para el GPO (Get Processing Options) de una tarjeta EMV.
     *
     * Este comando sigue la estructura definida por el estándar EMV para iniciar el procesamiento de la aplicación
     * seleccionada. Utiliza el Tag 83 para encapsular los datos del PDOL (Processing Data Object List), que contiene
     * información que el terminal proporciona a la tarjeta para iniciar la transacción.
     *
     * El comando tiene el siguiente formato:
     * [80 A8 00 00 Lc 83 XX ...] donde:
     * - CLA = 0x80 (proprietary class)
     * - INS = 0xA8 (Get Processing Options)
     * - P1, P2 = 0x00
     * - Lc = longitud total del campo de datos
     * - 83 = tag EMV para indicar el uso de PDOL
     * - XX = longitud del contenido del PDOL (en bytes)
     * - Datos = valores simulados para cada tag del PDOL requerido por la tarjeta
     *
     * Si no se recibe un PDOL (es decir, es null o vacío), se envía un campo de datos "8300", que significa GPO sin PDOL.
     *
     * @param pdolHex Cadena hexadecimal que representa los tags y longitudes del PDOL devuelto por la tarjeta en el SELECT AID.
     * @return APDU listo para ser enviado mediante NFC para ejecutar el comando GPO.
     */
    private fun buildGpoApdu(pdolHex: String?): ByteArray {
        val commandHeader = "80A80000" // CLA INS P1 P2
        var dataField = "83" // Tag para el PDOL en el GPO
        var pdolValueHex = "" // Contenido de datos del PDOL

        if (pdolHex.isNullOrEmpty()) {
            pdolValueHex = "00" // Longitud 00 para un PDOL vacío (8300)
        } else {
            var i = 0
            while (i < pdolHex.length) {
                var tagHex = pdolHex.substring(i, i + 2)
                var tagLenBytes = 1

                if ((tagHex.toInt(16) and 0x1F) == 0x1F) {
                    tagLenBytes = 2
                    tagHex = pdolHex.substring(i, minOf(i + 4, pdolHex.length))
                }
                i += (tagLenBytes * 2)

                if (i + 2 > pdolHex.length) {
                    break
                }

                val len = pdolHex.substring(i, i + 2).toInt(16)
                i += 2

                val simulatedData = when (tagHex.uppercase(Locale.ROOT)) {
                    PDOL_TAG_TTQ -> "20000000" // 4 bytes
                    PDOL_TAG_AMOUNT_AUTH -> "000000000000" // 6 bytes
                    PDOL_TAG_AMOUNT_OTHER -> "000000000000" // 6 bytes
                    PDOL_TAG_COUNTRY_CODE -> "0604" // 2 bytes (Perú)
                    PDOL_TAG_TVR -> "0000000000" // 5 bytes
                    PDOL_TAG_CURRENCY_CODE -> "0604" // 2 bytes (PEN)
                    PDOL_TAG_DATE -> LocalDate.now().format(DateTimeFormatter.ofPattern("yyMMdd")) // 3 bytes
                    PDOL_TAG_TRANSACTION_TYPE -> "00" // 1 byte (Purchase)
                    PDOL_TAG_UNPREDICTABLE_NUMBER -> String.format("%08X", Random.nextInt()) // 4 bytes
                    else -> "0".repeat(len * 2)
                }

                pdolValueHex += simulatedData.substring(0, minOf(simulatedData.length, len * 2)).padEnd(len * 2, '0')
            }
        }

        dataField += String.format("%02X", (pdolValueHex.length / 2)) + pdolValueHex
        val dataLength = String.format("%02X", (dataField.length / 2))

        return hexToBytes("$commandHeader$dataLength$dataField")
    }

    /**
     * Verifica si la respuesta APDU indica éxito.
     *
     * En el protocolo ISO/IEC 7816-4, una respuesta exitosa de una tarjeta (Smart Card o EMV) finaliza con los bytes
     * de estado 0x9000 (SW1=0x90, SW2=0x00). Este método extrae los dos últimos bytes de la respuesta para validar ese estado.
     *
     * @param response Arreglo de bytes devuelto por la tarjeta como respuesta a un comando APDU.
     * @return `true` si la respuesta termina con 0x9000 (éxito), `false` en cualquier otro caso.
     */
    private fun isSuccess(response: ByteArray): Boolean {
        if (response.size < 2) return false
        val sw1 = response[response.size - 2].toInt() and 0xFF
        val sw2 = response[response.size - 1].toInt() and 0xFF
        return sw1 == 0x90 && sw2 == 0x00
    }

    /**
     * Convierte una cadena hexadecimal (por ejemplo: "A0000000031010") en un array de bytes.
     *
     * Este método se usa para transformar comandos APDU representados como texto hexadecimal
     * en datos binarios reales que pueden ser enviados a través de NFC a una tarjeta.
     *
     * Por ejemplo, "A0000000031010" se convierte en:
     * [0xA0, 0x00, 0x00, 0x00, 0x03, 0x10, 0x10]
     *
     * @param hex Cadena en formato hexadecimal (sin espacios).
     * @return ByteArray representando los bytes reales.
     */
    private fun hexToBytes(hex: String): ByteArray {
        val len = hex.length
        val data = ByteArray(len / 2)
        for (i in 0 until len step 2) {
            data[i / 2] = ((Character.digit(hex[i], 16) shl 4) + Character.digit(hex[i + 1], 16)).toByte()
        }
        return data
    }

    /**
     * Convierte un arreglo de bytes en una cadena hexadecimal.
     *
     * Ejemplo: [0xA0, 0x00, 0x10] → "A00010"
     *
     * Se usa para mostrar de forma legible los comandos APDU o las respuestas de la tarjeta.
     */
    private fun bytesToHex(bytes: ByteArray): String {
        val hexChars = CharArray(bytes.size * 2)
        for (i in bytes.indices) {
            val v = bytes[i].toInt() and 0xFF
            hexChars[i * 2] = HEX_ARRAY[v ushr 4]
            hexChars[i * 2 + 1] = HEX_ARRAY[v and 0x0F]
        }
        return String(hexChars)
    }

    /**
     * Parsea una cadena hexadecimal codificada en formato TLV (Tag-Length-Value).
     *
     * Este método analiza una estructura TLV anidada o plana a partir de una cadena
     * hexadecimal, extrayendo cada campo con su respectivo tag, longitud y valor,
     * y los almacena en un mapa (LinkedHashMap) preservando el orden.
     *
     * Soporta tags de uno o varios bytes, longitudes codificadas en uno o más bytes
     * (formato BER-TLV), y valida que los datos no excedan los límites del string.
     *
     * @param hexData Cadena hexadecimal a analizar (por ejemplo, "6F1A8407A0000000031010A50F500B564953412044454249544F").
     * @return Mapa con los tags y sus valores hexadecimales correspondientes (ambos en mayúsculas).
     */
    private fun parseTlv(hexData: String): Map<String, String> {
        val tlvMap = LinkedHashMap<String, String>()
        var i = 0
        while (i < hexData.length - 1) {
            var tag: String
            var tagBytesCount = 0

            if (i + 2 > hexData.length) break
            val firstTagByte = hexData.substring(i, i + 2).toInt(16)
            i += 2
            tagBytesCount = 1

            if ((firstTagByte and 0x1F) == 0x1F) {
                while (i + 2 <= hexData.length) {
                    val nextTagByte = hexData.substring(i, i + 2).toInt(16)
                    i += 2
                    tagBytesCount++
                    if ((nextTagByte and 0x80) == 0) break
                }
            }
            tag = hexData.substring(i - (tagBytesCount * 2), i)

            if (i + 2 > hexData.length) break
            var lengthByte = hexData.substring(i, i + 2).toInt(16)
            i += 2

            var valueLength: Int
            if (lengthByte and 0x80 == 0x80) {
                val numLengthBytes = lengthByte and 0x7F
                if (i + (numLengthBytes * 2) > hexData.length) break
                val actualLengthHex = hexData.substring(i, i + (numLengthBytes * 2))
                valueLength = actualLengthHex.toInt(16)
                i += (numLengthBytes * 2)
            } else {
                valueLength = lengthByte
            }

            if (i + (valueLength * 2) > hexData.length) break
            val value = hexData.substring(i, i + (valueLength * 2))
            i += (valueLength * 2)

            tlvMap[tag.uppercase(Locale.ROOT)] = value.uppercase(Locale.ROOT)
        }
        return tlvMap
    }
}