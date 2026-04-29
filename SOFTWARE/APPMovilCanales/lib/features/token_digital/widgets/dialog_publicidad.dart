import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/home/providers/home_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_svg/flutter_svg.dart';
import 'package:card_swiper/card_swiper.dart';

class DialogPublicidad extends ConsumerWidget {
  const DialogPublicidad({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final publicidades = ref.watch(homeProvider).publicidades;

    return AlertDialog(
      shape: RoundedRectangleBorder(
        borderRadius: BorderRadius.circular(16),
      ),
      elevation: 0,
      backgroundColor: AppColors.primary50,
      insetPadding: const EdgeInsets.symmetric(horizontal: 24),
      contentPadding: EdgeInsets.zero,
      content: Stack(
        children: [
          ClipRRect(
            borderRadius: BorderRadius.circular(16),
            child: SizedBox(
              width: double.maxFinite,
              height: MediaQuery.of(context).size.width - 48,
              child: Swiper(
                autoplay: true,
                itemBuilder: (BuildContext context, int index) {
                  final publicidad = publicidades[index];

                  return Image.network(
                    publicidad.urlImagen,
                    fit: BoxFit.contain,
                    loadingBuilder: (context, child, loadingProgress) {
                      if (loadingProgress == null) {
                        return child;
                      }
                      return const Center(
                        child: CircularProgressIndicator(
                          strokeWidth: 4,
                          color: AppColors.primary700,
                        ),
                      );
                    },
                  );
                },
                itemCount: publicidades.length,
              ),
            ),
          ),
          Positioned(
            right: 18,
            top: 18,
            child: SizedBox(
              height: 40,
              width: 40,
              child: TextButton(
                style: TextButton.styleFrom(
                  shape: const CircleBorder(),
                  padding: EdgeInsets.zero,
                  backgroundColor: AppColors.primary100,
                ),
                onPressed: () {
                  Navigator.pop(context);
                },
                child: SvgPicture.asset(
                  'assets/icons/x.svg',
                  height: 20,
                  colorFilter: const ColorFilter.mode(
                    AppColors.primary700,
                    BlendMode.srcIn,
                  ),
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
