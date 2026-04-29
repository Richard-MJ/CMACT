class NfcCardData {
  final String pan;
  final String expiryDate;

  NfcCardData({
    required this.pan,
    required this.expiryDate,
  });

  factory NfcCardData.fromMap(Map<dynamic, dynamic> map) {
    return NfcCardData(
      pan: map["PAN"] as String,
      expiryDate: map["EXPIRY_DATE"] as String,
    );
  }

  @override
  String toString() => 'NfcCardData(pan: $pan, expiryDate: $expiryDate)';
}
