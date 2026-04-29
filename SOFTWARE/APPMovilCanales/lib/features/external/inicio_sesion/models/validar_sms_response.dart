class ValidarSmsResponse {
  final String tokenType;
  final String refreshToken;
  final String accessToken;
  final String expiresIn;
  final int inactivityIn;
  final String issued;
  final String expires;
  final String guid;

  ValidarSmsResponse({
    required this.tokenType,
    required this.refreshToken,
    required this.accessToken,
    required this.expiresIn,
    required this.inactivityIn,
    required this.issued,
    required this.expires,
    required this.guid,
  });

  factory ValidarSmsResponse.fromJson(Map<String, dynamic> json) =>
      ValidarSmsResponse(
        tokenType: json["token_type"],
        refreshToken: json["refresh_token"],
        accessToken: json["access_token"],
        expiresIn: json["expires_in"],
        inactivityIn: json["inactivity_in"],
        issued: json[".issued"],
        expires: json[".expires"],
        guid: json["guid"],
      );

  Map<String, dynamic> toJson() => {
        "token_type": tokenType,
        "refresh_token": refreshToken,
        "access_token": accessToken,
        "expires_in": expiresIn,
        "inactivity_in": inactivityIn,
        ".issued": issued,
        ".expires": expires,
        "guid": guid,
      };
}
