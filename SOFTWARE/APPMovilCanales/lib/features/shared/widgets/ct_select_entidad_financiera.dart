import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/models/select_entidad_financiera_option.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class CtSelectEntidadFinanciera<T extends SelectEntidadFinancieraOption>
    extends StatelessWidget {
  const CtSelectEntidadFinanciera({
    super.key,
    required this.value,
    required this.onChanged,
    required this.entidadesFinancieras,
  });
  final T? value;
  final List<T> entidadesFinancieras;
  final void Function(T entidadFinanciera) onChanged;

  void _showOptionsModal(BuildContext context) {
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
                  mainAxisSize: MainAxisSize.min,
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
                        final entidadFinanciera = entidadesFinancieras[index];
                        return ListTile(
                          contentPadding: const EdgeInsets.symmetric(
                            horizontal: 12,
                          ),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                          selected: entidadFinanciera.idEntidadCce ==
                              value?.idEntidadCce,
                          selectedColor: AppColors.primary700,
                          title: Text(entidadFinanciera.nombreEntidadCce),
                          onTap: () {
                            onChanged(entidadFinanciera);
                            Navigator.pop(context);
                          },
                        );
                      },
                      itemCount: entidadesFinancieras.length,
                    ),
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
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        FocusManager.instance.primaryFocus?.unfocus();
        _showOptionsModal(context);
      },
      child: CtTextFieldContainer(
        padding: const EdgeInsets.symmetric(horizontal: 14),
        child: Row(
          children: [
            Expanded(
              child: value != null
                  ? Text(
                      '${value?.nombreEntidadCce}',
                      style: const TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                        color: AppColors.gray800,
                        overflow: TextOverflow.ellipsis,
                      ),
                      maxLines: 1,
                    )
                  : const Text(
                      'Seleccione una opción',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.w400,
                        height: 1.5,
                        leadingDistribution: TextLeadingDistribution.even,
                        color: AppColors.gray500,
                        overflow: TextOverflow.ellipsis,
                      ),
                      maxLines: 1,
                    ),
            ),
            const SizedBox(
              width: 12,
            ),
            SvgPicture.asset(
              'assets/icons/chevron-down.svg',
              height: 20,
            ),
          ],
        ),
      ),
    );
  }
}
