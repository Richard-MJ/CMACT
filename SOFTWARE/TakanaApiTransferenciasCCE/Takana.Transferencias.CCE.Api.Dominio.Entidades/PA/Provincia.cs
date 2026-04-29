namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;
/// <summary>
/// Clase que representa a la entidad de dominio de una provincia
/// </summary>
public class Provincia : Empresa
{
    /// <summary>
    /// Código del departamento de SAF
    /// </summary>
    public string CodigoDepartamento { get; private set; }
    /// <summary>
    /// Código de la provincia de SAF
    /// </summary>
    public string CodigoProvincia { get; private set; }
    /// <summary>
    /// Descripción de la provincia
    /// </summary>
    public string DescripcionProvincia { get; private set; }
    /// <summary>
    /// Código del departamento de RENIEC
    /// </summary>
    public string CodigoDepartamentoReniec { get; private set; }
    /// <summary>
    /// Código de la provincia de RENIEC
    /// </summary>
    public string CodigoProvinciaReniec { get; private set; }
}
