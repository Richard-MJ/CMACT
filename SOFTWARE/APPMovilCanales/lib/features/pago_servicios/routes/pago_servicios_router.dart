import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_servicios/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_servicios/screens/pagar_screen.dart';
import 'package:caja_tacna_app/features/pago_servicios/screens/buscar_screen.dart';
import 'package:caja_tacna_app/features/pago_servicios/screens/buscar_cobro_screen.dart';
import 'package:caja_tacna_app/features/pago_servicios/screens/pago_exitoso_screen.dart';

import 'package:go_router/go_router.dart';

final pagoServiciosRouter = GoRoute(
  path: '/pago-servicios',
  routes: [
    GoRoute(
      path: 'buscar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const BuscarScreen(),
      ),
    ),
    GoRoute(
      path: 'buscar-cobro',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const BuscarCobroScreen(),
      ),
    ),
    GoRoute(
      path: 'pagar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagarScreen(),
      ),
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
  ],
  redirect: (context, state) {
    return null;
  },
);
