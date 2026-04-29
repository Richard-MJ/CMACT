import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/screens/qr_scanner_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/screens/transferencia_exitosa_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/screens/confirmar_transferencia_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/screens/contactos_celular_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/screens/transferir_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final transferenciaCelularRouter = GoRoute(
  path: 'transferencia-celular',
  routes: [
    GoRoute(
      path: 'contactos',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ContactosCelularScreen(),
      ),
    ),
    GoRoute(
      path: 'qr-scanner',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const QrScannerScreen(),
      ),
    ),
    GoRoute(
      path: 'transferir',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferirScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-transferencia',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarTransferenciaScreen(),
      ),
    ),
    GoRoute(
      path: 'transferencia-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const TransferenciaExitosaScreen(esAfiliacion: true),
      ),
    ),
  ],
  builder: (context, state) {
    return const SizedBox.shrink();
  },
);
