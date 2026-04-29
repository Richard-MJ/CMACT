class ErrorCuota {
  final String error;
  final String errorDescription;

  ErrorCuota({
    required this.error,
    required this.errorDescription,
  });

  factory ErrorCuota.fromJson(Map<String, dynamic> json) => ErrorCuota(
        error: json["error"],
        errorDescription: json["error_description"],
      );

  Map<String, dynamic> toJson() => {
        "error": error,
        "error_description": errorDescription,
      };
}
