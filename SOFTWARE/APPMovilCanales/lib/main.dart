import 'package:caja_tacna_app/config/router/app_router.dart';
import 'package:caja_tacna_app/config/theme/app_theme.dart';
import 'package:caja_tacna_app/constants/environment.dart';
import 'package:caja_tacna_app/features/shared/widgets/inactivity_detector.dart';
import 'package:caja_tacna_app/features/shared/widgets/services.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_native_splash/flutter_native_splash.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'dart:io';
import 'package:intl/date_symbol_data_local.dart';

void main() async {
  final widgetsBinding = WidgetsFlutterBinding.ensureInitialized();
  FlutterNativeSplash.preserve(widgetsBinding: widgetsBinding);

  // Solo para desarrollo
  HttpOverrides.global = MyHttpOverrides();

  await Future.wait([
    Environment.initEnvironment(),
    initializeDateFormatting(),
    SystemChrome.setPreferredOrientations([
      DeviceOrientation.portraitUp,
    ]),
  ]);

  runApp(const ProviderScope(child: MainApp()));
}

class MainApp extends ConsumerWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final appRouter = ref.watch(appRouterProvider);

    return MaterialApp.router(
      title: 'Caja Tacna App',
      routerConfig: appRouter,
      debugShowCheckedModeBanner: false,
      builder: (context, child) {
        return Services(
          child: InactivityDetector(child: child!),
        );
      },
      theme: AppTheme().getTheme(),
    );
  }
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}
