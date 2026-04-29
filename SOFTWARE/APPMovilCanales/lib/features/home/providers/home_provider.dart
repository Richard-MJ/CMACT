import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/services/afiliacion_celular_service.dart';
import 'package:caja_tacna_app/features/home/services/home_service.dart';
import 'package:caja_tacna_app/features/home/states/home_state.dart';
import 'package:caja_tacna_app/features/novedades/services/publicidad_service.dart';
import 'package:caja_tacna_app/features/providers.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_status_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/token_digital/widgets/dialog_publicidad.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/transferencias/index.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:url_launcher/url_launcher.dart';

final homeProvider =
    NotifierProvider<HomeNotifier, HomeState>(() => HomeNotifier());

class HomeNotifier extends Notifier<HomeState> {
  @override
  HomeState build() {
    return HomeState();
  }

  final storageService = StorageService();

  Future<void> initData() async {
    state = state.copyWith(
      creditos: [],
      cuentasAhorro: [],
      loadingCreditos: true,
      loadingCuentasAhorro: true,
      loadingDatosCliente: true,
      loadingTipoCambio: true,
      tipoCambioCompra: 0.00,
      tipoCambioVenta: 0.00,
      datosCliente: () => null,
    );

    var mostrarSaldo =
        await storageService.get<bool>(StorageKeys.mostrarSaldo) ?? false;

    state = state.copyWith(mostrarSaldo: mostrarSaldo);

    cargaPrimeraVez = true;
  }

  getHomeData() async {
    if (cargando) return;
    cargando = true;

    getAfiliacionBilleteraVirtual(withLoading: cargaPrimeraVez);
    getConfiguracion();
    getCuentas(withLoading: cargaPrimeraVez);
    getTipoCambio(withLoading: cargaPrimeraVez);
    getCreditos(withLoading: cargaPrimeraVez);

    await getDatosCliente(withLoading: cargaPrimeraVez);

    cargaPrimeraVez = false;
    cargando = false;
  }

  bool cargaPrimeraVez = true;
  bool cargando = false;

  Future<void> getCuentas({bool withLoading = false}) async {
    state = state.copyWith(
      loadingCuentasAhorro: withLoading,
    );
    try {
      final cuentasAhorro = await HomeService.getCuentas();

      state = state.copyWith(
        cuentasAhorro: cuentasAhorro,
      );

    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingCuentasAhorro: false,
    );
  }

  Future<void> getCreditos({bool withLoading = false}) async {
    state = state.copyWith(
      loadingCreditos: withLoading,
    );
    try {
      final creditos = await HomeService.getCreditos();

      state = state.copyWith(
        creditos: creditos,
      );

    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingCreditos: false,
    );
  }

  Future<void> getTipoCambio({bool withLoading = false}) async {
    state = state.copyWith(
      loadingTipoCambio: withLoading,
    );
    try {
      final tipoCambioResponse = await HomeService.getTasaCambio();

      state = state.copyWith(
        tipoCambioCompra: tipoCambioResponse.montoCompra,
        tipoCambioVenta: tipoCambioResponse.montoVenta,
      );

    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingTipoCambio: false,
    );
  }

