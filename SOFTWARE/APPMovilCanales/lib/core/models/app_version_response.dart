class AppVersionResponse {
  final String versionActual;
  final String versionPrueba;
  final String urlApp;

  AppVersionResponse({
    required this.versionActual,
    required this.versionPrueba,
    required this.urlApp,
  });

  factory AppVersionResponse.fromJson(Map<String, dynamic> json) =>
      AppVersionResponse(
        versionActual: json["versionActual"],
        versionPrueba: json["versionPrueba"],
        urlApp: json["urlApp"],
      );

  Map<String, dynamic> toJson() => {
        "versionActual": versionActual,
        "versionPrueba": versionPrueba,
        "urlApp": urlApp,
      };
}
