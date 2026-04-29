namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class Calendario : Empresa
{
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia {get; private set;}
    /// <summary>
    /// Codigo de sistema
    /// </summary>
    public string CodigoSistema {get; private set; }
    /// <summary>
    /// Fecha de sistema
    /// </summary>
    public DateTime FechaSistema {get; private set; }
    /// <summary>
    /// Fecha y hora del sistema
    /// </summary>
    public DateTime FechaHoraSistema
    {
        get
        {
            return FechaSistema.Date + DateTime.Now.TimeOfDay;
        }
    }
    /// <summary>
    /// Fecha del formato
    /// </summary>
    public string FechaFormato => FechaSistema.ToString("yyyyMMdd");
    /// <summary>
    /// Hora del formato
    /// </summary>
    public string HoraFormato => DateTime.Now.TimeOfDay.ToString("hhmmss");
    /// <summary>
    /// Fecha de formato de mes y dia
    /// </summary>
    public string FechaFormatoMesDia => FechaSistema.ToString("MMdd");
    /// <summary>
    /// Fecha de interoperabilidad formato
    /// </summary>
    public string FechaInteroperabilidadFormato => (FechaSistema + DateTime.Now.TimeOfDay).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'");
}
