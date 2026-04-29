class LoginResponse {
  final String accessToken;
  final String tokenType;
  final int expiresIn;
  final int inactivityIn;
  final String refreshToken;
  final String asClientId;
  final String autorizado;
  final String xIdClienteFinal;
  final String xIdVisual;
  final String xIdSesion;
  final String issued;
  final String expires;
  final String? tokenAdmin;
  final String? newGuid;

  LoginResponse({
    required this.accessToken,
    required this.tokenType,
    required this.expiresIn,
    required this.inactivityIn,
    required this.refreshToken,
    required this.asClientId,
    required this.autorizado,
    required this.xIdClienteFinal,
    required this.xIdVisual,
    required this.xIdSesion,
    required this.issued,
    required this.expires,
    this.tokenAdmin,
    this.newGuid,
  });

  factory LoginResponse.fromJson(Map<String, dynamic> json) => LoginResponse(
        accessToken: json["access_token"],
        tokenType: json["token_type"],
        expiresIn: json["expires_in"],
        inactivityIn: json["inactivity_in"],
        refreshToken: json["refresh_token"],
        asClientId: json["as:client_id"],
        autorizado: json["autorizado"],
        xIdClienteFinal: json["x:idClienteFinal"],
        xIdVisual: json["x:idVisual"],
        xIdSesion: json["x:idSesion"],
        issued: json[".issued"],
        expires: json[".expires"],
        tokenAdmin: json["token_admin"],
        newGuid: json["newGuid"],
      );

  Map<String, dynamic> toJson() => {
        "access_token": accessToken,
        "token_type": tokenType,
        "expires_in": expiresIn,
        "inactivity_in": inactivityIn,
        "refresh_token": refreshToken,
        "as:client_id": asClientId,
        "autorizado": autorizado,
        "x:idClienteFinal": xIdClienteFinal,
        "x:idVisual": xIdVisual,
        "x:idSesion": xIdSesion,
        ".issued": issued,
        ".expires": expires,
        "token_admin": tokenAdmin,
        "newGuid": newGuid,
      };
}
