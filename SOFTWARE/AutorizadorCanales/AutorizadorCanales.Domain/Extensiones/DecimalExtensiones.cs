using AutorizadorCanales.Domain.Entidades;
using System.Globalization;

namespace AutorizadorCanales.Domain.Extensiones;

/// <summary>
/// Extensiones para los tipos decimal
/// </summary>
public static class DecimalExtensiones
{
    /// <summary>
    /// Convierte el número en string para la trama procesad
    /// </summary>
    /// <param name="numero">Número a convertir</param>
    /// <param name="tipoConversion">Tipo de conversión</param>
    /// <param name="tamanio">Tamaño</param>
    /// <returns></returns>
    public static string ToStringTrama(this decimal numero, TipoConversionDecimal tipoConversion, int tamanio)
    {
        if (tipoConversion == TipoConversionDecimal.DosUltimosDigitosDecimal)
        {
            numero = Math.Round(numero, 2, MidpointRounding.AwayFromZero);
            return ConvertirAStringNUltimosDigitosDecimales(numero, tamanio, 2);
        }

        if (tipoConversion == TipoConversionDecimal.CuatroUltimosDigitosDecimal)
        {
            numero = Math.Round(numero, 4, MidpointRounding.AwayFromZero);
            return ConvertirAStringNUltimosDigitosDecimales(numero, tamanio, 4);
        }

        return string.Empty;
    }

    /// <summary>
    /// Convierte un núnero a string según tamaño
    /// </summary>
    /// <param name="numero">Valor de número</param>
    /// <param name="tamanio">Tamaño</param>
    /// <param name="numeroDecimales">Número de decimales</param>
    /// <returns></returns>
    private static string ConvertirAStringNUltimosDigitosDecimales(decimal numero, int tamanio, int numeroDecimales)
    {
        var numeroString = numero.ToString(CultureInfo.InvariantCulture);
        var valoresNumero = numeroString.Split('.');
        string parteEnteraString = string.Empty;
        string parteDecimalString = string.Empty;

        if (valoresNumero.Length > 1)
        {
            parteDecimalString = valoresNumero[1];
        }
        else
        {
            parteDecimalString = "00";
        }

        parteDecimalString = parteDecimalString.AgregarCaracteresAlFinal('0', numeroDecimales);
        parteEnteraString = valoresNumero[0];

        return (parteEnteraString + parteDecimalString).AgregarCaracteresAlInicio('0', tamanio);
    }

    /// <summary>
    /// Método que convierte un decimal a string, donde el string de salida puede contener
    /// caracteres de relleno según parámetros enviados
    /// </summary>
    /// <param name="numero">Número de referencia</param>
    /// <param name="tamanioRelleno">Tamaño de relleno</param>
    /// <param name="caracterRelleno">Caracter de relleno</param>
    /// <param name="orientacionRelleno">Orientación de relleno</param>
    /// <returns>Decimal convertido a string</returns>
    public static string ConvertirAStringConRelleno(this decimal numero, int tamanioRelleno,
        char caracterRelleno, OrientacionRellenoDecimal orientacionRelleno)
    {
        if (orientacionRelleno == OrientacionRellenoDecimal.LadoDerecho)
            return numero.ToString("N").PadRight(tamanioRelleno, caracterRelleno);
        if (orientacionRelleno == OrientacionRellenoDecimal.LadoIzquierdo)
            return numero.ToString("N").PadLeft(tamanioRelleno, caracterRelleno);
        throw new ArgumentException("Orientación de relleno no válido.");
    }
}
