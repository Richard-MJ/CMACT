namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class CanalPorSubTransaccionCCE
{
    /// <summary>
    /// Identificador Canal Por SubTransaccion
    /// </summary>
    public int IdCanalPorSubTransaccion { get; private set; }
    /// <summary>
    /// Codigo de canal de la CCE
    /// </summary>
    public string CodigoCanalCCE {get; private set;}

    /// <summary>
    /// Descripcion de canal
    /// </summary>
    public string IndicadorTipo {get; private set;}

    /// <summary>
    /// Codigo Canal
    /// </summary>
    public string CodigoCanal {get; private set;}

    /// <summary>
    /// Numero de sub canal
    /// </summary>
    public string NumeroSubCanal { get; private set; }
    /// <summary>
    /// Tipo de Transaccion
    /// </summary>
    public string CodigoTipoTransaccion { get; private set; }
    /// <summary>
    /// Sub Tipo de Transaccion
    /// </summary>
    public string CodigoSubTipoTransaccion { get; private set; }
    /// <summary>
    /// Codigo de Empresa
    /// </summary>
    public string CodigoEmpresa { get; private set; }
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Codigo usuario Transaccion
    /// </summary>
    public string CodigoUsuarioTransaccion { get; private set; }

    /// <summary>
    /// Entidad de canal CCE
    /// </summary>
    public virtual CanalCCE CanalCCE { get; private set; }
    /// <summary>
    /// Entidad De Sub Tipo de Transaccion
    /// </summary>
    public virtual SubTipoTransaccion SubTipoTransaccion { get; private set; }
}
