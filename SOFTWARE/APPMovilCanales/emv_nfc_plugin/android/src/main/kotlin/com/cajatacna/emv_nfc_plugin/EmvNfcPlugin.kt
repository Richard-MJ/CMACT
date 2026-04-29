package com.cajatacna.emv_nfc_plugin

import android.app.Activity
import android.content.Context
import android.nfc.NfcAdapter
import android.nfc.Tag
import android.nfc.tech.IsoDep
import androidx.annotation.NonNull

import io.flutter.embedding.engine.plugins.FlutterPlugin
import io.flutter.embedding.engine.plugins.activity.ActivityAware
import io.flutter.embedding.engine.plugins.activity.ActivityPluginBinding
import io.flutter.plugin.common.MethodCall
import io.flutter.plugin.common.MethodChannel
import io.flutter.plugin.common.MethodChannel.MethodCallHandler
import io.flutter.plugin.common.MethodChannel.Result
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import java.io.IOException

/** EmvNfcPlugin */
class EmvNfcPlugin: FlutterPlugin, MethodCallHandler, ActivityAware {
  private lateinit var channel : MethodChannel
  private lateinit var applicationContext: Context
  private var nfcAdapter: NfcAdapter? = null
  private var currentActivity: Activity? = null

  /**
   * Callback que se invoca automáticamente cuando se detecta una etiqueta NFC.
   * Lanza una corrutina en segundo plano para procesar el tag.
   */
  private val readerCallback = NfcAdapter.ReaderCallback { tag ->
    CoroutineScope(Dispatchers.IO).launch {
      handleNfcTag(tag)
    }
  }

  override fun onAttachedToEngine(flutterPluginBinding: FlutterPlugin.FlutterPluginBinding) {
    channel = MethodChannel(flutterPluginBinding.binaryMessenger, "emv_nfc_plugin")
    applicationContext = flutterPluginBinding.applicationContext
    nfcAdapter = NfcAdapter.getDefaultAdapter(applicationContext)
    channel.setMethodCallHandler(this)
  }

  override fun onDetachedFromEngine(binding: FlutterPlugin.FlutterPluginBinding) {
    channel.setMethodCallHandler(null)
    if (nfcAdapter != null && nfcAdapter!!.isEnabled && currentActivity != null) {
      nfcAdapter?.disableReaderMode(currentActivity)
    }
    currentActivity = null
  }

  override fun onMethodCall(call: MethodCall, result: Result) {
    when (call.method) {
      "startNfcEmvReading" -> {
        startNfcEmvReading(result)
      }
      "stopNfcEmvReading" -> {
        stopNfcEmvReading(result)
      }
      "isNfcAvailable" -> {
        isNfcAvailable(result)
      }
      else -> {
        result.notImplemented()
      }
    }
  }

  /**
   * Verifica si el dispositivo tiene hardware NFC y si está habilitado.
   *
   * @return "UNAVAILABLE", "DISABLED" o "AVAILABLE" según el estado.
   */
  private fun isNfcAvailable(result: Result) {
    val adapter = NfcAdapter.getDefaultAdapter(applicationContext)
    if (adapter == null) {
      result.success("UNAVAILABLE")
    } else if (!adapter.isEnabled) {
      result.success("DISABLED")
    } else {
      result.success("AVAILABLE")
    }
  }

  private fun startNfcEmvReading(result: Result) {
    if (nfcAdapter == null) {
      result.error("UNAVAILABLE", "NFC no disponible en este dispositivo.", null)
      return
    }
    if (!nfcAdapter!!.isEnabled) {
      result.error("DISABLED", "NFC está deshabilitado. Habilítalo en la configuración.", null)
      return
    }
    if (currentActivity == null) {
      result.error("NO_ACTIVITY", "La actividad no está adjunta al plugin.", null)
      return
    }

    nfcAdapter?.enableReaderMode(
      currentActivity,
      readerCallback,
      NfcAdapter.FLAG_READER_NFC_A or NfcAdapter.FLAG_READER_SKIP_NDEF_CHECK,
      null
    )
    result.success(true)
  }

  private fun stopNfcEmvReading(result: Result) {
    if (nfcAdapter != null && nfcAdapter!!.isEnabled && currentActivity != null) {
      nfcAdapter?.disableReaderMode(currentActivity)
    }
    result.success(true)
  }



  private suspend fun handleNfcTag(tag: Tag) {
    val isoDep = IsoDep.get(tag)
    if (isoDep == null) {
      sendFlutterResult("EMV_NOT_SUPPORTED", "La tarjeta no es compatible con IsoDep (no es EMV).", null)
      return
    }

    try {
      isoDep.connect()
      val emvCardReader = EmvCardReader(isoDep)
      val cardData = emvCardReader.readEmvCard()

      if (cardData != null) {
        sendFlutterResult("SUCCESS", "Datos de tarjeta leídos con éxito.", cardData)
      } else {
        sendFlutterResult("ERROR", "No se pudieron extraer los datos de la tarjeta EMV.", null)
      }
    } catch (e: IOException) {
      sendFlutterResult("ERROR", "Error de comunicación NFC: ${e.message}", null)
    } catch (e: Exception) {
      sendFlutterResult("ERROR", "Error al procesar la tarjeta: ${e.message}", null)
    } finally {
      try {
        isoDep.close()
      } catch (e: IOException) {
        println("Error al cerrar IsoDep: ${e.message}")
      }
    }
  }

  /**
   * Envía el resultado del procesamiento de la tarjeta EMV al código Flutter.
   *
   * @param status Código de estado: "SUCCESS", "ERROR", etc.
   * @param message Mensaje descriptivo del resultado.
   * @param data Mapa con los datos leídos de la tarjeta o null.
   */
  private suspend fun sendFlutterResult(status: String, message: String, data: Map<String, String>?) {
    withContext(Dispatchers.Main) {
      channel.invokeMethod("onEmvReadResult", mapOf(
        "status" to status,
        "message" to message,
        "data" to data
      ))
    }
  }

  override fun onAttachedToActivity(@NonNull binding: ActivityPluginBinding) {
    currentActivity = binding.activity
  }

  override fun onDetachedFromActivity() {
    if (nfcAdapter != null && nfcAdapter!!.isEnabled && currentActivity != null) {
      nfcAdapter?.disableReaderMode(currentActivity)
    }
    currentActivity = null
  }

  override fun onDetachedFromActivityForConfigChanges() {
  }

  override fun onReattachedToActivityForConfigChanges(binding: ActivityPluginBinding) {
    currentActivity = binding.activity
  }
}
