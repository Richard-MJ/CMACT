import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/screens/administrar_operaciones_frecuentes_screen.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/screens/detalle_operacion_screen.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/screens/editar_alias_screen.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/screens/operaciones_frecuentes_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final operacionesFrecuentesRouter = GoRoute(
  path: '/operaciones-frecuentes',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'lista-operaciones',
      pageBuilder: defaultPageBuilder(
        child: const OperacionesFrecuentesScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'administrar',
      pageBuilder: defaultPageBuilder(
        child: const AdministrarOperacionesFrecuentesScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'detalle-operacion',
      pageBuilder: defaultPageBuilder(
        child: const DetalleOperacionScreen(),
      ),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'editar-alias',
      pageBuilder: defaultPageBuilder(
        child: const EditarAliasScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
