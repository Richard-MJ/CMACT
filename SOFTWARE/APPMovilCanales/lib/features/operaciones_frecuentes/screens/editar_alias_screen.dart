import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/widgets/input_alias.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class EditarAliasScreen extends ConsumerStatefulWidget {
  const EditarAliasScreen({super.key});

  @override
  EditarAliasScreenState createState() => EditarAliasScreenState();
}

class EditarAliasScreenState extends ConsumerState<EditarAliasScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Editar alias',
      child: _EditarAliasView(),
    );
  }
}

class _EditarAliasView extends ConsumerWidget {
  const _EditarAliasView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final operacionesState = ref.watch(operacionesFrecuentesProvider);
    final detalleOperacion =
        ref.watch(operacionesFrecuentesProvider).detalleOperacion;

    return CustomScrollView(
      slivers: [
        SliverFillRemaining(
          child: Container(
            padding: const EdgeInsets.only(
              left: 24,
              right: 24,
              top: 34,
              bottom: 56,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
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
                InputAliasOpFrecuente(
                  onChangeAlias: (alias) {
                    ref
                        .read(operacionesFrecuentesProvider.notifier)
                        .changeNuevoAlias(alias);
                  },
                  alias: operacionesState.nuevoAlias,
                  hintText: detalleOperacion?.nombreOperacionFrecuente,
                ),
                const Spacer(),
                CtButton(
                  text: 'Guardar cambios',
                  onPressed: () {
                    ref
                        .read(operacionesFrecuentesProvider.notifier)
                        .editarAliasOperacion();
                  },
                  disabled: operacionesState.btnAliasDisabled,
                ),
              ],
            ),
          ),
        )
      ],
    );
  }
}
