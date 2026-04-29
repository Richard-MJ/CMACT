namespace AutorizadorCanales.Domain.Entidades;

/// <summary>
/// Tipos de aplicación que son registrados para acceder a la API Restful de TakanaCore.
/// </summary>
public enum TiposAplicacion
{
    /// <summary>
    /// Aplicaciones basadas en JavaScript/HTML.
    /// </summary>
    JavaScript = 0,

    /// <summary>
    /// Aplicaciones compiladas.
    /// </summary>
    NativoConfidencial = 1
}

public enum TipoConversionDecimal : byte
{
    DosUltimosDigitosDecimal,
    CuatroUltimosDigitosDecimal
}

/// <summary>
/// Enum de orientación de relleno a considerar en la conversión de decimal a string
/// </summary>
public enum OrientacionRellenoDecimal : byte
{
    LadoIzquierdo,
    LadoDerecho
}

public enum FormatoFecha : byte
{
    MMDD,
    HHMMSS
}