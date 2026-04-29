class ErrorManagerAuth {
  final String error;
  final String errorDescription;

  ErrorManagerAuth({
    required this.error,
    required this.errorDescription,
  });

  factory ErrorManagerAuth.fromJson(Map<String, dynamic> json) => ErrorManagerAuth(
        error: json["error"],
        errorDescription: json["error_description"],
      );

  Map<String, dynamic> toJson() => {
        "error": error,
        "error_description": errorDescription,
      };
}
