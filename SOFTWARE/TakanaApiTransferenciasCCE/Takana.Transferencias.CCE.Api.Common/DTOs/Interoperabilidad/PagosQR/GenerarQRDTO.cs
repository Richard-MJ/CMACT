namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
public record GenerarQRDTO
{
    /// <summary>
    /// Tipo de QR a generar
    /// </summary>
    public string TipoQr {get; set;}
    /// <summary>
    /// Codigo cuenta interbancario
    /// </summary>
    public string CodigoCuentaInterbancario {get; set;}
    /// <summary>
    /// Nombre de comerciante
    /// </summary>
    public string NombreCliente {get; set;}
    /// <summary>
    /// Tipo de generacion de QR
    /// </summary>
    public string TipoGeneracionQR {get; set;}
    /// <summary>
    /// Codigo Moneda
    /// </summary>
    public string CodigoMoneda { get; set; }
}
