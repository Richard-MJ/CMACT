import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/providers/afiliacion_celular_provider.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/widgets/billetera_header.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/widgets/cuenta_afiliacion_card.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/widgets/dialog_desafiliar_celular.dart';
import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/widgets/mensaje_alerta.dart';
import 'package:caja_tacna_app/features/billetera_virtual/shared/widgets/billetera_action_buttons.dart';
import 'package:caja_tacna_app/features/billetera_virtual/widgets/input_celular.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_6.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class DatosOperacionScreen extends ConsumerStatefulWidget {
  const DatosOperacionScreen({super.key});

  @override
  DatosOperacionScreenState createState() => DatosOperacionScreenState();
}

class DatosOperacionScreenState extends ConsumerState<DatosOperacionScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(afiliacionCelularProvider.notifier).goAfiliacionCelular(context);
    });
  }

  @override
  Widget build(BuildContext context) {
    return const CtLayout6(
      title: 'Volver',
      actions: BilleteraActionButtons(
        showSettings: false,
      ),
      child: CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _DatosOperacionView(),
          )
        ],
      ),
    );
  }
}

class _DatosOperacionView extends ConsumerWidget {
  const _DatosOperacionView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final afiliacionCelularState = ref.watch(afiliacionCelularProvider);
    final datosAfiliacion = afiliacionCelularState.datosAfiliacion;

    final bool esAfiliado = afiliacionCelularState.esAfiliada;
    final bool esAfiliadoCCE = datosAfiliacion?.indicadorAfiliacionCCE ?? false;
    final bool disabledButton = !esAfiliadoCCE && !esAfiliado ||
        afiliacionCelularState.numeroCelular.isNotValid;

    return Scaffold(
      body: Padding(
        padding:
            const EdgeInsets.only(top: 12, left: 24, right: 24, bottom: 36),
        child: Column(
          children: <Widget>[
            Expanded(
              child: SingleChildScrollView(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    BilleteraHeader(showQrButton: esAfiliadoCCE),
                    const SizedBox(height: 18),
                    Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        const Text(
                          'Nro. Celular',
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w500,
                            color: AppColors.gray900,
                            height: 22 / 14,
                            leadingDistribution: TextLeadingDistribution.even,
                          ),
                        ),
                        const SizedBox(height: 16),
                        InputCelular(
                          numeroCelular: afiliacionCelularState.numeroCelular,
                          onChangeNumeroCelular: (numerocelular) {
                            ref
                                .read(afiliacionCelularProvider.notifier)
                                .changeNumeroCelular(numerocelular);
                          },
                          withObscureText: true,
                          enable: false,
                        ),
                      ],
                    ),
                    const SizedBox(height: 26),
                    _SeccionCuentaAfiliar(
                      esAfiliado: esAfiliado,
                      datosAfiliacion: datosAfiliacion,
                    ),
                    const SizedBox(height: 26),
                    if (esAfiliadoCCE) _SeccionNotificaciones(),
                  ],
                ),
              ),
            ),

            if (!esAfiliado) ...[
              const SizedBox(height: 12),
              const MensajeAlerta(),
            ],
            const SizedBox(height: 18),

            _BotonAccion(
              esAfiliado: esAfiliado,
              esAfiliadoCCE: esAfiliadoCCE,
              disabled: disabledButton,
            ),
          ],
        ),
      ),
    );
  }
}

