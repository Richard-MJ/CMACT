import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/screens/confirmar_remover_dispositivos_screen.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/screens/dispositivos_seguros_screen.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/screens/dispostivos_removidos_exitoso_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final sesionCanalElectronicoRouter = GoRoute(
  path: '/sesion-canal-electronico',
  routes: [
    GoRoute(
      path: 'dispositivos-seguros',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DispositivosSegurosScreen(),
      ),
    ),
    GoRoute(
      path: 'remover/confirmar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarRemoverDispositivosScreen(),
      ),
    ),
    GoRoute(
      path: 'remover-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DispositivosRemovidosExitosoScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
