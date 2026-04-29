using Takana.Transferencias.CCE.Api.Common;
using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;


namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion
{
    /// <summary>
    /// Servicio encargado de recibir datos de los canales y armar el esquema para enviarselo a la Operadora
    /// </summary>
    public class ServicioAplicacionReporte : ServicioBase, IServicioAplicacionReporte
    {
        private readonly IServiceProvider _servicioProvider;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IServicioAplicacionSFTP _servicioAplicacionSFTP;
        private readonly IServicioAplicacionColas _servicioAplicacionColas;
        private readonly IServicioAplicacionAfiliacion _servicioAplicacionAfiliacion;

        /// <summary>
        /// Método Constructor
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="servicioProvider"></param>
        /// <param name="repositorioGeneral"></param>
        /// <param name="servicioAplicacionSFTP"></param>
        /// <param name="servicioAplicacionColas"></param>
        /// <param name="servicioAplicacionAfiliacion"></param>
        public ServicioAplicacionReporte(
            IContextoAplicacion contexto,
            IServiceProvider servicioProvider,
            IRepositorioGeneral repositorioGeneral,
            IServicioAplicacionSFTP servicioAplicacionSFTP,
            IServicioAplicacionColas servicioAplicacionColas,
            IServicioAplicacionAfiliacion servicioAplicacionAfiliacion) : base(contexto)
        {
            _servicioProvider = servicioProvider;
            _repositorioGeneral = repositorioGeneral;
            _servicioAplicacionSFTP = servicioAplicacionSFTP;
            _servicioAplicacionColas = servicioAplicacionColas;
            _servicioAplicacionAfiliacion = servicioAplicacionAfiliacion;
        }

        #region Generar Archivo de Reportes de Interoperablidad

        /// <summary>
        /// Método que valida y genera si es dia habil para generar el reporte
        /// </summary>
        /// <returns></returns>
        public async Task<List<int>> GenerarArchivoReporteManual(
            GenerarReporteDTO datos,
            bool esReporteIndividual,
            IConfiguracionSFTP configSFTP,
            IConfiguracionSFTP configCanalElectronicoWorkstation)
        {
            var idsReportes = new List<int>();

            var datosReporte = datos.FechaReporte.AdtoGenerarReporte(datos.IdTipoReporte, datos.Usuario);

            var idReporte = await GenerarArchivoReporte(datosReporte, esReporteIndividual,
                datos.FechaReporte, configSFTP, configCanalElectronicoWorkstation);

            idsReportes.Add(idReporte);

            return idsReportes;
        }

        /// <summary>
        /// Método que genera el archivo de reportes para la Circular-009-2024
        /// </summary>
        /// <param name="datos"></param>
        /// <param name="esReporteIndividual"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<int> GenerarArchivoReporte(
            List<GenerarReporteDTO> datos, 
            bool esReporteIndividual,
            DateTime fechaReporte,
            IConfiguracionSFTP configSFTP,
            IConfiguracionSFTP configCanalElectronicoWorkstation)
        {
            try
            {
                var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

                var periodo = ObtenerPeriodo();

                var dato = datos.OrderByDescending(d => d.FechaReporte).First();

                var estrategia = _servicioProvider
                    .GetRequiredKeyedService<IServicioGeneracionArchivoEstrategia>(dato.IdTipoReporte);
                byte[] archivoCSV = await estrategia.GenerarArchivo(datos, periodo);
                string nombreArchivo = estrategia.ObtenerNombreArchivo(dato);

                var reporte = RegistrarReporte(nombreArchivo, archivoCSV, dato.Usuario, periodo.Id,
                    dato.IdTipoReporte, fechaReporte, fechaSistema.FechaHoraSistema);

                if (esReporteIndividual)
                {
                    var archivos = reporte.AdtoFormatosArchivosAdjuntos(ArchivoAdjuntoDTO.TipoArchivoCSV);

                    await _servicioAplicacionSFTP.SubirArchivoSFTP(archivos, configSFTP);

                    ActualizarIndicadorSFTP(reporte, fechaSistema.FechaHoraSistema);

                    await _servicioAplicacionSFTP.CopiarArchivoCarpetaCompartida(archivos, configCanalElectronicoWorkstation);

                    await ArmarYEnviarCorreoReportesAsync(reporte, fechaSistema.FechaHoraSistema);
                }

                return reporte.Id;
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }

        /// <summary>
        /// Método que genera el archivo de directorio de clientes seleccionados
        /// </summary>
        /// <param name="datosArchivo"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<int> GenerarArchivoDirectorioClienteSeleccionado(
            ArchivoDirectorioClienteDTO datosArchivo, IConfiguracionSFTP configSFTP)
        {
            try
            {
                var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

                var periodo = ObtenerPeriodo();

                var estrategia = new DirectorioInteroperabilidadDiarioEstrategia(_repositorioGeneral);
                byte[] archivoCSV = await estrategia.GenerarArchivoCSV(datosArchivo.DatosArchivoDirectorio);

                int tipoReporte = (int)GenerarReporteDTO.TipoReporte.DirectorioInteroperabilidadDiario;

                var reporte = RegistrarReporte(datosArchivo.NombreArchivo, archivoCSV, datosArchivo.Usuario, 
                    periodo.Id, tipoReporte, fechaSistema.FechaHoraSistema, fechaSistema.FechaHoraSistema);

                await _servicioAplicacionSFTP.SubirArchivoSFTP(reporte.AdtoFormatosArchivosAdjuntos(
                    ArchivoAdjuntoDTO.TipoArchivoCSV), configSFTP);

                ActualizarIndicadorSFTP(reporte, fechaSistema.FechaHoraSistema);

                await ArmarYEnviarCorreoReportesAsync(reporte, fechaSistema.FechaHoraSistema);

                return reporte.Id;
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }

        /// <summary>
        /// Método que verifica la respuesta del Directorio de la CCE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="configSFTP"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public async Task<bool> VerificarRespuestaDirectorioCCE(int id, IConfiguracionSFTP configSFTP)
        {
            try
            {
                var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

                var reporte = _repositorioGeneral.ObtenerPorCodigo<Reporte>(id);

                var nombreArchivo = reporte.Nombre.Replace(".csv", ".ctl");

                byte[] archivo = await _servicioAplicacionSFTP.ObtenerArchivoSFTP(nombreArchivo, configSFTP);

                await _servicioAplicacionAfiliacion.RegistrarDatosProcesadosBitacoraAfiliacion(reporte.Contenido);

                var archivosAdjuntos = new List<ArchivoAdjuntoDTO> 
                {
                    reporte.Contenido.AdtoFormatoArchivoAdjunto(reporte.Nombre, ArchivoAdjuntoDTO.TipoArchivoCSV),
                    archivo.AdtoFormatoArchivoAdjunto(nombreArchivo, ArchivoAdjuntoDTO.TipoArchivoGenerico)
                };

                await EnviarCorreoPorRabitAsync(fechaSistema.FechaHoraSistema, 
                    Reporte.DescripcionRespuestaDirectorio, 
                    CorreoGeneralDTO.DescripcionArchivorRespuestaSFTP,
                    Reporte.DescripcionRespuestaDirectorio,
                    archivosAdjuntos);

                return true;
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }
        #endregion

        #region Subir archivos al SFTP y Enviar Correos
        /// <summary>
        /// Método que sube el archivo al servicio SFTP de la CCE
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enviarCorreo"></param>
        public async Task<bool> SubirArchivoSFTP(
            int id,
            bool enviarCorreo,
            IConfiguracionSFTP configSFTP)
        {
            try
            {
                var fechaSistema = _repositorioGeneral
                    .ObtenerCalendarioCuentaEfectivo();

                var reporte = _repositorioGeneral.ObtenerPorCodigo<Reporte>(id);

                reporte.ValidarEstadoReporte();

                await _servicioAplicacionSFTP.SubirArchivoSFTP(reporte.AdtoFormatosArchivosAdjuntos(
                    ArchivoAdjuntoDTO.TipoArchivoCSV), configSFTP);

                ActualizarIndicadorSFTP(reporte, fechaSistema.FechaHoraSistema);

                if (enviarCorreo)
                    await ArmarYEnviarCorreoReportesAsync(reporte, fechaSistema.FechaHoraSistema);

                return true;
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }

        /// <summary>
        /// Metodo que sube y envia un grupo de reportes adjuntos 
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <param name="idsReportes"></param>
        /// <param name="frecuencia"></param>
        /// <param name="configSFTP"></param>
        /// <param name="configCanalElectronicoWorkstation"></param>
        /// <returns></returns>
        public async Task SubirArchivosYEnviarCorreosPorGrupo(
            DateTime fechaSistema, 
            List<int> idsReportes, 
            string frecuencia,
            IConfiguracionSFTP configSFTP,
            IConfiguracionSFTP configCanalElectronicoWorkstation)
        {
            var reportes = _repositorioGeneral
                .ObtenerPorExpresionConLimite<Reporte>()
                .Where(x => idsReportes.Contains(x.Id))
                .ToList();

            var archivosAdjuntos = reportes.AdtoFormatosArchivosAdjuntos(ArchivoAdjuntoDTO.TipoArchivoCSV);

            var servicioDescripcion = frecuencia == GenerarReporteDTO.FrecuenciaDiaria
                ? Reporte.DescripcionReportesDiarios
                : Reporte.DescripcionReportesMensuales;

            var temaMensaje = CorreoGeneralDTO.DescripcionArchivoTemaMensaje;

            await _servicioAplicacionSFTP.SubirArchivoSFTP(archivosAdjuntos, configSFTP);

            reportes.ForEach(r => r.ActualizarIndicadorSFTP(fechaSistema));
            _repositorioGeneral.GuardarCambios();

            await _servicioAplicacionSFTP.CopiarArchivoCarpetaCompartida(archivosAdjuntos, configCanalElectronicoWorkstation);

            await EnviarCorreoPorRabitAsync(fechaSistema, servicioDescripcion,
                CorreoGeneralDTO.DescripcionArchivoEnviadoSFTP, temaMensaje, archivosAdjuntos);
        }

        /// <summary>
        /// Método que sube el archivo al servicio SFTP de la CCE
        /// </summary>
        /// <param name="id"></param>
        private async Task ArmarYEnviarCorreoReportesAsync(Reporte reporte, DateTime fechaSistema)
        {
            try
            {
                var estrategia = _servicioProvider
                    .GetRequiredKeyedService<IServicioGeneracionArchivoEstrategia>(reporte.IdTipoReporte);

                string servicioDescripcion = estrategia.ObtenerDescripcionReporte();
                string temaMensaje = estrategia.ObtenerDescripcionTemaMensaje();

                var archivoAdjunto = reporte.Contenido.AdtoFormatoArchivoAdjunto(reporte.Nombre, ArchivoAdjuntoDTO.TipoArchivoCSV);

                var archivosAdjuntos = new List<ArchivoAdjuntoDTO> { archivoAdjunto };

                await EnviarCorreoPorRabitAsync(fechaSistema, servicioDescripcion, CorreoGeneralDTO.DescripcionArchivoEnviadoSFTP, temaMensaje, archivosAdjuntos);
            }
            catch (Exception excepcion)
            {
                throw new ValidacionException(excepcion.Message);
            }
        }

        /// <summary>
        /// Método que envia el correo electronico y el archivo
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <param name="servicio"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        private async Task EnviarCorreoPorRabitAsync(
            DateTime fechaSistema,
            string servicio,
            string descripcion,
            string temaMensaje,
            IList<ArchivoAdjuntoDTO> archivosAdjuntos)
        {
            var correoRemitente = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoAdministrador)
                .First().ValorParametro;

            var correosDestinatarios = _repositorioGeneral
                .ObtenerPorExpresionConLimite<ParametroGeneralTransferencia>(x =>
                    x.CodigoParametro == ParametroGeneralTransferencia.CorreoElectronicoDestinatariosReporte)
                .First().ValorParametro;

            var correosElectronicosDestinatarios = correosDestinatarios.Split(";");

            foreach (var correoDestino in correosElectronicosDestinatarios)
            {
                var formatoCorreo = _contextoAplicacion.AdtoFormatoCorreo(fechaSistema, correoRemitente,
                    correoDestino, servicio, descripcion, temaMensaje, archivosAdjuntos);

                await _servicioAplicacionColas.EnviarCorreoAsync(CorreoGeneralDTO.CodigoReporteInteroperabilidad, formatoCorreo);
            }
        }
        #endregion

        #region Utilidades
        /// <summary>
        /// Registrar el reporte de Anexo
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="contenido"></param>
        /// <param name="usuario"></param>
        /// <param name="idPeriodo"></param>
        /// <param name="idTipoReporte"></param>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        private Reporte RegistrarReporte(
            string nombreArchivo,
            byte[] contenido,
            string usuario,
            int idPeriodo,
            int idTipoReporte,
            DateTime fechaReporte,
            DateTime fechaSistema)
        {
            var reporte = Reporte.RegistrarReporte(nombreArchivo, contenido, 
                usuario, idPeriodo, idTipoReporte, fechaReporte, fechaSistema);
            _repositorioGeneral.Adicionar(reporte);
            _repositorioGeneral.GuardarCambios();
            return reporte;
        }

        /// <summary>
        /// Actualiza el indicador de subido al SFTP
        /// </summary>
        /// <param name="reporte"></param>
        private void ActualizarIndicadorSFTP(Reporte reporte, DateTime fechaSistema)
        {
            reporte.ActualizarIndicadorSFTP(fechaSistema);
            _repositorioGeneral.GuardarCambios();
        }

        /// <summary>
        /// Obtener periodo
        /// </summary>
        /// <returns></returns>
        private Periodo ObtenerPeriodo()
        {
            return _repositorioGeneral
                .ObtenerPorExpresionConLimite<Periodo>(x =>
                    x.IndicadorEstado == General.Activo)
                .FirstOrDefault()
                .ValidarEntidad();
        }

        /// <summary>
        /// Método que verifica si es el primer dia habil del Mes
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns>Retorna True si es Habil del Mes</returns>
        public bool EsPrimerDiaHabilDelMes(DateTime fecha)
        {
            var primerDia = new DateTime(fecha.Year, fecha.Month, 1);

            while (!_repositorioGeneral.VerificarSiEsDiaHabil(primerDia))
            {
                primerDia = primerDia.AddDays(1);
            }

            return fecha.Date == primerDia.Date;
        }

        /// <summary>
        /// Método que verifica si ya se envio el reporte mensual
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna True si Existe</returns>
        public bool SeEnvioReporteMensual(DateTime fechaSistema)
        {
            return _repositorioGeneral
                .ObtenerPorExpresionConLimite<Reporte>(x =>
                    x.FechaRegistro.Year == fechaSistema.Year &&
                    x.FechaRegistro.Month == fechaSistema.Month &&
                    x.CodigoUsuarioRegistro == General.UsuarioPorDefecto)
                .Any();
        }

        /// <summary>
        /// Método que obtiene la fechas para generar reportes diarios
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <returns>Retorna True si Existe</returns>
        public List<DateTime> ObtenerFechasGenerarReporteDiario(DateTime fechaSistema)
        {
            var fechasReportes = new List<DateTime> { fechaSistema };
            DateTime fechaAnterior = fechaSistema.AddDays(-1);

            while (!_repositorioGeneral.VerificarSiEsDiaHabil(fechaAnterior))
            {
                fechasReportes.Add(fechaAnterior);
                fechaAnterior = fechaAnterior.AddDays(-1);
            }

            return fechasReportes.OrderBy(f => f).ToList();
        }
        #endregion
    }
}