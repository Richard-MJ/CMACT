import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/novedades/screens/novedad_screen.dart';
import 'package:caja_tacna_app/features/novedades/screens/novedades_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final novedadesRouter = GoRoute(
  path: '/novedades',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'menu',
      pageBuilder: defaultPageBuilder(
        child: const NovedadesScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'novedad',
      pageBuilder: defaultPageBuilder(
        child: const NovedadScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