  Future<void> getDatosCliente({bool withLoading = false}) async {
    state = state.copyWith(
      loadingDatosCliente: withLoading,
    );
    try {
      final datosCliente = await HomeService.getDatosCliente();

      state = state.copyWith(
        datosCliente: () => datosCliente,
      );

      _comprobarEmail();
      ref.read(perfilProvider.notifier).getApodo();

      await checkPendingRedirect();
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    state = state.copyWith(
      loadingDatosCliente: false,
    );
  }

  Future<void> checkPendingRedirect() async {
    final authNotifier = ref.read(authStatusProvider.notifier);
    final pendingPath = authNotifier.hasPendingRedirect()
        ? authNotifier.consumePendingRedirect()
        : null;

    if (pendingPath != null && state.datosCliente != null) {
      ref.read(appRouterProvider).push(pendingPath);
    }
  }

  Future<void> cargarPublicidades(BuildContext context) async {
    try {
      final publicidades =
          await PublicidadService.obtenerPublicidadCategoria(categoria: 0);

      state = state.copyWith(
        publicidades: publicidades,
      );

      if (publicidades.isEmpty) return;
      if (!context.mounted) return;
      await showDialog(
        context: context,
        builder: (BuildContext context) {
          return const DialogPublicidad();
        },
      );
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  Future<void> getConfiguracion() async {
    try {
      final configuracion = await HomeService.getConfiguracion();

      state = state.copyWith(
        configuracion: () => configuracion,
      );

    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  void resetearProviders() async {
    ref.read(adelantoSueldoProvider.notifier).initDatos();
    ref.read(anulacionTarjetasProvider.notifier).initDatos();
    ref.read(aperturaCuentasProvider.notifier).initDatos();
    ref.read(biometriaProvider.notifier).initDatos();
    ref.read(cambioClaveProvider.notifier).initDatos();
    ref.read(cancelacionCuentasProvider.notifier).initDatos();
    ref.read(comprasInternetProvider.notifier).initDatos();
    ref.read(configurarCuentasProvider.notifier).initDatos();
    ref.read(emisionGirosProvider.notifier).initDatos();
    ref.read(pagoCreditoPropioProvider.notifier).initDatosMenu();
    ref.read(pagoCreditoPropioProvider.notifier).initDatosCuenta();
    ref.read(pagoCreditoTercerosProvider.notifier).initDatos();
    ref.read(pagoSafetypayProvider.notifier).initDatos();
    ref.read(pagoServiciosProvider.notifier).initDatos();
    ref.read(pagoTarjetasCreditoDiferidaProvider.notifier).initDatos();
    ref.read(perfilProvider.notifier).initDatos();
    ref.read(recargaVirtualProvider.notifier).initDatos();
    ref.read(tokenDigitalProvider.notifier).initDatos();
    ref.read(afiliacionCelularProvider.notifier).initDatos();
    ref.read(transferenciaCelularProvider.notifier).initDatos();
    ref.read(transferenciaEntreMisCuentasProvider.notifier).initDatos();
    ref.read(transferenciaInterbancariaDiferidaProvider.notifier).initDatos();
    ref.read(transferenciaTercerosProvider.notifier).initDatos();
    ref.read(sesionCanalElectronicoProvider.notifier).initDatos();
    ref.read(solicitudCrediticiaProvider.notifier).initDatos();
  }

  changeMostrarSaldo() {
    var mostrarSaldo = !state.mostrarSaldo;
    state = state.copyWith(mostrarSaldo: mostrarSaldo);
    storageService.set(StorageKeys.mostrarSaldo, mostrarSaldo);
  }

  changeCargaPrimeraVez({required bool cargar}) {
    cargaPrimeraVez = cargar;
  }

  abrirWeb() async {
    if (state.datosCliente!.indicadorMenorEdad) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar("No puede realizar esta operación", SnackbarType.error);
      return;
    }

    final Uri url = Uri.parse('https://cmactacna.com.pe/');
    if (await canLaunchUrl(url)) {
      launchUrl(url, mode: LaunchMode.externalApplication);
    }
  }

  void _comprobarEmail() {
    final tieneEmail =
        state.datosCliente?.correoElectronico.isNotEmpty ?? false;

    if (!tieneEmail && cargaPrimeraVez) {
      ref.read(appRouterProvider).push(('/perfil/datos'));
    }
  }

  Future<void> getAfiliacionBilleteraVirtual({bool withLoading = false}) async {
    try {
      final bool afiliacionBilleteraVirtual =
          await AfiliacionCelularService.obtenerAfiliacionBilleteraVirtual();

      state = state.copyWith(
        esAfiliadoBilleteraVirtual: afiliacionBilleteraVirtual,
      );

      if (withLoading && afiliacionBilleteraVirtual) {
        ref.read(loaderProvider.notifier).showLoader(withOpacity: true);
        await ref
            .read(transferenciaCelularProvider.notifier)
            .obtenerDatosIniciales();
      }

    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    } finally {
      ref.read(loaderProvider.notifier).dismissLoader();
    }
  }
}
