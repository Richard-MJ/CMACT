namespace AutorizadorCanales.Logging.Interfaz;

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
    /// Mensajes de información, los cuales son normalmente activados en producción.
    /// </summary>
    /// <param name="mensaje">Mensaje a bitacorizar.</param>
    void Info(string mensaje);

    /// <summary>
    /// Bitacoriza un evento, generando toda la información en JSON.
    /// </summary>
    /// <param name="mensaje">Mensaje del evento, corto y solo texto.</param>
    /// <param name="datos">Lista de objeto que indicar el contexto del evento.</param>
    void Info(string mensaje, Dictionary<string, object> datos);

    /// <summary>
    /// Bitacoriza un evento con un grado de advertencia, generando un mensaje en plano.
    /// </summary>
    /// <param name="mensaje">Mensaje del evento, corto y solo texto.</param>
    void Warning(string mensaje);

    /// <summary>
    /// Bitacoriza un evento con un grado de advertencia, generando toda la información en JSON.
    /// </summary>
    /// <param name="mensaje">Mensaje del evento, corto y solo texto.</param>
    /// <param name="datos">Lista de objeto que indicar el contexto del evento.</param>
    void Warning(string mensaje, Dictionary<string, object> datos);

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
}
