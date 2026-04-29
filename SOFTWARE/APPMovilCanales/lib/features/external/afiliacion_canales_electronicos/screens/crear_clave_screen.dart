import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/providers/afiliacion_canales_electronicos_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_teclado.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class CrearClaveScreen extends ConsumerWidget {
  const CrearClaveScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        scrolledUnderElevation: 0,
        automaticallyImplyLeading: false,
        toolbarHeight: 64,
        flexibleSpace: SafeArea(
          child: Container(
            height: 64,
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Container(
                  width: 32,
                  height: 32,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: AppColors.primary100,
                  ),
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      context.pop();
                    },
                    child: SvgPicture.asset(
                      'assets/icons/shared/chevron-left.svg',
                      colorFilter: const ColorFilter.mode(
                        AppColors.primary700,
                        BlendMode.srcIn,
                      ),
                      width: 24,
                      height: 24,
                    ),
                  ),
                ),
                Image.asset(
                  'assets/images/logo_rojo.png',
                  width: 88,
                ),
                const SizedBox(
                  width: 32,
                )
              ],
            ),
          ),
        ),
      ),
      body: const CustomScrollView(
        physics: ClampingScrollPhysics(),
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _CrearClaveView(),
          )
        ],
      ),
    );
  }
}

class _CrearClaveView extends ConsumerWidget {
  const _CrearClaveView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final afiliacion = ref.watch(afiliacionCanElectProvider);

    return Container(
      padding: EdgeInsets.only(
        top: 25,
        left: 24,
        right: 24,
        bottom: 35 + MediaQuery.of(context).padding.bottom,
      ),
      child: Column(
        children: [
          const Text(
            'Crea tu clave de internet',
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.w600,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          const SizedBox(
            width: 330,
            height: 90,
            child: Text(
              'Por tu seguridad, la clave de internet no deberá contener números consecutivos o iguales. Evita usar: 123456, 111111 u otros similares.',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w400,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
              textAlign: TextAlign.center,
            ),
          ),
          const SizedBox(
            height: 18,
          ),
          CtTeclado(
            value: afiliacion.claveInternet,
            onChange: (value) {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .changeClaveInternet(value);
            },
            random: true,
          ),
          const Spacer(),
          const SizedBox(
            height: 15,
          ),
          CtButton(
            text: 'Continuar',
            disabled: afiliacion.claveInternet.length < 6,
            onPressed: () {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .crearClaveSubmit(withPush: true);
            },
          )
        ],
      ),
    );
  }
}
