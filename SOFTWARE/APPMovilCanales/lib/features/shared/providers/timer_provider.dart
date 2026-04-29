import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final timerProvider = StateNotifierProvider<TimerNotifier, TimerState>((ref) {
  return TimerNotifier();
});

class TimerNotifier extends StateNotifier<TimerState> {
  Timer? timer;

  TimerNotifier() : super(TimerState());

  @override
  void dispose() {
    cancelTimer();
    super.dispose();
  }

  //inicia un contador dependiendo de una fecha en especifico
  //Usada en transferencias a terceros e interbancarias para el vencimiento del token
  initDateTimer({
    void Function()? onFinish,
    required DateTime? date,
    DateTime? initDate,
  }) {
    DateTime now = initDate ?? DateTime.now();

    cancelTimer();
    if (date == null) return;

    Duration difference = date.difference(now);

    state = state.copyWith(
      timeDifference: difference.inSeconds,
    );
    _verifyDateTimer(date: date, initDate: now, onFinish: onFinish);

    timer = Timer.periodic(const Duration(seconds: 1), (Timer t) {
      now = now.add(const Duration(seconds: 1));
      _verifyDateTimer(date: date, initDate: now, onFinish: onFinish);
    });
  }

  _verifyDateTimer({
    void Function()? onFinish,
    required DateTime date,
    required DateTime initDate,
  }) {
    state = state.copyWith(
      timerOn: true,
    );

    Duration difference = date.difference(initDate);

    if (difference.isNegative || difference.inSeconds <= 0) {
      // Si la hora ya pasó
      state = state.copyWith(
        timerText: '00:00',
        curentTimeDifference: 0,
        timerOn: false,
      );
      cancelTimer();
      if (onFinish != null) {
        onFinish();
      }
    } else {
      int minutesDifference = difference.inMinutes;
      int secondsDifference = difference.inSeconds % 60;

      state = state.copyWith(
        timerText:
            '${format2digits(minutesDifference)}:${format2digits(secondsDifference)}',
        curentTimeDifference: difference.inSeconds,
      );
    }
  }

  String format2digits(int value) {
    if (value < 10) {
      return '0$value';
    }

    return '$value';
  }

  cancelTimer() {
    if (timer != null) {
      timer!.cancel();
      timer = null;
      state = state.copyWith(
        timerText: "00:00",
        timerOn: false,
        timeDifference: 1,
        curentTimeDifference: 1,
      );
    }
  }
}

class TimerState {
  final String timerText;
  final bool timerOn;
  final int timeDifference;
  final int curentTimeDifference;

  TimerState({
    this.timerText = '00:00',
    this.timerOn = false,
    this.timeDifference = 1,
    this.curentTimeDifference = 1,
  });

  TimerState copyWith({
    String? timerText,
    bool? timerOn,
    int? timeDifference,
    int? curentTimeDifference,
  }) =>
      TimerState(
        timerText: timerText ?? this.timerText,
        timerOn: timerOn ?? this.timerOn,
        timeDifference: timeDifference ?? this.timeDifference,
        curentTimeDifference: curentTimeDifference ?? this.curentTimeDifference,
      );
}
