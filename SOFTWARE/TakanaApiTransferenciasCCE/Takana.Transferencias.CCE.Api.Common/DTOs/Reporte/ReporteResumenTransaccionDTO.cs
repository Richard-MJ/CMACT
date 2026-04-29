namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    /// <summary>
    /// DTO de reporte de resumen de transacciones
    /// </summary>
    public class ReporteResumenTransaccionDTO
    {
        #region Propiedades
        /// <summary>
        /// Número de transacciones aprobadas en el periodo.
        /// </summary>
        public int TransaccionesAprobadasNumero { get; set; }

        /// <summary>
        /// Monto de transacciones aprobadas en el periodo.
        /// </summary>
        public decimal TransaccionesAprobadasMonto { get; set; }

        /// <summary>
        /// Número de transacciones denegadas en el periodo.
        /// </summary>
        public int TransaccionesDenegadasNumero { get; set; }

        /// <summary>
        /// Monto de transacciones denegadas en el periodo.
        /// </summary>
        public decimal TransaccionesDenegadasMonto { get; set; }

        /// <summary>
        /// Número de transacciones P2P en el periodo.
        /// </summary>
        public int TransaccionesP2PNumero { get; set; }

        /// <summary>
        /// Monto de transacciones P2P en el periodo.
        /// </summary>
        public decimal TransaccionesP2PMonto { get; set; }

        /// <summary>
        /// Número de transacciones P2M en el periodo.
        /// </summary>
        public int TransaccionesP2MNumero { get; set; }

        /// <summary>
        /// Monto de transacciones P2M en el periodo.
        /// </summary>
        public decimal TransaccionesP2MMonto { get; set; }
        /// <summary>
        /// Lista de transacciones aprobadas
        /// </summary>
        public List<TransaccionesAprobadasDTO> TransaccionesAprobadas { get; set; } = new List<TransaccionesAprobadasDTO>();
        /// <summary>
        /// Lista de transacciones denegadas
        /// </summary>
        public List<TransaccionesDenegadasDTO> TransaccionesDenegadas { get; set; } = new List<TransaccionesDenegadasDTO>();
        /// <summary>
        /// Lista de transacciones por rango de monto
        /// </summary>
        public List<TransaccionesPorRangoMontoDTO> TransaccionesPorRangoMonto { get; set; } = new List<TransaccionesPorRangoMontoDTO>();
        #endregion
    }

    /// <summary>
    /// DTO de transacciones Aprobadas
    /// </summary>
    public class TransaccionesAprobadasDTO
    {
        /// <summary>
        /// Codigo de la entidad beneficiaria
        /// </summary>
        public string CodigoEntidadBeneficiaria { get; set; }
        /// <summary>
        /// Número de transacciones aprobadas por entidad beneficiaria y procesador de pago.
        /// </summary>
        public int NumeroTransaccionesAprobadas { get; set; }

        /// <summary>
        /// Monto de transacciones aprobadas por entidad beneficiaria y procesador de pago.
        /// </summary>
        public decimal MontoTransaccionesAprobadas { get; set; }
    }

    /// <summary>
    /// DTO de Transacciones Denegadas
    /// </summary>
    public class TransaccionesDenegadasDTO
    {
        /// <summary>
        /// Codigo de la entidad beneficiaria
        /// </summary>
        public string CodigoEntidadBeneficiaria { get; set; }
        /// <summary>
        /// Código de error o negación devuelto por el Procesador de Pago
        /// </summary>
        public string IdentificadorCodigoNegacion { get; set; }
        /// <summary>
        /// Número de transacciones denegadas por entidad beneficiaria, procesador de pago y código de negación.
        /// </summary>
        public int NumeroTransaccionesDenegadas { get; set; }
        /// <summary>
        /// Monto de transacciones denegadas.
        /// </summary>
        public decimal MontoTransaccionesDenegadas { get; set; }
    }

    /// <summary>
    /// DTO de Transacciones Por Rango de Monto
    /// </summary>
    public class TransaccionesPorRangoMontoDTO
    {
        /// <summary>
        /// Codigo de la entidad beneficiaria
        /// </summary>
        public string CodigoEntidadBeneficiaria { get; set; }
        /// <summary>
        /// Número total de transacciones por un valor menor o igual a S/20.
        /// </summary>
        public int NumeroTransaccionesMenorIgual20 { get; set; }
        /// <summary>
        /// Número total de transacciones por un valor mayor a S/20 y menor o igual a S/50.
        /// </summary>
        public int NumeroTransaccionesMayor20MenorIgual50 { get; set; }
        /// <summary>
        /// Número total de transacciones por un valor mayor a S/50 y menor o igual a S/100.
        /// </summary>
        public int NumeroTransaccionesMayor50MenorIgual100 { get; set; }
        /// <summary>
        /// Número total de transacciones por un valor mayor a S/100 y menor o igual a S/200.
        /// </summary>
        public int NumeroTransaccionesMayor100MenorIgual200 { get; set; }
        /// <summary>
        /// Número total de transacciones por un valor mayor a S/200.
        /// </summary>
        public int NumeroTransaccionesMayor200 { get; set; }
    }
}
