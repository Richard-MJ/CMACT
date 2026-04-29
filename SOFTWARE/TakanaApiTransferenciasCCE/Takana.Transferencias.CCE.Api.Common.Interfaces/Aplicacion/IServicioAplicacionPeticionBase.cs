namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionPeticionBase
    {
        /// <summary>
        /// Interfaz de tarea para enviar un request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlServicio"></param>
        /// <param name="recurso"></param>
        /// <param name="metodoHttp"></param>
        /// <param name="encabezado"></param>
        /// <param name="timeout"></param>
        /// <param name="datos"></param>
        /// <param name="esMensajeParaCCE"></param>
        /// <param name="esParaToken"></param>
        /// <param name="token"></param>
        /// <returns>Retorna la clase enviada</returns>
        Task<T> EjecutarRequestAsync<T>(string urlServicio, string recurso, HttpMethod metodoHttp,
            Dictionary<string, string> encabezado, int timeout, object? datos = null,
            bool esMensajeParaCCE = false);
    }
}
