namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

public class OrdenTransferenciaRespuestaTraducidoDTO
{   
    /// <summary>
    /// Codigo de respuesta 
    /// </summary>
    public string CodigoRespuesta { get; set; }
    /// <summary>
    /// Razon de codigo de respuesta
    /// </summary>
    public string RazonCodigoRespuesta { get; set; }
    /// <summary>
    /// Numero de seguimiento
    /// </summary>
    public string Trace { get; set; }
    /// <summary>
    /// Monto de la transaferencia
    /// </summary>
    public decimal Monto { get; set; }
    /// <summary>
    /// Indentificador de la transaccion
    /// </summary>
    public string IdentificadorTransaccion { get; set; }
    /// <summary>
    /// Codigo de cuenta cuenta interbancario CCI
    /// </summary>
    public string CodigoCuentaInterbancariaReceptor { get; set; }
    /// <summary>
    /// Numero de tarjeta de credito del cliente receptor
    /// </summary>
    public string CodigoTarjetaCreditoReceptor { get; set; }
    /// <summary>
    /// Indicaro ITF
    /// </summary>
    public string IndicadorITF { get; set; }
    /// <summary>
    /// Codigo de monea
    /// </summary>
    public string CodigoMoneda { get; set; }
}
