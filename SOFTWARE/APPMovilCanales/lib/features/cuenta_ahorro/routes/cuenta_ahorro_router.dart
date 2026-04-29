import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/screens/detalle_cuenta_ahorro_screen.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/screens/movimientos_cuenta_ahorro_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final cuentaAhorroRouter = GoRoute(
  path: '/cuenta-ahorro',
  routes: [
    GoRoute(
      path: 'detalle/:codigoAgencia/:identificador',
      pageBuilder: (context, state) {
        final codigoAgencia =
            state.pathParameters['codigoAgencia'] ?? 'no-codigoAgencia';
        final identificador =
            state.pathParameters['identificador'] ?? 'no-identificador';
        return transition(
          context: context,
          state: state,
          child: DetalleCuentaAhorroScreen(
            codigoAgencia: codigoAgencia,
            identificador: identificador,
          ),
        );
      },
    ),
    GoRoute(
      path: 'movimientos/:codigoAgencia/:identificador',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: (context, state) {
        final codigoAgencia =
            state.pathParameters['codigoAgencia'] ?? 'no-codigoAgencia';
        final identificador =
            state.pathParameters['identificador'] ?? 'no-identificador';

        return transition(
          context: context,
          state: state,
          child: MovimientosCuentaAhorroScreen(
            codigoAgencia: codigoAgencia,
            identificador: identificador,
          ),
        );
      },
    )
  ],
  redirect: (context, state) {
    return null;
  },
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
