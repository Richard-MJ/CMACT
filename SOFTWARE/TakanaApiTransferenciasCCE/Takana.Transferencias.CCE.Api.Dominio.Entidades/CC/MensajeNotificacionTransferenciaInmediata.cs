using System.Globalization;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Notificaciones;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class MensajeNotificacionTransferenciaInmediata
    {
        #region Propiedades
        /// <summary>
        /// Identificador de mensaje
        /// </summary>
        public int IdentificadorMensaje { get; private set; }
        /// <summary>
        /// Identificador de trama
        /// </summary>
        public int IdentificadorTrama { get; private set; }
        /// <summary>
        /// Codigo de mensaje
        /// </summary>
        public string? CodigoMensaje { get; private set; }
        /// <summary>
        /// Fecha de mensaje
        /// </summary>
        public DateTime? FechaMensaje { get; private set; }
        /// <summary>
        /// Fecha de conciliacion
        /// </summary>
        public DateTime? FechaConciliacion { get; private set; }
        /// <summary>
        /// Fecha nueva de liquidacion
        /// </summary>
        public DateTime? FechaNuevaLiquidacion { get; private set; }
        /// <summary>
        /// Fecha anterior de liquidacion
        /// </summary>
        public DateTime? FechaAnteriorLiquidacion { get; private set; }
        /// <summary>
        /// Descripcion de mensaje de identificacion
        /// </summary>
        public string? DescripcionMensajeIdentificacion { get; private set; }
        /// <summary>
        /// Clase de mensaje
        /// </summary>
        public string? ClaseMensaje { get; private set; }
        /// <summary>
        /// Codigo de moneda
        /// </summary>
        public string? CodigoMoneda { get; private set; }
        /// <summary>
        /// Codigo de entidad
        /// </summary>
        public string? CodigoEntidad { get; private set; }
        /// <summary>
        /// Nombre de entidad
        /// </summary>
        public string? NombreEntidad { get; private set; }
        /// <summary>
        /// Saldo de balance
        /// </summary>
        public decimal? SaldoBalance { get; private set; }
        /// <summary>
        /// Saldo Minimo
        /// </summary>
        public decimal? SaldoMinimo { get; private set; }
        /// <summary>
        /// Saldo Normal
        /// </summary>
        public decimal? SaldoNormal { get; private set; }
        /// <summary>
        /// SaldoUmbral
        /// </summary>
        public decimal? SaldoUmbral { get; private set; }
        /// <summary>
        /// Saldo nuevo
        /// </summary>
        public decimal? SaldoNuevo { get; private set; }
        /// <summary>
        /// Saldo anterior
        /// </summary>
        public decimal? SaldoAnterior { get; private set; }
        /// <summary>
        /// Descripcion de mensaje
        /// </summary>
        public string? DescripcionMensaje { get; private set; }
        /// <summary>
        /// Estado de nueva liquidacion
        /// </summary>
        public string? EstadoNuevoLiquidacion { get; private set; }
        /// <summary>
        /// Estado de anterior liquidacion
        /// </summary>
        public string? EstadoAnteriorLiquidacion { get; private set; }
        /// <summary>
        /// Numero credito recibidas aceptadas
        /// </summary>
        public string? NumeroCreditosRecibidasAceptadas { get; private set; }
        /// <summary>
        /// Total creditos recibidos aceptados
        /// </summary>
        public decimal? TotalCreditosRecibidasAceptadas { get; private set; }
        /// <summary>
        /// Numero de creditos recibidos rechazados
        /// </summary>
        public string? NumeroCreditosRecibidasRechazadas { get; private set; }
        /// <summary>
        /// Total de creditos recibidos rechazados
        /// </summary>
        public decimal? TotalCreditosRecibidasRechazadas { get; private set; }
        /// <summary>
        /// Numero de creditos enviados aceptados
        /// </summary>
        public string? NumeroCreditosEnviadasAceptadas { get; private set; }
        /// <summary>
        /// Total de creditos enviados aceptados
        /// </summary>
        public decimal? TotalCreditosEnviadasAceptadas { get; private set; }
        /// <summary>
        /// Numero de creditos enviados rechazados
        /// </summary>
        public string? NumeroCreditosEnviadasRechazadas { get; private set; }
        /// <summary>
        /// Total de creditos enviados rechazados
        /// </summary>
        public decimal? TotalCreditosEnviadasRechazadas { get; private set; }
        /// <summary>
        /// Valor de concilicacion
        /// </summary>
        public decimal? ValorConciliacion { get; private set; }
        /// <summary>
        /// Numero anterior de conciliacion realizada
        /// </summary>
        public string? NumeroAnteriorConciliacionRealizada { get; private set; }
        /// <summary>
        /// valor bruto realizado
        /// </summary>
        public decimal? ValorBrutoRealizada { get; private set; }
        /// <summary>
        /// Numero de anterior conciliacion reduccion
        /// </summary>
        public string? NumeroAnteriorConciliacionReduccion { get; private set; }
        /// <summary>
        /// Valor de bruto reduccion
        /// </summary>
        public decimal? ValorBrutoReduccion { get; private set; }
        /// <summary>
        /// Estado de mensaje
        /// </summary>
        public string? EstadoMensaje { get; private set; }
        /// <summary>
        /// Codigo de sistema
        /// </summary>
        public string? CodigoSistema { get; private set; }
        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string? IndicadorEstado { get; private set; }
        /// <summary>
        /// Codigo de usuario registro
        /// </summary>
        public string? CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// Codigo de usuario modifico
        /// </summary>
        public string? CodigoUsuarioModifico { get; private set; }
        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Fecha de modificacion
        /// </summary>
        public DateTime? FechaModifico { get; private set; }

        #endregion Propiedades

        #region Métodos

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 971
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion971DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM971,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                EstadoMensaje = datosMensajeNotificacion.newServiceStatus,
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 972
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion972DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM972,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                CodigoMoneda = datosMensajeNotificacion.currency,
                CodigoEntidad = datosMensajeNotificacion.memberIdentification,
                NombreEntidad = datosMensajeNotificacion.name,
                EstadoMensaje = datosMensajeNotificacion.status,
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 981
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion981DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM981,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                DescripcionMensaje = datosMensajeNotificacion.eventDescription,
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 982
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion982DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM982,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                CodigoEntidad = datosMensajeNotificacion.participantIdentification,
                NombreEntidad = datosMensajeNotificacion.participantName,
                EstadoMensaje = datosMensajeNotificacion.participantStatus,
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 993
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion993DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM993,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                NombreEntidad = datosMensajeNotificacion.bankName,
                CodigoMoneda = datosMensajeNotificacion.currency,
                EstadoMensaje = datosMensajeNotificacion.availablePrefundBalanceStatus,
                SaldoBalance = datosMensajeNotificacion.openingPrefundedBalance.ObtenerImporteOperacion(),
                SaldoMinimo = datosMensajeNotificacion.lowWatermarkValue.ObtenerImporteOperacion(),
                SaldoNormal = datosMensajeNotificacion.highWatermarkValue.ObtenerImporteOperacion(),
                SaldoUmbral = datosMensajeNotificacion.availablePrefundedBalance.ObtenerImporteOperacion(),
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 998
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion998DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM998,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                CodigoEntidad = datosMensajeNotificacion.memberIdentification,
                NombreEntidad = datosMensajeNotificacion.bankName,
                CodigoMoneda = datosMensajeNotificacion.currency,
                EstadoMensaje = datosMensajeNotificacion.prefundedBalanceChangeStatus,
                SaldoNuevo = datosMensajeNotificacion.newPrefundedBalance.ObtenerImporteOperacion(),
                SaldoAnterior = datosMensajeNotificacion.previousPrefundedBalance.ObtenerImporteOperacion(),
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }

        /// <summary>
        /// Extensión que mapea el mensaje de notificacion 999
        /// </summary>
        /// <param name="datosMensajeNotificacion">datos del mensaje de notificacion</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Retorna el mensaje de notificacion</returns>
        public static MensajeNotificacionTransferenciaInmediata RegistrarMensajeNotificacion(
             Notificacion999DTO datosMensajeNotificacion,
             DateTime fechaSistema
        )
        {
            return new MensajeNotificacionTransferenciaInmediata
            {
                IdentificadorTrama = (int)TipoTramaEnum.SNM999,
                CodigoMensaje = datosMensajeNotificacion.eventCode,
                FechaMensaje = DateTime.ParseExact(datosMensajeNotificacion.eventDate + " " + datosMensajeNotificacion.eventTime,
                                            "yyyyMMdd HHmmss",
                                            CultureInfo.InvariantCulture),
                DescripcionMensajeIdentificacion = datosMensajeNotificacion.messageIdentification,
                ClaseMensaje = datosMensajeNotificacion.messageClass,
                FechaAnteriorLiquidacion = DateTime.ParseExact(datosMensajeNotificacion.previousSettlementWindowDate,
                                            "yyyyMMdd",
                                            CultureInfo.InvariantCulture),
                EstadoAnteriorLiquidacion = datosMensajeNotificacion.previousSettlementCycleStatus,
                FechaConciliacion = DateTime.ParseExact(datosMensajeNotificacion.reconciliationCheckpointCutOffDateAndTime,
                                            "yyyyMMddHHmmss",
                                            CultureInfo.InvariantCulture),
                FechaNuevaLiquidacion = DateTime.ParseExact(datosMensajeNotificacion.newSettlementWindowDate,
                                            "yyyyMMdd",
                                            CultureInfo.InvariantCulture),
                EstadoNuevoLiquidacion = datosMensajeNotificacion.newSettlementWindowStatus,
                CodigoMoneda = datosMensajeNotificacion.currency,
                CodigoEntidad = datosMensajeNotificacion.memberIdentification,
                NombreEntidad = datosMensajeNotificacion.bankName,
                SaldoAnterior = datosMensajeNotificacion.previousOpeningPrefundedBalance.ObtenerImporteOperacion(),
                SaldoNuevo = datosMensajeNotificacion.newOpeningPrefundedBalance.ObtenerImporteOperacion(),
                NumeroCreditosRecibidasAceptadas = datosMensajeNotificacion.numberOfCreditTransferReceivedAndAccepted,
                TotalCreditosRecibidasAceptadas = datosMensajeNotificacion
                    .valueOfCreditTransferReceivedAndAccepted.ObtenerImporteOperacion(),
                NumeroCreditosRecibidasRechazadas = datosMensajeNotificacion.numberOfCreditTransferReceivedAndRejected,
                TotalCreditosRecibidasRechazadas = datosMensajeNotificacion.valueOfCreditTransferReceivedAndRejected
                    .ObtenerImporteOperacion(),
                NumeroCreditosEnviadasAceptadas = datosMensajeNotificacion.numberOfCreditTransferSentAndAccepted,
                TotalCreditosEnviadasAceptadas = datosMensajeNotificacion.valueOfCreditTransferSentAndAccepted
                    .ObtenerImporteOperacion(),
                NumeroCreditosEnviadasRechazadas = datosMensajeNotificacion.numberCreditTransferSentAndRejected,
                TotalCreditosEnviadasRechazadas = datosMensajeNotificacion.valueOfCreditTransferSentAndRejected
                    .ObtenerImporteOperacion(),
                ValorConciliacion = datosMensajeNotificacion.netPosition.ObtenerImporteOperacion(),
                NumeroAnteriorConciliacionRealizada = datosMensajeNotificacion.countOfSupplementalFunding,
                ValorBrutoRealizada = datosMensajeNotificacion.grossValueOfSupplementalFunding.ObtenerImporteOperacion(),
                NumeroAnteriorConciliacionReduccion = datosMensajeNotificacion.countOfDrawdowns,
                ValorBrutoReduccion = datosMensajeNotificacion.grossValueOfDrawdowns.ObtenerImporteOperacion(),
                IndicadorEstado = General.Activo,
                CodigoSistema = Sistema.CuentaEfectivo,
                CodigoUsuarioRegistro = General.UsuarioPorDefecto,
                FechaRegistro = fechaSistema
            };
        }
        #endregion
    }
}