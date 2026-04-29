using Microsoft.Extensions.Logging;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencia.CCE.Api.Loggin.Nlog
{
    public class NLogProxy<T> : IBitacora<T> where T : class
    {
        private readonly IContextoBitacora _contexto;
        private readonly ILogger<T> _logger;

        public NLogProxy(IContextoBitacora contexto, ILogger<T> logger)
        {
            _logger = logger;
            _contexto = contexto;
        }

        /// <summary>
        /// Método que genera el evento del trace
        /// </summary>
        /// <param name="mensaje"></param>
        public void Trace(string mensaje)
        {
            _logger.Log(LogLevel.Trace,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del trace
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Trace(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Trace,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del debug
        /// </summary>
        /// <param name="mensaje"></param>
        public void Debug(string mensaje)
        {
            _logger.Log(LogLevel.Debug,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del debug
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Debug(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Debug,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del info
        /// </summary>
        /// <param name="mensaje"></param>
        public void Info(string mensaje)
        {
            _logger.Log(LogLevel.Information,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del info
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Info(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Information,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del warn
        /// </summary>
        /// <param name="mensaje"></param>
        public void Warn(string mensaje)
        {
            _logger.Log(LogLevel.Warning,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del warn
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Warn(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Warning,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del Error
        /// </summary>
        /// <param name="mensaje"></param>
        public void Error(string mensaje)
        {
            _logger.Log(LogLevel.Error,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del Error
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Error(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Error,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del Fatal
        /// </summary>
        /// <param name="mensaje"></param>
        public void Fatal(string mensaje)
        {
            _logger.Log(LogLevel.Critical,
                default(EventId),
                GenerarEvento(mensaje),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método que genera el evento del Fatal
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="parametros"></param>
        public void Fatal(string mensaje, params object[] parametros)
        {
            _logger.Log(LogLevel.Critical,
                default(EventId),
                GenerarEvento(mensaje, parametros),
                null,
                BitacoraLogEvent.Formatter);
        }

        /// <summary>
        /// Método general que genera el evento
        /// </summary>
        /// <param name="plantilla"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        private BitacoraLogEvent GenerarEvento(string plantilla, params object[] datos)
        {
            bool bitacoraConsola;
            bool.TryParse(Environment.GetEnvironmentVariable("TAK_BITACORA_CONSOLA"), out bitacoraConsola);
            if (bitacoraConsola)
            {
                Console.WriteLine("--Inicio de evento--");
                Console.WriteLine(plantilla);
                foreach (var item in datos)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("--Fin de evento--");
            }

            var evento = new BitacoraLogEvent(plantilla, datos);

            evento.AddProp("idSesion", _contexto.IdSesion)
                .AddProp("codigoUsuario", _contexto.CodigoUsuario)
                .AddProp("codigoAgencia", _contexto.CodigoAgencia)
                .AddProp("indicadorCanal", _contexto.IndicadorCanal)
                .AddProp("indicadorSubCanal", _contexto.IndicadorSubCanal)
                .AddProp("idTerminalCliente", _contexto.IdTerminalOrigen)
                .AddProp("fechaEvento", DateTime.Now.ToString("O"))
                .AddProp("idServicio", _contexto.IdServicio)
                .AddProp("idLogin", _contexto.IdLogin);

            return evento;
        }
    }
}