
namespace Takana.Transferencias.CCE.Api.Loggin.Interfaz
{
    public interface IBitacora<T> where T : class
    {
        /// <summary>
        /// Bitacora muy detallada, puede generar alto volumen de información.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        void Trace(string mensaje);

        /// <summary>
        /// Bitacoriza mensaje con nivel de Trace, el mensaje puede tener parametros
        /// adicionales para sustitución.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        /// <param name="parametros">Parametros que serán incluidos en el mensaje.</param>
        void Trace(string mensaje, params object[] parametros);

        /// <summary>
        /// Información de debugging, menos detalle que un trace.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        void Debug(string mensaje);

        /// <summary>
        /// Bitacoriza mensaje con nivel Debug, el mensaje puede tener parametros adicionales
        /// para sustitución.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        /// <param name="parametros">Parametros que serán incluidos en el mensaje.</param>
        void Debug(string mensaje, params object[] parametros);

        /// <summary>
        /// Bitacoriza un evento, generando toda la información en JSON.
        /// </summary>
        /// <param name="mensaje">Mensaje del evento, corto y solo texto.</param>
        void Info(string mensaje);

        /// <summary>
        /// Bitacoriza un evento, generando toda la información en JSON.
        /// </summary>
        /// <param name="mensaje">Mensaje del evento, corto y solo texto.</param>
        /// <param name="parametros">Lista de objeto que indicar el contexto del evento.</param>
        void Info(string mensaje, params object[] parametros);

        /// <summary>
        /// Mensajes de advertencia, para indicar posibles problemas en el proceso.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        void Warn(string mensaje);

        /// <summary>
        /// Mensajes de advertencia, para indicar posibles problemas en el proceso.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        /// <param name="parametros">Información del contexto del evento, datos que son importantes para
        /// identificar el porque del evento.</param>
        void Warn(string mensaje, params object[] parametros);

        /// <summary>
        /// Mensajes de error.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        void Error(string mensaje);

        /// <summary>
        /// Bitacoriza mensaje con nivel Error, el mensaje puede tener parametros adicionales
        /// para sustitución.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        /// <param name="parametros">Parametros que serán incluidos en el mensaje.</param>
        void Error(string mensaje, params object[] parametros);

        /// <summary>
        /// Mensaje de un error fatal, el sistema no debe seguir funcionando.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        void Fatal(string mensaje);

        /// <summary>
        /// Bitacoriza mensaje con nivel Fatal, el mensaje puede tener parametros adicionales
        /// para sustitución.
        /// </summary>
        /// <param name="mensaje">Mensaje a bitacorizar.</param>
        /// <param name="parametros">Parametros que serán incluidos en el mensaje.</param>
        void Fatal(string mensaje, params object[] parametros);
    }
}