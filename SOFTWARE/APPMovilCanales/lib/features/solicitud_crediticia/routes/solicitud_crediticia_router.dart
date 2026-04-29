import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/screens/datos_atencion.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/screens/datos_credito_screen.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/screens/solicitud_crediticia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/screens/solicitud_crediticia_screen.dart';
import 'package:go_router/go_router.dart';

final solicitudCrediticiaRouter = GoRoute(
  path: '/solicitud-crediticia',
  routes: [
    GoRoute(
      path: 'ingreso-mensual',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const SolicitudCrediticiaScreen(),
      ),
    ),
    GoRoute(
      path: 'datos-credito',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DatosCreditoScreen(),
      ),
    ),
    GoRoute(
      path: 'datos-atencion',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DatosAtencionScreen(),
      ),
    ),
    GoRoute(
      path: 'solicitud-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const SolicitudCrediticiaExitosaScreen(),
      ),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
