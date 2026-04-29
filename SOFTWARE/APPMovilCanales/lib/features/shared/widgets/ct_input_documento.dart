import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/inputs/numero_documento.dart';
import 'package:caja_tacna_app/features/shared/models/select_tipo_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_textfield_container.dart';
import 'package:caja_tacna_app/features/shared/widgets/header_modal.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_svg/flutter_svg.dart';

enum TipoValidacionDocumento { validacion1, validacion2 }

class CtInputNumeroDocumento<T extends SelectTipoDocumento>
    extends StatelessWidget {
  const CtInputNumeroDocumento({
    super.key,
    required this.tipoDocumento,
    required this.numeroDocumento,
    required this.onChangeTipoDocumento,
    required this.onChangeNumeroDocumento,
    this.withObscureText = false,
    required this.tiposDocumento,
    this.readOnly = false,
    required this.tipoValidacion,
  });

  final T? tipoDocumento;
  final NumeroDocumento numeroDocumento;
  final void Function(T tipoDocumento) onChangeTipoDocumento;
  final void Function(NumeroDocumento numeroDocumento) onChangeNumeroDocumento;
  final bool withObscureText;
  final List<T> tiposDocumento;
  final bool readOnly;
  final TipoValidacionDocumento tipoValidacion;

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
                  final documento = tiposDocumento[index];
                  return ListTile(
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12.0),
                    ),
                    selected: documento.idTipoDocumento ==
                        tipoDocumento?.idTipoDocumento,
                    selectedColor: AppColors.primary700,
                    title: Text(documento.descripcion.toUpperCase()),
                    onTap: () {
                      onChangeTipoDocumento(documento);
                      Navigator.pop(context);
                    },
                  );
                },
                itemCount: tiposDocumento.length,
              ),
            ],
          ),
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return CtTextFieldContainer(
      padding: const EdgeInsets.only(left: 14, right: 4),
      errorMessage: numeroDocumento.errorMessage,
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.center,
        children: [
          GestureDetector(
            onTap: () {
              if (readOnly) return;
              FocusManager.instance.primaryFocus?.unfocus();
              _showOptionsModal(context);
            },
            child: Row(
              children: [
                Container(
                  constraints: const BoxConstraints(
                    maxWidth: 80,
                  ),
                  child: Text(
                    '${tipoDocumento?.descripcion.toUpperCase()}',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                      color: readOnly
                          ? AppColors.inputReadOnly
                          : AppColors.gray800,
                      overflow: TextOverflow.ellipsis,
                    ),
                    maxLines: 1,
                  ),
                ),
                SvgPicture.asset(
                  'assets/icons/chevron-down.svg',
                  height: 20,
                )
              ],
            ),
          ),
          const SizedBox(
            width: 12,
          ),
          Expanded(
            child: _InputNumeroDocumento(
              numeroDocumento: numeroDocumento,
              withObscureText: withObscureText,
              onChangeNumeroDocumento: onChangeNumeroDocumento,
              tipoDocumento: tipoDocumento,
              readOnly: readOnly,
              tipoValidacion: tipoValidacion,
            ),
          ),
        ],
      ),
    );
  }
}

class _InputNumeroDocumento<T extends SelectTipoDocumento>
    extends StatefulWidget {
  const _InputNumeroDocumento({
    required this.numeroDocumento,
    required this.onChangeNumeroDocumento,
    this.withObscureText = false,
    required this.tipoDocumento,
    this.readOnly = false,
    required this.tipoValidacion,
  });

  final NumeroDocumento numeroDocumento;
  final void Function(NumeroDocumento numeroDocumento) onChangeNumeroDocumento;
  final bool withObscureText;
  final T? tipoDocumento;
  final bool readOnly;
  final TipoValidacionDocumento tipoValidacion;

  @override
  State<_InputNumeroDocumento> createState() => _InputNumeroDocumentoState();
}

class _InputNumeroDocumentoState extends State<_InputNumeroDocumento> {
  @override
  void initState() {
    super.initState();
    setState(() {
      showText = false;
    });
    _focusNode.addListener(() {
      if (!_focusNode.hasFocus) {
        widget.onChangeNumeroDocumento(
          NumeroDocumento.dirty(widget.numeroDocumento.value),
        );
      }
    });
  }

  @override
  void dispose() {
    _focusNode.dispose();
    super.dispose();
  }

