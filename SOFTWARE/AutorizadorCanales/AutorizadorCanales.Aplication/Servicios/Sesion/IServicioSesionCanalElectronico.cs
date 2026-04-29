using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Aplication.Servicios.Sesion;

public interface IServicioSesionCanalElectronico
{
    /// <summary>
    /// Crea la sesión
    /// </summary>
    /// <param name="esDispositivoNuevo">Identificador si es un dispositivo nuevo</param>
    /// <param name="dispositivoCanalElectronico">Entidad de dispositivo canal electrónico</param>
    /// <param name="tokenGuid">Identificador de token de sesión</param>
    /// <returns></returns>
    Task CrearSesionCanalElectronico(
        bool esDispositivoNuevo,
        DispositivoCanalElectronico? dispositivoCanalElectronico,
        string tokenGuid);
}