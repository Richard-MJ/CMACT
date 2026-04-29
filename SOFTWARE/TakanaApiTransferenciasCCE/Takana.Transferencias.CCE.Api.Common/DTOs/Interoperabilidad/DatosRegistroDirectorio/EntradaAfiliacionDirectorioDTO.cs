using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

/// <summary>
/// Clase de datos de entrada para registro de directorio CCE
/// </summary>
public record EntradaAfiliacionDirectorioDTO
{
    /// <summary>
    /// Codigo entidad originante
    /// </summary>
    [SwaggerSchema("Codigo entidad originante")]
    public string? CodigoEntidadOriginante { get; set; }
    /// <summary>
    /// Tipo de instruccion
    /// </summary>
    [SwaggerSchema("Tipo de instruccion")]
    public string TipoInstruccion { get; set; }
    /// <summary>
    /// Codigo de cuenta interbancario
    /// </summary>
    [SwaggerSchema("Codigo de cuenta interbancario")]
    public string CodigoCuentaInterbancario { get; set; }
    /// <summary>
    /// Numero de celular
    /// </summary>
    [SwaggerSchema("Numero de celula")]
    public string NumeroCelular { get; set; }
    /// <summary>
    /// Codigo de servicio
    /// </summary>
    [SwaggerSchema("Codigo de servicio")]
    public string? TipoOperacion { get; set; }
    /// <summary>
    /// Codigo de cliente
    /// </summary>
    [SwaggerSchema("Codigo de cliente")]
    public string? CodigoCliente { get; set; }
    /// <summary>
    /// Numero de cuenta
    /// </summary>
    [SwaggerSchema("Numero de cuenta")]
    public string NumeroCuentaAfiliada { get; set; }
    /// <summary>
    /// Numero antiguo
    /// </summary>
    [SwaggerSchema("Numero antiguo")]
    public string? NumeroAntiguo { get; set; }
    /// <summary>
    /// Numero de tarjeta
    /// </summary>
    [SwaggerSchema("Numero de tarjeta")]
    public string NumeroTarjeta { get; set; }
    /// <summary>
    /// Indicador para modificar numero
    /// </summary>
    [SwaggerSchema("Indicador para modificar numero")]
    public string? IndicadorModificarNumero { get; set; }
    /// <summary>
    /// Numero de afiliacion
    /// </summary>
    [SwaggerSchema("Numero de afiliacion")]
    public int NumeroAfiliacion { get; set; }
    /// <summary>
    /// Canal de operacion
    /// </summary>
    [SwaggerSchema("Canal de operacion")]
    public string Canal { get; set; }
    /// <summary>
    /// Indicador de envio de notificaciones por operaciones enviadas
    /// </summary>
    [DefaultValue(false)]
    [SwaggerSchema("Notificar operaciones enviadas. Por defecto es false si no se envía.")]
    public bool? NotificarOperacionesEnviadas { get; set; }
    /// <summary>
    /// Indicador de envio de notificaciones por operaciones recibidas
    /// </summary>
    [DefaultValue(false)]
    [SwaggerSchema("Notificar operaciones recibidas. Por defecto es false si no se envía.")]
    public bool? NotificarOperacionesRecibidas { get; set; }
}
