import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/screens/transferencia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/screens/transferir_screen.dart';
import 'package:go_router/go_router.dart';

final transferenciaTercerosRouter = GoRoute(
  path: 'terceros',
  routes: [
    GoRoute(
      path: 'transferir',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferirATercerosScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarTransferenciaATercerosScreen(),
      ),
    ),
    GoRoute(
      path: 'transferencia-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferenciaExitosaScreen(),
      ),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
