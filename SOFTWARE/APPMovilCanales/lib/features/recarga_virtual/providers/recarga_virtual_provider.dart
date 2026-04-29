import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/services/operaciones_frecuentes_service.dart';
import 'package:caja_tacna_app/features/recarga_virtual/inputs/monto_recarga.dart';
import 'package:caja_tacna_app/features/recarga_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/confirmar_response.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/pagar_response.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/operador_movile.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/cuenta_origen.dart';
import 'package:caja_tacna_app/features/recarga_virtual/services/recarga_virtual_service.dart';
import 'package:caja_tacna_app/features/shared/inputs/email.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/shared_services.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/operador_movil_kasnet.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/obtener_cobro_servicio_response.dart';
import 'package:caja_tacna_app/features/pago_servicios/models/pagar_response.dart' as pagoServiciosKasnet;
import 'package:caja_tacna_app/features/pago_servicios/models/confirmar_response.dart' as confirmaServiciosKasnet;
import 'package:caja_tacna_app/features/pago_servicios/services/pago_servicios_services.dart';

final recargaVirtualProvider =
    StateNotifierProvider<RecargaVirtualNotifier, RecargaVirtualState>((ref) {
  return RecargaVirtualNotifier(ref);
});

class RecargaVirtualNotifier extends StateNotifier<RecargaVirtualState> {
  RecargaVirtualNotifier(this.ref) : super(RecargaVirtualState());

  final Ref ref;
  String? _ultimoNumeroValidado;

  initDatos() {
    state = state.copyWith(
      cuentasOrigen: [],
      cuentaOrigen: () => null,
      operadores: [],
      operador: () => null,
      numeroCelular: const NumeroCelular.pure(''),
      montoRecarga: const MontoRecarga.pure(''),
      correoElectronicoDestinatario: const Email.pure(''),
      tokenDigital: '',
      operacionFrecuente: false,
      nombreOperacionFrecuente: '',
      confirmarResponse: () => null,
      pagarResponse: () => null,
      operadoresKasnet: [],
    	operadorKasnet: () => null,
      pagarResponseKasnet: () => null,
      servicioExterno: 2
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final DatosInicialesResponse datosInicialesResponse =
          await RecargaVirtualService.obtenerDatosIniciales();

      state = state.copyWith(
        cuentasOrigen: datosInicialesResponse.productoParaSeleccionar,
        operadores: datosInicialesResponse.operadorMovil,
        operadoresKasnet: datosInicialesResponse.operadorMovilKasnet,
      );
      autoCompletarOpFrecuente();
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  autoCompletarOpFrecuente() {
    try {
      final operacionFrecuente =
          ref.read(operacionesFrecuentesProvider).operacionSeleccionada;

      if (operacionFrecuente == null) return;

      final indexCuentaOrigen = state.cuentasOrigen.indexWhere(
          (cuenta) => cuenta.numeroProducto == operacionFrecuente.numeroCuenta);

      if (indexCuentaOrigen >= 0) {
        state = state.copyWith(
          cuentaOrigen: () => state.cuentasOrigen[indexCuentaOrigen],
        );
      }

      final indexOperador = state.operadores.indexWhere((operador) =>
          operador.codigoOperador ==
          operacionFrecuente.operacionesFrecuenteDetalle.codigoOperador);

      if (indexOperador >= 0) {
        state = state.copyWith(
          operador: () => state.operadores[indexOperador],
        );
      }

      state = state.copyWith(
        numeroCelular: NumeroCelular.pure(
            operacionFrecuente.operacionesFrecuenteDetalle.numeroCelular ?? ''),
      );

      ref.read(operacionesFrecuentesProvider.notifier).resetOperacion();
    } catch (e) {
      throw ServiceException('Ocurrió un error al cargar la operación');
    }
  }

  changeProducto(CuentaOrigenRV cuenta) {
    state = state.copyWith(
      cuentaOrigen: () => cuenta,
      montoRecarga: state.montoRecarga.copyWith(saldoMaximo: cuenta.montoSaldo),
    );
  }

  changeOperador(OperadorMovilKasnet operador) {
    state = state.copyWith(
      operadorKasnet: () => operador,
    );
  }

  changeNumeroCelular(NumeroCelular numeroCelular) {
    state = state.copyWith(
      numeroCelular: numeroCelular,
    );

	  if (state.numeroCelular.value.length < 9) {
		  _ultimoNumeroValidado = null;
		return;
	  }

    if (state.numeroCelular.value.length == 9 && state.numeroCelular.value != _ultimoNumeroValidado) {
		  _ultimoNumeroValidado = state.numeroCelular.value;
		  this.obtenerDeuda();
		}    
  }

  obtenerDeuda() async{
      
      try {
      final ObtenerCobroServicioResponse cobroResponse =
          await PagoServiciosService.obtenerCobroServicio(
      suministro: state.numeroCelular.value,
      codigoEmpresa: state.operadorKasnet!.codigoEmpresa,
      codigoServicio: state.operadorKasnet!.idDetalleEmpresaKasnet,
      codigoGrupoEmpresa: state.operadorKasnet!.codigoGrupoEmpresa.toInt(),
      tipoPagoServicio: state.servicioExterno,        
      codigoCategoria: state.operadorKasnet!.codigoCategoria.toInt(),       
      nombreCategoria: state.operadorKasnet!.nombreCategoria,
      nombreEmpresa: state.operadorKasnet!.nombreEmpresa,        
      nombreServicio: state.operadorKasnet!.nombreEmpresa,
      );        

      var montoRecarga = cobroResponse.montoDeuda;
          
      MontoRecarga montoRecargas = MontoRecarga.dirty(
              montoRecarga.toString(),              
          );
      changeMontoRecarga(montoRecargas);    

      state = state.copyWith(
        obtenerCobroServicioResponse : () => cobroResponse,
      );


      FocusManager.instance.primaryFocus?.unfocus();

      } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);        
      }
      ref.read(loaderProvider.notifier).dismissLoader();
  }  

