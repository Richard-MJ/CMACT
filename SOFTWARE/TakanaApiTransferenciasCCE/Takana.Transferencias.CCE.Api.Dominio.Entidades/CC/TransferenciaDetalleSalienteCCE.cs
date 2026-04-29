using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
/// <summary>
/// Entidad del detalle de la transferencia CCE
/// </summary>
public class TransferenciaDetalleSalienteCCE
{
    #region Propiedades
    /// <summary>
    /// Número de transferencia
    /// </summary>
    public int NumeroTransferencia { get; private set; }
    /// <summary>
    /// Número del detalle de transferencia
    /// </summary>
    public int NumeroDetalle { get; private set; }
    /// <summary>
    /// Código de cuenta interbancario - CCI
    /// </summary>
    public string CodigoCuentaInterbancario { get; private set; }
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
    /// Beneficiario de la transferencia
    /// </summary>
    public string Beneficiario { get; private set; }
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
    /// Indicador si requiere confirmación
    /// </summary>
    public string RequiereConfirmacion { get; private set; }
    /// <summary>
    /// Monto de la operación
    /// </summary>
    public decimal MontoOperacion { get; private set; }
    /// <summary>
    /// Periodo del pago de CTS
    /// </summary>
    public string PeriodoPagoCTS { get; private set; }
    /// <summary>
    /// Monto de remuneraciones
    /// </summary>
    public decimal MontoRemuneraciones { get; private set; }
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
    /// Propiedad virtual de la entidad financiera de destino
    /// </summary>
    public virtual EntidadFinancieraDiferida EntidadDestino { get; private set; }
    /// <summary>
    /// Entidad virutal de cabcera Transferencia
    /// </summary>
    public virtual Transferencia Transferencia { get; private set; }

    #endregion

    #region Metodos
    /// <summary>
    /// Crea el detalle de transferencia saliente
    /// </summary>
    /// <param name="transferencia">Datos de transferencia</param>
    /// <param name="numeroDetalle">Numero de detalle</param>
    /// <param name="numeroCuentaInterbancaria">Numero CCI</param>
    /// <param name="idEntidadFinancieraDestino">Identificador de entidad destino</param>
    /// <param name="entidadDestino">Datos de entidad destino</param>
    /// <param name="codigoTipoDocumento">Codigo de tipo documento</param>
    /// <param name="numeroDocumento">Numero de documento</param>
    /// <param name="beneficiario">Beneficiario</param>
    /// <param name="indicadorMismoTitular">Indicador de mismo titular</param>
    /// <param name="montoOperacion">Monto de operacion</param>
    /// <param name="codigoTarifario">Codigo de tarifario</param>
    /// <param name="montoComision">Monto de comision</param>
    /// <returns>Retorna el detalle de transferencia</returns>
    public static TransferenciaDetalleSalienteCCE Crear(
           Transferencia transferencia,
           int numeroDetalle,
           string numeroCuentaInterbancaria,
           int idEntidadFinancieraDestino,
           EntidadFinancieraDiferida entidadDestino,
           string codigoTipoDocumento,
           string numeroDocumento,
           string beneficiario,
           string indicadorMismoTitular,
           decimal montoOperacion,
           string codigoTarifario,
           decimal montoComision)
    {
        return new TransferenciaDetalleSalienteCCE()
        {
            NumeroTransferencia = transferencia.NumeroTransferencia,
            Transferencia = transferencia,
            NumeroDetalle = numeroDetalle,
            CodigoCuentaInterbancario = numeroCuentaInterbancaria,
            CodigoTipoDocumento = codigoTipoDocumento,
            IDEntidadFinanciera = idEntidadFinancieraDestino,
            EntidadDestino = entidadDestino,
            NumeroDocumento = numeroDocumento,
            Beneficiario = beneficiario,
            MismoTitular = indicadorMismoTitular,
            RequiereConfirmacion = "0",
            MontoOperacion = montoOperacion,
            PeriodoPagoCTS = "000",
            MontoRemuneraciones = 0,
            CodigoTarifario = codigoTarifario,
            MontoComision = montoComision,
            EstaActivo = General.Si,
            Nombres = beneficiario
        };
    }
    #endregion
}

