import 'dart:async';
import 'dart:convert';
import 'dart:io';

import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/credito_abonar.dart';
import 'package:caja_tacna_app/features/pago_creditos/models/opcion_pago_credito.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/models/index.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/services/pago_creditos_propios_service.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/widgets/dialog_condiciones.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/widgets/dialog_condiciones.dart';
import 'package:caja_tacna_app/features/shared/inputs/monto_anticipo.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';
import 'package:open_file/open_file.dart';
import 'package:path_provider/path_provider.dart';

final pagoCreditoAnticipoProvider =
    NotifierProvider<PagoCreditoAnticipoNotifier, PagoCreditoAnticipoState>(
        () => PagoCreditoAnticipoNotifier());

class PagoCreditoAnticipoNotifier extends Notifier<PagoCreditoAnticipoState> {
  @override
  PagoCreditoAnticipoState build() {
    return PagoCreditoAnticipoState();
  }

  initDatos() {
    state = state.copyWith(
        pagarAppAnticipoResponse: () => null,
        aceptarNuevoCronograma: false,
        aceptarCondiciones: false,
        montoAnticipo: const MontoAnticipo.pure(''));
  }

  setMontoAnticipo(CreditoAbonar creditoAbonar) {
    double valor = creditoAbonar.montoAnticipado >=
            creditoAbonar.montoSaldoCancelacion
        ? creditoAbonar.montoCuotaPactada
        : creditoAbonar.montoAnticipado;

    valor = valor < creditoAbonar.montoMinimoAnticipo
        ? creditoAbonar.montoMinimoAnticipo + 1
        : valor;

    state = state.copyWith(
        montoAnticipo: state.montoAnticipo.copyWith(
      montoCancelacion: creditoAbonar.montoSaldoCancelacion,
      simboloMoneda: creditoAbonar.simboloMoneda,
      montoMinimoAnticipo: creditoAbonar.montoMinimoAnticipo == 0
          ? 1
          : creditoAbonar.montoMinimoAnticipo,
      value: CtUtils.formatStringWithTwoDecimals(
        valor.toString(),
      ),
    ));
  }

  changeMontoAnticipo(MontoAnticipo montoAnticipo) {
    state = state.copyWith(
      montoAnticipo: montoAnticipo,
    );
  }

  toggleAceptarNuevoCronograma() {
    state = state.copyWith(
      aceptarNuevoCronograma: !state.aceptarNuevoCronograma,
    );
  }

  validarMontoAnticipo() {
    final montoAnticipo = state.montoAnticipo.copyWith(
      value: state.montoAnticipo.value,
      isPure: false,
    );

    state = state.copyWith(
      montoAnticipo: montoAnticipo,
    );

    return Formz.validate([montoAnticipo]);
  }

  pagarAnticipo(
      {required String? numeroProducto,
      required int? numeroCredito,
      required TipoAnticipo tipoAnticipo,
      required TipoPagoCredito tipoPago,
      required String? codigoMonedaProducto,
      required int tipoSolicitante,
      required bool withPush}) async {

    final isAnticipoDefault = tipoAnticipo.obtenerIdentificador() == -1;
    final codTipoPago =
        isAnticipoDefault ? 2 : tipoPago.obtenerIdentificador() ;
    final tipAnticipo = isAnticipoDefault ? 0 : tipoAnticipo.obtenerIdentificador();

    var pagarAppAnticipoResponse =
        await PagoCreditosPropiosService.pagarAnticipo(
      numeroCuenta: numeroProducto,
      numeroCredito: numeroCredito,
      codigoTipoAnticipo: tipAnticipo,
      codigoTipoPago: codTipoPago,
      codigoTipoSolicitante: tipoSolicitante,
      codigoMonedaOperacion: codigoMonedaProducto,
      montoAdelantar: state.montoAnticipo.value,
      identificadorDispositivo:
          ref.read(dispositivoProvider.notifier).getIdentificadorDispositivo(),
    );

    state = state.copyWith(
      pagarAppAnticipoResponse: () => pagarAppAnticipoResponse,
    );

    return pagarAppAnticipoResponse;
  }

