
namespace Takana.Transferencias.CCE.Api.Common.Constantes
{
    public enum CatalogoTransaccionEnum 
    {
        /// <summary>
        /// Constante Crediot por transferencia de CC
        /// </summary>
        CodigoTransferenciaInmediataSaliente = 40,
        /// <summary>
        /// Constante débito por transferencia de CC
        /// </summary>
        CodigoTransferenciaInmediataEntrante = 41,
        /// <summary>
        /// Constante Crediot por transferencia de CC
        /// </summary>
        CodigoTransferenciaInmediataComision = 32,
        /// <summary>
        /// Constante para aplicar ITF en transferencia de CC
        /// </summary>
        CodigoTransaccionCargoITF = 203,
        /// <summary>
        /// Constante para afiliaciones de TJ
        /// </summary>
        CodigoTransaccionAfiliacion = 10,
        /// <summary>
        /// Constante para deposito de cuenta efectivo de CJ
        /// </summary>
        CodigoDepositoCuentaEfectivo = 5,
    }

    public enum TipoDocumentoEnum
    {
        /// <summary>
        /// Numero identificador de DNI
        /// </summary>
        DNI =1,
        /// <summary>
        /// Numero identificador de CarnetExtranjeria
        /// </summary>
        CarnetExtranjeria = 2,
        /// <summary>
        /// Numero identificador de Pasaporte
        /// </summary>
        Pasaporte = 5,
        /// <summary>
        /// Numero identificador de RUC
        /// </summary>
        RUC = 12,
        /// <summary>
        /// Otro Documento
        /// </summary>
        OtroDocumento = 17
    }

    public enum SubTipoTransaccionEnum
    {
        /// <summary>
        /// Constante de código de sub transferencia Inmediata interbancaria CCE
        /// </summary>
        TransaccionInmediataOrdinariaSalida = 13,
        /// <summary>
        /// Constante de código de sub transferencia´por interoperabilidad
        /// </summary>
        TransaccionInteroperabilidad = 14,
        /// <summary>
        /// Constante de codigo de sub transferencia inmediata por tarjeta de credito
        /// </summary>
        TransaccionInmediataTarjetaSalida = 16,
        /// <summary>
        /// Constante de código de transferencia interbancaria CCE
        /// </summary>
        CodigoTransferenciaInmediataEntrante = 12,
        /// <summary>
        /// Constante de código de devolución de transferencia interbancaria CCE
        /// </summary>
        CodigoDevolucionTransferenciaEntrante = 14,
        /// <summary>
        /// Constante de codigo de cargo ITF
        /// </summary>
        CodigoTransaccionCargoITF = 1,
        /// <summary>
        /// Constante de codigo de cargo Comision
        /// </summary>
        CodigoTransaccionCargoComision = 127,
        /// <summary>
        /// Constate de afiliación de billetera virtual
        /// </summary>
        AfiliacionBilleteraVirtual = 14,
        /// <summary>
        /// Constante de desafiliación de billetera virtual
        /// </summary>
        DesafiliacionBilleteraVirtual = 15,
        /// <summary>
        /// Constante para deposito de cuenta efectivo de CJ
        /// </summary>
        CodigoDepositoGeneralCuentaEfectivo = 1,
        /// <summary>
        /// Constante de modificación de notificaciones de interoperatibilidad
        /// </summary>
        ConfiguracionNotificacionInteroperatibilidad = 16,
    }


    /// <summary>
    /// Enum de los identificadores de las propiedades para los detalles
    /// </summary>
    public enum IdentificadorPropiedadDetalle : int
    {
        CuentaDestinoCci = 4,
        NombreDestino = 5,
        MismoTitularEnDestino = 6,
        TipoDocumento = 7,
        NumeroDocumento = 8,
    }
    /// <summary>
    /// Enum de los identificadores de canales
    /// </summary>
    public enum CanalEnum : int
    {
        Ventanilla = 90,
        Interoperabilidad = 52,
        AppMovil = 91,
        HomeBanking = 15
    }
}
