import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/widgets/input_clave_aleatoria.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DesafiliarScreen extends ConsumerStatefulWidget {
  const DesafiliarScreen({super.key});

  @override
  DesafiliarScreenState createState() => DesafiliarScreenState();
}

class DesafiliarScreenState extends ConsumerState<DesafiliarScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {});
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Volver',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DesafiliarView(),
          )
        ],
      ),
    );
  }
}

class _DesafiliarView extends ConsumerWidget {
  const _DesafiliarView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tokenState = ref.watch(tokenDigitalProvider);
    bool disabledButton = tokenState.claveCajero.isNotValid ||
        tokenState.claveInternet.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 23, bottom: 56),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              'Desafiliarme del Token Digital',
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.w800,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
          ),
          const SizedBox(
            height: 48,
          ),
          const Text(
            'Clave de tarjeta',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputClaveAleatoria(
            titulo: 'Ingrese su clave de tarjeta',
            value: tokenState.claveCajero.value,
            onChanged: (valor) {
              ref
                  .read(tokenDigitalProvider.notifier)
                  .changeClaveCajero(ClaveTarjeta.dirty(valor));
            },
            length: 4,
            errorMessage: tokenState.claveCajero.errorMessage,
            hint: 'Clave de tarjeta',
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Clave de internet',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 24 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          InputClaveAleatoria(
            titulo: 'Ingrese su clave de internet',
            value: tokenState.claveInternet.value,
            onChanged: (valor) {
              ref
                  .read(tokenDigitalProvider.notifier)
                  .changeClaveInternet(ClaveInternet.dirty(valor));
            },
            length: 6,
            errorMessage: tokenState.claveInternet.errorMessage,
            hint: 'Clave de Internet',
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          CtButton(
            text: 'Desafiliarme',
            onPressed: () {
              ref.read(tokenDigitalProvider.notifier).desafiliar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}
