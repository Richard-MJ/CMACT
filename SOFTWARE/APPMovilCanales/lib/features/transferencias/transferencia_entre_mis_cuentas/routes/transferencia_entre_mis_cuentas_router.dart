import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/screens/transferencia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/screens/transferir_screen.dart';
import 'package:go_router/go_router.dart';

final transferenciaEntreMisCuentasRouter = GoRoute(
  path: 'entre-mis-cuentas',
  routes: [
    GoRoute(
      path: 'transferir',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferirEntreMisCuentasScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarTransferenciaEntreMisCuentasScreen(),
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
