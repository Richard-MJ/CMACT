namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Clase que representa a la entidad de dominio Tipo de Documento del Cliente
/// </summary>
public class TipoDocumento : EntidadEmpresa
{
    #region Propiedades
    /// <summary>
    /// Representa el código del tipo de documento
    /// </summary>
    public string CodigoTipoDocumento { get; private set; } = null!;
    /// <summary>
    /// Representa la descripción del tipo de documento
    /// </summary>
    public string DescripcionTipoDocumento { get; private set; } = null!;
    /// <summary>
    /// Mascara del tipo de documento
    /// </summary>
    public string Mascara { get; private set; } = null!;
    /// <summary>
    /// Representa la prioridad del tipo de documento
    /// </summary>
    public byte IndicadorPrioridad { get; private set; }
    /// <summary>
    /// Representa el uso en operaciones de lavado
    /// </summary>
    public string IndicadorUsoEnLavado { get; private set; } = null!;
    /// <summary>
    /// Representa al tipo de persona al que pertenece este tipo de documento
    /// </summary>
    public string IndicadorPersona { get; private set; } = null!;
    /// <summary>
    /// ID del tipo de documento definido para CCE.
    /// </summary>
    public string? CodigoTipoDocumentoCce { get; private set; } = null!;
    /// <summary>
    /// Indicador de la prioridad de un tipo de documento según CCE.
    /// </summary>
    public byte IndicadorPrioridadCce { get; private set; }
    /// <summary>
    /// Indicador de la prioridad en caso de ser una persona natural.
    /// </summary>
    public int? IndicadorPrioridadPersonaNatural { get; private set; }
    /// <summary>
    /// Indicador de la prioridad en caso de ser persona juridica
    /// </summary>
    public int? IndicadorPrioridadPersonaJuridica { get; private set; }
    /// <summary>
    /// Indicado del tipo de documento para Unibanca
    /// </summary>
    public int? CodigoTipoUnibanca { get; private set; }
    /// <summary>
    /// Indicado del tipo de documento de identidad según el Ministerio de Trabajo
    /// </summary>
    public string? CodigoTipoMinisterioTrabajo { get; private set; } = null!;
    /// <summary>
    /// Indicador para mostrar en la busqueda de cliente.
    /// </summary>
    public bool? IndicadorMostrarBusquedaCliente { get; private set; }
    /// <summary>
    /// Indicador para mostrar tipos de documento en emisión de giros.
    /// </summary>
    public string? IndicadorEmisionGiros { get; private set; } = null!;
    /// <summary>
    /// Indicador de validacion para tipos de documentos permitidos para HB
    /// </summary>
    public bool IndicadorHomeBankingAppCanales { get; private set; }
    /// <summary>
    /// Codigo equivalente para la camara de compensacion
    /// </summary>
    public string CodigoTipoDocumentoEquivalenteCamara { get; private set; } = null!;
    /// <summary>
    /// Indicador si es Persona Natural
    /// </summary>
    public string? IndicadorPersonaNatural { get; private set; } = null!;
    /// <summary>
    /// Longitud equivalente para la camara de compensacion
    /// </summary>
    public int LongitudTipoDocumentoEquivalenteCamara { get; private set; }
    #endregion

    #region Constantes
    public const string DNI = "1";
    public const string SBS = "11";
    public const string CARNETEXTRANJERIA = "2";
    public const string PASAPORTE = "5";
    public const string RUC = "12";
    #endregion

    #region Calculadas
    public bool EsTipoDocumentoIdentidad => CodigoTipoDocumento == DNI;
    #endregion
}
