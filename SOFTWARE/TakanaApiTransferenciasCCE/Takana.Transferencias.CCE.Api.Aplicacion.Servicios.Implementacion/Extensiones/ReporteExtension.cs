using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.Monitoreo;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones
{
    public static class ReporteExtension
    {

        /// <summary>
        /// Método que mapea para generar el reporte a su DTO
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="idTipoReporte"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static List<GenerarReporteDTO> AdtoGenerarReporte(
            this DateTime fecha, int idTipoReporte, string usuario)
        {
            return new List<GenerarReporteDTO>()
            {
                new GenerarReporteDTO()
                {
                    Anio = fecha.Year.ToString(),
                    Mes = fecha.Month.ToString("D2"),
                    Dia = fecha.Day.ToString("D2"),
                    IdTipoReporte = idTipoReporte,
                    Usuario = usuario
                }
            };
        }

        /// <summary>
        /// Método que mapea los datos del resumen diario
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static MonitoreoDTO AdtoObtenerDatosMonitoreoDiario(this GenerarReporteDTO datos)
        {
            var fecha = $"{datos.Anio}-{datos.Mes}-{datos.Dia}";

            return new MonitoreoDTO()
            {
                startDate = fecha,
                endDate = fecha,
            };
        }

        /// <summary>
        /// Método que mapea los datos del resumen mensual
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public static MonitoreoDTO AdtoObtenerDatosMonitoreoMensual(this GenerarReporteDTO datos)
        {
            var fecha = datos.Mes + '/' + datos.Anio;

            return new MonitoreoDTO()
            {
                startDate = fecha.ObtenerPrimerDia(),
                endDate = fecha.ObtenerUltimoDia(),
            };
        }

        /// <summary>
        /// Método que mapea el formato para correo de reportes
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="correoRemitente"></param>
        /// <param name="correoDestinatario"></param>
        /// <param name="servicio"></param>
        /// <param name="descripcion"></param>
        /// <param name="ArchivosAdjuntos"></param>
        /// <returns></returns>
        public static CorreoGeneralDTO AdtoFormatoCorreo( 
            this IContextoAplicacion contexto,
            DateTime fechaSistema,
            string correoRemitente,
            string correoDestinatario,
            string servicio,
            string descripcion,
            string temaMensaje,
            IList<ArchivoAdjuntoDTO> ArchivosAdjuntos)
        {
            return new CorreoGeneralDTO()
            {
                DescripcionOperacion = descripcion,
                CorreoElectronicoRemitente = correoRemitente,
                CorreoElectronicoDestinatario = correoDestinatario,
                FechaOperacion = fechaSistema,
                TemaMensaje = temaMensaje,
                Servicio = servicio,
                DireccionIP = contexto.IpAddress ?? "--",
                Modelo = contexto.ModeloDispositivo ?? "--",
                SistemaOperativo = contexto.SistemaOperativo ?? "--",
                Navegador = contexto.Navegador ?? "--",
                ArchivosAdjuntos = ArchivosAdjuntos
            };
        }

        /// <summary>
        /// Método que formatea de los archivos adjuntos
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static List<ArchivoAdjuntoDTO> AdtoFormatosArchivosAdjuntos(
            this List<Reporte> datos, string tipoArchivo)
        {
            return datos.ConvertAll(dato => dato.Contenido.AdtoFormatoArchivoAdjunto(dato.Nombre, tipoArchivo));
        }

        /// <summary>
        /// Método que formatea de los archivos adjuntos
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static List<ArchivoAdjuntoDTO> AdtoFormatosArchivosAdjuntos(
            this Reporte dato, string tipoArchivo)
        {
            return  new List<ArchivoAdjuntoDTO>
            {
                dato.Contenido.AdtoFormatoArchivoAdjunto(dato.Nombre, tipoArchivo)
            };
        }

        /// <summary>
        /// Mapear el formato para archivo adjunto
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="nombreArchivo"></param>
        /// <param name="tipoArchivo"></param>
        /// <returns></returns>
        public static ArchivoAdjuntoDTO AdtoFormatoArchivoAdjunto(
            this byte[] archivo,
            string nombreArchivo,
            string tipoArchivo)
        {
            return new ArchivoAdjuntoDTO()
            {
                Archivo = archivo,
                NombreArchivo = nombreArchivo,
                TipoMime = tipoArchivo,
            };
        }
    }
}
