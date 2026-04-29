import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/codigo_enlace.dart';
import 'package:caja_tacna_app/constants/estado_entidad.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/external/providers/parametros_provider.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/home/widgets/banner_libro_reclamaciones.dart';
import 'package:caja_tacna_app/features/home/widgets/icon_tarjeta.dart';
import 'package:caja_tacna_app/features/home/widgets/widgets.dart';
import 'package:caja_tacna_app/features/perfil/providers/perfil_provider.dart';
import 'package:caja_tacna_app/features/shared/models/service_exception.dart';
import 'package:caja_tacna_app/features/shared/providers/inactivity_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/signalr_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_shimmer.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:caja_tacna_app/features/token_digital/widgets/dialog_token_digital.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/svg.dart';
import 'package:visibility_detector/visibility_detector.dart';


class HomeScreen extends ConsumerStatefulWidget {
  const HomeScreen({super.key});

  @override
  HomeScreenState createState() => HomeScreenState();
}

class HomeScreenState extends ConsumerState<HomeScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() async {
      await Future.wait([
        ref.read(signalRProvider.notifier).initConnection(),
        ref.read(homeProvider.notifier).initData(),
      ]);
      ref.read(homeProvider.notifier).resetearProviders();
      ref.read(homeProvider.notifier).getHomeData();
      ref.read(inactivityProvider.notifier).resetearTimer();
      verificarTokenDigital();
      mostrarPublicidad();
    });
  }

  @override
  void dispose() {
    super.dispose();
  }

  verificarTokenDigital() async {
    try {
      await ref
          .read(tokenDigitalProvider.notifier)
          .obtenerDispositivoAfiliado();
      if (ref.read(tokenDigitalProvider).dispositivoAfiliado?.deviceStatus ==
              EstadoEntidad.inactivo ||
          ref.read(tokenDigitalProvider).dispositivoAfiliado == null) {
        if (!mounted) return;
        final bool? continuar = await showDialog(
          context: context,
          builder: (BuildContext context) {
            return const DialogTokenDigital();
          },
        );
        if (continuar == null || !continuar) return;
        ref.read(tokenDigitalProvider.notifier).goTokenDigital();
      }
    } on ServiceException catch (e) {
      ref
          .read(snackbarProvider.notifier)
          .showSnackbar(e.message, SnackbarType.error);
    }
  }

  mostrarPublicidad() async {
    if (!mounted) return;
    await ref.read(homeProvider.notifier).cargarPublicidades(context);
  }

  bool _isFirstLoad = true;

  @override
  Widget build(BuildContext context) {
    final esAfiliadoBilleteraVirtual = ref.watch(homeProvider.select((s) => s.esAfiliadoBilleteraVirtual));
    final enlaceReclamo = ref
        .watch(parametrosProvider)
        .enlaces
        ?.where((x) => x.codigoDocumento == CodigoEnlace.reclamo)
        .firstOrNull;
        
    return VisibilityDetector(
      key: const Key('myWidgetKey'),
      onVisibilityChanged: (info) {
        if (info.visibleFraction > 0) {
          if (_isFirstLoad) {
            _isFirstLoad = false;
            return;
          }
          // El widget es visible
          ref.read(timerProvider.notifier).cancelTimer();
          ref.read(homeProvider.notifier).getHomeData();
          ref.read(homeProvider.notifier).resetearProviders();
        }
      },
      child: Scaffold(
        extendBody: true,
        floatingActionButtonLocation: FloatingActionButtonLocation.centerDocked,
        floatingActionButton: SizedBox(
          height: 65,
          width: 65,
          child: FloatingActionButton(
            shape: const CircleBorder(),
            backgroundColor: AppColors.primary700,
            child: Icon(
              Icons.qr_code_scanner,
              color: AppColors.white,
              size: 35,
            ),
            onPressed: () {
              ref
                  .read(transferenciaCelularProvider.notifier)
                  .activarLecturaQrDesdeNavegacion(
                      afiliado: esAfiliadoBilleteraVirtual);
            },
          ),
        ),
        bottomNavigationBar: Builder(
          builder: (context) {
            return HomeBottomNavigation(
              esAfiliadoBilleteraVirtual:
                  esAfiliadoBilleteraVirtual,
            );
          },
        ),
        body: RefreshIndicator(
          color: AppColors.primary700,
          backgroundColor: AppColors.white,
          onRefresh: () async {
            ref.read(homeProvider.notifier).changeCargaPrimeraVez(cargar: true);
            ref.read(timerProvider.notifier).cancelTimer();
            ref.read(homeProvider.notifier).getHomeData();
            ref.read(homeProvider.notifier).resetearProviders();
          },
          child: CustomScrollView(
            physics: const AlwaysScrollableScrollPhysics(
              parent: ClampingScrollPhysics(),
            ),
            slivers: [
              SliverFillRemaining(
                hasScrollBody: false,
                child: Padding(
                  padding: const EdgeInsets.only(bottom: 110),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Padding(
                        padding: const EdgeInsets.only(
                          top: 8,
                          left: 24,
                          right: 24,
                          bottom: 5,
                        ),
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            const TipoCambio(),
                            const SizedBox(
                              height: 8,
                            ),
                            const _HomeHeader(),
                            const SizedBox(
                              height: 5,
                            ),
                          ],
                        ),
                      ),
                      const MisCuentas(),
                      Container(
                        alignment: Alignment.centerLeft,
                        padding: const EdgeInsets.only(
                          top: 0,
                          left: 24,
                          right: 24,
                          bottom: 6,
                        ),
                        child: const Text(
                          'Mis créditos',
                          style: TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w600,
                            color: AppColors.gray900,
                            height: 1.5,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ),
                      const MisCreditos(),
                      Container(
                        alignment: Alignment.centerLeft,
                        padding: const EdgeInsets.only(
                          top: 0,
                          left: 24,
                          right: 24,
                          bottom: 8,
                        ),
                        child: const Text(
                          'Operaciones',
                          style: TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w600,
                            color: AppColors.gray900,
                            height: 1.5,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                      ),
                      Container(
                        alignment: Alignment.centerLeft,
                        padding: const EdgeInsets.only(
                          top: 0,
                          left: 24,
                          right: 24,
                          bottom: 0,
                        ),
                        child: const Column(
                          children: [
                            Operaciones(),
                          ],
                        ),
                      ),
                      SizedBox(
                        height: 25,
                      ),
                      Container(
                        padding: const EdgeInsets.only(
                          top: 0,
                          left: 24,
                          right: 24,
                          bottom: 30,
                        ),
                        child: HomeCarousel(
                          children: [
                            BannerLibroReclamaciones(enlaceReclamo: enlaceReclamo),
                            const CreditoBanner(),
                            const HomeBanner(),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              )
            ],
          ),
        ),
      ),
    );
  }
}

class _HomeHeader extends ConsumerWidget {
  const _HomeHeader();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final homeState = ref.watch(homeProvider);
    final perfilState = ref.watch(perfilProvider);

    return Column(
      children: [
        homeState.loadingDatosCliente
            ? const CtShimmer.rectangular(
                width: 190,
                height: 24,
                margin: EdgeInsets.symmetric(vertical: 6),
              )
            : Row(
                children: [
                  Expanded(
                    child: Text(
                      'Hola, ${perfilState.apodoLocal}',
                      style: const TextStyle(
                        fontSize: 24,
                        fontWeight: FontWeight.w800,
                        color: AppColors.gray900,
                        height: 1.5,
                        leadingDistribution:
                            TextLeadingDistribution.even,
                      ),
                    ),
                  ),
                  if (homeState.datosCliente != null)
                    IconTarjeta(
                      tipoTarjeta: homeState
                          .datosCliente!.codigoTipoTarjeta,
                    )
                ],
              ),
        const SizedBox(
          height: 5,
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            const Text(
              'Mis cuentas',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w600,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution:
                    TextLeadingDistribution.even,
              ),
            ),
            IconButton(
              padding: EdgeInsets.zero,
              constraints: const BoxConstraints(),
              icon: SvgPicture.asset(
                !homeState.mostrarSaldo
                    ? 'assets/icons/eye.svg'
                    : 'assets/icons/eye_closed.svg',
                height: 30,
                colorFilter: const ColorFilter.mode(
                    AppColors.gray400, BlendMode.srcIn),
              ),
              onPressed: () {
                ref
                    .read(homeProvider.notifier)
                    .changeMostrarSaldo();
              },
            ),
          ],
        )
      ],
    );
  }
}
