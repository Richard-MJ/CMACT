import 'package:caja_tacna_app/features/sesion_canal_electronico/models/dispositivo_seguro.dart';

class RemoverDispositivoSeguroResponse {
  final List<DispositivoSeguro> dispositivos;
  final String numeroTarjeta;
  final String nombreCliente;
  final DateTime fechaOperacion;
  final String correoElectronicoRemitente;
  final String correoElectronicoDestinatario;

  RemoverDispositivoSeguroResponse({
    required this.dispositivos,
    required this.numeroTarjeta,
    required this.nombreCliente,
    required this.fechaOperacion,
    required this.correoElectronicoRemitente,
    required this.correoElectronicoDestinatario,
  });

  factory RemoverDispositivoSeguroResponse.fromJson(
          Map<String, dynamic> json) =>
      RemoverDispositivoSeguroResponse(
        dispositivos: List<DispositivoSeguro>.from(
            json["SesionesCanaleElectronico"]
                .map((x) => DispositivoSeguro.fromJson(x))),
        numeroTarjeta: json["NumeroTarjeta"],
        nombreCliente: json["NombreCliente"],
        fechaOperacion: DateTime.parse(json["FechaOperacion"]),
        correoElectronicoRemitente: json["CorreoElectronicoRemitente"],
        correoElectronicoDestinatario: json["CorreoElectronicoDestinatario"],
      );

  Map<String, dynamic> toJson() => {
        "SesionesCanaleElectronico":
            List<dynamic>.from(dispositivos.map((x) => x.toJson())),
        "NumeroTarjeta": numeroTarjeta,
        "NombreCliente": nombreCliente,
        "FechaOperacion": fechaOperacion.toIso8601String(),
        "CorreoElectronicoRemitente": correoElectronicoRemitente,
        "CorreoElectronicoDestinatario": correoElectronicoDestinatario,
      };
}
