namespace AutorizadorCanales.Domain.Entidades.CF;

public class Agencia : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Código de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; } = null!;
    /// <summary>
    /// Nombre de agencia
    /// </summary>
    public string NombreAgencia { get; private set; } = null!;
    /// <summary>
    /// Indicador de registro activo.
    /// </summary>
    public string? Activo { get; private set; } = null!;
    /// <summary>
    /// Dirección de la agencia.
    /// </summary>
    public string Direccion { get; private set; } = null!;
    /// <summary>
    /// Nombre de la ciudad de la agencia.
    /// </summary>
    public string? NombreCiudad { get; private set; } = null!;
    /// <summary>
    /// Ubigeo de la agencia.
    /// </summary>
    public string CodigoUbigeo { get; private set; } = null!;
    /// <summary>
    /// Indicador del estado de la agencia.
    /// </summary>
    public string Estado { get; private set; } = null!;
    #endregion

    #region Contantes
    public const string PRINCIPAL = "01";
    #endregion
}
