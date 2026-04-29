import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_teclado.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class IngresarClaveScreen extends ConsumerWidget {
  const IngresarClaveScreen({super.key});

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
            child: _IngresarClaveView(),
          )
        ],
      ),
    );
  }
}

class _IngresarClaveView extends ConsumerWidget {
  const _IngresarClaveView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final login = ref.watch(loginProvider);
    return Padding(
      padding: EdgeInsets.only(
        top: 10,
        left: 24,
        right: 24,
        bottom: 54 + MediaQuery.of(context).padding.bottom,
      ),
      child: Column(
        children: [
          const Text(
            'Ingresa tu clave de \ninternet',
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.w600,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
            textAlign: TextAlign.center,
          ),
          const SizedBox(
            height: 72,
          ),
          CtTeclado(
            value: login.claveInternet,
            random: true,
            onChange: (value) =>
                ref.read(loginProvider.notifier).changeClaveInternet(value),
          ),
          const Spacer(),
          CtButton(
            text: 'Ingresar',
            disabled: login.claveInternet.length < 6,
            onPressed: () {
              ref.read(loginProvider.notifier).iniciarSesion(withPush: true);
            },
          )
        ],
      ),
    );
  }
}