  bool showText = false;
  final FocusNode _focusNode = FocusNode();
  final TextEditingController controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    controller.value = controller.value.copyWith(
      text: widget.numeroDocumento.value,
      selection: TextSelection.collapsed(
        offset: controller.selection.end,
      ),
    );

    List<TextInputFormatter> inputFormatters = [];
    TextInputType? textInputType;

    //DNI: numero y máximo 8 caracteres
    if (widget.tipoDocumento?.idTipoDocumento == 1) {
      inputFormatters = [
        FilteringTextInputFormatter.digitsOnly,
        LengthLimitingTextInputFormatter(8),
      ];

      textInputType = TextInputType.number;
    } else {
      if (widget.tipoValidacion == TipoValidacionDocumento.validacion1) {
        // Alfanumérico y máximo 20 caracteres. (Ni Ñ ni tildes)
        inputFormatters = [
          FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z0-9]')),
          LengthLimitingTextInputFormatter(20),
        ];
        textInputType = TextInputType.text;
      }

      if (widget.tipoValidacion == TipoValidacionDocumento.validacion2) {
        //RUC: numérico y máximo 11 caracteres
        if (widget.tipoDocumento?.idTipoDocumento == 12) {
          inputFormatters = [
            FilteringTextInputFormatter.digitsOnly,
            LengthLimitingTextInputFormatter(11),
          ];
          textInputType = TextInputType.number;
        }

        //Otros documentos: numérico y máximo 12 caracteres
        if (widget.tipoDocumento?.idTipoDocumento == 19) {
          inputFormatters = [
            FilteringTextInputFormatter.digitsOnly,
            LengthLimitingTextInputFormatter(12),
          ];
          textInputType = TextInputType.text;
        }

        // Carnet de extranjería: Alfanumérico y máximo 12 caracteres. (Ni Ñ ni tildes)
        if (widget.tipoDocumento?.idTipoDocumento == 2) {
          inputFormatters = [
            FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z0-9]')),
            LengthLimitingTextInputFormatter(12),
          ];
          textInputType = TextInputType.text;
        }

        // Pasaporte: Alfanumérico y máximo 12 caracteres. (Ni Ñ ni tildes)
        if (widget.tipoDocumento?.idTipoDocumento == 5) {
          inputFormatters = [
            FilteringTextInputFormatter.allow(RegExp(r'[a-zA-Z0-9]')),
            LengthLimitingTextInputFormatter(12),
          ];
          textInputType = TextInputType.text;
        }
      }
    }

    return Row(
      children: [
        Expanded(
          child: TextFormField(
            onTapOutside: (event) {
              FocusManager.instance.primaryFocus?.unfocus();
            },
            style: TextStyle(
              color:
                  widget.readOnly ? AppColors.inputReadOnly : AppColors.gray800,
              fontSize: 16,
              fontWeight: FontWeight.w400,
            ),
            decoration: const InputDecoration(
              border: InputBorder.none,
              contentPadding: EdgeInsets.zero,
              hintText: 'N° de documento',
              isDense: true,
              hintStyle: TextStyle(
                color: AppColors.inputHint,
                fontSize: 16,
                fontWeight: FontWeight.w400,
              ),
            ),
            controller: controller,
            keyboardType: textInputType,
            obscureText: widget.withObscureText ? !showText : false,
            obscuringCharacter: '*',
            onChanged: (value) {
              widget.onChangeNumeroDocumento(
                widget.numeroDocumento.isPure
                    ? NumeroDocumento.pure(value)
                    : NumeroDocumento.dirty(value),
              );
            },
            inputFormatters: inputFormatters,
            focusNode: _focusNode,
            readOnly: widget.readOnly,
            enableSuggestions: false,
          ),
        ),
        const SizedBox(
          width: 8,
        ),
        widget.withObscureText
            ? SizedBox(
                width: 36,
                height: 36,
                child: TextButton(
                  style: TextButton.styleFrom(
                    shape: const CircleBorder(),
                    padding: EdgeInsets.zero,
                  ),
                  onPressed: () {
                    setState(() {
                      showText = !showText;
                    });
                  },
                  child: SvgPicture.asset(
                    showText
                        ? 'assets/icons/eye_closed.svg'
                        : 'assets/icons/eye.svg',
                    width: 16,
                    height: 16,
                    colorFilter: const ColorFilter.mode(
                        AppColors.gray400, BlendMode.srcIn),
                  ),
                ),
              )
            : const SizedBox(
                width: 10,
              ),
      ],
    );
  }
}
