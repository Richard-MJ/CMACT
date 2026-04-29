using Hangfire;
using System;
using System.Threading.Tasks;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.ServiciosExternos;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;

namespace Takana.Transferencias.CCE.Api.Servicio
{
    /// <summary>
    /// Clase de Tareas programadas
    /// </summary>
    public class TareasProgramadasServices
    {
        private readonly RecurringJobOptions _opciones;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IConfiguracionReporteSFTP _configReporteSFTP;
        private readonly IBitacora<TareasProgramadasServices> _bitacora;
        private readonly IConfiguracionDirectorioSFTP _configDirectorioSFTP;
        private readonly IServicioAplicacionReporte _servicioAplicacionReporte;
        private readonly IConfiguracionCanalElectronicoWorkstation _configCanalElectronicoWorkstation;
        private readonly IServicioAplicacionTransferenciaSalida _servicioAplicacionTransferenciaSalida;
        private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="repositorioGeneral"></param>
        /// <param name="configReporteSFTP"></param>
        /// <param name="configDirectorioSFTP"></param>
        /// <param name="servicioAplicacionReporte"></param>
        /// <param name="configCanalElectronicoSFTP"></param>
        /// <param name="servicioAplicacionTransferenciaSalida"></param>
        public TareasProgramadasServices(
            IRepositorioGeneral repositorioGeneral,
            IConfiguracionReporteSFTP configReporteSFTP,
            IBitacora<TareasProgramadasServices> bitacora,
            IConfiguracionDirectorioSFTP configDirectorioSFTP,
            IServicioAplicacionReporte servicioAplicacionReporte,
            IServicioAplicacionTransferenciaSalida servicioAplicacionTransferenciaSalida,
            IConfiguracionCanalElectronicoWorkstation configCanalElectronicoSFTP,
            IServicioAplicacionPeticion servicioAplicacionPeticion)
        {
            _bitacora = bitacora;
            _configReporteSFTP = configReporteSFTP;
            _repositorioGeneral = repositorioGeneral;
            _configDirectorioSFTP = configDirectorioSFTP;
            _servicioAplicacionReporte = servicioAplicacionReporte;
            _configCanalElectronicoWorkstation = configCanalElectronicoSFTP;
            _servicioAplicacionTransferenciaSalida = servicioAplicacionTransferenciaSalida;
            _servicioAplicacionPeticion = servicioAplicacionPeticion;
            _opciones = new RecurringJobOptions 
            { 
                TimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time") 
            };
        }

        /// <summary>
        /// Método que programa las tareas con HangFire
        /// </summary>
        /// <returns></returns>
        public void ProgramarTareasHangFire()
        {
            ProgramarTareasReportesCCE();

            ProgramarTareasManualSignOnOffCCE();

            ProgramarTareasProgamadaSignOnOffCCE();

            ProgramarTareasManualEchoTestCCE();
        }

