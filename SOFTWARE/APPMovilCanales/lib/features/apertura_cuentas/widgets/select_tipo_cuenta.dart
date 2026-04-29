import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/apertura_cuentas/models/datos_iniciales_response.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class SelectTipoCuenta extends StatelessWidget {
  const SelectTipoCuenta({
    super.key,
    required this.value,
    required this.onChanged,
    required this.tiposCuenta,
  });
  final ProductoApertura? value;
  final List<ProductoApertura> tiposCuenta;
  final void Function(ProductoApertura tipoCuenta) onChanged;

  void _showOptionsModal(BuildContext context) {
    showDialog<void>(
      context: context,
      builder: (BuildContext context) {
        return Dialog(
          shape: AppTheme.modalShape,
          insetPadding: AppTheme.modalInsetPadding,
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              const HeaderModal(),
              ListView.builder(
                shrinkWrap: true,
                padding: const EdgeInsets.only(
                  left: 4,
                  right: 4,
                  bottom: 12,
                ),
                itemBuilder: (context, index) {
                  final tipoCuenta = tiposCuenta[index];
                  return ListTile(
                    contentPadding: const EdgeInsets.symmetric(
                      horizontal: 12,
                    ),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
                    selected:
                        tipoCuenta.codigoProducto == value?.codigoProducto,
                    selectedColor: AppColors.primary700,
                    title: Text(tipoCuenta.nombreProducto),
                    onTap: () {
                      onChanged(tipoCuenta);
                      Navigator.pop(context);
                    },
                  );
                },
                itemCount: tiposCuenta.length,
              )
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
                      '${value?.nombreProducto}',
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
                      'Tipo de cuenta',
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
