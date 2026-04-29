using AutorizadorCanales.Domain.Entidades;

namespace AutorizadorCanales.Domain.Extensiones;

public static class DateTimeExtensiones
{
    /// <summary>
    /// Convierte la fecha en formato para trama procesada
    /// </summary>
    /// <param name="fecha">Fecha</param>
    /// <param name="formato">Formato</param>
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
    /// Método que obtiene el numero de dias transcurridos
    /// </summary>
    /// <param name="fechaActual">Fecha actual</param>
    /// <param name="fechaAnterior">Fecha anterior</param>
    /// <returns>Numero de dias calculados</returns>
    public static int ObtenerDiasTranscurridos(this DateTime fechaActual, DateTime fechaAnterior)
    {
        TimeSpan restante = fechaActual - fechaAnterior;
        return restante.Days;
    }
}
