import 'package:flutter_riverpod/flutter_riverpod.dart';

enum TipoTransferencia { inmediata, diferida }

final tipoTransferenciaProvider = StateProvider<TipoTransferencia>(
  (ref) => TipoTransferencia.inmediata,
);
