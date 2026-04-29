namespace AutorizadorCanales.Core.Constantes;

public static class TipoOperacionConstantes
{

}

/// <summary>
/// Clase estática que contiene los tipos de operacion de canales de login
/// </summary>
public static class TipoOperacionLogin
{
    /// <summary>
    /// Tipo operación login
    /// </summary>
    public const int LOGIN = 1;
    /// <summary>
    /// Tipo de operación afiliación
    /// </summary>
    public const int AFILIACION = 2;
    /// <summary>
    /// Tipo de operación de login biométrico
    /// </summary>
    public const int LOGIN_BIOMETRIA = 3;
    /// <summary>
    /// Tipo de operación de apertura pruductos
    /// </summary>
    public const int APERTURA_PRODUCTOS = 4;
}

/// <summary>
/// Tipo operacion de canal electrónico
/// </summary>
public static class TipoOperacionCanalElectronicoLogin
{
    /// <summary>
    /// Tipo de operación de confirmación de login
    /// </summary>
    public const int CONFIRMACION_LOGIN = 5;
}

/// <summary>
/// Clase estática que contiene los tipos de operacion
/// </summary>
public static class TipoOperacion
{
    /// <summary>
    /// Tipo de operación gestión de clave
    /// </summary>
    public const int GESTION_CLAVE = 32;
}