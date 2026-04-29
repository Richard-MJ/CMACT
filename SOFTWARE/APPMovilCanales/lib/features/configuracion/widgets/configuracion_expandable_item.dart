import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/constants/codigo_enlace.dart';
import 'package:caja_tacna_app/features/external/providers/parametros_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:url_launcher/url_launcher.dart';

class OtrosProductosExpandableItem extends ConsumerStatefulWidget {
  const OtrosProductosExpandableItem({
    super.key,
    required this.label,
    required this.icon,
  });

  final String label;
  final String icon;

  @override
  ConsumerState<OtrosProductosExpandableItem> createState() =>
      _OtrosProductosExpandableItemState();
}

class _OtrosProductosExpandableItemState
    extends ConsumerState<OtrosProductosExpandableItem> {
  bool _isExpanded = false;

  Future<void> _launchUrl(String? urlDoc) async {
    if (urlDoc != null && urlDoc.isNotEmpty) {
      final Uri url = Uri.parse(urlDoc);
      await launchUrl(
        url,
        mode: LaunchMode.externalApplication,
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final parametros = ref.watch(parametrosProvider);

    final enlaceReclamo = parametros.enlaces
        ?.where((x) => x.codigoDocumento == CodigoEnlace.reclamo)
        .firstOrNull;

    final enlaceRequerimiento = parametros.enlaces
        ?.where((x) => x.codigoDocumento == CodigoEnlace.requerimiento)
        .firstOrNull;

    return Column(
      children: [
        CtCardButton(
          padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 18),
          onPressed: () {
            setState(() {
              _isExpanded = !_isExpanded;
            });
          },
          color: AppColors.gray100,
          child: Row(
            children: [
              SvgPicture.asset(
                widget.icon,
                height: 24,
                colorFilter: const ColorFilter.mode(
                  AppColors.primary700,
                  BlendMode.srcIn,
                ),
              ),
              const SizedBox(width: 8),
              Expanded(
                child: Text(
                  widget.label,
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ),
              AnimatedRotation(
                turns: _isExpanded ? 0.5 : 0,
                duration: const Duration(milliseconds: 200),
                child: const Icon(
                  Icons.keyboard_arrow_down,
                  color: AppColors.primary700,
                ),
              ),
            ],
          ),
        ),
        AnimatedCrossFade(
          firstChild: const SizedBox.shrink(),
          secondChild: Container(
            margin: const EdgeInsets.only(left: 32, top: 8),
            child: Column(
              children: [
                Padding(
                  padding: const EdgeInsets.only(bottom: 8),
                  child: _buildSubItem(
                    label: 'Reclamo',
                    onPressed: () => _launchUrl(enlaceReclamo?.urlDocumento),
                  ),
                ),
                Padding(
                  padding: const EdgeInsets.only(bottom: 8),
                  child: _buildSubItem(
                    label: 'Requerimiento',
                    onPressed: () =>
                        _launchUrl(enlaceRequerimiento?.urlDocumento),
                  ),
                ),
              ],
            ),
          ),
          crossFadeState: _isExpanded
              ? CrossFadeState.showSecond
              : CrossFadeState.showFirst,
          duration: const Duration(milliseconds: 200),
        ),
      ],
    );
  }

  Widget _buildSubItem({
    required String label,
    required VoidCallback onPressed,
  }) {
    return CtCardButton(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 14),
      onPressed: onPressed,
      color: AppColors.gray100,
      child: Row(
        children: [
          Expanded(
            child: Text(
              label,
              style: const TextStyle(
                fontSize: 14,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 22 / 14,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
        ],
      ),
    );
  }
}
