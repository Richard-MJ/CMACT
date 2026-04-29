import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/emision_giros/models/nacionalidad.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class SelectNacionalidad extends StatelessWidget {
  const SelectNacionalidad({
    super.key,
    required this.value,
    required this.onChanged,
    required this.nacionalidades,
  });
  final Nacionalidad? value;
  final List<Nacionalidad> nacionalidades;
  final void Function(Nacionalidad nacionalidad) onChanged;

  void _showOptionsModal(BuildContext context) {
    showDialog<void>(
      context: context,
      builder: (BuildContext context) {
        return _Dialog(
          value: value,
          onChanged: onChanged,
          nacionalidades: nacionalidades,
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
                      '${value?.nombrePais}',
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
                      'Nacionalidad',
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

class _Dialog extends StatefulWidget {
  const _Dialog({
    required this.value,
    required this.onChanged,
    required this.nacionalidades,
  });

  final Nacionalidad? value;
  final List<Nacionalidad> nacionalidades;
  final void Function(Nacionalidad nacionalidad) onChanged;

  @override
  State<_Dialog> createState() => __DialogState();
}

class __DialogState extends State<_Dialog> {
  String filter = '';
  List<Nacionalidad> get nacionalidades => widget.nacionalidades
      .where((element) =>
          element.nombrePais.toLowerCase().contains(filter.toLowerCase()))
      .toList();

  @override
  Widget build(BuildContext context) {
    return Dialog(
      shape: AppTheme.modalShape,
      insetPadding: AppTheme.modalInsetPadding,
      child: Column(
        children: [
          const HeaderModal(),
          Container(
            padding: const EdgeInsets.only(
              left: 16,
              right: 16,
              top: 8,
              bottom: 8,
            ),
            child: CtTextFieldContainer(
              child: TextFormField(
                onTapOutside: (event) {
                  FocusManager.instance.primaryFocus?.unfocus();
                },
                style: const TextStyle(
                  color: AppColors.gray800,
                  fontSize: 16,
                  fontWeight: FontWeight.w400,
                ),
                decoration: const InputDecoration(
                  border: OutlineInputBorder(borderSide: BorderSide.none),
                  contentPadding: EdgeInsets.zero,
                  hintText: 'Buscar',
                  hintStyle: TextStyle(
                    color: AppColors.gray500,
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                  ),
                ),
                onFieldSubmitted: (value) {
                  setState(() {
                    filter = value;
                  });
                },
                onChanged: (value) {
                  setState(() {
                    filter = value;
                  });
                },
                textInputAction: TextInputAction.search,
                enableSuggestions: false,
              ),
            ),
          ),
          Expanded(
            child: ListView.builder(
              padding: const EdgeInsets.only(
                left: 4,
                right: 4,
                bottom: 12,
              ),
              itemBuilder: (context, index) {
                final nacionalidad = nacionalidades[index];
                return ListTile(
                  contentPadding: const EdgeInsets.symmetric(
                    horizontal: 12,
                  ),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(12),
                  ),
                  selected: nacionalidad.codigoPais == widget.value?.codigoPais,
                  selectedColor: AppColors.primary700,
                  title: Text(nacionalidad.nombrePais),
                  onTap: () {
                    widget.onChanged(nacionalidad);
                    Navigator.pop(context);
                  },
                );
              },
              itemCount: nacionalidades.length,
            ),
          ),
        ],
      ),
    );
  }
}
