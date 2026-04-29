namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class Periodo
{
    /// <summary>
    /// Identificador de Periodo
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// Descripcion del periodo
    /// </summary>
    public string Descripcion { get; private set; }
    /// <summary>
    /// Horario de inicio
    /// </summary>
    public string HoraDesde { get; private set; }
    /// <summary>
    /// horario final
    /// </summary>
    public string HoraHasta { get; private set; }
    /// <summary>
    /// Valor de segundos del tiempo minimo de una Consulta
    /// </summary>
    public decimal ConsultaTiempoMinimo { get; private set; }
    /// <summary>
    /// Valor de segundos del tiempo maximo de una Consulta
    /// </summary>
    public decimal ConsultaTiempoMaximo { get; private set; }
    /// <summary>
    /// Valor de segundos del tiempo minimo de una Transferencia
    /// </summary>
    public decimal TransferenciaTiempoMinimo { get; private set; }
    /// <summary>
    /// Valor de segundos del tiempo maximo de una Transferencia
    /// </summary>
    public decimal TransferenciaTiempoMaximo { get; private set; }
    /// <summary>
    /// Indicador de estado
    /// </summary>
    public string IndicadorEstado { get; private set; }
}
