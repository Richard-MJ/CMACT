using Microsoft.Extensions.Logging;
using System.Collections;

namespace AutorizadorCanales.Infrastructure.Logging.Nlog;

public class BitacoraLogEvent : ILogger, IReadOnlyList<KeyValuePair<string, object>>
{
    private readonly string _formato;
    private readonly object[] _parametros;
    private IReadOnlyList<KeyValuePair<string, object>> _logValores;
    private List<KeyValuePair<string, object>> _propiedadesExtra;

    public BitacoraLogEvent(string formato, params object[] valores)
    {
        _formato = formato;
        _parametros = valores;
    }

    public BitacoraLogEvent AddProp(string nombre, object valor)
    {
        var propiedades = _propiedadesExtra ??= new List<KeyValuePair<string, object>>();
        propiedades.Add(new KeyValuePair<string, object>(nombre, valor));

        return this;
    }

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
                return _propiedadesExtra.Concat(LogValores).GetEnumerator();
            }
            else
            {
                return LogValores.GetEnumerator();
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

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

    public int Count => MessagePropertyCount + ExtraPropertyCount;

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

    private int ExtraPropertyCount => _propiedadesExtra?.Count ?? 0;

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

    void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
        Func<TState, Exception, string> formatter)
    {
        _logValores = state as IReadOnlyList<KeyValuePair<string, object>> ??
                      Array.Empty<KeyValuePair<string, object>>();
    }

    bool ILogger.IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    IDisposable ILogger.BeginScope<TState>(TState state)
    {
        throw new NotImplementedException();
    }

    public static Func<BitacoraLogEvent, Exception, string> Formatter { get; } =
        (l, e) => l.LogValores.ToString();
}
