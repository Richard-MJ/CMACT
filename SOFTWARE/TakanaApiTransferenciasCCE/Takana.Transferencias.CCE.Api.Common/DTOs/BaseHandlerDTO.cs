using Newtonsoft.Json;

namespace Takana.Transferencias.CCE.Api.Common;
public interface IHandler
{
    IHandler SetNext(IHandler nextHandler);
    string? Handle(string datos, string codigoValidacion);
}

/// <summary>
/// Base Hanlder de cadena de manejadores
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseHandler<T> : IHandler where T : class
{
    private IHandler? _nextHandler;

    public IHandler SetNext(IHandler nextHandler)
    {
        _nextHandler = nextHandler;
        return nextHandler;
    }

    /// <summary>
    /// Método que deserealiza y obtiene un codigo de validacion
    /// </summary>
    /// <param name="datos"></param>
    /// <param name="codigoValidacion"></param>
    /// <returns></returns>
    public string? Handle(string datos, string codigoValidacion)
    {
        var datosDeserializados = JsonConvert.DeserializeObject<T>(datos);

        if (EsValido(datosDeserializados))
        {
            ModificarCodigoValidacion(datosDeserializados, codigoValidacion);
            return JsonConvert.SerializeObject(datosDeserializados);
        }

        return _nextHandler?.Handle(datos, codigoValidacion);
    }

    /// <summary>
    /// Verifica si es valido
    /// </summary>
    /// <param name="datosDeserializados"></param>
    /// <returns></returns>
    protected abstract bool EsValido(T datosDeserializados);

    /// <summary>
    /// Modificia el codigo de validacion
    /// </summary>
    /// <param name="datosDeserializados"></param>
    /// <param name="codigoValidacion"></param>
    protected abstract void ModificarCodigoValidacion(T datosDeserializados, string codigoValidacion);
}

/// <summary>
/// Propiedad de handler con Base EstructuraContenidoET1
/// </summary>
public class ET1Handler : BaseHandler<EstructuraContenidoET1>
{
    protected override bool EsValido(EstructuraContenidoET1 datosDeserializados) => datosDeserializados.ET1 != null;

    protected override void ModificarCodigoValidacion(EstructuraContenidoET1 datosDeserializados, string codigoValidacion)
    {
        datosDeserializados.codigoValidacionFirma = codigoValidacion;
    }
}

/// <summary>
/// Propiedad de handler con Base EstructuraContenidoAV2
/// </summary>
public class AV2Handler : BaseHandler<EstructuraContenidoAV2>
{
    protected override bool EsValido(EstructuraContenidoAV2 datosDeserializados) => datosDeserializados.AV2 != null;

    protected override void ModificarCodigoValidacion(EstructuraContenidoAV2 datosDeserializados, string codigoValidacion)
    {
        datosDeserializados.codigoValidacionFirma = codigoValidacion;
    }
}

/// <summary>
/// Propiedad de handler con Base EstructuraContenidoCT2
/// </summary>
public class CT2Handler : BaseHandler<EstructuraContenidoCT2>
{
    protected override bool EsValido(EstructuraContenidoCT2 datosDeserializados) => datosDeserializados.CT2 != null;

    protected override void ModificarCodigoValidacion(EstructuraContenidoCT2 datosDeserializados, string codigoValidacion)
    {
        datosDeserializados.codigoValidacionFirma = codigoValidacion;
    }
}

/// <summary>
/// Propiedad de handler con Base EstructuraContenidoCT5
/// </summary>
public class CT5Handler : BaseHandler<EstructuraContenidoCT5>
{
    protected override bool EsValido(EstructuraContenidoCT5 datosDeserializados) => datosDeserializados.CT5 != null;

    protected override void ModificarCodigoValidacion(EstructuraContenidoCT5 datosDeserializados, string codigoValidacion)
    {
        datosDeserializados.codigoValidacionFirma = codigoValidacion;
    }
}

/// <summary>
/// Propiedad de handler con Base EstructuraContenidoCTC1
/// </summary>
public class CTC1Handler : BaseHandler<EstructuraContenidoCTC1>
{
    protected override bool EsValido(EstructuraContenidoCTC1 datosDeserializados) => datosDeserializados.CTC1 != null;

    protected override void ModificarCodigoValidacion(EstructuraContenidoCTC1 datosDeserializados, string codigoValidacion)
    {
        datosDeserializados.codigoValidacionFirma = codigoValidacion;
    }
}