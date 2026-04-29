import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/screens/compartir_qr_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/screens/configuracion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/screens/datos_operacion_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/screens/operacion_exitosa_screen.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/screens/verificar_operacion_screen.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

final afiliacionCelularRouter = GoRoute(
  path: 'afiliacion',
  routes: [
    GoRoute(
      path: 'datos-operacion',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const DatosOperacionScreen(),
      ),
    ),
    GoRoute(
      path: 'compartir-qr',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const CompartirQrScreen(),
      ),
    ),
    GoRoute(
      path: 'verificar-numero',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const VerificarOperacionScreen(),
      ),
    ),  
    GoRoute(
      path: 'operacion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const OperacionExitosaScreen(),
      ),
    ),
    GoRoute(
      path: 'configuracion-exitosa',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfiguracionExitosaScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const Text('builder para que no salga error de parentNavigatorKey');
  },
);
