namespace Takana.Transferencias.CCE.Api.Common;
/// <summary>
/// Clase que refleja el DTO de TAKANA API CORE en el cliente web api
/// </summary>
public class RespuestaHSMDTO
{
    #region Constantes
    /// <summary>
    /// Codigo de Error de HSM
    /// </summary>
    public const string CodigoErrorHSM = "0";
    #endregion

    /// <summary>
    /// Código de error de la operación
    /// </summary>
    public string Codigo { get; set; }

    /// <summary>
    /// Mensaje de error de la operación
    /// </summary>
    public string Mensaje { get; set; }

    /// <summary>
    /// Datos de respuesta de la operación (usualmente esta propiedad
    /// tiene algún valor cuando la operación ha sido exitosa)
    /// </summary>
    public PinPropiedades Datos { get; set; }
}

public record class PinPropiedades
{
    /// <summary>
    /// Resultado de la operacion del HSM
    /// </summary>
    public string Resultado { get; set; }
    /// <summary>
    /// Mensaje de la operacion del HSM
    /// </summary>
    public string Mensaje { get; set; }
    /// <summary>
    /// Ip del servidor
    /// </summary>
    public string HostName { get; set; }
}
