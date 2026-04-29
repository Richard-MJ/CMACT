using Takana.Transferencias.CCE.Api.Common.SignOnOff;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
/// <summary>
/// Clase que mapea los datos de EchoTest
/// </summary>
public class SignOnOffExtensiones
{
    /// <summary>
    /// Metodo que mapea los datos para echo test
    /// </summary>
    /// <param name="datos">Datos para echo test</param>
    /// <returns>Estruvtura para echo test</returns>
    public static SignOnOffDTO MaquetacionDatosSalida(SignOnOffDTO datos)
        {
            return new SignOnOffDTO
            {
                participantCode =datos.participantCode,
                creationDate = datos.creationDate,
                creationTime = datos.creationTime,
                trace = datos.trace
            };
        }
}
