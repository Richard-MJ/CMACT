using Microsoft.OpenApi;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Takana.Transferencias.CCE.Api.Common;

namespace Takana.Transferencias.CCE.Api.Servicio
{
    public class ConfigureSwaggerVersionServices : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerVersionServices(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <summary>
        /// Configuracion de version de swagger
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        /// <summary>
        /// Creacion de informacion para la version de API
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = $"CMACT > BACKEND > SERVICIO > {ConfigApi.Nombre}",
                Version = description.ApiVersion.ToString(),
                Description = ConfigApi.Descripcion,
                Contact = new OpenApiContact() { Name = "CMACT", Email = "AreadeTIC@cmactacna.com.pe" },
                License = new OpenApiLicense() { Name = "Derechos Reservados", Url = new Uri("http://google.com.pe") }
            };

            if (description.IsDeprecated) info.Description += "Esta API ha quedado obsoleta.";

            return info;
        }
    }
}