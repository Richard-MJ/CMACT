namespace AutorizadorCanales.Api.Helpers;

public static class IpHelper
{
    /// <summary>
    /// Obtiene IP
    /// </summary>
    /// <param name="context">Contexto http</param>
    /// <returns></returns>
    public static string ObtenerIp(HttpContext context)
    {
        var forwardedHeader = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedHeader))
        {
            return forwardedHeader.Split(',', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()!.Trim();
        }

        var connection = context.Connection;
        if (connection.RemoteIpAddress != null)
        {
            return connection.RemoteIpAddress.ToString();
        }

        return string.Empty;
    }
}
