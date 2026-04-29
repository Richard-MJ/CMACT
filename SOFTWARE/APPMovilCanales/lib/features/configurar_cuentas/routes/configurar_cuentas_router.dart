import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/actualizacion_exitosa_limite_operaciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/actualizacion_exitosa_limite_transacciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/actualizacion_exitosa_limite_transacciones_semanal_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/configurar_cuentas_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/confirmar_limite_operaciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/confirmar_limite_transacciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/confirmar_limite_transacciones_semanal_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/cuenta_ahorro_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/limite_operaciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/limite_transacciones_screen.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/screens/limite_transacciones_semanal_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final configurarCuentasRouter = GoRoute(
  path: '/configurar-cuentas',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'lista-cuentas',
      pageBuilder: defaultPageBuilder(
        child: const ConfigurarCuentasScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'cuenta-ahorro',
      pageBuilder: defaultPageBuilder(
        child: const CuentaAhorroScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'limite-transacciones',
      pageBuilder: defaultPageBuilder(
        child: const LimiteTransaccionesScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'limite-transacciones-semanal',
      pageBuilder: defaultPageBuilder(
        child: const LimiteTransaccionesSemanalScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'limite-operaciones',
      pageBuilder: defaultPageBuilder(
        child: const LimiteOperacionesScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-limite-transacciones',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarLimiteTransaccionesScreen(),
      ),
    ),
    GoRoute(
      path: 'actualizacion-exitosa-limite-transacciones',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ActualizacionExitosaLimiteTransaccionesScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-limite-operaciones',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarLimiteOperacionesScreen(),
      ),
    ),
    GoRoute(
      path: 'actualizacion-exitosa-limite-operaciones',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ActualizacionExitosaLimiteOperacionesScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-limite-transacciones-semanal',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarLimiteTransaccionesSemanalScreen(),
      ),
    ),
    GoRoute(
      path: 'actualizacion-exitosa-limite-transacciones-semanal',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ActualizacionExitosaLimiteTransaccionesSemanalScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
