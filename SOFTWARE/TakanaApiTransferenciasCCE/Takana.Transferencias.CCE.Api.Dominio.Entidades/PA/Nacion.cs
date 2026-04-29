namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

/// <summary>
/// Clase que representa a la entidad de dominio de una Nacion
/// </summary>
public class Nacion
{
    #region Constantes
    /// <summary>
    /// Codigo de identificacion de nacion Peru
    /// </summary>
    public const string Peru = "4028";
    /// <summary>
    /// Nombre de identificacion de nacion Peru
    /// </summary>
    public const string NombrePeru = "PERÚ";
    #endregion Constantes

    /// <summary>
    /// Codigo de la nacion
    /// </summary>
    public string Codigo { get; private set; }
    /// <summary>
    /// Codigo del nombre
    /// </summary>
    public string Nombre { get; private set; }
    /// <summary>
    /// Indicador de tipo de nacion
    /// </summary>
    public string IndicadorTipo { get; private set; }
    
}
