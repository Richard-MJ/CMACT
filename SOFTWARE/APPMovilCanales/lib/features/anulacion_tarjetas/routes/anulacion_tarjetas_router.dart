import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/screens/anulacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/screens/anular_screen.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/screens/confirmar_screen.dart';
import 'package:go_router/go_router.dart';

final anulacionTarjetasRouter = GoRoute(
  path: '/anulacion-tarjetas',
  pageBuilder: defaultPageBuilder(
    child: const AnularScreen(),
  ),
  routes: [
    GoRoute(
      path: 'anular',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AnularScreen(),
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
      path: 'anulacion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const AnulacionExitosaScreen(),
      ),
    ),
  ],
);
