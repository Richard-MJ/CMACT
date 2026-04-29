using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase capa Aplicacion encargado de las validaciones para transferencias inmediatas
    /// </summary>
    public class ServicioAplicacionValidacion : IServicioAplicacionValidacion
    {
        private readonly IRepositorioRedis _repositorioRedis;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperaciones;
        private readonly IServicioAplicacionParametroGeneral _parametroGeneralServicio;
        private readonly IServicioAplicacionParametroGeneral _serivicioAplicacionParametro;
        
        public ServicioAplicacionValidacion(
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperaciones,
            IRepositorioRedis repositorioRedis,
            IServicioAplicacionParametroGeneral parametroGeneralServicio,
            IServicioAplicacionParametroGeneral serivicioAplicacionParametro)
        {
            _repositorioRedis = repositorioRedis;
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperaciones = repositorioOperaciones;
            _parametroGeneralServicio = parametroGeneralServicio;
            _serivicioAplicacionParametro = serivicioAplicacionParametro;
        }


        /// <summary>
        /// Valida las reglas que la IPS plantea en su documentacion
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna True si pasa las validaciones</returns>
        public void ValidarReglasIPS(ConsultaCanalDTO datos)
        {
            ServicioDominioValidacion.ValidarIndicadores(datos);

            var codigoEntidadReceptora = datos.AcreedorCCI != null
                ? General.Cero + datos.AcreedorCCI.Substring(0, 3) : datos.EntidadReceptora;

            datos.EntidadOriginante = General.Cero + datos.CodigoCuentaInterbancarioOriginante.Substring(0, 3);
            datos.EntidadReceptora = codigoEntidadReceptora;

            if ((datos.Canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad))
            {
                if (datos.EntidadOriginante == datos.EntidadReceptora)
                    throw new Exception("El número de celular (destino), no puede ser el mismo de la Entidad Financiera.");
                if ((datos.ValorProxy != null || datos.TipoProxy != null)) return;
            }
            else ServicioDominioValidacion.ValidarEstructuraNumeroDestino(datos);

            var directorios = _repositorioOperaciones.ObtenerPorExpresionConLimite<DirectorioInteroperabilidad>(
                x => x.CodigoDirectorio != DatosGeneralesInteroperabilidad.DirectorioCCE).ValidarEntidadTexto("Directorios");

            var nombreDirectorio = ServicioDominioInteroperabilidad.ObtenerNombreDirectorio(codigoEntidadReceptora, directorios.ToList());

            if (nombreDirectorio != null)  return;

            VerificarEstadoEntidades(datos.EntidadOriginante, datos.EntidadReceptora);
            VerificarEntidadesHabilitadas(datos);
        }

        /// <summary>
        /// Metodo de verifica el estado de las entidades originante y receptor
        /// </summary>
        /// <param name="originante">Codigo de entidad originante</param>
        /// <param name="receptora">Codigo de entidad receptora</param>
        /// <returns></returns>
        public List<EntidadFinancieraInmediata> VerificarEstadoEntidades(string originante, string receptora)
        {
            var estadoSistema = _parametroGeneralServicio.obtenerParametrosConfiguracion(ParametroGeneralTransferencia.EstadoSistema);

            var entidades = _repositorioGeneral.ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>()
                .Where(m => m.CodigoEntidad == originante
                        || m.CodigoEntidad == receptora).ToList();

            ServicioDominioValidacion.VerificarEstadoEntidades(originante, receptora, estadoSistema, entidades);

            return entidades;
        }
        /// <summary>
        /// Verifica que la entidad originante y receptora esten habilitadas
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna True si es que pasan esta validacion</returns>
        public  void VerificarEntidadesHabilitadas(
            ConsultaCanalDTO datos)
        {

            var entidades = VerificarEstadoEntidades(datos.EntidadOriginante, datos.EntidadReceptora);

            var originante = entidades.FirstOrDefault(m => m.CodigoEntidad == datos.EntidadOriginante);
            var receptor = entidades.FirstOrDefault(m => m.CodigoEntidad == datos.EntidadReceptora);

            var resultado = _repositorioGeneral.ObtenerPorExpresionConLimite<EntidadFinancieraPorTransferencia>()
            .Where(
                m => (m.IdEntidad == originante.IdentificadorEntidad
                    && m.TipoTransferencia.Codigo == datos.TipoTransaccion.ToString()
                    && m.IndicadorParticipanteOriginante == General.Originante)
                    || (m.IdEntidad == receptor.IdentificadorEntidad
                    && m.TipoTransferencia.Codigo == datos.TipoTransaccion.ToString()
                    && m.IndicadorParticipanteReceptor == General.Receptor)
            );

            if (resultado == null || resultado.Count() != 2)
                throw new Exception("Al menos una de las entidades no tiene habilitada el estado de Originante o Receptor");

        }
        /// <summary>
        /// Metodo que valida servicio de interoperabilidad
        /// </summary>
        /// <param name="fechaSistema">fecha y hora del sistema</param>
        /// <param name="afiliacion">datos de afiliacion</param>
        /// <param name="datosEntrada">Datos de entrada</param>
        public async Task ValidarServicioInteroperabilidad(
            Calendario fechaSistema,
            AfiliacionInteroperabilidadDetalle afiliacion,
            EntradaBarridoDTO datosEntrada)
        {
            var intentosMaximos = int.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                    DatosGeneralesInteroperabilidad.ExcesoOperacionesBarrido));
            var horasBloqueo = int.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                    DatosGeneralesInteroperabilidad.TiempoBloqueoServicio));

            var ttlBloqueo = await _repositorioRedis.ObtenerTiempoVidaAsync(afiliacion.KeyBloqueoBarrido);
            if (ttlBloqueo.HasValue && ttlBloqueo.Value > TimeSpan.Zero)
            {
                throw new ValidacionException(
                    $"Esta bloqueado del servicio por " +
                    $"{ttlBloqueo.Value.Hours}hrs {ttlBloqueo.Value.Minutes}min.");
            }

            var intentosActuales = await _repositorioRedis.GetAsync<int>(afiliacion.KeyIntentosBarrido);
            if ((intentosActuales + 1) > intentosMaximos)
            {
                await _repositorioRedis.SetAsync(afiliacion.KeyBloqueoBarrido,true,TimeSpan.FromHours(horasBloqueo));
                throw new ValidacionException("Se ha bloqueado el servicio por exceso de operaciones.");
            }

            ServicioDominioInteroperabilidad.ValidarNumerosCelulares(datosEntrada.ContactosBarrido);
        }


    }
}
