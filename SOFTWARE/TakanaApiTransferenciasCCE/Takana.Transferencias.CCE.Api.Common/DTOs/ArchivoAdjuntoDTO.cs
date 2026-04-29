namespace Takana.Transferencias.CCE.Api.Common;
/// <summary>
/// Clase que refleja el DTO de TAKANA API CORE en el cliente web api
/// </summary>
public class ArchivoAdjuntoDTO
{
    #region Constantes
    /// <summary>
    /// Tipo de archivo en CSV
    /// </summary>
    public const string TipoArchivoCSV = "text/csv";
    /// <summary>
    /// Tipo de archivo en CSV
    /// </summary>
    public const string TipoArchivoGenerico = "application/octet-stream";
    /// <summary>
    /// Tipo de archivo en PDF
    /// </summary>
    public const string TipoArchivoPDF = "application/pdf";
    #endregion

    #region Propiedades
    /// <summary>
    /// Nombre del archivo
    /// </summary>
    public string NombreArchivo { get; set; }
    /// <summary>
    /// Archivo
    /// </summary>
    public byte[] Archivo { get; set; }
    /// <summary>
    /// Tipo formato de archivo
    /// </summary>
    public string TipoMime { get; set; }
    #endregion
}
