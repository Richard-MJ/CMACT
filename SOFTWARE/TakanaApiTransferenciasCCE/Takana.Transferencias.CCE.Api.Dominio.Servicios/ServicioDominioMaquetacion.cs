using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.SignOnOff;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de maquetacion de las Entradas
    /// </summary>
    public static class ServicioDominioMaquetacion 
    {
        #region Metodos de Maquetacion Entrada
        /// <summary>
        /// Devuelve la maquetacion de respuesta de consulta cuenta
        /// </summary>
        /// <param name="codigoValidacion">codigo de validacion</param>
        /// <param name="clienteReceptor">datos del cliente receptor</param>
        /// <param name="datosConsultaCuenta">datos recibidos por la CCE</param>
        /// <returns>Retorna la respuesta (AV3)</returns>
        public static EstructuraContenidoAV3 MaquetarDatos(
            this ConsultaCuentaRecepcionEntradaDTO datosConsultaCuenta,
            string codigoValidacion, 
            ClienteReceptorDTO clienteReceptor)
        {     
            var resultado = new ConsultaCuentaRespuestaEntradaDTO();
              
            if(codigoValidacion == RazonRespuesta.codigo0000)
            {
                resultado.responseCode = CodigoRespuesta.Aceptada;
                resultado.creditorName = clienteReceptor.NombreCliente;
                resultado.creditorId =  clienteReceptor.NumeroDocumento;
                resultado.creditorIdCode = clienteReceptor.TipoDocumentoCCE
                    ?? ((int)TipoDocumento.TipoDocumentoCCE.LE).ToString();
                resultado.sameCustomerFlag =  MetodosGenerales.ObtenerIndicadorITF(
                                                datosConsultaCuenta.debtorId, 
                                                datosConsultaCuenta.debtorIdCode,
                                                clienteReceptor.NumeroDocumento,
                                                clienteReceptor.TipoDocumentoCCE);
            }else{
                resultado.responseCode = CodigoRespuesta.Rechazada;
                resultado.reasonCode = codigoValidacion;
            }
    
            return datosConsultaCuenta.ArmarDatos(resultado);              
        }

        /// <summary>
        /// Devuelve la maquetacion de respuesta de orden de transferencia
        /// </summary>
        /// <param name="codigoValidacion">codigo de validacion</param>
        /// <param name="clienteReceptor">datos del cliente receptor</param>
        /// <param name="datosOrdenTransferencia">datos recibidos de la CCE</param>
        /// <returns>Retorna la respuesta (CT3)</returns>
        public static EstructuraContenidoCT3 MaquetarDatos(
            this OrdenTransferenciaRecepcionEntradaDTO datosOrdenTransferencia,
            string codigoValidacion, 
            ClienteReceptorDTO clienteReceptor,
            Calendario fechaSistema)
        {     
            var resultado = new OrdenTransferenciaRespuestaEntradaDTO();
 
            resultado.responseDate = fechaSistema.FechaFormato;
            resultado.responseTime = fechaSistema.HoraFormato;

            if(codigoValidacion == RazonRespuesta.codigo0000)
            {           
                resultado.responseCode = CodigoRespuesta.Aceptada;
                resultado.sameCustomerFlag = MetodosGenerales.ObtenerIndicadorITF(
                                                datosOrdenTransferencia.debtorId, 
                                                datosOrdenTransferencia.debtorIdCode,
                                                clienteReceptor.NumeroDocumento,
                                                clienteReceptor.TipoDocumentoCCE);
            }else{
                resultado.responseCode = CodigoRespuesta.Rechazada;
                resultado.reasonCode = codigoValidacion;
            }          

            return datosOrdenTransferencia.ArmarDatos(resultado);
        }
       
        /// <summary>
        /// Devuelve la maquetacion de respuesta de la cancelacion
        /// </summary>
        /// <param name="codigoValidacion">Codigo de validación</param>
        /// <param name="datosCancelacion">datos recibidos de la CCE</param>
        /// <returns>Retorna la respuesta (CTC2)</returns>
        public static EstructuraContenidoCTC2 MaquetarDatos(
            this CancelacionRecepcionDTO datosCancelacion,
            string codigoValidacion, 
            Calendario fechaSistema)
        {     
            var resultado = new CancelacionRespuestaDTO();

            resultado.responseDate = fechaSistema.FechaFormato;
            resultado.responseTime = fechaSistema.HoraFormato;
   
            if(codigoValidacion == RazonRespuesta.codigo0000)
            {                   
                resultado.responseCode = CodigoRespuesta.Aceptada;
            }else{
                resultado.responseCode = CodigoRespuesta.Rechazada;
                resultado.reasonCode = codigoValidacion;
            }

            return datosCancelacion.ArmarDatos(resultado);
        } 

        /// <summary>
        /// Devuelve la maquetacion de respuesta de solicitud de estado de pago.
        /// </summary>
        /// <param name="datosTransaccion">datos recibidos de la CCE</param>
        /// <returns>Retorna la respuesta (PSR1)</returns>
        public static EstructuraContenidoPSR1 MaquetarDatos(
            this TransaccionOrdenTransferenciaInmediata datosTransaccion,
            DateTime fechaSistema
        ){
            return datosTransaccion.ArmarDatos(fechaSistema);
        }    

        /// <summary>
        /// Devuelve la maquetacion de respuesta de echo test.
        /// </summary>
        /// <param name="codigoValidacion">Codigo de validación</param>
        /// <param name="datosEchoTest">datos recibidos de la CCE</param>
        /// <returns>Retorna la respuesta (ET2)</returns>
        public static EstructuraContenidoET2 MaquetarDatos(
            this EchoTestDTO datosEchoTest,
            string codigoValidacion,
            Calendario fechaSistema)
        {
            var resultado = new EchoTestRespuestaDTO();

            resultado.responseDate = fechaSistema.FechaFormato;
            resultado.responseTime = fechaSistema.HoraFormato;

            if(codigoValidacion == RazonRespuesta.codigo0000)
            {                   
                resultado.status = CodigoRespuesta.Aceptada;
            }else{
                resultado.status = CodigoRespuesta.Rechazada;
                resultado.reasonCode = codigoValidacion;
            }

            return datosEchoTest.ArmarDatos(resultado);     
        }

        #endregion

        #region Metodos de maquetacion Salida
        /// <summary>
        /// Maqueta los datos segun la estructura requerida por la CCE
        /// </summary>
        /// <param name="datos">Datos enviados por el canal origen</param>
        /// <returns>Datos maquetados</returns>
        public static ConsultaCuentaSalidaDTO MaquetarDatos(
            this ConsultaCanalDTO datos,
            ConsultaCuentaSalidaDTO esquema,
            Calendario fechaSistema)
        {
            var codigoEntidadReceptora = datos.AcreedorCCI?.Substring(0,3);

            esquema.creditorParticipantCode = datos.TipoTransaccion == TipoTransferencia.CodigoPagoTarjeta
                    ? datos.EntidadReceptora! : General.Cero + codigoEntidadReceptora;

            esquema.creationDate = fechaSistema.FechaFormato;
            esquema.creationTime = fechaSistema.HoraFormato;
            esquema.retrievalReferenteNumber = fechaSistema.FechaFormatoMesDia
                    + esquema.creationTime
                    + Utilidades.ObtenerNumeroAleatorioCCE(0, 99);
            esquema.debtorId = datos.IndicadorCuentaMancomunada
                    ? DatosGenerales.CuentaMancomunada : datos.IdDeudor;
            esquema.debtorIdCode = datos.IndicadorCuentaMancomunada
                    ? DatosGenerales.DNI : esquema.debtorIdCode;
            esquema.currency = datos.Moneda == DatosGenerales.CodigoMonedaSoles
                    ? DatosGenerales.CodigoMonedaSolesCCE : DatosGenerales.CodigoMonedaDolaresCCE;
            esquema.transactionType = datos.Canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad
                    ? TipoTransferencia.CodigoTransferenciaOrdinaria : datos.TipoTransaccion;

            if ((datos.Canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad))
            {
                if ((datos.ValorProxy == null || datos.TipoProxy == null))
                {
                    esquema.creditorMobileNumber = datos.NumeroCelularReceptor != null
                    ? datos.NumeroCelularReceptor
                    : throw new ValidacionException("Se necesita el numero del receptor");
                }
                else
                {
                    esquema.proxyType = datos.TipoProxy;
                    esquema.proxyValue = datos.ValorProxy;
                }
                datos.AcreedorCCI = codigoEntidadReceptora + DatosGeneralesInteroperabilidad.DigitosFaltantesCCI;
            }

            return esquema.ArmarDatos(datos);
        }
        /// <summary>
        /// Maqueta datos par enviarlos a la CCE
        /// </summary>
        /// <param name="datosOrden">Datos del canal origen </param>
        /// <returns>Datos maquetados</returns>
        public static OrdenTransferenciaSalidaDTO MaquetarDatos(
            this OrdenTransferenciaCanalDTO datosOrden,
            Calendario fechaSistema,
            string numeroSeguimiento)
        {
            var esquema = new OrdenTransferenciaSalidaDTO();
            return esquema.ArmarDatos(datosOrden, fechaSistema, numeroSeguimiento);
        }

        /// <summary>
        /// Maqueta los datos para SignOnOff para enviarlo a la CCE
        /// </summary>
        /// <returns>Retorna los datos maquetados</returns>
        public static SignOnOffDTO MaquetarDatosSignOnOff(Calendario fechaSistema, string trace)
        {
            var esquema = new SignOnOffDTO()
            {
                participantCode = ParametroGeneralTransferencia.CodigoEntidadOriginante,
                creationDate = fechaSistema.FechaFormato,
                creationTime = fechaSistema.HoraFormato,
                trace = trace
            };

            return SignOnOffExtensiones.MaquetacionDatosSalida(esquema);
        }
        /// <summary>
        /// Maqueta los datos para SignOnOff para enviarlos al controlador
        /// </summary>
        /// <returns>Retorna la lista de SingOnOffProgramadoDTO</returns>
        public static List<SingOnOffProgramadoDTO> MaquetarDatosPromamacionSignOnOff(IList<EntidadFinancieroInmediataPeriodo> entidadFinancieroInmediataPeriodo)
        {
            return entidadFinancieroInmediataPeriodo
               .Select(x => new SingOnOffProgramadoDTO
               {
                   NumeroPeriodo = x.NumeroPeriodo,
                   IdentificadorEntidad = x.IdentificadorEntidad,
                   CodigoEntidad = x.CodigoEntidad,
                   DescripcionPeriodo = x.DescripcionPeriodo,
                   HoraSingOn = x.HoraSingOn,
                   HoraSingOff = x.HoraSingOff,
                   IndicadorEstado = x.IndicadorEstado,
                   CodigoUsuarioRegistro = x.CodigoUsuarioRegistro,
                   FechaRegistro = x.FechaRegistro,
                   CodigoUsuarioModificacion = x.CodigoUsuarioModificacion,
                   FechaModificacion = x.FechaModificacion
               })
               .ToList();
        }

        /// <summary>
        ///Maqueta la consulta que se enviara a la operadora.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns> Retorna el formato de salida</returns> 
        /// </summary>
        public static EsctruturaET1 CreacionEchoTest(Calendario fechaSistema, string entidad, string numeroSeguimiento)
        {
            var cuerpo = EchoTestExtensiones
                .MaquetacionDatosSalida(
                    entidad,
                    numeroSeguimiento,
                    fechaSistema.FechaFormato,
                    fechaSistema.HoraFormato
                    );

            return DarFormatoSalida(cuerpo);
        }
        #endregion Metodos de maquetacion salida

        #region Formatos de salida
        /// <summary>
        /// Convierte a formato de salida para el canal origen.
        /// </summary>
        /// <param name="datos">Datos desencriptados de la respuesta de la CCE</param>
        /// <returns>Formato de salida</returns>
        public static EsctruturaAV1 DarFormatoSalida(this ConsultaCuentaSalidaDTO datos)
        {
            datos.debtorName = datos.debtorName?.QuitarDiacriticos();
            return new EsctruturaAV1()
            {
                AV1 = datos
            };
        }
        /// <summary>
        /// Convierte los datos recibidos en la estructura requerida por la CCE
        /// </summary>
        /// <param name="datos">Datos</param>
        /// <returns> </returns>
        public static EsctruturaCT1 DarFormatoSalida(this OrdenTransferenciaSalidaDTO datos)
        {
            datos.debtorName = datos.debtorName.QuitarDiacriticos();
            datos.creditorName = datos.creditorName?.QuitarDiacriticos();
            return new EsctruturaCT1()
            {
                CT1 = datos
            };
        }
        /// <summary>
        /// Convierte a formato de salida para la CCE Sign ON
        /// </summary>
        /// <param name="datos">Datos maquetados/param>
        /// <returns>Formato para enviar a la CCE</returns>
        public static EstructuraSignOn1 DarFormatoSalidaOn(SignOnOffDTO datos)
        {
            return new EstructuraSignOn1()
            {
                SignOn1 = datos
            };
        }
        /// <summary>
        /// Convierte a formato de salida para la CCE Sign Off
        /// </summary>
        /// <param name="datos">Datos maquetados/param>
        /// <returns>Formato para enviar a la CCE</returns>
        public static EstructuraSignOff1 DarFormatoSalidaOff(SignOnOffDTO datos)
        {
            return new EstructuraSignOff1()
            {
                SignOff1 = datos
            };
        }

        /// <summary>
        /// Esctructura necesario para el envio con la cabecera
        /// </summary>
        /// <param name="datos">Datos necesarios para el EchoTest</param>
        /// <returns>Retorna la estructura con la cabecera</returns>
        public static EsctruturaET1 DarFormatoSalida(EchoTestDTO datos)
        {
            return new EsctruturaET1()
            {
                ET1 = datos
            };
        }


        /// <summary>
        /// Traduce los campos de la CCE que esta en ingles a campos de español
        /// </summary>
        /// <param name="datos">Datos de respuesta de la CCE</param>
        /// <returns>Campos traducidos</returns>
        public static ConsultaCuentaRespuestaTraducidoDTO TraducirCampos(
            this ConsultaCuentaRecepcionSalidaDTO datos)
        {
            return ConsultaCuentaExtensiones.TraduccionConsultaCuenta(datos);
        }

        /// <summary>
        /// Traduce los campos en ingles de la CCE a español para los canales de origen
        /// </summary>
        /// <param name="datos">Respuesta de la CCE</param>
        /// <returns>Campos traducidos para canales origen</returns>
        public static OrdenTransferenciaRespuestaTraducidoDTO TraducirCampos(
            this OrdenTransferenciaRecepcionSalidaDTO datos)
        {
            return OrdenTransferenciaExtensiones.TraduccionOrdenTransferenciaSalida(datos);
        }

        #endregion Formatos de salida 

        #region Metodos de Maquetacion Certificado
        /// <summary>
        /// Maqueta la respuesta de rechazo de la CCE por certificado.
        /// </summary>
        /// <param name="datos">datos Serializados Recibidos.</param>
        /// <param name="codigoValidacion">Código de validación.</param>
        /// <returns>Cadena JSON con la estructura de rechazo.</returns>
        /// <exception cref="Exception">Excepción de certificado inválido.</exception>
        public static string MaquetarDatos(this string datos, string codigoValidacion)
        {
            var handlerChain = new ET1Handler();
            handlerChain.SetNext(new AV2Handler())
                .SetNext(new CT2Handler())
                .SetNext(new CT5Handler())
                .SetNext(new CTC1Handler());

            var resultado = handlerChain.Handle(datos, codigoValidacion);

            if (resultado == null) 
                throw new Exception($"Ocurrio un error en la desencripcitación: Código de Error {codigoValidacion}");

            return resultado;
        }
        #endregion

    }
}
