import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pagar_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pago_creditos_propios_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pago_exitoso_app_screen.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/screens/pago_exitoso_cip_screen.dart';
import 'package:go_router/go_router.dart';

final pagoCreditosPropiosRouter = GoRoute(
  path: 'creditos-propios',
  pageBuilder: defaultPageBuilder(child: const PagoCreditosPropiosScreen()),
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
      path: 'pago-exitoso-app',
      pageBuilder: defaultPageBuilder(child: const PagoExitosoAppScreen()),
    ),
    GoRoute(
      parentNavigatorKey: rootNavigatorKey,
      path: 'pago-exitoso-cip',
      pageBuilder: defaultPageBuilder(child: const PagoExitosoCipScreen()),
    ),
  ],
);
