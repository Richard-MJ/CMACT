import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/perfil/screens/actualizacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/perfil/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/perfil/screens/perfil_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final perfilRouter = GoRoute(
  path: '/perfil',
  routes: [
    GoRoute(
      path: 'datos',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PerfilScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarScreen(),
      ),
    ),
    GoRoute(
      path: 'actualizacion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ActualizacionExitosaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
