import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/transferencias/models/confirmar_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/models/cuenta_transferencia.dart';
import 'package:caja_tacna_app/features/transferencias/models/transferir_entre_cuentas_response.dart';
import 'package:caja_tacna_app/features/transferencias/services/transferencia_entre_mis_cuentas_service.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/models/datos_iniciales_response.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TransferenciaEntreCuentasController<T extends Ref> {
  final T ref;

  TransferenciaEntreCuentasController(this.ref);

  Future<DatosInicialesResponse?> getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final response =
          await TransferenciaEntreCuentasService.obtenerDatosIniciales();
      return response;
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      return null;
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  Future<TransferirEntreCuentasResponse?> transferir({
    required String cuentaOrigen,
    required String cuentaDestino,
    required String monto,
    required String identificadorDispositivo,
    String codigoSistema = 'CC',
  }) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final response = await TransferenciaEntreCuentasService.transferir(
        numeroCuentaOrigen: cuentaOrigen,
        numeroCuentaDestino: cuentaDestino,
        monto: monto,
        codigoSistema: codigoSistema,
        identificadorDispositivo: identificadorDispositivo,
      );
      return response;
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      return null;
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  Future<ConfirmarResponseTransEntreCuentas?> confirmar(
      {required String? cuentaOrigen,
      required String? cuentaDestino,
      required String monto,
      required String? codigoMoneda,
      String tokenDigital = '',
      String? codigoSistema = 'CC'}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      final response = await TransferenciaEntreCuentasService.confirmar(
        numeroCuentaOrigen: cuentaOrigen,
        numeroCuentaDestino: cuentaDestino,
        monto: monto,
        codigoMoneda: codigoMoneda,
        tokenDigital: tokenDigital,
        codigoSistema: codigoSistema,
      );
      return response;
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      return null;
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }

  Future<void> agregarOperacionFrecuentePropia(
      {required CuentaTransferencia cuentaOrigen,
      required CuentaTransferencia cuentaDestino,
      required String nombreOperacionFrecuente}) async {
    try {
      await OperacionesFrecuentesService.agregarTransPropia(
        numeroCuentaOrigen: cuentaOrigen.numeroProducto,
        numeroCuentaDestino: cuentaDestino.numeroProducto,
        nombreOperacionFrecuente: nombreOperacionFrecuente,
        codigoSistema: cuentaDestino.codigoSistema,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  Future<void> agregarOperacionFrecuenteTerceros(
      {required String? cuentaOrigen,
      required String? cuentaDestino,
      required String nombreOperacionFrecuente}) async {
    try {
      await OperacionesFrecuentesService.agregarTransTerceros(
        numeroCuentaOrigen: cuentaOrigen,
        numeroCuentaDestino: cuentaDestino,
        nombreOperacionFrecuente: nombreOperacionFrecuente,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  Future<void> reenviarComprobante({
    required String tipoOperacion,
    required String correo,
    required int? idOperacionTts,
  }) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      await SharedService.reenviarComprobante(
        tipoOperacion: tipoOperacion,
        correoElectronicoDestinatario: correo,
        idOperacionTts: idOperacionTts,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }
}
