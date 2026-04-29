using PagareElectronico.Application.Exceptions;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PagareElectronico.Aplicacion.Helper
{
    public static class UtilsHelper
    {
        private static readonly Regex RegexAlfanumericoGuion = new(@"^[A-Za-z0-9-]+$", RegexOptions.Compiled);
        private static readonly Regex RegexEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        /// <summary>
        /// Valida que una cadena no exceda la longitud máxima.
        /// </summary>
        public static void ValidarLongitudMaxima(string valor, int longitudMaxima, string codigo, string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(valor) && valor.Trim().Length > longitudMaxima)
                throw new ValidationException(codigo, mensaje);
        }

        /// <summary>
        /// Valida que una cadena solo contenga letras, números y guion.
        /// </summary>
        public static void ValidarAlfanumericoConGuion(string valor, string codigo, string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(valor) && !RegexAlfanumericoGuion.IsMatch(valor.Trim()))
                throw new ValidationException(codigo, mensaje);
        }

        /// <summary>
        /// Valida el formato del correo.
        /// </summary>
        public static void ValidarCorreo(string correo, string codigo, string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(correo) && !RegexEmail.IsMatch(correo.Trim()))
                throw new ValidationException(codigo, mensaje);
        }

        /// <summary>
        /// Valida si una cadena es Base64 válida.
        /// </summary>
        public static bool EsBase64Valido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return false;

            Span<byte> buffer = new byte[valor.Length];
            return Convert.TryFromBase64String(valor, buffer, out _);
        }

        /// <summary>
        /// Valida precisión decimal.
        /// </summary>
        public static bool TienePrecision(decimal valor, int precisionTotal, int escala)
        {
            valor = Math.Abs(valor);
            var texto = valor.ToString(System.Globalization.CultureInfo.InvariantCulture);

            var partes = texto.Split('.');
            var enteros = partes[0].TrimStart('0');
            var decimales = partes.Length > 1 ? partes[1] : string.Empty;

            var longitudEnteros = string.IsNullOrEmpty(enteros) ? 1 : enteros.Length;

            return (longitudEnteros + decimales.Length) <= precisionTotal && decimales.Length <= escala;
        }

        /// <summary>
        /// Determina si el documento parece un RUC.
        /// </summary>
        public static bool EsRuc(string numeroDocumento)
        {
            return !string.IsNullOrWhiteSpace(numeroDocumento) &&
                   Regex.IsMatch(numeroDocumento.Trim(), @"^\d{11}$");
        }

        /// <summary>
        /// Metodo para formatear el Json
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string FormatearJsonSiAplica(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return string.Empty;

            try
            {
                using var documento = JsonDocument.Parse(body);
                return JsonSerializer.Serialize(documento.RootElement, new JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return body;
            }
        }
    }
}
