using System.Globalization;
using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Common.Utilidades
{
    public static class Utilidades
    {
        /// <summary>
        /// Redondea un numero decinal con cantidad de decimales
        /// </summary>
        /// <param name="numeroDecimal">numero a redondear</param>
        /// <param name="decimales">cantidad de decimales</param>
        /// <returns>Numero redondeado</returns>
        public static decimal Redondear(this decimal numeroDecimal, int decimales)
        {
            return decimal.Round(numeroDecimal, decimales);
        }
        /// <summary>
        /// Genera un numero aleatorio
        /// </summary>
        /// <param name="min">Rango minimo</param>
        /// <param name="max">Rango maximo</param>
        /// <returns>Numero aleatorio</returns>
        public static int ObtenerNumeroAleatorio(int min, int max){
            Random r = new Random();
            return r.Next (min,max);                
        }
        /// <summary>
        /// Obtiene el importe de la comision
        /// </summary>
        /// <param name="numero"> numero de importe de la comision</param>
        /// <returns>Numero convetido al formato de la CCE</returns>
        public static decimal ObtenerImporteOperacion(this decimal numero){
            return numero / 100m;                
        }
        /// <summary>
        /// Obtiene el importe de la comision
        /// </summary>
        /// <param name="numero"> numero de importe de la comision</param>
        /// <returns>Numero convetido al formato de la CCE</returns>
        public static decimal ObtenerImporteOperacion(this long numero)
        {
            return numero / 100m;
        }
        /// <summary>
        /// Genera un numero aleatorio siempre con 2 digitos
        /// </summary>
        /// <param name="min">Rango minimo</param>
        /// <param name="max">Rango maximo</param>
        /// <returns>Retorna numero aleatoria siempre de 2 digitos</returns>
        public static string ObtenerNumeroAleatorioCCE(int min, int max)
        {
            var numero = ObtenerNumeroAleatorio(min, max);
            return numero < 10 ? General.Cero + numero : numero.ToString();
        }

        /// <summary>
        /// Obtiene el primer día del mes con la hora especificada y devuelve la fecha y hora combinadas como una cadena.
        /// </summary>
        /// <param name="fechaSistema">Fecha en formato "MM/yyyy".</param>
        /// <returns>Fecha y hora combinadas en formato "dd/MM/yyyy".</returns>
        public static string ObtenerPrimerDia(this string fechaSistema)
        {
            DateTime fechaInicio = DateTime.ParseExact(fechaSistema, "MM/yyyy", CultureInfo.InvariantCulture);
            DateTime primerDiaDelMes = new DateTime(fechaInicio.Year, fechaInicio.Month, 1);
            return primerDiaDelMes.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Método que obteine el primer dia del Mes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime ObtenerPrimerDia(this DateTime fecha)
        {
            return new DateTime(fecha.Year, fecha.Month, 1);
        }
        /// <summary>
        /// Obtiene el último día del mes con la hora especificada y devuelve la fecha y hora combinadas como una cadena.
        /// </summary>
        /// <param name="fechaSistema">Fecha en formato "MM/yyyy".</param>
        /// <param name="hora">Hora en formato "HH:mm:ss.fff".</param>
        /// <returns>Fecha y hora combinadas en formato "dd/MM/yyyy HH:mm:ss.fff".</returns>
        public static string ObtenerUltimoDia(this string fechaSistema)
        {
            DateTime fechaInicio = DateTime.ParseExact(fechaSistema, "MM/yyyy", CultureInfo.InvariantCulture);
            DateTime primerDiaDelMes = new DateTime(fechaInicio.Year, fechaInicio.Month, 1);
            DateTime ultimoDiaDelMes = primerDiaDelMes.AddMonths(1).AddDays(-1);
            return ultimoDiaDelMes.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Método que obtiene la última fecha y hora del mes 
        /// </summary>
        /// <param name="fecha">fecha a calcular</param>
        /// <returns>ultimo dia del mes con última hora del dia</returns>
        public static DateTime ObtenerUltimoDia(this DateTime fecha)
        {
            DateTime ultimaFechaMes = new DateTime(
                fecha.Year,
                fecha.Month,
                DateTime.DaysInMonth(fecha.Year, fecha.Month),
                23,
                59,
                59);
            return ultimaFechaMes;
        }

        /// <summary>
        /// Devuelve la primera palabra de una oracion
        /// </summary>
        /// <param name="input">String a formatear</param>
        /// <returns></returns>
        public static string PrimeraPalabra(this string input)
        {
            string[] palabras = input.Split(' ');
            return palabras[0];
        }
    }
}