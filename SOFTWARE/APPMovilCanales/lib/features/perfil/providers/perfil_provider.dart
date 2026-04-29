import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/storage_keys.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/core/services/core_service.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/perfil/inputs/apodo.dart';
import 'package:caja_tacna_app/features/perfil/inputs/email.dart';
import 'package:caja_tacna_app/features/perfil/inputs/numero_celular.dart';
import 'package:caja_tacna_app/features/perfil/services/perfil_service.dart';
import 'package:caja_tacna_app/features/perfil/states/perfil_state.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/services/storage_service.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:formz/formz.dart';

final perfilProvider =
    NotifierProvider<PerfilNotifier, PerfilState>(() => PerfilNotifier());

class PerfilNotifier extends Notifier<PerfilState> {
  @override
  PerfilState build() {
    return PerfilState();
  }

  initDatos() async {
    getApodo();
    state = state.copyWith(
      numeroCelular: const NumeroCelular.pure(''),
      email: const Email.pure(''),
      tokenDigital: '',
      confirmarResponse: () => null,
      actualizarResponse: () => null,
      apodo: const Apodo.pure(''),
      datosIniciales: () => null,
    );
  }

  getApodo() async {
    final Map<String, dynamic> apodos = await getApodos();
    String? apodo = apodos[ref.read(loginProvider).numeroTarjeta.value];
    if (apodo == null) {
      apodo = CtUtils.capitalize(
        ref.read(homeProvider).datosCliente?.nombres ?? '',
        allWords: true,
      );
    } else {
      apodo = apodo.toString();
    }

    state = state.copyWith(
      apodoLocal: apodo,
    );
  }

  getDatosIniciales() async {
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    try {
      final response = await PerfilService.obtenerDatos();

      state = state.copyWith(
          datosIniciales: () => response,
          tieneEmail: response.correoElectronico.isNotEmpty,
          email: response.correoElectronico.isNotEmpty
              ? Email.pure('')
              : Email.dirty(response.correoElectronico));
    } on ServiceException catch (e) {
      ref.read(appRouterProvider).pop();
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
    ref.read(loaderProvider.notifier).dismissLoader();
  }

  Future<Map<String, dynamic>> getApodos() async {
    final apodos =
        await StorageService().get<Map<String, dynamic>>(StorageKeys.apodos);
    if (apodos != null) {
      return apodos;
    }

    return {};
  }

  changeNumeroCelular(NumeroCelular numeroCelular) {
    state = state.copyWith(
      numeroCelular: numeroCelular,
    );
  }

  changeEmail(Email email) {
    state = state.copyWith(
      email: email,
    );
  }

  changeApodo(Apodo apodo) {
    state = state.copyWith(
      apodo: apodo,
    );
  }

  actualizarApodo() async {
    if (state.apodo.value.isEmpty) return;
    final Map<String, dynamic> apodos = await getApodos();

    final Map<String, String> nuevosApodos = {
      ...apodos,
      ref.read(loginProvider).numeroTarjeta.value: state.apodo.value
    };
    await StorageService()
        .set<Map<String, String>>(StorageKeys.apodos, nuevosApodos);
    state = state.copyWith(
      apodoLocal: state.apodo.value,
      apodo: const Apodo.pure(''),
    );

    ref
        .read(snackbarProvider.notifier)
        .showSnackbar('Actualizado', SnackbarType.info);
  }

  actualizar({required bool withPush}) async {
    FocusManager.instance.primaryFocus?.unfocus();
    await actualizarApodo();
    if (state.email.value.isEmpty && state.numeroCelular.value.isEmpty) return;

    final email = Email.dirty(state.tieneEmail
        ? state.datosIniciales?.correoElectronico ?? ''
        : state.email.value);
    final numeroCelular = NumeroCelular.dirty(state.numeroCelular.value);

    state = state.copyWith(
      email: email,
      numeroCelular: numeroCelular,
    );

    if (!Formz.validate([email, numeroCelular])) return;
    ref.read(loaderProvider.notifier).showLoader(withOpacity: true);

    resetToken();
    try {
      final response = await PerfilService.actualizar(
        correoElectronico: state.email.value.isEmpty
            ? state.datosIniciales?.correoElectronico
            : state.email.value,
        numeroCelular: state.numeroCelular.value.isEmpty
            ? state.datosIniciales?.numeroTelefonoCasa
            : state.numeroCelular.value,
        identificadorDispositivo: ref
            .read(dispositivoProvider.notifier)
            .getIdentificadorDispositivo(),
      );

      state = state.copyWith(
        actualizarResponse: () => response,
        tokenDigital: await CoreService.desencriptarToken(
          response.codigoSolicitado,
        ),
      );
      initTimer();

      if (withPush) {
        ref.read(appRouterProvider).push('/perfil/confirmar');
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
      final response = await PerfilService.confirmar(
        tokenDigital: state.tokenDigital,
        correoElectronico: state.email.value.isEmpty
            ? state.datosIniciales?.correoElectronico
            : state.email.value,
        numeroCelular: state.numeroCelular.value.isEmpty
            ? state.datosIniciales?.numeroTelefonoCasa
            : state.numeroCelular.value,
      );

      state = state.copyWith(
        confirmarResponse: () => response,
        tieneEmail: true,
      );
      ref.read(homeProvider.notifier).getCuentas();
      ref.read(appRouterProvider).push('/perfil/actualizacion-exitosa');
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
          initDate: state.actualizarResponse?.fechaSistema,
          date: state.actualizarResponse?.fechaVencimiento,
        );
  }

  resetToken() {
    state = state.copyWith(
      tokenDigital: '',
    );
  }

  changeToken(String tokenDigital) {
    state = state.copyWith(
      tokenDigital: tokenDigital,
    );
  }

  changeCorreoDestinatario(Email correo) {
    state = state.copyWith(
      email: correo,
    );
  }
}
