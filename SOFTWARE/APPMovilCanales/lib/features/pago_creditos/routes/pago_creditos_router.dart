import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_propios/routes/pago_creditos_propios_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/pago_creditos_terceros/routes/pago_creditos_terceros_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/screens/pago_creditos_screen.dart';
import 'package:go_router/go_router.dart';

final pagoCreditosRouter = GoRoute(
  path: '/pago-creditos',
  pageBuilder: defaultPageBuilder(child: const PagoCreditosScreen()),
  routes: [
    pagoCreditosPropiosRouter,
    pagoCreditosTercerosRouter,
  ],
);
