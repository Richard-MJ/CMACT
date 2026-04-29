import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/cambio_clave/providers/cambio_clave_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_teclado.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class ConfirmarClaveNuevaScreen extends ConsumerWidget {
  const ConfirmarClaveNuevaScreen({super.key});

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
            child: _ConfirmarClaveNuevaView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarClaveNuevaView extends ConsumerWidget {
  const _ConfirmarClaveNuevaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final cambioClaveState = ref.watch(cambioClaveProvider);

    return Container(
      padding: const EdgeInsets.only(
        top: 70,
        left: 24,
        right: 24,
        bottom: 54,
      ),
      child: Column(
        children: [
          const Text(
            'Confirma tu nueva clave\n de internet',
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
            height: 24,
          ),
          const Text(
            'Ingresa nuevamente tu clave de internet.',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w400,
              color: AppColors.gray900,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
            textAlign: TextAlign.center,
          ),
          const SizedBox(
            height: 62,
          ),
          CtTeclado(
            value: cambioClaveState.passwordNuevoConfirm,
            onChange: (value) {
              ref
                  .read(cambioClaveProvider.notifier)
                  .changePasswordNuevoConfirm(value);
            },
            random: true,
          ),
          const Spacer(),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Continuar',
            disabled: cambioClaveState.passwordNuevoConfirm.length < 6,
            onPressed: () {
              if (cambioClaveState.passwordNuevoConfirm !=
                  cambioClaveState.passwordNuevo) {
                ref.read(snackbarProvider.notifier).showSnackbar(
                    'La clave a confirmar no es la misma', SnackbarType.error);
              } else {
                ref
                    .read(cambioClaveProvider.notifier)
                    .cambiarClave(withPush: true);
              }
            },
          )
        ],
      ),
    );
  }
}
