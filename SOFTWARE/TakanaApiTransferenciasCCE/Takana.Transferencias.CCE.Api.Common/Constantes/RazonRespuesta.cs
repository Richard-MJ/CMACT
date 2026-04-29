namespace Takana.Transferencias.CCE.Api.Common.Constantes
{
    public static class RazonRespuesta
    {
        #region Constantes
        /// <summary>
        /// Indica que no existen ninguna error en la validacion
        /// </summary>
        public const string codigo0000 = "0000";
        /// <summary>
        /// Número de Cuenta Incorrecto
        /// </summary>
        public const string codigoAC01 = "AC01";
        /// <summary>
        /// Cuenta Bloqueada
        /// </summary>
        public const string codigoAC06 = "AC06";
        /// <summary>
        /// Cuenta a Acreditar Cerrada
        /// </summary>
        public const string codigoAC07 = "AC07";
        /// <summary>
        /// Moneda de la Cuenta a Acreditar Inválida
        /// </summary>
        public const string codigoAC11 = "AC11";
        /// <summary>
        /// El tipo de transferencia enviada no es permitida para la cuenta destino
        /// </summary>
        public const string codigoAC14 = "AC14";
        /// <summary>
        /// Operación no Soportada
        /// </summary>
        public const string codigoFF07 = "FF07";
        /// <summary>
        /// Monto Cero
        /// </summary>
        public const string codigoAM01 = "AM01";
        /// <summary>
        /// Duplicado
        /// </summary>
        public const string codigoAM05 = "AM05";
        /// <summary>
        /// Monto Equivocado
        /// </summary>
        public const string codigoAM09 = "AM09";
        /// <summary>
        /// Nombre de Cliente Originante
        /// </summary>
        public const string codigoBE08 = "BE08";
        /// <summary>
        /// ID de Referencia Requerido
        /// </summary>
        public const string codigoBE15 = "BE15";
        /// <summary>
        /// Código de Identificación de Originante Inválido
        /// </summary>
        public const string codigoBE16 = "BE16";
        /// <summary>
        /// Identificador de Cliente Receptor Incorrecto
        /// </summary>
        public const string codigoCH11 = "CH11";
        /// <summary>
        /// Entidad Originante no Registrada
        /// </summary>
        public const string codigoDNOR = "DNOR";
        /// <summary>
        /// Firma de Datos Requerida
        /// </summary>
        public const string codigoDS0A = "DS0A";
        /// <summary>
        ///  Formato de Firma Desconocido
        /// </summary>
        public const string codigoDS0B = "DS0B";
        /// <summary>
        /// Certificado de Firma Inválido
        /// </summary>
        public const string codigoDS0D = "DS0D";
        /// <summary>
        /// Versión de Llave Pública Incorrecta 
        /// </summary>
        public const string codigoDS16 = "DS16";
        /// <summary>
        /// Error de formato
        /// </summary>
        public const string codigoFF02 = "FF02";
        /// <summary>
        /// Motivo Regulatorio (políticas de AML/CFT)
        /// </summary>
        public const string codigoRR04 = "RR04";
        /// <summary>
        /// Set de Caracteres Inválido
        /// </summary>
        public const string codigoRR10 = "RR10";
        /// <summary>
        /// Error en Firma de Mensaje
        /// </summary>
        public const string codigoERRFIRMA = "ERRFIRMA";
        /// <summary>
        /// Error interno de CMACT
        /// </summary>
        public const string codigoERRCMACT = "ERRCMACT";
        /// <summary>
        /// Canal de Pin Verify invalido
        /// </summary>
        public const string codigoERR1 = "ERR1";
        /// <summary>
        /// Error de Conexión con el Pin Verify o HSM
        /// </summary>
        public const string codigoERR2 = "ERR2";
        /// <summary>
        /// Mensaje de Firma no valida.
        /// </summary>
        public const string codigoERR3 = "ERR3";
        /// <summary>
        /// Funcion jwtf invalida
        /// </summary>
        public const string codigoERR4 = "ERR4";
        /// <summary>
        /// Codigo de error cuando ocurre un incoveniente al enviar la petición de consulta
        /// </summary>
        public const string codigoERRCONSUL = "ERRCONSUL";

        #endregion Constantes      

    }
}
