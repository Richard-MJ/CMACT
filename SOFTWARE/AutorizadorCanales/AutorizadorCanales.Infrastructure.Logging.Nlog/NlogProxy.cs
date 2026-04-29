using AutorizadorCanales.Logging.Interfaz;
using Microsoft.Extensions.Logging;

namespace AutorizadorCanales.Infrastructure.Logging.Nlog;

public class NlogProxy<T> : IBitacora<T> where T : class
{
    private readonly IContextoBitacora _contexto;
    private readonly ILogger<T> _logger;

    public NlogProxy(IContextoBitacora contexto, ILogger<T> logger)
    {
        _contexto = contexto;
        _logger = logger;
    }

    public void Debug(string mensaje)
    {
        _logger.Log(LogLevel.Debug,
             default(EventId),
             GenerarEvento(mensaje),
             null,
             BitacoraLogEvent.Formatter);
    }

    public void Debug(string mensaje, params object[] parametros)
    {
        _logger.Log(LogLevel.Debug,
            default(EventId),
            GenerarEvento(mensaje, parametros),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Error(string mensaje)
    {
        _logger.Log(LogLevel.Error,
            default(EventId),
            GenerarEvento(mensaje),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Error(string mensaje, params object[] parametros)
    {
        _logger.Log(LogLevel.Error,
            default(EventId),
            GenerarEvento(mensaje, parametros),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Fatal(string mensaje)
    {
        _logger.Log(LogLevel.Critical,
            default(EventId),
            GenerarEvento(mensaje),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Info(string mensaje)
    {
        _logger.Log(LogLevel.Information,
            default(EventId),
            GenerarEvento(mensaje),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Info(string mensaje, Dictionary<string, object> parametros)
    {
        _logger.Log(LogLevel.Information,
            default(EventId),
            GenerarEvento(mensaje, parametros),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Warning(string mensaje)
    {
        _logger.Log(LogLevel.Warning,
            default(EventId),
            GenerarEvento(mensaje),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Warning(string mensaje, Dictionary<string, object> parametros)
    {
        _logger.Log(LogLevel.Warning,
            default(EventId),
            GenerarEvento(mensaje, parametros),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Trace(string mensaje)
    {
        _logger.Log(LogLevel.Trace,
            default(EventId),
            GenerarEvento(mensaje),
            null,
            BitacoraLogEvent.Formatter);
    }

    public void Trace(string mensaje, params object[] parametros)
    {
        _logger.Log(LogLevel.Trace,
            default(EventId),
            GenerarEvento(mensaje, parametros),
            null,
            BitacoraLogEvent.Formatter);
    }

    private BitacoraLogEvent GenerarEvento(string plantilla, params object[] datos)
    {
        var evento = new BitacoraLogEvent(plantilla, datos);

        evento.AddProp("idSesion", _contexto.IdSesion)
            .AddProp("codigoUsuario", _contexto.CodigoUsuario)
            .AddProp("codigoAgencia", _contexto.CodigoUsuario)
            .AddProp("indicadorCanal", _contexto.IndicadorCanal)
            .AddProp("indicadorSubCanal", _contexto.IndicadorSubCanal)
            .AddProp("idTerminalCliente", _contexto.IdTerminalOrigen)
            .AddProp("fechaEvento", DateTime.Now.ToString("O"))
            .AddProp("idServicio", _contexto.IdServicio)
            .AddProp("windowsIdentity", _contexto.CodigoUsuario)
            .AddProp("identity", _contexto.IdentidadUsuario)
            .AddProp("remoteAddress", _contexto.IdTerminalOrigen)
            .AddProp("contenidoEvento", plantilla);

        return evento;
    }
}
