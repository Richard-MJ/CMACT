import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:card_swiper/card_swiper.dart';
import 'package:flutter/material.dart';

class HomeCarousel extends StatelessWidget {
  final List<Widget> children;

  const HomeCarousel({super.key, required this.children});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      height: 120,
      child: Swiper(
        itemBuilder: (BuildContext context, int index) {
          return Column(
            children: [
              children[index],
            ],
          );
        },
        itemCount: children.length,
        pagination: const SwiperPagination(
          builder: DotSwiperPaginationBuilder(
            activeColor: AppColors.primary700,
            color: AppColors.gray300,
            size: 8.0,
            activeSize: 8.0,
            space: 4.0,
          ),
          margin: EdgeInsets.zero,
          alignment: Alignment.bottomCenter,
        ),
        loop: true,
        autoplay: true,
        autoplayDelay: 4000,
      ),
    );
  }
}
