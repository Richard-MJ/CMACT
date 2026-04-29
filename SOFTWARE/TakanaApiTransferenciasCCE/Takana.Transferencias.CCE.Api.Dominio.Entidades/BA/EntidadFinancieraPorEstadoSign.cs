using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
/// <summary>
/// Clase que representa a la entidad de dominio Entidad financiera por transferencia
/// </summary>
public class EntidadFinancieraPorTransferencia
{
    #region Constantes
    public const int CantidadValida = 2;
    #endregion

    #region Propiedades

    /// <summary>
    /// Id de los registros de EntidadFinancieraPorEstadoSign
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Identificador de la Entidad Financiera
    /// </summary>
    public int IdEntidad { get; private set; }

    /// <summary>
    /// Identificador del Tipo de de transferencia
    /// </summary>
    public int IdentificadorTipoTransferencia { get; private set; }

    /// <summary>
    /// Identificador de la Entidad originante
    /// </summary>
    public string IndicadorParticipanteOriginante { get; private set; }

    /// <summary>
    /// Identificador de la Entidad receptora
    /// </summary>
    public string IndicadorParticipanteReceptor { get; private set; }

    /// <summary>
    /// Identificador de tipo de transferencia
    /// </summary>
    public virtual TipoTransferencia TipoTransferencia { get; private set; }

    #endregion Propiedades
}