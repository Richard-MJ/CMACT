using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica
{
    public class PagoTarjetaCreditoEstrategia : ServicioBase, IServicioTipoTransferencia
    {
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IServicioAplicacionCliente _servicioAplicacionCliente;

        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioGeneral"></param>
        public PagoTarjetaCreditoEstrategia(
            IContextoAplicacion contexto,
            IRepositorioGeneral repositorioGeneral,
            IServicioAplicacionCliente servicioAplicacionCliente) : base(contexto)
        {
            _repositorioGeneral = repositorioGeneral;
            _servicioAplicacionCliente = servicioAplicacionCliente;
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

            var entidadFinancieraReceptora = _repositorioGeneral
                .ObtenerPorExpresionConLimite<EntidadFinancieraInmediata>(x =>
                    x.CodigoEntidad == datos.CodigoEntidadReceptora)
                .Select(x => new EntidadFinancieraTinDTO()
                   {
                       CodigoEstadoSign = x.CodigoEstadoSign,
                       CodigoEntidad = x.CodigoEntidad,
                       NombreEntidad = x.NombreEntidad,
                       OficinaPagoTarjeta = x.OficinaPagoTarjeta
                   })
                .FirstOrDefault()
                .ValidarEntidadTexto("Entidad Receptora");

            var cuerpoConsulta = cuentaEfectivo.AConsultaCuenta(datos.NumeroCuentaReceptor, datos.CodigoTipoTransferencia,
                datos.CodigoCanalCCE, _contextoAplicacion.IdTerminalOrigen);

            cuerpoConsulta.EntidadReceptora = entidadFinancieraReceptora;

            return cuerpoConsulta;
        }

        /// <summary>
        /// Obtener el tipo de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerTipoMensajeParaCorreo() => CorreoGeneralDTO.CodigoPagoTarjetaCredito;

        /// <summary>
        /// Obtener el tema de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerTemaMensajeParaCorreo() => "CONSTANCIA DE PAGO DE TARJETA DE CRÉDITO";
        /// <summary>
        /// Obtener el servicio de mensaje para el correo
        /// </summary>
        /// <returns></returns>
        public string ObtenerServicioMensajeParaCorreo(string canal) => "Pago inmediato de tarjeta de crédito";

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
            string codigoCuentaInterbancario) => General.MismaPlaza;

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
            DateTime fechaProceso) => false;

    }
}