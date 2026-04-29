using Takana.Transferencias.CCE.Api.Common;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;

/// <summary>
/// Clase encargada de construir las respuestas en caso de exepciones
/// </summary>
public class RespuestasExtensiones<T> : RespuestaSalidaDTO<T>
{
        /// <summary>
        /// Metodo encargado construir las respuesta en caso de resultado POSITIVO
        /// </summary>
        /// <param name="resultado">Resultado donde obtendremos el codigo de respuesta</param>
        /// <param name="code">Codigo de resultado definido por la Entidad</param>
        /// <param name="mensajeRespuesta">Mensaje de respuesta obtenida</param>
        /// <param name="mensaje">Mensaje extra</param>
        /// <param name="datos">Resultado de datos consultados a la operadora</param>
        /// <returns>Esctructura de respuesta para canal origen</returns>
        public RespuestaSalidaDTO<T> ConstruirRespuestaDatos(
        string codigoCCe,string? mensajeRespuestaCCE,string? razonMensaje,T? datos, string? tipo = null)
        { 
            return new RespuestaSalidaDTO<T>{
                Codigo=codigoCCe,
                Razon=mensajeRespuestaCCE,
                RazonExtra=razonMensaje,
                Datos=datos,
                Tipo=tipo
            };
        }
        /// <summary>
        /// Metodo encargado construir las respuesta en caso de resultado NEGATIVO
        /// </summary>
        /// <param name="mensajeRespuesta">Mensaje de respuesta obtenida</param>
        /// <param name="code">Codigo de resultado definido por la Entidad</param>
        /// <param name="mensaje">Mensaje extra</param>
        /// <returns>Esctructura de respuesta para canal origen</returns>
        public RespuestaSalidaDTO<T> ConstruirRespuestaGeneral(
        string mensajeRespuestaCCE,string? codigo=null,string? razonMensaje=null)
        {
            return new RespuestaSalidaDTO<T>{
                Razon=mensajeRespuestaCCE,
                Codigo = codigo,
                RazonExtra =razonMensaje,
            };
        }

        
}
