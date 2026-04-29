namespace AutorizadorCanales.Core.DTO;

/// <summary>
/// Dto de usuario logueado
/// </summary>
public class UsuarioLogueadoDto
{
    /// <summary>
    /// Apellido del usuario
    /// </summary>
    public string ApellidoUsuario { get; set; } = null!;
    /// <summary>
    /// Código de usuario
    /// </summary>
    public string CodigoUsuario { get; set; } = null!;
    /// <summary>
    /// Número de tarjeta
    /// </summary>
    public string NumeroTarjeta { get; set; } = null!;
    /// <summary>
    /// Primer nombre de usuario
    /// </summary>
    public string PrimerNombreUsuario { get; set; } = null!;
    /// <summary>
    /// Segundo nombre de usuario
    /// </summary>
    public string SegundoNombreUsuario { get; set; } = null!;
    /// <summary>
    /// Afiliación sms
    /// </summary>
    public bool AfiliacionSmsConfirmado { get; set; }
    /// <summary>
    /// Login sms
    /// </summary>
    public bool LoginSmsConfirmado { get; set; }
    /// <summary>
    /// Id de registro nuevo de dispositivo
    /// </summary>
    public string? IdRegistroDispositivoNuevo { get; set; } = null!;
    /// <summary>
    /// Id de cliente api
    /// </summary>
    public int IdClienteApi { get; set; }
    /// <summary>
    /// Indicador de dispositivo autorizado
    /// </summary>
    public bool DispositivoAutorizado =>
        AfiliacionSmsConfirmado && LoginSmsConfirmado;
}
