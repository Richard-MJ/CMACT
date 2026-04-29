namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Clase que representa la entidad de las operaciones para canales electronicos
/// </summary>
public class TipoOperacionCanalElectronico
{
    /// <summary>
    /// Id del tipo de operacion del canal electronico
    /// </summary>
    public int IdTipoOperacionCanalElectronico { get; private set; }
    /// <summary>
    /// Descripcion de la operacion
    /// </summary>
    public string? DescripcionOperacion { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public bool IndicadorEstado { get; private set; }
    /// <summary>
    /// Codigo de usuario de registro
    /// </summary>
    public string? CodigoUsuarioRegistro { get; private set; } = null!;
    /// <summary>
    /// Fecha de registo
    /// </summary>
    public DateTime FechaRegistro { get; private set; }

    /// <summary>
    /// Validar si es tipo de operacion login
    /// </summary>
    public bool EsTipoOperacionLogin
        => IdTipoOperacionCanalElectronico == (int)ModeloTipoOperacionCanalElectronico.Login;

    /// <summary>
    /// Validar si es tipo de operacion afiliacion
    /// </summary>
    public bool EsTipoOperacionAfiliacion
        => IdTipoOperacionCanalElectronico == (int)ModeloTipoOperacionCanalElectronico.Afiliacion;
}

/// <summary>
/// Modelo del tipo de operacion del canal electronico
/// </summary>
public enum ModeloTipoOperacionCanalElectronico : int
{
    Login = 1,
    Afiliacion = 2
}
