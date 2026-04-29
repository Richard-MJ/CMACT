import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/services/afiliacion_canales_electronicos_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_entidad_financiera_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/services/transferencia_celular_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_consulta_cuenta_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/datos_cliente_origen_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/dialog_invitar_cliente.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/montos_totales_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/models/datos_afiliacion_response.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/models/lista_contacto_barrido.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/transferencia_celular_state.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/inputs/monto_transferir.dart';
import 'package:caja_tacna_app/features/billetera_virtual/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_contacts/flutter_contacts.dart';
import 'package:flutter/material.dart';

final transferenciaCelularProvider =
    NotifierProvider<TransferenciaCelularNotifier, TransferenciaCelularState>(() => TransferenciaCelularNotifier());

class TransferenciaCelularNotifier extends Notifier<TransferenciaCelularState> {
  @override
  TransferenciaCelularState build() {
    return TransferenciaCelularState();
  }

  initDatos() {
    state = state.copyWith(
      search: '',
      idCodigoQr: '',
      tokenDigital: '',
      tokenCodigoQr: '',
      simboloMoneda: '',
      listaContactos: [],
      filtroContactos: [],
      montoMaximoDia: 0.00,
      montoDisponible: 0.00,
      limiteMontoMaximo: 0.00,
      tokenResponse: () => null,
      listaEntidadesFinancieras: [],
      entidadFinancieraSeleccionada: () => null,
      numeroCelular: const NumeroCelular.pure(''),
      montoTransferencia: const MontoTransferir.pure(''),
      cargandoContactos: false,
      permisoDenegado: false,
      sinContactos: false,
    );
  }  

  goTransferenciaCelular() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      initDatos();
      await obtenerDatosIniciales();
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref.read(snackbarProvider.notifier)
        .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerDatosIniciales() async {
    final DatosValidacionResponse datosValidacionResponse =
      await TransferenciaCelularService.obtenerDatosAfiliacion();

    state = state.copyWith(
      simboloMoneda: datosValidacionResponse.datosAfiliacion!.simboloMoneda,
      montoDisponible: datosValidacionResponse.datosAfiliacion!.saldoDisponible,
      datosAfiliacion: () => datosValidacionResponse.datosAfiliacion,
    );      
    return;  
  }

  listarContactos() async {
    try {
      state = state.copyWith(
        cargandoContactos: true,
        permisoDenegado: false,
        sinContactos: false,
      );

      if (!await FlutterContacts.requestPermission(readonly: true)) {
        state = state.copyWith(
          cargandoContactos: false,
          permisoDenegado: true,
        );
        return;
      }

      List<Contact> contactos = await FlutterContacts.getContacts(withProperties: true, withPhoto: true);

      if (contactos.isEmpty) {
        state = state.copyWith(
          cargandoContactos: false,
          sinContactos: true,
        );
        return;
      }

      formatearCelulares(contactos);

      state = state.copyWith(cargandoContactos: false);
    } catch (e) {
      state = state.copyWith(cargandoContactos: false);
    }
  }

  initTimer() {
    ref.read(timerProvider.notifier).initDateTimer(
      onFinish: () {
        resetTokenDigital();
      },
      initDate: state.tokenResponse?.fechaSistema,
      date: state.tokenResponse?.fechaVencimiento,
    );
  }

