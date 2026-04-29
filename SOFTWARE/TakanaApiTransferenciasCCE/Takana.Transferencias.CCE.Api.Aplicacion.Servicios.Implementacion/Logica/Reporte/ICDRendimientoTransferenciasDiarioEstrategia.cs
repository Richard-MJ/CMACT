using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class ICDRendimientoTransferenciasDiarioEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ICDRendimientoTransferenciasDiarioEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var encabezado = datos.OrderByDescending(d => d.FechaReporte).First();
            var acumulado = new List<ReporteICDRendimientoDTO>();

            foreach (var dato in datos)
            {
                var parcial = _repositorioGeneral.ObtenerDatosCalidadServicioRendimientoTransferencias(
                    dato.Anio, dato.Mes, dato.Dia, periodo, GenerarReporteDTO.FrecuenciaDiaria);

                MapearDatosAcumulados(acumulado, parcial);
            }

            return await GenerarArchivoCSV(encabezado, acumulado);
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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoICDRendimientoTransferenciasDiario;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE DIARIO DEL ANEXO 17: INDICADORES DE CALIDAD DE SERVICIO - ICD DE RENDIMIENTO DE TRANSFERENCIAS";

        /// <summary>
        /// Genera el reporte de resumen de consultas del ANEXO 19
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task<byte[]> GenerarArchivoCSV(
            GenerarReporteDTO datos,
            List<ReporteICDRendimientoDTO> datosReporte)
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
                        csv.WriteField("Calidad Servicio");
                        csv.WriteField("Año");
                        csv.WriteField("Mes");
                        csv.WriteField("Día");
                        csv.WriteField("ICD");
                        csv.WriteField("Func. Específica");
                        csv.WriteField("Tipo ICD");
                        csv.WriteField("Cant. Transf. Totales");
                        csv.WriteField("Cant. Transf. Totales <= t");
                        csv.WriteField("Cant. Transf. Totales >= tmax");
                        csv.WriteField("ICD Resultado");
                        csv.WriteField("Comentarios");
                        csv.WriteField("Id Gestor Directorio");
                        csv.NextRecord();

                        foreach (var dato in datosReporte)
                        {
                            csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                            csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                            csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadOrdenante);
                            csv.WriteField(GenerarReporteDTO.DescripcionRendimiento);
                            csv.WriteField(datos.Anio);
                            csv.WriteField(datos.Mes);
                            csv.WriteField(datos.Dia);
                            csv.WriteField(GenerarReporteDTO.DescripcionTransferencia);
                            csv.WriteField(ConfigApi.Nombre);
                            csv.WriteField(dato.TipoICD);
                            csv.WriteField(dato.CantidadTotales);
                            csv.WriteField(dato.CantidadMenorIgualT);
                            csv.WriteField(dato.CantidadMayorIgualTMax);
                            csv.WriteField(dato.ICDResultado.ToString("F2", CultureInfo.InvariantCulture));
                            csv.WriteField(dato.Comentario.ParaUnaSolaLinea());
                            csv.WriteField(EntidadFinancieraInmediata.CodigoGestorDirectorio);
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

        /// <summary>
        /// Mapea los datos de indisponibilidad acumulados.
        /// </summary>
        /// <param name="acumulado">Objeto con los datos acumulados hasta el momento.</param>
        /// <param name="parcial">Datos parciales del período actual.</param>
        private void MapearDatosAcumulados(
            List<ReporteICDRendimientoDTO> acumulado,
            List<ReporteICDRendimientoDTO> parcial)
        {
            foreach (var item in parcial)
            {
                var target = acumulado.FirstOrDefault(x =>
                    string.Equals(x.TipoICD, item.TipoICD, StringComparison.OrdinalIgnoreCase));

                if (target is null)
                {
                    target = new ReporteICDRendimientoDTO
                    {
                        TipoICD = item.TipoICD,
                        CantidadTotales = 0,
                        CantidadMenorIgualT = 0,
                        CantidadMayorIgualTMax = 0,
                        Comentario = string.Empty
                    };
                    acumulado.Add(target);
                };

                target.CantidadTotales += item.CantidadTotales;

                if (item.TipoICD.Equals("PERCENTIL", StringComparison.OrdinalIgnoreCase) && item.CantidadMenorIgualT.HasValue)
                    target.CantidadMenorIgualT = (target.CantidadMenorIgualT ?? 0) + item.CantidadMenorIgualT.Value;
                else
                    target.CantidadMayorIgualTMax = (target.CantidadMayorIgualTMax ?? 0) + item.CantidadMayorIgualTMax.GetValueOrDefault();
            }
        }
    }
}
