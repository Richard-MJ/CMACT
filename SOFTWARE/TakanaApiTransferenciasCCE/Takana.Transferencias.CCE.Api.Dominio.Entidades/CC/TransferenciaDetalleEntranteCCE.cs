using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

/// <summary>
/// Entidad del detalle de la transferencia CCE
/// </summary>
public class TransferenciaDetalleEntranteCCE
{
    #region Propiedades
    /// <summary>
    /// Número de transferencia
    /// </summary>
    public int NumeroTransferencia { get; private set; }
    /// <summary>
    /// Código de cuenta interbancario - CCI
    /// </summary>
    public string CodigoCuentaInterbancario { get; private set; }
    /// <summary>
    /// Código de cliente 
    /// </summary>
    public string CodigoCliente { get; private set; }
    /// <summary>
    /// Id de la entidad financiera
    /// </summary>
    public int IDEntidadFinanciera { get; private set; }
    /// <summary>
    /// Código del tipo de documento del beneficiario
    /// </summary>
    public string CodigoTipoDocumento { get; private set; }
    /// <summary>
    /// Número de documento del beneficiario
    /// </summary>
    public string NumeroDocumento { get; private set; }
    /// <summary>
    /// Ordenante de la transferencia
    /// </summary>
    public string Ordenante { get; private set; }
    /// <summary>
    /// Nombres del beneficiario
    /// </summary>
    public string Nombres { get; private set; }
    /// <summary>
    /// Apellido paterno del beneficiario
    /// </summary>
    public string ApellidoPaterno { get; private set; }
    /// <summary>
    /// Apellido materno del beneficiario
    /// </summary>
    public string ApellidoMaterno { get; private set; }
    /// <summary>
    /// Indicador si es el mismo titular
    /// </summary>
    public string MismoTitular { get; private set; }
    /// <summary>
    /// Monto de la operación
    /// </summary>
    public decimal MontoOperacion { get; private set; }
    /// <summary>
    /// Codigo del tarifario a cobrar
    /// </summary>
    public string CodigoTarifario { get; private set; }
    /// <summary>
    /// Monto de la comisión a cobrar
    /// </summary>
    public decimal MontoComision { get; private set; }
    /// <summary>
    /// Indicador del estado
    /// </summary>
    public string EstaActivo { get; private set; }
    /// <summary>
    /// Entidad virutal de cabcera Transferencia
    /// </summary>
    public virtual Transferencia Transferencia { get; private set; }

    #endregion

    #region Metodos
    /// <summary>
    /// Método que crea la transferencia cce
    /// </summary>
    /// <param name="transferencia"></param>
    /// <param name="numeroCuentaInterbancaria"></param>
    /// <param name="codigoCliente"></param>
    /// <param name="EntidadOrdenanteCCE"></param>
    /// <param name="codigoTipoDocumento"></param>
    /// <param name="numeroDocunento"></param>
    /// <param name="ordenante"></param>
    /// <param name="indicadorMismoTitular"></param>
    /// <param name="montoOperacion"></param>
    /// <param name="codigoTarifario"></param>
    /// <param name="montoComision"></param>
    /// <returns></returns>
    public static TransferenciaDetalleEntranteCCE Crear(
        Transferencia transferencia,
        string numeroCuentaInterbancaria,
        string codigoCliente,
        EntidadFinancieraInmediata EntidadOrdenanteCCE,
        string codigoTipoDocumento,
        string numeroDocunento,
        string ordenante,
        string indicadorMismoTitular,
        decimal montoOperacion,
        string? codigoTarifario,
        decimal montoComision)
    {
        return new TransferenciaDetalleEntranteCCE()
        {
            NumeroTransferencia = transferencia.NumeroTransferencia,
            Transferencia = transferencia,
            CodigoCuentaInterbancario = numeroCuentaInterbancaria,
            CodigoCliente = codigoCliente,
            CodigoTipoDocumento = codigoTipoDocumento,
            IDEntidadFinanciera = EntidadOrdenanteCCE.IdentificadorEntidad,
            NumeroDocumento = numeroDocunento,
            Ordenante = ordenante,
            MismoTitular = indicadorMismoTitular,
            MontoOperacion = montoOperacion,
            CodigoTarifario = codigoTarifario,
            MontoComision = montoComision,
            EstaActivo = General.Si,
        };
    }
    /// <summary>
    /// Metodo que incluye los datos personales de natural
    /// </summary>
    /// <param name="nombre">Nombre</param>
    /// <param name="apellidoParterno">Apellido Paterno</param>
    /// <param name="apellidoMaterno">Apellido Materno</param>
    public void IncluirDatosPersonaNatural(
        string nombre, string apellidoParterno, string apellidoMaterno
        )
    {
        Nombres = nombre;
        ApellidoPaterno = apellidoParterno;
        ApellidoMaterno = apellidoMaterno;
    }

    #endregion
}

