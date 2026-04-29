using Takana.Transferencias.CCE.Api.Common.DTOs;

namespace Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

public record OrdenTransferenciaCanalDTO : CanalDTO
{
    /// <summary>
    /// Codigo de la entidad originante
    /// </summary>
    public string CodigoEntidadOriginante { get; set; }
    /// <summary>
    /// Codigo de la entidad Receptor
    /// </summary>
    public string CodigoEntidadReceptora { get; set; }
    /// <summary>
    /// Fecha de creacion de la transaccion
    /// </summary>
    public string FechaCreacionTransaccion { get; set; }
    /// <summary>
    /// Hora de creacion de la transaccion
    /// </summary>
    public string HoraCreacionTransaccion { get; set; }
    /// <summary>
    /// Codigo de reitento
    /// </summary>
    public string? CodigoRequerimientoReintento { get; set; }
    /// <summary>
    /// Identifica al terminal o dispositivo
    /// </summary>
    public string IdentificadorTerminal { get; set; }
    /// <summary>
    /// Numero de referencia de la transaccion
    /// </summary>
    public string NumeroReferencia { get; set; }
    /// <summary>
    /// Numero de seguimiento de la transaccion
    /// </summary>
    public string Trace { get; set; }
    /// <summary>
    /// Monto a enviar de la transaferencia
    /// </summary>
    public decimal MontoImporte { get; set; }
    /// <summary>
    /// Codigo del tipo de moneda
    /// </summary>
    public string CodigoMoneda { get; set; }
    /// <summary>
    /// Codigo  de transferencia 
    /// </summary>
    public string CodigoTransferencia { get; set; }
    /// <summary>
    /// Monto de la comision de la transferencia
    /// </summary>
    public decimal MontoComision { get; set; }
    /// <summary>
    /// Monto ITF de la transaccion en caso de que aplique
    /// </summary>
    public decimal MontoITF { get; set; }
    /// <summary>
    /// Codigo de Tarifa
    /// </summary>
    public string? CodigoTarifa { get; set; }
    /// <summary>
    /// Codigo del criterio de plaza
    /// </summary>
    public string? CriterioPlaza { get; set; }
    /// <summary>
    /// Tipo de persona originante
    /// </summary>
    public string? TipoPersonaDeudor { get; set; }
    /// <summary>
    /// Nombre el cliente originante
    /// </summary>
    public string? NommbreDeudor { get; set; }
    /// <summary>
    /// Direccoin del cliente originante
    /// </summary>
    public string? DireccionDeudor { get; set; }
    /// <summary>
    /// Tipo de documento del cliente originante
    /// </summary>
    public string TipoDocumentoDeudor { get; set; }
    /// <summary>
    /// Numero de indentificador de cliente originante
    /// </summary>
    public string NumeroIdentidadDeudor { get; set; }
    /// <summary>
    /// Numero de telefono del cliente originante
    /// </summary>
    public string? NumeroTelefonoDeudor { get; set; }
    /// <summary>
    /// Numero de celuclar del cliente originante
    /// </summary>
    public string? NumeroCelularDeudor { get; set; }
    /// <summary>
    /// Codigo de cuenta interbancaria del cliente originante
    /// </summary>
    public string? CodigoCuentaInterbancariaDeudor { get; set; }
    /// <summary>
    /// Tipo de documento del cliente receptor
    /// </summary>
    public string? TipoDocumentoReceptor { get; set; }
    /// <summary>
    /// Numero de indentificacion del cliente receptor
    /// </summary>
    public string? NumeroIdentidadReceptor { get; set; }
    /// <summary>
    /// Nombre del cliente receptor
    /// </summary>
    public string? NombreReceptor { get; set; }
    /// <summary>
    /// Direccion del cliente receptor
    /// </summary>
    public string? DireccionReceptor { get; set; }
    /// <summary>
    /// Numero de telegono del cliente receptor
    /// </summary>
    public string? NumeroTelefonoReceptor { get; set; }
    /// <summary>
    /// Numero de celular del cliente receptor
    /// </summary>
    public string? NumeroCelularReceptor { get; set; }
    /// <summary>
    /// Codigo de cuenta interbancario del cliente receptor
    /// </summary>
    public string? CodigoCuentaInterbancariaReceptor { get; set; }
    /// <summary>
    /// CodigoTarjetaReceptor
    /// </summary>
    public string? CodigoTarjetaReceptor { get; set; }
    /// <summary>
    /// Indicaor ITF
    /// </summary>
    public string IndicadroITF { get; set; }
    /// <summary>
    /// Concepto de CobroTarifa
    /// </summary>
    public string? ConceptoCobroTarifa { get; set; }
    /// <summary>
    /// Descripcion de la Transaccion
    /// </summary>
    public string? GlosaTransaccion { get; set; }
    /// <summary>
    /// Importe de Sueldos
    /// </summary>
    public decimal? ImporteSueldos { get; set; }
    /// <summary>
    /// Indicadore de Haberes
    /// </summary>
    public string? IndicadorHaberes { get; set; }
    /// <summary>
    /// Mes de pago
    /// </summary>
    public string? MesPago { get; set; }
    /// <summary>
    /// Año de pago
    /// </summary>
    public string? Añopago { get; set; }
    /// <summary>
    /// Tipo de transaccion
    /// </summary>
    public string TipoTransaccion { get; set; }
    /// <summary>
    /// Instruccion ID
    /// </summary>
    public string InstruccionId { get; set; }
    /// <summary>
    /// Mismo titular
    /// </summary>
    public string MismoTitular { get; set; }
    /// <summary>
    /// Numero Transferencia
    /// </summary>
    public int NumeroTransferencia { get; set; }
    /// <summary>
    /// Numero Movimiento
    /// </summary>
    public int NumeroMovimiento { get; set; }
    /// <summary>
    /// Numero Lavada
    /// </summary>
    public int NumeroLavado { get; set; }
    /// <summary>
    /// Numero Asiento
    /// </summary>
    public int NumeroAsiento { get; set; }
    /// <summary>
    /// Usuario que registra la operacion
    /// </summary>
    public string UsuarioRegistro { get; set; }
    /// <summary>
    /// Tipo de documento del cliente originante CCE
    /// </summary>
    public string? TipoDocumentoDeudorCCE { get; set; }
    /// <summary>
    /// Tipo de documento del cliente receptor
    /// </summary>
    public string? TipoDocumentoReceptorCCE { get; set; }
    /// <summary>
    /// Clave para los intentos de barrido
    /// </summary>
    public string KeyIntentosBarrido => $"interop:barrido:intentos:{CodigoCuentaInterbancariaDeudor}";
}
