namespace AutorizadorCanales.Aplication.Common.ServicioColas.DTOs;

public class DtoOperacionNotificacionCola
{
    /// <summary>
    /// Codigo de sistema de operacion
    /// </summary>
    public string CodigoSistema { get; set; } = null!;
    /// <summary>
    /// Codigo tipo transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; set; } = null!;
    /// <summary>
    /// Codigo subtipo transaccion
    /// </summary>
    public string CodigoSubtipoTransaccion { get; set; } = null!;
    /// <summary>
    /// Codigo agencia de movimiento
    /// </summary>
    public string CodigoAgenciaMovimiento { get; set; } = null!;
    /// <summary>
    /// Numero de movimiento
    /// PR --> PR_RECIBOS
    /// CC --> MOVTO_DIARIO / MENSUAL
    /// DP --> DP_MOVIM
    /// TJ --> TJ_MOVIM
    /// CJ --> CJ.TRAN_DIARIO / MENSUAL
    /// </summary>
    public string NumeroMovimiento { get; set; } = null!;
}
