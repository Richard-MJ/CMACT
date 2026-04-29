using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{   
    /// <summary>
    /// Clase que mapea datos para echo test
    /// </summary>
    public static class EchoTestExtensiones
    {
        /// <summary>
        /// Metodo que mapea los datos echo test
        /// </summary>
        /// <param name="datosRecibidos">Datos recibidos</param>
        /// <param name="datosCalculados">Datos calculados</param>
        /// <returns>reotrna la estructura necesario</returns>
        public static EstructuraContenidoET2 ArmarDatos(
            this EchoTestDTO datosRecibidos,
            EchoTestRespuestaDTO datosCalculados)
        {
            
            return new EstructuraContenidoET2(){
                ET2 = new EchoTestRespuestaDTO
                {
                    participantCode = datosRecibidos.participantCode,
                    responseDate = datosCalculados.responseDate,
                    responseTime = datosCalculados.responseTime,
                    trace = datosRecibidos.trace,                
                    status = datosCalculados.status,
                    reasonCode = datosCalculados.reasonCode?.EsVacioTexto()
                } 
            };          
        }

        /// <summary>
        /// Metodo que maqueta los datos de salida
        /// </summary>
        /// <param name="entidad">Codigo de entidad originante</param>
        /// <param name="ObtenerNumeroSeguimiento">Numero de seguimiento</param>
        /// <returns></returns>
        public static EchoTestDTO MaquetacionDatosSalida(
            string entidad, 
            string ObtenerNumeroSeguimiento,
            string fecha,
            string hora)
        {
            return new EchoTestDTO
            {
                participantCode =entidad,
                creationDate = fecha,
                creationTime = hora,
                trace = ObtenerNumeroSeguimiento
            };
        }
    }
}