namespace Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;

/// <summary>
/// Clase de constantes de valores fijos requeridos por la CCE segun documentacion
/// </summary>
public class DatosValoresFijos
{
    /// <summary>
    /// Tipo de proxy para QR, valor fijo CCE O
    /// </summary>
    public const string TipoProxy = "O";
    /// <summary>
    /// Codigo de respuesta exitosa QR
    /// </summary>
    public const string RespuestaExitosaQR = "0";

    #region Barrido de Contactos
    /// <summary>
    /// Identificador de barrido, valor fijo CCE
    /// </summary>
    public const string IdBarrido = "0998";
    /// <summary>
    /// Identificador de trama de barrido para la CCE
    /// </summary>
    public const string MsgDefIdrBarrido = "prxy.003.001.01";
    /// <summary>
    /// Nombre de barrido, valor fijo CCE
    /// </summary>
    public const string Barrido = "BARRIDO";
    /// <summary>
    /// LookUp de barrido, valor fijo CCE
    /// </summary>
    public const string LkUpTpBarrido = "PXSR";
    /// <summary>
    /// Tipo de barrido de contactos, valor fijo CCE
    /// </summary>
    public const string TypeBarrido = "M";
    /// <summary>
    /// Valor dummy segun documento de la CCE, valor fijo CCE
    /// </summary>
    public const string ValorDummy = "+51999999999";
    /// <summary>
    /// Tipo de barrido lista, valor fijo CCE
    /// </summary>
    public const string TpBarrido = "LIST";
    /// <summary>
    /// Codigo de verificacion de barrido de contactos
    /// </summary>
    public const string ValorCuentaVerificacion = "8";
    /// <summary>
    /// Valor request, valor fijo CCE
    /// </summary>
    public const string ValorRequest = "1";
    #endregion Barrido de Contactos

    #region Registro Directorio
    /// <summary>
    /// Identificador de Registro de directorio CCE, valor fijo CCE
    /// </summary>
    public const string IdRegistro = "0998";
    /// <summary>
    /// valor fijo CCE de cabeera para la CCE, valor fijo CCE 
    /// </summary>
    public const string ValorIdCabeceraRegistro = "B";
    /// <summary>
    /// Requerimiento de registro, valor fijo CCE 
    /// </summary>
    public const string RequerimientoRegistro = "1";
    /// <summary>
    /// Valor de documento, valor fijo CCE 
    /// </summary>
    public const string ValorIdDocumentoRegistro = "M";
    /// <summary>
    /// Codigo nuevo registro
    /// </summary>
    public const string NuevoRegistro = "NEWR";
    /// <summary>
    /// Coidgo modificar registro
    /// </summary>
    public const string ModificarRegistro = "AMND";
    /// <summary>
    /// Codigo eliminar registro
    /// </summary>
    public const string EliminarRegistro = "DEAC";
    /// <summary>
    /// Identificador documento, valor fijo
    /// </summary>
    public const string ValorIdDocumentoIdRegistro = "R";
    /// <summary>
    /// Identificador trama registro, valor fijo
    /// </summary>
    public const string MsgDefIdrRegistro = "prxy.001.001.01";
    #endregion Registro Directorio

    #region Generacion QR
    /// <summary>
    /// QR estatico
    /// </summary>
    public const string QrEstatico = "11";
    /// <summary>
    /// QR dinamico
    /// </summary>
    public const string QrDinamico = "12";
    /// <summary>
    /// Tipo de QR formato texto
    /// </summary>
    public const string tipoQrText = "TEXT";
    /// <summary>
    /// TIpo de QR formato imagen
    /// </summary>
    public const string tipoQrBase64 = "BASE64";
    /// <summary>
    /// Tipo de QR EMV
    /// </summary>
    public const string tipoQrEmv = "EMV";
    /// <summary>
    /// Valor por defecto codigo comerciante
    /// </summary>
    public const string ValorPorDefectoCodigoComerciante = "4829";
    #endregion Generacion QR

}
