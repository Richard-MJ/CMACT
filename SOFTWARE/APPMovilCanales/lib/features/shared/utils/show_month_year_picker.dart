import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

const List<String> meses = [
  'Ene',
  'Feb',
  'Mar',
  'Abr',
  'May',
  'Jun',
  'Jul',
  'Ago',
  'Set',
  'Oct',
  'Nov',
  'Dic',
];

void showMonthYearPicker({
  required BuildContext context,
  required Function(int month, int year) onChange,
  DateTime? initialDate,
}) {
  int selectedMonth = 0;
  int selectedYear = 0;

  final currentMonth = DateTime.now().month;
  final currentYear = DateTime.now().year;
  if (initialDate == null) {
    selectedMonth = currentMonth;
    selectedYear = currentYear;
  } else {
    selectedMonth = initialDate.month;
    selectedYear = initialDate.year;
  }

  // Establece el índice inicial del mes
  final FixedExtentScrollController monthScrollController =
      FixedExtentScrollController(initialItem: selectedMonth - 1);

  // Establece el índice inicial del año
  final FixedExtentScrollController yearScrollController =
      FixedExtentScrollController(initialItem: selectedYear - currentYear);

  // onChange(selectedMonth, selectedYear);

  showDialog<void>(
    context: context,
    barrierDismissible: false,
    builder: (BuildContext context) {
      return Dialog(
        shape: AppTheme.modalShape,
        insetPadding: AppTheme.modalInsetPadding,
        child: Container(
          padding:
              const EdgeInsets.only(bottom: 16, right: 16, left: 16, top: 16),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: [
              const Text(
                'Seleccione una fecha',
                style: TextStyle(
                  fontSize: 18,
                  fontWeight: FontWeight.w800,
                  color: AppColors.black,
                  height: 28 / 18,
                  leadingDistribution: TextLeadingDistribution.even,
                ),
              ),
              Container(
                height: 250,
                padding: const EdgeInsets.symmetric(horizontal: 4, vertical: 8),
                child: Stack(
                  children: [
                    Center(
                      child: Container(
                        height: 40,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(8),
                          color: Colors.black12,
                        ),
                      ),
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        SizedBox(
                          width: 90,
                          child: ListWheelScrollView.useDelegate(
                            physics: const FixedExtentScrollPhysics()
                                .applyTo(const BouncingScrollPhysics()),
                            onSelectedItemChanged: (value) {
                              selectedMonth = value + 1;
                              // onChange(selectedMonth, selectedYear);
                            },
                            controller: monthScrollController,
                            itemExtent: 40,
                            diameterRatio: 1.2,
                            perspective: 0.005,
                            childDelegate: ListWheelChildBuilderDelegate(
                              childCount: 12,
                              builder: (context, index) {
                                return Center(
                                  child: Text(
                                    meses[index],
                                    style: const TextStyle(
                                      fontSize: 28,
                                    ),
                                  ),
                                );
                              },
                            ),
                          ),
                        ),
                        SizedBox(
                          width: 90,
                          child: ListWheelScrollView.useDelegate(
                            physics: const FixedExtentScrollPhysics()
                                .applyTo(const BouncingScrollPhysics()),
                            onSelectedItemChanged: (value) {
                              selectedYear = DateTime.now().year + value;
                              // onChange(selectedMonth, selectedYear);
                            },
                            controller: yearScrollController,
                            itemExtent: 40,
                            diameterRatio: 1.2,
                            perspective: 0.005,
                            childDelegate: ListWheelChildBuilderDelegate(
                              childCount: 8,
                              builder: (context, index) {
                                return Center(
                                  child: Text(
                                    (DateTime.now().year + index).toString(),
                                    style: const TextStyle(
                                      fontSize: 28,
                                    ),
                                  ),
                                );
                              },
                            ),
                          ),
                        ),
                      ],
                    )
                  ],
                ),
              ),
              const SizedBox(
                height: 8,
              ),
              Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  CtButton(
                    text: 'Cancelar',
                    onPressed: () {
                      Navigator.of(context).pop(true);
                    },
                    type: ButtonType.outline,
                    width: 100,
                    borderRadius: 8,
                  ),
                  const SizedBox(
                    width: 8,
                  ),
                  CtButton(
                    text: 'Aceptar',
                    onPressed: () {
                      Navigator.of(context).pop(true);
                      onChange(selectedMonth, selectedYear);
                    },
                    width: 100,
                    borderRadius: 8,
                  )
                ],
              )
            ],
          ),
        ),
      );
    },
  );
}
