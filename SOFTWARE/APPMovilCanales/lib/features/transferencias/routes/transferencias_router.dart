import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/transferencias/screens/transferencias_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_entre_mis_cuentas/routes/transferencia_entre_mis_cuentas_router.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/routes/transferencia_interancaria_router.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_terceros/routes/transferencia_terceros_router.dart';
import 'package:go_router/go_router.dart';

final transferenciasRouter = GoRoute(
  path: '/transferencias',
  pageBuilder: defaultPageBuilder(child: const TransferenciasScreen()),
  routes: [
    transferenciaEntreMisCuentasRouter,
    transferenciaTercerosRouter,
    transferenciaInterbancariaRouter,
  ],
);
