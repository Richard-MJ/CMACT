import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:flutter/material.dart';

/// Widget de switch personalizado con animación.
class CtSwitch extends StatelessWidget {
  const CtSwitch({
    super.key,
    required this.value,
    required this.onTap,
  });

  final bool value;
  final void Function() onTap;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        onTap();
      },
      child: Stack(
        children: [
          SizedBox(
            width: 40,
            height: 16,
            child: Center(
              child: AnimatedContainer(
                duration: const Duration(milliseconds: 300),
                width: 24,
                height: 10,
                decoration: BoxDecoration(
                  color: value ? AppColors.success400 : AppColors.gray100,
                  borderRadius: BorderRadius.circular(16),
                  border: value
                      ? null
                      : Border.all(color: AppColors.gray200, width: 1),
                  boxShadow: AppColors.shadowSm,
                ),
              ),
            ),
          ),
          AnimatedPositioned(
            duration: const Duration(milliseconds: 300),
            curve: Curves.easeInOut,
            left: value ? 24 : 0,
            child: Container(
              width: 16,
              height: 16,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(8),
                color: value ? AppColors.white : AppColors.gray100,
                border: value
                    ? null
                    : Border.all(color: AppColors.gray200, width: 1),
              ),
              child: Center(
                child: value
                    ? Container(
                        width: 2,
                        height: 6,
                        decoration: BoxDecoration(
                          color: AppColors.success400,
                          borderRadius: BorderRadius.circular(1),
                        ),
                      )
                    : Container(
                        width: 5,
                        height: 5,
                        decoration: BoxDecoration(
                          color: Colors.transparent,
                          borderRadius: BorderRadius.circular(2.5),
                          border:
                              Border.all(color: AppColors.gray200, width: 1),
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
