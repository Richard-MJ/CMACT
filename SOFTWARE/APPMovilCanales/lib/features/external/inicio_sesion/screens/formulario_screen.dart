import 'dart:async';

import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/core/providers/app_version_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_app_mantenimiento.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_app_version.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_internet.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/formulario_body.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/formulario_fab.dart';
import 'package:caja_tacna_app/features/external/providers/parametros_provider.dart';

import 'package:caja_tacna_app/features/shared/widgets/ct_exit_confirm.dart';
import 'package:connectivity_plus/connectivity_plus.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_native_splash/flutter_native_splash.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:visibility_detector/visibility_detector.dart';

class FormularioScreen extends ConsumerStatefulWidget {
  const FormularioScreen({super.key});

  @override
  FormularioScreenState createState() => FormularioScreenState();
}

class FormularioScreenState extends ConsumerState<FormularioScreen> {
  bool loginVisible = true;
  GlobalKey? dialogVersionamientoKey;
  GlobalKey? dialogInternetKey;
  late final AppLifecycleListener _listener;

  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(loginProvider.notifier).initFormularioScreen();
      verificarVersionApp();
    });
    //verificar cersion cuando la aplicacion pase de estar en segundo plano a primer
    _listener = AppLifecycleListener(
      onShow: () => verificarVersionApp(),
    );
  }

  @override
  void dispose() {
    _listener.dispose();
    super.dispose();
  }

  verificarVersionApp() async {
    //cerrar el modal si es que esta abierto
    if (dialogVersionamientoKey != null) {
      if (dialogVersionamientoKey!.currentContext != null) {
        Navigator.pop(dialogVersionamientoKey!.currentContext!);
      }
      dialogVersionamientoKey = null;
    }

    if (dialogInternetKey != null) {
      if (dialogInternetKey!.currentContext != null) {
        Navigator.pop(dialogInternetKey!.currentContext!);
      }
      dialogInternetKey = null;
    }

    final List<ConnectivityResult> connectivityResult =
        await (Connectivity().checkConnectivity());

    //verifica si hay internet
    bool dispositivoConInternet =
        connectivityResult.contains(ConnectivityResult.mobile) ||
            connectivityResult.contains(ConnectivityResult.wifi) ||
            connectivityResult.contains(ConnectivityResult.ethernet);

    if (!dispositivoConInternet) {
      //si es que no hay internet muestra el modal de internet

      FlutterNativeSplash.remove();
      if (rootNavigatorKey.currentContext == null) return;
      if (dialogInternetKey != null) return;
      dialogInternetKey = GlobalKey();

      if (!mounted) return;
      showDialog(
        barrierDismissible: false,
        context: context,
        builder: (context) {
          return PopScope(
            canPop: false,
            child: DialogInternet(
              key: dialogInternetKey,
            ),
          );
        },
      );

      return;
    } else {
      //si es que hay internet verifica la version de la app
      await ref.read(appVersionProvider.notifier).getAppVersion();
      await ref.read(parametrosProvider.notifier).getParametros();
      FlutterNativeSplash.remove();
    }

    final appVersionState = ref.read(appVersionProvider);
    if (!mounted) return;

    //si la aplicacion esta en matenimiento o si falla el servicio de verificacion de versionamiento
    if (appVersionState.versionamiento == null ||
        ('0.0.0' == appVersionState.versionamiento?.versionActual &&
            '0.0.0' == appVersionState.versionamiento?.versionPrueba)) {
      dialogVersionamientoKey = GlobalKey();

      showDialog(
        barrierDismissible: false,
        context: context,
        builder: (context) {
          return PopScope(
            canPop: false,
            child: DialogAppMantenimiento(
              key: dialogVersionamientoKey,
            ),
          );
        },
      );
    } else if (appVersionState.appVersion !=
            appVersionState.versionamiento?.versionActual &&
        appVersionState.appVersion !=
            appVersionState.versionamiento?.versionPrueba) {
      //mostrar modal para actualizar la app
      dialogVersionamientoKey = GlobalKey();

      showDialog(
        barrierDismissible: false,
        context: context,
        builder: (context) {
          return PopScope(
            canPop: false,
            child: DialogAppVersion(
              key: dialogVersionamientoKey,
            ),
          );
        },
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return ExitConfirm(
      child: VisibilityDetector(
        key: const Key('loginKey'),
        onVisibilityChanged: (info) {
          if (info.visibleFraction > 0) {
            loginVisible = true;
          } else {
            if (mounted) {
              setState(() {
                loginVisible = false;
              });
            }
          }
        },
        child: Scaffold(
          appBar: AppBar(
            toolbarHeight: 0,
            backgroundColor: AppColors.white,
            scrolledUnderElevation: 0,
            systemOverlayStyle: const SystemUiOverlayStyle(
              statusBarColor: Colors.transparent,
              statusBarIconBrightness: Brightness.dark,
              systemNavigationBarColor: AppColors.gray50,
              systemNavigationBarIconBrightness: Brightness.dark,
            ),
          ),
          body: Column(
            children: [
              const Expanded(
                child: CustomScrollView(
                  physics: ClampingScrollPhysics(),
                  slivers: [
                    SliverFillRemaining(
                      hasScrollBody: false,
                      child: FormularioBody(),
                    )
                  ],
                ),
              ),
            ],
          ),
          floatingActionButton: FormularioFab(),
          floatingActionButtonLocation: FloatingActionButtonLocation.miniEndTop,
        ),
      ),
    );
  }
}
