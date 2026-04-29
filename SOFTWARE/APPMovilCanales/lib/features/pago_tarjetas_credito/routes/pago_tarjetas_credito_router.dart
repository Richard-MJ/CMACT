import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/screens/ingreso_monto_pagar.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/screens/pago_exitoso_screen.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/screens/ingreso_monto_pagar.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/screens/pago_exitoso_screen.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/screens/pago_tarjeta_credito_screen.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/screens/confirmar_screen.dart';
import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

final pagoTarjetasCreditoRouter = GoRoute(
  path: '/pago-tarjetas-credito',
  routes: [
    GoRoute(
      path: 'ingreso-datos-tarjeta',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagoTarjetaCreditoScreen(),
      ),
    ),
    GoRoute(
      path: 'ingreso-monto-pagar-diferida',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const IngresoMontoPagarDiferidaScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-diferida',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarPagoTarjetaDiferidaScreen(),
      ),
    ),
    GoRoute(
      path: 'pago-diferida-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagoTarjetaExitosoDiferidaScreen(),
      ),
    ),
    GoRoute(
      path: 'ingreso-monto-pagar-inmediata',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const IngresoMontoPagarInmediataScreen(),
      ),
    ),
    GoRoute(
      path: 'confirmar-inmediata',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const ConfirmarPagoTarjetaInmediataScreen(),
      ),
    ),
    GoRoute(
      path: 'pago-inmediata-exitoso',
      parentNavigatorKey: rootNavigatorKey,
      pageBuilder: defaultPageBuilder(
        child: const PagoTarjetaExitosoInmediataScreen(),
      ),
    ),
  ],
  builder: (context, state) {
    return const SizedBox.shrink();
  },
);
