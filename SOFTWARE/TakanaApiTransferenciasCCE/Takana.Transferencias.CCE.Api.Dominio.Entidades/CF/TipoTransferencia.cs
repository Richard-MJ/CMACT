namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class TipoTransferencia
{
    #region Constantes
    /// <summary>
    /// Número de Cuenta Incorrecto
    /// </summary>
    public const string CodigoTransferenciaOrdinaria = "320";
    /// <summary>
    /// Cuenta Bloqueada
    /// </summary>
    public const string CodigoPagoTarjeta = "325";
    /// <summary>
    /// Estado Activo de la transferencia
    /// </summary>
    public const string EstadoActivo = "A";
    #endregion Constantes

    #region Propiedades
    /// <summary>
    /// Identificador del tipo de tansferencia
    /// </summary>
    public int IdTipoTransferencia { get; private set; }
    /// <summary>
    /// Codigo del tipo de transferencia
    /// </summary>
    public string Codigo { get; private set; }    
    /// <summary>
    /// Nombre del tipo de transferencia
    /// </summary>
    public string Descripcion { get; private set; }    
    /// <summary>
    /// Indicador de estado Activo o Inactivo del tipo de transferencia.
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Entidad de comision
    /// </summary>
    public virtual ICollection<ComisionCCE> Comisiones { get; private set; }
    #endregion Propiedades
}