  transferir({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
        final DatosConsultaCuentaResponse datosConsultaCuenta = 
          await TransferenciaCelularService.consultaCuentaClienteReceptor(
          numeroCelular: state.contactoSeleccionada?.numeroCelular.replaceAll(RegExp(r'\s+|-'),''),
          codigoEntidad: state.entidadFinancieraSeleccionada?.codigoEntidad,
          cuentaEfectivo: state.datosClienteOrigenResponse?.cuentaEfectivo
        );

        state = state.copyWith(
          detalleTransferencia: () => datosConsultaCuenta.detalleTransferencia
        );

        await obtenerMontosYToken(withPush: withPush);
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  obtenerMontosYToken({required bool withPush}) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
        final MontosTotales montosTotales = await TransferenciaCelularService.calcularMontosTotales(
          numeroCuenta: state.datosClienteOrigenResponse?.cuentaEfectivo.numeroCuenta,
          mismoTitular: state.detalleTransferencia?.mismoTitular, 
          saldoActual: state.datosClienteOrigenResponse?.cuentaEfectivo.saldoDisponible, 
          montoOperacion: state.montoTransferencia.value,
          montoMinimoCuenta: state.datosClienteOrigenResponse?.cuentaEfectivo.montoMinimo,
          esExoneradaItf: state.datosClienteOrigenResponse?.cuentaEfectivo.esExoneradaItf,
          esCuentaSueldo: state.datosClienteOrigenResponse?.cuentaEfectivo.esCuentaSueldo,
          comision: state.detalleTransferencia?.comision,
          esExoneradoComision: state.detalleTransferencia?.esExoneradoComision
        );

        final response = await TransferenciaCelularService.obtenerTokenDigital(
          codigoMonedaCuenta: state.detalleTransferencia?.codigoMoneda,
          identificadorDispositivo: ref
              .read(dispositivoProvider.notifier)
              .getIdentificadorDispositivo(),
        );
        
        response.codigoSolicitado = await CoreService
          .desencriptarToken(response.codigoSolicitado);

        state = state.copyWith(
          tokenDigital: response.codigoSolicitado,
          tokenResponse: () => response,
          montosTotales: () => montosTotales,
        );

        initTimer();   
        if(withPush){
          ref.read(appRouterProvider).push('/billetera-virtual/transferencia-celular/confirmar-transferencia');
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
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    if (state.tokenDigital.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese su token digital', SnackbarType.error);
      return;
    }
    if (state.tokenDigital.length != 6) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El token debe tener 6 dígitos', SnackbarType.error);
      return;
    }
    ref.read(timerProvider.notifier).cancelTimer();    

    try {
      await TransferenciaCelularService.validarToken(
        numeroVerificacion: state.tokenDigital);
        
      final datosOperacionResponse = await TransferenciaCelularService.confirmarTransferencia(        
        numeroCuenta: state.datosAfiliacion?.numeroCuentaAfiliada,
        controlMonto: state.montosTotales?.controlMonto,
        detalleTransferencia: state.detalleTransferencia,
        identificadorQR: state.idCodigoQr,
        numeroTarjeta: await storageService.get<String>(StorageKeys.numeroTarjeta) ?? '',
        entidadDestino: state.entidadFinancieraSeleccionada?.nombreEntidad,
        celularOriginante: state.datosAfiliacion?.numeroCelular,
        celularReceptor: state.contactoSeleccionada?.numeroCelular.replaceAll(RegExp(r'\s+|-'),'')
      );

      state = state.copyWith(
        datosOperacionResponse: () => datosOperacionResponse,
      );

      ref.read(appRouterProvider).push('/billetera-virtual/transferencia-celular/transferencia-exitosa');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  selectContacto(ContactosBarrido contacto, BuildContext context) async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
    try {
      await resetDatosTransferencia();
      state = state.copyWith(
        contactoSeleccionada: () => contacto,
      );
      
      final DatosRespuestaBarrido barridoResponse = await TransferenciaCelularService.barridoContacto(
        codigoCCi: state.datosAfiliacion?.codigoCuentaInterbancario,
        numeroCelularOrigen: state.datosAfiliacion?.numeroCelular,
        numeroCelular: contacto.numeroCelular.replaceAll(RegExp(r'\s+|-'),''), 
        nombreAlias: contacto.nombreAlias);

      final DatosClienteOrigenResponse datosClienteOriginante =
         await TransferenciaCelularService.obtenerDatosCuentaOriginante(
            state.datosAfiliacion!.numeroCuentaAfiliada
          );

      if(barridoResponse.resultadosBarridos[0].entidadesFinancieras.isEmpty){
        ref.read(loaderProvider.notifier).dismissLoader();
        if (!context.mounted) return;
        bool? continuar = await showDialog(
          context: context,
          builder: (BuildContext context) {
            return const DialogInvitarCliente();
          },
        );
        if (continuar == null || !continuar) return;
      }

      List<EntidadesReceptorAfiliado> listaEntidadesFinancieras = [];
      listaEntidadesFinancieras.addAll(
        barridoResponse.resultadosBarridos.expand((barrido) => barrido.entidadesFinancieras)
      );

      state = state.copyWith(
        montoTransferencia: MontoTransferir.pure('',
          montoMaximo: barridoResponse.limiteMontoMaximo,
          montoMinimo: barridoResponse.limiteMontoMinimo),
        montoMaximoDia: barridoResponse.montoMaximoDia,
        limiteMontoMaximo: barridoResponse.limiteMontoMaximo,
        listaEntidadesFinancieras: listaEntidadesFinancieras,
        datosClienteOrigenResponse: () => datosClienteOriginante
      );      

      ref.read(appRouterProvider).push('/billetera-virtual/transferencia-celular/transferir');
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  buscarContacto(BuildContext context) async {
    FocusManager.instance.primaryFocus?.unfocus();

    if (state.numeroCelular.value.isEmpty) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('Ingrese su número de celular', SnackbarType.error);
      return;
    }
    
    if (state.numeroCelular.value.length != 9) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar('El número de celular debe tener 9 dígitos', SnackbarType.error);
      return;
    }

    var contacto = ContactosBarrido(nombreAlias: "", numeroCelular: state.numeroCelular.value);

    selectContacto(contacto, context);
  }

  changeSearch(String search) {
    state = state.copyWith(
      search: search,
    );

    if(search.length >= 3 || search.isEmpty){
      var contactosFiltrados = state.listaContactos
        .where((x) {
          bool contacto = x.nombreAlias.toLowerCase().contains(search.toLowerCase());
          bool numeros = x.numeroCelular.replaceAll(RegExp(r'\s+|-'),'').contains(search.toLowerCase());
          return contacto || numeros;
        })
        .toList();

      state = state.copyWith(
        filtroContactos: contactosFiltrados,
      );
    }
  }

  formatearCelulares(List<Contact> contactos) {
    List<ContactosBarrido> contactosPeruanos = [];

    for (var contacto in contactos) {
      for (var celular in contacto.phones) {
        celular.number = celular.number.replaceAll(RegExp(r'\D'), '');

        if (celular.number.length == 11 && celular.number.startsWith('51')) {
          celular.number = celular.number.substring(2);
        }

        if (celular.number.length == 9) {
          celular.number = celular.number
              .replaceAllMapped(RegExp(r'.{1,3}'),
                  (match) => '${match.group(0)} ')
              .trim();

          contactosPeruanos.add(ContactosBarrido(
            nombreAlias: contacto.displayName,
            numeroCelular: celular.number,
          ));
        }
      }
    }

    state = state.copyWith(
      listaContactos: contactosPeruanos,
      filtroContactos: contactosPeruanos,
    );
  }

  lecturaCodigoQr(String? tokenCodigoQr) async{
    await resetDatosTransferencia();
    state = state.copyWith(
      tokenCodigoQr: tokenCodigoQr
    );
    
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await TransferenciaCelularService
        .obtenerDatosPorQR(
          numeroCuenta: state.datosAfiliacion!.numeroCuentaAfiliada,
          tokenCodigoQr: tokenCodigoQr);

      final DatosClienteOrigenResponse datosClienteOriginante =
         await TransferenciaCelularService.obtenerDatosCuentaOriginante(
            state.datosAfiliacion!.numeroCuentaAfiliada
          );

      var contactoSeleccionada = ContactosBarrido(
        nombreAlias: response.datosConsultaCuenta.detalleTransferencia.nombreReceptor, 
        numeroCelular: "");

      var entidadFinancieraSeleccionada = EntidadesReceptorAfiliado(
        codigoEntidad: response.datosConsultaCuenta.detalleTransferencia.codigoEntidadReceptora,
        nombreEntidad: response.nombreEntidadReceptora);

      List<EntidadesReceptorAfiliado> listaEntidadesFinancieras = [
        EntidadesReceptorAfiliado(
        codigoEntidad: response.datosConsultaCuenta.detalleTransferencia.codigoEntidadReceptora,
        nombreEntidad: response.nombreEntidadReceptora)
      ];

      state = state.copyWith(
        montoTransferencia: MontoTransferir.pure('',
          montoMaximo: response.limiteMontoMaximo,
          montoMinimo: response.limiteMontoMinimo),
        montoMaximoDia: response.montoMaximoDia,
        limiteMontoMaximo: response.limiteMontoMaximo,
        idCodigoQr: response.identificadorQR,
        entidadFinancieraSeleccionada: () => entidadFinancieraSeleccionada,
        listaEntidadesFinancieras: listaEntidadesFinancieras,
        contactoSeleccionada: () => contactoSeleccionada,
        detalleTransferencia: () => response.datosConsultaCuenta.detalleTransferencia,
        datosClienteOrigenResponse: () => datosClienteOriginante
      );

      ref.read(loaderProvider.notifier).dismissLoader();

      return true;
    } on ServiceException catch (e) {
      ref.read(loaderProvider.notifier).dismissLoader();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);

      return false;
    }
  }

