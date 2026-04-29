using AutorizadorCanales.Aplication.Common.ServicioSeguridad;
using AutorizadorCanales.Aplication.Common.ServicioSeguridad.Configuracion;
using AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;
using AutorizadorCanales.Infrastructure.Servicios.Requests;
using AutorizadorCanales.Logging.Interfaz;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioApiSeguridad : ServicioOperaciones<ServicioApiSeguridad>, IServicioApiSeguridad
{
    private readonly string _host;
    private readonly string _puerto;
    protected string _urlBase => $"https://{_host}:{_puerto}";
    private readonly IContexto _contexto;

    public ServicioApiSeguridad(
        ConfiguracionServicioSeguridad configuracionServicioSeguridad,
        IContexto contexto,
        IBitacora<ServicioApiSeguridad> bitacora) : base(bitacora)
    {
        _host = configuracionServicioSeguridad.Ip;
        _puerto = configuracionServicioSeguridad.Puerto;
        _contexto = contexto;
    }

    public async Task<RespuestaAutorizacionCanalElectronico> ValidarCodigoAutorizacionSmsLogin(AutorizacionCanalElectronico autorizacionCanalElectronico)
    {
        var ruta = "api/factorautenticacion/segundofactor/confirmacion";
        var request = new RequestPost
            (_urlBase, true, ruta, autorizacionCanalElectronico, _contexto.Token);

        return await InvocarOperacionAsync<RespuestaAutorizacionCanalElectronico>(request);
    }
}
