using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.SG;

/// <summary>
/// Entidad token refresco
/// </summary>
public class TokenRefresco
{
    #region Propiedades

    /// <summary>
    /// Id token de refresco
    /// </summary>
    public long Id { get; private set; }
    /// <summary>
    /// Id secreto
    /// </summary>
    public string IdSecreto { get; private set; } = null!;
    /// <summary>
    /// Id de sistema cliente
    /// </summary>
    public string IdSistemaCliente { get; private set; } = null!;
    /// <summary>
    /// Id de cliente api
    /// </summary>
    public int IdClienteApi { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Fecha de emisión
    /// </summary>
    public DateTime FechaEmision { get; private set; }
    /// <summary>
    /// Fecha de expiración
    /// </summary>
    public DateTime FechaExpiracion { get; private set; }
    /// <summary>
    /// Ticket protegido
    /// </summary>
    public string TicketProtegido { get; private set; } = null!;
    /// <summary>
    /// ID del tipo de autenticación que se realiza en el autorizador
    /// (0: Login, 1: Refresh).
    /// </summary>
    public byte? IdTipoAutenticacion { get; private set; }
    /// <summary>
    /// ID del dispositivo donde se realiza el proceso de login o refresh.
    /// </summary>
    public string? IdDispositivoAutenticacion { get; private set; } = null!;
    /// <summary>
    /// Id visual
    /// </summary>    
    public string IdVisual { get; private set; } = null!;
    /// <summary>
    /// Entidad sistema cliente
    /// </summary>
    public virtual Audiencia Audiencia { get; private set; } = null!;
    /// <summary>
    /// Entidad Cliente Api
    /// </summary>
    public virtual ClienteApi ClienteApi { get; private set; } = null!;

    #endregion

    #region Constantes
    public const byte TIPO_AUTENTICACION_PASSWORD = 0;
    public const byte TIPO_AUTENTICACION_REFRESH = 1;
    #endregion

    public static TokenRefresco Crear(
       string idSecreto,
       string idSistemaCliente,
       int idClienteApi,
       string indicadorEstado,
       DateTime fechaEmision,
       DateTime fechaExpiracion,
       string ticketProtegido,
       byte idTipoAutenticacion,
       string idDispositivoAutenticacion,
       string idVisual)
    {
        return new TokenRefresco
        {
            IdSecreto = idSecreto,
            IdSistemaCliente = idSistemaCliente,
            IdClienteApi = idClienteApi,
            IndicadorEstado = indicadorEstado,
            FechaEmision = fechaEmision,
            FechaExpiracion = fechaExpiracion,
            TicketProtegido = ticketProtegido,
            IdTipoAutenticacion = idTipoAutenticacion,
            IdDispositivoAutenticacion = idDispositivoAutenticacion,
            IdVisual = idVisual
        };
    }

    public void Desactivar()
    {
        IndicadorEstado = EstadoEntidad.DESACTIVADO;
    }
}