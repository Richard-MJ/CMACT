namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Persona Juridica
/// </summary>
public class PersonaJuridica : Empresa, IPersonaCuantia
{
    /// <summary>
    /// Codigo de cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Codigo de sector
    /// </summary>
    public string CodigoSector { get; private set; }
    /// <summary>
    /// Clase sociedad
    /// </summary>
    public string ClaseSociedad { get; private set; }
    /// <summary>
    /// Codigo de actividad
    /// </summary>
    public string CodigoActividad { get; private set; }
    /// <summary>
    /// Codigo de subactividad
    /// </summary>
    public string CodigoSubactividad { get; private set; }
    /// <summary>
    /// Codigo de sub sub actividada
    /// </summary>
    public string CodigoSubsubactividad { get; private set; }
    /// <summary>
    /// Ide serctor actividad
    /// </summary>
    public byte IdSectorActividad { get; private set; }
    /// <summary>
    /// Razon social
    /// </summary>
    public string RazonSocial { get; private set; }
    /// <summary>
    /// Nombre comercial
    /// </summary>
    public string NombreComercial { get; private set; }
    /// <summary>
    /// Indicador de obligado
    /// </summary>
    public string IndicadorObligado { get; private set; }
    /// <summary>
    /// Datos del cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; }
    /// <summary>
    /// Nombres del cliente
    /// </summary>
    public string Nombres => string.Empty;
    /// <summary>
    /// Apellidos paternos
    /// </summary>
    public string ApellidoPaterno => RazonSocial;
    /// <summary>
    /// Apellidos maternos
    /// </summary>
    public string ApellidoMaterno => string.Empty;
    /// <summary>
    /// Tipo cliente
    /// </summary>
    public string TipoCliente { get; private set; } = Cliente.EsquemaCliente;

}