class _SeccionNotificaciones extends ConsumerWidget {
  const _SeccionNotificaciones();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            CtCheckbox(
              value: ref
                  .watch(afiliacionCelularProvider)
                  .notificarOperacionesRecibidas,
              onPressed: () {
                ref
                    .read(afiliacionCelularProvider.notifier)
                    .changeNotificarOperacionesRecibidas(!ref
                        .watch(afiliacionCelularProvider)
                        .notificarOperacionesRecibidas);
              },
              disabled: !ref.watch(afiliacionCelularProvider).esAfiliada,
            ),
            const SizedBox(
              width: 8,
            ),
            RichText(
              text: TextSpan(
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                children: <TextSpan>[
                  TextSpan(
                    text: 'Notificaciones por ',
                    style: const TextStyle(color: AppColors.black),
                  ),
                  TextSpan(
                    text: 'operaciones recibidas',
                    style: const TextStyle(color: AppColors.primary700),
                  ),
                ],
              ),
            ),
          ],
        ),
        const SizedBox(
          height: 10,
        ),
        Row(
          children: [
            CtCheckbox(
              value: ref
                  .watch(afiliacionCelularProvider)
                  .notificarOperacionesEnviadas,
              onPressed: () {
                ref
                    .read(afiliacionCelularProvider.notifier)
                    .changeNotificarOperacionesEnviadas(!ref
                        .watch(afiliacionCelularProvider)
                        .notificarOperacionesEnviadas);
              },
              disabled: !ref.watch(afiliacionCelularProvider).esAfiliada,
            ),
            const SizedBox(
              width: 8,
            ),
            RichText(
              text: TextSpan(
                style: const TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
                children: <TextSpan>[
                  TextSpan(
                    text: 'Notificaciones por ',
                    style: const TextStyle(color: AppColors.black),
                  ),
                  TextSpan(
                    text: 'operaciones enviadas',
                    style: const TextStyle(color: AppColors.primary700),
                  ),
                ],
              ),
            ),
          ],
        ),
      ],
    );
  }
}

class _SeccionCuentaAfiliar extends ConsumerWidget {
  const _SeccionCuentaAfiliar({
    required this.esAfiliado,
    required this.datosAfiliacion,
  });

  final bool esAfiliado;
  final dynamic datosAfiliacion;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          'Cuenta a afiliar',
          style: TextStyle(
            fontSize: 14,
            fontWeight: FontWeight.w500,
            color: AppColors.gray900,
            height: 22 / 14,
            leadingDistribution: TextLeadingDistribution.even,
          ),
        ),
        const SizedBox(height: 16),
        CuentaAfiliacionCard(
          esAfiliado: esAfiliado,
          nombreProducto: datosAfiliacion?.nombreProducto,
          numeroCuenta: datosAfiliacion?.numeroCuentaAfiliada,
          saldoDisponible: datosAfiliacion?.saldoDisponible,
          simboloMoneda: datosAfiliacion?.simboloMoneda,
          onPressed: () {
            ref
                .read(afiliacionCelularProvider.notifier)
                .selectCuenta(esAfiliado);
          },
        ),
      ],
    );
  }
}

class _BotonAccion extends ConsumerWidget {
  const _BotonAccion({
    required this.esAfiliado,
    required this.esAfiliadoCCE,
    required this.disabled,
  });

  final bool esAfiliado;
  final bool esAfiliadoCCE;
  final bool disabled;

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return CtButton(
      text: esAfiliadoCCE &&
              esAfiliado &&
              !ref.watch(afiliacionCelularProvider).esModificacion
          ? 'Enviar dinero'
          : 'Guardar cambios',
      type: esAfiliadoCCE &&
              esAfiliado &&
              !ref.watch(afiliacionCelularProvider).esModificacion
          ? ButtonType.outline
          : ButtonType.solid,
      onPressed: () async {
        if (esAfiliadoCCE && esAfiliado && !ref.watch(afiliacionCelularProvider).esModificacion) {
          context.pop();
        } else {
          if (!esAfiliado) {
            bool? continuar = await showDialog(
              context: context,
              builder: (BuildContext context) {
                return const DialogDesafiliarCelular();
              },
            );

            if (continuar == null || !continuar) return;
            if (!context.mounted) return;
          }
          ref.read(afiliacionCelularProvider.notifier).afiliar(withPush: true);
        }
      },
      disabled: disabled,
    );
  }
}
