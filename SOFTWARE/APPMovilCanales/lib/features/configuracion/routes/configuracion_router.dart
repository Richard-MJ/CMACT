import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/configuracion/screens/configuracion_screen.dart';
import 'package:go_router/go_router.dart';

final configuracionRouter = GoRoute(
  parentNavigatorKey: rootNavigatorKey,
  path: '/configuracion',
  pageBuilder: defaultPageBuilder(child: const ConfiguracionScreen()),
);
