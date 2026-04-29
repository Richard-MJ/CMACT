using AutorizadorCanales.Domain.Entidades.TJ;

namespace AutorizadorCanales.Aplication.Servicios.Afiliacion;

public interface IServicioAfiliacion
{
    /// <summary>
    /// Método que realiza la afiliación a canales electrónicos
    /// </summary>
    /// <param name="idAudiencia">Id de audiencia</param>
    /// <param name="passwordCajero">Password de cajero (4 dígitos)</param>
    /// <param name="passwordInternet">Password de internet (6 dígitos)</param>
    /// <param name="numeroDiasVencimiento">Número de días de vencimiento</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    Task AfiliarCanalesElectronicos(string idAudiencia, string passwordCajero, string passwordInternet, int numeroDiasVencimiento, Tarjeta tarjeta);
}
