namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion
{
    public interface IServicioAplicacionColas
    {
        /// <summary>
        /// Método que envia un correo por colas en rabbit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tipoMensaje"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task EnviarCorreoAsync<T>(string tipoMensaje, T datos) where T : class;

        /// <summary>
        /// Método que envia un sms por colas en rabbit
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tipoMensaje"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task EnviarSmsGeneralAsync<T>(string tipoMensaje, T datos) where T : class;

        /// <summary>
        /// Método que envía a la cola de notificacionas para la bitacorización
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task EnviarNotificacionBitacora<T>(T datos) where T : class;
        /// <summary>
        /// Metodo de envio de notificaciones de unibanca
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tipoMensaje"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        Task EnviarNotificacionAntifraude<T>(string tipoMensaje, T datos) where T : class;
    }
}
