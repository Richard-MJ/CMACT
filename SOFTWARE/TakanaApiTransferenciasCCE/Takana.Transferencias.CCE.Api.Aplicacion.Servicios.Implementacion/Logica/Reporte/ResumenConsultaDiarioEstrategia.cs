using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class ResumenConsultaDiarioEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ResumenConsultaDiarioEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var acumulado = new ReporteResumenConsultaDTO();

            foreach (var dato in datos)
            {
                var parcial = _repositorioGeneral.ObtenerDatosResumenConsultas(dato.Anio, dato.Mes, dato.Dia, periodo);

                MapearDatosAcumulados(acumulado, parcial);
            }

            return await GenerarArchivoCSV(acumulado);
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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoResumenConsultaDiario;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE DIARIO DEL ANEXO 19: RESUMEN DE CONSULTAS";

        /// <summary>
        /// Genera el reporte de resumen de consultas del ANEXO 19
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task<byte[]> GenerarArchivoCSV(
            ReporteResumenConsultaDTO datosReporte)
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
                        csv.WriteField("Id Gestor Directorio");
                        csv.WriteField("Func. Especifica");
                        csv.WriteField("Cant. Consultas Totales");
                        csv.WriteField("Cant. Consultas Exitosas");
                        csv.WriteField("Cant. Consultas Erradas");
                        csv.WriteField("Cant. Consultas QR");
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                        csv.WriteField(ConfigApi.Nombre);
                        csv.WriteField(datosReporte.CantidadConsultasTotales);
                        csv.WriteField(datosReporte.CantidadConsultasExitosas);
                        csv.WriteField(datosReporte.CantidadConsultasErradas);
                        csv.WriteField(datosReporte.CantidadConsultasQR);
                        csv.NextRecord();

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

        /// <summary>
        /// Mapea los datos acumulados.
        /// </summary>
        /// <param name="acumulado">Objeto con los datos acumulados hasta el momento.</param>
        /// <param name="parcial">Datos parciales del período actual.</param>
        private void MapearDatosAcumulados(
            ReporteResumenConsultaDTO acumulado,
            ReporteResumenConsultaDTO parcial)
        {
            acumulado.CantidadConsultasTotales += parcial.CantidadConsultasTotales;
            acumulado.CantidadConsultasExitosas += parcial.CantidadConsultasExitosas;
            acumulado.CantidadConsultasErradas += parcial.CantidadConsultasErradas;
            acumulado.CantidadConsultasQR += parcial.CantidadConsultasQR;
        }
    }
}
