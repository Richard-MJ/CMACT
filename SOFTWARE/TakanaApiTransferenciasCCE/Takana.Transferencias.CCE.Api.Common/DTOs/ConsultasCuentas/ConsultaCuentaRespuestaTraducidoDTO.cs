namespace Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
public record ConsultaCuentaRespuestaTraducidoDTO
{
    /// <summary>
    /// Codigo de entidad originante
    /// </summary> 
    public string CodigoEntidadOriginante { get; set; }
    /// <summary>
    /// Codigo de entidad receptora
    /// </summary> 
    public string CodigoEntidadReceptora { get; set; }
    /// <summary>
    /// Fecha de creacion de la transaccion
    /// </summary> 
    public string FechaCreacionTransaccion { get; set; }
    /// <summary>
    /// Hora de creacion de la transaccion
    /// </summary> 
    public string HoraCreacionTransaccion { get; set; }
    /// <summary>
    /// Identificador del terminal o dispositivo
    /// </summary> 
    public string IdentificadorTerminal { get; set; }
    /// <summary>
    /// Numero de referencia
    /// </summary> 
    public string NumeroReferencia { get; set; }
    /// <summary>
    /// Numero de Seguimiento
    /// </summary> 
    public string Trace { get; set; }
    /// <summary>
    /// Nombre del cliente originante
    /// </summary> 
    public string NombreDeudor{ get; set; }
    /// <summary>
    /// Numero de documento del cliente originante
    /// </summary> 
    public string NumeroDocumentoDeudor{ get; set; }
    /// <summary>
    /// Tipo de documentoe del cliente originante
    /// </summary> 
    public string TipoDocumentoDeudor{ get; set; }
    /// <summary>
    /// Telefono del cliete originante
    /// </summary> 
    public string TelefonoDeudor{ get; set; }
    /// <summary>
    /// Numero de celular del lciente originante
    /// </summary> 
    public string NumeroCelularDeudor { get; set; }
    /// <summary>
    /// Tipo de transaccion  de la transaferencia
    /// </summary> 
    public string TipoTransaccion { get; set; }
    /// <summary>
    /// Canal
    /// </summary> 
    public string Canal { get; set; }
    /// <summary>
    /// Numero de identificador de la transaccion
    /// </summary> 
    public string IdentificadorTransaccion { get; set; }
    /// <summary>
    /// Nombre completo de cliente Receptor
    /// </summary> 
    public string NombreCompletoReceptor { get; set; }
    /// <summary>
    /// Direccion del cliente receptor
    /// </summary> 
    public string DireccionReceptor { get; set; }
    /// <summary>
    /// Numero de documento del cliente receptor
    /// </summary> 
    public string NumeroDocuementoReceptor { get; set; }
    /// <summary>
    /// Tipo de documento del cliente receptor
    /// </summary> 
    public string? TipoDocumentoReceptor { get; set; }
    /// <summary>
    /// Telefono del cliente receptor
    /// </summary> 
    public string TelefonoReceptor { get; set; }
    /// <summary>
    /// Numero de celular del cliente receptor
    /// </summary> 
    public string NumeroCelularReceptor  { get; set; }
    /// <summary>
    /// Codigo de cuenta interbancario Receptor
    /// </summary> 
    public string CodigoCuentaInterbancariaReceptor { get; set; }
    /// <summary>
    /// Numero de tarjeta del cliente receptor
    /// </summary> 
    public string CodigoTarjetaCreditoReceptor { get; set; }
    /// <summary>
    /// Indicaro de ITF
    /// </summary> 
    public string IndicadorITF { get; set; }
    /// <summary>
    /// Codigo de moneda
    /// </summary> 
    public string CodigoMoneda { get; set; }
    /// <summary>
    /// Valor proxy
    /// </summary> 
    public string ValorProxy { get; set; }
    /// <summary>
    /// Tipo Proxy
    /// </summary> 
    public string TipoProxy { get; set; }

}
