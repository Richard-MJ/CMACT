using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.DTOs;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.CanalCCE;
using Takana.Transferencias.CCE.Api.Common.Excepciones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones
{
    public static class OperacionSalienteExtension
    {
        /// <summary>
        /// Metodo que mapea los datos de control monto
        /// </summary>
        /// <param name="itf">Itf</param>
        /// <param name="montoOriginal"></param>
        /// <param name="montoComisionCce"></param>
        /// <param name="montoComisionEntidad"></param>
        /// <param name="comisionTotal"></param>
        /// <returns>DTO de datos de control de monto</returns>
        public static ControlMontoDTO ADatosControlMonto(
            this decimal itf,
            decimal montoOriginal,
            decimal montoComisionCce,
            decimal montoComisionEntidad,
            decimal comisionTotal)
        {
            return new ControlMontoDTO
            {
                Itf = itf,
                Monto = montoOriginal,
                MontoComisionEntidad = montoComisionEntidad,
                MontoComisionCce = montoComisionCce,
                TotalComision = montoComisionEntidad,
                Total = montoOriginal + comisionTotal + itf
            };
        }
        /// <summary>
        /// Metodo que mapea los datos de consulta de salida
        /// </summary>
        /// <param name="datosConsulta"></param>
        /// <returns></returns>
        public static ConsultaCanalDTO ADatosConsultaCuentaSalida(
         this ConsultaCuentaOperacionDTO datosConsulta)
        {
            return new ConsultaCanalDTO
            {
                IdTerminal = datosConsulta.Usuario.NombreEquipo,
                IdDeudor = datosConsulta.CuentaEfectivoDTO.NumeroDocumento!,
                NombreDeudor = datosConsulta.CuentaEfectivoDTO.Titular,
                TipoDocumentoDeudor = datosConsulta.CuentaEfectivoDTO.TipoDocumentoOriginante!.CodigoTipoDocumento,
                TipoTransaccion = datosConsulta.TipoTransferencia,
                TipoPersonaDeudor = datosConsulta.CuentaEfectivoDTO.TipoDocumentoOriginante.EsTipoPersonaJuridica
                    ? TipoDocumento.PersonaJuridica : TipoDocumento.PersonaNatural,
                Moneda = datosConsulta.CuentaEfectivoDTO.CodigoMoneda,
                Canal = datosConsulta.Canal,
                AcreedorCCI = datosConsulta.TipoTransferencia == TipoTransferencia.CodigoTransferenciaOrdinaria
                    ? datosConsulta.NumeroCuentaOTarjeta : null,
                NumeroCuenta = datosConsulta.CuentaEfectivoDTO.NumeroCuenta,
                TarjetaCreditoAcreedor = datosConsulta.TipoTransferencia == TipoTransferencia.CodigoPagoTarjeta
                    ? datosConsulta.NumeroCuentaOTarjeta : null,
                CodigoCuentaInterbancarioOriginante = datosConsulta.CuentaEfectivoDTO.CodigoCuentaInterbancario,
                EntidadReceptora = datosConsulta.TipoTransferencia == TipoTransferencia.CodigoPagoTarjeta
                    ? datosConsulta.EntidadReceptora?.CodigoEntidad : null,
                IndicadorCuentaMancomunada = datosConsulta.CuentaEfectivoDTO.IndicadorTipoCuenta == CuentaEfectivo.Mancomunada,
                IndicadorCuentaValidaTransaccion = datosConsulta.CuentaEfectivoDTO.IndicadorTransferenciaCce == General.Si,
                NumeroCelularReceptor = datosConsulta.NumeroCelularReceptor,
                ValorProxy = datosConsulta.ValorProxy,
                TipoProxy = datosConsulta.TipoProxy

            };

        }
        /// <summary>
        /// Mapea los datos de la consulta CCE a un resultado de la consulta con los datos extras que se necesita
        /// </summary>
        /// <param name="datosConsulta">Datos de la consulta</param>
        /// <param name="resultado">Datos de respuesta de la consulta</param>
        /// <param name="comision">Comision</param>
        /// <param name="plaza">PLaza</param>
        /// <returns>Mapeo del resultado de la consulta</returns>
        public static ResultadoConsultaCuentaCCE AResultadoConsulta(
          this ConsultaCuentaOperacionDTO datosConsulta,
          ConsultaCuentaRespuestaTraducidoDTO resultado,
          ComisionDTO comision,
          bool esExoneradoComision,
          string plaza)
        {
            return new ResultadoConsultaCuentaCCE()
            {
                CodigoEntidadOriginante = resultado.CodigoEntidadOriginante,
                CodigoEntidadReceptora = resultado.CodigoEntidadReceptora,
                FechaCreacionTransaccion = resultado.FechaCreacionTransaccion,
                HoraCreacionTransaccion = resultado.HoraCreacionTransaccion,
                NumeroReferencia = resultado.NumeroReferencia,
                Trace = resultado.Trace,
                Canal = resultado.Canal,
                CodigoMoneda = resultado.CodigoMoneda,
                CodigoTransferencia = resultado.IdentificadorTransaccion,
                CriterioPlaza = plaza,
                TipoPersonaDeudor = datosConsulta.CuentaEfectivoDTO.TipoDocumentoOriginante!.EsTipoPersonaJuridica
                    ? TipoDocumento.PersonaJuridica : TipoDocumento.PersonaNatural,
                NommbreDeudor = string.IsNullOrEmpty(resultado.NombreDeudor)
                    ? datosConsulta.CuentaEfectivoDTO.Titular : resultado.NombreDeudor,
                TipoDocumentoDeudor = resultado.TipoDocumentoDeudor.ToString(),
                NumeroIdentidadDeudor = resultado.NumeroDocumentoDeudor,
                NumeroCelularDeudor = resultado.NumeroCelularDeudor,
                CodigoCuentaInterbancariaDeudor = datosConsulta.CuentaEfectivoDTO.CodigoCuentaInterbancario,
                NombreReceptor = resultado.NombreCompletoReceptor,
                DireccionReceptor = resultado.DireccionReceptor,
                NumeroTelefonoReceptor = resultado.TelefonoReceptor,
                NumeroCelularReceptor = resultado.NumeroCelularReceptor,
                CodigoCuentaInterbancariaReceptor = resultado.CodigoCuentaInterbancariaReceptor,
                CodigoTarjetaReceptor = resultado.CodigoTarjetaCreditoReceptor,
                IndicadorITF = resultado.IndicadorITF,
                Plaza = plaza,
                TipoTransaccion = datosConsulta.TipoTransferencia,
                InstruccionId = resultado.IdentificadorTransaccion,
                TipoDocumentoReceptor = string.IsNullOrEmpty(resultado.TipoDocumentoReceptor)
                    ? General.Cero : resultado.TipoDocumentoReceptor,
                NumeroIdentidadReceptor = string.IsNullOrEmpty(resultado.NumeroDocuementoReceptor)
                    ? General.Cero : resultado.NumeroDocuementoReceptor,
                MismoTitular = resultado.IndicadorITF,
                Comision = comision,
                CodigoCuentaTransaccion = string.IsNullOrEmpty(resultado.CodigoTarjetaCreditoReceptor)
                    ? resultado.CodigoCuentaInterbancariaReceptor : resultado.CodigoTarjetaCreditoReceptor,
                EsExoneradoComision = esExoneradoComision
            };
        }
        /// <summary>
        /// Mapea los datos para enviar la orden de transferencia
        /// </summary>
        /// <param name="transferenciaResultado">Resultado del debito interno</param>
        /// <param name="controlMonto">Datos de control de los montos</param>
        /// <param name="conceptoCobro">Concepto de cobro de la operacion</param>
        /// <param name="consultaCCE">COnsulta cuenta de la CCE</param>
        /// <param name="sesionUsuario">Sesion de Usuario</param>
        /// <returns>Datos mapeados para enviar la orden de transferencia</returns>
        public static OrdenTransferenciaCanalDTO ADatosOrdenTransferencia(
           this ResultadoTransferenciaInmediataDTO transferenciaResultado,
           ControlMontoDTO controlMonto,
           string conceptoCobro,
           ResultadoConsultaCuentaCCE consultaCCE,
           SesionUsuarioDTO? sesionUsuario)
        {
            if (string.IsNullOrEmpty(conceptoCobro))
                throw new ValidacionException("No se tiene codigo de concepto de cobro");

            return new OrdenTransferenciaCanalDTO
            {
                CodigoEntidadOriginante = consultaCCE.CodigoEntidadOriginante,
                CodigoEntidadReceptora = consultaCCE.CodigoEntidadReceptora,
                FechaCreacionTransaccion = consultaCCE.FechaCreacionTransaccion,
                HoraCreacionTransaccion = consultaCCE.HoraCreacionTransaccion,
                IdentificadorTerminal = sesionUsuario.NombreEquipo,
                NumeroReferencia = consultaCCE.NumeroReferencia,
                Trace = consultaCCE.Trace,
                Canal = consultaCCE.Canal,
                CodigoMoneda = consultaCCE.CodigoMoneda,
                CodigoTransferencia = consultaCCE.InstruccionId,
                CodigoTarifa = consultaCCE.Comision.CodigoComision,
                CriterioPlaza = consultaCCE.CriterioPlaza,
                TipoPersonaDeudor = consultaCCE.TipoPersonaDeudor,
                NommbreDeudor = consultaCCE.NommbreDeudor,
                TipoDocumentoDeudor = consultaCCE.TipoDocumentoDeudor.ToString(),
                NumeroIdentidadDeudor = consultaCCE.NumeroIdentidadDeudor,
                NumeroCelularDeudor = consultaCCE.NumeroCelularDeudor,
                CodigoCuentaInterbancariaDeudor = consultaCCE.CodigoCuentaInterbancariaDeudor,
                NombreReceptor = consultaCCE.NombreReceptor,
                DireccionReceptor = consultaCCE.DireccionReceptor,
                NumeroTelefonoReceptor = consultaCCE.NumeroTelefonoReceptor,
                NumeroCelularReceptor = consultaCCE.NumeroCelularReceptor,
                CodigoCuentaInterbancariaReceptor = consultaCCE.CodigoCuentaInterbancariaReceptor,
                CodigoTarjetaReceptor = consultaCCE.CodigoTarjetaReceptor,
                IndicadroITF = consultaCCE.MismoTitular,
                ConceptoCobroTarifa = conceptoCobro,
                TipoTransaccion = consultaCCE.TipoTransaccion,
                InstruccionId = consultaCCE.InstruccionId,
                TipoDocumentoReceptor = consultaCCE.TipoDocumentoReceptor,
                NumeroIdentidadReceptor = consultaCCE.NumeroIdentidadReceptor,
                MontoImporte = controlMonto.Monto,
                MontoComision = controlMonto.MontoComisionCce,
                MontoITF = controlMonto.Itf,
                NumeroTransferencia = transferenciaResultado.NumeroTransaccion,
                NumeroMovimiento = transferenciaResultado.NumeroOperacion,
                NumeroLavado = transferenciaResultado.NumeroLavado,
                NumeroAsiento = transferenciaResultado.NumeroAsiento,
                UsuarioRegistro = sesionUsuario.CodigoUsuario,
                MismoTitular = consultaCCE.MismoTitular
            };
        }
        /// <summary>
        /// Mapea los datos para realizar el debito interno desde ventanilla
        /// </summary>
        /// <param name="datos">datos enviados por ventanilla</param>
        /// <returns>Datos mapeados para realizar la transferencia interna</returns>
        public static RealizarTransferenciaInmediataDTO ADatosRealizarOrdenVentanilla(
           this OrdenTransferenciaVentanillaDTO datos)
        {
            return new RealizarTransferenciaInmediataDTO
            {
                CodigoAgencia = datos.SesionUsuario!.CodigoAgencia,
                CodigoUsuario = datos.SesionUsuario.CodigoUsuario!,
                NumeroCuentaOriginante = datos.NumeroCuenta,
                CodigoTipoTransferenciaCce = datos.DatosConsultaCuentaCCE.TipoTransaccion,
                ControlMonto = datos.ControlMonto,
                CodigoEntidadReceptora = datos.DatosConsultaCuentaCCE.CodigoEntidadReceptora,
                CodigoCuentaTransaccionReceptor = datos.DatosConsultaCuentaCCE.TipoTransaccion == TipoTransferencia.CodigoTransferenciaOrdinaria
                    ? datos.DatosConsultaCuentaCCE.CodigoCuentaInterbancariaReceptor : datos.DatosConsultaCuentaCCE.CodigoTarjetaReceptor,
                CodigoTipoDocumentoReceptor = datos.DatosConsultaCuentaCCE.TipoDocumentoReceptor,
                NumeroDocumentoReceptor = datos.DatosConsultaCuentaCCE.NumeroIdentidadReceptor,
                Beneficiario = datos.DatosConsultaCuentaCCE.NombreReceptor,
                MismoTitularEnDestino = datos.DatosConsultaCuentaCCE.MismoTitular == General.MismoTitular ? true : false,
                CodigoTarifarioComision = datos.DatosConsultaCuentaCCE.CriterioPlaza,
                NumeroLavado = datos.NumeroLavado,
                Canal = General.CanalCCE,
                SubCanal = ((int)CanalInmediataEnum.SubCanalTinInmediata).ToString(),
                MotivoVinculo = datos.MotivoVinculo,
                NombreImpresora = datos.SesionUsuario.NombreImpresora
            };
        }
        /// <summary>
        /// Mapea el resultado de la transferencia interna
        /// </summary>
        /// <param name="transferencia">Datos de la transferencia</param>
        /// <param name="numeroAsiento">Numero de asiento </param>
        /// <param name="numeroLavado">Numero de lavado</param>
        /// <param name="resultadoImpresion">resultado de la impresion</param>
        /// <returns>Datos mapeados para el resultado</returns>
        public static ResultadoTransferenciaInmediataDTO AResultadoTransferenciaInternaSaliente(
           this Transferencia transferencia,
           int numeroAsiento,
           int numeroLavado,
           string resultadoImpresion)
        {
            return new ResultadoTransferenciaInmediataDTO
            {
                NumeroTransaccion = transferencia.NumeroTransferencia,
                NumeroOperacion = transferencia.NumeroMovimiento,
                NumeroAsiento = numeroAsiento,
                NumeroLavado = numeroLavado,
                ResultadoImpresion = resultadoImpresion
            };
        }

        /// <summary>
        /// Mapea los datos para realizar el debito interno desde interoperabilidad
        /// </summary>
        /// <param name="datos">Datos de respuesta del debito interno</param>
        /// <param name="sesionUsuario">sesion del usuario</param>
        /// <param name="numeroLavado">numero de lavado de la operacion</param>
        /// <returns>Datos mapeados para realizar el debito interno</returns>
        public static RealizarTransferenciaInmediataDTO ARealizarTranferenciaCanalElectronico(
           this OrdenTransferenciaCanalElectronicoDTO datos,
           SesionUsuarioDTO sesionUsuario,
           int numeroLavado,
           string subCanal)
        {
            return new RealizarTransferenciaInmediataDTO
            {
                CodigoAgencia = sesionUsuario.CodigoAgencia,
                CodigoUsuario = sesionUsuario.CodigoUsuario!,
                NumeroCuentaOriginante = datos.NumeroCuenta,
                CodigoTipoTransferenciaCce = datos.ResultadoConsultaCuenta.TipoTransaccion,
                ControlMonto = datos.ControlMonto,
                CodigoEntidadReceptora = datos.ResultadoConsultaCuenta.CodigoEntidadReceptora,
                CodigoCuentaTransaccionReceptor = datos.ResultadoConsultaCuenta.CodigoCuentaInterbancariaReceptor
                    ?? datos.ResultadoConsultaCuenta.CodigoCuentaTransaccion,
                CodigoTipoDocumentoReceptor = datos.ResultadoConsultaCuenta.TipoDocumentoReceptor,
                NumeroDocumentoReceptor = datos.ResultadoConsultaCuenta.NumeroIdentidadReceptor,
                Beneficiario = datos.ResultadoConsultaCuenta.NombreReceptor,
                MismoTitularEnDestino = datos.ResultadoConsultaCuenta.MismoTitular == General.MismoTitular,
                CodigoTarifarioComision = datos.ResultadoConsultaCuenta.CriterioPlaza,
                NumeroLavado = numeroLavado,
                Canal = General.CanalCCE,
                SubCanal = subCanal,
                GlosarioTransaccion = datos.ResultadoConsultaCuenta.CodigoEntidadReceptora == DatosGeneralesInteroperabilidad.CodigoDirectorioCCE
                    ? datos.IdentificadorQR : string.Empty,
                MotivoVinculo = datos.AVinculoYMotivo()
            };
        }

        /// <summary>
        /// Mapea para vinculo y motivo de la transferencia
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        private static IngresoVinculoMotivoDTO? AVinculoYMotivo(
            this OrdenTransferenciaCanalElectronicoDTO datos)
        {
            if (string.IsNullOrEmpty(datos.Motivo)) return null;

            return new IngresoVinculoMotivoDTO
            {
                IdMotivo = IngresoVinculoMotivoDTO.IdOtrosMotivos,
                IdVinculo = IngresoVinculoMotivoDTO.IdOtrosVinculos,
                MotivoEspecificado = datos.Motivo,
                VinculoEspecificado = string.Empty,
            };
        }

        /// <summary>
        /// Transforma los datos de una transferencia inmediata en un objeto de orden de transferencia/>.
        /// </summary>
        /// <param name="transferenciaInterna">Resultado de la transferencia inmediata, con detalles del proceso.</param>
        /// <param name="orden"> Datos de la transferencia a realizar, como monto y destinatario. </param>
        /// <param name="conceptoCobro"> Descripción del concepto o motivo del cobro. </param>
        /// <param name="sesionUsuario"> Información de la sesión del usuario que realiza la transferencia.</param>
        /// <returns> Refleja la transferencia adaptada con la información proporcionada.</returns>
        public static OrdenTransferenciaCanalDTO ADatosOrdenTransferenciaInteroperabilidad(
            this ResultadoTransferenciaInmediataDTO transferenciaInterna,
            OrdenTransferenciaCanalElectronicoDTO orden,
            string conceptoCobro,
            SesionUsuarioDTO sesionUsuario)
        {
            var cuerpoOrden = transferenciaInterna.ADatosOrdenTransferencia(orden.ControlMonto, conceptoCobro,
                orden.ResultadoConsultaCuenta, sesionUsuario);
            cuerpoOrden.NumeroCelularDeudor = orden.NumeroCelularOriginante;
            cuerpoOrden.NumeroCelularReceptor = string.IsNullOrEmpty(orden.NumeroCelularReceptor)? null : orden.NumeroCelularReceptor;
            cuerpoOrden.Canal = DatosGeneralesInteroperabilidad.CanalInteroperabilidad;
            return cuerpoOrden;
        }

        /// <summary>
        /// Método que mapea a correo de Transferencia Inmediata
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="correoRemitente"></param>
        /// <param name="fecha"></param>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static CorreoTransferenciaInmediataDTO ACorreoTransferenciaInmediata(
           this CorreoTransferenciaInmediataDTO datos,
           IContextoAplicacion contexto,
           string correoRemitente,
           DateTime fecha,
           string temaMensaje,
           string servicioMensaje,
           string correoDestinatario,
           IList<ArchivoAdjuntoDTO> archivosAdjuntos)
        {
            if (datos.CodigoCanal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad)
            {
                datos.NombreClienteOrigen = datos.NombreClienteOrigen.EnmascararNombreAlternado();
                datos.CelularOrigen = datos.CelularOrigen.EnmascararCelular();
                datos.CuentaInterbancariaOrigen = datos.CuentaInterbancariaOrigen.EnmascararProducto();

                datos.NombreClienteDestino = datos.NombreClienteDestino.EnmascararNombreAlternado();
                datos.CelularDestino = datos.CelularDestino.EnmascararCelular();
                datos.CuentaInterbancariaDestino = datos.CuentaInterbancariaDestino.EnmascararProducto();
            }

            if (!string.IsNullOrEmpty(correoDestinatario))
                datos.CorreoElectronicoDestinatario = correoDestinatario;

            if (!string.IsNullOrEmpty(datos.NumeroTarjetaDestino))
                datos.NumeroTarjetaDestino = datos.NumeroTarjetaDestino.EnmascarasTexto(4);

            datos.TemaMensaje = temaMensaje;
            datos.Servicio = servicioMensaje;
            datos.CorreoElectronicoRemitente = correoRemitente;
            datos.FechaOperacion = fecha;
            datos.DireccionIP = contexto.IpAddress;
            datos.Modelo = contexto.ModeloDispositivo;
            datos.SistemaOperativo = contexto.SistemaOperativo;
            datos.Navegador = contexto.Navegador;
            datos.ArchivosAdjuntos = archivosAdjuntos;

            return datos;
        }

        /// <summary>
        /// Extension creadora del detalle para transferencia interbancaria
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static Dictionary<int, string> AFormatoOperacionDetalle(
            this OperacionFrecuenteDTO datos)
        {
            return new Dictionary<int, string>
            {
                { (int) IdentificadorPropiedadDetalle.CuentaDestinoCci, datos.CodigoCuentaInterbancariaReceptor},
                { (int) IdentificadorPropiedadDetalle.NombreDestino, datos.NombreDestino},
                { (int) IdentificadorPropiedadDetalle.MismoTitularEnDestino, datos.MismoTitularEnDestino.ToString()},
                { (int) IdentificadorPropiedadDetalle.TipoDocumento, datos.TipoDocumento.ToString()},
                { (int) IdentificadorPropiedadDetalle.NumeroDocumento, datos.NumeroDocumento},
            };
        }
    }
}
