import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/external/contactanos/screens/contactanos_screen.dart';
import 'package:go_router/go_router.dart';

final contactanosRouter = GoRoute(
  path: '/contactanos',
  pageBuilder: defaultPageBuilder(child: const ContactanosScreen()),
);
