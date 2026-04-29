using Swashbuckle.AspNetCore.Annotations;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones
{
    /// <summary>
    /// Clase de datos para realizar la transferencia por ventanilla
    /// </summary>
    public record OrdenTransferenciaVentanillaDTO
    {
        /// <summary>
        /// Datos de consulta de cuenta para CCE (Consulta de Cuenta Efectivo).
        /// </summary>
        [SwaggerSchema("Datos de consulta de cuenta para CCE (Consulta de Cuenta Efectivo).")]
        public ResultadoConsultaCuentaCCE DatosConsultaCuentaCCE { get; set; }

        /// <summary>
        /// Sesión de usuario actual, nullable.
        /// </summary>
        [SwaggerSchema("esión de usuario actual, nullable.")]
        public SesionUsuarioDTO? SesionUsuario { get; set; }

        /// <summary>
        /// Control de montos para la operación.
        /// </summary>
        [SwaggerSchema("Control de montos para la operación.")]
        public ControlMontoDTO ControlMonto { get; set; }

        /// <summary>
        /// Motivo de vinculación, nullable.
        /// </summary>
        [SwaggerSchema("Motivo de vinculación, nullable.")]
        public IngresoVinculoMotivoDTO? MotivoVinculo { get; set; }

        /// <summary>
        /// Número de cuenta asociado.
        /// </summary>
        [SwaggerSchema("Número de cuenta asociado.")]
        public string NumeroCuenta { get; set; }

        /// <summary>
        /// Número de lavado asociado.
        /// </summary>
        [SwaggerSchema("Número de lavado asociado.")]
        public int NumeroLavado { get; set; }

        /// <summary>
        /// Código de usuario asociado.
        /// </summary>
        [SwaggerSchema("Código de usuario asociado.")]
        public string CodigoUsuario { get; set; }

        /// <summary>
        /// Concepto de cobro de tarifa asociado.
        /// </summary>
        [SwaggerSchema("Concepto de cobro de tarifa asociado.")]
        public string ConceptoCobroTarifa { get; set; }
        /// <summary>
        /// Documento en bytes de terminos de beneficios y Riesgos
        /// </summary>
        [SwaggerSchema("Documento en bytes de terminos de beneficios y Riesgos")]
        public byte[] DocumentoTerminos { get; set; }
        /// <summary>
        /// Nombre de documento de terminos
        /// </summary>
        [SwaggerSchema("Nombre de documento de terminos")]
        public string NombreDocumentoTerminos { get; set; }

    }
}
