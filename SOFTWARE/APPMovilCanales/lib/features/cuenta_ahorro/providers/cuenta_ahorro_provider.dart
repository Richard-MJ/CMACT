import 'package:caja_tacna_app/features/cuenta_ahorro/models/cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/movimiento_cuenta_ahorro.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/models/tipo_movimiento.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/services/cuenta_ahorro_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final cuentaAhorroProvider =
    NotifierProvider<CuentaAhorroNotifier, CuentaAhorroState>(
        () => CuentaAhorroNotifier());

class CuentaAhorroNotifier extends Notifier<CuentaAhorroState> {
  @override
  CuentaAhorroState build() {
    return CuentaAhorroState();
  }

  final storageService = StorageService();

  initVistaDetalle() {
    state = state.copyWith(
        cuentaAhorro: () => null,
        movimientos: [],
        movimientosCongelados: [],
        tipoMovimiento: TipoMovimiento.movimiento);
  }

  initVistaMovimientos() {
    state = state.copyWith(
      fechaInicio: () => null,
      fechaFin: () => null,
    );
  }

  getDetalle(String codigoAgencia, String identificador) async {
    state = state.copyWith(
      loadingDetalle: true,
    );
    try {
      final detalleResponse = await CuentaAhorroService.obtenerDetalleCuenta(
          codigoAgencia, identificador);

      state = state.copyWith(
        cuentaAhorro: () => detalleResponse,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingDetalle: false,
    );
  }

  getMovimientos(String codigoAgencia, String identificador) async {
    state = state.copyWith(
      loadingMovimientos: true,
    );
    try {
      final movimientosResponse = await CuentaAhorroService.obtenerMovimientos(
          codigoAgencia, identificador);

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

  enviarEstadoCuentaAhorro() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await CuentaAhorroService.enviarEstadoCuentaAhorro(
        anioInicio: state.fechaInicio?.year,
        mesInicio: state.fechaInicio!.month < 10
            ? '0${state.fechaInicio?.month}'
            : '${state.fechaInicio?.month}',
        anioFin: state.fechaFin?.year,
        mesFin: state.fechaFin!.month < 10
            ? '0${state.fechaFin?.month}'
            : '${state.fechaFin?.month}',
        numeroCuenta: state.cuentaAhorro?.identificador,
        codigoSistema: state.cuentaAhorro?.codigoSistema,
      );

      ref.read(snackbarProvider.notifier).showSnackbar(
          'Movimientos enviados con éxito', SnackbarType.floating);
      initVistaMovimientos();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  setFechaInicio(int month, int year) {
    state = state.copyWith(
      fechaInicio: () => DateTime(year, month),
    );
  }

  setFechaFin(int month, int year) {
    state = state.copyWith(
      fechaFin: () => DateTime(year, month),
    );
  }

  getMovimientosCongelados(String numeroProducto) async {
    try {
      final movimientosCongeladosResponse =
          await CuentaAhorroService.obtenerMovimientosCongelados(
              numeroProducto);

      state = state.copyWith(
        movimientosCongelados: movimientosCongeladosResponse,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  changeMovimientosCongelados(TipoMovimiento tipoMovimiento) {
    state = state.copyWith(tipoMovimiento: tipoMovimiento);
  }
}

class CuentaAhorroState {
  final DateTime? fechaInicio;
  final DateTime? fechaFin;
  final CuentaAhorro? cuentaAhorro;
  final List<MovimientoCuentaAhorro> movimientos;
  final List<MovimientoCuentaAhorro> movimientosCongelados;
  final bool loadingMovimientos;
  final bool loadingDetalle;
  final TipoMovimiento tipoMovimiento;

  CuentaAhorroState(
      {this.fechaInicio,
      this.fechaFin,
      this.cuentaAhorro,
      this.movimientos = const [],
      this.movimientosCongelados = const [],
      this.loadingMovimientos = false,
      this.loadingDetalle = false,
      this.tipoMovimiento = TipoMovimiento.movimiento});

  CuentaAhorroState copyWith(
          {ValueGetter<DateTime?>? fechaInicio,
          ValueGetter<DateTime?>? fechaFin,
          ValueGetter<CuentaAhorro?>? cuentaAhorro,
          List<MovimientoCuentaAhorro>? movimientos,
          List<MovimientoCuentaAhorro>? movimientosCongelados,
          bool? loadingMovimientos,
          bool? loadingDetalle,
          TipoMovimiento? tipoMovimiento}) =>
      CuentaAhorroState(
          fechaInicio: fechaInicio != null ? fechaInicio() : this.fechaInicio,
          fechaFin: fechaFin != null ? fechaFin() : this.fechaFin,
          cuentaAhorro:
              cuentaAhorro != null ? cuentaAhorro() : this.cuentaAhorro,
          movimientos: movimientos ?? this.movimientos,
          movimientosCongelados:
              movimientosCongelados ?? this.movimientosCongelados,
          loadingMovimientos: loadingMovimientos ?? this.loadingMovimientos,
          loadingDetalle: loadingDetalle ?? this.loadingDetalle,
          tipoMovimiento:
              tipoMovimiento ?? this.tipoMovimiento);
}
