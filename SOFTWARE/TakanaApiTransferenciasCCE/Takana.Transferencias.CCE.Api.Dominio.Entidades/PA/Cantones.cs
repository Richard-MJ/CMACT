namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

/// <summary>
/// Clase para la entidad Cantones
/// </summary>
public class Cantones
{
    /// <summary>
    /// Código del departamento SAF
    /// </summary>
    public string CodigoDepartamento { get; private set; }
    /// <summary>
    /// Código de la provincia SAF
    /// </summary>
    public string CodigoProvincia { get; private set; }
    /// <summary>
    /// Código del distrito SAF
    /// </summary>
    public string CodigoDistrito { get; private set; }
    /// <summary>
    /// Descripción del canton
    /// </summary>
    public string DescripcionCanton { get; private set; }
    /// <summary>
    /// Código del departamento de RENIEC
    /// </summary>
    public string CodigoDepartamentoReniec { get; private set; }
    /// <summary>
    /// Código de la provincia de RENIEC
    /// </summary>
    public string CodigoProvinciaReniec { get; private set; }
    /// <summary>
    /// Código del distrito de RENIEC
    /// </summary>
    public string CodigoDistritoReniec { get; private set; }
    /// <summary>
    /// Codigo del ubigeo del distrito
    /// </summary>
    public string CodigoUbigeo { get; private set; }
}