        #region Tarea programada para sign On - Off y Echo Test
        /// <summary>
        /// Método que programa las tareas manuales para sign on y off
        /// </summary>
        private void ProgramarTareasManualSignOnOffCCE()
        {
            RecurringJob.AddOrUpdate(SignOnOffDTO.TareaManualSignOn,
                () => _servicioAplicacionTransferenciaSalida.SignOn(),
                Cron.Never(), _opciones);

            RecurringJob.AddOrUpdate(SignOnOffDTO.TareaManualSignOff,
                () => _servicioAplicacionTransferenciaSalida.SignOff(),
                Cron.Never(), _opciones);
        }
        /// <summary>
        /// Programa tareas con hora fija para SignOn y SignOff
        /// </summary>
        private void ProgramarTareasProgamadaSignOnOffCCE()
        {
            var periodos =  _repositorioGeneral.ObtenerPorExpresionConLimite
                <EntidadFinancieroInmediataPeriodo>(x => x.CodigoEntidad == General.CodigoEntidad);

            foreach (var periodo in periodos)
            {
                bool activo = periodo.IndicadorEstado == General.Activo;

                string jobIdSignOn = $"{SingOnOffProgramadoDTO.TareaProgramadaSignOn}-{periodo.NumeroPeriodo}";
                string jobIdSignOff = $"{SingOnOffProgramadoDTO.TareaProgramadaSignOff}-{periodo.NumeroPeriodo}";

                string cronSignOn = activo ? Cron.Daily(periodo.HoraSingOn.Hour, periodo.HoraSingOn.Minute) : Cron.Never();
                string cronSignOff = activo ? Cron.Daily(periodo.HoraSingOff.Hour, periodo.HoraSingOff.Minute) : Cron.Never();

                RecurringJob.AddOrUpdate(
                    jobIdSignOn,
                    () => _servicioAplicacionTransferenciaSalida.SignOn(),
                   cronSignOn, _opciones
                );

                RecurringJob.AddOrUpdate(
                    jobIdSignOff,
                    () => _servicioAplicacionTransferenciaSalida.SignOff(),
                    cronSignOff, _opciones
                );
            }
        }
        /// <summary>
        /// Actualiza las tareas programadas de Sign On y Sign Off asociadas a un período financiero inmediato,
        /// eliminando previamente cualquier programación existente y configurando nuevamente los trabajos
        /// recurrentes según el estado y las horas definidas en el período.
        /// </summary>
        /// <param name="periodo">
        /// Entidad del período financiero inmediato que contiene el estado y las horas de ejecución
        /// para las tareas Sign On y Sign Off.
        /// </param>
        public void ActualizarTareaPeriodoAsync(EntidadFinancieroInmediataPeriodo periodo)
        {
            if (periodo == null)
                throw new ArgumentNullException(nameof(periodo));

            string jobIdSignOn = $"{SingOnOffProgramadoDTO.TareaProgramadaSignOn}-{periodo.NumeroPeriodo}";
            string jobIdSignOff = $"{SingOnOffProgramadoDTO.TareaProgramadaSignOff}-{periodo.NumeroPeriodo}";

            bool activo = periodo.IndicadorEstado == General.Activo;

            string cronSignOn = activo ? Cron.Daily(periodo.HoraSingOn.Hour, periodo.HoraSingOn.Minute) : Cron.Never();
            string cronSignOff = activo ? Cron.Daily(periodo.HoraSingOff.Hour, periodo.HoraSingOff.Minute) : Cron.Never();

            RecurringJob.AddOrUpdate(
                jobIdSignOn,
                () => _servicioAplicacionTransferenciaSalida.SignOn(),
                cronSignOn,
                _opciones
            );

            RecurringJob.AddOrUpdate(
                jobIdSignOff,
                () => _servicioAplicacionTransferenciaSalida.SignOff(),
                cronSignOff,
                _opciones
            );

        }
        /// <summary>
        /// Método que programa las tareas manuales para sign on y off
        /// </summary>
        private void ProgramarTareasManualEchoTestCCE()
        {
            RecurringJob.AddOrUpdate(GeneralEchoTestDTO.TareaManualEchoTest,
                () => _servicioAplicacionTransferenciaSalida.EchoTest(),
                Cron.Never(), _opciones);
        }
        #endregion

        #region Tarea programada para archivos de Reportes
        /// <summary>
        /// Metodo que programa las tareas de reporte de la CCE
        /// </summary>
        private void ProgramarTareasReportesCCE()
        {
            var tiposReportes = _repositorioGeneral.Listar<TipoReporte>();

            var parametro = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(
                    x => x.CodigoParametro == ParametroGeneralTransferencia.HoraEnvioReportes)
                .First();

            int horaEnvio = int.Parse(parametro.ValorParametro);

            var listaReportesDiarios = tiposReportes
                .Where(x => x.TipoFrecuencia == GenerarReporteDTO.FrecuenciaDiaria &&
                 x.Id != (int)GenerarReporteDTO.TipoReporte.DirectorioInteroperabilidadDiario)
                .Select(x => x.Id)
                .ToList();

            var listaReportesMensual = tiposReportes
                .Where(x => x.TipoFrecuencia == GenerarReporteDTO.FrecuenciaMensual)
                .Select(x => x.Id)
                .ToList();

            RecurringJob.AddOrUpdate(GenerarReporteDTO.TareaProgramadaDiariaReportes,
                () => EvaluarEnvioReportesConMutex(
                    GenerarReporteDTO.TareaProgramadaDiariaReportes,
                    GenerarReporteDTO.FrecuenciaDiaria,
                    listaReportesDiarios),
                Cron.Daily(horaEnvio), _opciones);

            RecurringJob.AddOrUpdate(GenerarReporteDTO.TareaProgramadaMensualReportes,
                () => EvaluarEnvioReportesConMutex(
                    GenerarReporteDTO.TareaProgramadaMensualReportes,
                    GenerarReporteDTO.FrecuenciaMensual,
                    listaReportesMensual),
                Cron.Daily(horaEnvio), _opciones);
        }

