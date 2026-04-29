import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/screens/transferencia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/screens/transferencia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/screens/transferencia_interbancaria_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

final transferenciaInterbancariaRouter = GoRoute(
  path: 'interbancaria',
  routes: [
    GoRoute(
      path: 'transferir',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferenciaInterbancariaScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-diferida',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarTransferenciaInterbancariaDiferidaScreen(),
      ),
    ),
    GoRoute(
      path: 'transferencia-diferida-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferenciaDiferidaExitosaScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-inmediata',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarTransferenciaInterbancariaInmediataScreen(),
      ),
    ),
    GoRoute(
      path: 'transferencia-inmediata-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferenciaInmediataExitosaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const SizedBox.shrink();
  },
);
