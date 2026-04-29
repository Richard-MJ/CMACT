import 'package:caja_tacna_app/features/shared/layouts/ct_layout_4.dart';
import 'package:caja_tacna_app/features/configuracion/widgets/configuracion_item.dart';
import 'package:caja_tacna_app/features/configuracion/widgets/configuracion_expandable_item.dart';
import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';
import 'package:caja_tacna_app/features/token_digital/providers/token_digital_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class ConfiguracionScreen extends StatelessWidget {
  const ConfiguracionScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return const CtLayout4(
      title: 'Mi configuración',
      child: _ConfiguracionesView(),
    );
  }
}

class _ConfiguracionesView extends ConsumerWidget {
  const _ConfiguracionesView();

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return ListView(
      padding: const EdgeInsets.only(top: 36, left: 24, right: 24, bottom: 76),
      children: [
        OtrosProductosItem(
          label: 'Mi perfil',
          onPressed: () {
            context.push('/perfil/datos');
          },
          icon: 'assets/icons/configuracion/mi_perfil.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Configurar mi tarjeta',
          onPressed: () {
            context.push('/tarjeta/datos');
          },
          icon: 'assets/icons/configuracion/app_debito.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Operaciones frecuentes',
          onPressed: () {
            context.push('/operaciones-frecuentes/lista-operaciones');
          },
          icon: 'assets/icons/configuracion/ops_frecuentes.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Configurar cuentas',
          onPressed: () {
            context.push('/configurar-cuentas/lista-cuentas');
          },
          icon: 'assets/icons/configuracion/app_ahorros.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Activar biometría',
          onPressed: () {
            context.push('/biometria/seguridad');
          },
          icon: 'assets/icons/configuracion/biometria.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Token Digital',
          onPressed: () {
            ref.read(tokenDigitalProvider.notifier).goTokenDigital();
          },
          icon: 'assets/icons/configuracion/token_digital.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Cambiar clave de internet',
          onPressed: () {
            context.push('/cambio-clave/clave-actual');
          },
          icon: 'assets/icons/configuracion/cambio_clave.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Compras por internet',
          onPressed: () {
            context.push('/compras-internet/configurar-afiliacion');
          },
          icon: 'assets/icons/configuracion/compras_internet.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: '¿Donde inicié sesión?',
          onPressed: () {
            context.push('/sesion-canal-electronico/dispositivos-seguros');
          },
          icon: 'assets/icons/configuracion/lupa.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosExpandableItem(
          label: 'Genera tu reclamo o requerimiento',
          icon: 'assets/icons/configuracion/reclamo.svg',
        ),
        const SizedBox(height: 16),
        OtrosProductosItem(
          label: 'Cerrar sesión',
          onPressed: () {
            ref.read(authProvider.notifier).logout();
          },
          buttonRed: true,
          icon: 'assets/icons/configuracion/cerrar_sesion.svg',
        ),
      ],
    );
  }
}
