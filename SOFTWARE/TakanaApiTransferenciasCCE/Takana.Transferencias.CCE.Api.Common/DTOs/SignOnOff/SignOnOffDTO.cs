using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.SignOnOff;

public class SignOnOffDTO
{
    #region Constantes
    /// <summary>
    /// Descripción de la tarea manual sign on
    /// </summary>
    public const string TareaManualSignOn = "tarea_manual_sign_on";
    /// <summary>
    /// Descripción de la tarea manual sign off
    /// </summary>
    public const string TareaManualSignOff = "tarea_manual_sign_off";
    #endregion

    #region Propiedades
    /// <summary>
    /// Codigo de entidad originante
    /// </summary>
    [SwaggerSchema("Codigo de entidad originante")]
    public string participantCode { get; set; }
    /// <summary>
    /// Fecha de creacion
    /// </summary>
    [SwaggerSchema("Fecha de creacion")]
    public string creationDate { get; set; }
    /// <summary>
    /// Hora de creacion
    /// </summary>
    [SwaggerSchema("Hora de creacion")]
    public string creationTime { get; set; }
    /// <summary>
    /// Numero de seguimiento
    /// </summary>
    [SwaggerSchema("Numero de seguimiento")]
    public string trace { get; set; }
    /// <summary>
    /// Fecha de respuesta
    /// </summary>
    [SwaggerSchema("Fecha de respuesta")]
    public string responseDate { get; set; }
    /// <summary>
    /// Hora de respuesta
    /// </summary>
    [SwaggerSchema("Hora de respuesta")]
    public string responseTime { get; set; }
    /// <summary>
    /// Codigo de respuesta
    /// </summary>
    [SwaggerSchema("Codigo de respuesta")]
    public string status { get; set; }
    /// <summary>
    /// Razon de codigo de respuesta
    /// </summary>
    [SwaggerSchema("Razon de codigo de respuesta")]
    public string reasonCode { get; set; }
    #endregion
}
