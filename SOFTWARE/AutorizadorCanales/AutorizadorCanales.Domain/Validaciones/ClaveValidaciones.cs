using AutorizadorCanales.Excepciones;
using System.Text.RegularExpressions;

namespace AutorizadorCanales.Domain.Validaciones;

public static class ClaveValidaciones
{
    /// <summary>
    /// Patrón para no repetir los 6 dígitos de la clave de internet
    /// </summary>
    private const string _regexPassword = @"^(?!([\d])\1{5})\d{6}$";
    /// <summary>
    /// Patrón para los caracteres no repetidos
    /// </summary>
    private const string _regexNoRepetidos = @"^(.)(?!\1+$)";
    /// <summary>
    /// Patrón para validar numeros y letras
    /// </summary>
    private const string _regexNumerosYLetras = @"^[a-zA-Z0-9\s]*$";
    /// <summary>
    /// Patrón para validar solo números
    /// </summary>
    private const string _regexSoloNumeros = "^[0-9]+$";

    public static void ValidarCreacionClave(string claveCajero, string claveInternet)
    {
        if (claveCajero.Trim().Length != 4)
            throw new ExcepcionAUsuario("06", $"Clave de cajero no válida.");

        if (claveInternet.Trim().Length != 6)
            throw new ExcepcionAUsuario("06", $"Clave de internet no válida.");

        if (!Regex.IsMatch(claveInternet, _regexSoloNumeros))
            throw new ExcepcionAUsuario("06", "Solo se pueden usar números como nueva clave.");

        if (!Regex.IsMatch(claveInternet, _regexPassword))
            throw new ExcepcionAUsuario("06", "Clave de internet inválida.");
    }
}