  confirmarAnticipo({
    required String? numeroProducto,
    required int? numeroCredito,
    required TipoAnticipo tipoAnticipo,
    required TipoPagoCredito tipoPago,
    required int tipoSolicitante,
    required String? codigoMonedaProducto,
    required String codigoAutorizacion,
  }) async {

    final isAnticipoDefault = tipoAnticipo.obtenerIdentificador() == -1;
    final codTipoPago =
        isAnticipoDefault ? 2 : tipoPago.obtenerIdentificador() ;
    final tipAnticipo = isAnticipoDefault ? 0 : tipoAnticipo.obtenerIdentificador();

    return await PagoCreditosPropiosService.confirmarAnticipo(
      numeroCuenta: numeroProducto,
      numeroCredito: numeroCredito,
      codigoTipoAnticipo: tipAnticipo,
      codigoTipoPago: codTipoPago,
      codigoTipoSolicitante: tipoSolicitante,
      codigoMonedaOperacion: codigoMonedaProducto,
      montoAdelantar: state.montoAnticipo.value,
      identificadorDispositivo:
          ref.read(dispositivoProvider.notifier).getIdentificadorDispositivo(),
      codigoAutorizacion: codigoAutorizacion,
    );
  }

  mostrarArchivoDocumentoPlanPago() async {
    Completer<File> completer = Completer();
    try {
      final filename =
          "${state.pagarAppAnticipoResponse!.cronogramaPlanPagos.nombreCartilla}.pdf";
      var bytes = base64
          .decode(state.pagarAppAnticipoResponse!.cronogramaPlanPagos.datos!);
      var dir = await getApplicationDocumentsDirectory();
      File file = File("${dir.path}/$filename");

      await file.writeAsBytes(bytes, flush: true);
      completer.complete(file);

      OpenFile.open(file.path);
    } catch (e) {
      throw Exception('Error al mostrar el documento.');
    }

    return completer.future;
  }

  toggleAceptarCondiciones() {
    state = state.copyWith(
      aceptarCondiciones: !state.aceptarCondiciones,
    );
  }

  showCondicionesModal(
      {required BuildContext context,
      required TipoPagoCredito tipoPagoCredito,
      required bool creditoPropio}) async {
    await showDialog(
      context: context,
      builder: (BuildContext context) {
        return creditoPropio 
          ? DialogCondicionesPropio(tipoPagoCredito: tipoPagoCredito)
          : DialogCondicionesTercero(tipoPagoCredito: tipoPagoCredito);
      },
    );
  }
}

class PagoCreditoAnticipoState {
  final PagarAppAnticipoResponse? pagarAppAnticipoResponse;
  final bool aceptarNuevoCronograma;
  final bool aceptarCondiciones;
  final MontoAnticipo montoAnticipo;

  PagoCreditoAnticipoState({
    this.pagarAppAnticipoResponse,
    this.aceptarNuevoCronograma = false,
    this.aceptarCondiciones = false,
    this.montoAnticipo = const MontoAnticipo.pure(''),
  });

  PagoCreditoAnticipoState copyWith({
    ValueGetter<PagarAppAnticipoResponse?>? pagarAppAnticipoResponse,
    bool? aceptarNuevoCronograma,
    bool? aceptarCondiciones,
    MontoAnticipo? montoAnticipo,
  }) =>
      PagoCreditoAnticipoState(
          pagarAppAnticipoResponse: pagarAppAnticipoResponse != null
              ? pagarAppAnticipoResponse()
              : this.pagarAppAnticipoResponse,
          aceptarNuevoCronograma:
              aceptarNuevoCronograma ?? this.aceptarNuevoCronograma,
          aceptarCondiciones: aceptarCondiciones ?? this.aceptarCondiciones,
          montoAnticipo: montoAnticipo ?? this.montoAnticipo);
}
