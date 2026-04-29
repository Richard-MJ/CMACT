using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
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
    public class ICDEfectividadBusquedaAliasMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ICDEfectividadBusquedaAliasMensualEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var acumulado = new ReporteICDEfectividadBusquedaAliasDTO();

            foreach (var dato in datos)
            {
                var parcial = _repositorioGeneral.ObtenerDatosCalidadServicioEfectividadBusquedaAlias(
                    dato.Anio, dato.Mes, dato.Dia, periodo, GenerarReporteDTO.FrecuenciaMensual);

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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoICDEfectividadBusquedaAliasMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 17: INDICADORES DE CALIDAD DE SERVICIO - ICD DE EFECTIVIDAD DE BUSQUEDA DE ALIAS";

        /// <summary>
        /// Genera el reporte de calidad de servicio de efectividad busqueda de alias ANEXO 17
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task<byte[]> GenerarArchivoCSV(
            GenerarReporteDTO datos,
            ReporteICDEfectividadBusquedaAliasDTO datosReporte)
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
                        csv.WriteField("Cant. Intentos Totales Consulta");
                        csv.WriteField("Cant. Fallas Ordenante");
                        csv.WriteField("Cant. Fallas YP");
                        csv.WriteField("Cant. Fallas CCE");
                        csv.WriteField("ICD Resultado");
                        csv.WriteField("Comentarios");
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoCajaTacna);
                        csv.WriteField(GenerarReporteDTO.AbreviaturaCajaTacna);
                        csv.WriteField(GenerarReporteDTO.DescripcionRolEntidadOrdenante);
                        csv.WriteField(GenerarReporteDTO.DescripcionEfectividad);
                        csv.WriteField(datos.Anio);
                        csv.WriteField(datos.Mes);
                        csv.WriteField(GenerarReporteDTO.ValorDiaPorFrencuenciaMensual);
                        csv.WriteField(GenerarReporteDTO.DescripcionBusquedaAlias);
                        csv.WriteField(ConfigApi.Nombre);
                        csv.WriteField(datosReporte.CantidadIntentosTotalesConsulta);
                        csv.WriteField(datosReporte.CantidadFallasCMACT);
                        csv.WriteField(datosReporte.CantidadFallasYellowPepper);
                        csv.WriteField(datosReporte.CantidadFallasCCE);
                        csv.WriteField(datosReporte.ICDResultado.ToString("F2", CultureInfo.InvariantCulture));
                        csv.WriteField(datosReporte.Comentario.ParaUnaSolaLinea());
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
            ReporteICDEfectividadBusquedaAliasDTO acumulado,
            ReporteICDEfectividadBusquedaAliasDTO parcial)
        {
            acumulado.CantidadIntentosTotalesConsulta += parcial.CantidadIntentosTotalesConsulta;
            acumulado.CantidadFallasCMACT += parcial.CantidadFallasCMACT;
            acumulado.CantidadFallasCCE += parcial.CantidadFallasCCE;
            acumulado.Comentario = string.Empty;
        }
    }
}
