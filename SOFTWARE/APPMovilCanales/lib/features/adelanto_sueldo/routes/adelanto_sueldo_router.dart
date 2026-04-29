import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/screens/pagar_screen.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/adelanto_sueldo/screens/pago_exitosa_screen.dart';
import 'package:go_router/go_router.dart';

final adelantoSueldoRouter = GoRoute(
  path: '/adelanto-sueldo',
  pageBuilder: defaultPageBuilder(
    child: const PagarScreen(),
  ),
  routes: [
    GoRoute(
      path: 'pagar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagarScreen(),
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
      path: 'pago-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagoExitosoScreen(),
      ),
    ),
  ],
);
