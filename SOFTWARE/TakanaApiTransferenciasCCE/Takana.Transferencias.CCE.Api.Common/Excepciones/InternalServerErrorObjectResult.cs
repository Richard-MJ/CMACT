using Microsoft.AspNetCore.Mvc;

namespace Takana.Transferencias.CCE.Api.Common.Excepciones;

public class InternalServerErrorObjectResult : ObjectResult
{
    /// <summary>
    /// Internal sever error de objeto resultante
    /// </summary>
    /// <param name="error"></param>
    public InternalServerErrorObjectResult(object error)
        : base(error)
    {
        StatusCode = 500;
    }
}
