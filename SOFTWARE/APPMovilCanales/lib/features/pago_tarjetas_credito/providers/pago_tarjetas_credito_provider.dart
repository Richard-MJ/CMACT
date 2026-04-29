import 'package:flutter_riverpod/flutter_riverpod.dart';

enum TipoTransferencia { inmediata, diferida }

final tipoPagoTarjetaCreditoTransferenciaProvider = StateProvider<TipoTransferencia>(
  (ref) => TipoTransferencia.inmediata,
);