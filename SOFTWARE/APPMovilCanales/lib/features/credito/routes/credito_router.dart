import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/credito/screens/detalle_credito_screen.dart';
import 'package:caja_tacna_app/features/credito/screens/movimientos_credito_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final creditoRouter = GoRoute(
  path: '/credito',
  routes: [
    GoRoute(
      path: 'detalle/:numeroCredito',
      parentNavigatorKey: mainShellNavigatorKey,
      pageBuilder: (context, state) {
        final numeroCredito =
            state.pathParameters['numeroCredito'] ?? 'no-numeroCredito';
        return transition(
          context: context,
          state: state,
          child: DetalleCredito(
            numeroCredito: numeroCredito,
          ),
        );
      },
    ),
    GoRoute(
      path: 'movimientos/:numeroCredito',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: (context, state) {
        final numeroCredito =
            state.pathParameters['numeroCredito'] ?? 'no-numeroCredito';
        return transition(
          context: context,
          state: state,
          child: MovimientosCredito(
            numeroCredito: numeroCredito,
          ),
        );
      },
    ),
  ],
  redirect: (context, state) {
    return null;
  },
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
