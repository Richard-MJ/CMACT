using System.Security.Claims;

namespace AutorizadorCanales.Aplication.Features.Common;

public static class Utilitarios
{
    private static readonly DateTime _fechaDefault = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Obtiene los roles para canales electrónicos
    /// </summary>
    /// <returns>Lista de role</returns>
    public static string[] ObtenerRolesFinales()
    {
        return new[] { "usuarioFinal", "OPAUTH" };
    }

    /// <summary>
    /// Verifica si la fecha de cambio de clave es mayor a la fecha de sistema / emisión token
    /// </summary>
    /// <param name="fechaCambioClave">Fecha del camio de clave</param>
    /// <param name="fechaEmision">Fecha de emisión del token</param>
    /// <param name="fechaSistema">Fecha del sistena</param>
    /// <returns>Booleano</returns>
    public static bool FechaCambioClaveMayorAFechaEmisionTokenRefresco(DateTime? fechaCambioClave, DateTime fechaEmision, DateTime fechaSistema)
    {
        return (fechaCambioClave ?? fechaSistema) > (fechaCambioClave == null ? fechaSistema : fechaEmision);
    }

    /// <summary>
    /// Convierte uan fecha y hora en formato normal a formato UNIX.
    /// </summary>
    /// <param name="fechaUtc">Fecha y hora en formato normal.</param>
    /// <returns>Fecha en formato UNIX.</returns>
    public static long ConvertirATimeUnix(DateTime fechaUtc)
    {
        return (long)(fechaUtc - _fechaDefault).TotalSeconds;
    }

    /// <summary>
    /// Convierte un formato unix a fecha
    /// </summary>
    /// <param name="fechaUnix">Formato unix</param>
    /// <returns></returns>
    public static DateTime ConvertirFechaUnixAFecha(long fechaUnix)
    {
        return _fechaDefault.AddSeconds(fechaUnix);
    }

    /// <summary>
    /// Obtiene el valor de un claim por el tipo
    /// </summary>
    /// <param name="claims">Lista de claims</param>
    /// <param name="valor">Valor del tipo de claim</param>
    /// <returns>Valor del claim</returns>
    public static string ObtenerValorClaim(IEnumerable<Claim> claims, string valor)
    {
        return claims.FirstOrDefault(x => x.Type == valor)?.Value ?? "";
    }
}
