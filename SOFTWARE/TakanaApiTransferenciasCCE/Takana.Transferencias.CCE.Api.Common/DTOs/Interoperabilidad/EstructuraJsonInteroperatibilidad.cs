namespace Takana.Transferencias.CCE.Api.Common.Interoperatibilidad;

/// <summary>
/// Clase encargada de representar la estructura de datos que la CCE requiere con jerarquia y nombres espeficicos
/// </summary>
public class EstructuraJsonInteroperatibilidad
{
    #region Barrido de Contactos – Tramo 1 (prxy.003.001.01)

    public class Othr
    {
        public string Id { get; set; }
    }

    public class FinInstnId
    {
        public string Nm { get; set; }
        public Othr Othr { get; set; }
    }

    public class FIId
    {
        public FinInstnId FinInstnId { get; set; }
    }

    public class Fr
    {
        public FIId FIId { get; set; }
    }

    public class To
    {
        public FIId FIId { get; set; }
    }

    public class AppHdr
    {
        public To To { get; set; }
        public Fr Fr { get; set; }
        public string BizMsgIdr { get; set; }
        public string MsgDefIdr { get; set; }
        public string CreDt { get; set; }
    }
    public class Agt
    {
        public FinInstnId FinInstnId { get; set; }
    }

    public class MsgSndr
    {
        public Agt Agt { get; set; }

    }

    public class GrpHdr
    {
        public string MsgId { get; set; }
        public string CreDtTm { get; set; }
        public MsgSndr MsgSndr { get; set; }
        public MsgRcpt MsgRcpt { get; set; }
    }