  changeMontoRecarga(MontoRecarga montoRecarga) {
    state = state.copyWith(
      montoRecarga: montoRecarga,
    );
  }

  pagar({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.cuentaOrigen == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione la cuenta de origen', SnackbarType.error);
      return;
    }

    if (state.operadorKasnet == null) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Seleccione el operador', SnackbarType.error);
      return;
    }

    final montoRecarga = state.montoRecarga.copyWith(isPure: false);
    final numeroCelular = NumeroCelular.dirty(state.numeroCelular.value);

    state = state.copyWith(
      montoRecarga: montoRecarga,
      numeroCelular: numeroCelular,
    );

    if (!Formz.validate([montoRecarga, numeroCelular])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final pagoServiciosKasnet.PagarResponse pagarResponseKasnet =
          await PagoServiciosService.pagarServicio(            
        codigoMonedaDeuda: state.cuentaOrigen?.codigoMonedaProducto,
        montoDeuda: double.parse(state.montoRecarga.value),
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),        
      );

      state = state.copyWith(
        pagarResponseKasnet: () => pagarResponseKasnet,
        tokenDigital: await CoreService.desencriptarToken(
          pagarResponseKasnet.codigoSolicitado,
        ),
      );

      initTimer();
      if (withPush) {        
        ref.read(appRouterProvider).push('/recarga-virtual/confirmar');
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }

