namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class ReporteVariacionUsuarioMontoTransferenciasDTO
    {
        #region Propiedades
        /// <summary>
        /// Número total de usuarios al último día del periodo reportado
        /// </summary>
        public int NumeroUsuariosTotal { get; set; }
        /// <summary>
        /// Número de usuarios nuevos en el periodo reportado
        /// </summary>
        public int NumeroUsuariosNuevos { get; set; }
        /// <summary>
        /// Número de usuarios activos en el periodo reportado
        /// </summary>
        public int NumeroUsuariosActivos { get; set; }
        /// <summary>
        /// Número de usuarios que realizaron al menos una transferencia P2P en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxP2P { get; set; }
        /// <summary>
        /// Número de usuarios que realizaron al menos una transferencia P2M en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxP2M { get; set; }
        /// <summary>
        /// Número total de usuarios que realizaron transacciones por un valor menor o igual a S/20 en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxMenorOigual20 { get; set; }
        /// <summary>
        /// Número total de usuarios que realizaron transacciones por un valor mayor a S/20 y menor o igual a S/50 en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxMayor20MenorOigual50 { get; set; }
        /// <summary>
        /// Número total de usuarios que realizaron transacciones por un valor mayor a S/50 y menor o igual a S/100 en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxMayor50MenorOigual100 { get; set; }
        /// <summary>
        /// Número total de usuarios que realizaron transacciones por un valor mayor a S/100 y menor o igual a S/200 en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxMayor100MenorOigual200 { get; set; }
        /// <summary>
        /// Número total de usuarios que realizaron transacciones por un valor mayor a S/200 en el periodo reportado
        /// </summary>
        public int NumeroUsuariosTxMayor200 { get; set; }
        #endregion
    }
}
