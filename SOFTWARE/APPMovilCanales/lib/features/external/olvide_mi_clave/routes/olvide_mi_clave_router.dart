import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/external/olvide_mi_clave/screens/olvide_mi_clave_screen.dart';
import 'package:go_router/go_router.dart';

final olvideMiClaveRouter = GoRoute(
  path: '/olvide-mi-clave',
  pageBuilder: defaultPageBuilder(child: const OlvideMiClaveScreen()),
);
