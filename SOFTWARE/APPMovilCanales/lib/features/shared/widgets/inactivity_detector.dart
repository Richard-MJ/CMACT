import 'package:caja_tacna_app/features/shared/providers/auth_provider.dart';
import 'package:caja_tacna_app/features/shared/providers/inactivity_provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class InactivityDetector extends ConsumerStatefulWidget {
  final Widget child;

  const InactivityDetector({super.key, required this.child});

  @override
  ConsumerState<InactivityDetector> createState() => _InactivityDetectorState();
}

class _InactivityDetectorState extends ConsumerState<InactivityDetector>
    with WidgetsBindingObserver {
  int _tiempoSesionRestante = 0;
  bool _isListening = false;

  void _registrarActividadUsuario() {
    ref
        .read(inactivityProvider.notifier)
        .resetearTimer(tiempoRestante: _tiempoSesionRestante);
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
  }

  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }

  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    if (state == AppLifecycleState.resumed) {
      ref.read(inactivityProvider.notifier).checkInactivity();
    } else if (state == AppLifecycleState.paused) {
      ref.read(inactivityProvider.notifier).cancelarTimers();
    }
  }

  @override
  Widget build(BuildContext context) {
    if (!_isListening) {
      _isListening = true;

      ref.listen<AuthState>(authProvider, (prev, next) {
        _tiempoSesionRestante = next.tiempoSesionRestante;
      });
    }

    return GestureDetector(
      behavior: HitTestBehavior.translucent,
      onTap: _registrarActividadUsuario,
      onPanDown: (_) => _registrarActividadUsuario(),
      onScaleStart: (_) => _registrarActividadUsuario(),
      child: widget.child,
    );
  }
}
