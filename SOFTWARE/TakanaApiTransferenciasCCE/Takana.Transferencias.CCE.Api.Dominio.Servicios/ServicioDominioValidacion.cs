using System.Text.RegularExpressions;
using Takana.Transferencias.CCE.Api.Common.EchoTest;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Cancelaciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CL.TipoDocumento;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    ///  Servicio que realiza las validaciones de las tramas de Entradas
    /// </summary>
    public static class ServicioDominioValidacion
    {
        #region Metodos de Validacion Transferencias Entrantes
        /// <summary>
        /// Devuelve el codigo de validacion de Consulta Cuenta
        /// </summary>
        /// <param name="datosValidar">datos recibidos por la CCE</param>
        /// <param name="cliente">datos del Cliente</param>
        /// <param name="tramaOperacion">Bitacora de Trama</param>
        /// <param name="entidadesFinancieras">Lista de entidades Financieras</param>
        /// <param name="afiliacionInteroperabilidad">Afiliacion Interoperabilidad</param>
        /// <param name="codigoValidacionFirma">codigo de Validacion Firma</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarReglas(
            this ConsultaCuentaRecepcionEntradaDTO datosValidar,
            ClienteReceptorDTO cliente,
            BitacoraTransferenciaInmediata? tramaOperacion,
            List<EntidadFinancieraInmediata> entidadesFinancieras,
            AfiliacionInteroperabilidadDetalle? afiliacionInteroperabilidad,
            CanalCCE? canal, string codigoValidacionFirma)
        {
            try
            {
                var codigoRespuesta = codigoValidacionFirma.ValidarFirmaMensaje();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                if (tramaOperacion != null) 
                    return RazonRespuesta.codigoAM05;

                if (canal == null)
                    return RazonRespuesta.codigoFF07;

                if (datosValidar.transactionType != TipoTransferencia.CodigoTransferenciaOrdinaria)
                    return RazonRespuesta.codigoAC14;

                codigoRespuesta = entidadesFinancieras
                    .First(x => x.CodigoEntidad == EntidadFinancieraInmediata.CodigoCajaTacna)
                    .ValidarEntidadReceptor();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = entidadesFinancieras
                    .First(x => x.CodigoEntidad == datosValidar.debtorParticipantCode)
                    .ValidarEntidadOriginante();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.creditorCCI.ValidarCodigoCuentaInterbancaria();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = afiliacionInteroperabilidad.VerificarAfiliacionPorInteroperabilidad(
                    datosValidar.creditorCCI, datosValidar.channel);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.debtorIdCode.ValidarTipoDocumento(datosValidar.debtorId);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = cliente.ValidarDatosClienteReceptor(datosValidar.currency);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                return codigoRespuesta;
            }
            catch
            {
                return RazonRespuesta.codigoFF07;
            }
        }

        /// <summary>
        /// Devuelve el codigo de validacion de Orden de transferencia
        /// </summary>
        /// <param name="datosValidar">datos recibidos por la CCE</param>
        /// <param name="cliente">datos del Cliente</param>
        /// <param name="tramaOperacion">Bitacora de Trama</param>
        /// <param name="entidadesFinancieras">Lista de entidades Financieras</param>
        /// <param name="limiteTransferencia">LimiteTransferencia</param>
        /// <param name="codigoValidacionFirma">codigo de Validacion Firma</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarReglas(
            this OrdenTransferenciaRecepcionEntradaDTO datosValidar,
            ClienteReceptorDTO cliente,
            TransaccionOrdenTransferenciaInmediata? tramaOperacion,
            List<EntidadFinancieraInmediata> entidadesFinancieras,
            LimiteTransferenciaInmediata? limiteTransferencia,
            CanalCCE? canal, string codigoValidacionFirma)
        {
            try
            {
                var codigoRespuesta = codigoValidacionFirma.ValidarFirmaMensaje();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                if (tramaOperacion != null) 
                    return RazonRespuesta.codigoAM05;

                if (canal == null)
                    return RazonRespuesta.codigoFF07;

                if (datosValidar.transactionType != TipoTransferencia.CodigoTransferenciaOrdinaria)
                    return RazonRespuesta.codigoAC14;

                codigoRespuesta = entidadesFinancieras
                    .First(x => x.CodigoEntidad == EntidadFinancieraInmediata.CodigoCajaTacna)
                    .ValidarEntidadReceptor();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = entidadesFinancieras
                    .First(x => x.CodigoEntidad == datosValidar.debtorParticipantCode)
                    .ValidarEntidadOriginante();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.creditorCCI.ValidarCodigoCuentaInterbancaria();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.debtorIdCode.ValidarTipoDocumento(datosValidar.debtorId);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = cliente.ValidarDatosClienteReceptor(datosValidar.currency);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.creditorCCI.ValidarCodigoCuentaInterbancaria();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                codigoRespuesta = datosValidar.debtorCCI.ValidarCodigoCuentaInterbancaria();
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                var importeTransferencia = Utilidades.ObtenerImporteOperacion(datosValidar.amount);

                if (importeTransferencia <= 0.00m) 
                    return RazonRespuesta.codigoAM01;

                codigoRespuesta = limiteTransferencia.ValidarLimiteTransferencia(importeTransferencia);
                if (codigoRespuesta != RazonRespuesta.codigo0000)
                    return codigoRespuesta;

                if (string.IsNullOrEmpty(datosValidar.debtorName))
                    return RazonRespuesta.codigoBE08;

                if (string.IsNullOrEmpty(datosValidar.referenceTransactionId))
                    return RazonRespuesta.codigoBE15;

                return codigoRespuesta;
            }
            catch
            {
                return RazonRespuesta.codigoFF07;
            }
        }

        /// <summary>
        /// Verificar si esta Afiliado a Interoperabilidad
        /// </summary>
        /// <param name="datosAfiliacion"></param>
        /// <param name="codigoCuentaInterbancaria"></param>
        /// <param name="canal"></param>
        /// <returns></returns>
        public static string VerificarAfiliacionPorInteroperabilidad(
            this AfiliacionInteroperabilidadDetalle? datosAfiliacion, 
            string codigoCuentaInterbancaria, string canal)
        {
            if (canal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad)
            {
                if (datosAfiliacion == null || codigoCuentaInterbancaria != datosAfiliacion.CodigoCuentaInterbancario)
                    return RazonRespuesta.codigoCH11;
            }

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Devuelve el codigo de validacion de Confirmacion
        /// </summary>
        /// <param name="datosValidar">datos recibidos por la CCE</param>
        /// <param name="transaccion">datos recibidos por la CCE</param>
        /// <param name="codigoValidacionFirma">codigo de Validacion Firma</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarReglas(
            this OrdenTransferenciaConfirmacionEntradaDTO datosValidar,
            TransaccionOrdenTransferenciaInmediata? transaccion,
            string codigoValidacionFirma)
        {
            var codigoRespuesta = codigoValidacionFirma.ValidarFirmaMensaje();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return codigoRespuesta;

            codigoRespuesta = datosValidar.creditorParticipantCode.ValidarCodigoEntidad();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return RazonRespuesta.codigoFF02;

            if (Utilidades.ObtenerImporteOperacion(datosValidar.amount) <= 0.00m &&
                Utilidades.ObtenerImporteOperacion(datosValidar.feeAmount) <= 0.00m &&
                Utilidades.ObtenerImporteOperacion(datosValidar.interbankSettlementAmount) <= 0.00m)
                return RazonRespuesta.codigoAM01;

            if (transaccion.CodigoCuentaInterbancariaReceptor != datosValidar.creditorCCI)
                return RazonRespuesta.codigoFF02;

            if (transaccion.MontoTransferencia != Utilidades.ObtenerImporteOperacion(datosValidar.amount))
                return RazonRespuesta.codigoFF02;

            if (transaccion.MontoComision != Utilidades.ObtenerImporteOperacion(datosValidar.feeAmount))
                return RazonRespuesta.codigoFF02;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Devuelve el codigo de validacion de Cancelacion
        /// </summary>
        /// <param name="datosValidar">datos recibidos por la CCE</param>
        /// <param name="codigoValidacionFirma">codigo de Validacion Firma</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarReglas(
            this CancelacionRecepcionDTO datosValidar,
            string codigoValidacionFirma)
        {
            var codigoRespuesta = codigoValidacionFirma.ValidarFirmaMensaje();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return codigoRespuesta;

            codigoRespuesta = datosValidar.creditorParticipantCode.ValidarCodigoEntidad();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return RazonRespuesta.codigoFF02;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Devuelve el codigo de validacion de Echo Test
        /// </summary>
        /// <param name="datosValidar">datos recibidos por la CCE</param>
        /// <param name="codigoValidacionFirma">codigo de Validacion Firma</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarReglas(
            this EchoTestDTO datosValidar, 
            string codigoValidacionFirma)
        {
            var codigoRespuesta = codigoValidacionFirma.ValidarFirmaMensaje();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return codigoRespuesta;

            codigoRespuesta = datosValidar.participantCode.ValidarCodigoEntidad();
            if (codigoRespuesta != RazonRespuesta.codigo0000)
                return RazonRespuesta.codigoFF02;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Validar Mensaje de Firma con Entorno de Seguridad
        /// </summary>
        /// <param name="codigoValidacionFirma"></param>
        /// <returns>Codigo de Validacion Firma</returns>
        private static string ValidarFirmaMensaje(
            this string codigoValidacionFirma)
        {
            switch (codigoValidacionFirma)
            {
                case RazonRespuesta.codigoDS0A:
                    return RazonRespuesta.codigoDS0A;
                case RazonRespuesta.codigoERR3:
                    return RazonRespuesta.codigoDS0B;
                case RazonRespuesta.codigoDS0D:
                    return RazonRespuesta.codigoDS0D;
                case var firma when firma != RazonRespuesta.codigo0000:
                    return RazonRespuesta.codigoFF07;
                default:
                    return RazonRespuesta.codigo0000;
            }
        }

        /// <summary>
        /// Valida la Entidad receptor de la transferencia entrante
        /// </summary>
        /// <param name="EntidadFinanciera"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarEntidadReceptor(
            this EntidadFinancieraInmediata? EntidadFinanciera)
        {
            if (EntidadFinanciera == null ||
                EntidadFinanciera.CodigoEstadoSign != EstadoInmediata.EstadoSignOn ||
                EntidadFinanciera.CodigoEstadoCCE != EstadoInmediata.Normalizado)
                return RazonRespuesta.codigoFF07;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida la Entidad Originante de la transferencia entrante
        /// </summary>
        /// <param name="EntidadFinanciera"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarEntidadOriginante(
            this EntidadFinancieraInmediata? EntidadFinanciera)
        {
            if (EntidadFinanciera == null ||
                EntidadFinanciera.CodigoEstadoSign != EstadoInmediata.EstadoSignOn ||
                EntidadFinanciera.CodigoEstadoCCE != EstadoInmediata.Normalizado)
                return RazonRespuesta.codigoDNOR;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida el codigo de cuenta interbancaria
        /// </summary>
        /// <param name="EntidadFinanciera"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarCodigoCuentaInterbancaria(
            this string? codigoCuentaInterbancaria)
        {
            if (string.IsNullOrEmpty(codigoCuentaInterbancaria)
                || codigoCuentaInterbancaria.Length != General.LongitudCCI
                || !codigoCuentaInterbancaria.All(char.IsDigit))
                return RazonRespuesta.codigoFF02;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida los limites de transferencia interbancaria
        /// </summary>
        /// <param name="limiteTransferencia"></param>
        /// <param name="importeTransferencia"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarLimiteTransferencia(
            this LimiteTransferenciaInmediata? limiteTransferencia,
            decimal importeTransferencia)
        {
            if (limiteTransferencia == null ||
                importeTransferencia < limiteTransferencia.MontoLimiteMinimo ||
                importeTransferencia > limiteTransferencia.MontoLimiteMaximo)
                return RazonRespuesta.codigoFF07;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida el tipo de documento
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="numeroDocumento"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarTipoDocumento(
            this string tipoDocumento, string numeroDocumento)
        {
            if (string.IsNullOrEmpty(tipoDocumento))
                return RazonRespuesta.codigoFF02;

            if ((tipoDocumento == ((int)TipoDocumentoCCE.DNI).ToString() && numeroDocumento.Length != (int)LongitudDocumento.DNI) ||
                (tipoDocumento == ((int)TipoDocumentoCCE.LM).ToString() && numeroDocumento.Length != (int)LongitudDocumento.LM) ||
                (tipoDocumento == ((int)TipoDocumentoCCE.Pasaporte).ToString() && numeroDocumento.Length < (int)LongitudDocumento.Pasaporte) ||
                (tipoDocumento == ((int)TipoDocumentoCCE.CarnetExtranjeria).ToString() && numeroDocumento.Length < (int)LongitudDocumento.CarnetExtranjeria) ||
                (tipoDocumento == ((int)TipoDocumentoCCE.RUC).ToString() && numeroDocumento.Length != (int)LongitudDocumento.RUC) ||
                (tipoDocumento == ((int)TipoDocumentoCCE.LE).ToString() && numeroDocumento.Length <= (int)LongitudDocumento.LE))
                return RazonRespuesta.codigoBE16;

            if ((tipoDocumento == ((int)TipoDocumentoCCE.Pasaporte).ToString() ||
                tipoDocumento == ((int)TipoDocumentoCCE.CarnetExtranjeria).ToString()) &&
                !numeroDocumento.All(char.IsLetterOrDigit))
                return RazonRespuesta.codigoBE16;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida los datos del cliente receptor
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="moneda"></param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        private static string ValidarDatosClienteReceptor(
            this ClienteReceptorDTO cliente, string moneda)
        {
            if (string.IsNullOrEmpty(cliente.CodigoCuentaInterbancaria))
                return RazonRespuesta.codigoAC01;

            if (cliente.IndicadorCuentaValida != CuentaEfectivo.CuentaValida)
                return RazonRespuesta.codigoAC06;

            var codigoEstado = ValidarEstadoCuenta(cliente.EstadoCuenta);

            if (codigoEstado != RazonRespuesta.codigo0000)
                return codigoEstado;

            if (moneda != cliente.CodigoMonedaISO)
                return RazonRespuesta.codigoAC11;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Metodo que valida el estado de la cuenta del cliente receptor.
        /// </summary>
        /// <param name="CodigoEstado">El codigo de estado de la cuenta del cliente</param>
        /// <returns> En caso de no cumplir alguna validación retorna codigo de error correspondiente.</returns>
        public static string ValidarEstadoCuenta(string CodigoEstado)
        {
            switch (CodigoEstado)
            {
                case CuentaEfectivo.Anulado:
                    return RazonRespuesta.codigoAC07;
                case CuentaEfectivo.Cancelado:
                    return RazonRespuesta.codigoAC07;
                case CuentaEfectivo.Inactiva:
                    return RazonRespuesta.codigoAC06;
                case CuentaEfectivo.BloqueoTotal:
                    return RazonRespuesta.codigoAC06;
                default:
                    break;
            }
            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida si el codigo de la Entidad Receptora corresponde a CMAC TACNA.
        /// </summary>
        /// <param name="codigoEntidad">dato Codigo Entidad</param>
        public static string ValidarCodigoEntidad(this string codigoEntidad)
        {
            if (codigoEntidad != EntidadFinancieraInmediata.CodigoCajaTacna)
                return RazonRespuesta.codigoFF02;

            return RazonRespuesta.codigo0000;
        }

        /// <summary>
        /// Valida la Transaccion de Orden de Transferencia Entrante
        /// </summary>
        /// <param name="transaccion"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TransaccionOrdenTransferenciaInmediata ValidarOrdenTransferencia(
            this TransaccionOrdenTransferenciaInmediata? transaccion)
        {
            ValidarEntidad(transaccion);

            if (transaccion?.IndicadorEstadoOperacion == General.Finalizado ||
                transaccion?.IndicadorEstadoOperacion == General.Rechazo)
                throw new Exception("La transaccion ya ha sido Procesada.");

            return transaccion;
        }

        /// <summary>
        /// Valida una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidad"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T ValidarEntidad<T>(this T? entidad)
        {
            if (entidad == null)
                throw new Exception($"El resultado de la entidad {typeof(T).Name} no tiene registros ");

            return entidad;
        }

        /// <summary>
        /// Valida una entidad con parametro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entidad"></param>
        /// <param name="texto">Parametro de para mostrar</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static T ValidarEntidadTexto<T>(this T? entidad, string texto)
        {
            if (entidad == null)
               throw new ValidacionException($"No se encontraron resultados para la búsqueda de {texto}.");

            return entidad;
        }
        #endregion

        #region Validacion Saliente
        /// <summary>
        /// Valida las reglas que la IPS plantea en su documentacion
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna True si pasa las validaciones</returns>
        public static void ValidarIndicadores(ConsultaCanalDTO datos)
        {
            if (!datos.IndicadorCuentaValidaTransaccion)
                throw new Exception("La cuenta originante no es valida para la transacción");
            if (!datos.IndicadorSaldoValido)
                throw new Exception("El saldo no es valido porque no cumple con lo mínimo");
        }
        /// <summary>
        /// Valida la estructura del CCI del cliente Receptor o el numero de tarjeta
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna un throw si no pasa estas validaciones</returns>
        public static void ValidarEstructuraNumeroDestino(ConsultaCanalDTO datos)
        {
            if (datos.TipoTransaccion == TipoTransferencia.CodigoPagoTarjeta)
                ValidarEstructuraNumeroTarjeta(datos.TarjetaCreditoAcreedor);

            if (datos.TipoTransaccion == TipoTransferencia.CodigoTransferenciaOrdinaria)
                ValidarEstructuraCodigoCuentaInterbancario(datos.AcreedorCCI);
        }
        /// <summary>
        /// Valida la estructura del numero de Tarjeta del cliente receptor
        /// </summary>
        /// <param name="digitos">Numero de tarjeta de credito</param>
        /// <returns>Retorna un throw si no pasa esta validaciones</returns>
        public static void ValidarEstructuraNumeroTarjeta(string digitos)
        {
            bool resultadoTarjeta = true;
            int operacionDigitos = 48;
            int posicionesOcupadas = 2;
            int reducirMayoresQueNueve = 9;
            int moduloLuhun = 10;   
            resultadoTarjeta = digitos.All(char.IsDigit) && digitos.Reverse()
            .Select(c => c - operacionDigitos)
            .Select((numero, i) => i % posicionesOcupadas == 0
                ? numero
                : ((numero *= posicionesOcupadas) > reducirMayoresQueNueve ? numero - reducirMayoresQueNueve : numero)
            ).Sum() % moduloLuhun == 0;
            if (resultadoTarjeta is false)
                throw new Exception("La estructura de la tarjeta es invalida: " + digitos);
        }
        /// <summary>
        /// Valida la estructura del CCI del cliente receptor
        /// </summary>
        /// <param name="digitos">Digitos del CCI</param>
        /// <returns>Retorna un throw si no pasa estas validaciones</returns>
        public static void ValidarEstructuraCodigoCuentaInterbancario(string digitos)
        {
            var digitosCCI = 20;
            var numeroFormateado = string.Concat(digitos.Where(c => !char.IsWhiteSpace(c)));
            Regex regex = new Regex("^[0-9]+$");
            if (string.IsNullOrEmpty(numeroFormateado) ||
                numeroFormateado.Length != digitosCCI || !regex.IsMatch(numeroFormateado))
            {
                throw new DomainException("El número de CCI debe ser un número y su longitud" +
                    " no debe ser diferente de 20 caracteres.");
            }
            var codigoCuentaInterbancario = CodigoCuentaInterbancario.Generar(digitos.Substring(0, 18));
            if (codigoCuentaInterbancario.CodigoEntidad == ParametroGeneralTransferencia.CodigoEntidadOriginanteTakana)
                throw new DomainException("Estimado cliente, no se permite realizar transferencias interbancarias entre cuentas de la misma Caja Tacna.");

            if (digitos.Substring(18, 1) != codigoCuentaInterbancario.DigitoValidadorEntidadOficina)
                throw new DomainException("Dígito de control de la entidad y oficina es incorrecto." + digitos);

            if (digitos.Substring(19, 1) != codigoCuentaInterbancario.DigitoValidadorCuenta)
                throw new DomainException("Dígito de control de la cuenta interbancaria es incorrecto." + digitos);
        }
        /// <summary>
        /// Verifica que la entidad originante y receptora esten habilitadas
        /// </summary>
        /// <param name="datos">Datos enviados por el canal de originen</param>
        /// <returns>Retorna True si es que pasan esta validacion</returns>
        public static void VerificarEntidadesHabilitadas(List<EntidadFinancieraPorTransferencia> resultado)
        {
            if (resultado == null || resultado.Count() != 2)
                throw new Exception("Al menos una de las entidades no tiene habilitada el estado de originante o receptor");
        }

        /// <summary>
        /// Metodo de verifica el estado de las entidades originante y receptor
        /// </summary>
        /// <param name="originante">Codigo de entidad originante</param>
        /// <param name="receptora">Codigo de entidad receptora</param>
        /// <returns></returns>
        public static List<EntidadFinancieraInmediata> VerificarEstadoEntidades(
            string originante,
            string receptora,
            string estadoSistema,
            List<EntidadFinancieraInmediata> entidades)
        {
            if (estadoSistema != General.Disponible)
                throw new Exception("La entidad originante esta suspendida");

            if (entidades.Count != 2)
                throw new Exception("No se ha encontrado una o las 2 Entidades implicadas en esta operacion");

            var entidadOriginante = entidades.FirstOrDefault(m => m.CodigoEntidad == originante);
            var entidadReceptota = entidades.FirstOrDefault(m => m.CodigoEntidad == receptora);

            if (entidadOriginante.EstadoSign.Codigo == General.SignOff)
                throw new Exception("La entidad origen esta en Sign Off");
            if (entidadReceptota.EstadoSign.Codigo == General.SignOff)
                throw new Exception("La entidad receptora esta en Sign Off");
            if (entidadOriginante.EstadoCCE.Codigo != General.Normalizado)
                throw new Exception("La entidad origen esta en estado Bloqueado");
            if (entidadReceptota.EstadoCCE.Codigo != General.Normalizado)
                throw new Exception("La entidad receptora esta en estado Bloqueado");

            return entidades;
        }
        #endregion Validacion Saliente
    }
}
