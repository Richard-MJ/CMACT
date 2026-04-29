namespace AutorizadorCanales.Core.Constantes;

/// <summary>
/// Clase estática que contiene las constates para la entidad
/// Tramas Procesadas
/// </summary>
public static class TramaProcesadaConstante
{
    /// <summary>
    /// Limite de la id de terminal
    /// </summary>
    public const int LIMITE_ID_TERMINAL = 12;
    /// <summary>
    /// Límite del número de tarjeta
    /// </summary>
    public const int LIMITE_NUMERO_TARJETA = 16;
    /// <summary>
    /// Tipo de trama de canales de inicio de sesión
    /// </summary>
    public const string TIPO_TRAMA_INICIO_SESION = "0000";
    /// <summary>
    /// Tipo de proceso de inicio de sesión
    /// </summary>
    public const string TIPO_PROCESO_INICIO_SESION = "040000";
    /// <summary>
    /// Código bin de canales de tarjeta 
    /// </summary>
    public const string CODIGO_BIN_CANALES = "47720000";
}