    ref.read(loaderProvider.notifier).dismissLoader();
  }

  confirmar() async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.tokenDigital.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese su Token Digital', SnackbarType.error);
      return;
    }
    if (state.tokenDigital.length != 6) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El token debe tener 6 dígitos', SnackbarType.error);
      return;
    }

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    
    try {
      final confirmaServiciosKasnet.ConfirmarResponse confirmarResponseKasnet =
          await PagoServiciosService.confirmarServicio(
        tokenDigital: state.tokenDigital,
        codigoEmpresa: state.operadorKasnet?.codigoEmpresa,
        codigoMonedaDeuda: state.obtenerCobroServicioResponse?.codigoMoneda,
        codigoServicio: state.operadorKasnet!.idDetalleEmpresaKasnet,
        comisionDeuda: state.obtenerCobroServicioResponse?.comisionDeuda,
        montoDeuda: state.montoRecarga.value,
        moraDeuda: state.obtenerCobroServicioResponse?.moraDeuda,
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        numeroRecibo: state.obtenerCobroServicioResponse?.numeroRecibo,
        tipoPagoServicio: state.servicioExterno,
      );

      state = state.copyWith(
        confirmarResponseKasnet: () => confirmarResponseKasnet,
      );
      ref.read(homeProvider.notifier).getCuentas();
      agregarOperacionFrecuente();
      ref.read(appRouterProvider).push('/recarga-virtual/recarga-exitosa');      
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
      ref.read(timerProvider.notifier).cancelTimer();
      resetToken();
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
          onFinish: () {
            resetToken();
          },
          initDate: state.pagarResponse?.fechaSistema,
          date: state.pagarResponse?.fechaVencimiento,
        );
  }

  agregarOperacionFrecuente() async {
    try {
      if (!state.operacionFrecuente) return;
      await OperacionesFrecuentesService.agregarRecargaVirtual(
        numeroCuentaOrigen: state.cuentaOrigen?.numeroProducto,
        nombreOperacionFrecuente: state.nombreOperacionFrecuente,
        codigoOperador: state.operador?.codigoOperador,
        numeroCelular: state.numeroCelular.value,
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeNombreOperacionFrecuente(String nombreOperacionFrecuente) {
    state = state.copyWith(
      nombreOperacionFrecuente: nombreOperacionFrecuente,
    );
  }

  toggleOperacionFrecuente() {
    state = state.copyWith(
      operacionFrecuente: !state.operacionFrecuente,
      nombreOperacionFrecuente: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      correoElectronicoDestinatario: correo,
    );
  }

  reenviarComprobante() async {
    FocusManager.instance.primaryFocus?.unfocus();

    final correoElectronicoDestinatario =
        Email.dirty(state.correoElectronicoDestinatario.value);
    state = state.copyWith(
      correoElectronicoDestinatario: correoElectronicoDestinatario,
    );

    if (!Formz.validate([correoElectronicoDestinatario])) return;

    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      await SharedService.reenviarComprobante(
        tipoOperacion: "8",
        correoElectronicoDestinatario:
            state.correoElectronicoDestinatario.value,
        idOperacionTts: state.confirmarResponse?.idOperacionTts,
      );

      ref.read(snackbarProvider.notifier).showSnackbar(
            'Correo enviado con éxito',
            SnackbarType.floating,
          );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }
}

class RecargaVirtualState {
  final List<CuentaOrigenRV> cuentasOrigen;
  final List<OperadorMovil> operadores;
  final CuentaOrigenRV? cuentaOrigen;
  final OperadorMovil? operador;
  final NumeroCelular numeroCelular;
  final MontoRecarga montoRecarga;
  final String tokenDigital;
  final bool operacionFrecuente;
  final String nombreOperacionFrecuente;
  final Email correoElectronicoDestinatario;
  final PagarResponse? pagarResponse;
  final ConfirmarResponse? confirmarResponse;
  final List<OperadorMovilKasnet> operadoresKasnet;
	final OperadorMovilKasnet? operadorKasnet;
  final pagoServiciosKasnet.PagarResponse? pagarResponseKasnet;
	final ObtenerCobroServicioResponse? obtenerCobroServicioResponse;  
  final confirmaServiciosKasnet.ConfirmarResponse? confirmarResponseKasnet;
  final int servicioExterno;

  RecargaVirtualState({
    this.cuentasOrigen = const [],
    this.operadores = const [],
    this.cuentaOrigen,
    this.operador,
    this.numeroCelular = const NumeroCelular.pure(''),
    this.montoRecarga = const MontoRecarga.pure(''),
    this.tokenDigital = '',
    this.operacionFrecuente = false,
    this.nombreOperacionFrecuente = '',
    this.correoElectronicoDestinatario = const Email.pure(''),
    this.pagarResponse,
    this.confirmarResponse,
    this.operadoresKasnet = const [],
	  this.operadorKasnet,
    this.pagarResponseKasnet,
	  this.obtenerCobroServicioResponse,
    this.confirmarResponseKasnet,
    this.servicioExterno = 2    
  });

  RecargaVirtualState copyWith({
    List<CuentaOrigenRV>? cuentasOrigen,
    List<OperadorMovil>? operadores,
    ValueGetter<CuentaOrigenRV?>? cuentaOrigen,
    ValueGetter<OperadorMovil?>? operador,
    NumeroCelular? numeroCelular,
    MontoRecarga? montoRecarga,
    String? tokenDigital,
    bool? operacionFrecuente,
    String? nombreOperacionFrecuente,
    Email? correoElectronicoDestinatario,
    ValueGetter<PagarResponse?>? pagarResponse,
    ValueGetter<ConfirmarResponse?>? confirmarResponse,
    List<OperadorMovilKasnet>? operadoresKasnet,
	  ValueGetter<OperadorMovilKasnet?>? operadorKasnet,
    ValueGetter<pagoServiciosKasnet.PagarResponse?>? pagarResponseKasnet,
    ValueGetter<ObtenerCobroServicioResponse?>? obtenerCobroServicioResponse,
    ValueGetter<confirmaServiciosKasnet.ConfirmarResponse?>? confirmarResponseKasnet,
    int? servicioExterno
    
  }) =>
      RecargaVirtualState(
        cuentasOrigen: cuentasOrigen ?? this.cuentasOrigen,
        operadores: operadores ?? this.operadores,
        cuentaOrigen: cuentaOrigen != null ? cuentaOrigen() : this.cuentaOrigen,
        operador: operador != null ? operador() : this.operador,
        numeroCelular: numeroCelular ?? this.numeroCelular,
        montoRecarga: montoRecarga ?? this.montoRecarga,
        tokenDigital: tokenDigital ?? this.tokenDigital,
        operacionFrecuente: operacionFrecuente ?? this.operacionFrecuente,
        nombreOperacionFrecuente:
            nombreOperacionFrecuente ?? this.nombreOperacionFrecuente,
        correoElectronicoDestinatario:
            correoElectronicoDestinatario ?? this.correoElectronicoDestinatario,
        pagarResponse:
            pagarResponse != null ? pagarResponse() : this.pagarResponse,
        confirmarResponse: confirmarResponse != null
            ? confirmarResponse()
            : this.confirmarResponse,
          operadoresKasnet: operadoresKasnet ?? this.operadoresKasnet,
	        operadorKasnet: operadorKasnet != null ? operadorKasnet() : this.operadorKasnet,
        pagarResponseKasnet: 
            pagarResponseKasnet != null ? pagarResponseKasnet() : this.pagarResponseKasnet,
        obtenerCobroServicioResponse: obtenerCobroServicioResponse != null
            ? obtenerCobroServicioResponse()
            : this.obtenerCobroServicioResponse,   
        confirmarResponseKasnet: confirmarResponseKasnet != null
            ? confirmarResponseKasnet()
            : this.confirmarResponseKasnet,       
      );
}
