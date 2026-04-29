import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/inputs/alias.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/providers/configurar_cuentas_provider.dart';
import 'package:caja_tacna_app/features/configurar_cuentas/widgets/input_alias.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DialogEditarAlias extends ConsumerStatefulWidget {
  const DialogEditarAlias({super.key});

  @override
  DialogEditarAliasState createState() => DialogEditarAliasState();
}

class DialogEditarAliasState extends ConsumerState<DialogEditarAlias> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref
          .read(configurarCuentasProvider.notifier)
          .changeAlias(const AliasCuentaAhorro.pure(''));
    });
  }

  @override
  Widget build(BuildContext context) {
    final configurarCuentaState = ref.watch(configurarCuentasProvider);
    final cuentaAhorro = configurarCuentaState.cuentaAhorro;

    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: const EdgeInsets.only(
        top: 18,
        left: 24,
        right: 24,
        bottom: 48,
      ),
      titlePadding: const EdgeInsets.only(
        top: 18,
        left: 24,
        right: 18,
        bottom: 0,
      ),
      title: Row(
        mainAxisAlignment: MainAxisAlignment.end,
        children: [
          SizedBox(
            height: 36,
            width: 36,
            child: TextButton(
              style: TextButton.styleFrom(
                shape: const CircleBorder(),
                padding: EdgeInsets.zero,
              ),
              onPressed: () {
                Navigator.pop(context);
              },
              child: SvgPicture.asset(
                'assets/icons/x.svg',
                height: 24,
              ),
            ),
          ),
        ],
      ),
      content: SizedBox(
        width: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            const Text(
              'Editar alias',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w800,
                color: AppColors.black,
                height: 28 / 18,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            Container(
              decoration: BoxDecoration(
                color: AppColors.gray50,
                border: Border.all(
                  width: 1,
                  color: AppColors.gray300,
                ),
                borderRadius: BorderRadius.circular(8),
              ),
              width: double.maxFinite,
              padding: const EdgeInsets.symmetric(
                horizontal: 16,
                vertical: 18,
              ),
              child: Column(
                children: [
                  Text(
                    cuentaAhorro?.alias ?? '',
                    style: const TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.w500,
                      color: AppColors.gray900,
                      height: 28 / 18,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(
                    height: 4,
                  ),
                  Text(
                    CtUtils.formatNumeroCuenta(
                      numeroCuenta: cuentaAhorro?.identificador,
                      hash: true,
                    ),
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w400,
                      color: AppColors.gray900,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            const Text(
              'Alias',
              style: TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray700,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 6,
            ),
            InputAliasCuentaAhorro(
              onChangeAlias: (alias) {
                ref.read(configurarCuentasProvider.notifier).changeAlias(alias);
              },
              alias: configurarCuentaState.alias,
              hintText: cuentaAhorro?.alias,
            ),
            const SizedBox(
              height: 24,
            ),
            CtButton(
              text: 'Guardar cambios',
              onPressed: () {
                ref.read(configurarCuentasProvider.notifier).actualizarAlias();
              },
              borderRadius: 8,
              width: double.infinity,
              disabled: configurarCuentaState.btnAliasDisabled,
            )
          ],
        ),
      ),
    );
  }
}
