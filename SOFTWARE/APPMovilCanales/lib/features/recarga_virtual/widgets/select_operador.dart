import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:caja_tacna_app/features/recarga_virtual/models/operador_movil_kasnet.dart';

class SelectOperador extends StatelessWidget {
  const SelectOperador({
    super.key,
    required this.value,
    required this.onChanged,
    required this.operadoresMovilesKasnet,
  });
  final OperadorMovilKasnet? value;
  final List<OperadorMovilKasnet> operadoresMovilesKasnet;
  final void Function(OperadorMovilKasnet operadorMovilKasnet) onChanged;

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
                        final operadorMovil = operadoresMovilesKasnet[index];
                        return ListTile(
                          contentPadding: const EdgeInsets.symmetric(
                            horizontal: 12,
                          ),
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(12),
                          ),
                          selected: operadorMovil.codigoOperador ==
                              value?.codigoOperador,
                          selectedColor: AppColors.primary700,
                          title: Text(operadorMovil.descripcionOperador),
                          onTap: () {
                            onChanged(operadorMovil);
                            Navigator.pop(context);
                          },
                        );
                      },
                      itemCount: operadoresMovilesKasnet.length,
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
                      '${value?.descripcionOperador}',
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
                      'Operador',
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
