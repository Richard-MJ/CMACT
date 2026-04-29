import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter_svg/flutter_svg.dart';

class HomeBottomNavigation extends StatelessWidget {
  final bool esAfiliadoBilleteraVirtual;

  const HomeBottomNavigation({
    super.key,
    required this.esAfiliadoBilleteraVirtual,
  });

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        boxShadow: <BoxShadow>[
          BoxShadow(
            color: const Color.fromARGB(186, 239, 241, 244),
            blurRadius: 10,
            offset: const Offset(0, -1),
          ),
        ],
      ),
      child: BottomAppBar(
        padding: EdgeInsets.zero,
        height: 90,
        shape: const CircularNotchedRectangle(),
        color: const Color.fromARGB(189, 255, 255, 255),
        surfaceTintColor: Colors.white,
        clipBehavior: Clip.antiAlias,
        notchMargin: 10,
        child: Padding(
          padding: const EdgeInsets.only(top: 0, bottom: 10),
          child: Row(
            children: [
              Expanded(
                child: InkWell(
                  onTap: () {
                    if (esAfiliadoBilleteraVirtual) {
                      context.push(
                          '/billetera-virtual/transferencia-celular/contactos');
                    } else {
                      context.push(
                          '/billetera-virtual/afiliacion/datos-operacion');
                    }
                  },
                  splashColor: Colors.transparent,
                  highlightColor: Colors.transparent,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Material(
                        color: Colors.transparent,
                        child: InkWell(
                          customBorder: const CircleBorder(),
                          onTap: () {
                            if (esAfiliadoBilleteraVirtual) {
                              context.push(
                                  '/billetera-virtual/transferencia-celular/contactos');
                            } else {
                              context.push(
                                  '/billetera-virtual/afiliacion/datos-operacion');
                            }
                          },
                          child: Container(
                            width: 40,
                            height: 40,
                            padding: const EdgeInsets.all(8),
                            decoration: const BoxDecoration(
                              color: AppColors.primary200,
                              shape: BoxShape.circle,
                            ),
                            child: SvgPicture.asset(
                              'assets/icons/otras_operaciones/billetera.svg',
                              colorFilter: const ColorFilter.mode(
                                AppColors.primary700,
                                BlendMode.srcIn,
                              ),
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 4),
                      const Text(
                        'Billetera\nvirtual',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 12,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 1.1,
                          leadingDistribution: TextLeadingDistribution.even,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              Expanded(
                child: InkWell(
                  onTap: () {
                    context.push('/operaciones-frecuentes/lista-operaciones');
                  },
                  splashColor: Colors.transparent,
                  highlightColor: Colors.transparent,
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Material(
                        color: Colors.transparent,
                        child: InkWell(
                          customBorder: const CircleBorder(),
                          onTap: () {
                            context.push(
                                '/operaciones-frecuentes/lista-operaciones');
                          },
                          child: Container(
                            width: 40,
                            height: 40,
                            padding: const EdgeInsets.all(8),
                            decoration: const BoxDecoration(
                              color: AppColors.primary200,
                              shape: BoxShape.circle,
                            ),
                            child: SvgPicture.asset(
                              'assets/icons/configuracion/ops_frecuentes.svg',
                              colorFilter: const ColorFilter.mode(
                                AppColors.primary700,
                                BlendMode.srcIn,
                              ),
                            ),
                          ),
                        ),
                      ),
                      const SizedBox(height: 4),
                      const Text(
                        'Operaciones\nfrecuentes',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 12,
                          fontWeight: FontWeight.w500,
                          color: AppColors.gray900,
                          height: 1.1,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
