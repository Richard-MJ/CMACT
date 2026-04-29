using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Clase de datos de la consulta de cuenta receptor
    /// </summary>
    public record ConsultaCuentaCelularDTO
    {
        /// <summary>
        /// Numero de celular cliente receptor
        /// </summary>
        [SwaggerSchema("Numero de celular cliente receptor")]
        public string NumeroCelular{ get; set;}
        /// <summary>
        /// Codigo de entidad receptora
        /// </summary>
        [SwaggerSchema("Codigo de entidad receptora")]
        public string CodigoEntidad { get; set; }
        /// <summary>
        /// Numero de cuenta originante
        /// </summary>
        [SwaggerSchema("Numero de cuenta originante")]
        public CuentaEfectivoDTO? CuentaEfectivo { get; set; }
    }
}
