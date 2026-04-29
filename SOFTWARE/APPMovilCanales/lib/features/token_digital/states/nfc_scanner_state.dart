import 'package:caja_tacna_app/config/plugins/emv_nfc_plugin/nfc_card_data.dart';
import 'package:flutter/material.dart';

class NfcScannerState {
  final String status;
  final String message;
  final NfcCardData? nfcCardData; // Ahora tipado
  final bool isLoading;

  NfcScannerState({
    this.status = 'INACTIVO',
    this.message = 'Listo para escanear',
    this.nfcCardData,
    this.isLoading = false,
  });

  NfcScannerState copyWith({
    String? status,
    String? message,
    ValueGetter<NfcCardData?>?
        nfcCardData, // Usar ValueGetter para nullabilidad
    bool? isLoading,
  }) {
    return NfcScannerState(
      status: status ?? this.status,
      message: message ?? this.message,
      nfcCardData: nfcCardData != null ? nfcCardData() : this.nfcCardData,
      isLoading: isLoading ?? this.isLoading,
    );
  }
}
