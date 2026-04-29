import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/afiliacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/configurar_afiliacion_screen.dart';
import 'package:caja_tacna_app/features/compras_internet/screens/confirmar_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final comprasInternetRouter = GoRoute(
  path: '/compras-internet',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'configurar-afiliacion',
      pageBuilder: defaultPageBuilder(
        child: const ConfigurarScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'confirmar',
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'afiliacion-exitosa',
      pageBuilder: defaultPageBuilder(
        child: const AfiliacionExitosaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
