using Takana.Transferencias.CCE.Api.Common;
using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.CF;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using Takana.Transferencias.CCE.Api.Common.DTOs.BitacoraNotificaciones;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase de aplicacion encargada de los control del cliente
    /// </summary>
    public class ServicioAplicacionCliente : ServicioBase, IServicioAplicacionCliente
    {
        #region declaraciones
        private readonly IServiceProvider _servicioProvider;
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IBitacora<ServicioAplicacionCliente> _bitacora;
        private readonly IServicioAplicacionColas _servicioAplicacionColas;
        private readonly IServicioAplicacionProducto _servicioAplicacionProducto;
        #endregion declaraciones

        #region Constructor 
        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="repositorioOperaciones"></param>
        public ServicioAplicacionCliente(
            IServiceProvider serviceProvider,
            IRepositorioGeneral repositorioGeneral,
            IContextoAplicacion contextoAplicacion,
            IRepositorioOperacion repositorioOperaciones,
            IBitacora<ServicioAplicacionCliente> bitacora,
            IServicioAplicacionColas servicioAplicacionColas,
            IServicioAplicacionProducto servicioAplicacionProducto) : base (contextoAplicacion)
        {
            _bitacora = bitacora;
            _servicioProvider = serviceProvider;
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperacion = repositorioOperaciones;
            _servicioAplicacionColas = servicioAplicacionColas;
            _servicioAplicacionProducto = servicioAplicacionProducto;
        }
        #endregion Constructor

        #region Métodos clientes

        /// <summary>
        /// Metodo de busca la cuenta del cliente por Codigo de Cuenta Interbancaria
        /// </summary>
        /// <param name="codigoCuentaInterbancario">Codigo de Cuenta Interbancaria</param>
        /// <returns>datos del cliente</returns>
        public ClienteReceptorDTO ObtenerDatosPorCodigoCuentaInterbancaria(
            string? codigoCuentaInterbancario)
        {
            try
            {
                return _repositorioGeneral
                    .ObtenerDatosClientePorCodigoCuentaInterbancaria(codigoCuentaInterbancario!);
            }
            catch
            {
                return new ClienteReceptorDTO();
            }
        }

        /// <summary>
        /// Metodo que obtiene los datos del cliente originante para operacion saliente
        /// </summary>
        /// <param name="numeroCuenta">Numero cuenta del cliente originante</param>
        /// <returns>Datos cuenta efectivo</returns>
        public async Task<CuentaEfectivoDTO> ObtenerDatosCuentaOrigen(string numeroCuenta)
        {
            try
            {
                var cuentaEfectivo = _repositorioOperacion
                    .ObtenerPorCodigo<CuentaEfectivo>(Empresa.CodigoPrincipal, numeroCuenta);

                var documento = cuentaEfectivo.Cliente.Documentos
                    .FirstOrDefault(d => d.TipoDocumento.CodigoTipoDocumento == cuentaEfectivo.Cliente.CodigoTipoDocumento)
                    .ValidarEntidadTexto($"tipo de documento: {cuentaEfectivo.Cliente.CodigoTipoDocumento}");

                ServicioDominioTransaccionOperacion.ValidarCuentaOriginantePermitidaSaliente(cuentaEfectivo);

                return cuentaEfectivo.ACuentaEfectivoDTO(documento);
            }
            catch (Exception e)
            {
                throw new ValidacionException("Fallo al obtener cuenta cliente originante: " + e.Message);
            }
        }

        /// <summary>
        /// Método que devuelve el cliente Originante en Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transaccion">Datos de la transaccion</param>
        /// <returns>Retorna datos del cliente Receptor</returns>
        public ClienteExternoDTO? ObtenerClienteOriginante(
            TransaccionOrdenTransferenciaInmediata transaccion)
        {
            var clienteOriginante = _repositorioOperacion
                .Listar<DocumentoCliente>()
                .FirstOrDefault(x =>
                    x.CodigoTipoDocumento == transaccion.TipoDocumentoIdentidadOriginante!.Trim() &&
                    x.NumeroDocumento == transaccion.NumeroDocumentoIdentidadOriginante);

            var clienteOriginanteExterno = new ClienteExternoDTO();

            if (clienteOriginante != null)
            {
                var clienteOrigen = clienteOriginante.Cliente.ObtenerPersonaCuantia();

                clienteOriginanteExterno = transaccion
                    .CodigoCuentaInterbancariaOriginante?
                    .ClienteExistente(clienteOrigen);
            }
            else
            {
                clienteOriginanteExterno = transaccion.CodigoCuentaInterbancariaOriginante?
                    .ClienteNoExistente(string.Empty, transaccion.TipoDocumentoIdentidadOriginante?.Trim(),
                    transaccion.NombreOriginante, transaccion.NumeroDocumentoIdentidadOriginante,
                    transaccion.TipoPersonaOriginante?.Trim(), transaccion.TelefonoOriginante);
            }

            return clienteOriginanteExterno;
        }

        /// <summary>
        /// Método que devuelve el cliente Originante en Transferencia Interbancaria Inmediata Entrante
        /// </summary>
        /// <param name="transaccion">Datos de la transaccion</param>
        /// <returns>Retorna datos del cliente Receptor</returns>
        public ClienteExternoDTO ObtenerClienteOriginante(CuentaEfectivo cuentaEfectivo)
        {
            return cuentaEfectivo.NumeroCuenta.ClienteExistente(cuentaEfectivo.Cliente);
        }

        #endregion

        #region Métodos Ventanilla
        /// <summary>
        /// Obttiene todo lo necesario para iniciar el canal de ventanilla
        /// </summary>
        /// <returns>Datos para inicializar ventanilla</returns>
        public async Task<InicializarVentanillaDTO> ObtenerDatosInicialesVentanilla()
        {
            return new InicializarVentanillaDTO()
            {
                TipoTransferenciaCCE = await ListarTipoTransferenciasVigentesCCE(),
                EntidadFinancieraCCE = await ListarEntidadesFinancieras(),
                TipoDocumentoCCE = await ListarTiposDocumentoCCE(),
                ConceptoCobro = await ListarConceptoCobrosCCE(),
                VinculoMotivo = await ListarMotivosVinculos(Sistema.CuentaEfectivo)
            };
        }

        /// <summary>
        /// Lista la entidades financieras segun la CCE (Expuesto)
        /// </summary>
        /// <returns>Retorna lista de las entidades de la CCE</returns>
        public async Task<List<ConceptoCobroDTO>> ListarConceptoCobrosCCE()
        {
            var concepto = _repositorioOperacion
                .Listar<ConceptoCobroCCE>()
                .Where(x => x.IndicadorHabilitado)
                .ToList();

            if (concepto == null || concepto.Count() <= 0)
                throw new ValidacionException("No existen conceptos disponibles.");

            return concepto.AConceptoCobroDTO();
        }
        /// <summary>
        /// Lista la entidades financieras segun la CCE (Expuesto)
        /// </summary>
        /// <returns>Retorna lista de las entidades de la CCE</returns>
        public async Task<List<EntidadFinancieraTinDTO>> ListarEntidadesFinancieras()
        {
            var entidades = _repositorioOperacion
                .Listar<EntidadFinancieraInmediata>()
                .Where(x => x.CodigoEstadoSign == General.SignOn &&
                    x.CodigoEstadoCCE == General.Normalizado)
                .ToList();

            if (entidades == null || entidades.Count() <= 0)
                throw new ValidacionException("No existen entidades disponibles.");

            return entidades.AEntidadFinancietaTin();
        }

        /// <summary>
        /// Lista los tipos de transferencias TIN disponibles(Expuesto)
        /// </summary>
        /// <returns>Los tipos de transfernecia TIN</returns>
        public async Task<List<TipoTransferenciaDTO>> ListarTipoTransferenciasVigentesCCE()
        {
            var listaTipoTransferencia = _repositorioOperacion
                .Listar<TipoTransferencia>()
                .Where(x => x.IndicadorEstado == TipoTransferencia.EstadoActivo)
                .ToList();

            if (listaTipoTransferencia == null)
                throw new ValidacionException("No existen tipos de transferencias disponibles.");

            return listaTipoTransferencia.TipoTransferenciasCce();
        }

        /// <summary>
        /// Obtiene los tipos de documento
        /// </summary>
        /// <returns>Lista de tipos de documentos</returns>
        public async Task<List<TipoDocumentoTinDTO>> ListarTiposDocumentoCCE()
        {
            return _repositorioOperacion
                .ObtenerPorExpresionConLimite<TipoDocumento>(x =>
                    x.IndicadorAplicaCCE == 1)
                .ToList()
                .ValidarEntidadTexto("Tipos de documentos")
                .DatosTipoDocumento();
        }

        /// <summary>
        /// Método que obtiene la lista de motivos y vinculos
        /// </summary>
        /// <param name="codigoSistema"></param>
        /// <returns></returns>
        public async Task<VinculosMotivosDTO> ListarMotivosVinculos(string codigoSistema)
        {
            var motivos = _repositorioOperacion
                .ObtenerPorExpresionConLimite<MotivoMovimiento>(x =>
                    x.IndicadorActivo && x.CodigoSistema == codigoSistema)
                .ToList()
                .ValidarEntidadTexto("Motivos");

            var vinculos = _repositorioOperacion
                .ObtenerPorExpresionConLimite<VinculoMovimiento>(x =>
                    x.IndicadorActivo && x.CodigoSistema == codigoSistema)
                .ToList()
                .ValidarEntidadTexto("Vinculos");

            return motivos.AVinculoMotivo(vinculos);
        }

        /// <summary>
        /// Método que obtiene los limites de transferencia de Canal de Origen
        /// </summary>
        /// <returns>Lista de transferencias</returns>
        public async Task<List<LimiteTransferenciaDTO>> ListarLimitesTransferencias()
        {
            var limitesTransferencias = _repositorioOperacion
                .ObtenerPorExpresionConLimite<LimiteTransferenciaInmediata>(x =>
                    x.CodigoCanal == _contextoAplicacion.IdCanalOrigen &&
                    x.EstadoLimite == General.Activo)
                .ToList()
                .ValidarEntidadTexto("Limites de Transferencia");

            return limitesTransferencias.ALimitesTransferencias();
        }
        #endregion

        #region Metodos Canales Electronicos
        /// <summary>
        /// Método que obtiene los datos iniciales para el cliente
        /// </summary>
        /// <returns></returns>
        public async Task<InicializarCanalElectronicoDTO> ObtenerDatosInicialesCanalElectronico()
        {
            string numeroTarjeta = _contextoAplicacion.IdUsuarioAutenticado;

            if (string.IsNullOrEmpty(numeroTarjeta))
                throw new ValidacionException("No se puedo encontrar el identificador del usuario");

            var indicadroGrupo = GrupoProducto.IndicadorGrupoDebitoTinInmediata;

            return new InicializarCanalElectronicoDTO
            {
                ProductosDebito = await ObtenerDatosCuentaEfectivoPorCliente(numeroTarjeta, indicadroGrupo),
                TiposDocumentos = await ListarTiposDocumentoCCE(),
                EntidadesFinancieras = await ListarEntidadesFinancieras(),
                LimitesTransferencias = await ListarLimitesTransferencias()
            };
        }

        /// <summary>
        /// Método que obtiene los datos iniciales para el cliente
        /// </summary>
        /// <returns></returns>
        private async Task<List<CuentaEfectivoDTO>> ObtenerDatosCuentaEfectivoPorCliente(string numeroTarjeta, string indicadroGrupo)
        {
            var cliente = _servicioAplicacionProducto.ObtenerClienteAPartirDeTarjeta(numeroTarjeta).Duenio;

            var productos = _servicioAplicacionProducto.ObtenerProductosPasivosDeGrupoDebito(cliente.CodigoCliente, indicadroGrupo);

            return productos.Select(x => x.ACuentaEfectivoDTO(x.Cliente.DocumentoCartillaPorDefecto)).ToList();
        }
        #endregion

        #region Correo Electronico
        /// <summary>
        /// Metodo que enviar el correo de confirmacion de la operacion
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <param name="indicadorTipo"></param>
        /// <param name="correoDestinatario"></param>
        /// <returns></returns>
        public async Task EnviarCorreoClienteTransferenciaInmediata(
            int numeroMovimiento,
            string nombreDocumentoTerminos,
            byte[] documentoTerminos,
            string correoDestinatario)
        {
            try
            {
                var archivosAdjuntos = new List<ArchivoAdjuntoDTO>();

                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

                var correoRemitente = _repositorioOperacion.ObtenerPorCodigo<ParametroPorEmpresa>
                    (Empresa.CodigoPrincipal, Sistema.CuentaEfectivo, General.EmailCanales);

                var datosOperacion = _repositorioGeneral
                    .ObtenerDetalleTransferenciaInmediataPorNumeroMovimiento(numeroMovimiento);

                var estrategia = _servicioProvider
                    .GetRequiredKeyedService<IServicioTipoTransferencia>(datosOperacion.TipoTransferencia);

                var temaMensaje = estrategia.ObtenerTemaMensajeParaCorreo();
                var tipoMensaje = estrategia.ObtenerTipoMensajeParaCorreo();
                var servicioMensaje = estrategia.ObtenerServicioMensajeParaCorreo(datosOperacion.CodigoCanal);

                if (!string.IsNullOrEmpty(nombreDocumentoTerminos))
                {
                    var archivoAdjunto = documentoTerminos.AdtoFormatoArchivoAdjunto(
                        nombreDocumentoTerminos, ArchivoAdjuntoDTO.TipoArchivoPDF);
                    archivosAdjuntos.Add(archivoAdjunto);
                }

                var cuerpoCorreo = datosOperacion.ACorreoTransferenciaInmediata(_contextoAplicacion, correoRemitente.ValorParametro, 
                    fechaSistema.FechaHoraSistema, temaMensaje, servicioMensaje, correoDestinatario, archivosAdjuntos);

                await _servicioAplicacionColas.EnviarCorreoAsync(tipoMensaje, cuerpoCorreo);
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrio un error al enviar correo de transferencia inmediata: {excepcion.Message}");
            }
        }
        /// <summary>
        /// Metodo envio de notificaciones Unibanca
        /// </summary>
        /// <param name="numeroMovimiento">numero movimiento</param>
        /// <param name="codigoTipoOperacion">Codigo tipo operacion</param>
        /// <returns></returns>
        public async Task EnviarNotificacionAntifraudeUNIBANCA(int numeroMovimiento, string codigoTipoOperacion)
        {
            try
            {
                var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();
                var datosOperacion = _repositorioGeneral
                                    .ObtenerDetalleTransferenciaInmediataPorNumeroMovimiento(numeroMovimiento);                

                var cuentaEfectivo = _repositorioOperacion.ObtenerPorCodigo<CuentaEfectivo>
                                    (Empresa.CodigoPrincipal, datosOperacion.NumeroCuentaOrigen);
                var tarjeta = _repositorioOperacion.ObtenerPorExpresionConLimite<Tarjeta>(t =>
                                    t.CodigoCliente == cuentaEfectivo.CodigoCliente &&
                                    t.CodigoEstadoTarjeta == Tarjeta.TarjetaActiva && 
                                    t.CodigoTipoTarjeta == Tarjeta.TipoVisa).FirstOrDefault();

                if (tarjeta == null) return;

                var envio = new DatosEnvioMicroservicios()
                {
                    FechaSistema = fechaSistema.FechaSistema,
                    MontoDebitar = datosOperacion.MontoTransferencia,
                    NumeroTarjeta = tarjeta.NumeroTarjeta.ToString(),
                    NumeroOperacion = numeroMovimiento.ToString().PadLeft(6, '0')[^6..],
                    CodigoMoneda = cuentaEfectivo.CodigoMoneda,
                    CuentaOrigen = datosOperacion.CuentaInterbancariaOrigen,
                    CuentaDestino = datosOperacion.CuentaInterbancariaDestino,
                    NumeroMovimientoTTS = numeroMovimiento
                };

                await _servicioAplicacionColas.EnviarNotificacionAntifraude(codigoTipoOperacion, envio);
            }
            catch (Exception excepcion)
            {
                _bitacora.Error($"Ocurrio un error al enviar correo de transferencia inmediata: {excepcion.Message}");
            }

        }
        #endregion
    }
}