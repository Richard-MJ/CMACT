import 'package:flutter/material.dart';

class TipoTarjeta {
  static const clasica = '1';
  static const debito = '2';
  static const debitoVisa = '3';
  static const ahorrosEmpresarial = '4';
  static const primeraServiCard = '5';
  static const coordenada = '6';
}

const Map<String, Color> cardColors = {
  "1": Color(0x9F85919B),
  "2": Color(0x9F85919B),
  "3": Color(0xFF161513),
  "4": Color(0xFF6E88AC),
  "5": Color(0xD5250073),
  "6": Color(0x9F85919B),
};
