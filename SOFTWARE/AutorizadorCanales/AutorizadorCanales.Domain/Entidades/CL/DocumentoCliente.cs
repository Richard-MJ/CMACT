namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Documento del Cliente
/// </summary>
public class DocumentoCliente : EntidadEmpresa
{
    /// <summary>
    /// Código del cliente
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Código del tipo de documento
    /// </summary>
    public string CodigoTipoDocumento { get; private set; } = null!;
    /// <summary>
    /// Número de documento
    /// </summary>
    public string NumeroDocumento { get; private set; } = null!;
    /// <summary>
    /// Fecha de vencimiento del documento
    /// </summary>
    public DateTime? FechaVencimiento { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string? IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Entidad cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; } = null!;
}