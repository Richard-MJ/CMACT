import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/pago_servicios_recargas_virtuales/screens/pago_servicios_recargas_virtuales_screen.dart';
import 'package:go_router/go_router.dart';

final pagoServiciosRecargasVirtualesRouter = GoRoute(
  path: '/pago-servicios-recargas-virtuales',
  pageBuilder: defaultPageBuilder(
    child: const PagoServiciosRecargasVirtualesScreen(),
  ),
);
