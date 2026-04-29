using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Aplication.Servicios.Sesion;

public interface IServicioAlertaInicioSesion
{
    /// <summary>
    /// Registra alerta de inicio de sesión
    /// </summary>
    /// <param name="estado">Estado</param>
    /// <param name="idRegistroNuevoDispositivo">Id del nuevo dispositivo</param>
    /// <returns></returns>
    Task<DispositivoCanalElectronico?> RegistrarAlertaInicioSesion(string estado, string numeroTarjeta, string? idRegistroNuevoDispositivo = null, bool adjuntarDocumento = false, decimal numeroMovimiento = 0);
}
