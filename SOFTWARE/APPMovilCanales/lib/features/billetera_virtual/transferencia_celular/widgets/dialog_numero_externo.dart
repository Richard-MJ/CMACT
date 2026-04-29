import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/billetera_virtual/widgets/input_celular.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/providers/transferencia_celular_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';


class DialogNumeroExternoScreen extends ConsumerStatefulWidget {
  const DialogNumeroExternoScreen({super.key});

  @override
  DialogNumeroExternoStateScreen createState() => DialogNumeroExternoStateScreen();
}

class DialogNumeroExternoStateScreen extends ConsumerState<DialogNumeroExternoScreen> {
  @override
  void initState() {
    super.initState();
    Future.microtask(() {
      ref.read(transferenciaCelularProvider.notifier).resetNumeroCelular();
    });
  }

  @override
  Widget build(BuildContext context) {
    return _DialogNumeroExternoView();
  }
}

class _DialogNumeroExternoView extends ConsumerWidget {
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final transferenciaCelularState = ref.watch(transferenciaCelularProvider);
    bool disabled = transferenciaCelularState.numeroCelular.isNotValid;
    
    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(8),
      ),
      elevation: 0,
      backgroundColor: AppColors.white,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      content: SizedBox(
        width: double.maxFinite,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [   
            const Text(
              textAlign: TextAlign.center,
              'Ingresa el número de celular.',
              style: TextStyle(
                fontSize: 16,
                fontWeight: FontWeight.w600,
                color: AppColors.black,
                height: 22 / 16,
                leadingDistribution: TextLeadingDistribution.even,
              ),
            ),
            const SizedBox(
              height: 12,
            ),
            InputCelular(
              numeroCelular: transferenciaCelularState.numeroCelular,
              onChangeNumeroCelular: (numerocelular) {
                ref
                    .read(transferenciaCelularProvider.notifier)
                    .changeNumeroCelular(numerocelular);
              },
              withObscureText: false,
              enable: true,
              placeholder: '',
              colorOscure: false,
              showText: true
            ),
            const SizedBox(
              height: 16,
            ),
            CtButton(
              text: 'Buscar',
              onPressed: () {
                Navigator.of(context).pop(true);
              },
              borderRadius: 8,
              width: double.infinity,
              disabled: disabled
            ),
          ],
        ),
      ),
    );
  }
}
