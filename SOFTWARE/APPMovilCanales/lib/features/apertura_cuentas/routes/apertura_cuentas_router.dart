import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/screens/apertura_exitosa_screen.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/screens/ingreso_datos_screen.dart';
import 'package:go_router/go_router.dart';

final aperturaCuentasRouter = GoRoute(
  path: '/apertura-cuentas',
  routes: [
    GoRoute(
      path: 'ingreso-datos',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const IngresoDatosScreen(),
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
      path: 'apertura-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AperturaExitosaScreen(),
      ),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
