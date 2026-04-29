import 'dart:async';

import 'package:caja_tacna_app/config/plugins/safe_device.dart';
import 'package:caja_tacna_app/config/plugins/screen_lock_check_ct.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/constants/environment.dart';
import 'package:caja_tacna_app/core/providers/dispositivo_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_dispositivo_seguro.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/dialog_dispositivo_sin_bloqueo.dart';
import 'package:caja_tacna_app/features/shared/providers/loader_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_loader.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

//widget que engloba a la aplicacion para gestionar mejor el snackbar

class Services extends ConsumerStatefulWidget {
  const Services({
    super.key,
    required this.child,
  });
  final Widget child;

  @override
  ServicesState createState() => ServicesState();
}

class ServicesState extends ConsumerState<Services>
    with WidgetsBindingObserver {
  AppLifecycleState appLifecycleState = AppLifecycleState.resumed;
  GlobalKey dialogInternetKey = GlobalKey();
  GlobalKey? dialogDispositivoSinBloqueoKey;
  GlobalKey? dialogDispositivoSeguro;

  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(dispositivoProvider.notifier).setDispositivo();
    });
    WidgetsBinding.instance.addObserver(this);

    //verificamos si el dispositivo tiene bloqueo al abrir la app
    verificarDispositivoConDesbloqueo();
    verificarDispositivoSeguro();
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    setState(() {
      appLifecycleState = state;
    });

    //Verificaciones luego de estar en segundo plano y pasar a primero
    if (appLifecycleState == AppLifecycleState.resumed) {
      verificarDispositivoConDesbloqueo();
    }

    super.didChangeAppLifecycleState(state);
  }

  verificarDispositivoConDesbloqueo() async {
    final isScreenLockEnabled = await ScreenLockCheckCt.isScreenLockEnabled();

    //cerrar el modal si es que esta abierto
    if (dialogDispositivoSinBloqueoKey != null) {
      if (dialogDispositivoSinBloqueoKey!.currentContext != null) {
        Navigator.pop(dialogDispositivoSinBloqueoKey!.currentContext!);
      }
      dialogDispositivoSinBloqueoKey = null;
    }

    if (!isScreenLockEnabled) {
      if (rootNavigatorKey.currentContext == null) return;
      if (dialogDispositivoSinBloqueoKey != null) return;
      dialogDispositivoSinBloqueoKey = GlobalKey();
      showDialog(
        barrierDismissible: false,
        context: rootNavigatorKey.currentContext!,
        builder: (context) {
          return PopScope(
            canPop: false,
            child: DialogDispositivoSinBloqueo(
              key: dialogDispositivoSinBloqueoKey,
            ),
          );
        },
      );
    }
  }

  verificarDispositivoSeguro() async {
    if (!Environment.dispositivoSeguro) return;

    final (esDispositivoSeguro, mensajeError) = await SafeDevicePlugin.verify();

    //cerrar el modal si es que esta abierto
    if (dialogDispositivoSeguro != null) {
      if (dialogDispositivoSeguro!.currentContext != null) {
        Navigator.pop(dialogDispositivoSeguro!.currentContext!);
      }
      dialogDispositivoSeguro = null;
    }

    if (!esDispositivoSeguro) {
      if (rootNavigatorKey.currentContext == null) return;
      if (dialogDispositivoSeguro != null) return;
      dialogDispositivoSeguro = GlobalKey();
      showDialog(
        barrierDismissible: false,
        context: rootNavigatorKey.currentContext!,
        builder: (context) {
          return PopScope(
            canPop: false,
            child: DialogDispositivoSeguro(
              message: mensajeError,
            ),
          );
        },
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final snackbar = SnackbarService();
    final loader = ref.watch(loaderProvider);

    ref.listen(snackbarProvider, (previous, next) {
      if (next.showSnackbar) {
        snackbar.showSnackbar(
          context: context,
          message: next.message,
          type: next.type,
        );
      }
    });

    return Stack(
      children: [
        widget.child,
        if (loader.loading)
          CtLoader(
            withOpacity: loader.withOpacity,
          ),
        if (appLifecycleState != AppLifecycleState.resumed)
          Container(
            color: Colors.white,
          )
      ],
    );
  }
}
