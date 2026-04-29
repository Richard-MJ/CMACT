import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/billetera_virtual/models/token_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/inputs/monto_transferir.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_cliente_origen_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_operacion_exitosa_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/detalle_transferencia.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_contacto_barrido.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_entidad_financiera_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/montos_totales_response.dart';
import 'package:flutter/material.dart';

class TransferenciaCelularState {
  final String search;
  final String tokenDigital;
  final String tokenCodigoQr;
  final String idCodigoQr;
  final String simboloMoneda;
  final NumeroCelular numeroCelular;
  final MontoTransferir montoTransferencia;
  final double montoDisponible;
  final double limiteMontoMaximo;
  final double montoMaximoDia;
  final TokenResponse? tokenResponse;
  final List<ContactosBarrido> listaContactos;
  final List<ContactosBarrido> filtroContactos;
  final ContactosBarrido? contactoSeleccionada;
  final List<EntidadesReceptorAfiliado> listaEntidadesFinancieras;
  final DatosAfiliacionResponse? datosAfiliacion;
  final EntidadesReceptorAfiliado? entidadFinancieraSeleccionada;
  final DatosClienteOrigenResponse? datosClienteOrigenResponse;
  final DetalleTransferencia? detalleTransferencia;
  final MontosTotales? montosTotales;
  final DatosOperacionExitosaResponse? datosOperacionResponse;
  final bool? cargandoContactos;
  final bool? sinContactos;
  final bool? permisoDenegado;

  TransferenciaCelularState({
    this.search = '',
    this.tokenDigital = '',
    this.tokenCodigoQr = '',
    this.simboloMoneda = '',
    this.idCodigoQr = '',
    this.filtroContactos = const [],
    this.contactoSeleccionada,
    this.listaContactos = const [],
    this.datosAfiliacion,
    this.listaEntidadesFinancieras = const [],
    this.entidadFinancieraSeleccionada,
    this.datosClienteOrigenResponse,
    this.datosOperacionResponse,
    this.detalleTransferencia,
    this.montosTotales,
    this.tokenResponse,
    this.montoTransferencia = const MontoTransferir.pure(''),
    this.montoDisponible = 0.00,
    this.limiteMontoMaximo = 0.00,
    this.montoMaximoDia = 0.00,
    this.numeroCelular = const NumeroCelular.pure(''),
    this.cargandoContactos,
    this.sinContactos,
    this.permisoDenegado,
  });

  TransferenciaCelularState copyWith({
    String? search,
    String? tokenDigital,
    String? tokenCodigoQr,
    String? idCodigoQr,
    String? simboloMoneda,
    MontoTransferir? montoTransferencia,
    double? montoDisponible,
    double? limiteMontoMaximo,
    double? montoMaximoDia,
    NumeroCelular? numeroCelular,
    List<ContactosBarrido>? listaContactos,
    List<ContactosBarrido>? filtroContactos,
    List<EntidadesReceptorAfiliado>? listaEntidadesFinancieras,
    ValueGetter<ContactosBarrido?>? contactoSeleccionada,
    ValueGetter<EntidadesReceptorAfiliado?>? entidadFinancieraSeleccionada,
    ValueGetter<TokenResponse?>? tokenResponse,
    ValueGetter<DatosClienteOrigenResponse?>? datosClienteOrigenResponse,
    ValueGetter<DetalleTransferencia?>? detalleTransferencia,
    ValueGetter<MontosTotales?>? montosTotales,
    ValueGetter<DatosAfiliacionResponse?>? datosAfiliacion,
    ValueGetter<DatosOperacionExitosaResponse?>? datosOperacionResponse, 
    bool? cargandoContactos, 
    bool? sinContactos, 
    bool? permisoDenegado,
  }) =>
      TransferenciaCelularState(
        search: search ?? this.search,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        tokenCodigoQr: tokenCodigoQr ?? this.tokenCodigoQr,
        idCodigoQr: idCodigoQr ?? this.idCodigoQr,
        simboloMoneda: simboloMoneda ?? this.simboloMoneda,
        montoTransferencia: montoTransferencia ?? this.montoTransferencia,
        montoDisponible: montoDisponible ?? this.montoDisponible,
        limiteMontoMaximo: limiteMontoMaximo ?? this.limiteMontoMaximo,
        montoMaximoDia: montoMaximoDia ?? this.montoMaximoDia,
        numeroCelular: numeroCelular ?? this.numeroCelular,
        listaContactos: listaContactos ?? this.listaContactos,
        filtroContactos: filtroContactos ?? this.filtroContactos,
        contactoSeleccionada: contactoSeleccionada != null
            ? contactoSeleccionada()
            : this.contactoSeleccionada,
        listaEntidadesFinancieras:
            listaEntidadesFinancieras ?? this.listaEntidadesFinancieras,
        entidadFinancieraSeleccionada: entidadFinancieraSeleccionada != null
            ? entidadFinancieraSeleccionada()
            : this.entidadFinancieraSeleccionada,
        tokenResponse:
            tokenResponse != null ? tokenResponse() : this.tokenResponse,
        datosClienteOrigenResponse: datosClienteOrigenResponse != null
            ? datosClienteOrigenResponse()
            : this.datosClienteOrigenResponse,
        detalleTransferencia: detalleTransferencia != null
            ? detalleTransferencia()
            : this.detalleTransferencia,
        montosTotales:
            montosTotales != null ? montosTotales() : this.montosTotales,
        datosAfiliacion:
            datosAfiliacion != null ? datosAfiliacion() : this.datosAfiliacion,
        datosOperacionResponse: datosOperacionResponse != null
            ? datosOperacionResponse()
            : this.datosOperacionResponse,
        cargandoContactos: cargandoContactos ?? this.cargandoContactos,
        sinContactos: sinContactos ?? this.sinContactos,
        permisoDenegado: permisoDenegado ?? this.permisoDenegado,
      );
}
