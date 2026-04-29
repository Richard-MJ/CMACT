class ObtenerDispositivoResponse {
  final int id;
  final String deviceModel;
  final String deviceManufacturer;
  final String deviceName;
  final String devicePlatform;
  final String deviceIdiom;
  final String deviceType;
  final String deviceUuid;
  final DateTime creationDate;
  final DateTime lastUpdateDate;
  final String deviceStatus;

  ObtenerDispositivoResponse({
    required this.id,
    required this.deviceModel,
    required this.deviceManufacturer,
    required this.deviceName,
    required this.devicePlatform,
    required this.deviceIdiom,
    required this.deviceType,
    required this.deviceUuid,
    required this.creationDate,
    required this.lastUpdateDate,
    required this.deviceStatus,
  });

  factory ObtenerDispositivoResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerDispositivoResponse(
        id: json["id"],
        deviceModel: json["device_model"],
        deviceManufacturer: json["device_manufacturer"],
        deviceName: json["device_name"],
        devicePlatform: json["device_platform"],
        deviceIdiom: json["device_idiom"],
        deviceType: json["device_type"],
        deviceUuid: json["device_uuid"],
        creationDate: DateTime.parse(json["creation_date"]),
        lastUpdateDate: DateTime.parse(json["last_update_date"]),
        deviceStatus: json["device_status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "device_model": deviceModel,
        "device_manufacturer": deviceManufacturer,
        "device_name": deviceName,
        "device_platform": devicePlatform,
        "device_idiom": deviceIdiom,
        "device_type": deviceType,
        "device_uuid": deviceUuid,
        "creation_date": creationDate.toIso8601String(),
        "last_update_date": lastUpdateDate.toIso8601String(),
        "device_status": deviceStatus,
      };
}
