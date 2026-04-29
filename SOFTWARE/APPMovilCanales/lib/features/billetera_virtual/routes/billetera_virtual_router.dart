import 'package:caja_tacna_app/features/billetera_virtual/afiliacion_celular/routes/afiliacion_celular_router.dart';
import 'package:caja_tacna_app/features/billetera_virtual/transferencia_celular/routes/transferencia_celular_router.dart';
import 'package:go_router/go_router.dart';
import 'package:flutter/material.dart';

final billeteraVirtualRouter = GoRoute(
  path: '/billetera-virtual',
  routes: [
    afiliacionCelularRouter,
    transferenciaCelularRouter,
  ],
  builder: (context, state) {
    return const SizedBox.shrink();
  },
);
