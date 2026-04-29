import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';
import 'package:flutter_svg/flutter_svg.dart';

class DialogTerminosCondiciones extends StatelessWidget {
  const DialogTerminosCondiciones({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        automaticallyImplyLeading: false,
        toolbarHeight: 0,
        elevation: 0,
        scrolledUnderElevation: 0,
        backgroundColor: AppColors.primary50,
      ),
      backgroundColor: AppColors.primary50,
      body: SizedBox(
        width: double.infinity,
        height: double.infinity,
        child: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Padding(
              padding: const EdgeInsets.only(
                top: 18,
                left: 24,
                right: 18,
                bottom: 0,
              ),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  SizedBox(
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
                ],
              ),
            ),
            Container(
              padding: const EdgeInsetsDirectional.symmetric(
                horizontal: 24,
              ),
              child: const Text(
                'Términos y condiciones',
                style: TextStyle(
                  fontSize: 24,
                  fontWeight: FontWeight.w600,
                  color: AppColors.black,
                  height: 1.5,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            Expanded(
              child: SingleChildScrollView(
                child: Container(
                  padding: const EdgeInsetsDirectional.symmetric(
                    horizontal: 24,
                  ),
                  child: const Text(
                    'Lorem ipsum dolor sit amet consectetur. Dictum ac ac velit mauris urna in at. Velit feugiat nec fusce tortor. Vitae at volutpat scelerisque lacus natoque fringilla risus nisi. Et et tortor tellus vel feugiat. Sit porttitor elementum vel pharetra nibh nisl nulla elit. Porttitor nibh sodales risus sed habitasse diam sit enim mauris. Erat amet lectus quam mattis sem id bibendum. Pretium urna velit in neque lorem ut velit adipiscing. Nulla ullamcorper nulla elit ultricies. Euismod hendrerit sapien vel aliquet amet fames. At tristique ut in amet bibendum vitae venenatis. Sit libero nulla maecenas montes velit massa. Proin nibh egestas sit in at. Eget urna est suscipit massa nam morbi turpis cursus sit. Arcu et non malesuada eget. Sed ut dictum aenean habitant ipsum leo convallis massa sem. Eget nam amet sit amet. Lorem ipsum dolor sit amet consectetur. Dictum ac ac velit mauris urna in at. Velit feugiat nec fusce tortor. Vitae at volutpat scelerisque lacus natoque fringilla risus nisi. Et et tortor tellus vel feugiat. Sit porttitor elementum vel pharetra nibh nisl nulla elit. Porttitor nibh sodales risus sed habitasse diam sit enim mauris. Erat amet lectus quam mattis sem id bibendum. Pretium urna velit in neque lorem ut velit adipiscing. Nulla ullamcorper nulla elit ultricies. Euismod hendrerit sapien vel aliquet amet fames. At tristique ut in amet bibendum vitae venenatis. Sit libero nulla maecenas montes velit massa. Proin nibh egestas sit in at. Eget urna est suscipit massa nam morbi turpis cursus sit. Arcu et non malesuada eget. Sed ut dictum aenean habitant ipsum leo convallis massa sem. Eget nam amet sit amet.',
                    style: TextStyle(
                      fontSize: 16,
                      fontWeight: FontWeight.w400,
                      color: AppColors.black,
                      height: 1.5,
                      leadingDistribution: TextLeadingDistribution.even,
                    ),
                  ),
                ),
              ),
            ),
            const SizedBox(
              height: 24,
            ),
            Center(
              child: CtButton(
                text: 'Acepto',
                onPressed: () {
                  Navigator.of(context).pop(true);
                },
              ),
            ),
            const SizedBox(
              height: 54,
            ),
          ],
        ),
      ),
    );
  }
}
