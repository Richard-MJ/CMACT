import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/screens/pagar_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/screens/pago_exitoso_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/screens/pago_exitoso_cip_screen.dart';
import 'package:go_router/go_router.dart';

final pagoCreditosTercerosRouter = GoRoute(
  path: 'creditos-terceros',
  routes: [
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'pagar',
      pageBuilder: defaultPageBuilder(child: const PagarScreen()),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'confirmar',
      pageBuilder: defaultPageBuilder(child: const ConfirmarScreen()),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'pago-exitoso',
      pageBuilder: defaultPageBuilder(child: const PagoExitosoScreen()),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'pago-exitoso-cip',
      pageBuilder: defaultPageBuilder(child: const PagoExitosoCipScreen()),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
