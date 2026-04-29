import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/home/screens/otras_operaciones_screen.dart';
import 'package:go_router/go_router.dart';

final otrasOperacionesRouter = GoRoute(
  path: '/otras-operaciones',
  pageBuilder: defaultPageBuilder(child: const OtrasOperacionesScreen()),
);
