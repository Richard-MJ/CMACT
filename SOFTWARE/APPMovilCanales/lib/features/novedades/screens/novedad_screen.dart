import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/novedades/providers/novedades_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class NovedadScreen extends StatelessWidget {
  const NovedadScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Novedad',
      child: _NovedadView(),
    );
  }
}

class _NovedadView extends ConsumerWidget {
  const _NovedadView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final novedad = ref.watch(novedadesProvider).novedad;

    if (novedad == null) return Container();

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: Container(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              top: 28,
              bottom: 43,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  novedad.titulo,
                  style: const TextStyle(
                    fontSize: 24,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 36 / 24,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 9,
                ),
                Text(
                  'Vigencia: Del ${novedad.fechaInicioFormat} al ${novedad.fechaFinalFormat}.',
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 9,
                ),
                ClipRRect(
                  borderRadius: BorderRadius.circular(8),
                  child: SizedBox(
                    width: double.infinity,
                    height: 226,
                    child: Image.network(
                      novedad.urlImagen,
                      fit: BoxFit.cover,
                      errorBuilder: (context, error, stackTrace) {
                        return Container();
                      },
                    ),
                  ),
                ),
                const SizedBox(
                  height: 24,
                ),
                Text(
                  novedad.descripcion,
                  style: const TextStyle(
                    fontSize: 16,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 24 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const Spacer(),
                CtButton(
                  text: 'Volver al inicio',
                  onPressed: () {
                    context.go('/home');
                  },
                  type: ButtonType.outline,
                )
              ],
            ),
          ),
        )
      ],
    );
  }
}
