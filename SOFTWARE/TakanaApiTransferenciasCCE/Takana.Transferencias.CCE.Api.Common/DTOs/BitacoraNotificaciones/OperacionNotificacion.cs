namespace Takana.Transferencias.CCE.Api.Common.DTOs.BitacoraNotificaciones;

public class OperacionNotificacion
{
    /// <summary>
    /// Codigo de sistema de operacion
    /// </summary>
    public string CodigoSistema { get; set; }
    /// <summary>
    /// Codigo tipo transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; set; }
    /// <summary>
    /// Codigo subtipo transaccion
    /// </summary>
    public string CodigoSubtipoTransaccion { get; set; }
    /// <summary>
    /// Codigo agencia de movimiento
    /// </summary>
    public string CodigoAgenciaMovimiento { get; set; }
    /// <summary>
    /// Numero de movimiento
    /// PR --> PR_RECIBOS
    /// CC --> MOVTO_DIARIO / MENSUAL
    /// DP --> DP_MOVIM
    /// TJ --> TJ_MOVIM
    /// CJ --> CJ.TRAN_DIARIO / MENSUAL
    /// </summary>
    public string NumeroMovimiento { get; set; }
    /// <summary>
    /// Codigo de canal
    /// </summary>
    public string CodigoCanal { get; set; }
}
