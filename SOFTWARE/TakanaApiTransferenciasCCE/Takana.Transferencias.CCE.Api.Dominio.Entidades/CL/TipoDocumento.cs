using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio de un Tipo de documento
/// </summary>
public class TipoDocumento
{
    #region Constantes
    /// <summary>
    /// Numero identificador de PersonaNatural
    /// </summary>
    public const string PersonaNatural = "N";
    /// <summary>
    /// Numero identificador de PersonaJuridica
    /// </summary>
    public const string PersonaJuridica = "J";
    #endregion

    #region Constantes Enum
    /// <summary>
    /// Longitudes segun el tipo de documento
    /// </summary>
    public enum LongitudDocumento
    {
        LE = 12,
        DNI = 8,
        LM = 12,
        Pasaporte = 8,
        CarnetExtranjeria = 9,
        RUC = 11
    }

    /// <summary>
    /// Tipo de documento segun la CCE
    /// </summary>
    public enum TipoDocumentoCCE
    {
        LE = 1,
        DNI = 2,
        LM = 3,
        Pasaporte = 4,
        CarnetExtranjeria = 5,
        RUC = 6
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Representa el código del tipo de documento
    /// </summary>
    public string CodigoTipoDocumento { get; private set; }

    /// <summary>
    /// Representa la descripción del tipo de documento
    /// </summary>
    public string DescripcionTipoDocumento { get; private set; }

    /// <summary>
    /// ID del tipo de documento definido para CCE.
    /// </summary>
    public string? CodigoTipoDocumentoInmediataCce { get; private set; }
    /// <summary>
    /// Indicador de la prioridad en caso de ser una persona natural.
    /// </summary>
    public int IndicadorPrioridadPersonaNatural { get; private set; }
    /// <summary>
    /// Indicador de la prioridad en caso de ser persona juridica
    /// </summary>
    public int IndicadorPrioridadPersonaJuridica { get; private set; }
    /// <summary>
    /// Indicador de la prioridad en caso de ser persona juridica
    /// </summary>
    public string? IndicadorPersonaNatural { get; private set; }    
    /// <summary>
    /// Representa al tipo de persona al que pertenece este tipo de documento
    /// </summary>
    public string IndicadorPersona { get; private set; }
    /// <summary>
    /// Indicador si aplica para la CCE
    /// </summary>
    public int IndicadorAplicaCCE { get; private set; }
    /// <summary>
    /// Especifica la longitud del documento
    /// </summary>
    public int LongitudDocumentoCCE { get; private set; }
    #endregion
}
