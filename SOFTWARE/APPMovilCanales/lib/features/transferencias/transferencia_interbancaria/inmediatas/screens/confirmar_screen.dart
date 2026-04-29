import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/providers/transferencia_interbancaria_inmediata_provider.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_agregar_operaciones_frecuentes.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_alias_operacion.dart';
import 'package:caja_tacna_app/features/shared/providers/timer_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_timer.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_otp.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:animate_do/animate_do.dart';
import 'package:flutter/gestures.dart';
import 'package:flutter/material.dart';

class ConfirmarTransferenciaInterbancariaInmediataScreen extends ConsumerStatefulWidget {
  const ConfirmarTransferenciaInterbancariaInmediataScreen({super.key});

  @override
  ConfirmarTransferenciaInterbancariaScreenState createState() =>
      ConfirmarTransferenciaInterbancariaScreenState();
}

class ConfirmarTransferenciaInterbancariaScreenState
    extends ConsumerState<ConfirmarTransferenciaInterbancariaInmediataScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout2(
      title: 'Confirma la operación',
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _ConfirmarTransferenciaInterbancariaView(),
          )
        ],
      ),
    );
  }
}

class _ConfirmarTransferenciaInterbancariaView extends ConsumerWidget {
  const _ConfirmarTransferenciaInterbancariaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaState = ref.watch(transferenciaInterbancariaInmediataProvider);
    final timerState = ref.watch(timerProvider);
    final bool disabledButton = transferenciaState.tokenDigital.isEmpty ||
        transferenciaState.tokenDigital.length != 6 ||
        !transferenciaState.aceptarTerminos;

    return Container(
      padding: const EdgeInsets.only(top: 24, bottom: 24, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Operación',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              SizedBox(
                width: 200,
                child: Text(
                  'Transferencia inmediata a otro banco',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 19 / 16,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                  textAlign: TextAlign.end,
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta origen',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(
                width: 30,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      transferenciaState.cuentaOrigen!.alias,
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCCI(
                        numerocci: transferenciaState.cuentaOrigen!.numeroProducto,
                      ),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Cuenta destino',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              const SizedBox(
                width: 10,
              ),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.end,
                  children: [
                    Text(
                      transferenciaState.nombreEntidadBeneficiaria,
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      transferenciaState.nombreBeneficiario.value,
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray900,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                    Text(
                      CtUtils.formatNumeroCuenta(
                        numeroCuenta:
                            transferenciaState.cuentaDestino.value,
                      ),
                      style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w400,
                        color: AppColors.gray500,
                        height: 19 / 16,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                      textAlign: TextAlign.end,
                    ),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Monto',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  double.parse(transferenciaState.monto.value),
                  transferenciaState.cuentaOrigen?.simboloMonedaProducto,
                ),
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'Comisión',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  transferenciaState.montosTotales?.controlMonto.montoComisionEntidad,
                  transferenciaState.cuentaOrigen?.simboloMonedaProducto,
                ),
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const SizedBox(
            height: 37,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'ITF',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Text(
                CtUtils.formatCurrency(
                  transferenciaState.montosTotales?.controlMonto.itf,
                  transferenciaState.cuentaOrigen?.simboloMonedaProducto,
                ),
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 19 / 16,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                textAlign: TextAlign.end,
              ),
            ],
          ),
          const SizedBox(
            height: 36,
          ),
          Center(
            child: CtAgregarOperacionesFrecuentes(
              value: transferenciaState.operacionFrecuente,
              onChanged: () {
                ref
                    .read(transferenciaInterbancariaInmediataProvider.notifier)
                    .toggleOperacionFrecuente();
              },
            ),
          ),
          FadeIn(
            animate: transferenciaState.operacionFrecuente,
            duration: const Duration(milliseconds: 150),
            child: Container(
              padding: const EdgeInsets.only(top: 16),
              child: InputAliasOperacion(
                alias: transferenciaState.nombreOperacionFrecuente,
                onChanged: (operacionFrecuente) {
                  ref
                      .read(transferenciaInterbancariaInmediataProvider.notifier)
                      .changeNombreOperacionFrecuente(operacionFrecuente);
                },
              ),
            ),
          ),
          const Spacer(),
          const SizedBox(
            height: 20,
          ),
          const Text(
            'Ingresa tu Token Digital',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w800,
              color: AppColors.gray900,
              height: 28 / 16,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 20,
          ),
          CtOtp(
            value: transferenciaState.tokenDigital,
            onChanged: (value) {
              ref
                  .read(transferenciaInterbancariaInmediataProvider.notifier)
                  .changeToken(value);
            },
          ),
          const SizedBox(
            height: 13,
          ),
          timerState.timerOn
              ? const CtTimer()
              : Align(
                  alignment: Alignment.centerRight,
                  child: GestureDetector(
                    onTap: () {
                      ref
                          .read(transferenciaInterbancariaInmediataProvider.notifier)
                          .transferir(withPush: false);
                    },
                    child: const Text(
                      'Solicitar nuevo token',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.primary700,
                        height: 28 / 14,
                        leadingDistribution: TextLeadingDistribution.even,
                      ),
                    ),
                  ),
                ),
          const SizedBox(
            height: 36,
          ),
          Row(
            children: [
              CtCheckbox(
                value: transferenciaState.aceptarTerminos,
                onPressed: () {
                  ref
                      .read(transferenciaInterbancariaInmediataProvider.notifier)
                      .toggleAceptarTerminos();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              Container(
                width: double.infinity,
                constraints: const BoxConstraints(maxWidth: 260),
                child: RichText(
                  text: TextSpan(
                    style: const TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      height: 22 / 14,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                    children: <TextSpan>[
                      const TextSpan(
                        text: 'Acepta los ',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                      TextSpan(
                        text: 'beneficios, riesgos y condiciones',
                        style: const TextStyle(color: AppColors.primary700),
                        recognizer: TapGestureRecognizer()
                          ..onTap = () async {
                            CtUtils.abrirWeb(url: transferenciaState.documentoTermino!.urlDocumento);
                          },
                      ),
                      const TextSpan(
                        text: ' de los servicios electrónicos',
                        style: TextStyle(color: AppColors.gray700),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 11,
          ),
          CtButton(
            text: 'Confirmar',
            onPressed: () {
              ref.read(transferenciaInterbancariaInmediataProvider.notifier).confirmar();
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}