  resetDatosTransferencia() async{
    state = state.copyWith(
      tokenCodigoQr: '',
      montoMaximoDia: 0.00,
      limiteMontoMaximo: 0.00,
      entidadFinancieraSeleccionada: () => null,
      montoTransferencia: const MontoTransferir.pure('')
    );
  }

  changeNumeroCelular(NumeroCelular numeroCelular) {
    state = state.copyWith(
      numeroCelular: numeroCelular,
    );
  }

  resetNumeroCelular() {
    state = state.copyWith(
      numeroCelular: const NumeroCelular.pure(''),
    );
  }

  changeMonto(MontoTransferir monto) {
    state = state.copyWith(
      montoTransferencia: monto,
    );
  }

  changeEntidadFinanciera(EntidadesReceptorAfiliado entidadFinanciera) {
    state = state.copyWith(
      entidadFinancieraSeleccionada: () => entidadFinanciera,
    );
  }

  resetTokenDigital() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  resetTokenQr() {
    state = state.copyWith(
      tokenCodigoQr: '',
      idCodigoQr: ''
    );
  }

  activarLecturaQrDesdeNavegacion({required bool afiliado}) {
    if (!afiliado) {
      ref.read(snackbarProvider.notifier).showSnackbar(
          'Debe estar afiliado a la billetera virtual',
          SnackbarType.error);
      return;
    }

    ref.read(appRouterProvider).push('/billetera-virtual/transferencia-celular/qr-scanner');
  }
}
