import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_safetypay/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_safetypay/screens/pagar_screen.dart';
import 'package:caja_tacna_app/features/pago_safetypay/screens/pago_exitoso_screen.dart';
import 'package:go_router/go_router.dart';

final pagoSafetyRouter = GoRoute(
  path: '/pago-safetypay',
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