        /// <summary>
        /// Método que evalua el envió de Reportes Mensuales
        /// </summary>
        /// <param name="llaveBloqueo"></param>
        /// <param name="frecuencia"></param>
        /// <param name="listaReportes"></param>
        /// <returns></returns>
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task EvaluarEnvioReportesConMutex(string llaveBloqueo, string frecuencia, List<int> listaReportes)
        {
            try
            {
                _bitacora.Trace($"Iniciando la Tarea Programada de Generación de Reportes con llave {llaveBloqueo}");

                var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

                if (!_repositorioGeneral.VerificarSiEsDiaHabil(fechaSistema.FechaHoraSistema))
                {
                    _bitacora.Trace("La fecha del sistema no es un día hábil. Tarea cancelada.");
                    return;
                }

                if (frecuencia == GenerarReporteDTO.FrecuenciaDiaria)
                    await ProcesarReportesDiarios(frecuencia, fechaSistema.FechaHoraSistema, listaReportes);
                else
                    await ProcesarReportesMensuales(frecuencia, fechaSistema.FechaHoraSistema, listaReportes);

                _bitacora.Trace($"Finalizó la Tarea Programada de Generación de Reportes con llave {llaveBloqueo}.");
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrio un error al generar el reporte: {excepcion.Message}");
            }
            finally
            {
                ProgramarTareasReportesCCE();
            }
        }

        /// <summary>
        /// Método que procesa los reportes diarios
        /// </summary>
        /// <param name="frecuencia"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="listaReportes"></param>
        /// <returns></returns>
        private async Task ProcesarReportesDiarios(string frecuencia, DateTime fechaSistema, List<int> listaReportes)
        {
            var fechasReportes = _servicioAplicacionReporte.ObtenerFechasGenerarReporteDiario(fechaSistema);

            _bitacora.Trace("Iniciando el proceso de la generación de Reportes Diarios");

            foreach (var fecha in fechasReportes)
            {
                await GenerarArchivoReporte(frecuencia, fecha, listaReportes);
            }
        }

        /// <summary>
        /// Método que procesa los reportes mensuales
        /// </summary>
        /// <param name="frecuencia"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="listaReportes"></param>
        /// <returns></returns>
        private async Task ProcesarReportesMensuales(string frecuencia, DateTime fechaSistema, List<int> listaReportes)
        {
            if (_servicioAplicacionReporte.SeEnvioReporteMensual(fechaSistema))
            {
                _bitacora.Trace("El reporte mensual ya fue enviado este mes.");
                return;
            }

            if (!_servicioAplicacionReporte.EsPrimerDiaHabilDelMes(fechaSistema))
            {
                _bitacora.Trace("Aún no es el primer día hábil del mes dentro del margen permitido.");
                return;
            }

            _bitacora.Trace("Iniciando el proceso de la generación de Reportes Mensuales");

            await GenerarArchivoReporte(frecuencia, fechaSistema, listaReportes);
        }

        /// <summary>
        /// Método que genera los reportes con mutex del Consul
        /// </summary>
        /// <param name="frecuencia"></param>
        /// <param name="fecha"></param>
        /// <param name="listaTiposReportes"></param>
        /// <returns></returns>
        public async Task GenerarArchivoReporte(string frecuencia, DateTime fecha, List<int> listaTiposReportes)
        {
            var idsReportes = new List<int>();

            foreach (var tipoReporte in listaTiposReportes)
            {
                try
                {
                    var datosReporte = new List<GenerarReporteDTO>();

                    var mesFormateado = frecuencia == GenerarReporteDTO.FrecuenciaDiaria
                        ? fecha.Month.ToString("D2")
                        : fecha.AddMonths(-1).Month.ToString("D2");

                    var datos = new GenerarReporteDTO
                    {
                        Anio = fecha.Year.ToString(),
                        Mes = mesFormateado,
                        Dia = fecha.AddDays(-1).Day.ToString("D2"),
                        IdTipoReporte = tipoReporte,
                        Usuario = General.UsuarioPorDefecto
                    };

                    datosReporte.Add(datos);

                    var idReporte = await _servicioAplicacionReporte.GenerarArchivoReporte(
                        datosReporte, false, datos.FechaReporte, _configReporteSFTP, _configCanalElectronicoWorkstation);

                    idsReportes.Add(idReporte);
                }
                catch (Exception excepcion)
                {
                    _bitacora.Error($"Error al generar el reporte con IdTipoReporte {tipoReporte}: {excepcion.Message}", excepcion);
                }
            }

            if (idsReportes.Any())
                await _servicioAplicacionReporte.SubirArchivosYEnviarCorreosPorGrupo(fecha, 
                    idsReportes, frecuencia, _configReporteSFTP, _configCanalElectronicoWorkstation);
            else
                _bitacora.Warn($"No se generó ningún reporte exitosamente con frecuencia {frecuencia}.");
        }
        #endregion

