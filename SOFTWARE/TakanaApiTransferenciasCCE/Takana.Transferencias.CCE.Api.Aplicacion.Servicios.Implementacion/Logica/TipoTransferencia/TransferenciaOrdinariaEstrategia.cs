using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class TransferenciaOrdinariaEstrategia : ServicioBase, IServicioTipoTransferencia
    {
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
        private readonly IServicioDominioTransaccionOperacion _servicioDominioTransaccion;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="repositorioOperacion"></param>
        /// <param name="servicioAplicacionCliente"></param>
        public TransferenciaOrdinariaEstrategia(
            IContextoAplicacion contexto,
            IRepositorioOperacion repositorioOperacion,
            IServicioDominioTransaccionOperacion servicioDominioTransaccion,
            IServicioAplicacionCliente servicioAplicacionCliente) : base(contexto)
        {
            _repositorioOperacion = repositorioOperacion;
            _servicioAplicacionCliente = servicioAplicacionCliente;
            _servicioDominioTransaccion = servicioDominioTransaccion;
        }

        /// <summary>
        /// Método que construye el cuerpo de consulta cuenta
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public async Task<ConsultaCuentaOperacionDTO> ConstruirCuerpoConsulta(
            ConsultaCuentaReceptorDTO datos)
        {
            var cuentaEfectivo = await _servicioAplicacionCliente.ObtenerDatosCuentaOrigen(datos.NumeroCuentaOriginante);

            return cuentaEfectivo.AConsultaCuenta(datos.NumeroCuentaReceptor, datos.CodigoTipoTransferencia, 
                datos.CodigoCanalCCE, _contextoAplicacion.IdTerminalOrigen);
        }

        /// <summary>
        /// Obtener el tipo de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerTipoMensajeParaCorreo() => CorreoGeneralDTO.CodigoTransferenciaInmediata;

        /// <summary>
        /// Obtener el tema de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerTemaMensajeParaCorreo() => "CONSTANCIA DE TRANSFERENCIA";
        /// <summary>
        /// Obtener el servicio de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerServicioMensajeParaCorreo(string canal) 
            => canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad 
                ? "Transferencia - Billetera virtual" : "Transferencia inmediata a otro banco";

        /// <summary>
        /// Define la plaza de la operacion
        /// </summary>
        /// <param name="entidadFinancieraOriginante">Entidad originante</param>
        /// <param name="oficinaDestino">Codigo de entidad receptora</param>
        /// <param name="codigoCuentaInterbancario">CCI del originante</param>
        /// <returns>Codigo de plaza</returns>
        public string DefinirPlaza(
            EntidadFinancieraDiferida entidadFinancieraOriginante,
            OficinaCCE oficinaDestino,
            string codigoCuentaInterbancario)
        {
            try
            {
                if (entidadFinancieraOriginante == null)
                    throw new ValidacionException(
                        "No se pudo definir la plaza porque no se encontro la entidad o la oficina de la Entidad originate o Receptora");

                var oficinaOrigen = entidadFinancieraOriginante.Oficinas
                    .FirstOrDefault(x => x.CodigoOficina == codigoCuentaInterbancario.Substring(3, 3));

                return ObtenerCodigoTarifario(oficinaOrigen, oficinaDestino);
            }
            catch (Exception)
            {
                throw new ValidacionException("Error al Definir Plaza");
            }
        }

        /// <summary>
        /// Método que verifica si es exonerado de Comisión
        /// </summary>
        /// <param name="esMismaPlaza"></param>
        /// <param name="numeroCuenta"></param>
        /// <param name="fechaProceso"></param>
        /// <returns></returns>
        public bool VerificarSiEsExoneradoComisión(
            bool esMismaPlaza,
            string numeroCuenta,
            DateTime fechaProceso)
        {
            var configuracionComisionTransferencia = _repositorioOperacion
                .ObtenerPorExpresionConLimite<ConfiguracionComision>(c =>
                    c.CodigoComision == ConfiguracionComision.CodigoTransferenciaInterbancaria)
                .FirstOrDefault()
               .ValidarEntidadTexto("Configuracion de codigo de comision");

            if (!esMismaPlaza) return false;

            var cuentaEfectivo = _repositorioOperacion
                .ObtenerPorCodigo<CuentaEfectivo>(Empresa.CodigoPrincipal, numeroCuenta);

            return _servicioDominioTransaccion.VerificarExoneracionSegunConfiguracion(
                fechaProceso, cuentaEfectivo, configuracionComisionTransferencia);
        }

        /// <summary>
        /// Obtiene el codigo de plaza para el tarifaria
        /// </summary>
        /// <param name="oficinaCceOrigen">Oficina origen</param>
        /// <param name="oficinaCceDestino">Oficina destino</param>
        /// <returns>Retorna el codigo de plaza</returns>
        private static string ObtenerCodigoTarifario(
            OficinaCCE oficinaCceOrigen,
            OficinaCCE oficinaCceDestino)
        {
            if (oficinaCceOrigen == null)
                throw new ValidacionException("No hay ninguna oficina origen para obtener el codigo de tarifa");

            if (oficinaCceDestino == null)
                throw new ValidacionException("No hay ninguna oficina destino para obtener el codigo de tarifa");

            if (oficinaCceDestino.PlazaCCE.EsPlazaExclusiva == General.Si)
                return General.PlazaExclusiva;
            else
            {
                if (oficinaCceOrigen.EsOficinaDeLima() && oficinaCceDestino.EsOficinaDeLima())
                    return General.MismaPlaza;
                else
                {
                    return oficinaCceOrigen.CodigoUbigeoReferencia == oficinaCceDestino.CodigoUbigeoReferencia
                        ? General.MismaPlaza
                        : General.OtraPlaza;
                }
            }
        }
    }
}