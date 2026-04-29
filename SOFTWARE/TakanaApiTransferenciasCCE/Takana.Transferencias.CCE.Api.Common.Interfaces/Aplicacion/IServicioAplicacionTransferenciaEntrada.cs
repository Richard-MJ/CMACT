using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Rechazos;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.ReprocesarTransaccion;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionTransferenciaEntrada : IServicioBase
    {
        /// <summary>
        /// Interfaz que obtiene, maqueta y devuelve la respuesta de la operacion consulta cuenta
        /// </summary>
        /// <param name="datosConsultaCuenta">datos recepcionados enviados por la CCE</param>
        /// <param name="codigoValidacionFirma">datos de codigo de Validacion de Firma</param>
        /// <returns>Retorna la respuesta del tramo V3 a la CCE</returns>
        Task<EstructuraContenidoAV3> ConsultaCuentaDeCCE(
            ConsultaCuentaRecepcionEntradaDTO datosConsultaCuenta, 
            string codigoValidacionFirma);

        /// <summary>
        /// Interfaz que obtiene, maqueta y devuelve la respuesta de la operacion orden de transferencia
        /// </summary>
        /// <param name="datosOrdenTransferencia">datos recepcionados enviados por la CCE</param>
        /// <param name="codigoValidacionFirma">datos de codigo de Validacion de Firma</param>
        /// <returns>Retorna la respuesta del tramo V3 a la CCE</returns>
        Task<EstructuraContenidoCT3> AutorizaOrdenTransferenciaDeCCE(
            OrdenTransferenciaRecepcionEntradaDTO datosOrdenTransferencia,
            string codigoValidacionFirma);

        /// <summary>
        /// Interfaz que envia la confirmacion de abono al API CORE
        /// </summary>
        /// <param name="datosConfirmacion">datos recepcionados enviados por la CCE</param>
        /// <param name="codigoValidacionFirma">datos de codigo de Validacion de Firma</param>
        Task ConfirmaOrdenTransferenciaDeCCE(
            OrdenTransferenciaConfirmacionEntradaDTO datosConfirmacion,
            string codigoValidacionFirma);

        /// <summary>
        /// Interfaz que procesa la Transferencia Entrante CCE
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        /// <returns>Retorna true</returns>
        Task<bool> ProcesarTransferenciaEntranteCCE(
            string identificadorInstruccion);

        /// <summary>
        /// Interfaz que obtiene, maqueta y devuelve la respuesta de la operacion solicitud estado de pago
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador de Instruccion enviado por el modulo de tesoreria</param>
        /// <returns>Retorna la respuesta Json(200) o Json(500)</returns>
        Task<bool> SolicitudEstadoPagoParaCCE(
            string identificadorInstruccion, 
            bool modalidad);

        /// <summary>
        /// Interfaz que procesa, maqueta y devuelve la respuesta de la operacion cancelacion
        /// </summary>
        /// <param name="datosCancelacion">datos recepcionados enviados por la CCE</param>
        /// <returns>Retorna la respuesta de la cancelacion</returns>        
        Task<EstructuraContenidoCTC2> CancelacionOrdenTransferenciaDeCCE(
            CancelacionRecepcionDTO datosCancelacion,
            string codigoValidacionFirma);

        /// <summary>
        /// Interfaz que procesa de realizar rechazo de transferencia inmedaita entrante
        /// </summary>
        /// <param name="identificadorInstruccion">Identificador de Instruccion enviado por el modulo de tesoreria</param>
        /// <returns>Retorna la respuesta true</returns>
        Task<bool> ProcesarRechazoTransferenciaEntranteCCE(string identificadorInstruccion);


        /// <summary>
        /// Interfaz que procesa, maqueta y devuelve la respuesta de la operacion Echo Test
        /// </summary>
        /// <param name="datosEchoTest">datos recepcionados enviados por la CCE</param>
        /// <param name="codigoValidacionFirma">datos de codigo de Validacion de Firma</param>
        /// <returns>Retorna la respuesta del echo test</returns>        
        Task<EstructuraContenidoET2> EchoTestDeCCE(
            EchoTestDTO datosEchoTest, string codigoValidacionFirma);        

        /// <summary>
        /// Interfaz que procesa, maqueta y devuelve la respuesta de la operacion Rechazo(REJECT)
        /// </summary>
        /// <param name="datosRechazo">datos recepcionados enviados por la CCE</param>
        Task RechazoDeCCE(RechazoRecepcionDTO datosRechazo);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 971
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>
        Task Notificacion971DeCCE(Notificacion971DTO datosMensaje);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 972
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param> 
        Task Notificacion972DeCCE(Notificacion972DTO datosMensaje);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 981
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>          
        Task Notificacion981DeCCE(Notificacion981DTO datosMensaje);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 982
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>          
        Task Notificacion982DeCCE(Notificacion982DTO datosMensaje);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 993
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>           
        Task Notificacion993DeCCE(Notificacion993DTO datosMensaje);
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 998
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>          
        Task Notificacion998DeCCE(Notificacion998DTO datosMensaje); 
        /// <summary>
        /// Interfaz que procesa el mensaje de notificacion de notificacion 999
        /// </summary>
        /// <param name="datosMensaje">datos recepcionados enviados por la CCE</param>          
        Task Notificacion999DeCCE(Notificacion999DTO datosMensaje);
        /// <summary>
        /// Interfaz que re procesa las transacciones
        /// </summary>
        /// <param name="transacciones"></param>
        Task ReprocesarTransaccion(List<ReprocesarTransaccionDTO> transacciones);
    }
}
