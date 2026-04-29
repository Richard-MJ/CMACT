package com.cajatacna.droid

import android.os.Bundle
import android.os.PersistableBundle
import android.view.WindowManager
import com.example.caja_tacna_app.DeviceUuidPlugin
import com.example.caja_tacna_app.ScreenLockCheckCtPlugin
import io.flutter.embedding.android.FlutterFragmentActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugins.GeneratedPluginRegistrant

class MainActivity: FlutterFragmentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        window.setFlags(
            WindowManager.LayoutParams.FLAG_SECURE,
            WindowManager.LayoutParams.FLAG_SECURE
        )
    }

    override fun configureFlutterEngine(flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)
        GeneratedPluginRegistrant.registerWith(flutterEngine)
        flutterEngine.plugins.add(ScreenLockCheckCtPlugin())
        flutterEngine.plugins.add(DeviceUuidPlugin())
    }
}
