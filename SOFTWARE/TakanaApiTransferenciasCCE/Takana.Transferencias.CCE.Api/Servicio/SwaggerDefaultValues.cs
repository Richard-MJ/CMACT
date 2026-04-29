using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;

namespace Takana.Transferencias.CCE.Api.Servicio
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// Aplica valores predeterminados a cada operación en el Swagger/OpenAPI.
        /// </summary>
        /// <param name="operation">La operación OpenAPI que se genera.</param>
        /// <param name="context">El contexto de la operación (API description).</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null || !operation.Parameters.Any())
                return;            

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.FirstOrDefault(p => p.Name == parameter.Name);

                if (description == null) continue;                

                if (string.IsNullOrWhiteSpace(parameter.Description))
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }
            }
        }
    }
}
