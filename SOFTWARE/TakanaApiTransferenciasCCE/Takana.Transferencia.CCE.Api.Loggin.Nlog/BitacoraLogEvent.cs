using System.Collections;
using Microsoft.Extensions.Logging;

namespace Takana.Transferencia.CCE.Api.Loggin.Nlog
{
    public class BitacoraLogEvent : ILogger, IReadOnlyList<KeyValuePair<string, object>>
    {
        #region Declaraciones
        private readonly string _formato;
        private readonly object[] _parametros;
        private IReadOnlyList<KeyValuePair<string, object>> _logValores;
        private List<KeyValuePair<string, object>> _propiedadesExtra;
        #endregion

        /// <summary>
        /// Clase de constructor de bitacore de eventos
        /// </summary>
        /// <param name="formato"></param>
        /// <param name="valores"></param>
        public BitacoraLogEvent(string formato, params object[] valores)
        {
            _formato = formato;
            _parametros = valores;
        }

        /// <summary>
        /// Método que agrega el prop
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public BitacoraLogEvent AddProp(string nombre, object valor)
        {
            var propiedades = _propiedadesExtra ??= new List<KeyValuePair<string, object>>();
            propiedades.Add(new KeyValuePair<string, object>(nombre, valor));

            return this;
        }

        /// <summary>
        /// Método quelee la lista del log
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter)
        {
            _logValores = state as IReadOnlyList<KeyValuePair<string, object>> ??
                          Array.Empty<KeyValuePair<string, object>>();
        }

        /// <summary>
        /// Método que enumero los key values
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            if (MessagePropertyCount == 0)
            {
                if (ExtraPropertyCount > 0)
                {
                    return _propiedadesExtra.GetEnumerator();
                }
                else
                {
                    return Enumerable.Empty<KeyValuePair<string, object>>().GetEnumerator();
                }
            }
            else
            {
                if (ExtraPropertyCount > 0)
                {
                    return Enumerable.Concat(_propiedadesExtra, LogValores).GetEnumerator();
                }
                else
                {
                    return LogValores.GetEnumerator();
                }
            }
        }

        /// <summary>
        /// Método que recibe los enumeradores
        /// </summary>
        /// <returns>Retorna el get value</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Método que identifica el key value pair
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                int contadorExtra = ExtraPropertyCount;
                if (index < contadorExtra)
                {
                    return _propiedadesExtra[index];
                }
                else
                {
                    return LogValores[index - contadorExtra];
                }
            }
        }

        /// <summary>
        /// Contador
        /// </summary>
        public int Count => MessagePropertyCount + ExtraPropertyCount;

        /// <summary>
        /// Lectura de key value pair
        /// </summary>
        private IReadOnlyList<KeyValuePair<string, object>> LogValores
        {
            get
            {
                if (_logValores == null)
                {
                    this.LogDebug(_formato, _parametros);
                }

                return _logValores;
            }
        }

        /// <summary>
        /// Método que habilita el log
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// Método que devuelve el valor del begin scope
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Funcion estatica para el formate de los valores del log
        /// </summary>
        public static Func<BitacoraLogEvent, Exception, string> Formatter { get; } =
           (l, e) => l.LogValores.ToString();

        /// <summary>
        /// Propieda extra de contador
        /// </summary>
        private int ExtraPropertyCount => _propiedadesExtra?.Count ?? 0;

        /// <summary>
        /// Propiedad de mensaje de contador
        /// </summary>
        private int MessagePropertyCount
        {
            get
            {
                if (LogValores.Count > 1 && !string.IsNullOrEmpty(LogValores[0].Key) && !char.IsDigit(LogValores[0].Key[0]))
                {
                    return LogValores.Count;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}