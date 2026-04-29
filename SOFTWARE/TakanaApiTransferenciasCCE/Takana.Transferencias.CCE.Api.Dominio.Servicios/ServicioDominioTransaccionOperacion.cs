using System.Globalization;
using Takana.Transferencias.CCE.Api.Common.DTOs;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.OrdenesTransferencias;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.CanalCCE;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Servicio de maquetacion de las Entradas
    /// </summary>
    public class ServicioDominioTransaccionOperacion : IServicioDominioTransaccionOperacion
    {
        #region Declaraciones
        private readonly IServicioDominioCuenta _servicioDominioCuenta;
        #endregion

        #region Constructor
        /// <summary>
        /// Método constructor
        /// </summary>
        /// <param name="servicioDominioCuenta"></param>
        public ServicioDominioTransaccionOperacion(
            IServicioDominioCuenta servicioDominioCuenta)
        {
            _servicioDominioCuenta = servicioDominioCuenta;
        }

        #endregion

        #region Métodos Entrantes
        /// <summary>
        /// Método para generar y validar el nombre del beneficiario
        /// </summary>
        /// <param name="indicadorPersona">indicador de persona</param>
        /// <param name="nombreEmpresa">nombre de la empresa</param>
        /// <param name="nombrePersonaNatural">nombre de la persona natural</param>
        /// <param name="apellidoPaterno">apellido paterno</param>
        /// <param name="apellidoMaterno">apellido materno</param>
        /// <returns>nombre del beneficiario</returns>
        public virtual string GenerarYValidarNombreOrdenante(
            string indicadorPersona,
            string nombreEmpresa,
            string nombrePersonaNatural,
            string apellidoPaterno,
            string apellidoMaterno)
        {
            if (indicadorPersona == Cliente.TipoPersonaJuridica)
            {
                nombreEmpresa = string.IsNullOrEmpty(nombreEmpresa) ? nombreEmpresa : nombreEmpresa.Trim();
                if (string.IsNullOrEmpty(nombreEmpresa))
                    throw new Exception("Debe ingesar la razón social de la persona jurídica.");

                return nombreEmpresa;
            }
            else
            {
                nombrePersonaNatural = string.IsNullOrEmpty(nombrePersonaNatural) 
                    ? nombrePersonaNatural : nombrePersonaNatural.Trim();
                if (string.IsNullOrEmpty(nombrePersonaNatural))
                    throw new Exception("Debe ingesar el nombre de la persona natural.");

                if ((!string.IsNullOrEmpty(apellidoPaterno))
                        && (!string.IsNullOrEmpty(apellidoMaterno)))
                    return nombrePersonaNatural + " " + apellidoPaterno.Trim() + " " + apellidoMaterno.Trim();

                return nombrePersonaNatural;
            }
        }

        /// <summary>
        /// Método que genera el detalle de la transferencia CCE
        /// </summary>
        /// <param name="transferencia">transferencia CCE</param>
        /// <param name="EntidadOrdenanteCCE">entidad financiera</param>
        /// <param name="transaccion">Transaccion</param>
        /// <param name="clienteOriginante">Cliente Originante</param>
        /// <returns>Retorna el detalle de la transferencia entrante CCE</returns>
        public virtual TransferenciaDetalleEntranteCCE GenerarDetalleTransferencia(
            TransaccionOrdenTransferenciaInmediata transaccion,
            Transferencia transferencia,
            ClienteExternoDTO clienteOriginante,
            EntidadFinancieraInmediata EntidadOrdenanteCCE)
        {
            var indicadorPersona = clienteOriginante.TipoPersona.Trim() == ((int)ClienteExternoDTO.Persona.Natural).ToString()
                ? Cliente.TipoPersonaNatural : Cliente.TipoPersonaJuridica;
            var nombreEmpresa = clienteOriginante.TipoPersona.Trim() == ((int)ClienteExternoDTO.Persona.Natural).ToString()
                ? string.Empty : clienteOriginante.Nombres;
            var nombres = clienteOriginante.TipoPersona.Trim() == ((int)ClienteExternoDTO.Persona.Natural).ToString()
                ? clienteOriginante.Nombres : string.Empty;
            var apellidoPaterno = clienteOriginante.TipoPersona.Trim() == ((int)ClienteExternoDTO.Persona.Natural).ToString()
                ? clienteOriginante.ApellidoPaterno : string.Empty;
            var apellidoMaterno = clienteOriginante.TipoPersona.Trim() == ((int)ClienteExternoDTO.Persona.Natural).ToString()
                ? clienteOriginante.ApellidoMaterno : string.Empty;
            var mismoTitular = transaccion.CodigoTitular == General.MismoTitular 
                ? Cliente.MismoTitular : Cliente.OtroTitular;

            string nombreBeneficiario = GenerarYValidarNombreOrdenante(
                indicadorPersona,
                nombreEmpresa,
                nombres,
                apellidoPaterno,
                apellidoMaterno);

            var transferenciaDetalle = TransferenciaDetalleEntranteCCE.Crear(
                transferencia,
                clienteOriginante.CodigoCuentaInterbancaria,
                clienteOriginante.CodigoCliente,
                EntidadOrdenanteCCE,
                clienteOriginante.CodigoTipoDocumento,
                clienteOriginante.NumeroDocumento,
                nombreBeneficiario,
                mismoTitular,
                transaccion.MontoTransferencia,
                transaccion.CodigoTarifa,
                (decimal)transaccion.MontoComision);

            transferenciaDetalle.IncluirDatosPersonaNatural(
                nombres, apellidoPaterno, apellidoMaterno);

            return transferenciaDetalle;
        }
        #endregion

        #region Metodos Salientes
        /// <summary>
        /// Valida las caracteristicas definidas para que una cuenta sea apta para la transferencia
        /// </summary>
        /// <param name="cuentaEfectivo">Cuenta efectivo del cliente</param>
        /// <returns>True si pasa validaciones</returns>
        public static void ValidarCuentaOriginantePermitidaSaliente(CuentaEfectivo cuentaEfectivo)
        {
            if (!cuentaEfectivo.EsCuentaActiva)
                throw new ValidacionException("La cuenta no se encuentra en estado ACTIVO.");
            if (cuentaEfectivo.EsCuentaCTS)
                throw new ValidacionException("La cuenta es del tipo CTS no se puede utilizar como origen de esta Transferencia.");
            if (!cuentaEfectivo.TieneAsignadoCCI)
                throw new ValidacionException("La cuenta no tiene asignado un CCI.");
            if (!cuentaEfectivo.Caracteristicas.EsHabilitadoTIN)
                throw new ValidacionException("El tipo de cuenta seleccionada no se puede utilizar como origen de esta transferencia.");
            if(!cuentaEfectivo.TieneSaldoMinimoSuficiente())
                throw new ValidacionException("Saldo disponible insuficiente.");

            var documento = cuentaEfectivo.Cliente.Documentos
                .FirstOrDefault(d => d.TipoDocumento.CodigoTipoDocumento == cuentaEfectivo.Cliente.CodigoTipoDocumento);
            if (documento.TipoDocumento.CodigoTipoDocumentoInmediataCce == null)
                throw new ValidacionException(
                    "El tipo de documento de la cuenta seleccionada no se puede utilizar como origen de esta transferencia");
        }
        /// <summary>
        /// Validar si es que el monto de la transaferencia es permitido
        /// </summary>
        /// <param name="saldoActual">Saldo actual del cliente originante</param>
        /// <param name="montoOperacion">Monto de Operacion a realizar</param>
        /// <param name="limiteMaximo">Limite max establecido en Limites TIN</param>
        /// <param name="limiteMinimo">Limite min establecido en Limites TIN</param>
        /// <returns>Retorna TRUE si el monto es valido</returns>
        public static void ValidarMontoTransferenciaInmediata(
             decimal saldoActual,
             decimal montoOperacion,
             decimal limiteMaximo,
             decimal limiteMinimo
             )
        {
            if (montoOperacion > saldoActual)
                throw new ValidacionException("No cuenta con saldo suficiente para realizar la operacion.");
            if (montoOperacion < limiteMinimo || montoOperacion > limiteMaximo)
                throw new ValidacionException("Monto de la transferencia fuera del rango permitido.");
        }
        /// <summary>
        /// Calcula la comision total
        /// La comision de la CCE la asume la entidad
        /// Si la comision de la entida es mayor a la CCE, es un ingreso
        /// Si la comision de la CCE es mayor, es un gasto
        /// El cliente solo asume la comision impuesta por la entidad 
        /// </summary>
        /// <param name="montoComisionEntidad">Monto de la comisionTotal</param>
        /// <param name="montoComisionCce">Monto comision de la CCE</param>
        /// <returns>Monto de la comision</returns>
        public static decimal CalcularComisionTotal(
            decimal montoComisionEntidad,
            decimal montoComisionCce)
        {
            if (montoComisionEntidad == 0 || montoComisionEntidad < montoComisionCce)
                return 0;

            return montoComisionEntidad;
        }
        /// <summary>
        /// Metodo que calcula si el cliente tiene el saldo suficiente para la operacion
        /// </summary>
        /// <param name="montoOperacion">Monto de la operacion</param>
        /// <param name="montoMinimoCuenta">Monto minimo que debe tener la cuenta</param>
        /// <param name="saldoCuenta">Saldo actual de la cuenta</param>
        public static void CalcularSaldoSuficiente(
            decimal montoOperacion,
            decimal montoMinimoCuenta,
            decimal saldoCuenta)
        {
            if(!(saldoCuenta - montoMinimoCuenta >= montoOperacion))
                throw new ValidacionException("Saldo Insuficiente");
        }

        /// <summary>
        /// Calcula una comisión aplicando indicadores de porcentaje o fijo,
        /// y límites mínimo y máximo.
        /// </summary>
        /// <param name="montoOriginal"></param>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        /// <param name="porcentaje"></param>
        /// <param name="indicadorCCE"></param>
        /// <returns></returns>
        public static decimal CalcularComisionConPorcentaje(
            decimal montoOriginal,
            decimal minimo,
            decimal maximo,
            decimal porcentaje,
            string indicadorCCE)
        {
            decimal porcentajeCalculado = montoOriginal * (porcentaje / 100m);

            if (indicadorCCE == General.Si)
            {
                if (porcentajeCalculado < minimo)
                    return minimo;
                if (porcentajeCalculado > maximo)
                    return maximo;
                return porcentajeCalculado;
            }

            decimal montoConMinimo = minimo + porcentajeCalculado;

            if (montoConMinimo > maximo)
                return maximo;

            return montoConMinimo;
        }

        /// <summary>
        /// Método que calcula los comisiones sin porcentaje
        /// </summary>
        /// <param name="montoOriginal"></param>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        /// <returns></returns>
        public static decimal CalcularComisionSinPorcentaje(
            decimal montoOriginal,
            decimal minimo,
            decimal maximo)
        {
            if (minimo == maximo)
                return minimo;

            if (montoOriginal < minimo)
                return minimo;
            if (montoOriginal > maximo)
                return maximo;

            return montoOriginal;
        }

        /// <summary>
        /// Calcula los montos de comision
        /// </summary>
        /// <param name="detalle">detalle sde comision y operacion</param>
        /// <returns>Comision de nuestra Entidad y la CCE</returns>
        public static (decimal MontoEntidad, decimal MontoCce) CalcularMontosComision(CalculoComisionDTO detalle)
        {
            var comision = detalle.Comision;
            var montoOriginal = detalle.MontoOperacion;

            decimal montoComisionEntidad = 0m;
            decimal montoComisionCce = 0m;

            if (!detalle.EsExoneradoComision)
            {
                if (comision.IndicadorPorcentaje == General.Si && comision.Porcentaje > 0)
                {
                    montoComisionEntidad = CalcularComisionConPorcentaje(
                        montoOriginal,
                        comision.Minimo,
                        comision.Maximo,
                        comision.Porcentaje,
                        General.No
                    );
                }
                else
                {
                    montoComisionEntidad = CalcularComisionSinPorcentaje(
                        montoOriginal,
                        comision.Minimo,
                        comision.Maximo
                    );
                }
            }

            if (comision.PorcentajeCCE > 0)
            {
                montoComisionCce = CalcularComisionConPorcentaje(
                    montoOriginal,
                    comision.MinimoCCE,
                    comision.MaximoCCE,
                    comision.PorcentajeCCE,
                    General.Si
                );
            }
            else
            {
                montoComisionCce = CalcularComisionSinPorcentaje(
                    montoOriginal,
                    comision.MinimoCCE,
                    comision.MaximoCCE
                );
            }

            return (
                Math.Round(montoComisionEntidad, 2),
                Math.Round(montoComisionCce, 2)
            );
        }

        /// <summary>
        /// Valida si el saldo es suficiente para realizar la operacion
        /// </summary>
        /// <param name="saldoDisponible">Saldo que tiene el cliente</param>
        /// <returns>Valida saldo</returns>
        public static bool ValidarSaldoDisponible(decimal saldoDisponible, decimal saldoMinimo) => saldoDisponible >= saldoMinimo;

        /// <summary>
        /// Metodo que obtiene el filtro para oficina y la entidad
        /// </summary>
        /// <param name="datosConsulta">Datos de la consulta</param>
        /// <returns>Retorna la oficina y la entidad dependiendo del tipo de transaccion</returns>
        public static (string, string) ObtenerFiltroParaOficina(
            string? codigoEntidadReceptora, 
            string? oficinaTarjeta, 
            string numeroCuentaOTarjeta,
            string tipoTransferencia)
        {
            return tipoTransferencia == TipoTransferencia.CodigoPagoTarjeta
                ? ObtenerOficinaPorTarjeta(codigoEntidadReceptora, oficinaTarjeta)
                : ObtenerOficinaPorCCI(numeroCuentaOTarjeta);
        }
        /// <summary>
        /// Metodo que obtiene la oficina y la entidad con el numero CCI
        /// </summary>
        /// <param name="codigoCuenta">CCI</param>
        /// <returns>Retorna el coddigo de entidad destino y el codigo de la oficina destino</returns>
        public static (string,string) ObtenerOficinaPorCCI(string codigoCuenta)
        {
            var codigoCuentaInterbancario = CodigoCuentaInterbancario.Generar(codigoCuenta.Substring(0, 18));

            return (codigoCuentaInterbancario.CodigoEntidad, codigoCuentaInterbancario.CodigoOficina);
        }
        /// <summary>
        /// Metodo que obtiene la oficina destino y le entidad destino con el numero de tarjeta de credito
        /// </summary>
        /// <param name="codigoEntidad">Codigo de la entidad</param>
        /// <param name="oficinaPagoTarjeta">Oficina de pago de la tarjeta</param>
        /// <returns>Retorna el coddigo de entidad destino y el codigo de la oficina destino</returns>
        public static (string, string) ObtenerOficinaPorTarjeta(string? codigoEntidad,string? oficinaPagoTarjeta)
        {
            if (string.IsNullOrEmpty(oficinaPagoTarjeta))
                throw new Exception("La entidad destino no tiene código de oficina habilitado para pagos con tarjeta");

            string codigoEntidadDestino = codigoEntidad!.Substring(1, 3);
            string codigoOficinaDestino = oficinaPagoTarjeta!.Substring(1, 3);

            return (codigoEntidadDestino, codigoOficinaDestino);
        }

        /// <summary>
        /// Genera la cabecera y detalle de la transferencia
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia">Movimiento principal de la transferencia</param>
        /// <param name="detalleTransferencia">Datos de la transferencia</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="entidadDestino">Entidad Destino</param>
        /// <returns>Transferencia</returns>
        public Transferencia GenerarTransferenciaInmediata(
            MovimientoDiario movimientoPrincipalTransferencia,
            RealizarTransferenciaInmediataDTO detalleTransferencia,
            DateTime fechaSistema,
            Usuario usuario,
            EntidadFinancieraDiferida entidadDestino,
            int numeroTransferencia)
        {
            try
            {
                var transferencia = Transferencia.CrearTransferenciaInmediataCCE(
                   numeroTransferencia,
                   detalleTransferencia.CodigoTipoTransferenciaCce,
                   movimientoPrincipalTransferencia.Cuenta,
                   Convert.ToInt32(movimientoPrincipalTransferencia.NumeroMovimiento),
                   detalleTransferencia.ControlMonto.Monto,
                   fechaSistema,
                   usuario,
                   General.CanalCCE);
                
                int numeroDetalle = 1;
                var detalleTransferenciaCCE = detalleTransferencia.ADetalleTransferencia(
                    transferencia, entidadDestino, numeroDetalle);

                transferencia.AgregarDetalleCCE(detalleTransferenciaCCE);
                return transferencia;
            }
            catch (Exception)
            {
                throw new Exception("Error al generar la transferencia.");
            }

        }

        /// <summary>
        /// Aplica la comision a la transferencia
        /// </summary>
        /// <param name="movimientoPrincipalTransferencia"> Movimiento principal de la operacion</param>
        /// <param name="usuario">Datos de usuario del sistema</param>
        /// <param name="comisionAhorros">comisiones de ahoroos</param>
        /// <param name="numeroMovimiento">numero de movimiento</param>
        /// <returns></returns>
        public MovimientoDiario AplicarComisionTransferencia(
            MovimientoDiario movimientoPrincipalTransferencia,
            Usuario usuario,
            ComisionAhorrosAuxiliar comisionAhorros,
            int numeroMovimiento,
            bool indicadorCuentaSueldo)
        {
            try
            {
                return _servicioDominioCuenta.GenerarMovimientoDiarioDeComision(
                    movimientoPrincipalTransferencia,comisionAhorros,usuario,numeroMovimiento, indicadorCuentaSueldo);
            }
            catch (Exception)
            {
                throw new ValidacionException("Error en la generacion de movimiento de la comision");
            }

        }

        /// <summary>
        /// Realiza el ingreso de los motivos/vinculos de la transferencia CCE.
        /// </summary>
        /// <param name="motivoVinculo">vinculo y motivo</param>
        /// <param name="codigoTipoTransferencia">Codigo el tipo de transferencia</param>
        public OperacionesVinculosMotivos AgregarVinculoMotivo(
            IngresoVinculoMotivoDTO motivoVinculo,
            MovimientoDiario movimiento,
            string codigoTipoTransferencia)
        {
            if (motivoVinculo.IdVinculo <= 0 || motivoVinculo.IdMotivo <= 0)
                throw new ValidacionException("No se encontro un Vinculo o Motivo");

            var operacion = OperacionesVinculosMotivos
                .Generar(
                movimiento,
                motivoVinculo.IdVinculo,
                motivoVinculo.IdMotivo,
                motivoVinculo.VinculoEspecificado,
                motivoVinculo.MotivoEspecificado);

            return operacion;
        }
        /// <summary>
        /// Obtiene el Tipo de sub transaccoin 
        /// </summary>
        /// <param name="codigoTipoTransferencia">Codigo de tipo de transferencia</param>
        /// <returns>Retorna el subtipo de transaccion</returns>
        public static string ObtenerSubTipoTransaccionSalida(string codigoTipoTransferencia,string subCanal)
        {
            switch (codigoTipoTransferencia)
            {
                case TipoTransferencia.CodigoTransferenciaOrdinaria:
                    return subCanal == ((int)CanalInmediataEnum.SubCanalTinInmediata).ToString()
                        ? ((int)SubTipoTransaccionEnum.TransaccionInmediataOrdinariaSalida).ToString()
                        : ((int)SubTipoTransaccionEnum.TransaccionInteroperabilidad).ToString();
                case TipoTransferencia.CodigoPagoTarjeta:
                    return ((int)SubTipoTransaccionEnum.TransaccionInmediataTarjetaSalida).ToString();
                default:
                    throw new ValidacionException("Tipo de Tranferencia Interbacaria Inmediata CCE no válido "
                        + codigoTipoTransferencia + ".");
            }
        }

        /// <summary>
        /// Actualiza estado de la transaccion anteriormente registrada
        /// </summary>
        /// <param name="transaccion">Datos de la transaccion</param>
        /// <param name="orden">Datos de respuesta de la CCE</param>
        /// <param name="fechaSistema">Datos de respuesta de la CCE</param>
        public void ActualizarEstadoTransaccion(
            TransaccionOrdenTransferenciaInmediata transaccion,
            TramaProcesada tramaProcesada,
            OrdenTransferenciaRecepcionSalidaDTO orden, 
            DateTime fechaSistema)
        {
            var valorConversionCCE = orden.interbankSettlementAmount.ObtenerImporteOperacion();
            var fechaRespuesta = DateTime.ParseExact(orden.responseDate + " " + orden.responseTime,
                "yyyyMMdd HHmmss",CultureInfo.InvariantCulture);

            if (transaccion.IndicadorEstadoOperacion == General.Pendiente && orden.responseCode == CodigoRespuesta.Aceptada)
            {
                transaccion.ActualizarTransaccionSaliente(General.Confirmado, orden.instructionId, valorConversionCCE, fechaRespuesta, fechaSistema);
                tramaProcesada.ActualizarTramaProcesada(fechaSistema, CodigoRespuesta.Aceptada, TramaProcesada.Procesado);
            }
            else
            {
                transaccion.ActualizarInstruccion(orden.instructionId, fechaRespuesta, fechaSistema);
                tramaProcesada.ActualizarTramaProcesada(fechaSistema, CodigoRespuesta.Rechazada, TramaProcesada.Reversado);
            }                
        }

        /// <summary>
        /// Metodo que define si se reversara la comision
        /// </summary>
        /// <param name="tipoRespuesta">tipo de respuesta del servicio de orden transferencia</param>
        /// <returns>True si se reversara la comision</returns>
        public static bool DefinirReversarComision(string tipoRespuesta) => tipoRespuesta == CodigoRespuesta.ErrorCCE;

        /// <summary>
        /// Define si la transaccion agregar un vinculo y motivo
        /// </summary>
        /// <param name="datosOrden">Datos de la transaccion</param>
        /// <returns>True si se agregara vinculo y motivo</returns>
        public static bool DefinirAgregacionVinculoMotivo(RealizarTransferenciaInmediataDTO datosOrden) =>
            datosOrden.MotivoVinculo != null && datosOrden.CodigoTipoTransferenciaCce == TipoTransferencia.CodigoTransferenciaOrdinaria;

        /// <summary>
        /// Metodo que genera la comisión exonerada
        /// </summary>
        /// <param name="esMismaPlaza"></param>
        /// <param name="movimiento"></param>
        /// <param name="comisionAhorros"></param>
        /// <returns></returns>
        /// <exception cref="ValidacionException"></exception>
        public ComisionExonerada? GenerarComisionExonerada(
            bool esMismaPlaza,
            MovimientoDiario movimiento,
            ComisionAhorrosAuxiliar comisionAhorros)
        {
            if (!esMismaPlaza) return null;

            bool aplicaExoneracion = VerificarExoneracionSegunConfiguracion(
                movimiento.FechaOperacion,
                movimiento.CuentaEfectivo,
                comisionAhorros.ConfiguracionComision);

            if (!aplicaExoneracion) return null;

            return ComisionExonerada.Crear(movimiento, 
                comisionAhorros.ConfiguracionComision.CodigoComision);
        }

        /// <summary>
        /// Método que aplica la Exoneracion de comisión segun la configuracion
        /// </summary>
        /// <param name="fechaProceso"></param>
        /// <param name="configuracionComision"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <returns></returns>
        public bool VerificarExoneracionSegunConfiguracion(
            DateTime fechaProceso,
            CuentaEfectivo cuentaEfectivo,
            ConfiguracionComision configuracionComision)
        {
            var configProducto = configuracionComision.ConfiguracionProductos
                .FirstOrDefault(p => p.CodigoProducto == cuentaEfectivo.CodigoProducto);

            if (configProducto == null || configProducto.NumeroOperacionesLibresSinComision <= 0)
                return false;

            var configAgencia = configProducto.ConfiguracionAgencias
                .FirstOrDefault(a => a.CodigoAgencia == cuentaEfectivo.CodigoAgencia);

            if (configAgencia == null || !configAgencia.IndicadorAplicaOperacionesLibres)
                return false;

            var inicioMes = fechaProceso.ObtenerPrimerDia();
            var finMes = fechaProceso.ObtenerUltimoDia();

            var exoneradasDelMes = cuentaEfectivo
                .ComisionesExoneradas.Count(c =>
                    c.CodigoComision == configuracionComision.CodigoComision &&
                    c.FechaMovimiento >= inicioMes &&
                    c.FechaMovimiento <= finMes);

            return exoneradasDelMes < configProducto.NumeroOperacionesLibresSinComision;
        }

        #endregion Metodos Salientes
    }
}
