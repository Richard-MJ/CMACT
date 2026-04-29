class EncrypResponse {
  final String ct;
  final String iv;
  final String s;
  final String? tag;

  EncrypResponse({
    required this.ct,
    required this.iv,
    required this.s,
    this.tag,
  });

  bool get isGCM => tag != null && tag!.isNotEmpty;

  factory EncrypResponse.fromJson(Map<String, dynamic> json) => EncrypResponse(
        ct: json["ct"],
        iv: json["iv"],
        s: json["s"],
        tag: json["tag"],
      );

  Map<String, dynamic> toJson() => {
        "ct": ct,
        "iv": iv,
        "s": s,
        if (tag != null) "tag": tag,
      };
}
