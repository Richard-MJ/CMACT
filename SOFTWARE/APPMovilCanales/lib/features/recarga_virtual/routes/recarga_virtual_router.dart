import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/recarga_virtual/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/recarga_virtual/screens/recarga_exitosa_screen.dart';
import 'package:caja_tacna_app/features/recarga_virtual/screens/recargar_screen.dart';
import 'package:go_router/go_router.dart';

final recargaVirtualRouter = GoRoute(
  path: '/recarga-virtual',
  pageBuilder: defaultPageBuilder(
    child: const RecargarScreen(),
  ),
  routes: [
    GoRoute(
      path: 'recargar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const RecargarScreen(),
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
      path: 'recarga-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const RecargaExitosaScreen(),
      ),
    ),
  ],
);
