namespace Takana.Transferencias.CCE.Api.Common.Constantes
{
    /// <summary>
    /// Clase que representa los datos de la audiencia.
    /// </summary>
    public static class General
    {
        #region Lavado
        /// <summary>
        /// Modalidad de otros medio no presenciales
        /// </summary>
        public const int ModalidadOtrosMediosNoPresenciales = 2;
        /// <summary>
        /// Submodalidad de cajero de CCE
        /// </summary>
        public const int SubModalidadCajeroCCE = 10;
        /// <summary>
        /// Submodalidad de APPMOVIL
        /// </summary>
        public const int SubModalidadAppMovil = 14;
        /// <summary>
        /// Forma de pago cuenta corriente
        /// </summary>
        public const string FormaPagoCuentaCorriente = "9";
        #endregion

        #region Estados
        /// <summary>
        /// Indicador Activo
        /// </summary>
        public const string Activo = "A";
        /// <summary>
        /// Indicador Inactivo
        /// </summary>
        public const string Inactivo = "I";
        /// <summary>
        /// Indicador Activo
        /// </summary>
        public const string Habilitado = "H";
        #endregion

        #region Estados CCE
        /// <summary>
        /// Codigo de Estado de la Entidad SIGN ON/OFF
        /// </summary>
        public const string DescripcionSignOn = "SIGNON";
        /// <summary>
        /// Indicador Sign On
        /// </summary>
        public const string SignOn = "H";
        /// <summary>
        /// Indicador Sign Off
        /// </summary>
        public const string SignOff = "I";
        /// <summary>
        /// Codigo de Estado del Sistema de Transferencia Inmediata de la CCE
        /// </summary>
        public const string Disponible = "D";
        /// <summary>
        /// Indicador estado suspendido
        /// </summary>
        public const string Suspendido = "S";
        /// <summary>
        /// Codigo de Estado de la CCE hacia una entidad
        /// </summary>
        public const string DescripcionNormal = "NORMAL";
        /// <summary>
        /// Estado normalizado
        /// </summary>
        public const string Normalizado = "N";
        /// <summary>
        /// Estado bloqueado
        /// </summary>
        public const string Bloqueado = "B";

        #endregion

        #region Indicador Tipo
        /// <summary>
        /// Cliente originante
        /// </summary>
        public const string Originante = "O";
        /// <summary>
        /// Cliente receptor
        /// </summary>
        public const string Receptor = "R";
        /// <summary>
        /// Indica la misma plaza
        /// </summary>
        public const string MismaPlaza = "M";
        /// <summary>
        /// Indica que es otra plaza
        /// </summary>
        public const string OtraPlaza = "O";
        /// <summary>
        /// Indica que es plaza exclusiva
        /// </summary>
        public const string PlazaExclusiva = "E";
        #endregion

        #region Estados Trama
        /// <summary>
        /// Estado Aceptado general
        /// </summary>
        public const string Aceptado = "A";
        /// <summary>
        /// Estado Pendiente general
        /// </summary>
        public const string Pendiente = "P";
        /// <summary>
        /// Estado Confirmado general
        /// </summary>
        public const string Confirmado = "C";
        /// <summary>
        /// Estado Finalizado general
        /// </summary>
        public const string Finalizado = "F";
        /// <summary>
        /// Estado Rechazo general
        /// </summary>
        public const string Rechazo = "R";
        /// <summary>
        /// Estado NoEncontrado general
        /// </summary>
        public const string NoEncontrado = "N";
        #endregion

        #region Constantes
        /// <summary>
        /// Codigo de parametro de correo electronico de canales
        /// </summary>
        public const string EmailCanales = "EMAIL_CANALES";
        /// <summary>
        /// usuario por defecto de cajero
        /// </summary>
        public const string UsuarioPorDefecto = "CAJEROCCE";
        /// <summary>
        /// usuario por defecto de cajero
        /// </summary>
        public const string UsuarioPorDefectoInteroperabilidad = "APP_MOVIL";
        /// <summary>
        /// Canal
        /// </summary>
        public const string CanalVentanilla = "T";
        /// <summary>
        /// Indicador genera SI
        /// </summary>
        public const string Si = "S";
        /// <summary>
        /// Indicador genera NO
        /// </summary>
        public const string No = "N";
        /// <summary>
        /// Canal de transferencia inmediata
        /// </summary>
        public const string CanalCCE = "E";
        /// <summary>
        /// Valor para convertir en segundos
        /// </summary>
        public const int ValorConvetirEnSegundos = 1000;
        /// <summary>
        /// Cantidad de caracteres CCI
        /// </summary>
        public const int LongitudCCI = 20;
        /// <summary>
        /// Valor 0
        /// </summary>
        public const string Cero = "0";
        /// <summary>
        /// Canal APP
        /// </summary>
        public const string APP = "A";
        /// <summary>
        /// Canal APP
        /// </summary>
        public const string Ventanilla = "T";
        /// <summary>
        /// Descripcion que la consulta es por QR
        /// </summary>
        public const string DescripcionConsultaPorQR = "CONSULTA POR QR";
        /// <summary>
        /// Código parámetro Moneda Base
        /// </summary>
        public const string MonedaBase = "MONEDA_BASE";
        #endregion

        #region Titular
        /// <summary>
        /// Mismo titular version CCE
        /// </summary>
        public const string MismoTitular = "M";
        /// <summary>
        /// Otro titular version CCE
        /// </summary>
        public const string OtroTitular = "O";
        #endregion

        #region SingOnOff
        /// <summary>
        /// Identificador de Entidad Caja Tacna
        /// </summary>
        public const int IndentificadorEntidad = 37;
        /// <summary>
        /// Codigo Entidad
        /// </summary>
        public const string CodigoEntidad = "0813";
        #endregion
    }
}
