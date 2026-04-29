namespace AutorizadorCanales.Domain.Entidades.SG;

public class UsuarioRol : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; } = null!;
    /// <summary>
    /// Codigo usuario
    /// </summary>
    public string CodigoUsuario { get; private set; } = null!;
    /// <summary>
    /// Codigo rol
    /// </summary>
    public string CodigoRol { get; private set; } = null!;

    public virtual Usuario Usuario { get; private set; } = null!;
    #endregion

    #region Constantes
    /// <summary>
    /// Código de rol de administrador
    /// </summary>
    public const string CODIGO_ROL_ADMINISTRADOR = "ADMAG"; 
    /// <summary>
    /// Código de rol de jefe de agencia
    /// </summary>
    public const string CODIGO_ROL_JEFE_AGENCIA = "JEFAG";
    #endregion
}
