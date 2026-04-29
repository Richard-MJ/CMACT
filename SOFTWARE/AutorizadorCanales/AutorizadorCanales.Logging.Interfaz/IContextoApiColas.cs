namespace AutorizadorCanales.Logging.Interfaz;

/// <summary>
/// Información del contexto a nivel del API.
/// </summary>
public interface IContextoApiColas
{
    /// <summary>
    /// ID con el que se identifica un request que llega al API.
    /// </summary>
    string IdSesion { get; }
    /// <summary>
    /// ID generado en el momento de la autenticación que llega en el token de autorización.
    /// </summary>
    string IdLogin { get; }
    /// <summary>
    /// ID de la audiencia en la cual se autentico el usuario, llega en el token de autorización.
    /// </summary>
    string IdAudiencia { get; }
    /// <summary>
    /// ID del usuario que se autentico, puede ser la tarjeta en el caso de clientes y código de
    /// usuario en el caso de usuario internos.
    /// </summary>
    string IdUsuarioAutenticado { get; }
    /// <summary>
    /// ID del terminal desde el cual se autentico el usuario.
    /// </summary>
    string IdTerminalLogin { get; }
    /// <summary>
    /// ID del canal en el cual se autentico el usuario, guarda relación con IdAudiencia.
    /// </summary>
    string IdCanalOrigen { get; }
    /// <summary>
    /// Código de usuario que debe figurar como el ejecutor de las operaciones, en el caso de
    /// usuarios internos debe ser igual a IdUsuarioAutenticado.
    /// </summary>
    string CodigoUsuario { get; }
    /// <summary>
    /// Código de la agencia desde la que se autentico el usuario, en el caso de usuario internos
    /// debe ser la agencia en la cual esta activo el usuario al momento de autenticarse.
    /// </summary>
    string CodigoAgencia { get; }
    /// <summary>
    /// Indicador del sub canal
    /// </summary>
    byte IndicadorSubCanal { get; }
    /// <summary>
    /// Token para operaciones
    /// </summary>
    string Token { get; }

    void ActualizarSesion(string idLogin, string idAudiencia, string idUsuarioAutenticado,
        string idTerminalLogin, string idCanalOrigen, string codigoUsuario, string codigoAgencia,
        byte indicadorSubCanal);
}
