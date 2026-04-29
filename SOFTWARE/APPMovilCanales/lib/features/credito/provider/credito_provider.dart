import 'package:caja_tacna_app/features/credito/models/credito.dart';
import 'package:caja_tacna_app/features/credito/models/movimiento_credito.dart';
import 'package:caja_tacna_app/features/credito/services/credito_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/providers/pago_creditos_propios_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/providers/pago_creditos_terceros_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final creditoProvider =
    StateNotifierProvider<CreditoNotifier, CreditoState>((ref) {
  return CreditoNotifier(ref);
});

class CreditoNotifier extends StateNotifier<CreditoState> {
  CreditoNotifier(this.ref) : super(CreditoState());

  final Ref ref;

  getMovimientos(String numeroCredito) async {
    state = state.copyWith(
      loadingMovimientos: true,
    );
    try {
      final movimientosResponse =
          await CreditoService.getMovimientos(numeroCredito: numeroCredito);

      state = state.copyWith(
        movimientos: movimientosResponse,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingMovimientos: false,
    );
  }

  setCredito(Credito credito) {
    state = state.copyWith(
      credito: credito,
      movimientos: [],
    );
  }

  enviarPlanPagosCredito() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await CreditoService.enviarPlanPagosCredito(
        numeroCredito: state.credito?.numeroCredito,
      );

      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Cronograma enviado con éxito', SnackbarType.floating);
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  setPagarCredito() async {
    ref.read(pagoCreditoPropioProvider.notifier).initDatosCuenta();
    await ref.read(pagoCreditoPropioProvider.notifier).getDatosIniciales();

    final indexCredito = ref
        .read(pagoCreditoPropioProvider)
        .creditos
        .indexWhere((c) => c.numeroCredito == state.credito?.numeroCredito);

    if (indexCredito >= 0) {
      final credito =
          ref.read(pagoCreditoPropioProvider).creditos[indexCredito];
      await ref
          .read(pagoCreditoPropioProvider.notifier)
          .setCreditoAbonar(credito);
    } else {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'No se encontró el crédito seleccionado.', SnackbarType.error);
    }
  }

  pagarApp() async {
    ref
        .read(pagoCreditoPropioProvider.notifier)
        .setTipoPago(TipoPago.aplicativo);
    await setPagarCredito();
  }

  pagarCip() async {
    ref.read(pagoCreditoPropioProvider.notifier).setTipoPago(TipoPago.cip);
    await setPagarCredito();
  }
}

class CreditoState {
  final Credito? credito;
  final List<MovimientoCredito> movimientos;
  final bool loadingMovimientos;

  CreditoState({
    this.credito,
    this.movimientos = const [],
    this.loadingMovimientos = false,
  });

  CreditoState copyWith({
    Credito? credito,
    List<MovimientoCredito>? movimientos,
    bool? loadingMovimientos,
  }) =>
      CreditoState(
        credito: credito ?? this.credito,
        movimientos: movimientos ?? this.movimientos,
        loadingMovimientos: loadingMovimientos ?? this.loadingMovimientos,
      );
}
