import Flutter
import UIKit
import CoreNFC

@available(iOS 16.0, *)
public class EmvNfcPlugin: NSObject, FlutterPlugin, NFCTagReaderSessionDelegate {

    private var channel: FlutterMethodChannel!
    private var nfcSession: NFCTagReaderSession?
    private var resultCallback: FlutterResult?

    public static func register(with registrar: FlutterPluginRegistrar) {
        let channel = FlutterMethodChannel(name: "emv_nfc_plugin", binaryMessenger: registrar.messenger())
        let instance = EmvNfcPlugin()
        instance.channel = channel
        registrar.addMethodCallDelegate(instance, channel: channel)
    }

    public func handle(_ call: FlutterMethodCall, result: @escaping FlutterResult) {
        self.resultCallback = result
        switch call.method {
        case "startNfcEmvReading":
            startNfcEmvReading()
        case "stopNfcEmvReading":
            stopNfcEmvReading()
        case "isNfcAvailable":
            isNfcAvailable()
        default:
            resultCallback?(FlutterMethodNotImplemented)
        }
    }

    private func isNfcAvailable() {
        resultCallback?(NFCTagReaderSession.readingAvailable ? "AVAILABLE" : "UNAVAILABLE")
    }

    private func startNfcEmvReading() {
        guard NFCTagReaderSession.readingAvailable else {
            resultCallback?(FlutterError(code: "UNAVAILABLE", message: "NFC no disponible", details: nil))
            return
        }
        nfcSession = NFCTagReaderSession(pollingOption: .iso14443, delegate: self)
        nfcSession?.alertMessage = "Acerca tu tarjeta EMV al lector."
        nfcSession?.begin()
        resultCallback?(true)
    }

    private func stopNfcEmvReading() {
        nfcSession?.invalidate()
        nfcSession = nil
        resultCallback?(true)
    }

    public func tagReaderSessionDidBecomeActive(_ session: NFCTagReaderSession) {
    }

    public func tagReaderSession(_ session: NFCTagReaderSession, didDetect tags: [NFCTag]) {
        guard let firstTag = tags.first, case let .iso7816(iso7816Tag) = firstTag else {
            session.invalidate(errorMessage: "Tarjeta no compatible con EMV")
            sendFlutterResult(status: "EMV_NOT_SUPPORTED", message: "Tarjeta no compatible", data: nil)
            return
        }

        session.connect(to: .iso7816(iso7816Tag)) { error in
            if let error = error {
                session.invalidate(errorMessage: "Error de conexión: \(error.localizedDescription)")
                self.sendFlutterResult(status: "ERROR", message: "Conexión fallida: \(error.localizedDescription)", data: nil)
                return
            }

            // 🔄 Aquí se ejecuta todo el flujo de lectura EMV con la sesión vigente
            Task {
                await self.handleNfcTag(iso7816Tag, session: session)
            }
        }
    }

    public func tagReaderSession(_ session: NFCTagReaderSession, didInvalidateWithError error: Error) {
        let nsErr = error as NSError
        if nsErr.domain == "com.apple.CoreNFC" &&
           nsErr.code == NFCReaderError.readerSessionInvalidationErrorUserCanceled.rawValue {
            self.sendFlutterResult(status: "CANCELLED", message: "Lectura cancelada", data: nil)
        } else {
            self.sendFlutterResult(status: "ERROR", message: "Sesión NFC inválida: \(error.localizedDescription)", data: nil)
        }
        nfcSession = nil
    }

    // MARK: - Handle NFC Tag

    private func handleNfcTag(_ tag: NFCISO7816Tag, session: NFCTagReaderSession) async {
        let reader = EmvCardReader(iso7816Tag: tag)

        do {
            if let cardData = try await reader.readEmvCard() {
                DispatchQueue.main.async {
                    session.alertMessage = "Datos leídos con éxito"
                    session.invalidate()
                    self.sendFlutterResult(status: "SUCCESS", message: "Datos de tarjeta leídos", data: cardData)
                }
            } else {
                DispatchQueue.main.async {
                    session.invalidate(errorMessage: "No se pudo extraer datos EMV")
                    self.sendFlutterResult(status: "ERROR", message: "Extracción fallida", data: nil)
                }
            }
        } catch {
            DispatchQueue.main.async {
                session.invalidate(errorMessage: "Error lectura tarjeta: \(error.localizedDescription)")
                self.sendFlutterResult(status: "ERROR", message: "Error lectura: \(error.localizedDescription)", data: nil)
            }
        }
    }

    private func sendFlutterResult(status: String, message: String, data: [String: String]?) {
        DispatchQueue.main.async {
            self.channel.invokeMethod("onEmvReadResult", arguments: [
                "status": status,
                "message": message,
                "data": data as Any
            ])
        }
    }
}
