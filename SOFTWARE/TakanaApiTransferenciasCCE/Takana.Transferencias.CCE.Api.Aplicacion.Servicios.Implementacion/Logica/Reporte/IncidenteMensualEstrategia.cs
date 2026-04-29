using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class IncidenteMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public IncidenteMensualEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var datosReporte = _repositorioGeneral.ObtenerDatosReporteIncidente(dato.Anio, dato.Mes);

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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoIncidenteMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 15: INCIDENTES";

        /// <summary>
        /// Genera el reporte de incidente del ANEXO 15
        /// </summary>
        /// <param name="datosEncabezado"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<byte[]> GenerarArchivoCSV(
            GenerarReporteDTO datosEncabezado,
            List<ReporteIncidenteDTO> datosReporte)
        {
            try
            {
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
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                        csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                        csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadBeneficiaria);
                        csv.WriteField(datosEncabezado.Anio);
                        csv.WriteField(datosEncabezado.Mes);
                        csv.NextRecord();

                        csv.WriteField("DETALLE INCIDENTES");
                        csv.NextRecord();

                        csv.WriteField("Tipo");
                        csv.WriteField("Escenario");
                        csv.WriteField("Fecha Inicio");
                        csv.WriteField("Hora Inicio");
                        csv.WriteField("Solucionado");
                        csv.WriteField("Fecha Fin");
                        csv.WriteField("Hora Fin");
                        csv.WriteField("Superó RTO");
                        csv.WriteField("Servicios Impactados");
                        csv.WriteField("Entidades Impactadas");
                        csv.WriteField("Desc. Incidente");
                        csv.WriteField("Acciones ejecutadas");
                        csv.WriteField("Pase a Producción");
                        csv.WriteField("Indisponibilidad");
                        csv.WriteField("Tiempo Indisp.");
                        csv.WriteField("Cant. Transf./Pagos Afectados");
                        csv.WriteField("Valor Transf./Pagos Afectados");
                        csv.WriteField("Recomendaciones");
                        csv.WriteField("Comentarios");
                        csv.NextRecord();

                        foreach (var dato in datosReporte)
                        {
                            csv.WriteField(dato.Tipo);
                            csv.WriteField(dato.Escenario);
                            csv.WriteField(dato.FechaInicio);
                            csv.WriteField(dato.HoraInicio);
                            csv.WriteField(dato.Solucionado);
                            csv.WriteField(dato.FechaFin);
                            csv.WriteField(dato.HoraFin);
                            csv.WriteField(dato.SuperoRTO);
                            csv.WriteField(dato.ServiciosImpactados.ParaUnaSolaLinea());
                            csv.WriteField(dato.EntidadesImpactadas.ParaUnaSolaLinea());
                            csv.WriteField(dato.DescripcionIncidente.ParaUnaSolaLinea());
                            csv.WriteField(dato.AccionesEjecutadas.ParaUnaSolaLinea());
                            csv.WriteField(dato.PaseProduccion);
                            csv.WriteField(dato.Indisponibilidad);
                            csv.WriteField(dato.TiempoIndisp);
                            csv.WriteField(dato.CantidadPagosAfectados);
                            csv.WriteField(dato.ValorPagosAfectados.ToString("F2", CultureInfo.InvariantCulture));
                            csv.WriteField(dato.Recomendaciones.ParaUnaSolaLinea());
                            csv.WriteField(dato.Comentarios.ParaUnaSolaLinea());
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