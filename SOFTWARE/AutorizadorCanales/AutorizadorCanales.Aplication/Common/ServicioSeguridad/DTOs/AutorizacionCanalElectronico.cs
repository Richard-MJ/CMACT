namespace AutorizadorCanales.Aplication.Common.ServicioSeguridad.DTOs;

public class AutorizacionCanalElectronico
{
    /// <summary>
    /// Identificador de la operación
    /// </summary>
    public int IdTipoOperacion { get; set; }
    /// <summary>
    /// Identificador de la operación
    /// </summary>
    public string IdVerificacion { get; set; }
    /// <summary>
    /// Código de autorización generado
    /// </summary>
    public string CodigoAutorizacion { get; set; }
    /// <summary>
    /// Código de la moneda autorizada
    /// </summary>
    public string CodigoMonedaAutorizada { get; set; }
    /// <summary>
    /// Monto autorizado
    /// </summary>
    public decimal MontoAutorizado { get; set; }
    /// <summary>
    /// Monto autorizado
    /// </summary>
    public int IdTipoOperacionCanalElectronico { get; set; }
    /// <summary>
    /// Monto autorizado
    /// </summary>
    public string IdRegistroNuevoDispositivo { get; set; }
}
