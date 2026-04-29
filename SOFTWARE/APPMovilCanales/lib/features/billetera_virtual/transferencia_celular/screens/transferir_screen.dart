import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/billetera_virtual/shared/widgets/billetera_action_buttons.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_6.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_card.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/input_monto.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TransferirScreen extends ConsumerStatefulWidget {
  const TransferirScreen({super.key});

  @override
  TransferirScreenState createState() =>
      TransferirScreenState();
}

class TransferirScreenState
    extends ConsumerState<TransferirScreen> {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout6(
      title: 'Billetera virtual',
      actions: BilleteraActionButtons(
        showSettings: false,
      ),
      child: _TransferirView(),
    );
  }
}

class _TransferirView extends ConsumerWidget {
  const _TransferirView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferirState = ref.watch(transferenciaCelularProvider);
    final bool disabledButton = transferirState.montoTransferencia.isNotValid ||
        transferirState.entidadFinancieraSeleccionada == null;

    return Container(
      padding: const EdgeInsets.only(top: 24, left: 24, right: 24, bottom: 36),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: [
          CtCard(
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
              vertical: 18,
            ),
            child: Column(
              children: [
                const Text(
                  'Transferir a',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 8,
                ),
                Text(
                  transferirState.contactoSeleccionada!.nombreAlias.isEmpty ? 
                    transferirState.contactoSeleccionada!.numeroCelular
                    : transferirState.contactoSeleccionada!.nombreAlias,
                  style: TextStyle(
                    fontSize: transferirState.contactoSeleccionada!.nombreAlias.isEmpty ? 22 : 15,
                    fontWeight: FontWeight.w600,
                    color: AppColors.gray900,
                    height: 1.5,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                if(transferirState.contactoSeleccionada!.nombreAlias.isNotEmpty 
                  && transferirState.tokenCodigoQr.isEmpty)
                const SizedBox(
                  height: 8,
                ),
                if(transferirState.contactoSeleccionada!.nombreAlias.isNotEmpty
                  && transferirState.tokenCodigoQr.isEmpty)
                Text(
                  transferirState.contactoSeleccionada!.numeroCelular,
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          const Text(
            'Monto a enviar',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w500,
              color: AppColors.gray900,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 6,
          ),
          InputMontoTransferir(
            onChangeMonto: (value) {
              ref.read(transferenciaCelularProvider.notifier).changeMonto(value);
            },
            monto: transferirState.montoTransferencia,
            simboloMoneda: transferirState.simboloMoneda
          ),
          const SizedBox(
            height: 2,
          ),
          Text(
            'Monto máximo por transferencia ${CtUtils.formatCurrency(
              transferirState.limiteMontoMaximo,
              transferirState.datosAfiliacion?.simboloMoneda,
            )}, Monto máximo por transferencia diaria ${CtUtils.formatCurrency(
              transferirState.montoMaximoDia,
              transferirState.datosAfiliacion?.simboloMoneda,
            )}',
            style: const TextStyle(
              fontSize: 10,
              fontWeight: FontWeight.w300,
              color: AppColors.gray800,
              height: 18 / 10,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          const Divider(
            color: AppColors.gray300,
            thickness: 1.0,
            height: 0,
          ),
          const SizedBox(
            height: 16,
          ),
          Expanded(
            child: ListView.separated(
              padding: const EdgeInsets.only(
                left: 0,
                right: 0,
                bottom: 0,
              ),
              separatorBuilder: (context, index) {
                return const SizedBox(
                  height: 16,
                );
              },
              itemBuilder: (context, index) {
                final entidadFinanciera = transferirState.listaEntidadesFinancieras[index];
                final isSelected = 
                  transferirState.entidadFinancieraSeleccionada?.codigoEntidad == entidadFinanciera.codigoEntidad;

                return Container(
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(8),
                    color: isSelected ? AppColors.gray100 : AppColors.gray25,
                    border:
                        isSelected ? null : Border.all(color: AppColors.gray300),
                    boxShadow: AppColors.shadowSm,
                  ),
                  child: TextButton(
                    onPressed: () {
                      ref
                          .read(transferenciaCelularProvider.notifier)
                          .changeEntidadFinanciera(entidadFinanciera);
                    },
                    style: TextButton.styleFrom(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(8),
                      ),
                      padding: EdgeInsets.zero,
                    ),
                    child: Container(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 18,
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Container(
                            width: 16,
                            height: 16,
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(8),
                              border: Border.all(
                                color: isSelected
                                    ? AppColors.primary600
                                    : AppColors.gray300,
                                width: 1,
                              ),
                            ),
                            child: isSelected
                                ? Center(
                                    child: Container(
                                      width: 6,
                                      height: 6,
                                      decoration: BoxDecoration(
                                        borderRadius: BorderRadius.circular(3),
                                        color: AppColors.primary600,
                                      ),
                                    ),
                                  )
                                : null,
                          ),
                          const SizedBox(
                            width: 16,
                          ),
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            mainAxisAlignment: MainAxisAlignment.start,
                            children: [
                              Text(
                                entidadFinanciera.nombreEntidad ?? "",
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w500,
                                  color: AppColors.gray900,
                                  height: 22 / 14,
                                  leadingDistribution:
                                      TextLeadingDistribution.even,
                                ),
                              )
                            ],
                          )
                        ],
                      ),
                    ),
                  ),
                );
              },
              itemCount: transferirState.listaEntidadesFinancieras.length,
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          CtButton(
            text: 'Enviar',
            onPressed: () {
              if(transferirState.tokenCodigoQr.isEmpty){
                ref.read(transferenciaCelularProvider.notifier).transferir(withPush: true);
              }else{
                ref.read(transferenciaCelularProvider.notifier).obtenerMontosYToken(withPush: true);
              }
            },
            disabled: disabledButton,
          )
        ],
      ),
    );
  }
}
