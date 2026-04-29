using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Documento del Cliente
/// </summary>
public class DocumentoCliente : Empresa
{
    /// <summary>
    /// Código del cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Código del tipo de documento
    /// </summary>
    public string CodigoTipoDocumento { get; private set; }
    /// <summary>
    /// Número de documento
    /// </summary>
    public string NumeroDocumento { get; private set; }
    /// <summary>
    /// Fecha de vencimiento del documento
    /// </summary>
    public DateTime? FechaVencimiento { get; private set; }

    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string? IndicadorEstado { get; private set; }
    /// <summary>
    /// Entidad tipo de documento
    /// </summary>
    public virtual TipoDocumento TipoDocumento { get; private set; }
    /// <summary>
    /// Entidad cliente
    /// </summary>
    public virtual Cliente Cliente { get; private set; }

    /// <summary>
    /// Valida si es DNI o no
    /// </summary>
    public bool EsDNI =>
        CodigoTipoDocumento == ((int)TipoDocumentoEnum.DNI).ToString();

    /// <summary>
    /// Valida si es RUC o no
    /// </summary>
    public bool EsRUC =>
        CodigoTipoDocumento == ((int)TipoDocumentoEnum.RUC).ToString();
}
