namespace AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;

public class RespuestaAutorizacionCanalElectronico
{
    /// <summary>
    /// Propiedad que determina si el codigo de autorizacion es
    /// valido o no
    /// </summary>
    public bool EsValido { get; set; }
    /// <summary>
    /// Id Navegador a devolver cuando se realiza una verificacion de
    /// un nuevo dispositivo
    /// </summary>
    public string IdNavegador { get; set; } = null!;
    /// <summary>
    /// Numero de serie para nuevo dispositivo
    /// </summary>
    public int NumeroMovimiento { get; set; }
}
