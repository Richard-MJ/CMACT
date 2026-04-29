using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

public class Transferencia : Empresa
{
    #region Constantes
    /// <summary>
    /// Codigo de serie de transfernecia
    /// </summary>
    public const string CodigoSerieTransferenciaEnCC = "CC_TRANSF";
    /// <summary>
    /// Indicador de operacion de transfernecia inmediata
    /// </summary>
    public const int IdOperacionTransferenciaInmediata = 11;
    /// <summary>
    /// Estado No
    /// </summary>
    public const string EstadoNo = "N";
    /// <summary>
    /// Estado Activo
    /// </summary>
    public const string EstadoActivo = "A";
    /// <summary>
    /// Codigo Originante
    /// </summary>
    public const string CodigoOrigante = "C";
    #endregion

    #region Propiedades
    /// <summary>
    /// Numero de transferencia
    /// </summary>
    public int NumeroTransferencia { get; private set; }
    /// <summary>
    /// Codigo de tipo de transferencia
    /// </summary>
    public string? CodigoTipoTransferencia { get; private set; }
    /// <summary>
    /// Fecha de transferencia
    /// </summary>
    public DateTime FechaTransferencia { get; private set; }
    /// <summary>
    /// COidog de origen
    /// </summary>
    public string CodigoOrigen { get; private set; }
    /// <summary>
    /// Codigo de cliente
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Codigo de agencia
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Numeor de cuenta
    /// </summary>
    public string? NumeroCuenta { get; private set; }
    /// <summary>
    /// Numero de movimiento
    /// </summary>
    public int NumeroMovimiento { get; private set; }
    /// <summary>
    /// Codigo de moneda
    /// </summary>
    public string CodigoMoneda { get; private set; }
    /// <summary>
    /// Monto de transferencia
    /// </summary>
    public decimal MontoTransferencia { get; private set; }
    /// <summary>
    /// Estado de transferencia
    /// </summary>
    public string EstadoTransferencia { get; private set; }
    /// <summary>
    /// Codigo de usuario
    /// </summary>
    public string CodigoUsuario { get; private set; }
    /// <summary>
    /// Datos de la cuenta origen
    /// </summary>
    public virtual CuentaEfectivo CuentaOrigen { get; private set; }
    /// <summary>
    /// Datos de la agencia
    /// </summary>
    public virtual Agencia Agencia { get; private set; }
    /// <summary>
    /// Transaccion detalle saliente
    /// </summary>
    public virtual ICollection<TransferenciaDetalleSalienteCCE> DetallesSalientes { get; private set; }
    /// <summary>
    /// Transaccion detalle de entrante
    /// </summary>
    public virtual ICollection<TransferenciaDetalleEntranteCCE> DetallesEntrantes { get; private set; }
    /// <summary>
    /// Codigo ente
    /// </summary>
    public string? CodigoEnte { get; private set; }
    /// <summary>
    /// Numero de documento
    /// </summary>
    public int? NumeroDocumento { get; private set; }
    /// <summary>
    /// Detall sumilla transferencia
    /// </summary>
    public string? DetalleSumillaTransferencia { get; private set; }
    /// <summary>
    /// Numero de movimiento  fuente
    /// </summary>
    public decimal? NumeroMovimientoFuente { get; private set; }
    /// <summary>
    /// Canal de la operacion
    /// </summary>
    public string? Canal { get; private set; }

    #endregion

    #region Constructor

    public Transferencia()
    {
        DetallesSalientes = new List<TransferenciaDetalleSalienteCCE>();
        DetallesEntrantes = new List<TransferenciaDetalleEntranteCCE>();
    }

    #endregion

    #region Métodos
    /// <summary>
    /// Completa la transferencia
    /// </summary>
    /// <param name="numeroMovimiento">Numero de movimiento</param>
    public void CompletarTransferencia(int numeroMovimiento)
    {
        NumeroMovimiento = numeroMovimiento;
        EstadoTransferencia = EstadoActivo;
    }

    /// <summary>
    /// Crea la transferencia inmediata saliente
    /// </summary>
    /// <param name="numeroTransferencia">numero de transferencia</param>
    /// <param name="codigoTipoTransferencia">codigo de tipo de transferencia</param>
    /// <param name="cuenta">cuenta efectivo</param>
    /// <param name="numeroMovimiento">numero de movimiento</param>
    /// <param name="montoTransferencia">monto de transferncia</param>
    /// <param name="fechaOperacion">Fecha de operacion</param>
    /// <param name="usuario">Usuario</param>
    /// <param name="canal">Canal</param>
    /// <returns>Retorna la transfernecia</returns>
    public static Transferencia CrearTransferenciaInmediataCCE(
     int numeroTransferencia
     ,string codigoTipoTransferencia
     ,CuentaEfectivo cuenta
     ,int numeroMovimiento
     ,decimal montoTransferencia
     ,DateTime fechaOperacion
     ,Usuario usuario
     ,string canal)
    {
        return new Transferencia()
        {
            CodigoEmpresa = Empresa.CodigoPrincipal,
            NumeroTransferencia = numeroTransferencia,
            CodigoTipoTransferencia = codigoTipoTransferencia,
            FechaTransferencia = fechaOperacion,
            CodigoOrigen = CodigoOrigante,
            CodigoCliente = cuenta.CodigoCliente,
            CodigoAgencia = usuario.CodigoAgencia,
            NumeroCuenta = cuenta.NumeroCuenta,
            NumeroDocumento = 0,
            NumeroMovimiento = numeroMovimiento,
            CodigoMoneda = cuenta.CodigoMoneda,
            MontoTransferencia = montoTransferencia,
            EstadoTransferencia = EstadoActivo,
            CodigoUsuario = usuario.CodigoUsuario,
            CuentaOrigen = cuenta,
            Canal = canal

        };
    }
   
    /// <summary>
    /// Agrega un detalle CCE
    /// </summary>
    /// <param name="detalleCCE">Entidad del detalle CCE.</param>
    /// <returns>La misma transferencia.</returns>
    public Transferencia AgregarDetalleCCE(TransferenciaDetalleSalienteCCE detalles)
    {
        DetallesSalientes.Add(detalles);
        return this;
    }

    /// <summary>
    /// Agrega un detalle CCE
    /// </summary>
    /// <param name="detalleCCE">Entidad del detalle CCE.</param>
    /// <returns>La misma transferencia.</returns>
    public Transferencia AgregarDetalleEntrantesCCE(TransferenciaDetalleEntranteCCE detalles)
    {
        DetallesEntrantes.Add(detalles);
        return this;
    }

    /// <summary>
    /// Agrega el numero de movimiento de origen de la transferencia
    /// </summary>
    /// <param name="firma">Entidad de la firma empresarial.</param>
    /// <returns>La misma transferencia.</returns>
    public void ActualizarNumeroMovimientoFuente(decimal numeroMovimiento)
    {
        NumeroMovimientoFuente = numeroMovimiento;
    }
    /// <summary>
    /// Invalida la transferencia
    /// </summary>
    public void InvalidarTransferencia()
    {
        EstadoTransferencia = EstadoNo;
    }
    /// <summary>
    /// Agrega la agencia a la transaccion
    /// </summary>
    public void AgregarAgencia(Agencia agencia)
    {
        Agencia = agencia;
    }
    #endregion

}

