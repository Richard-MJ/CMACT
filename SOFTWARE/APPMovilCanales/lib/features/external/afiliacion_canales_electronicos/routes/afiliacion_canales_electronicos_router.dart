import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/screens/afiliacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/screens/crear_clave_screen.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/screens/formulario_screen.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/screens/verificar_identidad_screen.dart';
import 'package:go_router/go_router.dart';

final afiliacionCanalesElectronicosRouter = GoRoute(
  path: '/afiliacion-canales-electronicos',
  routes: [
    GoRoute(
      path: 'formulario',
      pageBuilder: defaultPageBuilder(child: const FormularioScreen()),
    ),
    GoRoute(
      path: 'crear-clave',
      pageBuilder: defaultPageBuilder(child: const CrearClaveScreen()),
    ),
    GoRoute(
      path: 'verificar-identidad',
      builder: (context, state) => const VerificarIdentidadScreen(),
    ),
    GoRoute(
      path: 'afiliacion-exitosa',
      builder: (context, state) => const AfiliacionExitosaScreen(),
    ),
  ],
  redirect: (context, state) {
    return null;
  },
);
