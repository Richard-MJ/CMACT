import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/inmediatas/screens/transferir_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/diferidas/screens/transferir_screen.dart';
import 'package:caja_tacna_app/features/transferencias/transferencia_interbancaria/providers/transferencia_interbancaria_provider.dart';
import 'package:caja_tacna_app/features/operaciones_frecuentes/providers/operaciones_frecuentes_provider.dart';
import 'package:caja_tacna_app/features/shared/layouts/ct_layout_2.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter/material.dart';

class TransferenciaInterbancariaScreen extends ConsumerStatefulWidget {
  const TransferenciaInterbancariaScreen({super.key});

  @override
  ConsumerState<TransferenciaInterbancariaScreen> createState() =>
      _TransferenciaInterbancariaScreenState();
}

class _TransferenciaInterbancariaScreenState
    extends ConsumerState<TransferenciaInterbancariaScreen> {
  late final PageController _pageController;
  bool _initialized = false;

  @override
  void initState() {
    super.initState();
    _pageController = PageController();

    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (_initialized) return;

      final operacionFrecuente = ref.read(operacionesFrecuentesProvider).operacionSeleccionada;
      final notifier = ref.read(tipoTransferenciaProvider.notifier);

      if (operacionFrecuente == null) {
        notifier.state = TipoTransferencia.inmediata;
        _pageController.jumpToPage(0);
      } else {
        final tipoOperacion = operacionFrecuente.numeroTipoOperacionFrecuente;
        if (tipoOperacion == 4) {
          notifier.state = TipoTransferencia.diferida;
          _pageController.jumpToPage(1);
        } else if (tipoOperacion == 10) {
          notifier.state = TipoTransferencia.inmediata;
          _pageController.jumpToPage(0);
        }
      }

      _initialized = true;
    });
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {

    ref.listen(tipoTransferenciaProvider, (previous, next) {
      if (next == TipoTransferencia.inmediata) {
        _pageController.jumpToPage(0);
      } else {
        _pageController.jumpToPage(1);
      }
    });

    return CtLayout2(
      title: 'Transferencia a otros bancos',
      child: Column(
        children: [
          const SizedBox(height: 16),

          _TipoTransferenciaSelector(controller: _pageController),
          const SizedBox(height: 16),

          Expanded(
            child: PageView(
              controller: _pageController,
              physics: const NeverScrollableScrollPhysics(),
              children: const [
                TransferirInterbancarioInmediataScreen(),
                TransferirInterbancarioDiferidaScreen(),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

class _TipoTransferenciaSelector extends ConsumerWidget {
  final PageController controller;

  const _TipoTransferenciaSelector({required this.controller});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final tipo = ref.watch(tipoTransferenciaProvider);

    return Padding(
      padding: const EdgeInsets.symmetric(horizontal: 64),
      child: Row(
        children: TipoTransferencia.values.map((tipoActual) {
          return Expanded(
            child: _buildOpcion(
              context,
              ref,
              tipoActual,
              tipo == tipoActual,
              controller,
            ),
          );
        }).toList(),
      ),
    );
  }

  Widget _buildOpcion(
    BuildContext context,
    WidgetRef ref,
    TipoTransferencia tipo,
    bool seleccionado,
    PageController controller,
  ) {
    final texto = tipo == TipoTransferencia.inmediata ? 'Inmediata' : 'Diferida';

    return GestureDetector(
      onTap: () {
        ref.read(tipoTransferenciaProvider.notifier).state = tipo;
      },
      child: AnimatedContainer(
        duration: const Duration(milliseconds: 250),
        curve: Curves.easeInOut,
        padding: const EdgeInsets.symmetric(vertical: 8),
        decoration: BoxDecoration(
          color: seleccionado ? AppColors.primary100 : Colors.white,
          border: Border.all(
            color: seleccionado ? AppColors.primary500 : AppColors.gray300,
          ),
          borderRadius: tipo == TipoTransferencia.inmediata
              ? const BorderRadius.only(
                  topLeft: Radius.circular(8),
                  bottomLeft: Radius.circular(8),
                )
              : const BorderRadius.only(
                  topRight: Radius.circular(8),
                  bottomRight: Radius.circular(8),
                ),
        ),
        alignment: Alignment.center,
        child: Text(
          texto,
          style: TextStyle(
            fontWeight: FontWeight.w500,
            color: seleccionado ? AppColors.primary500 : AppColors.gray700,
          ),
        ),
      ),
    );
  }
}