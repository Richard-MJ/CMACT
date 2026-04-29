using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class ObjetivoTiempoRecuperacionMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ObjetivoTiempoRecuperacionMensualEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var datosReporte = _repositorioGeneral.ObtenerDatosReporteObjetoTiempoRecuperacion(dato.Anio, dato.Mes);

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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoRTOMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 16: OBJETIVO DE TIEMPO DE RECUPERACIÓN(RTO)";

        /// <summary>
        /// Genera el Reporte de Objetivo de tiempo de recuperación del ANEXO 16
        /// </summary>
        /// <param name="datosEncabezado"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<byte[]> GenerarArchivoCSV(
            GenerarReporteDTO datosEncabezado,
            List<ReporteObjetivoTiempoRecuperacionDTO> datosReporte)
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
                        csv.WriteField("Id Entidad");
                        csv.WriteField("Nombre Entidad");
                        csv.WriteField("Rol Entidad");
                        csv.WriteField("Año");
                        csv.WriteField("Mes");
                        csv.WriteField("Tipo");
                        csv.WriteField("Cant. Incidencias");
                        csv.WriteField("RTO");
                        csv.WriteField("Cant. Incidencias > RTO");
                        csv.WriteField("ICD Resultado");
                        csv.NextRecord();

                        foreach (var dato in datosReporte)
                        {
                            csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                            csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                            csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadBeneficiaria);
                            csv.WriteField(datosEncabezado.Anio);
                            csv.WriteField(datosEncabezado.Mes);
                            csv.WriteField(dato.Tipo);
                            csv.WriteField(dato.CantidadIncidencias);
                            csv.WriteField(dato.TiempoRTO);
                            csv.WriteField(dato.CantidadIncidenciasRTO);
                            csv.WriteField(dato.ICDResultado.ToString("F2", CultureInfo.InvariantCulture));
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
