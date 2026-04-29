using AutorizadorCanales.Domain.Entidades.TJ;

namespace AutorizadorCanales.Domain.Entidades.CL;

/// <summary>
/// Dispositivo canal electronico para identificar los inicios de sesion de los usuarios de
/// homebanking, app movil
/// </summary>
public class DispositivoCanalElectronico
{
    /// <summary>
    /// Numero identificador del dispositivo
    /// </summary>
    public string DispositivoId { get; private set; } = null!;
    /// <summary>
    /// Numero de tarjeta del cliente
    /// </summary>
    public decimal NumeroTarjeta { get; private set; }
    /// <summary>
    /// Codigo del cliente
    /// </summary>
    public string CodigoCliente { get; private set; } = null!;
    /// <summary>
    /// Fecha de generacion del registro
    /// </summary>
    public DateTime? FechaGeneracion { get; private set; }
    /// <summary>
    /// Indicador de estado del registro
    /// </summary>
    public string? IndicadorEstado { get; private set; } = null!;
    /// <summary>
    /// Indicador de canal
    /// </summary>
    public string? IndicadorCanal { get; private set; } = null!;
    /// <summary>
    /// Codigo de empresa
    /// </summary>
    public string? CodigoEmpresa { get; private set; } = null!;
    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime? FechaRegistro { get; private set; }
    /// <summary>
    /// Tarjeta asociada al dispositivo
    /// </summary>
    public virtual Tarjeta Tarjeta { get; private set; } = null!;

    /// <summary>
    /// Metodo para generar un nuevo dispositivo canal electronico
    /// </summary>
    /// <param name="dispositivoId">identificador del dispositivo</param>
    /// <param name="tarjeta">numero de tarjeta del cliente</param>
    /// <param name="indicadorCanal">indicador canal donde se inicio sesion</param>
    /// <param name="fechaGeneracion">fecha de generacion</param>
    /// <returns>entidad dispositivo canal electronico con estado pendiente</returns>
    public static DispositivoCanalElectronico Generar(string dispositivoId, Tarjeta tarjeta,
                                string indicadorCanal, DateTime fechaGeneracion)
    {
        return new DispositivoCanalElectronico()
        {
            DispositivoId = dispositivoId,
            NumeroTarjeta = tarjeta.NumeroTarjeta,
            CodigoEmpresa = EntidadEmpresa.EMPRESA,
            CodigoCliente = tarjeta.Duenio!.CodigoCliente,
            FechaGeneracion = fechaGeneracion,
            IndicadorEstado = EstadoEntidad.PENDIENTE,
            IndicadorCanal = indicadorCanal,
            FechaRegistro = DateTime.Now
        };
    }

    /// <summary>
    /// Método que cambia el estado a inactivo
    /// </summary>
    public void CancelarDispositivo()
    {
        IndicadorEstado = EstadoEntidad.INACTIVO;
    }
}