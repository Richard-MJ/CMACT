import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/screens/formulario_screen.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/screens/ingresar_clave_screen.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/screens/verificar_identidad_screen.dart';
import 'package:go_router/go_router.dart';

final inicioSesionRouter = GoRoute(
  path: '/inicio-sesion',
  routes: [
    GoRoute(
      path: 'formulario',
      pageBuilder: defaultPageBuilder(child: const FormularioScreen()),
    ),
    GoRoute(
      path: 'ingresar-clave',
      pageBuilder: defaultPageBuilder(child: const IngresarClaveScreen()),
    ),
    GoRoute(
      path: 'verificar-identidad',
      pageBuilder: defaultPageBuilder(child: const VerificarIdentidadScreen()),
    )
  ],
  redirect: (context, state) {
    return null;
  },
);
