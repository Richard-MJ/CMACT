using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Se Declaran los objetos y sus tipos de datos de archivo conciliacion
    /// </summary>
    public class Reporte 
    {
        #region Constante
        /// <summary>
        /// Constante de la descripción de Reportes Diarios
        /// </summary>
        public const string DescripcionReportesDiarios = "REPORTES DIARIOS";
        /// <summary>
        /// Constante de la descripción de Reportes Mensuales
        /// </summary>
        public const string DescripcionReportesMensuales = "REPORTES MENSUALES";
        /// <summary>
        /// Constante de la descripción de respuesta de directorio
        /// </summary>
        public const string DescripcionRespuestaDirectorio = "RESPUESTA DE DIRECTORIO DEL SERVICIO SFTP";
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador del Reporte
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Identificador de tipo de reporte
        /// </summary>
        public int IdTipoReporte { get; private set; }
        /// <summary>
        /// Identificador de periodo
        /// </summary>
        public int IdPeriodo { get; private set; }
        /// <summary>
        /// Nombre del Reporte
        /// </summary>
        public string Nombre { get; private set; }
        /// <summary>
        /// Contenido del Reporte
        /// </summary>
        public byte[] Contenido { get; private set; }
        /// <summary>
        /// Codigo de usuario del registro
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// Indicador que si el archivo fue subido al servicio SFTP
        /// </summary>
        public string IndicadorSubidoSFTP { get; private set; }
        /// <summary>
        /// Fecha del reporte
        /// </summary>
        public DateTime FechaReporte { get; private set; }
        /// <summary>
        /// Fecha que se genero el reporte
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Fecha de Registro en la cual fue subido al SFTP
        /// </summary>
        public DateTime? FechaSubidoSFTP { get; private set; }
        /// <summary>
        /// Entidad de periodo
        /// </summary>
        public virtual Periodo Periodo { get; set; }
        /// <summary>
        /// Entidad de de Tipo de reporte
        /// </summary>
        public virtual TipoReporte TipoReporte { get; set; }
        #endregion

        #region Metodos
        /// <summary>
        /// Extension que mapea el Reporte para agregar
        /// </summary>
        /// <param name="dato">Reporte</param>
        /// <returns>Retorna Reporte DTO</returns>
        public static Reporte RegistrarReporte(
            string nombre,
            byte[] contenido,
            string usuario,
            int idPeriodo,
            int idTipoReporte,
            DateTime fechaReporte,
            DateTime fechaSistema)
        {
            return new Reporte()
            {
                IdTipoReporte = idTipoReporte,
                IdPeriodo = idPeriodo,
                Nombre = nombre,
                Contenido = contenido,
                IndicadorSubidoSFTP = General.No,
                CodigoUsuarioRegistro = usuario,
                FechaReporte = fechaReporte,
                FechaRegistro = fechaSistema,
            };
        }

        /// <summary>
        /// Actualiza el indicador de SFTP 
        /// </summary>
        public void ActualizarIndicadorSFTP(DateTime fechaSistema)
        {
            IndicadorSubidoSFTP = General.Si;
            FechaSubidoSFTP = fechaSistema;
        }

        /// <summary>
        /// Valida el estado del reporte
        /// </summary>
        public void ValidarEstadoReporte()
        {
            if (IndicadorSubidoSFTP == General.Si)
                throw new Exception("El archivo ya fue subido al servicio SFTP");
        }
        #endregion
    }
}