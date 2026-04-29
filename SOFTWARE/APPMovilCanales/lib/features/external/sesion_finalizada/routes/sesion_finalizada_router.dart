import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/external/sesion_finalizada/screens/sesion_finalizada_screen.dart';
import 'package:go_router/go_router.dart';

final sesionFinalizadaRouter = GoRoute(
  path: '/sesion-finalizada',
  pageBuilder: defaultPageBuilder(child: const SesionFinalizadaView()),
);
