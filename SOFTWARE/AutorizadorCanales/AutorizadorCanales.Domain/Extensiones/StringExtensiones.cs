namespace AutorizadorCanales.Domain.Extensiones;

/// <summary>
/// Extensiones de tipos string
/// </summary>
public static class StringExtensiones
{
    /// <summary>
    /// Agrega carácteres al final en un string
    /// </summary>
    /// <param name="cadena">Texto</param>
    /// <param name="caracter">Caracteres</param>
    /// <param name="tamanioTotal">Tamaño total</param>
    /// <returns></returns>
    public static string AgregarCaracteresAlFinal(this string cadena, char caracter, int tamanioTotal)
    {
        int diferenciaTamaño = tamanioTotal - cadena.Length;

        if (diferenciaTamaño == 0)
        {
            return cadena;
        }
        else if (diferenciaTamaño < 0)
        {
            return cadena.Substring(0, tamanioTotal);
        }

        return cadena.PadRight(tamanioTotal, caracter);
    }

    /// <summary>
    /// Agrega caracteres al inicio
    /// </summary>
    /// <param name="cadena">Texto</param>
    /// <param name="caracter">Caracteres</param>
    /// <param name="tamanioTotal">Tamaño total</param>
    /// <returns></returns>
    public static string AgregarCaracteresAlInicio(this string cadena, char caracter, int tamanioTotal)
    {
        int diferenciaTamaño = tamanioTotal - cadena.Length;

        if (diferenciaTamaño == 0)
        {
            return cadena;
        }
        else if (diferenciaTamaño < 0)
        {
            return cadena.Substring(0, tamanioTotal);
        }

        return cadena.PadLeft(tamanioTotal, caracter);
    }
}
