import 'package:caja_tacna_app/features/pago_tarjetas_credito/inmediatas/models/comision.dart';

class DetalleTransferenciaResponse {
    String codigoCuentaTransaccion;
    String codigoEntidadOriginante;
    String codigoEntidadReceptora;
    String fechaCreacionTransaccion;
    String horaCreacionTransaccion;
    String numeroReferencia;
    String trace;
    String canal;
    String codigoMoneda;
    String codigoTransferencia;
    String criterioPlaza;
    String tipoPersonaDeudor;
    String nommbreDeudor;
    String tipoDocumentoDeudor;
    String numeroIdentidadDeudor;
    String? numeroCelularDeudor;
    String codigoCuentaInterbancariaDeudor;
    String nombreReceptor;
    String? direccionReceptor;
    String? numeroTelefonoReceptor;
    String? numeroCelularReceptor;
    String? codigoCuentaInterbancariaReceptor;
    String? codigoTarjetaReceptor;
    String indicadorItf;
    String plaza;
    String tipoTransaccion;
    String instruccionId;
    String tipoDocumentoReceptor;
    String numeroIdentidadReceptor;
    String mismoTitular;
    Comision comision;
    bool esExoneradoComision;

    DetalleTransferenciaResponse({
        required this.codigoCuentaTransaccion,
        required this.codigoEntidadOriginante,
        required this.codigoEntidadReceptora,
        required this.fechaCreacionTransaccion,
        required this.horaCreacionTransaccion,
        required this.numeroReferencia,
        required this.trace,
        required this.canal,
        required this.codigoMoneda,
        required this.codigoTransferencia,
        required this.criterioPlaza,
        required this.tipoPersonaDeudor,
        required this.nommbreDeudor,
        required this.tipoDocumentoDeudor,
        required this.numeroIdentidadDeudor,
        this.numeroCelularDeudor,
        required this.codigoCuentaInterbancariaDeudor,
        required this.nombreReceptor,
        this.direccionReceptor,
        this.numeroTelefonoReceptor,
        this.numeroCelularReceptor,
        required this.codigoCuentaInterbancariaReceptor,
        this.codigoTarjetaReceptor,
        required this.indicadorItf,
        required this.plaza,
        required this.tipoTransaccion,
        required this.instruccionId,
        required this.tipoDocumentoReceptor,
        required this.numeroIdentidadReceptor,
        required this.mismoTitular,
        required this.comision,
        required this.esExoneradoComision
    });

    factory DetalleTransferenciaResponse.fromJson(Map<String, dynamic> json) =>
      DetalleTransferenciaResponse(
        codigoCuentaTransaccion: json["CodigoCuentaTransaccion"],
        codigoEntidadOriginante: json["CodigoEntidadOriginante"],
        codigoEntidadReceptora: json["CodigoEntidadReceptora"],
        fechaCreacionTransaccion: json["FechaCreacionTransaccion"],
        horaCreacionTransaccion: json["HoraCreacionTransaccion"],
        numeroReferencia: json["NumeroReferencia"],
        trace: json["Trace"],
        canal: json["Canal"],
        codigoMoneda: json["CodigoMoneda"],
        codigoTransferencia: json["CodigoTransferencia"],
        criterioPlaza: json["CriterioPlaza"],
        tipoPersonaDeudor: json["TipoPersonaDeudor"],
        nommbreDeudor: json["NommbreDeudor"],
        tipoDocumentoDeudor: json["TipoDocumentoDeudor"],
        numeroIdentidadDeudor: json["NumeroIdentidadDeudor"],
        numeroCelularDeudor: json["NumeroCelularDeudor"],
        codigoCuentaInterbancariaDeudor: json["CodigoCuentaInterbancariaDeudor"],
        nombreReceptor: json["NombreReceptor"],
        direccionReceptor: json["DireccionReceptor"],
        numeroTelefonoReceptor: json["NumeroTelefonoReceptor"],
        numeroCelularReceptor: json["NumeroCelularReceptor"],
        codigoCuentaInterbancariaReceptor: json["CodigoCuentaInterbancariaReceptor"],
        codigoTarjetaReceptor: json["CodigoTarjetaReceptor"],
        indicadorItf: json["IndicadorITF"],
        plaza: json["Plaza"],
        tipoTransaccion: json["TipoTransaccion"],
        instruccionId: json["InstruccionId"],
        tipoDocumentoReceptor: json["TipoDocumentoReceptor"],
        numeroIdentidadReceptor: json["NumeroIdentidadReceptor"],
        mismoTitular: json["MismoTitular"],
        comision: Comision.fromJson(json["Comision"]),
        esExoneradoComision: json["EsExoneradoComision"]
      );

    Map<String, dynamic> toJson() => {
      "CodigoCuentaTransaccion" : codigoCuentaTransaccion,
      "CodigoEntidadOriginante" : codigoEntidadOriginante,
      "CodigoEntidadReceptora" : codigoEntidadReceptora,
      "FechaCreacionTransaccion" : fechaCreacionTransaccion,
      "HoraCreacionTransaccion" : horaCreacionTransaccion,
      "NumeroReferencia" : numeroReferencia,
      "Trace" : trace,
      "Canal" : canal,
      "CodigoMoneda" : codigoMoneda,
      "CodigoTransferencia" : codigoTransferencia,
      "CriterioPlaza" : criterioPlaza,
      "TipoPersonaDeudor" : tipoPersonaDeudor,
      "NommbreDeudor" : nommbreDeudor,
      "TipoDocumentoDeudor" : tipoDocumentoDeudor,
      "NumeroIdentidadDeudor" : numeroIdentidadDeudor,
      "NumeroCelularDeudor" : numeroCelularDeudor,
      "CodigoCuentaInterbancariaDeudor" : codigoCuentaInterbancariaDeudor,
      "NombreReceptor" : nombreReceptor,
      "DireccionReceptor" : direccionReceptor,
      "NumeroTelefonoReceptor" : numeroTelefonoReceptor,
      "NumeroCelularReceptor" : numeroCelularReceptor,
      "CodigoCuentaInterbancariaReceptor" : codigoCuentaInterbancariaReceptor,
      "CodigoTarjetaReceptor" : codigoTarjetaReceptor,
      "IndicadorITF" : indicadorItf,
      "Plaza" : plaza,
      "TipoTransaccion" : tipoTransaccion,
      "InstruccionId" : instruccionId,
      "TipoDocumentoReceptor" : tipoDocumentoReceptor,
      "NumeroIdentidadReceptor" : numeroIdentidadReceptor,
      "MismoTitular" : mismoTitular,
      "Comision" : comision.toJson(),
      "EsExoneradoComision" : esExoneradoComision,
  };
}
