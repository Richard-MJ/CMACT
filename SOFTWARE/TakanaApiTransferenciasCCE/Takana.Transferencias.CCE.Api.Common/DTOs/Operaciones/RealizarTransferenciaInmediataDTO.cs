using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

/// <summary>
/// Clase de datos para realizar la transfernecia inmediata
/// </summary>
public record RealizarTransferenciaInmediataDTO
    {
        /// <summary>
        /// Titular si/no
        /// </summary>
        public enum NumeroTitular
        {
            No = 0,
            Si = 1,
        }
        /// <summary>
        /// Código de la agencia asociada.
        /// </summary>
        public string CodigoAgencia { get; set; }
        /// <summary>
        /// Código del usuario asociado.
        /// </summary>
        public string CodigoUsuario { get; set; }
        /// <summary>
        /// Número de cuenta originante.
        /// </summary>
        public string NumeroCuentaOriginante { get; set; }
        /// <summary>
        /// Código del tipo de transferencia CCE (Consulta de Cuenta Efectivo).
        /// </summary>
        public string CodigoTipoTransferenciaCce { get; set; }
        /// <summary>
        /// Control de montos para la operación.
        /// </summary>
        public ControlMontoDTO ControlMonto { get; set; }
        /// <summary>
        /// Código de la entidad receptora asociada.
        /// </summary>
        public string CodigoEntidadReceptora { get; set; }
        /// <summary>
        /// Código de cuenta de transacción del receptor.
        /// </summary>
        public string CodigoCuentaTransaccionReceptor { get; set; }
        /// <summary>
        /// Código del tipo de documento del receptor.
        /// </summary>
        public string CodigoTipoDocumentoReceptor { get; set; }
        /// <summary>
        /// Número de documento del receptor.
        /// </summary>
        public string NumeroDocumentoReceptor { get; set; }
        /// <summary>
        /// Beneficiario de la transacción.
        /// </summary>
        public string Beneficiario { get; set; }
        /// <summary>
        /// Indicador de si el titular en destino es el mismo.
        /// </summary>
        public bool MismoTitularEnDestino { get; set; }
        /// <summary>
        /// Código tarifario de la comisión asociada.
        /// </summary>
        public string CodigoTarifarioComision { get; set; }
        /// <summary>
        /// Número de lavado asociado.
        /// </summary>
        public int NumeroLavado { get; set; }
        /// <summary>
        /// Canal utilizado para la transacción.
        /// </summary>
        public string Canal { get; set; }
        /// <summary>
        /// Sub Canal utilizado para la transacción.
        /// </summary>
        public string SubCanal { get; set; }
        /// <summary>
        /// Motivo de vinculación, nullable.
        /// </summary>
        public IngresoVinculoMotivoDTO? MotivoVinculo { get; set; }
        /// <summary>
        /// Nombre de la impresora utilizada.
        /// </summary>
        public string NombreImpresora { get; set; }
        /// <summary>
        /// Glosario o descripción de la transacción, nullable.
        /// </summary>
        public string? GlosarioTransaccion { get; set; }
        /// <summary>
        /// Indicadro de misma plaza
        /// </summary>
        public bool EsMismaPlaza => CodigoTarifarioComision == General.MismaPlaza;
}

    



