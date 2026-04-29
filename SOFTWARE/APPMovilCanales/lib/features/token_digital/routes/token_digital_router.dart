import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/token_digital/screens/afiliacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/afiliar_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/confirmar_restablecer_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/desafiliar_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/restablecer_exitosa_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/restablecer_screen.dart';
import 'package:caja_tacna_app/features/token_digital/screens/token_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final tokenDigitalRouter = GoRoute(
  path: '/token-digital',
  routes: [
    GoRoute(
      path: 'token',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TokenScreen(),
      ),
    ),
    GoRoute(
      path: 'afiliar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AfiliarScreen(),
      ),
    ),
    GoRoute(
      path: 'desafiliar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DesafiliarScreen(),
      ),
    ),
    GoRoute(
      path: 'afiliacion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AfiliacionExitosaScreen(esAfiliacion: true),
      ),
    ),
    GoRoute(
      path: 'desafiliacion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AfiliacionExitosaScreen(esAfiliacion: false),
      ),
    ),
    GoRoute(
      path: 'confirmar-restablecer',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarRestablecerScreen(),
      ),
    ),
    GoRoute(
      path: 'restablecer',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const RestablecerScreen(),
      ),
    ),
    GoRoute(
      path: 'restablecer-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const RestablecerExitosaScreen(),
      ),
    )
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
