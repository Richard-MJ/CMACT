import 'package:caja_tacna_app/features/pago_tarjetas_credito/diferidas/providers/pago_tarjetas_credito_diferida_provider.dart';
import 'package:caja_tacna_app/features/pago_tarjetas_credito/widgets/input_tarjeta_credito.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_cuenta.dart';
import 'package:caja_tacna_app/features/transferencias/widgets/input_nombre_beneficiario.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_select_entidad_financiera.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_stepper.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class IngresoDatosTarjetaDiferidaScreen extends ConsumerStatefulWidget {
  const IngresoDatosTarjetaDiferidaScreen({super.key});

  @override
  IngresoDatosTarjetaDiferidaScreenState createState() =>
      IngresoDatosTarjetaDiferidaScreenState();
}

class IngresoDatosTarjetaDiferidaScreenState
    extends ConsumerState<IngresoDatosTarjetaDiferidaScreen> {
  @override
  Widget build(BuildContext context) {
    return const CustomScrollView(
      slivers: [
        SliverFillRemaining(
          hasScrollBody: false,
          child: _IngresoDatosTarjetaView(),
        )
      ],
    );
  }

  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(pagoTarjetasCreditoDiferidaProvider.notifier).initDatos();
      ref.read(pagoTarjetasCreditoDiferidaProvider.notifier).getDatosIniciales();
    });
  }
}

class _IngresoDatosTarjetaView extends ConsumerWidget {
  const _IngresoDatosTarjetaView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final pagoState = ref.watch(pagoTarjetasCreditoDiferidaProvider);

    return Container(
      padding: const EdgeInsets.only(top: 0, bottom: 36, left: 24, right: 24),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          const Center(
            child: CtStepper(
              numPasos: 2,
              pasoActual: 1,
            ),
          ),
          const SizedBox(
            height: 18,
          ),
          const Text(
            'Ingresa los datos de la tarjeta',
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 28 / 18,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const Divider(
            color: AppColors.border1,
          ),
          const SizedBox(
            height: 12,
          ),
          const Text(
            'Banco de destino',
            style: TextStyle(
              fontSize: 15,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 8,
          ),
          CtSelectEntidadFinanciera(
            value: pagoState.entidadFinanciera,
            onChanged: (entidadFinanciera) {
              ref
                  .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                  .changeEntidadFinanciera(entidadFinanciera);
            },
            entidadesFinancieras: pagoState.entidadesFinancieras,
          ),
          if (pagoState.entidadFinanciera != null) ...[
            const SizedBox(
              height: 24,
            ),
            const Text(
              'Cuenta de cargo',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 24 / 16,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 8,
            ),
            CtSelectCuenta(
              cuentas: pagoState.cuentasOrigen,
              onChange: (producto) {
                ref
                    .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                    .changeCuentaOrigen(producto);
              },
              value: pagoState.cuentaOrigen,
            ),
            const SizedBox(
              height: 24,
            ),
            const Text(
              'N° de tarjeta de crédito',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 8,
            ),
            InputTarjetaCredito(
              numeroTarjeta: pagoState.numeroTarjetaCredito,
              onChanged: (value) {
                ref
                    .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                    .changeNumeroTarjetaCredito(value);
              },
            ),
            const SizedBox(
              height: 8,
            ),
            Row(
              children: [
                CtCheckbox(
                  value: pagoState.esTitular,
                  onPressed: () {
                    ref
                        .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                        .toggleEsTitular();
                  },
                ),
                const SizedBox(
                  width: 8,
                ),
                const Text(
                  'Soy titular de la cuenta',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray700,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
            const SizedBox(
              height: 16,
            ),
            const Text(
              'Nombres y apellidos del titular',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 8,
            ),
            InputNombreBeneficiario(
              nombreBeneficiario: pagoState.nombreBeneficiario,
              onChanged: (value) {
                ref
                    .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                    .changeNombreBeneficiario(value);
              },
              readOnly: pagoState.esTitular,
            ),
            const SizedBox(
              height: 16,
            ),
            const Text(
              'Nº de documento de identidad',
              style: TextStyle(
                fontSize: 15,
                fontWeight: FontWeight.w500,
                color: AppColors.gray900,
                height: 1.5,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 8,
            ),
            CtInputNumeroDocumento(
              tipoDocumento: pagoState.tipoDocumento,
              numeroDocumento: pagoState.numeroDocumento,
              onChangeTipoDocumento: (tipoDocumento) {
                ref
                    .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                    .changeTipoDocumento(tipoDocumento);
              },
              onChangeNumeroDocumento: (numeroDocumento) {
                ref
                    .read(pagoTarjetasCreditoDiferidaProvider.notifier)
                    .changeNumeroDocumento(numeroDocumento);
              },
              tiposDocumento: pagoState.tiposDocumento,
              tipoValidacion: TipoValidacionDocumento.validacion2,
              readOnly: pagoState.esTitular,
            ),
          ],
          const SizedBox(
              height: 24,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(pagoTarjetasCreditoDiferidaProvider.notifier).ingresarMonto();
            },
            disabled: pagoState.entidadFinanciera == null,
          )
        ],
      ),
    );
  }
}
