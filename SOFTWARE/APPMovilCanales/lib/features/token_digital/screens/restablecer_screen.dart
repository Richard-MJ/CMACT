import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_intenet.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/inputs/clave_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/widgets/input_clave_aleatoria.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/info_red_card.dart';
import 'package:flutter/material.dart';
import 'package:flutter/widgets.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class RestablecerScreen extends ConsumerStatefulWidget {
  const RestablecerScreen({super.key});

  @override
  RestablecerScreenState createState() => RestablecerScreenState();
}

class RestablecerScreenState extends ConsumerState<RestablecerScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(tokenDigitalProvider.notifier).resetClave();
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Volver',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _RestablecerView(),
          )
        ],
      ),
    );
  }
}

class _RestablecerView extends ConsumerWidget {
  const _RestablecerView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tokenState = ref.watch(tokenDigitalProvider);
    bool disabledButton = tokenState.claveCajero.isNotValid ||
        tokenState.claveInternet.isNotValid;

    return Container(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 24, bottom: 36),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Center(
            child: Text(
              'Restablecer Token Digital',
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
            height: 24,
          ),
          const Center(
            child: Text(
              'Podrás restablecer tu Token Digital acercando tu tarjeta de débito VISA a tu nuevo dispositivo con tecnología NFC.',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w400,
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
            height: 55,
          ),
          InfoRedCard(
            content:
                'Al restablecer tu Token Digital, se anulará la afiliación anterior y se activará automáticamente en este nuevo dispositivo.',
          ),
          const Spacer(),
          const SizedBox(
            height: 35,
          ),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              context.push('/token-digital/confirmar-restablecer');
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}
