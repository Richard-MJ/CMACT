import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/cambio_clave/screens/cambio_exitoso_screen.dart';
import 'package:caja_tacna_app/features/cambio_clave/screens/clave_actual_screen.dart';
import 'package:caja_tacna_app/features/cambio_clave/screens/clave_nueva_screen.dart';
import 'package:caja_tacna_app/features/cambio_clave/screens/confirmar_clave_nueva_screen.dart';
import 'package:caja_tacna_app/features/cambio_clave/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_safetypay/screens/pagar_screen.dart';
import 'package:go_router/go_router.dart';

final cambioClaveRouter = GoRoute(
  path: '/cambio-clave',
  pageBuilder: defaultPageBuilder(
    child: const PagarScreen(),
  ),
  routes: [
    GoRoute(
      path: 'clave-actual',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ClaveActualScreen(),
      ),
    ),
    GoRoute(
      path: 'clave-nueva',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ClaveNuevaScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-clave-nueva',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarClaveNuevaScreen(),
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
      path: 'cambio-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const CambioExitosoScreen(),
      ),
    ),
  ],
);
