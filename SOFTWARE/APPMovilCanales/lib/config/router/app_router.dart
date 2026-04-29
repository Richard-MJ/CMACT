import 'package:caja_tacna_app/features/adelanto_sueldo/routes/adelanto_sueldo_router.dart';
import 'package:caja_tacna_app/features/anulacion_tarjetas/routes/anulacion_tarjetas_router.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/routes/apertura_cuentas_router.dart';
import 'package:caja_tacna_app/features/billetera_virtual/routes/billetera_virtual_router.dart';
import 'package:caja_tacna_app/features/biometria/routes/biometria_router.dart';
import 'package:caja_tacna_app/features/cambio_clave/routes/cambio_clave_router.dart';
import 'package:caja_tacna_app/features/cancelacion_cuentas/routes/cancelacion_cuentas_router.dart';
import 'package:caja_tacna_app/features/compras_internet/routes/compras_internet_router.dart';
import 'package:caja_tacna_app/features/configuracion/routes/configuracion_router.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/routes/configurar_cuentas_router.dart';
import 'package:caja_tacna_app/features/credito/routes/credito_router.dart';
import 'package:caja_tacna_app/features/cuenta_ahorro/routes/cuenta_ahorro_router.dart';
import 'package:caja_tacna_app/features/emision_giros/routes/emision_giros_router.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/routes/afiliacion_canales_electronicos_router.dart';
import 'package:caja_tacna_app/features/external/contactanos/routes/contactanos_router.dart';
import 'package:caja_tacna_app/features/external/olvide_mi_clave/routes/olvide_mi_clave_router.dart';
import 'package:caja_tacna_app/features/external/sesion_finalizada/routes/sesion_finalizada_router.dart';
import 'package:caja_tacna_app/features/home/routes/home_router.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/routes/inicio_sesion_router.dart';
import 'package:caja_tacna_app/features/home/routes/otras_operaciones_router.dart';
import 'package:caja_tacna_app/features/novedades/routes/novedades_router.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/routes/operaciones_frecuentes_router.dart';
import 'package:caja_tacna_app/features/pago_creditos/routes/pago_creditos_router.dart';
import 'package:caja_tacna_app/features/pago_safetypay/routes/pago_safety_router.dart';
import 'package:caja_tacna_app/features/pago_servicios/routes/pago_servicios_router.dart';
import 'package:caja_tacna_app/features/pago_servicios_recargas_virtuales/routes/pago_servicios_recargas_virtuales_router.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/routes/pago_tarjetas_credito_router.dart';
import 'package:caja_tacna_app/features/perfil/routes/perfil_router.dart';
import 'package:caja_tacna_app/features/recarga_virtual/routes/recarga_virtual_router.dart';
import 'package:caja_tacna_app/features/sesion_canal_electronico/routes/sesion_canal_eletronico_router.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_1.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_status_provider.dart';
import 'package:caja_tacna_app/features/solicitud_crediticia/routes/solicitud_crediticia_router.dart';
import 'package:caja_tacna_app/features/tarjeta/routes/tarjeta_router.dart';
import 'package:caja_tacna_app/features/token_digital/routes/token_digital_router.dart';
import 'package:caja_tacna_app/features/transferencias/routes/transferencias_router.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

final GlobalKey<NavigatorState> rootNavigatorKey = GlobalKey<NavigatorState>();
final GlobalKey<NavigatorState> mainShellNavigatorKey =
    GlobalKey<NavigatorState>();
String? pendingPath;

final appRouterProvider = Provider<GoRouter>((ref) {
  final router = GoRouter(
    initialLocation: '/inicio-sesion/formulario',
    debugLogDiagnostics: false,
    navigatorKey: rootNavigatorKey,
    routes: <RouteBase>[
      GoRoute(
        path: '/canales/caja-tacna-app',
        redirect: (context, state) {
          final isLoggedIn = ref.read(authStatusProvider) == AuthStatus.authenticated;
          ref.read(authStatusProvider.notifier).setPendingRedirect('/tarjeta/datos');
          if (!isLoggedIn) {
            return '/inicio-sesion/formulario';
          }
          return '/home';
        },
      ),
      sesionFinalizadaRouter,
      afiliacionCanalesElectronicosRouter,
      inicioSesionRouter,
      contactanosRouter,
      olvideMiClaveRouter,
      configuracionRouter,
      pagoTarjetasCreditoRouter,
      pagoSafetyRouter,
      recargaVirtualRouter,
      pagoServiciosRouter,
      emisionGirosRouter,
      aperturaCuentasRouter,
      anulacionTarjetasRouter,
      cancelacionCuentasRouter,
      cambioClaveRouter,
      tokenDigitalRouter,
      transferenciasRouter,
      billeteraVirtualRouter,
      operacionesFrecuentesRouter,
      perfilRouter,
      tarjetaRouter,
      novedadesRouter,
      solicitudCrediticiaRouter,
      otrasOperacionesRouter,
      pagoCreditosRouter,
      pagoServiciosRecargasVirtualesRouter,
      ShellRoute(
        builder: (context, state, child) {
          return CtLayout1(child: child);
        },
        navigatorKey: mainShellNavigatorKey,
        routes: [
          homeRouter,
          cuentaAhorroRouter,
          creditoRouter,
          adelantoSueldoRouter,
          comprasInternetRouter,
          sesionCanalElectronicoRouter,
          configurarCuentasRouter,
          biometriaRouter,
        ],
      ),
    ],
  );

  ref.listen(authStatusProvider, (_, __) {
    router.refresh();
  });

  return router;
});

CustomTransitionPage transition<T>({
  required BuildContext context,
  required GoRouterState state,
  required Widget child,
}) {
  return CustomTransitionPage<T>(
    key: state.pageKey,
    child: child,
    transitionsBuilder: (context, animation, secondaryAnimation, child) {
      const begin = Offset(1.0, 0.0);
      const end = Offset.zero;
      const curve = Curves.easeInOutSine;

      var tween = Tween(begin: begin, end: end).chain(CurveTween(curve: curve));
      var offsetAnimation = animation.drive(tween);

      var fadeTween = Tween(begin: 0.7, end: 1.0);
      var fadeAnimation = animation.drive(fadeTween);

      return FadeTransition(
        opacity: fadeAnimation,
        child: SlideTransition(position: offsetAnimation, child: child),
      );
    },
  );
}

Page<dynamic> Function(BuildContext, GoRouterState) defaultPageBuilder<T>({
  required Widget child,
  Brightness? brightness,
}) =>
    (BuildContext context, GoRouterState state) {
      return transition<T>(
        context: context,
        state: state,
        child: child,
      );
    };
