using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs
{
    public class IngresoVinculoMotivoDTO
    {
        #region MyRegion
        /// <summary>
        /// Identificador de otros motivos
        /// </summary>
        public const int IdOtrosMotivos = 7;
        /// <summary>
        /// Identificador de otros vinculos
        /// </summary>
        public const int IdOtrosVinculos = 5;
        #endregion
        /// <summary>
        /// Numero de operacion
        /// </summary>
        [SwaggerSchema("Numero de operacion")]
        public int NumeroOperacion { get; set; }
        /// <summary>
        /// Codigo de objeto
        /// </summary>
        [SwaggerSchema("Codigo de objeto")]
        public string CodigoObjeto { get; set; }
        /// <summary>
        /// Identificador de motivo
        /// </summary>
        [SwaggerSchema("dentificador de motivo")]
        public int IdMotivo { get; set; }
        /// <summary>
        /// Motivo especificado
        /// </summary>
        [SwaggerSchema("Motivo especificado")]
        public string MotivoEspecificado { get; set; }
        /// <summary>
        /// indentificador de vinculo
        /// </summary>
        [SwaggerSchema("indentificador de vinculo")]
        public int IdVinculo { get; set; }
        /// <summary>
        /// Vinculo especificado
        /// </summary>
        [SwaggerSchema("Vinculo especificado")]
        public string VinculoEspecificado { get; set; }
        /// <summary>
        /// Codigo de sistema
        /// </summary>
        [SwaggerSchema("Codigo de sistema")]
        public string CodigoSistema { get; set; }
        /// <summary>
        /// Identificador nacionalidad
        /// </summary>
        [SwaggerSchema("Identificador nacionalidad")]
        public string? idNacionalidad { get; set; }
    }
}
