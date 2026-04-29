class ObtenerTokenIdentityResponse {
  final String data;
  final bool success;

  ObtenerTokenIdentityResponse({
    required this.data,
    required this.success,
  });

  factory ObtenerTokenIdentityResponse.fromJson(Map<String, dynamic> json) =>
      ObtenerTokenIdentityResponse(
        data: json["Data"],
        success: json["Success"],
      );

  Map<String, dynamic> toJson() => {
        "data": data,
        "success": success,
      };
}
