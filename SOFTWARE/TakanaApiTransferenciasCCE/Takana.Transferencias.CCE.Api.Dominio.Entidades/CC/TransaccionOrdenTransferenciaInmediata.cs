using System.Globalization;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class TransaccionOrdenTransferenciaInmediata
    {
        #region Constantes
        /// <summary>
        /// Monto de tarifac CCE para reversion total 
        /// </summary>
        public const decimal MontoTarifaReversion = 0.0M;
        #endregion

        #region Propiedades
        /// <summary>
        /// Identificador de la tabla de transaccion.
        /// </summary>
        public int IdTransaccion { get; private set; }
        /// <summary>
        /// Indicador del tipo de transaccion entrante o saliente.
        /// </summary>
        public string IndicadorTransaccion { get; private set; }
        /// <summary>
        /// Indentificador utogenerado de identificación única de transacción.
        /// </summary>
        public string IdentificadorInstruccion { get; private set; }
        /// <summary>
        /// Numero de la transferencia generado para la transaccion.
        /// </summary>
        public int? NumeroTransferencia { get; private set; }
        /// <summary>
        /// Numero del lavado generado para la transaccion.
        /// </summary>
        public int? NumeroLavado { get; private set; }
        /// <summary>
        /// Numero de asiento generado para la transaccion.
        /// </summary>
        public int? NumeroAsiento { get; private set; }
        /// <summary>
        /// Numero de movimiento de transaccion.
        /// </summary>
        public int? NumeroMovimiento { get; private set; }
        /// <summary>
        /// Fecha de envio a la CCE/Fecha de envio de la CCE.
        /// </summary>
        public DateTime FechaOperacion { get; private set; }
        /// <summary>
        /// Fecha que recepcionamos de la CCE/ Fecha de respuesta a la CCE.
        /// </summary>
        public DateTime? FechaRespuesta { get; private set; }
        /// <summary>
        /// Fecha que hace referencia a la fecha de liquidación.
        /// </summary>
        public DateTime? FechaLiquidacion { get; private set; }
        /// <summary>
        /// Código de la Entidad Originante.
        /// </summary>
        public string EntidadOriginante { get; private set; }
        /// <summary>
        /// Código de la Entidad Receptora.
        /// </summary>
        public string? EntidadReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Código de Cuenta Interbancario del Cliente Originante.
        /// </summary>
        public string? CodigoCuentaInterbancariaOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Código de Tipo de Persona del Cliente Originante (Natural/Juridico)
        /// </summary>
        public string? TipoPersonaOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Tipo de Documento del Cliente Originante.
        /// </summary>
        public string? TipoDocumentoIdentidadOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Numero documento de identidad del cliente originante.
        /// </summary>
        public string? NumeroDocumentoIdentidadOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Nombre del cliente originante.
        /// </summary>
        public string? NombreOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Telefono del cliente originante.
        /// </summary>
        public string? TelefonoOriginante { get; private set; } = string.Empty;
        /// <summary>
        /// Código de Cuenta Interbancario del Cliente Receptor.
        /// </summary>
        public string? CodigoCuentaInterbancariaReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Nombre del Cliente Receptor.
        /// </summary>
        public string? NombreReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Teléfono del cliente receptor.
        /// </summary>
        public string? TelefonoReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Tipo documento de identidad del cliente Receptor.
        /// </summary>
        public string? TipoDocumentoIdentidadReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Numero de documento identidad del cliente Receptor.
        /// </summary>
        public string? NumeroDocumentoIdentidadReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de tarjeta de credito del cliente Receptor.
        /// </summary>
        public string? TarjetaCreditoReceptor { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de primero envio o reintentos.
        /// </summary>
        public string? CodigoPlaza { get; private set; } = string.Empty;
        /// <summary>
        /// Número de seguimiento de la transacción.
        /// </summary>
        public string? CodigoTrace { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de moneda.
        /// </summary>
        public string CodigoMoneda { get; private set; }
        /// <summary>
        /// Codigo de tarifa.
        /// </summary>
        public string? CodigoTarifa { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de Canal.
        /// </summary>
        public string? CodigoCanal { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de Titular
        /// </summary>
        public string? CodigoTitular { get; private set; } = string.Empty;
        /// <summary>
        /// Monto de importe.
        /// </summary>
        public decimal MontoTransferencia { get; private set; }
        /// <summary>
        /// Monto de comision.
        /// </summary>
        public decimal MontoComision { get; private set; }
        /// <summary>
        /// Monto de comision.
        /// </summary>
        public decimal? MontoITF { get; private set; }
        /// <summary>
        /// Monto de liquidacion.
        /// </summary>
        public decimal? MontoLiquidacionInterbancaria { get; private set; } = 0.00M;
        /// <summary>
        /// Tipo de transferencia de la entidad.
        /// </summary>
        public string TipoTransferencia { get; private set; }
        /// <summary>
        /// Numero de Reintentos para la Solicitud Estado de Pago (solo aplica en Entrantes)
        /// </summary>        
        public int? NumeroReintentoSolicitud { get; private set; } = 0;
        /// <summary>
        /// Indicador de Estado de la Operacion
        /// </summary>
        public string? IndicadorEstadoOperacion { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo del Sistema
        /// </summary>
        public string? CodigoSistema { get; private set; } = string.Empty;
        /// <summary>
        /// Indicador de Estado del Sistema
        /// </summary>
        public string? IndicadorEstado { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de usuario que realiza la operacion.
        /// </summary>
        public string? CodigoUsuarioRegistro { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de usuario que modifica la operacion.
        /// </summary>
        public string? CodigoUsuarioModifico { get; private set; } = string.Empty;
        /// <summary>
        /// Codigo de usuario que concilia la operacion.
        /// </summary>
        public string? CodigoUsuarioConciliacion { get; private set; } = string.Empty;
        /// <summary>
        /// Fecha de registro en la tabla.
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Fecha de modificacion de la tabla.
        /// </summary>
        public DateTime? FechaModifico { get; private set; }
        /// <summary>
        /// Entidad de asiento contable
        /// </summary>
        public virtual AsientoContable AsientoContable { get; private set; }
        /// <summary>
        /// Entidad de transferencia
        /// </summary>
        public virtual Transferencia Transferencia { get; private set; }
        /// <summary>
        /// Entidad de Canal de la CCE
        /// </summary>
        public virtual CanalCCE CanalCCE { get; private set; }
        /// <summary>
        /// Entidad de Financiera Originante de la CCE
        /// </summary>
        public virtual EntidadFinancieraInmediata EntidadFinancieraOriginante { get; private set; }
        #endregion

        #region Métodos
        /// <summary>
        /// Extensión que mapea un registro de una transaccion de orden de transferencia
        /// </summary>
        /// <param name="datosTransaccion"></param>
        /// <param name="datosRespuesta"></param>
        /// <param name="clienteReceptor"></param>
        /// <returns>Retorna la transaccion mapeada</returns>
        public static TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaRecepcionEntradaDTO datosTransaccion,
            OrdenTransferenciaRespuestaEntradaDTO datosRespuesta,
            ClienteReceptorDTO clienteReceptor,
            DateTime fechaSistema
        )
        {
            return new TransaccionOrdenTransferenciaInmediata
            {
                IndicadorTransaccion = General.Receptor,
                IdentificadorInstruccion = datosTransaccion.instructionId,
                FechaOperacion = DateTime.ParseExact(datosTransaccion.creationDate + " " + datosTransaccion.creationTime,
                                    "yyyyMMdd HHmmss",
                                    CultureInfo.InvariantCulture),
                FechaLiquidacion = DateTime.ParseExact(datosTransaccion.settlementDate,
                                    "yyyyMMdd",
                                    CultureInfo.InvariantCulture),
                EntidadOriginante = datosTransaccion.debtorParticipantCode,
                EntidadReceptor = datosTransaccion.creditorParticipantCode,
                TipoPersonaOriginante = datosTransaccion.debtorTypeOfPerson?.Trim(),
                CodigoCuentaInterbancariaOriginante = datosTransaccion.debtorCCI,
                TipoDocumentoIdentidadOriginante = datosTransaccion.debtorIdCode?.Trim(),
                NumeroDocumentoIdentidadOriginante = datosTransaccion.debtorId?.Trim(),
                NombreOriginante = datosTransaccion.debtorName,
                TelefonoOriginante = datosTransaccion.debtorPhoneNumber ?? datosTransaccion.debtorMobileNumber ?? string.Empty,
                CodigoCuentaInterbancariaReceptor = clienteReceptor.CodigoCuentaInterbancaria.CompletarTextoAVacio(),
                NombreReceptor = clienteReceptor.NombreCliente,
                TelefonoReceptor = datosTransaccion.creditorMobileNumber.CompletarTextoAVacio(),
                TipoDocumentoIdentidadReceptor = clienteReceptor.TipoDocumento?.Trim(),
                NumeroDocumentoIdentidadReceptor = clienteReceptor.NumeroDocumento,
                TarjetaCreditoReceptor = datosTransaccion.creditorCreditCard.CompletarTextoAVacio(),
                CodigoTrace = datosTransaccion.trace,
                CodigoPlaza = datosTransaccion.applicationCriteria,
                CodigoMoneda = datosTransaccion.currency == DatosGenerales.CodigoMonedaSolesCCE
                    ? DatosGenerales.CodigoMonedaSoles : DatosGenerales.CodigoMonedaDolares,
                CodigoTarifa = datosTransaccion.feeCode,
                CodigoCanal = datosTransaccion.channel,
                CodigoTitular = datosRespuesta.sameCustomerFlag,
                MontoTransferencia = datosTransaccion.amount.ObtenerImporteOperacion(),
                MontoComision = datosTransaccion.feeAmount.ObtenerImporteOperacion(),
                MontoLiquidacionInterbancaria = datosTransaccion.interbankSettlementAmount.ObtenerImporteOperacion(),
                TipoTransferencia = datosTransaccion.transactionType,
                IndicadorEstadoOperacion = General.Pendiente,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                CodigoSistema = Sistema.CuentaEfectivo,
                IndicadorEstado = General.Activo,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Mapea los datos para el registro de la transaccion
        /// </summary>
        /// <param name="datosTransaccion">Datos para la transaferencia de salida</param>
        /// <param name="datosCanal">Datos del canal</param>
        /// <returns></returns>
        public static TransaccionOrdenTransferenciaInmediata RegistrarTransaccion(
            OrdenTransferenciaSalidaDTO datosTransaccion, 
            OrdenTransferenciaCanalDTO datosCanal,
            AsientoContable asiento,
            Transferencia transferencia,
            DateTime fechaSistema)
        {
            return new TransaccionOrdenTransferenciaInmediata
            {
                IndicadorTransaccion = General.Originante,
                EntidadOriginante = datosTransaccion.debtorParticipantCode,
                EntidadReceptor = datosTransaccion.creditorParticipantCode,
                FechaOperacion = DateTime.ParseExact(datosTransaccion.creationDate + " " + datosTransaccion.creationTime,
                                    "yyyyMMdd HHmmss",
                                    CultureInfo.InvariantCulture),
                CodigoPlaza = datosTransaccion.applicationCriteria,
                CodigoTrace = datosTransaccion.trace,
                CodigoMoneda = datosCanal.CodigoMoneda == DatosGenerales.CodigoMonedaSolesCCE 
                    ? DatosGenerales.CodigoMonedaSoles : DatosGenerales.CodigoMonedaDolares,
                MontoTransferencia = datosTransaccion.amount.ObtenerImporteOperacion(),
                TipoTransferencia = datosTransaccion.transactionType,
                MontoComision = datosTransaccion.feeAmount.ObtenerImporteOperacion(),
                MontoITF = datosCanal.MontoITF,
                NombreOriginante = datosTransaccion.debtorName,
                TipoPersonaOriginante = datosCanal.TipoPersonaDeudor,
                CodigoTarifa = datosCanal.CodigoTarifa,
                CodigoTitular = datosCanal.MismoTitular,
                CodigoCanal = datosTransaccion.channel,
                TipoDocumentoIdentidadOriginante = datosCanal.TipoDocumentoDeudor,
                NumeroDocumentoIdentidadOriginante = datosTransaccion.debtorId,
                TipoDocumentoIdentidadReceptor = datosCanal.TipoDocumentoReceptor,
                NumeroDocumentoIdentidadReceptor = datosCanal.NumeroIdentidadReceptor,
                CodigoCuentaInterbancariaOriginante = datosTransaccion.debtorCCI,
                NombreReceptor = datosTransaccion.creditorName,
                CodigoCuentaInterbancariaReceptor = datosTransaccion.creditorCCI.CompletarTextoAVacio(),
                TarjetaCreditoReceptor = datosTransaccion.creditorCreditCard.CompletarTextoAVacio(),
                NumeroMovimiento = transferencia.NumeroMovimiento,
                NumeroLavado = datosCanal.NumeroLavado,
                NumeroAsiento = asiento.NumeroAsiento,
                NumeroTransferencia = transferencia.NumeroTransferencia,
                AsientoContable = asiento,
                Transferencia = transferencia,
                IdentificadorInstruccion = datosCanal.InstruccionId,
                IndicadorEstadoOperacion = General.Pendiente,
                CodigoUsuarioRegistro = datosCanal.UsuarioRegistro,
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                FechaRegistro = fechaSistema,
                FechaModifico = fechaSistema,
                TelefonoOriginante = datosCanal.NumeroCelularDeudor.CompletarTextoAVacio(),
                TelefonoReceptor = datosCanal.NumeroCelularReceptor.CompletarTextoAVacio()
            };
        }
        /// <summary>
        /// Finaliza la transaccion exitosa
        /// </summary>
        /// <param name="transferencia">Numero de la transferencia</param>
        /// <param name="asientoContable">Numero de movimienot</param>
        /// <param name="numeroLavado">Numero de lavado</param>
        /// <param name="estado">Estado de finalizacion</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="fechaSistema">Fecha de sistema</param>
        public void FinalizarTransaccionExitosa(
            int numeroLavado,
            Transferencia transferencia,
            AsientoContable asientoContable,
            string estado,
            string usuario,
            DateTime fechaSistema)
        {
            NumeroTransferencia = transferencia.NumeroTransferencia;
            NumeroMovimiento = transferencia.NumeroMovimiento;
            NumeroAsiento = asientoContable.NumeroAsiento;
            Transferencia = transferencia;
            AsientoContable = asientoContable;
            NumeroLavado = numeroLavado;
            IndicadorEstadoOperacion = estado;
            CodigoUsuarioModifico = usuario;
            FechaModifico = fechaSistema;
        }
        /// <summary>
        /// Finaliza la transaccion de rechazo
        /// </summary>
        /// <param name="estado">estado de finalizacion rechazo</param>
        /// <param name="usuario">usuario</param>
        /// <param name="fechaSistema">Fecha de sistema</param>
        public void FinalizarTransaccionRechazo(
            AsientoContable asientoContable,
            string usuario,
            string estado,
            DateTime fechaSistema)
        {
            NumeroAsiento = asientoContable.NumeroAsiento;
            AsientoContable = asientoContable;
            IndicadorEstadoOperacion = estado;
            CodigoUsuarioModifico = usuario;
            FechaModifico = fechaSistema;
        }
        /// <summary>
        /// Actualizacion de estado transaccion
        /// </summary>
        /// <param name="estado">Finalizacion</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        public void ActualizarEstadoTransaccion(string estado, DateTime fechaSistema)
        {
            if(IndicadorEstadoOperacion != General.Finalizado || 
                IndicadorEstadoOperacion != General.Rechazo)
            {
                FechaRespuesta = fechaSistema;
                FechaModifico = fechaSistema;
                IndicadorEstadoOperacion = estado;
            }
        }
        /// <summary>
        /// Actualizar Fecha de respuesta
        /// </summary>
        /// <param name="fechaSistema"></param>
        public void ActualizarFechaRespuesta(DateTime fechaSistema)
        {
            FechaRespuesta = fechaSistema;
        }
        /// <summary>
        /// Cambiar de estado
        /// </summary>
        /// <param name="codigoEstado"></param>
        public void CambiarEstado(string codigoEstado)
        {
            IndicadorEstadoOperacion = codigoEstado;
        }
        /// <summary>
        /// Incrementa el numero de reintento solicitado
        /// </summary>
        public void IncrementarNumeroReintentoSolicitud()
        {
            NumeroReintentoSolicitud += 1;
        }
        /// <summary>
        /// Restablecer Comision
        /// </summary>
        public void RestablecerComision()
        {
            MontoComision = 0.00m;
        }
        /// <summary>
        /// Estable ITF
        /// </summary>
        /// <param name="montoITF"></param>
        public void EstablecerITF(decimal montoITF)
        {
            MontoITF = montoITF;
        }

        /// <summary>
        /// Actualizar Número de asiento
        /// </summary>
        /// <param name="numeroAsiento"></param>
        public void ActualizarNumeroAsiento(int numeroAsiento)
        {
            NumeroAsiento = numeroAsiento;
        }

        /// <summary>
        /// Actualiza la instruccion
        /// </summary>
        /// <param name="instruccion"></param>
        /// <param name="fechaRespuesta"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarInstruccion(
            string instruccion, 
            DateTime fechaRespuesta,
            DateTime fechaSistema)
        {
            IdentificadorInstruccion = instruccion;
            CodigoUsuarioModifico = this.CodigoUsuarioRegistro;
            FechaRespuesta = fechaRespuesta;
            FechaModifico = fechaSistema;
        }

        /// <summary>
        /// Actualiza la transaccion saliente
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="instruccion"></param>
        /// <param name="montoLiquidacion"></param>
        /// <param name="fechaRespuesta"></param>
        /// <param name="fechaSistema"></param>
        public void ActualizarTransaccionSaliente(
            string estado, 
            string instruccion, 
            decimal montoLiquidacion, 
            DateTime fechaRespuesta,
            DateTime fechaSistema)
        {
            IndicadorEstadoOperacion = estado;
            IdentificadorInstruccion = instruccion;
            FechaRespuesta = fechaRespuesta;
            MontoLiquidacionInterbancaria = montoLiquidacion;
            CodigoUsuarioModifico = this.CodigoUsuarioRegistro;
            FechaModifico = fechaSistema;
        }

        /// <summary>
        /// Actualiza la transaccion saliente
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="instruccion"></param>
        /// <param name="montoLiquidacion"></param>
        public void ActualizarTransaccionSaliente(
            string estado, 
            string usuario,
            string instruccion, 
            DateTime fechaRespuesta)
        {
            IndicadorEstadoOperacion = estado;
            IdentificadorInstruccion = instruccion;
            CodigoUsuarioModifico = usuario;
            FechaModifico = fechaRespuesta;
        }
        /// <summary>
        /// Actualizar el monto de la tarifa(TOMARA PARA LA CONCIALIACION)
        /// </summary>
        /// <param name="montoTarifaCCE">Monto de la tarifa final</param>
        public void ActualizarComisionTarifaCCE(decimal montoTarifaCCE, DateTime fecha)
        {
            MontoComision = montoTarifaCCE;
            FechaModifico = fecha;
            CodigoUsuarioModifico = CodigoUsuarioRegistro;
        }
        #endregion
    }
}
