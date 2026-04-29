import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/emision_giros/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/emision_giros/screens/datos_beneficiario_screen.dart';
import 'package:caja_tacna_app/features/emision_giros/screens/direccion_beneficiario_screen.dart';
import 'package:caja_tacna_app/features/emision_giros/screens/giro_exitoso_screen.dart';
import 'package:caja_tacna_app/features/emision_giros/screens/ingreso_monto_screen.dart';
import 'package:go_router/go_router.dart';

final emisionGirosRouter = GoRoute(
  path: '/emision-giros',
  routes: [
    GoRoute(
      path: 'ingreso-monto',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const IngresoMontoScreen(),
      ),
    ),
    GoRoute(
      path: 'datos-beneficiario',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DatosBeneficiarioScreen(),
      ),
    ),
    GoRoute(
      path: 'direccion-beneficiario',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DireccionBeneficiarioScreen(),
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
      path: 'giro-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const GiroExitosoScreen(),
      ),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
