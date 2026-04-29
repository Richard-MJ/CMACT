using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Datos.Contexto;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Repositorios
{
    public class RepositorioReportes
    {
        public readonly ContextoGeneral _contextoGeneral;
        public RepositorioReportes(ContextoGeneral contextoGeneral)
        {
            _contextoGeneral = contextoGeneral;
        }
        /// <summary>
        /// Procedimiento almancenado que obtiene datos de mantenimiento del Reporte Anexo 14
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReporteMantenimientoDTO> ObtenerDatosReporteMantenimiento(string anio, string mes)
        {
            var datosReporte = new List<ReporteMantenimientoDTO>();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_MANTENIMIENTOS_EJECUTADOS_ANEXO_14";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var datoReporte = new ReporteMantenimientoDTO()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ID_MANTENIMIENTO")),
                                    IdTipoMantenimiento = reader.GetInt32(reader.GetOrdinal("ID_TIPO_MANTENIMIENTO")),
                                    TipoMantenimiento = GetString(reader, "DES_TIPO_MANTENIMIENTO"),
                                    FechaInicio = GetString(reader, "FEC_INICIO"),
                                    HoraInicio = GetString(reader, "HOR_INICIO"),
                                    FechaFin = GetString(reader, "FEC_FIN"),
                                    HoraFin = GetString(reader, "HOR_FIN"),
                                    Duracion = GetString(reader, "NUM_DURACION"),
                                    Indisponibilidad = GetString(reader, "DES_INDISPONIBILIDAD"),
                                    Tiempo = GetString(reader, "NUM_TIEMPO"),
                                    PaseProduccion = GetString(reader, "DES_PASE_PRODUCCION"),
                                    DescripcionMotivo = GetString(reader, "DES_MOTIVO"),
                                    DescripcionImpacto = GetString(reader, "DES_IMPACTO"),
                                    DescripcionProblemas = GetString(reader, "DES_PROBLEMAS"),
                                    DescripcionComentarios = GetString(reader, "DES_COMENTARIOS"),
                                    DuracionTotalMantenimientoProgramados = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_DURACION_PROGRAMADOS")),
                                    CantidadTotalMantenimientoProgramados = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_CANTIDAD_PROGRAMADOS")),
                                    DuracionTotalMantenimientoCorrectivos = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_DURACION_CORRECTIVOS")),
                                    CantidadTotalMantenimientoCorrectivos = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_CANTIDAD_CORRECTIVOS")),
                                    DuracionTotalMantenimientoEmergencia = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_DURACION_EMERGENCIAS")),
                                    CantidadTotalMantenimientoEmergencia = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_CANTIDAD_EMERGENCIAS"))
                                };
                                datosReporte.Add(datoReporte);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Incidentes del Reporte Anexo 15
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReporteIncidenteDTO> ObtenerDatosReporteIncidente(string anio, string mes)
        {
            var datosReporte = new List<ReporteIncidenteDTO>();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_INCIDENTES_ANEXO_15";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var datoReporte = new ReporteIncidenteDTO()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("ID_INCIDENTE")),
                                    Tipo = GetString(reader, "TIP_INCIDENTE"),
                                    Escenario = GetString(reader, "DES_ESCENARIO"),
                                    FechaInicio = GetString(reader, "FEC_INICIO"),
                                    HoraInicio = GetString(reader, "HOR_INICIO"),
                                    FechaFin = GetString(reader, "FEC_FIN"),
                                    HoraFin = GetString(reader, "HOR_FIN"),
                                    Solucionado = GetString(reader, "DES_SOLUCIONADO"),
                                    SuperoRTO = GetString(reader, "DES_SUPERO_RTO"),
                                    ServiciosImpactados = GetString(reader, "DES_SERV_IMPACTADOS"),
                                    EntidadesImpactadas = GetString(reader, "DES_IMPACTO"),
                                    DescripcionIncidente = GetString(reader, "DES_INCIDENTE"),
                                    AccionesEjecutadas = GetString(reader, "DES_ACCION_EJECUTADAS"),
                                    PaseProduccion = GetString(reader, "DES_PASE_PRODUCCION"),
                                    Indisponibilidad = GetString(reader, "DES_INDISPONIBILIDAD"),
                                    TiempoIndisp = GetString(reader, "NUM_TIEMPO"),
                                    CantidadPagosAfectados = GetString(reader, "NUM_PAGOS"),
                                    ValorPagosAfectados = reader.GetDecimal(reader.GetOrdinal("VAL_PAGOS")),
                                    Recomendaciones = GetString(reader, "DES_RECOMENDACIONES"),
                                    Comentarios = GetString(reader, "DES_COMENTARIOS")
                                };
                                datosReporte.Add(datoReporte);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Objeto de tiempo de Recuperación del Reporte Anexo 16
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReporteObjetivoTiempoRecuperacionDTO> ObtenerDatosReporteObjetoTiempoRecuperacion(string anio, string mes)
        {
            var datosReporte = new List<ReporteObjetivoTiempoRecuperacionDTO>();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_OBJETO_TIEMPO_RECUPERACION_ANEXO_16";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var datoReporte = new ReporteObjetivoTiempoRecuperacionDTO()
                                {
                                    Tipo = GetString(reader, "TIP_INCIDENTE"),
                                    CantidadIncidencias = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_INCIDENTES")),
                                    TiempoRTO = reader.GetInt32(reader.GetOrdinal("VAL_TIEMPO_RTO")),
                                    CantidadIncidenciasRTO = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_INCIDENTES_SUPERO_RTO")),
                                };
                                datosReporte.Add(datoReporte);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de no disponibilidad Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteICDNoDisponibilidadDTO ObtenerDatosCalidadServicioNoDisponibilidad(string anio, string mes, string dia, Periodo periodo, string frecuencia)
        {
            var datosReporte = new ReporteICDNoDisponibilidadDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_CALIDAD_SERVICIO_NO_DISPONIBILIDAD_ANEXO_17";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_FRECUENCIA", SqlDbType.VarChar)).Value = frecuencia;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.MinutosTotales = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_MINUTOS_PERIODO"));
                                datosReporte.MinutosTotalesMantenimiento = reader.GetInt32(reader.GetOrdinal("VAL_TOTAL_TIEMPO_MANTENIMIENTO"));
                                datosReporte.Comentarios = string.Empty;
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de efectividad de busqueda de alias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteICDEfectividadBusquedaAliasDTO ObtenerDatosCalidadServicioEfectividadBusquedaAlias(string anio, string mes, string dia, Periodo periodo, string frecuencia)
        {
            var datosReporte = new ReporteICDEfectividadBusquedaAliasDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_CALIDAD_SERVICIO_EFECTIVIDAD_BUSQUEDA_ALIAS_ANEXO_17";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_FRECUENCIA", SqlDbType.VarChar)).Value = frecuencia;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.CantidadIntentosTotalesConsulta = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL"));
                                datosReporte.CantidadFallasCMACT = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_FALLIDAS_CMACT"));
                                datosReporte.CantidadFallasCCE = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_FALLIDAS_CCE"));
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de efectividad de Transferencia Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteICDEfectividadTransferenciasDTO ObtenerDatosCalidadServicioEfectividadTransferencias(string anio, string mes, string dia, Periodo periodo, string frecuencia)
        {
            var datosReporte = new ReporteICDEfectividadTransferenciasDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_CALIDAD_SERVICIO_EFECTIVIDAD_TRANSFERENCIAS_ANEXO_17";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_FRECUENCIA", SqlDbType.VarChar)).Value = frecuencia;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.CantidadIntentosTotalesTransferencia = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL"));
                                datosReporte.CantidadFallasCMACT = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_FALLIDAS_CMACT"));
                                datosReporte.CantidadFallasCCE = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_FALLIDAS_CCE"));
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de rendimiento de Transferencias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReporteICDRendimientoDTO> ObtenerDatosCalidadServicioRendimientoBusquedaAlias(string anio, string mes, string dia, Periodo periodo, string frecuencia)
        {
            var datosReporte = new List<ReporteICDRendimientoDTO>();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_CALIDAD_SERVICIO_RENDIMIENTO_BUSQUEDA_ALIAS_ANEXO_17";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_FRECUENCIA", SqlDbType.VarChar)).Value = frecuencia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@ADC_TIEMPO_MINIMO", SqlDbType.Decimal)).Value = periodo.ConsultaTiempoMinimo;
                    loSqlCommand.Parameters.Add(new SqlParameter("@ADC_TIEMPO_MAXIMO", SqlDbType.Decimal)).Value = periodo.ConsultaTiempoMaximo;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var datoReporte = new ReporteICDRendimientoDTO
                                {
                                    TipoICD = GetString(reader, "TIP_ICD"),
                                    CantidadTotales = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_CONSULTAS")),
                                    CantidadMenorIgualT = reader.GetInt32(reader.GetOrdinal("VAL_CONSULTAS_TIEMPO_MINIMO")),
                                    CantidadMayorIgualTMax = reader.GetInt32(reader.GetOrdinal("VAL_CONSULTAS_TIEMPO_MAXIMO")),
                                    Comentario = string.Empty
                                };
                                datosReporte.Add(datoReporte);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Calidad de servicio de rendimiento de Transferencias Reporte Anexo 17
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <param name="frecuencia"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ReporteICDRendimientoDTO> ObtenerDatosCalidadServicioRendimientoTransferencias(string anio, string mes, string dia, Periodo periodo, string frecuencia)
        {
            var datosReporte = new List<ReporteICDRendimientoDTO>();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_CALIDAD_SERVICIO_RENDIMIENTO_TRANSFERENCIAS_ANEXO_17";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_FRECUENCIA", SqlDbType.VarChar)).Value = frecuencia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@ADC_TIEMPO_MINIMO", SqlDbType.Decimal)).Value = periodo.ConsultaTiempoMinimo;
                    loSqlCommand.Parameters.Add(new SqlParameter("@ADC_TIEMPO_MAXIMO", SqlDbType.Decimal)).Value = periodo.ConsultaTiempoMaximo;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var datoReporte = new ReporteICDRendimientoDTO
                                {
                                    TipoICD = GetString(reader, "TIP_ICD"),
                                    CantidadTotales = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_TRANSFERENCIAS")),
                                    CantidadMenorIgualT = reader.GetInt32(reader.GetOrdinal("VAL_TRANSFERENCIAS_TIEMPO_MINIMO")),
                                    CantidadMayorIgualTMax = reader.GetInt32(reader.GetOrdinal("VAL_TRANSFERENCIAS_TIEMPO_MAXIMO")),
                                    Comentario = string.Empty
                                };
                                datosReporte.Add(datoReporte);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Resumen de Consultas del Reporte Anexo 19
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteResumenConsultaDTO ObtenerDatosResumenConsultas(string anio, string mes, string dia, Periodo periodo)
        {
            var datosReporte = new ReporteResumenConsultaDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_RESUMEN_CONSULTAS_ANEXO_19";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.CantidadConsultasTotales = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL"));
                                datosReporte.CantidadConsultasExitosas = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_EXITOSOS"));
                                datosReporte.CantidadConsultasErradas = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_ERRADAS"));
                                datosReporte.CantidadConsultasQR = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_QR"));
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Resumen de Consultas del Reporte Anexo 19
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="periodo"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteResumenTransaccionDTO ObtenerDatosResumenTransacciones(string anio, string mes, string dia, Periodo periodo)
        {
            var datosReporte = new ReporteResumenTransaccionDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_RESUMEN_TRANSACCIONES_ANEXO_19";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_DIA", SqlDbType.VarChar)).Value = dia;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_INICIO", SqlDbType.VarChar)).Value = periodo.HoraDesde;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_HORA_FIN", SqlDbType.VarChar)).Value = periodo.HoraHasta;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.TransaccionesAprobadasNumero = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_APROBADAS"));
                                datosReporte.TransaccionesAprobadasMonto = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL_APROBADAS"));
                                datosReporte.TransaccionesDenegadasNumero = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_RECHAZADAS"));
                                datosReporte.TransaccionesDenegadasMonto = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL_RECHAZADAS"));
                                datosReporte.TransaccionesP2PNumero = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_P2P"));
                                datosReporte.TransaccionesP2PMonto = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL_P2P"));
                                datosReporte.TransaccionesP2MNumero = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_P2M"));
                                datosReporte.TransaccionesP2MMonto = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL_P2M"));
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                var transaccionAprobada = new TransaccionesAprobadasDTO
                                {
                                    CodigoEntidadBeneficiaria = GetString(reader, "COD_ENTIDAD_RECEPTOR"),
                                    NumeroTransaccionesAprobadas = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL")),
                                    MontoTransaccionesAprobadas = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL"))
                                };
                                datosReporte.TransaccionesAprobadas.Add(transaccionAprobada);
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                var transaccionDenegadas = new TransaccionesDenegadasDTO
                                {
                                    CodigoEntidadBeneficiaria = GetString(reader, "COD_ENTIDAD_RECEPTOR"),
                                    IdentificadorCodigoNegacion = GetString(reader, "COD_RAZON"),
                                    NumeroTransaccionesDenegadas = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL")),
                                    MontoTransaccionesDenegadas = reader.GetDecimal(reader.GetOrdinal("VAL_MONTO_TOTAL"))
                                };
                                datosReporte.TransaccionesDenegadas.Add(transaccionDenegadas);
                            }
                        }

                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                var transaccionPorRangoMonto = new TransaccionesPorRangoMontoDTO
                                {
                                    CodigoEntidadBeneficiaria = GetString(reader, "COD_ENTIDAD_RECEPTOR"),
                                    NumeroTransaccionesMenorIgual20 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_MENOR_20")),
                                    NumeroTransaccionesMayor20MenorIgual50 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_MAYOR_20_MENOR_50")),
                                    NumeroTransaccionesMayor50MenorIgual100 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_MAYOR_50_MENOR_100")),
                                    NumeroTransaccionesMayor100MenorIgual200 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_MAYOR_100_MENOR_200")),
                                    NumeroTransaccionesMayor200 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_MAYOR_200")),
                                };
                                datosReporte.TransaccionesPorRangoMonto.Add(transaccionPorRangoMonto);
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos de Variación de usuarios y montos de transferencias del Reporte Anexo 20
        /// </summary>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ReporteVariacionUsuarioMontoTransferenciasDTO ObtenerDatosReporteVariacionUsuarioMontoTransferencias(string anio, string mes)
        {
            var datosReporte = new ReporteVariacionUsuarioMontoTransferenciasDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_REPORTE_VARIACION_USUARIO_TRANSFERENCIA_ANEXO_20";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_ANIO", SqlDbType.VarChar)).Value = anio;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_MES", SqlDbType.VarChar)).Value = mes;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosReporte.NumeroUsuariosTotal = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_TOTAL_USUARIOS"));
                                datosReporte.NumeroUsuariosNuevos = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_NUEVOS"));
                                datosReporte.NumeroUsuariosActivos = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_ACTIVOS"));
                                datosReporte.NumeroUsuariosTxP2P = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_P2P"));
                                datosReporte.NumeroUsuariosTxP2M = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_P2M"));
                                datosReporte.NumeroUsuariosTxMenorOigual20 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_MENOR_20"));
                                datosReporte.NumeroUsuariosTxMayor20MenorOigual50 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_20_50"));
                                datosReporte.NumeroUsuariosTxMayor50MenorOigual100 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_50_100"));
                                datosReporte.NumeroUsuariosTxMayor100MenorOigual200 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_100_200"));
                                datosReporte.NumeroUsuariosTxMayor200 = reader.GetInt32(reader.GetOrdinal("VAL_CANTIDAD_USUARIOS_TX_MAYOR_200"));
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosReporte;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Método que verifica si es dia habil
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool VerificarSiEsDiaHabil(DateTime fecha)
        {
            string resultado;
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "SELECT CF.FN_VERIFICAR_ES_DIA_HABIL_CCE(@fecha)";
                    loSqlCommand.CommandType = CommandType.Text;
                    loSqlCommand.Parameters.Add(new SqlParameter("@fecha", SqlDbType.Date) { Value = fecha });

                    resultado = loSqlCommand.ExecuteScalar().ToString()!;

                    loSqlConnection.Close();
                }
                return resultado == General.Si;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }

        /// <summary>
        /// Verifica si es NUll y devuelve el valor
        /// </summary>
        /// <param name="lectura"></param>
        /// <param name="nombreColumna"></param>
        /// <returns></returns>
        public string GetString(IDataReader lectura, string nombreColumna)
        {
            int ordinal = lectura.GetOrdinal(nombreColumna);
            return lectura.IsDBNull(ordinal) ? string.Empty : lectura.GetString(ordinal);
        }
    }
}
