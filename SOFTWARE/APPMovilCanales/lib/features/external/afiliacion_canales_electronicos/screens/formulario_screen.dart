import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/providers/afiliacion_canales_electronicos_provider.dart';
import 'package:caja_tacna_app/features/external/afiliacion_canales_electronicos/widgets/input_clave_tarjeta.dart';
import 'package:caja_tacna_app/features/shared/data/documentos.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_tarjeta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';

class FormularioScreen extends ConsumerStatefulWidget {
  const FormularioScreen({super.key});

  @override
  FormularioScreenState createState() => FormularioScreenState();
}

class FormularioScreenState extends ConsumerState<FormularioScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(afiliacionCanElectProvider.notifier).initForm();
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      appBar: AppBar(
        scrolledUnderElevation: 0,
        automaticallyImplyLeading: false,
        toolbarHeight: 64,
        flexibleSpace: SafeArea(
          child: Container(
            height: 64,
            padding: const EdgeInsets.symmetric(horizontal: 24),
            child: Row(
              children: [
                Container(
                  width: 32,
                  height: 32,
                  decoration: const BoxDecoration(
                    shape: BoxShape.circle,
                    color: AppColors.primary100,
                  ),
                  child: TextButton(
                    style: TextButton.styleFrom(
                      shape: const CircleBorder(),
                      padding: EdgeInsets.zero,
                    ),
                    onPressed: () {
                      context.pop();
                    },
                    child: SvgPicture.asset(
                      'assets/icons/shared/chevron-left.svg',
                      colorFilter: const ColorFilter.mode(
                        AppColors.primary700,
                        BlendMode.srcIn,
                      ),
                      width: 24,
                      height: 24,
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
      body: const CustomScrollView(
        physics: ClampingScrollPhysics(),
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _FormularioView(),
          )
        ],
      ),
    );
  }
}

class _FormularioView extends ConsumerWidget {
  const _FormularioView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final afiliacionState = ref.watch(afiliacionCanElectProvider);

    return Container(
      padding: EdgeInsets.only(
        left: 24,
        right: 24,
        bottom: 54 + MediaQuery.of(context).padding.bottom,
        top: 36,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Spacer(),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Image.asset(
                'assets/images/logo_rojo.png',
                width: 220,
              )
            ],
          ),
          const SizedBox(
            height: 44,
          ),
          const Text(
            '¡Hola!',
            style: TextStyle(
              fontSize: 40,
              fontWeight: FontWeight.w700,
              height: 1.35,
              leadingDistribution: TextLeadingDistribution.even,
            ),
            textAlign: TextAlign.left,
          ),
          const SizedBox(
            height: 8,
          ),
          const Text(
            'Afíliate ingresando tus datos',
            textAlign: TextAlign.center,
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w400,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          CtInputNumeroTarjeta(
            numeroTarjeta: afiliacionState.numeroTarjeta,
            onChanged: (value) {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .changeNumeroTarjeta(value);
            },
            withObscureText: true,
          ),
          const SizedBox(
            height: 16,
          ),
          CtInputNumeroDocumento(
            tiposDocumento: tiposDocumento,
            tipoDocumento: afiliacionState.documento,
            numeroDocumento: afiliacionState.numeroDocumento,
            onChangeTipoDocumento: (documento) {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .changeDocumento(documento);
            },
            onChangeNumeroDocumento: (numeroDocumento) {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .changeNumeroDocumento(numeroDocumento);
            },
            withObscureText: true,
            tipoValidacion: TipoValidacionDocumento.validacion1,
          ),
          const SizedBox(
            height: 16,
          ),
          InputClaveTajeta(
            value: afiliacionState.claveTarjeta,
            onChanged: (value) {
              ref
                  .read(afiliacionCanElectProvider.notifier)
                  .changeClaveTarjeta(value);
            },
          ),
          const SizedBox(
            height: 49,
          ),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(afiliacionCanElectProvider.notifier).goPaso2();
            },
          ),
        ],
      ),
    );
  }
}