        #region Archivos Directorio
        /// <summary>
        /// Metodo que programa las tareas de reporte de la CCE
        /// </summary>
        private void ProgramarTareasDirectorioCCE()
        {
            RecurringJob.AddOrUpdate(GenerarReporteDTO.TareaProgramadaDiariaDirectorio,
                () => GenerarArchivoDirectorioConMutex(GenerarReporteDTO.TareaProgramadaDiariaDirectorio),
                Cron.Hourly(), _opciones);
        }

        /// <summary>
        /// Método que genera los reportes con mutex del Consul
        /// </summary>
        /// <param name="llaveBloqueo"></param>
        /// <returns></returns>
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task GenerarArchivoDirectorioConMutex(string llaveBloqueo)
        {
            _bitacora.Trace($"Iniciando la Tarea Programada de Generación de Archivo Masivo para Directorio de la CCE con llave {llaveBloqueo}.");

            try
            {
                var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

                var datos = new List<GenerarReporteDTO>()
                    {
                        new GenerarReporteDTO()
                        {
                            Anio = fechaSistema.FechaHoraSistema.Year.ToString(),
                            Mes = fechaSistema.FechaHoraSistema.Month.ToString("D2"),
                            Dia = fechaSistema.FechaHoraSistema.Day.ToString("D2"),
                            IdTipoReporte = (int)GenerarReporteDTO.TipoReporte.DirectorioInteroperabilidadDiario,
                            Usuario = General.UsuarioPorDefecto
                        }
                    };

                var idReporte = await _servicioAplicacionReporte.GenerarArchivoReporte(
                    datos, true, fechaSistema.FechaHoraSistema, _configDirectorioSFTP,
                    _configCanalElectronicoWorkstation);

                ProgramarTareaVerificarRepuestaDirectorioCCE(idReporte);

                _bitacora.Trace("Finalizo la Tarea Programada de Generación de Archivo Masivo para Directorio de la CCE.");
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrio un error al generar el Archivo de Directorio: {excepcion.Message}");
            }
        }

        /// <summary>
        /// Método de programación de tarea para verificar la respuesta del directorio de la CCE
        /// </summary>
        /// <param name="idReporte"></param>
        public void ProgramarTareaVerificarRepuestaDirectorioCCE(int idReporte)
        {
            RecurringJob.AddOrUpdate(GenerarReporteDTO.TareaProgramadaDiariaVerificarDirectorio,
                () => VerificarRespuestaDirectorioCCE(idReporte),
                "*/5 * * * *", _opciones);
        }

        /// <summary>
        /// Método que verifica
        /// </summary>
        /// <param name="idReporte"></param>
        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public async Task VerificarRespuestaDirectorioCCE(int idReporte)
        {
            _bitacora.Trace($"Iniciando la Tarea Programada de Verificación de Respuesta de Archivo Masivo para Directorio de la CCE.");

            var verificado = await _servicioAplicacionReporte.VerificarRespuestaDirectorioCCE(idReporte, _configDirectorioSFTP);

            if (verificado)
            {
                RecurringJob.RemoveIfExists(GenerarReporteDTO.TareaProgramadaDiariaVerificarDirectorio);
            }

            _bitacora.Trace("Finalizo la Tarea Programada de Verificación de Archivo Masivo para Directorio de la CCE.");

        }
        #endregion
    }
}