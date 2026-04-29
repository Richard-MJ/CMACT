using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Takana.Transferencias.CCE.Api.Common.Interoperabilidad;

/// <summary>
/// Clase de datos de entrada para barrido de contactos
/// </summary>
public record ContactosBarrido
{
    /// <summary>
    /// Numero Celular receptor
    /// </summary>
    [Required]
    [SwaggerSchema("Numero Celular receptor")]
    public  string NumeroCelular {get; set;}
    /// <summary>
    /// Nombre Aliar
    /// </summary>
    [SwaggerSchema("Nombre Aliar")]
    public string NombreAlias {get; set;}
}


