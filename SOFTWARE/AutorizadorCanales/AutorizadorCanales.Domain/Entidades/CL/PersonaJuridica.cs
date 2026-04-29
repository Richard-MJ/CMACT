namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Entidad persona juridica
/// </summary>
public class PersonaJuridica : EntidadEmpresa
{
    /// <summary>
    /// Código de cliente
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Código sector
    /// </summary>
    public string CodigoSector { get; private set; } = null!;
    /// <summary>
    /// Clase sociedad
    /// </summary>
    public string ClaseSociedad { get; private set; } = null!;
    /// <summary>
    /// Código de actividad
    /// </summary>
    public string CodigoActividad { get; private set; } = null!;
    /// <summary>
    /// Código de subactividad
    /// </summary>
    public string CodigoSubactividad { get; private set; } = null!;
    /// <summary>
    /// Código de sub-sub actividad
    /// </summary>
    public string CodigoSubsubactividad { get; private set; } = null!;
    /// <summary>
    /// Id sector de actividad
    /// </summary>
    public byte IdSectorActividad { get; private set; }
    /// <summary>
    /// Razón social
    /// </summary>
    public string RazonSocial { get; private set; } = null!;
    /// <summary>
    /// Nombre comercial
    /// </summary>
    public string NombreComercial { get; private set; } = null!;
    /// <summary>
    /// Indicador de obligado
    /// </summary>
    public string IndicadorObligado { get; private set; } = null!;
    /// <summary>
    /// Entidad cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; } = null!;
    /// <summary>
    /// Nombres
    /// </summary>
    public string Nombres => string.Empty;
    /// <summary>
    /// Apellido paterno
    /// </summary>
    public string ApellidoPaterno => RazonSocial;
    /// <summary>
    /// Apellido manterno
    /// </summary>
    public string ApellidoMaterno => string.Empty;
    /// <summary>
    /// Tipo de cliente
    /// </summary>
    public string TipoCliente { get; set; } = "CL";
}
