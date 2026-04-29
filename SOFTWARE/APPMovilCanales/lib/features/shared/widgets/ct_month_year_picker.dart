import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/month_year_picker.dart';
import 'package:flutter/material.dart';

class CtMonthYearPicker extends StatelessWidget {
  const CtMonthYearPicker({
    super.key,
    required this.value,
    required this.onChange,
  });

  final DateTime? value;
  final void Function(int, int) onChange;

  @override
  Widget build(BuildContext context) {
    final valueText = value == null
        ? 'mm/aaaa'
        : value!.month < 10
            ? ('0${value!.month}/${value!.year}')
            : ('${value!.month}/${value!.year}');

    return GestureDetector(
      onTap: () {
        showDialog(
          context: context,
          builder: (context) {
            return MonthYearPicker(
              onChange: (month, year) {
                onChange(month, year);
              },
              selectedMonth: value?.month,
              selectedYear: value?.year,
            );
          },
        );
      },
      child: Container(
        decoration: BoxDecoration(
          border: Border.all(
            color: AppColors.gray300,
            width: 1,
          ),
          borderRadius: BorderRadius.circular(8),
        ),
        height: 44,
        padding: const EdgeInsets.symmetric(horizontal: 14),
        margin: const EdgeInsets.only(top: 6),
        child: Align(
          alignment: Alignment.centerLeft,
          child: Text(
            valueText,
            style: TextStyle(
              fontSize: 16,
              fontWeight: FontWeight.w400,
              color: value == null ? AppColors.gray500 : AppColors.gray800,
              height: 1.5,
              leadingDistribution: TextLeadingDistribution.even,
            ),
          ),
        ),
      ),
    );
  }
}
