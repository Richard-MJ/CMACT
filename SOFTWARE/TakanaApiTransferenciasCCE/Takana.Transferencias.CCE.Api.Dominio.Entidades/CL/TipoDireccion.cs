namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
/// <summary>
/// Clase que representa a la entidad de dominio de un Tipo de direccion
/// </summary>
public class TipoDireccion
{
    /// <summary>
    /// Codigo de tipo de transaccion
    /// </summary>
    public string CodigoTipoDireccion { get; private set; }
    /// <summary>
    /// Descripcion de tipo de transaccion
    /// </summary>
    public string DescripcionTipoDireccion { get; private set; }
    /// <summary>
    /// Indicador de prioridad natural
    /// </summary>
    public int IndicadorPrioridadNatural { get; private set; }
    /// <summary>
    /// Indicador de prioridad juridica
    /// </summary>
    public int IndicadorPrioridadJuridica { get; private set; }
}
