using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Takana.Transferencias.CCE.Api.Common.Excepciones;

namespace Takana.Transferencias.CCE.Api.Servicio;
public class HttpGlobalExceptionFilter : IExceptionFilter, IFilterMetadata
{
    private readonly ILogger<HttpGlobalExceptionFilter> logger;

    /// <summary>
    /// Constructor de http global de excepciones filter
    /// </summary>
    /// <param name="logger"></param>
    public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Excepcion
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

        if (context.Exception.GetType() == typeof(DomainException))
        {
            context.Result = new BadRequestObjectResult(BaseException.DescripcionExcepcionGeneral);
            context.HttpContext.Response.StatusCode = 400;
        }
        else
        {
            context.Result = new InternalServerErrorObjectResult(BaseException.DescripcionExcepcionGeneral);
            context.HttpContext.Response.StatusCode = 500;
        }

        context.ExceptionHandled = true;
    }
}