    public class PrxyLookUp
    {
        public GrpHdr GrpHdr { get; set; }
        public LookUp LookUp { get; set; }
        public SplmtryData SplmtryData { get; set; }
    }
    public class PrxyRtrvl
    {
        public string Tp { get; set; }
        public string Val { get; set; }
    }
    public class PrxyOnly
    {
        public string LkUpTp { get; set; }
        public string Id { get; set; }
        public PrxyRtrvl PrxyRtrvl { get; set; }
    }
    public class Proxy
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string Present { get; set; }
        public List<string> BankCode { get; set; }
    }
    public class Envlp
    {
        public Proxy[] Proxy { get; set; }
        public Directories[] Directories { get; set; }
    }
    public class SplmtryData
    {
        public string PlcAndNm { get; set; }
        public Envlp Envlp { get; set; }
    }
    public class LookUp
    {
        public PrxyOnly PrxyOnly { get; set; }
    }

    public class Document
    {
        public PrxyLookUp PrxyLookUp { get; set; }
        public PrxyLookUpRspn PrxyLookUpRspn { get; set; }
        public PrxyRegn PrxyRegn { get; set; }
        public PrxyRegnRspn PrxyRegnRspn { get; set; }
        public MessageReject MessageReject { get; set; }
        public SplmtryData SplmtryData { get; set; }

    }
    #endregion Barrido de Contactos – Tramo 1 (prxy.003.001.01)

    #region Barrido de Contactos – Tramo 2 (prxy.004.001.01)

    public class RltdRef
    {
        public string Ref { get; set; }
    }
    public class Rsn
    {
        public string RjctgPtyRsn { get; set; }
        public string RjctnDtTm { get; set; }
        public string ErrLctn { get; set; }
        public string RsnDesc { get; set; }
        public string AddtlData { get; set; }

    }
    public class MessageReject
    {
        public RltdRef RltdRef { get; set; }
        public Rsn Rsn { get; set; }
    }
    public class MsgRcpt
    {
        public Agt Agt { get; set; }
    }
    public class PrxyLookUpRspn
    {
        public GrpHdr GrpHdr { get; set; }
        public OrgnlGrpInf OrgnlGrpInf { get; set; }
        public LkUpRspn LkUpRspn { get; set; }
        public SplmtryData SplmtryData { get; set; }
    }

    public class OrgnlGrpInf
    {
        public string OrgnlMsgId { get; set; }
        public string OrgnlMsgNmId { get; set; }
        public string OrgnlCreDtTm { get; set; }
    }
    public class OrgnlPrxyRtrvl
    {
        public string Tp { get; set; }
        public string Val { get; set; }
    }
    public class LkUpRspn
    {
        public string OrgnlId { get; set; }
        public OrgnlPrxyRtrvl OrgnlPrxyRtrvl { get; set; }
        public RegnRspn RegnRspn { get; set; }
    }
    public class Prxy
    {
        public string Tp { get; set; }
        public string Val { get; set; }
    }
    public class StsRsnInf
    {
        public string Prtry { get; set; }
    }

    public class OrgnlPrxy
    {
        public string Tp { get; set; }
        public string Val { get; set; }
    }

    public class RegnRspn
    {
        public string OrgnlId { get; set; }
        public string PrxRspnSts { get; set; }
        public string OrgnlRegnTp { get; set; }
        public string OrgnlRegnSubTp { get; set; }
        public StsRsnInf StsRsnInf { get; set; }
        public Prxy Prxy { get; set; }
        public OrgnlPrxy OrgnlPrxy { get; set; }
    }
    public class Directories
    {
        public string Directory { get; set; }
        public Proxy[] Proxy { get; set; }
    }
    #endregion Barrido de Contactos – Tramo 2 (prxy.004.001.01)

    #region Clases Principales Barrido

    public class BusMsg
    {
        public AppHdr AppHdr { get; set; }
        public Document Document { get; set; }
    }
    public class EstructuraBarridoContacto
    {
        public BusMsg BusMsg { get; set; }
    }
    #endregion Clases Principales Barrido

    #region  Registro Directorio Request (prxy.001.001.01) 
    public class Acct
    {
        public string AcctHldrTp { get; set; }
    }

    public class PrxyRegn
    {
        public string RegnId { get; set; }
        public string DsplNm { get; set; }
        public GrpHdr GrpHdr { get; set; }
        public Regn Regn { get; set; }
        public Acct Acct { get; set; }
        public SplmtryData SplmtryData { get; set; }
    }

    public class Regn
    {
        public string RegnTp { get; set; }
        public string RegnSubTp { get; set; }
        public Prxy Prxy { get; set; }
        public GrpHdr GrpHdr { get; set; }
        public PrxyRegn PrxyRegn { get; set; }
    }
    public class PrxyRegnRspn
    {
        public GrpHdr GrpHdr { get; set; }
        public OrgnlGrpInf OrgnlGrpInf { get; set; }
        public RegnRspn RegnRspn { get; set; }
        public SplmtryData SplmtryData { get; set; }
        public PrxyRegn PrxyRegn { get; set; }

    }

    #endregion  Registro Directorio Request (prxy.001.001.01)

    #region Clases Principales Registro Directorio

    public class EstructuraRegistroDirectorio
    {
        public BusMsg BusMsg { get; set; }
    }
    #endregion Clases Principales Registro Directorio

    #region Pagos QR
    /// <summary>
    /// Clase encargada de representar los datos del encabezado de envío, que incluye el usuario.
    /// </summary>
    public class headerEnvio
    {
        /// <summary>
        /// Usuario que realiza el envío.
        /// </summary>
        public string user { get; set; }
    }

    /// <summary>
    /// Clase encargada de representar los datos de respuesta del encabezado, incluyendo un código y texto de retorno.
    /// </summary>
    public class headerRespuesta
    {
        /// <summary>
        /// Código de retorno de la respuesta.
        /// </summary>
        public string codReturn { get; set; }

        /// <summary>
        /// Texto de retorno de la respuesta.
        /// </summary>
        public string txtReturn { get; set; }
    }

    /// <summary>
    /// Clase encargada de representar la información adicional sobre un QR, incluyendo código y valor.
    /// </summary>
    public class info
    {
        /// <summary>
        /// Código asociado a la información del QR.
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// Valor asociado a la información del QR.
        /// </summary>
        public string valor { get; set; }
    }

    /// <summary>
    /// Clase encargada de representar los datos de un QR, incluyendo tipo, cuenta, moneda, y nombre del comerciante.
    /// </summary>
    public class data
    {
        /// <summary>
        /// Tipo de QR.
        /// </summary>
        public string qrTipo { get; set; }

        /// <summary>
        /// Identificador de la cuenta asociada al QR.
        /// </summary>
        public string idCuenta { get; set; }

        /// <summary>
        /// Moneda asociada al QR.
        /// </summary>
        public string moneda { get; set; }

        /// <summary>
        /// Nombre del comerciante asociado al QR.
        /// </summary>
        public string nombreComerciante { get; set; }
    }

    /// <summary>
    /// Clase encargada de representar un EMV (electronic monetary value) dentro de un QR, incluyendo su etiqueta, longitud y valor.
    /// </summary>
    public class emv
    {
        /// <summary>
        /// Etiqueta del EMV.
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Longitud del EMV.
        /// </summary>
        public string longitud { get; set; }

        /// <summary>
        /// Valor asociado al EMV.
        /// </summary>
        public string valor { get; set; }
    }

    /// <summary>
    /// Clase encargada de representar la estructura completa de un QR, con diversos detalles como emisor, importe, moneda, etc.
    /// </summary>
    public class qr
    {
        /// <summary>
        /// Tipo de QR.
        /// </summary>
        public string? qrTipo { get; set; }

        /// <summary>
        /// Emisor del QR.
        /// </summary>
        public string emisor { get; set; }

        /// <summary>
        /// Identificador de la cuenta asociada al QR.
        /// </summary>
        public string? idCuenta { get; set; }

        /// <summary>
        /// Identificador del QR.
        /// </summary>
        public string? idQr { get; set; }

        /// <summary>
        /// Importe asociado al QR.
        /// </summary>
        public string? importe { get; set; }

        /// <summary>
        /// Moneda utilizada en el QR.
        /// </summary>
        public string? moneda { get; set; }

        /// <summary>
        /// Cantidad de pagos relacionados con el QR.
        /// </summary>
        public string? cantidadPagos { get; set; }

        /// <summary>
        /// Fecha de vencimiento del QR.
        /// </summary>
        public string? fechaVencimiento { get; set; }

        /// <summary>
        /// Glosa asociada al QR.
        /// </summary>
        public string? glosa { get; set; }

        /// <summary>
        /// Nombre del comerciante asociado al QR.
        /// </summary>
        public string? nombreComerciante { get; set; }

        /// <summary>
        /// Fecha de registro del QR.
        /// </summary>
        public string? fechaRegistro { get; set; }

        /// <summary>
        /// Lista de EMVs asociados al QR.
        /// </summary>
        public emv[]? emv { get; set; }

        /// <summary>
        /// Lista de información adicional asociada al QR.
        /// </summary>
        public info[]? info { get; set; }
    }

    /// <summary>
    /// Estructura para obtener el token, incluye el código de la solicitud.
    /// </summary>
    public class EstructuraObtenerToken
    {
        /// <summary>
        /// Código de la solicitud para obtener el token.
        /// </summary>
        public string code { get; set; }
    }

    /// <summary>
    /// Respuesta al obtener el token, incluye el encabezado y el token generado.
    /// </summary>
    public class EstructuraObtenerTokenRespuesta : EstructuraObtenerToken
    {
        /// <summary>
        /// Encabezado con los datos de respuesta.
        /// </summary>
        public headerRespuesta header { get; set; }

        /// <summary>
        /// Token generado en la respuesta.
        /// </summary>
        public string token { get; set; }
    }

    /// <summary>
    /// Estructura para generar un QR, incluye los datos del encabezado, tipo de datos y el QR.
    /// </summary>
    public class EstructuraGenerarQR
    {
        /// <summary>
        /// Encabezado con los datos del envío.
        /// </summary>
        public headerEnvio header { get; set; }

        /// <summary>
        /// Datos específicos para generar el QR.
        /// </summary>
        public data data { get; set; }

        /// <summary>
        /// Tipo de la solicitud para generar el QR.
        /// </summary>
        public string type { get; set; }
    }

    /// <summary>
    /// Respuesta de la generación de un QR, incluye el encabezado, hash y el ID del QR.
    /// </summary>
    public class EstructuraGenerarQRRespuesta
    {
        /// <summary>
        /// Encabezado con los datos de la respuesta.
        /// </summary>
        public headerRespuesta header { get; set; }

        /// <summary>
        /// Hash asociado al QR generado.
        /// </summary>
        public string hash { get; set; }

        /// <summary>
        /// Identificador del QR generado.
        /// </summary>
        public string idQr { get; set; }
    }

    /// <summary>
    /// Estructura para leer un QR, incluye el encabezado y el hash del QR.
    /// </summary>
    public class EstructuraLeerQR
    {
        /// <summary>
        /// Encabezado con los datos del envío.
        /// </summary>
        public headerEnvio header { get; set; }

        /// <summary>
        /// Hash del QR para leer.
        /// </summary>
        public string hash { get; set; }
    }

    /// <summary>
    /// Estructura para consultar los datos de un QR, incluye el encabezado, hash y los detalles del QR.
    /// </summary>
    public class EstructuraConsultaDatosQR
    {
        /// <summary>
        /// Encabezado con los datos de la respuesta.
        /// </summary>
        public headerRespuesta header { get; set; }

        /// <summary>
        /// Hash asociado al QR.
        /// </summary>
        public string hash { get; set; }

        /// <summary>
        /// Datos completos del QR.
        /// </summary>
        public qr qr { get; set; }

        /// <summary>
        /// Información adicional del QR.
        /// </summary>
        public info info { get; set; }
    }

    /// <summary>
    /// Estructura para obtener los datos de un QR, incluye los detalles del encabezado, cuenta y QR.
    /// </summary>
    public class EstructuraObtenerDatosQR
    {
        /// <summary>
        /// Encabezado con los datos del envío.
        /// </summary>
        public headerEnvio header { get; set; }

        /// <summary>
        /// Identificador de la cuenta asociada al QR.
        /// </summary>
        public string idCuenta { get; set; }

        /// <summary>
        /// Identificador del QR.
        /// </summary>
        public string idQr { get; set; }
    }

    /// <summary>
    /// Respuesta al obtener los datos de un QR, incluye los detalles del encabezado, cuenta, QR e información.
    /// </summary>
    public class EstructuraObtenerDatosQRRespuesta
    {
        /// <summary>
        /// Encabezado con los datos de la respuesta.
        /// </summary>
        public headerRespuesta header { get; set; }

        /// <summary>
        /// Identificador de la cuenta asociada al QR.
        /// </summary>
        public string idCuenta { get; set; }

        /// <summary>
        /// Identificador del QR.
        /// </summary>
        public string idQr { get; set; }

        /// <summary>
        /// Datos completos del QR.
        /// </summary>
        public qr qr { get; set; }
    }
    #endregion Pagos QR
}
