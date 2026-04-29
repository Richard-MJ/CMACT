using AutorizadorCanales.Domain.Entidades.SG;

namespace AutorizadorCanales.Domain.Entidades.CL;

public class AfiliacionBiometrica
{
    #region Propiedades
    /// <summary>
    /// Identificador de la afiliación
    /// </summary>
    public int NumeroAfiliacionBiometria { get; private set; }
    /// <summary>
    /// Identificador del dispositivo
    /// </summary>
    public int? NumeroAfiliacion { get; private set; }
    /// <summary>
    /// Identificador del tipo de biometria
    /// </summary>
    public int NumeroTipoBiometria { get; private set; }
    /// <summary>
    /// Dispositivo ID registrado
    /// </summary>
    public string DispositivoId { get; private set; } = null!;
    /// <summary>
    /// Clave unica generada para verificar el dispositivo para la biometria
    /// </summary>
    public string ClaveAfiliacion { get; private set; } = null!;
    /// <summary>
    /// Indicador de estado de la afiliación
    /// </summary>
    public string IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Fecha en la que se registro la afiliación
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Fecha en la que se registro la afiliación
    /// </summary>
    public DateTime FechaModificacion { get; private set; }
    /// <summary>
    /// Codigo del usuario que agrego la afiliación
    /// </summary>
    public string CodigoUsuario { get; private set; } = null!;
    /// <summary>
    /// Referencia a los tipo de biometria
    /// </summary>
    public virtual TipoBiometria TipoBiometria { get; private set; } = null!;
    /// <summary>
    /// Referencia a los dispositivos afiliados
    /// </summary>
    public virtual AfiliacionTokenDigital AfiliacionTokenDigital { get; private set; } = null!;
    #endregion

    #region Métodos
    /// <summary>
    /// Método que verifica la autoridad del dispositivo afiliado
    /// </summary>
    /// <param name="dispositivoId"></param>
    /// <returns></returns>
    public bool EsDispositivoRegistrado(string dispositivoId)
    {
        return DispositivoId == dispositivoId;
    }

    /// <summary>
    /// Valida si la clave ingresada es la misma con la registrada
    /// </summary>
    /// <param name="claveAfiliacion"></param>
    /// <returns></returns>
    public bool EsClaveValida(string claveAfiliacion)
        => ClaveAfiliacion == claveAfiliacion;
    #endregion
}
