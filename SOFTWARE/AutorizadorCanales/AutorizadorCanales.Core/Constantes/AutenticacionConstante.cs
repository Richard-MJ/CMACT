namespace AutorizadorCanales.Core.Constantes;

/// <summary>
/// Clase estática que contiene los tipos de autenticación y token
/// </summary>
public static class AutenticacionConstante
{
    /// <summary>
    /// Tipo de token de accesso bearer
    /// </summary>
    public const string TIPO_BEARER = "bearer";
    /// <summary>
    /// Tipo de autenticación password
    /// </summary>
    public const string TIPO_PASSWORD = "password";
    /// <summary>
    /// Tipo de autenticación refresh
    /// </summary>
    public const string TIPO_REFRESH = "refresh";
}
