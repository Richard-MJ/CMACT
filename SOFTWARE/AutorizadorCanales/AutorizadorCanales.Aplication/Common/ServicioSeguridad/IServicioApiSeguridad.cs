using AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;

namespace AutorizadorCanales.Aplication.Common.ServicioSeguridad;

public interface IServicioApiSeguridad
{
    Task<RespuestaAutorizacionCanalElectronico> ValidarCodigoAutorizacionSmsLogin(AutorizacionCanalElectronico autorizacionCanalElectronico);
}
