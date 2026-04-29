using Hangfire.Dashboard;

namespace Takana.Transferencias.CCE.Api.Servicio;
public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    /// <summary>
    /// Método que habilita la autorizacion del servicio de HangFire
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public bool Authorize(DashboardContext context)
    {
        return true;
    }
}
