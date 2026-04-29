import 'package:caja_tacna_app/features/home/screens/home_screen.dart';
import 'package:go_router/go_router.dart';

final homeRouter = GoRoute(
  path: '/home',
  builder: (context, state) {
    return const HomeScreen();
  },
);
