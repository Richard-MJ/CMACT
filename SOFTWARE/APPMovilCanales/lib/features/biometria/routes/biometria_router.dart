import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/biometria/screens/cambio_exitoso_screen.dart';
import 'package:caja_tacna_app/features/biometria/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/biometria/screens/face_jd_screen.dart';
import 'package:caja_tacna_app/features/biometria/screens/huella_screen.dart';
import 'package:caja_tacna_app/features/biometria/screens/seguridad_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final biometriaRouter = GoRoute(
  path: '/biometria',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'seguridad',
      pageBuilder: defaultPageBuilder(
        child: const SeguridadScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'huella',
      pageBuilder: defaultPageBuilder(
        child: const HuellaScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'face-id',
      pageBuilder: defaultPageBuilder(
        child: const FaceIdScreen(),
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
      path: 'operacion-exitosa',
      pageBuilder: defaultPageBuilder(
        child: const OperacionExitosaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
