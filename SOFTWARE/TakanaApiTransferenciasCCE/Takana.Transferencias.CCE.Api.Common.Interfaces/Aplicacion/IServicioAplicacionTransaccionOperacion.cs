using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IServicioAplicacionTransaccionOperacion
    {
        #region Entrantes
        /// <summary>
        /// Metodo que procesa la transferencia interbancaria Inmediata Entrante CCE
        /// </summary>
        /// <param name="datosTransferencia">datos de la transferencia</param>
        /// <returns>Retorna respuesta Json(200) o Json(500)</returns>
        Task<(int, bool)> RealizarTransferenciaEntranteCCE(string identificadorInstruccion);
        /// <summary>
        /// Metodo que procesa el rechazo de la transferencias Interbancarias Inmediata Pendiente y registra solo la comisión
        /// </summary>
        /// <param name="identificadorInstruccion"></param>
        /// <returns>Retorna respuesta Json(200) o Json(500)</returns>
        Task RealizarRechazoTransferenciaEntranteCCE(string identificadorInstruccion);
        /// <summary>
        /// Metodo que procesa la devolucion transferencia interbancaria Inmediata CCE
        /// </summary>
        /// <param name="transaccion">datos de la transaccion</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <param name="usuario">usuario que reprocesa</param>
        Task RealizarTransferenciaDevolucionCCE(TransaccionOrdenTransferenciaInmediata transaccion, 
            string identificadorTransaccion, Calendario fechaSistema, string usuario);
        #endregion Entrantes

        #region Salientes
        /// <summary>
        /// Metodo que realizar el debito interno y el debito externo
        /// Realiza la transferencia interna, luego envia la ordne de transferencia a la CCE
        /// En caso de fallar algo en el envio de orden a la CCE, esta se reversara internamente
        /// </summary>
        /// <param name="realizarTransferencia">Datos para realizar la transferencia</param>
        /// <returns>Retorna el resultado la operacion en ventanilla</returns>
        Task<string> RealizarTransferenciaVentanilla(
            OrdenTransferenciaVentanillaDTO realizarTransferencia);
        /// <summary>
        /// Realiza la transferencia completa para interoperabilidad
        /// </summary>
        /// <param name="orden">Datos de la transferencia</param>
        /// <returns>Resultado de la transferencia</returns>
        Task<ResultadoTransferenciaCanalElectronico> RealizarTransferenciaCanalElectronico(OrdenTransferenciaCanalElectronicoDTO orden);
        /// <summary>
        /// Obtiene los datos del cliente receptor desde la CCE
        /// </summary>
        /// <param name="datosConsulta">Datos de la consulta cuenta origen</param>
        /// <param name="consultarPorQr">Si la consulta es por un QR</param>
        /// <returns>Datos del cliente recpetor desde la CCE</returns>
        Task<ResultadoConsultaCuentaCCE> ConsultaCuentaReceptorPorCCE(
            ConsultaCuentaOperacionDTO datosConsulta, bool consultarPorQr = false);

        /// <summary>
        /// Calcula los totales segun la comision obtenida (expuesto)
        /// </summary>
        /// <param name="detalle">Datos de transferencia donde esta la comision</param>
        /// <returns>Control monto lleno</returns>
        Task<CalculoComisionDTO> CalcularMontosTotales(CalculoComisionDTO detalle);
        /// <summary>
        /// Valida detalles de la transferencia
        /// </summary>
        /// <param name="datosValidar">Datos a validar</param>
        /// <returns>Retorna codigo de respuesta exitosa</returns>
        Task<string> ObtenerDetallesValidadosMontosYLimites(ValidarTransaccionInmediataDTO datosValidar);

        /// <summary>
        /// Metodo que realiza la transferencia
        /// </summary>
        /// <param name="datosOrden">Datos de transferencia</param>
        /// <param name="esCanalElectronico">esCanalElectronico</param>
        Task<(ResultadoTransferenciaInmediataDTO, AsientoContable, Transferencia, MovimientoDiario?)> RealizarTransferenciaSalienteCCE(
            RealizarTransferenciaInmediataDTO datosOrden, bool esCanalElectronico);
        /// <summary>
        /// Metodo que envia la orden de transferencia a la CCE
        /// </summary>
        /// <param name="cuepoOrden">Datos para armar el envio</param>
        Task EnviarOrdenTransferenciaCCE(
            OrdenTransferenciaCanalDTO cuepoOrden, 
            Calendario fechaSistema,
            AsientoContable asiento,
            Transferencia transferencia);
        /// <summary>
        /// Método que realiza la consulta de cuenta del Receptor
        /// </summary>
        /// <returns></returns>
        Task<ResultadoConsultaCuentaCCE> ConsultaCuentaReceptor(ConsultaCuentaReceptorDTO datos);
        /// <summary>
        /// Método que agrega a una operacion frecuente
        /// </summary>
        /// <param name="operacion"></param>
        /// <returns></returns>
        Task AgregarOperacionFrecuente(OperacionFrecuenteDTO operacion);
        #endregion
    }
}
