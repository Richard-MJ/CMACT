import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/dialog_numero_externo.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/skeleton_contactos.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/monto_disponible.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/widgets/input_buscar.dart';
import 'package:caja_tacna_app/features/billetera_virtual/shared/widgets/billetera_action_buttons.dart';
import 'package:caja_tacna_app/features/shared/providers/snackbar_provider.dart';
import 'package:caja_tacna_app/features/shared/services/snackbar_service.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_6.dart';
import 'package:permission_handler/permission_handler.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

class ContactosCelularScreen extends ConsumerStatefulWidget {
  const ContactosCelularScreen({super.key});

  @override
  ContactosCelularScreenState createState() => ContactosCelularScreenState();
}

class ContactosCelularScreenState
    extends ConsumerState<ContactosCelularScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(transferenciaCelularProvider.notifier).goTransferenciaCelular();
      ref.read(transferenciaCelularProvider.notifier).listarContactos();
    });
  }

  @override
  Widget build(BuildContext context) {
    return CtLayout6(
      title: 'Billetera Virtual',
      onBack: () {
        context.pop();
      },
      actions: const BilleteraActionButtons(
        showSettings: true,
      ),
      child: const _ContactosCelularView(),
    );
  }
}

class _ContactosCelularView extends ConsumerWidget {
  const _ContactosCelularView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferirState = ref.watch(transferenciaCelularProvider);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.stretch,
      children: [
        Container(
          padding: const EdgeInsets.only(
            left: 24,
            right: 24,
            top: 24,
          ),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const Row(
                children: [
                  Text(
                    'Busca a quién enviar',
                    style: TextStyle(
                      fontSize: 14,
                      fontWeight: FontWeight.w500,
                      color: AppColors.black,
                    ),
                  ),
                ],
              ),
              InputBuscar(
                value: transferirState.search,
                onChanged: (value) {
                  ref
                      .read(transferenciaCelularProvider.notifier)
                      .changeSearch(value);
                },
                onSubmitted: () async {
                  if (await Permission.camera.request().isGranted) {
                    if (!context.mounted) return;
                    context.push(
                      '/billetera-virtual/transferencia-celular/qr-scanner',
                    );
                  } else {
                    ref.read(snackbarProvider.notifier).showSnackbar(
                          "La aplicación no tiene permiso para acceder a tu camara.",
                          SnackbarType.info,
                        );
                  }
                },
              ),
              const SizedBox(height: 10),
              GestureDetector(
                onTap: () async {
                  bool? continuar = await showDialog(
                    context: context,
                    builder: (_) => const DialogNumeroExternoScreen(),
                  );

                  if (continuar != true) return;
                  if (!context.mounted) return;

                  ref
                      .read(transferenciaCelularProvider.notifier)
                      .buscarContacto(context);
                },
                child: Row(
                  children: [
                    SvgPicture.asset(
                      'assets/icons/phone.svg',
                      height: 20,
                      width: 20,
                      colorFilter: const ColorFilter.mode(
                        AppColors.primary700,
                        BlendMode.srcIn,
                      ),
                    ),
                    const SizedBox(width: 8),
                    const Text(
                      'Enviar a otros números',
                      style: TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w500,
                        color: AppColors.primary700,
                      ),
                    ),
                  ],
                ),
              ),
              const SizedBox(height: 16),
              const Text(
                'Saldo Disponible',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w500,
                  color: AppColors.black,
                ),
              ),
              const SizedBox(height: 8),
              MontoDisponible(
                monto: transferirState.montoDisponible,
                simboloMoneda: transferirState.simboloMoneda,
              ),
              const SizedBox(height: 16),
              const Text(
                'Contactos',
                style: TextStyle(
                  fontSize: 14,
                  fontWeight: FontWeight.w500,
                  color: AppColors.black,
                ),
              ),
            ],
          ),
        ),
        const SizedBox(height: 10),
        Expanded(
          child: Builder(
            builder: (_) {
              if (transferirState.cargandoContactos ?? false) {
                return const ContactosSkeleton();
              }

              return ListView.separated(
                padding: const EdgeInsets.only(
                  left: 24,
                  right: 24,
                  bottom: 58,
                ),
                separatorBuilder: (_, __) => const SizedBox(height: 10),
                itemCount: transferirState.filtroContactos.length,
                itemBuilder: (context, index) {
                  final contacto = transferirState.filtroContactos[index];

                  return ListTile(
                    contentPadding: EdgeInsetsDirectional.zero,
                    dense: true,
                    minVerticalPadding: 0,
                    onTap: () {
                      ref
                          .read(transferenciaCelularProvider.notifier)
                          .selectContacto(contacto, context);
                    },
                    title: Container(
                      padding: const EdgeInsets.symmetric(
                        horizontal: 16,
                        vertical: 8,
                      ),
                      decoration: const BoxDecoration(
                        border: Border(
                          bottom: BorderSide(
                            width: 2,
                            color: AppColors.gray200,
                          ),
                        ),
                      ),
                      child: Row(
                        children: [
                          Container(
                            width: 36,
                            height: 36,
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(18),
                              color: AppColors.primary100,
                            ),
                            child: Center(
                              child: Text(
                                contacto.nombreAlias.isEmpty
                                    ? ''
                                    : contacto.nombreAlias
                                        .substring(0, 1)
                                        .toUpperCase(),
                                style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w500,
                                  color: AppColors.primary700,
                                ),
                              ),
                            ),
                          ),
                          const SizedBox(width: 16),
                          Expanded(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  contacto.nombreAlias,
                                  style: const TextStyle(
                                    fontSize: 14,
                                    fontWeight: FontWeight.w500,
                                    color: AppColors.gray900,
                                  ),
                                ),
                                Text(
                                  contacto.numeroCelular,
                                  style: const TextStyle(
                                    fontSize: 12,
                                    fontWeight: FontWeight.w500,
                                    color: AppColors.gray700,
                                  ),
                                ),
                              ],
                            ),
                          ),
                          SvgPicture.asset(
                            'assets/icons/chevron-right.svg',
                            height: 20,
                            width: 20,
                          ),
                        ],
                      ),
                    ),
                  );
                },
              );
            },
          ),
        ),
      ],
    );
  }
}
