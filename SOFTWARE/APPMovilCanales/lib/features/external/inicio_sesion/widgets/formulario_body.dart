import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/core/providers/app_version_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/providers/login_provider.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/input_clave_internet.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/libro_reclamaciones.dart';

import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/olvide_mi_clave.dart';
import 'package:caja_tacna_app/features/external/inicio_sesion/widgets/requerimientos.dart';

import 'package:caja_tacna_app/features/shared/data/documentos.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_checkbox.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_documento.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_input_tarjeta.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';

class FormularioBody extends ConsumerWidget {
  const FormularioBody({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final loginState = ref.watch(loginProvider);
    final appVersion = ref.watch(appVersionProvider).appVersion;

    return Container(
      padding: EdgeInsets.only(
        left: 24,
        right: 24,
        top: 36,
        bottom: 20 + MediaQuery.of(context).padding.bottom,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Spacer(),
          Center(
            child: Row(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Image.asset(
                  'assets/images/logo_rojo.png',
                  width: 220,
                ),
                const SizedBox(
                  width: 2,
                ),
                Container(
                  padding: const EdgeInsets.only(bottom: 4),
                  child: Text(
                    'v.$appVersion',
                    style: const TextStyle(
                      fontSize: 10,
                      fontWeight: FontWeight.w500,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                      color: AppColors.gray500,
                    ),
                    textAlign: TextAlign.right,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 38,
          ),
          const Text(
            'Te damos la bienvenida',
            style: TextStyle(
              fontSize: 24,
              fontWeight: FontWeight.w600,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
              color: AppColors.gray900,
            ),
            textAlign: TextAlign.left,
          ),
          const SizedBox(
            height: 8,
          ),
          Row(
            children: [
              const Text(
                '¿Eres nuevo? ',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                  color: AppColors.gray900,
                ),
              ),
              GestureDetector(
                child: const Text(
                  'Afíliate aquí.',
                  style: TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                    color: AppColors.primary700,
                  ),
                ),
                onTap: () => ref.read(loginProvider.notifier).goAfiliarse(),
              )
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          CtInputNumeroTarjeta(
            numeroTarjeta: loginState.numeroTarjeta,
            onChanged: (value) {
              ref.read(loginProvider.notifier).changeNumeroTarjeta(value);
            },
            withObscureText: true,
          ),
          const SizedBox(
            height: 8,
          ),
          Row(
            children: [
              CtCheckbox(
                value: loginState.recordarTarjeta,
                onPressed: () {
                  ref.read(loginProvider.notifier).toggleRecordarTarjeta();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              const Text(
                'Recordar tarjeta',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w500,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                  color: AppColors.gray700,
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          CtInputNumeroDocumento(
            tiposDocumento: tiposDocumento,
            tipoDocumento: loginState.documento,
            numeroDocumento: loginState.numeroDocumento,
            onChangeTipoDocumento: (documento) {
              ref.read(loginProvider.notifier).changeDocumento(documento);
            },
            onChangeNumeroDocumento: (numeroDocumento) {
              ref
                  .read(loginProvider.notifier)
                  .changeNumeroDocumento(numeroDocumento);
            },
            withObscureText: true,
            tipoValidacion: TipoValidacionDocumento.validacion1,
          ),
          const SizedBox(
            height: 8,
          ),
          Row(
            children: [
              CtCheckbox(
                value: loginState.recordarDocumento,
                onPressed: () {
                  ref.read(loginProvider.notifier).toggleRecordarDocumento();
                },
              ),
              const SizedBox(
                width: 8,
              ),
              const Text(
                'Recordar documento',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w500,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                  color: AppColors.gray700,
                ),
              ),
            ],
          ),
          const SizedBox(
            height: 16,
          ),
          Row(
            children: [
              const Expanded(
                child: InputClaveInternet(),
              ),
              if (loginState.passwordBiometrico != null) ...[
                const SizedBox(
                  width: 16,
                ),
                GestureDetector(
                  onTap: () {
                    ref.read(loginProvider.notifier).iniciarSesionBiometria();
                  },
                  child: SvgPicture.asset(
                    loginState.passwordBiometrico?.codigoTipoBiometria == 1
                        ? 'assets/icons/touch_id.svg'
                        : 'assets/icons/face_id.svg',
                    height: 43,
                  ),
                ),
              ],
            ],
          ),
          const SizedBox(
            height: 6,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.end,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [OlvideMiClave()],
          ),
          const SizedBox(
            height: 71,
          ),
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              const Requerimientos(
                width: 97,
              ),
              const LibroReclamaciones(width: 100)
            ],
          )
        ],
      ),
    );
  }
}
