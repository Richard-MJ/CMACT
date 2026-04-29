class TipoDocumento {
    int codigoTipoDocumento;
    int codigoTipoDocumentoCCE;
    String? descripcionTipoDocumento;
    bool esTipoPersonaJuridica;
    String? codigoTipoDocumentoCceTransferenciasInmediatas;

    TipoDocumento({
        required this.codigoTipoDocumento,
        required this.codigoTipoDocumentoCCE,
        this.descripcionTipoDocumento,
        required this.esTipoPersonaJuridica,
        this.codigoTipoDocumentoCceTransferenciasInmediatas,
    });

    factory TipoDocumento.fromJson(Map<String, dynamic> json) =>
        TipoDocumento(
          codigoTipoDocumento: json["CodigoTipoDocumento"],
          codigoTipoDocumentoCCE: json["CodigoTipoDocumentoCCE"],
          descripcionTipoDocumento: json["DescripcionTipoDocumento"],
          esTipoPersonaJuridica: json["EsTipoPersonaJuridica"],
          codigoTipoDocumentoCceTransferenciasInmediatas: json["CodigoTipoDocumentoCceTransferenciasInmediatas"],
        );

    Map<String, dynamic> toJson() => {
          "CodigoTipoDocumento": codigoTipoDocumento,
          "CodigoTipoDocumentoCCE": codigoTipoDocumentoCCE,
          "DescripcionTipoDocumento": descripcionTipoDocumento,
          "EsTipoPersonaJuridica": esTipoPersonaJuridica,
          "CodigoTipoDocumentoCceTransferenciasInmediatas": codigoTipoDocumentoCceTransferenciasInmediatas
      };
}