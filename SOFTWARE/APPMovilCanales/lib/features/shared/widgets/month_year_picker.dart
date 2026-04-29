import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/app_colors.dart';
import 'package:caja_tacna_app/features/shared/models/mes.dart';
import 'package:caja_tacna_app/features/shared/widgets/ct_button.dart';
import 'package:flutter/material.dart';

List<Mes> mesesInfo = [
  Mes(numero: 1, nombre: 'Ene'),
  Mes(numero: 2, nombre: 'Feb'),
  Mes(numero: 3, nombre: 'Mar'),
  Mes(numero: 4, nombre: 'Abr'),
  Mes(numero: 5, nombre: 'May'),
  Mes(numero: 6, nombre: 'Jun'),
  Mes(numero: 7, nombre: 'Jul'),
  Mes(numero: 8, nombre: 'Ago'),
  Mes(numero: 9, nombre: 'Set'),
  Mes(numero: 10, nombre: 'Oct'),
  Mes(numero: 11, nombre: 'Nov'),
  Mes(numero: 12, nombre: 'Dic'),
];

int cantidadUltimosMeses = 6;

class MonthYearPicker extends StatefulWidget {
  const MonthYearPicker({
    super.key,
    required this.onChange,
    this.selectedMonth,
    this.selectedYear,
  });

  final Function(int month, int year) onChange;
  final int? selectedMonth;
  final int? selectedYear;

  @override
  State<MonthYearPicker> createState() => _MonthYearPickerState();
}

class _MonthYearPickerState extends State<MonthYearPicker> {
  final currentMonth = DateTime.now().month;
  final currentYear = DateTime.now().year;

  int selectedMonth = DateTime.now().month;
  int selectedYear = DateTime.now().year;

  List<Mes> meses = mesesInfo;

  final cantYears = DateTime.now().month >= cantidadUltimosMeses ? 1 : 2;

  changeSelectedMonth(int value) {
    setState(() {
      selectedMonth = value;
    });
  }

  changeSelectedYear(int value) {
    setState(() {
      selectedYear = value;
    });

    actualizarMeses();

    changeSelectedMonth(meses[meses.length - 1].numero);

    if (selectedYear < currentYear) {
      monthScrollController.jumpToItem(selectedMonth - (12 - meses.length) - 1);
    } else {
      monthScrollController.jumpToItem(selectedMonth - 1);
    }
  }

  actualizarMeses() {
    setState(() {
      if (selectedYear < currentYear) {
        meses = mesesInfo.sublist(6 + currentMonth);
      } else {
        if (currentMonth >= cantidadUltimosMeses) {
          meses = mesesInfo.sublist(
              currentMonth - cantidadUltimosMeses, currentMonth);
        } else {
          meses = mesesInfo.sublist(0, currentMonth);
        }
      }
    });
  }

  FixedExtentScrollController monthScrollController =
      FixedExtentScrollController();

  FixedExtentScrollController yearScrollController =
      FixedExtentScrollController();

  @override
  void initState() {
    super.initState();

    changeSelectedYear(widget.selectedYear ?? currentYear);
    if (widget.selectedMonth != null) {
      setState(() {
        selectedMonth = widget.selectedMonth!;
      });
    }

    yearScrollController = FixedExtentScrollController(
        initialItem: selectedYear - currentYear + (cantYears - 1));
    scrollMonth();
  }

  scrollMonth() {
    // Establece el índice inicial del mes
    if (selectedYear < currentYear) {
      monthScrollController = FixedExtentScrollController(
          initialItem: selectedMonth - (12 - meses.length) - 1);
    } else {
      if (currentMonth > cantidadUltimosMeses) {
        monthScrollController = FixedExtentScrollController(
            initialItem:
                selectedMonth - (currentMonth - cantidadUltimosMeses) - 1);
      } else {
        monthScrollController =
            FixedExtentScrollController(initialItem: selectedMonth - 1);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
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
                            changeSelectedMonth(meses[value].numero);
                          },
                          controller: monthScrollController,
                          itemExtent: 40,
                          diameterRatio: 1.2,
                          perspective: 0.005,
                          childDelegate: ListWheelChildBuilderDelegate(
                            childCount: meses.length,
                            builder: (context, index) {
                              return Center(
                                child: Text(
                                  meses[index].nombre,
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
                            changeSelectedYear(
                                DateTime.now().year + value - (cantYears - 1));
                          },
                          controller: yearScrollController,
                          itemExtent: 40,
                          diameterRatio: 1.2,
                          perspective: 0.005,
                          childDelegate: ListWheelChildBuilderDelegate(
                            childCount: cantYears,
                            builder: (context, index) {
                              return Center(
                                child: Text(
                                  (DateTime.now().year +
                                          index -
                                          (cantYears - 1))
                                      .toString(),
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
                    widget.onChange(selectedMonth, selectedYear);
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
  }
}
