using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class ICDNoDisponibilidadMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ICDNoDisponibilidadMensualEstrategia(
            IRepositorioGeneral repositorioGeneral, 
            IServicioAplicacionPeticion servicioAplicacionPeticion)
        {
            _repositorioGeneral = repositorioGeneral;
            _servicioAplicacionPeticion = servicioAplicacionPeticion;
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
            var acumulado = new ReporteICDNoDisponibilidadDTO();
            double totalMinutosRedondeado = 0;

            foreach (var dato in datos)
            {
                var datosMonitoreo = dato.AdtoObtenerDatosMonitoreoMensual();

                var horaDesde = TimeSpan.Parse(periodo.HoraDesde);
                var horaHasta = TimeSpan.Parse(periodo.HoraHasta);

                var listaRegistros = await _servicioAplicacionPeticion.ObtenerRegistrosDisponibilidad(datosMonitoreo);

                var totalMinutosIndisponibilidad = listaRegistros
                    .Where(x => x.TipoEvento == "api.down")
                    .Sum(x =>
                    {
                        var inicio = x.FechaHoraInicio;
                        var fin = x.FechaHoraFin;

                        if (fin <= inicio) return 0;

                        double minutosTotales = 0;

                        var fechaActual = inicio.Date;
                        var fechaFin = fin.Date;

                        while (fechaActual <= fechaFin)
                        {
                            DateTime desdeDia = fechaActual + horaDesde;
                            DateTime hastaDia = fechaActual + horaHasta;

                            var inicioDia = inicio > desdeDia ? inicio : desdeDia;
                            var finDia = fin < hastaDia ? fin : hastaDia;

                            if (inicioDia < finDia)
                                minutosTotales += (finDia - inicioDia).TotalMinutes;

                            fechaActual = fechaActual.AddDays(1);
                        }

                        return minutosTotales;
                    });

                totalMinutosRedondeado += Math.Round(totalMinutosIndisponibilidad);

                var parcial = _repositorioGeneral.ObtenerDatosCalidadServicioNoDisponibilidad(
                    dato.Anio, dato.Mes, dato.Dia, periodo, GenerarReporteDTO.FrecuenciaMensual);

                MapearDatosAcumulados(acumulado, parcial);
            }

            return await GenerarArchivoCSV(totalMinutosRedondeado, encabezado, acumulado);
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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoICDNoDisponibilidadMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 17: INDICADORES DE CALIDAD DE SERVICIO - ICD DE NO DISPONIBILIDAD";

        /// <summary>
        /// Genera el reporte de calidad de servicio de no disponibilidad diaria ANEXO 17
        /// </summary>
        /// <param name="totalMinutosIndisponibilidad"></param>
        /// <param name="datosEncabezado"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>        
        private async Task<byte[]> GenerarArchivoCSV(
            double totalMinutosIndisponibilidad,
            GenerarReporteDTO datosEncabezado,
            ReporteICDNoDisponibilidadDTO datosReporte)
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

                    datosReporte.ActualizarICDNoDisponibilidad(totalMinutosIndisponibilidad);

                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteField("Id Entidad");
                        csv.WriteField("Nombre Entidad");
                        csv.WriteField("Rol Entidad");
                        csv.WriteField("Calidad Servicio");
                        csv.WriteField("ICD");
                        csv.WriteField("Minutos Totales");
                        csv.WriteField("Minutos Totales Indisp.");
                        csv.WriteField("Minutos Totales Mant.");
                        csv.WriteField("ICD Resultado");
                        csv.WriteField("Comentarios");
                        csv.WriteField("Año");
                        csv.WriteField("Mes");
                        csv.WriteField("Día");
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                        csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                        csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadOrdenante);
                        csv.WriteField(GenerarReporteDTO.DescripcionNoDisponibilidad);
                        csv.WriteField(GenerarReporteDTO.DescripcionBusquedaAlias);
                        csv.WriteField(datosReporte.MinutosTotales);
                        csv.WriteField(totalMinutosIndisponibilidad);
                        csv.WriteField(datosReporte.MinutosTotalesMantenimiento);
                        csv.WriteField(datosReporte.ICDResultado.ToString("F2", CultureInfo.InvariantCulture));
                        csv.WriteField(datosReporte.Comentarios.ParaUnaSolaLinea());
                        csv.WriteField(datosEncabezado.Anio);
                        csv.WriteField(datosEncabezado.Mes);
                        csv.WriteField(GenerarReporteDTO.ValorDiaPorFrencuenciaMensual);
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
        /// Mapea los datos de indisponibilidad acumulados.
        /// </summary>
        /// <param name="acumulado">Objeto con los datos acumulados hasta el momento.</param>
        /// <param name="parcial">Datos parciales del período actual.</param>
        private void MapearDatosAcumulados(
            ReporteICDNoDisponibilidadDTO acumulado,
            ReporteICDNoDisponibilidadDTO parcial)
        {
            acumulado.MinutosTotales += parcial.MinutosTotales;
            acumulado.MinutosTotalesMantenimiento += parcial.MinutosTotalesMantenimiento;
            acumulado.Comentarios = string.Empty;
        }
    }
}
