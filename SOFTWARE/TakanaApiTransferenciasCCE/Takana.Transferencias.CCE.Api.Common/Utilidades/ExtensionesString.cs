
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Takana.Transferencias.CCE.Api.Common.Utilidades
{
    public static class ExtensionesString
    {
        public enum TipoConversionDecimal : byte
        {
            DosUltimosDigitosDecimal,
            CuatroUltimosDigitosDecimal
        }
        public enum FormatoFecha : byte
        {
            MMDD,
            HHMMSS
        }

        /// <summary>
        /// Funcion para limitar el tamaño de una cadena
        /// </summary>
        /// <param name="dato">Cadena de texto</param>
        /// <param name="longitud">Número de caracteres</param>
        /// <returns>Cadena de caracteres solicitado</returns>
        public static string LimitarCaracteres(this string dato, int longitud)
        {
            if (string.IsNullOrEmpty(dato)) return string.Empty;
            return dato.Length <= longitud ? dato : dato.Substring(0, longitud);
        }

        /// <summary>
        /// Funcion que verifica si el dato es vacio
        /// </summary>
        /// <param name="dato"></param>
        /// <returns>Retorna null o el dato</returns>
        public static string? EsVacioTexto(this string dato)
        {
            return string.IsNullOrEmpty(dato) ? null : dato;
        }

        /// <summary>
        /// Función que normaliza el texto: si es nulo o vacío, devuelve string.Empty; de lo contrario, retorna el mismo texto.
        /// </summary>
        /// <param name="dato">Texto de entrada</param>
        /// <returns>string.Empty si es vacío, o el texto original</returns>
        public static string CompletarTextoAVacio(this string? dato)
        {
            return string.IsNullOrEmpty(dato) ? string.Empty : dato;
        }

        /// <summary>
        /// Método que enmascara el numero
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static string EnmascarasTexto(this string numero, int cantidadVisible)
        {
            if (string.IsNullOrEmpty(numero) || numero.Length < cantidadVisible) 
                return numero;

            var ultimos4 = numero.Substring(numero.Length - cantidadVisible);
            return new string('*', numero.Length - cantidadVisible) + ultimos4;
        }

        /// <summary>
        /// Método para dejar todo en una sola linea
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ParaUnaSolaLinea(this string? s)
        {
            var t = (s ?? string.Empty)
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");

            t = t.Replace("\n", " ");
            t = Regex.Replace(t, @"[ \t\u00A0]{2,}", " ");
            return t.Trim();
        }

        /// <summary>
        /// Método de string Trama
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="formato"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
        /// Método de string trama
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="formato"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static string ToStringTrama(this DateTime fecha, FormatoFecha formato)
        {
            if (fecha == default(DateTime))
            {
                throw new ArgumentException("fecha invalida");
            }

            switch (formato)
            {
                case FormatoFecha.HHMMSS:
                    return fecha.ToString("HHmmss");
                case FormatoFecha.MMDD:
                    return fecha.ToString("MMdd");
            }

            throw new ArgumentException("formato invalido");
        }

        /// <summary>
        /// Convertir en ultimos digitos en decimal
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="tamanio"></param>
        /// <param name="numeroDecimales"></param>
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
        /// Agregar caracteres al final
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="caracter"></param>
        /// <param name="tamanioTotal"></param>
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
        /// Agregar caracteres al inicio
        /// </summary>
        /// <param name="cadena"></param>
        /// <param name="caracter"></param>
        /// <param name="tamanioTotal"></param>
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

        /// <summary>
        /// Quita tildes/diacríticos y normaliza a ASCII básico.
        /// </summary>
        public static string QuitarDiacriticos(this string? input, bool mapEspeciales = true)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string decomposed = input.Normalize(NormalizationForm.FormD);

            var sb = new StringBuilder(capacity: decomposed.Length);

            foreach (var ch in decomposed)
            {
                var cat = CharUnicodeInfo.GetUnicodeCategory(ch);

                if (cat == UnicodeCategory.NonSpacingMark ||
                    cat == UnicodeCategory.SpacingCombiningMark ||
                    cat == UnicodeCategory.EnclosingMark ||
                    cat == UnicodeCategory.OtherSymbol)
                {
                    continue;
                }

                sb.Append(ch);
            }

            string cleaned = sb.ToString().Normalize(NormalizationForm.FormC);

            if (!mapEspeciales) return cleaned;

            return cleaned
                .Replace("ß", "ss")
                .Replace("Æ", "AE").Replace("æ", "ae")
                .Replace("Œ", "OE").Replace("œ", "oe")
                .Replace("Ð", "D").Replace("ð", "d")
                .Replace("Þ", "Th").Replace("þ", "th")
                .Replace("Ł", "L").Replace("ł", "l");
        }

        /// <summary>
        /// Centra el texto para la impresion del Voucher.
        /// </summary>
        public static string CentrarTextoVoucher(this string texto, int ancho, string lineaVacia)
        {
            if (string.IsNullOrEmpty(texto))
                return texto ?? string.Empty;

            if (lineaVacia.Length < ancho)
                lineaVacia = lineaVacia.PadRight(ancho, ' ');

            int espacios = (ancho - texto.Length);
            if (espacios <= 0)
                return texto; 

            int lado = espacios / 2;
            return lineaVacia.Substring(0, lado) + texto + lineaVacia.Substring(0, lado);
        }

        /// <summary>
        /// Metodo que enmascara el nombre
        /// </summary>
        /// <param name="nombreCompleto"></param>
        /// <returns></returns>
        public static string EnmascararNombreAlternado(this string? nombreCompleto)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                return string.Empty;

            var palabras = nombreCompleto.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var palabrasProcesadas = palabras.Select((palabra, indice) =>
            {
                if (indice % 2 == 0)
                    return palabra;

                return palabra.Length > 0
                    ? $"{palabra[0]}***"
                    : string.Empty;
            });

            return string.Join(" ", palabrasProcesadas);
        }

        /// <summary>
        /// Enmascarar producto
        /// </summary>
        /// <param name="numeroProducto"></param>
        /// <returns></returns>
        public static string? EnmascararProducto(this string? numeroProducto)
        {
            if (string.IsNullOrEmpty(numeroProducto)) return numeroProducto;

            return new string('*', numeroProducto.Length - 4) + numeroProducto.Substring(numeroProducto.Length - 4);
        }

        /// <summary>
        /// Enmascarar celular
        /// </summary>
        /// <param name="numeroProducto"></param>
        /// <returns></returns>
        public static string? EnmascararCelular(this string? numeroProducto)
        {
            if (string.IsNullOrEmpty(numeroProducto)) return numeroProducto;

            return new string('*', numeroProducto.Length - 3) + numeroProducto.Substring(numeroProducto.Length - 3);
        }
    }
}