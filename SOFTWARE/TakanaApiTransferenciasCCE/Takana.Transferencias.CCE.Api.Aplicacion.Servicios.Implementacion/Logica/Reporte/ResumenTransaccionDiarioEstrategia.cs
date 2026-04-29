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
    public class ResumenTransaccionDiarioEstrategia : IServicioGeneracionArchivoEstrategia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public ResumenTransaccionDiarioEstrategia(IRepositorioGeneral repositorioGeneral)
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
            var acumulado = new ReporteResumenTransaccionDTO();

            foreach (var dato in datos)
            {
                var parcial = _repositorioGeneral.ObtenerDatosResumenTransacciones(dato.Anio, dato.Mes, dato.Dia, periodo);

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
        public string ObtenerNombreArchivo(GenerarReporteDTO datos) => datos.NombreArchivoResumenTransaccionDiario;

        /// <summary>
        /// Obtener descripción del Reporte
        /// </summary>
        /// <returns></returns>
        public string ObtenerDescripcionReporte() => "REPORTE DIARIO DEL ANEXO 19: RESUMEN DE TRANSACCIONES";

        /// <summary>
        /// Genera el reporte de mantenimiento del ANEXO 19
        /// </summary>
        /// <param name="datosReporte"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        private async Task<byte[]> GenerarArchivoCSV(ReporteResumenTransaccionDTO datosReporte)
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

                        csv.WriteField("Id Procesador Pago");
                        csv.WriteField("Tx Aprobadas Nro.");
                        csv.WriteField("Tx Aprobadas Monto");
                        csv.WriteField("Tx Denegadas Nro.");
                        csv.WriteField("Tx Denegadas Monto");
                        csv.WriteField("Tx P2P Nro.");
                        csv.WriteField("Tx P2P Monto");
                        csv.WriteField("Tx P2M Nro.");
                        csv.WriteField("Tx P2M Monto");
                        csv.NextRecord();

                        csv.WriteField(EntidadFinancieraInmediata.CodigoGestorDirectorio);
                        csv.WriteField(datosReporte.TransaccionesAprobadasNumero);
                        csv.WriteField(datosReporte.TransaccionesAprobadasMonto.ToString("F2", CultureInfo.InvariantCulture));
                        csv.WriteField(datosReporte.TransaccionesDenegadasNumero);
                        csv.WriteField(datosReporte.TransaccionesDenegadasMonto.ToString("F2", CultureInfo.InvariantCulture));
                        csv.WriteField(datosReporte.TransaccionesP2PNumero);
                        csv.WriteField(datosReporte.TransaccionesP2PMonto.ToString("F2", CultureInfo.InvariantCulture));
                        csv.WriteField(datosReporte.TransaccionesP2MNumero);
                        csv.WriteField(datosReporte.TransaccionesP2MMonto.ToString("F2", CultureInfo.InvariantCulture));
                        csv.NextRecord();

                        csv.WriteField("TRANSACCIONES APROBADAS POR ENTIDAD BENEFICIARIA");
                        csv.NextRecord();

                        csv.WriteField("Id Entidad Beneficiaria");
                        csv.WriteField("Id Procesador Pago");
                        csv.WriteField("Número");
                        csv.WriteField("Monto");
                        csv.NextRecord();

                        foreach (var dato in datosReporte.TransaccionesAprobadas)
                        {
                            csv.WriteField(dato.CodigoEntidadBeneficiaria);
                            csv.WriteField(EntidadFinancieraInmediata.CodigoGestorDirectorio);
                            csv.WriteField(dato.NumeroTransaccionesAprobadas);
                            csv.WriteField(dato.MontoTransaccionesAprobadas.ToString("F2", CultureInfo.InvariantCulture));
                            csv.NextRecord();
                        }

                        csv.WriteField("TRANSACCIONES DENEGADAS POR ENTIDAD BENEFICIARIA");
                        csv.NextRecord();

                        csv.WriteField("Id Entidad Beneficiaria");
                        csv.WriteField("Id Procesador Pago");
                        csv.WriteField("Id Código Negación");
                        csv.WriteField("Número");
                        csv.WriteField("Monto");
                        csv.NextRecord();

                        foreach (var dato in datosReporte.TransaccionesDenegadas)
                        {
                            csv.WriteField(dato.CodigoEntidadBeneficiaria);
                            csv.WriteField(EntidadFinancieraInmediata.CodigoGestorDirectorio);
                            csv.WriteField(dato.IdentificadorCodigoNegacion);
                            csv.WriteField(dato.NumeroTransaccionesDenegadas);
                            csv.WriteField(dato.MontoTransaccionesDenegadas.ToString("F2", CultureInfo.InvariantCulture));
                            csv.NextRecord();
                        }

                        csv.WriteField("TRANSACCIONES POR RANGO DE MONTO");
                        csv.NextRecord();

                        csv.WriteField("Id Entidad Beneficiaria");
                        csv.WriteField("Nro. Tx <= 20");
                        csv.WriteField("Nro. Tx > 20 <= 50");
                        csv.WriteField("Nro. Tx > 50 <= 100");
                        csv.WriteField("Nro. Tx > 100 <= 200");
                        csv.WriteField("Nro. Tx > 200");
                        csv.NextRecord();

                        foreach (var dato in datosReporte.TransaccionesPorRangoMonto)
                        {
                            csv.WriteField(dato.CodigoEntidadBeneficiaria);
                            csv.WriteField(dato.NumeroTransaccionesMenorIgual20);
                            csv.WriteField(dato.NumeroTransaccionesMayor20MenorIgual50);
                            csv.WriteField(dato.NumeroTransaccionesMayor50MenorIgual100);
                            csv.WriteField(dato.NumeroTransaccionesMayor100MenorIgual200);
                            csv.WriteField(dato.NumeroTransaccionesMayor200);
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
        /// Acumula datos de resumen de transacciones.
        /// </summary>
        /// <param name="acumulado">Objeto con los datos acumulados hasta ahora.</param>
        /// <param name="parcial">Datos del periodo actual a agregar.</param>
        private void MapearDatosAcumulados(
            ReporteResumenTransaccionDTO acumulado,
            ReporteResumenTransaccionDTO parcial)
        {
            acumulado.TransaccionesAprobadasNumero += parcial.TransaccionesAprobadasNumero;
            acumulado.TransaccionesAprobadasMonto += parcial.TransaccionesAprobadasMonto;
            acumulado.TransaccionesDenegadasNumero += parcial.TransaccionesDenegadasNumero;
            acumulado.TransaccionesDenegadasMonto += parcial.TransaccionesDenegadasMonto;
            acumulado.TransaccionesP2PNumero += parcial.TransaccionesP2PNumero;
            acumulado.TransaccionesP2PMonto += parcial.TransaccionesP2PMonto;
            acumulado.TransaccionesP2MNumero += parcial.TransaccionesP2MNumero;
            acumulado.TransaccionesP2MMonto += parcial.TransaccionesP2MMonto;

            foreach (var parAprob in parcial.TransaccionesAprobadas)
            {
                var existing = acumulado.TransaccionesAprobadas
                    .FirstOrDefault(x => x.CodigoEntidadBeneficiaria == parAprob.CodigoEntidadBeneficiaria);
                if (existing != null)
                {
                    existing.NumeroTransaccionesAprobadas += parAprob.NumeroTransaccionesAprobadas;
                    existing.MontoTransaccionesAprobadas += parAprob.MontoTransaccionesAprobadas;
                }
                else
                {
                    acumulado.TransaccionesAprobadas.Add(new TransaccionesAprobadasDTO
                    {
                        CodigoEntidadBeneficiaria = parAprob.CodigoEntidadBeneficiaria,
                        NumeroTransaccionesAprobadas = parAprob.NumeroTransaccionesAprobadas,
                        MontoTransaccionesAprobadas = parAprob.MontoTransaccionesAprobadas
                    });
                }
            }

            foreach (var parDeneg in parcial.TransaccionesDenegadas)
            {
                var existing = acumulado.TransaccionesDenegadas
                    .FirstOrDefault(x =>
                        x.CodigoEntidadBeneficiaria == parDeneg.CodigoEntidadBeneficiaria &&
                        x.IdentificadorCodigoNegacion == parDeneg.IdentificadorCodigoNegacion);

                if (existing != null)
                {
                    existing.NumeroTransaccionesDenegadas += parDeneg.NumeroTransaccionesDenegadas;
                    existing.MontoTransaccionesDenegadas += parDeneg.MontoTransaccionesDenegadas;
                }
                else
                {
                    acumulado.TransaccionesDenegadas.Add(new TransaccionesDenegadasDTO
                    {
                        CodigoEntidadBeneficiaria = parDeneg.CodigoEntidadBeneficiaria,
                        IdentificadorCodigoNegacion = parDeneg.IdentificadorCodigoNegacion,
                        NumeroTransaccionesDenegadas = parDeneg.NumeroTransaccionesDenegadas,
                        MontoTransaccionesDenegadas = parDeneg.MontoTransaccionesDenegadas
                    });
                }
            }

            foreach (var parRango in parcial.TransaccionesPorRangoMonto)
            {
                var existing = acumulado.TransaccionesPorRangoMonto
                    .FirstOrDefault(x => x.CodigoEntidadBeneficiaria == parRango.CodigoEntidadBeneficiaria);

                if (existing != null)
                {
                    existing.NumeroTransaccionesMenorIgual20 += parRango.NumeroTransaccionesMenorIgual20;
                    existing.NumeroTransaccionesMayor20MenorIgual50 += parRango.NumeroTransaccionesMayor20MenorIgual50;
                    existing.NumeroTransaccionesMayor50MenorIgual100 += parRango.NumeroTransaccionesMayor50MenorIgual100;
                    existing.NumeroTransaccionesMayor100MenorIgual200 += parRango.NumeroTransaccionesMayor100MenorIgual200;
                    existing.NumeroTransaccionesMayor200 += parRango.NumeroTransaccionesMayor200;
                }
                else
                {
                    acumulado.TransaccionesPorRangoMonto.Add(new TransaccionesPorRangoMontoDTO
                    {
                        CodigoEntidadBeneficiaria = parRango.CodigoEntidadBeneficiaria,
                        NumeroTransaccionesMenorIgual20 = parRango.NumeroTransaccionesMenorIgual20,
                        NumeroTransaccionesMayor20MenorIgual50 = parRango.NumeroTransaccionesMayor20MenorIgual50,
                        NumeroTransaccionesMayor50MenorIgual100 = parRango.NumeroTransaccionesMayor50MenorIgual100,
                        NumeroTransaccionesMayor100MenorIgual200 = parRango.NumeroTransaccionesMayor100MenorIgual200,
                        NumeroTransaccionesMayor200 = parRango.NumeroTransaccionesMayor200
                    });
                }
            }
        }

    }
}