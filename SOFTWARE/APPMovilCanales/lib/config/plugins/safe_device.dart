import 'package:safe_device/safe_device.dart';

class SafeDevicePlugin {
  static Future<(bool, String)> verify() async {
    bool isJailBroken = await SafeDevice.isJailBroken;

    if (isJailBroken) {
      return (
        false,
        "La aplicación ha detectado un entorno modificado (root o jailbreak) en el dispositivo. La ejecución de la aplicación está bloqueada debido a consideraciones de seguridad."
      );
    }

    bool isRealDevice = await SafeDevice.isRealDevice;

    if (!isRealDevice) {
      return (
        false,
        "La aplicación no puede ejecutarse en un entorno de emulación. Por favor, ejecute la aplicación en un dispositivo físico."
      );
    }

    bool isDeveloperModeEnabled = await SafeDevice.isDevelopmentModeEnable;

    if (isDeveloperModeEnabled) {
      return (
        false,
        "El modo de desarrollador está habilitado en el dispositivo. Por favor, desactive el modo de desarrollador para continuar utilizando la aplicación."
      );
    }

    bool isDeveloperModeOn = await SafeDevice.isOnExternalStorage;

    if (isDeveloperModeOn) {
      return (
        false,
        "La aplicación está instalada en un almacenamiento externo, lo cual puede causar problemas de funcionamiento. Por favor, reinstale la aplicación en el almacenamiento interno del dispositivo."
      );
    }

    return (true, "ok");
  }
}
