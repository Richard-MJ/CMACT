import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/tarjeta/screens/tarjeta_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final tarjetaRouter = GoRoute(
  path: '/tarjeta', 
  routes: [
    GoRoute(
        path: 'datos',
        parentNavigatorKey: rootNavigatorKey,
        pageBuilder: defaultPageBuilder(child: const TarjetaScreen()))
  ],
  builder: (context, state) 
    => const Text('builder para que no salga error de parentNavigatorKey')
);
