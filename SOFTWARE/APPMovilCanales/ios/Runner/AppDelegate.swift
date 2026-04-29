import Flutter
import UIKit
import XYUUID
import LocalAuthentication

@main
@objc class AppDelegate: FlutterAppDelegate {
  override func application(
    _ application: UIApplication,
    didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?
  ) -> Bool {

    SecurityGuard.performChecks()

    self.window?.secureApp()
    
      let controller = window?.rootViewController as! FlutterViewController
      
      let screenLockCheckCtChannel = FlutterMethodChannel(name: "screen_lock_check_ct", binaryMessenger: controller.binaryMessenger)
      screenLockCheckCtChannel.setMethodCallHandler { (call: FlutterMethodCall, result: @escaping FlutterResult) in
          if call.method == "isScreenLockEnabled" {
              let context = LAContext()
              var error: NSError?
              result(context.canEvaluatePolicy(.deviceOwnerAuthentication,error: &error))
          }else {
              result(FlutterMethodNotImplemented)
          }
      }
      
      let deviceUuidCtChannel = FlutterMethodChannel(name: "device_uuid_ct", binaryMessenger: controller.binaryMessenger)
      deviceUuidCtChannel.setMethodCallHandler { (call: FlutterMethodCall, result: @escaping FlutterResult) in
          if call.method == "getUUID" {
              result(XYUUID.uuidForDevice())
          }else {
              result("")
          }
      }
      
    GeneratedPluginRegistrant.register(with: self)
    return super.application(application, didFinishLaunchingWithOptions: launchOptions)
  }
}

extension UIWindow {
  func secureApp(){
    let field = UITextField()
    field.isSecureTextEntry = true
    self.addSubview(field)
    field.centerYAnchor.constraint(equalTo: self.centerYAnchor).isActive = true
    field.centerXAnchor.constraint(equalTo: self.centerXAnchor).isActive = true
    self.layer.superlayer?.addSublayer(field.layer)
    field.layer.sublayers?.last?.addSublayer(self.layer)
  }
}
