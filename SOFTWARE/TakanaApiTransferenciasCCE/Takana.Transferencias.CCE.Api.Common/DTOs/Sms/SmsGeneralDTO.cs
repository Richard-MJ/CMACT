namespace Takana.Transferencias.CCE.Api.Common.DTOs.Sms
{
    public class SmsGeneralDTO
    {
        #region Constantes
        /// <summary>
        /// Llave que generara el correo
        /// </summary>
        public const string LlaveGenerarSMS = "enviar_sms_general";
        #endregion

        #region Propiedades
        /// <summary>
        /// Numero de celular
        /// </summary>
        public string NumeroCelular { get; set; }
        /// <summary>
        /// Tipo de mensaje
        /// </summary>
        public string TipoMensaje { get; set; }
        /// <summary>
        /// Adjunto
        /// </summary>
        public string Adjunto { get; set; }
        /// <summary>
        /// Bitacora
        /// </summary>
        public DtoBitacora Bitacora { get; set; }

        #endregion
    }

    public class DtoBitacora
    {
        /// <summary>
        /// Id bitacora
        /// </summary>
        public int IdBitacora { get; set; }
        /// <summary>
        /// Id detalle de la bitacora detalle a actualizar
        /// </summary>
        public int IdDetalleBitacora { get; set; }
    }
}
