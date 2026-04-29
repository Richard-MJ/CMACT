import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/cancelacion_exitosa_interbancaria_screen.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/cancelacion_exitosa_interna_screen.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/cancelacion_exitosa_saldo_cero_screen.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/configurar_cancelacion_screen.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/confirmar_interbancaria_screen.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/screens/confirmar_interna_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final cancelacionCuentasRouter = GoRoute(
  path: '/cancelacion-cuentas',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'configurar-cancelacion',
      pageBuilder: defaultPageBuilder(
        child: const ConfigurarCancelacionScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'confirmar-interna',
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarInternaScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'cancelacion-exitosa-interna',
      pageBuilder: defaultPageBuilder(
        child: const CancelacionExitosaInternaScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'cancelacion-exitosa-saldo-cero',
      pageBuilder: defaultPageBuilder(
        child: const CancelacionExitosaSaldoCeroScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'confirmar-interbancaria',
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarInterbancariaScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'cancelacion-exitosa-interbancaria',
      pageBuilder: defaultPageBuilder(
        child: const CancelacionExitosaInterbancariaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
