using CsvHelper;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class VariacionUsuarioMontoTransferenciasMensualEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public VariacionUsuarioMontoTransferenciasMensualEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var datosReporte = _repositorioGeneral.ObtenerDatosReporteVariacionUsuarioMontoTransferencias(dato.Anio, dato.Mes);

            return await GenerarArchivoCSV(datosReporte);
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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoCNTUsuarioMensual;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE MENSUAL DEL ANEXO 20: VARIACIÓN DE USUARIOS DE MONTO DE TRANSFERENCIAS";

        /// <summary>
        /// Genera el Reporte de Variación de usuarios y montos de transferencias del ANEXO 20
        /// </summary>
        /// <param name="datosEncabezado"></param>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<byte[]> GenerarArchivoCSV(
            ReporteVariacionUsuarioMontoTransferenciasDTO datosReporte)
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
                        csv.WriteField("USUARIOS");
                        csv.NextRecord();

                        csv.WriteField("Nro. Usuarios Total");
                        csv.WriteField("Nro. Usuarios Nuevos");
                        csv.WriteField("Nro. Usuarios Activos");
                        csv.WriteField("Nro. Usuarios Tx P2P");
                        csv.WriteField("Nro. Usuarios Tx P2M");
                        csv.NextRecord();

                        csv.WriteField(datosReporte.NumeroUsuariosTotal);
                        csv.WriteField(datosReporte.NumeroUsuariosNuevos);
                        csv.WriteField(datosReporte.NumeroUsuariosActivos);
                        csv.WriteField(datosReporte.NumeroUsuariosTxP2P);
                        csv.WriteField(datosReporte.NumeroUsuariosTxP2M);
                        csv.NextRecord();

                        csv.WriteField("TRANSFERENCIAS");
                        csv.NextRecord();

                        csv.WriteField("Nro. Usuarios Tx <= 20");
                        csv.WriteField("Nro. Usuarios Tx > 20 <= 50");
                        csv.WriteField("Nro. Usuarios Tx > 50 <= 100");
                        csv.WriteField("Nro. Usuarios Tx > 100 <= 200");
                        csv.WriteField("Nro. Usuarios Tx > 200");
                        csv.NextRecord();

                        csv.WriteField(datosReporte.NumeroUsuariosTxMenorOigual20);
                        csv.WriteField(datosReporte.NumeroUsuariosTxMayor20MenorOigual50);
                        csv.WriteField(datosReporte.NumeroUsuariosTxMayor50MenorOigual100);
                        csv.WriteField(datosReporte.NumeroUsuariosTxMayor100MenorOigual200);
                        csv.WriteField(datosReporte.NumeroUsuariosTxMayor200);
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
