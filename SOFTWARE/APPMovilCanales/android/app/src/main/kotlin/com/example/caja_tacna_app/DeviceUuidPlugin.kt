package com.example.caja_tacna_app

import android.app.KeyguardManager
import android.content.Context
import android.provider.Settings
import androidx.annotation.NonNull
import io.flutter.embedding.engine.plugins.FlutterPlugin
import io.flutter.plugin.common.MethodCall
import io.flutter.plugin.common.MethodChannel
import java.security.MessageDigest

class DeviceUuidPlugin : FlutterPlugin, MethodChannel.MethodCallHandler {

    private lateinit var channel: MethodChannel
    private lateinit var context: Context

    override fun onAttachedToEngine(@NonNull flutterPluginBinding: FlutterPlugin.FlutterPluginBinding) {
        channel = MethodChannel(flutterPluginBinding.binaryMessenger, "device_uuid_ct")
        context = flutterPluginBinding.applicationContext
        channel.setMethodCallHandler(this)
    }

    override fun onDetachedFromEngine(@NonNull binding: FlutterPlugin.FlutterPluginBinding) {
        channel.setMethodCallHandler(null)
    }

    override fun onMethodCall(@NonNull call: MethodCall, @NonNull result: MethodChannel.Result) {
        if (call.method == "getUUID") {
            try {
                var deviceId =
                    Settings.Secure.getString(context.contentResolver, Settings.Secure.ANDROID_ID)
                val bytes = MessageDigest
                    .getInstance("SHA-1")
                    .digest(deviceId.toByteArray())
                result.success(bytes.fold("") { str, it -> str + "%02x".format(it) })
            } catch (e: Exception) {
                result.success(null);
            }
        } else {
            result.notImplemented()
        }
    }
}