import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:caja_tacna_app/features/perfil/providers/perfil_provider.dart';
import 'package:caja_tacna_app/features/perfil/widgets/input_apodo.dart';
import 'package:caja_tacna_app/features/perfil/widgets/input_celular.dart';
import 'package:caja_tacna_app/features/perfil/widgets/input_disabled.dart';
import 'package:caja_tacna_app/features/perfil/widgets/input_email.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/features/shared/utils/ct_utils.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_message.dart';
import 'package:caja_tacna_app/features/shared/widgets/info_red_card.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class PerfilScreen extends ConsumerStatefulWidget {
  const PerfilScreen({super.key});

  @override
  PerfilScreenState createState() => PerfilScreenState();
}

class PerfilScreenState extends ConsumerState<PerfilScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(perfilProvider.notifier).initDatos();
      ref.read(perfilProvider.notifier).getDatosIniciales();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout2(
      title: 'Mi perfil',
      child: const CustomScrollView(
        slivers: [
          SliverFillRemaining(
            hasScrollBody: false,
            child: _PerfilView(),
          )
        ],
      ),
    );
  }
}

class _PerfilView extends ConsumerWidget {
  const _PerfilView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final perfilState = ref.watch(perfilProvider);
    final homeState = ref.watch(homeProvider);

    return Container(
      padding: const EdgeInsets.only(
        top: 28,
        left: 24,
        right: 24,
        bottom: 56,
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Container(
            decoration: BoxDecoration(
              color: AppColors.gray50,
              border: Border.all(
                width: 1,
                color: AppColors.gray300,
              ),
              borderRadius: BorderRadius.circular(8),
            ),
            width: double.infinity,
            padding: const EdgeInsets.symmetric(
              horizontal: 16,
              vertical: 18,
            ),
            child: Column(
              children: [
                Text(
                  perfilState.datosIniciales?.nombreCompleto ?? '',
                  style: const TextStyle(
                    fontSize: 18,
                    fontWeight: FontWeight.w500,
                    color: AppColors.gray900,
                    height: 28 / 18,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
                const SizedBox(
                  height: 4,
                ),
                Text(
                  'DNI ${perfilState.datosIniciales?.dni}',
                  style: const TextStyle(
                    fontSize: 14,
                    fontWeight: FontWeight.w400,
                    color: AppColors.gray900,
                    height: 22 / 14,
                    leadingDistribution: TextLeadingDistribution.even,
                  ),
                ),
              ],
            ),
          ),
          const SizedBox(
            height: 24,
          ),
          const Text(
            'Apodo',
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
          InputApodo(
            onChangeApodo: (apodo) {
              ref.read(perfilProvider.notifier).changeApodo(apodo);
            },
            apodo: perfilState.apodo,
            hintText: perfilState.apodoLocal,
          ),
          const SizedBox(
            height: 16,
          ),
          if (!perfilState.tieneEmail) ...[
            InfoRedCard(
              content:
                  'No tienes un correo electrónico registrado, te recomendamos ingresarlo para optimizar tu experiencia de servicio.',
            ),
            const SizedBox(
              height: 16,
            ),
          ],
          const Text(
            'Correo electrónico',
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
          if (!perfilState.tieneEmail) ...[
            InputEmail(
              hintText: perfilState.datosIniciales?.correoElectronico,
              email: perfilState.email,
              onChangeEmail: (email) {
                ref
                    .read(perfilProvider.notifier)
                    .changeCorreoDestinatario(email);
              },
            )
          ] else ...[
            InputDisabled(
              content: perfilState.datosIniciales?.correoElectronico ?? '',
              svgIcon: 'assets/icons/mail.svg',
            )
          ],
          const SizedBox(
            height: 16,
          ),
          const Text(
            'Teléfono SMS',
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
          InputDisabled(
            content: CtUtils.hashNumeroCelular(
                homeState.datosCliente?.numeroTelefonoSms ?? ''),
            svgIcon: 'assets/icons/cellphone.svg',
          ),
          const SizedBox(
            height: 16,
          ),
          const Text(
            'Teléfono',
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
          InputCelular(
            numeroCelular: perfilState.numeroCelular,
            onChangeNumeroCelular: (numerocelular) {
              ref
                  .read(perfilProvider.notifier)
                  .changeNumeroCelular(numerocelular);
            },
            hintText: perfilState.datosIniciales?.numeroTelefonoCasa,
          ),
          const SizedBox(
            height: 6,
          ),
          const Text(
            'Solo sirve para contactar mas no para tus operaciones.',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w400,
              color: AppColors.gray500,
              height: 22 / 14,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
          const SizedBox(
            height: 16,
          ),
          const CtMessage(
            child: Expanded(
              child: Text(
                'Si deseas cambiar otros datos, acércate a nuestra red de agencias a nivel nacional.',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w400,
                  color: AppColors.gray900,
                  height: 22 / 14,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
          ),
          const SizedBox(
            height: 30,
          ),
          const Spacer(),
          CtButton(
            text: 'Continuar',
            onPressed: () {
              ref.read(perfilProvider.notifier).actualizar(withPush: true);
            },
            disabled: perfilState.btnDisabled,
          )
        ],
      ),
    );
  }
}
