import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/models/select_cuenta_option.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtSelectCuenta<T extends SelectCuentaOption> extends ConsumerWidget {
  const CtSelectCuenta({
    super.key,
    required this.cuentas,
    required this.onChange,
    required this.value,
    this.cuentaDisabled,
  });

  final List<T> cuentas;
  final void Function(T producto) onChange;
  final T? value;
  final T? cuentaDisabled;

  void _showOptionsModal(BuildContext context, WidgetRef ref) {
    showDialog<void>(
      context: context,
      builder: (BuildContext context) {
        return Dialog(
          shape: AppTheme.modalShape,
          insetPadding: AppTheme.modalInsetPadding,
          child: Stack(
            children: [
              SingleChildScrollView(
                child: Column(
                  children: [
                    ListView.builder(
                      shrinkWrap: true,
                      physics: const NeverScrollableScrollPhysics(),
                      padding: const EdgeInsets.only(
                        left: 4,
                        right: 4,
                        bottom: 12,
                        top: 46,
                      ),
                      itemBuilder: (context, index) {
                        final cuenta = cuentas[index];
                        return ListTile(
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                          contentPadding: const EdgeInsets.symmetric(
                            horizontal: 12,
                          ),
                          selected:
                              value?.numeroProducto == cuenta.numeroProducto,
                          enabled: cuentaDisabled?.numeroProducto !=
                              cuenta.numeroProducto,
                          selectedColor: AppColors.primary700,
                          title: Row(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Expanded(
                                child: Text(
                                  cuenta.alias,
                                  style: const TextStyle(fontSize: 14),
                                ),
                              ),
                              const SizedBox(
                                width: 10,
                              ),
                              Text(
                                CtUtils.formatCurrency(
                                  cuenta.montoSaldo,
                                  cuenta.simboloMonedaProducto,
                                ),
                              )
                            ],
                          ),
                          onTap: () {
                            onChange(cuenta);
                            Navigator.pop(context);
                          },
                        );
                      },
                      itemCount: cuentas.length,
                    )
                  ],
                ),
              ),
              const HeaderModal(),
            ],
          ),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return GestureDetector(
      onTap: () {
        FocusManager.instance.primaryFocus?.unfocus();
        _showOptionsModal(context, ref);
      },
      child: Container(
        height: 76,
        padding: const EdgeInsets.symmetric(horizontal: 16),
        decoration: BoxDecoration(
          border: Border.all(color: AppColors.primary700, width: 1),
          borderRadius: BorderRadius.circular(8),
        ),
        child: Row(
          children: [
            Expanded(
              child: value == null
                  ? const Center(
                      child: Text('Seleccione una cuenta'),
                    )
                  : Row(
                      children: [
                        Expanded(
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.center,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                                '${value?.alias}',
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                                overflow: TextOverflow.ellipsis,
                              ),
                              Text(
                                CtUtils.formatNumeroCuenta(
                                  numeroCuenta: value?.numeroProducto,
                                ),
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w400,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              ),
                            ],
                          ),
                        ),
                        const SizedBox(
                          width: 20,
                        ),
                        Column(
                          mainAxisAlignment: MainAxisAlignment.center,
                          crossAxisAlignment: CrossAxisAlignment.end,
                          children: [
                            const Text(
                              'Saldo',
                              style: TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w400,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                            Text(
                              CtUtils.formatCurrency(
                                value?.montoSaldo,
                                value?.simboloMonedaProducto,
                              ),
                              style: const TextStyle(
                                fontSize: 14,
                                fontWeight: FontWeight.w500,
                                color: AppColors.gray900,
                                height: 22 / 14,
                                leadingDistribution:
                                    TextLeadingDistribution.even,
                              ),
                            ),
                          ],
                        ),
                        const SizedBox(
                          width: 16,
                        ),
                      ],
                    ),
            ),
            SvgPicture.asset(
              'assets/icons/shared/chevron-down-black.svg',
              height: 24,
            ),
          ],
        ),
      ),
    );
  }
}
