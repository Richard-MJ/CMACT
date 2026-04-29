using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class MantenimientoMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public MantenimientoMensualEstrategia(IRepositorioGeneral repositorioGeneral)
        {
            _repositorioGeneral = repositorioGeneral;
        }

        /// <summary>
        /// Método que genera el archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        public async Task<byte[]> GenerarArchivo(List<GenerarReporteDTO> datos, Periodo periodo)
        {
            var dato = datos.OrderByDescending(d => d.FechaReporte).First();
            var datosReporte = _repositorioGeneral.ObtenerDatosReporteMantenimiento(dato.Anio, dato.Mes);

            return await GenerarArchivoCSV(dato, datosReporte);
        }

        /// <summary>
        /// Obtener descripción de Tema del Reporte para Correo Electronico
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionTemaMensaje() => CorreoGeneralDTO.DescripcionArchivoTemaMensaje;

        /// <summary>
        /// Obtener nombre del archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoMantenimientoMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 14: MANTENIMIENTOS EJECUTADOS";

        /// <summary>
        /// Genera el reporte de mantenimiento del ANEXO 14
        /// </summary>
        /// <param name="datosEncabezado"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task<byte[]> GenerarArchivoCSV(
            GenerarReporteDTO datosEncabezado,
            List<ReporteMantenimientoDTO> datosReporte)
        {
            try
            {
                var primerRegistro = datosReporte.FirstOrDefault();

                using (var memoryStream = new MemoryStream())
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, leaveOpen: true))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ";",
                        Encoding = Encoding.UTF8
                    };

                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteField("RESUMEN");
                        csv.NextRecord();

                        csv.WriteField("Id Entidad");
                        csv.WriteField("Nombre Entidad");
                        csv.WriteField("Rol Entidad");
                        csv.WriteField("Año");
                        csv.WriteField("Mes");
                        csv.WriteField("Duración Total Mant. Programados");
                        csv.WriteField("Cantidad Total Mant. Programados");
                        csv.WriteField("Duración Total Mant. Correctivos");
                        csv.WriteField("Cantidad Total Mant. Correctivos");
                        csv.WriteField("Duración Total Mant. Emergencia");
                        csv.WriteField("Cantidad Total Mant. Emergencia");
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                        csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                        csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadBeneficiaria);
                        csv.WriteField(datosEncabezado.Anio);
                        csv.WriteField(datosEncabezado.Mes);
                        csv.WriteField(primerRegistro?.DuracionTotalMantenimientoProgramados ?? 0);
                        csv.WriteField(primerRegistro?.CantidadTotalMantenimientoProgramados ?? 0);
                        csv.WriteField(primerRegistro?.DuracionTotalMantenimientoCorrectivos ?? 0);
                        csv.WriteField(primerRegistro?.CantidadTotalMantenimientoCorrectivos ?? 0);
                        csv.WriteField(primerRegistro?.DuracionTotalMantenimientoEmergencia ?? 0);
                        csv.WriteField(primerRegistro?.CantidadTotalMantenimientoEmergencia ?? 0);
                        csv.NextRecord();

                        csv.WriteField("MANTENIMIENTOS EJECUTADOS");
                        csv.NextRecord();

                        csv.WriteField("Fecha Inicio");
                        csv.WriteField("Hora Inicio");
                        csv.WriteField("Fecha Fin");
                        csv.WriteField("Hora Fin");
                        csv.WriteField("Duración");
                        csv.WriteField("Tipo Mant.");
                        csv.WriteField("Indisponibilidad");
                        csv.WriteField("Tiempo Indisp.");
                        csv.WriteField("Pase a Producción");
                        csv.WriteField("Motivo");
                        csv.WriteField("Impacto");
                        csv.WriteField("Problemas detectados");
                        csv.WriteField("Comentarios");
                        csv.NextRecord();

                        foreach (var dato in datosReporte)
                        {
                            csv.WriteField(dato.FechaInicio);
                            csv.WriteField(dato.HoraInicio);
                            csv.WriteField(dato.FechaFin);
                            csv.WriteField(dato.HoraFin);
                            csv.WriteField(dato.Duracion);
                            csv.WriteField(dato.TipoMantenimiento);
                            csv.WriteField(dato.Indisponibilidad);
                            csv.WriteField(dato.Tiempo);
                            csv.WriteField(dato.PaseProduccion);
                            csv.WriteField(dato.DescripcionMotivo.ParaUnaSolaLinea());
                            csv.WriteField(dato.DescripcionImpacto.ParaUnaSolaLinea());
                            csv.WriteField(dato.DescripcionProblemas.ParaUnaSolaLinea());
                            csv.WriteField(dato.DescripcionComentarios.ParaUnaSolaLinea());
                            csv.NextRecord();
                        }

                        await writer.FlushAsync();

                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }
    }
}
