using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class DirectorioInteroperabilidadDiarioEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public DirectorioInteroperabilidadDiarioEstrategia(IRepositorioGeneral repositorioGeneral)
        {
            _repositorioGeneral = repositorioGeneral;
        }

        /// <summary>
        /// Método que genera el archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public async Task<byte[]> GenerarArchivo(List<GenerarReporteDTO> datos, Periodo periodo)
        {
            var dato = datos.First();
            var fecha = DateTime.Parse($"{dato.Anio}-{dato.Mes}-{dato.Dia}");
            var fechaHora = fecha.Date.Add(DateTime.Now.TimeOfDay);

            var bitacorasAfiliaciones = _repositorioGeneral
                .ObtenerPorExpresionConLimite<BitacoraInteroperabilidadAfiliacion>(x => 
                    x.FechaRegistro.Date == fecha.Date)
                .GroupBy(x => new
                {
                    x.CodigoCCI,
                    x.NumeroCelular
                })
                .Select(g => g.OrderByDescending(x => x.FechaRegistro).FirstOrDefault())
                .Where(x => x?.CodigoRespuesta == General.Pendiente)
                .ToList();

            if (!bitacorasAfiliaciones.Any())
                throw new Exception("No se tienen afiliaciones pendientes para enviar al servicio SFTP de la CCE");

            int numeroSeguimiento = _repositorioGeneral.ObtenerNumeroSerieNoBloqueante("%",
                Sistema.CuentaEfectivo, DatosGenerales.CodigoSeguimientoAfiliacion, bitacorasAfiliaciones.Count());

            var datosArchivo = bitacorasAfiliaciones.AdtoDirectorioArchivoMasivo(fechaHora, numeroSeguimiento);

            return await GenerarArchivoCSV(datosArchivo);
        }

        /// <summary>
        /// Obtener descripción de Tema del Reporte para Correo Electronico
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionTemaMensaje() => CorreoGeneralDTO.DescripcionDirectorioTemaMensaje;

        /// <summary>
        /// Obtener nombre del archivo
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => EntidadFinancieraInmediata.CodigoCajaTacna + datos.NombreArchivoCargaMasivaDirectorio;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "ARCHIVO DE CARGA MASIVA: AFILIACIONES Y DESAFILIACIONES DE INTEROPERABLIDAD";

        /// <summary>
        /// Genera el reporte de calidad de servicio de efectividad busqueda de alias ANEXO 17
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<byte[]> GenerarArchivoCSV(
            List<EstructuraArchivoDirectorioDTO> datosArchivo)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                using (var writer = new StreamWriter(memoryStream, Encoding.UTF8, 1024, leaveOpen: true))
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        Encoding = Encoding.UTF8
                    };

                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteField(DatosGeneralesInteroperabilidad.CodigoTipoInstruccion);
                        csv.WriteField(DatosGeneralesInteroperabilidad.CodigoIdTramoMensajeProxy01);
                        csv.WriteField(DatosGeneralesInteroperabilidad.CodigoCCI);
                        csv.WriteField(DatosGeneralesInteroperabilidad.CodigoNumeroCelular);
                        csv.NextRecord();

                        foreach (var dato in datosArchivo)
                        {
                            csv.WriteField(dato.CodigoAfiliacion);
                            csv.WriteField(dato.IdTrama);
                            csv.WriteField(dato.CodigoCuentaInterbancaria);
                            csv.WriteField(dato.NumeroCelular);
                            csv.NextRecord();
                        }

                        csv.WriteField($"REGISTROS = {datosArchivo.Count()}");
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
    }
}