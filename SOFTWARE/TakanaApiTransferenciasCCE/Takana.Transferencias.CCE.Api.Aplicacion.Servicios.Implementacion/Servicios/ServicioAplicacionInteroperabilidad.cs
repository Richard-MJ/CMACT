using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Servicios;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Notificaciones;
using Takana.Transferencias.CCE.Api.Common.Utilidades;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;
/// <summary>
/// Clase de capa Aplicacion que se encarga de la interoperabilidad de transferencias inmediatas
/// </summary>
public class ServicioAplicacionInteroperabilidad : ServicioBase, IServicioAplicacionInteroperabilidad
{
    #region Propiedades
    private readonly IRepositorioRedis _repositorioRedis;
    private readonly IRepositorioGeneral _repositorioGeneral;
    private readonly IRepositorioOperacion _repositorioOperacion;
    private readonly IServicioAplicacionValidacion _aplicacionValidacion;
    private readonly IServicioAplicacionLavado _servicioAplicacionLavado;
    private readonly IServicioAplicacionCliente _servicioAplicacionCliente;
    private readonly IServicioAplicacionPeticion _servicioAplicacionPeticion;
    private readonly IServicioAplicacionParametroGeneral _serivicioAplicacionParametro;
    private readonly IServicioAplicacionTransaccionOperacion _servicioAplicacionTransaccionOperacion;
    private readonly IServicioAplicacionNotificaciones _servicioAplicacionNotificaciones;
    #endregion Propiedades

    #region Constructor
    public ServicioAplicacionInteroperabilidad(
        IContextoAplicacion contexto,
        IRepositorioGeneral repositorioGeneral,
        IRepositorioRedis repositorioRedis,
        IRepositorioOperacion repositorioOperaciones,
        IServicioAplicacionLavado servicioAplicacionLavado,
        IServicioAplicacionValidacion aplicacionValidacion,
        IServicioAplicacionCliente servicioAplicacionCliente,
        IServicioAplicacionPeticion servicioAplicacionPeticion,
        IServicioAplicacionParametroGeneral parametroGeneralServicio,
        IServicioAplicacionTransaccionOperacion servicioAplicacionTransaccionOperacion,
        IServicioAplicacionNotificaciones servicioAplicacionNotificaciones) : base(contexto)
    {
        _repositorioRedis = repositorioRedis;
        _repositorioGeneral = repositorioGeneral;
        _aplicacionValidacion = aplicacionValidacion;
        _repositorioOperacion = repositorioOperaciones;
        _servicioAplicacionLavado = servicioAplicacionLavado;
        _servicioAplicacionCliente = servicioAplicacionCliente;
        _serivicioAplicacionParametro = parametroGeneralServicio;
        _servicioAplicacionPeticion = servicioAplicacionPeticion;
        _servicioAplicacionTransaccionOperacion = servicioAplicacionTransaccionOperacion;
        _servicioAplicacionNotificaciones = servicioAplicacionNotificaciones;
    }
    #endregion Constructor

    #region Barrido de contactos
    /// <summary>
    /// Metodo que realiza el barrido de contactos para interoperabilidad
    /// </summary>
    /// <param name="datosEntrada">Datos de entrada de interoperabilidad</param>
    /// <returns>Datos de respuesta del barrido de contactos</returns>
    public async Task<ResultadoPrincipalBarridoDTO> BarridoContacto(
       EntradaBarridoDTO datosEntrada)
    {
        try
        {
            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var afiliacion = _repositorioGeneral.ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                x => x.NumeroCelular == datosEntrada.NumeroCelularOrigen
                && x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado).FirstOrDefault();

            if (afiliacion == null) throw new ValidacionException("No hay registro de afiliacion a interoperabilidad.");

            await _aplicacionValidacion.ValidarServicioInteroperabilidad(fechaSistema, afiliacion,datosEntrada);

            var directorios = _repositorioGeneral.ObtenerPorExpresionConLimite<DirectorioInteroperabilidad>(
                x => x.CodigoDirectorio != DatosGeneralesInteroperabilidad.DirectorioCCE).ValidarEntidadTexto("Directorios");

            var listaEntidades = await _servicioAplicacionCliente.ListarEntidadesFinancieras();

            var resultadoBarrido = await BarrerContactosCCE(datosEntrada, fechaSistema, 
                directorios.ToList(), listaEntidades, afiliacion);

            var limiteMaximo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                 DatosGeneralesInteroperabilidad.MaximoOperacion));

            var limiteMontoMinimo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                 DatosGeneralesInteroperabilidad.MinimoOperacion));

            var montoMaximoDia = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MontoLimiteDia));

            return resultadoBarrido.ARespuestaBarrido(limiteMaximo, limiteMontoMinimo, montoMaximoDia);

        }
        catch (Exception e)
        {
            throw new ValidacionException(e.Message);
        }

    }

    /// <summary>
    /// Realiza el barrido de contactos en la CCE utilizando los datos de entrada proporcionados.
    /// </summary>
    /// <param name="datosEntrada">Datos de entrada necesarios para el barrido de contactos.</param>
    /// <param name="fechaSistema">Objeto que contiene la fecha del sistema utilizada para el barrido.</param>
    /// <param name="directorios">Objeto que contiene los directorios.</param>
    /// <param name="entidades">Objeto que contiene las entidades.</param>
    /// <param name="datosAfiliacion">Objeto que contiene los datos de los afiliaciones.</param>
    /// <returns>Lista de objetos ResultadoBarridoDTO que representan los resultados del barrido de contactos.</returns>
    public async Task<List<ResultadoBarridoDTO>> BarrerContactosCCE(
        EntradaBarridoDTO datosEntrada, 
        Calendario fechaSistema, 
        List<DirectorioInteroperabilidad> directorios, 
        List<EntidadFinancieraTinDTO> entidades, 
        AfiliacionInteroperabilidadDetalle datosAfiliacion)
    {
        try
        {
            var numeroSeguimiento = _serivicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimientoBarrido);
            var minutosExpiracion = int.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MinutosBloquedoPorBarrido));

            var datosMaquetados = datosEntrada.ContactosBarrido.MaquetarDatos(fechaSistema, numeroSeguimiento);

            var bitacora = RegistrarBitacoraBarrido(datosEntrada, datosMaquetados, fechaSistema, numeroSeguimiento);

            var resultado = await EnviarBarridoYObtenerResultado(datosMaquetados, numeroSeguimiento, bitacora, fechaSistema);

            await _repositorioRedis.IncrementarAsync(datosAfiliacion.KeyIntentosBarrido, TimeSpan.FromMinutes(minutosExpiracion));

            bitacora.ActualicarBitacora(resultado.CodigoRespuesta, fechaSistema.FechaHoraSistema, General.UsuarioPorDefectoInteroperabilidad);

            if (resultado.CodigoRespuesta == DatosGeneralesInteroperabilidad.Rechazado)
            {
                var error = ObtenerDescripcionErrorBarrido(resultado);
                throw new ValidacionException(error.MensajeCliente ?? error.Nombre);
            }

            var resultadoFiltrado = resultado.Directorios?.FiltrarRespuesta(entidades, directorios);

            if (resultadoFiltrado == null || resultadoFiltrado.Any(x => x.EntidadesReceptor.Count < 1))
                bitacora.ContarComoIntentoFallido();    

            _repositorioGeneral.GuardarCambios();

            return resultadoFiltrado;
        }
        catch (Exception e)
        {
            throw new ValidacionException(e.Message);
        }
    }

    /// <summary>
    /// Crea y Guarda la bitacora de Barrido de contactos
    /// </summary>
    /// <param name="datosEntrada"></param>
    /// <param name="datosMaquetados"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="numeroSeguimiento"></param>
    /// <returns></returns>
    private BitacoraInteroperabilidadBarrido RegistrarBitacoraBarrido(
        EntradaBarridoDTO datosEntrada,
        EstructuraBarridoContacto datosMaquetados,
        Calendario fechaSistema,
        string numeroSeguimiento)
    {
        var bitacora = datosEntrada.CrearBitacoraBarrido(datosMaquetados, fechaSistema.FechaHoraSistema, numeroSeguimiento);
        _repositorioGeneral.Adicionar(bitacora);
        _repositorioGeneral.GuardarCambios();
        return bitacora;
    }

    /// <summary>
    /// Envia y Traduce la respuesta de la Barrido
    /// </summary>
    /// <param name="datosMaquetados"></param>
    /// <param name="numeroSeguimiento"></param>
    /// <returns></returns>
    private async Task<RespuestaBarridoDTO> EnviarBarridoYObtenerResultado(EstructuraBarridoContacto datosMaquetados, string numeroSeguimiento,
        BitacoraInteroperabilidadBarrido bitacora, Calendario fechaSistema)
    {
        try
        {
            var resultado = await _servicioAplicacionPeticion.EnviarBarridoContacto(datosMaquetados);
            return resultado.TraduccionRespuesta(numeroSeguimiento);
        } catch
        {
            bitacora.ActualicarBitacora(CodigoRespuesta.ErrorEnBarrido, fechaSistema.FechaHoraSistema, General.UsuarioPorDefectoInteroperabilidad);
            var error = _serivicioAplicacionParametro.ObtenerErrorLocal(CodigoRespuesta.ErrorEnBarrido);
            _repositorioGeneral.GuardarCambios();
            throw new ValidacionException(error.MensajeCliente);
        }
    }

    /// <summary>
    /// Obtiene la descripcion del error de barrido de contactos
    /// </summary>
    /// <param name="resultado"></param>
    /// <returns></returns>
    private CodigoRespuesta ObtenerDescripcionErrorBarrido(RespuestaBarridoDTO resultado)
    {
        if (resultado.CodigoRespuesta != DatosGeneralesInteroperabilidad.Aceptado)
            return _serivicioAplicacionParametro.ObtenerErrorLocal(resultado.RazonRespuesta);

        return new CodigoRespuesta();
    }

    #endregion Barrido de contactos

    #region Afiliacion de directorios CCE
    /// <summary>
    /// Metodo que gestionar afilia un cliente en el directorio CCE (afilia, desafilia, modifica)
    /// </summary>
    /// <param name="datosEntrada">Datos para el registro</param>
    /// <returns>Datos de respuesta de la afiliacion</returns>
    public async Task<RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO>> GestionarAfiliacionDirectorioCCE(
        EntradaAfiliacionDirectorioDTO datosEntrada, AfiliacionInteroperabilidadDetalle? afiliacionDetalle)
    {
        var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

        var numeroSeguimiento = _serivicioAplicacionParametro.ObtenerNumeroSeguimiento(DatosGenerales.CodigoSeguimientoAfiliacion);

        var datosMaquetados = datosEntrada.MaquetarDatos(fechaSistema, numeroSeguimiento);

        var bitacora = RegistrarBitacoraAfiliacion(datosEntrada, datosMaquetados, fechaSistema, numeroSeguimiento, afiliacionDetalle);

        try
        {
            var resultado = await EnviarAfiliacionYObtenerResultado(datosMaquetados, numeroSeguimiento);

            ActualizarEstadoBitacora(datosEntrada, resultado, bitacora, fechaSistema);

            if (resultado.Respuesta == DatosGeneralesInteroperabilidad.Aceptado)
                return ConstruirRespuestaExitoso(resultado);

            var error = _serivicioAplicacionParametro.ObtenerErrorLocal(resultado.RazonRespuesta);
            return ConstruirRespuestaError(resultado, error);
        }
        catch (Exception)
        {
            var codigoRespuesta = datosEntrada.TipoInstruccion == DatosGeneralesInteroperabilidad.TipoInstruccionEliminacion ?
                CodigoRespuesta.ErrorDesafiliacion : CodigoRespuesta.ErrorAfiliacion;

            var error = _serivicioAplicacionParametro.ObtenerErrorLocal(codigoRespuesta);

            ActualizarEstadoBitacora(bitacora, fechaSistema, afiliacionDetalle, codigoRespuesta);

            throw new ValidacionException(error.MensajeCliente);
        }
    }

    /// <summary>
    /// Crea y Guarda la bitacora de afiliación o desafiliación
    /// </summary>
    /// <param name="datosEntrada"></param>
    /// <param name="datosMaquetados"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="numeroSeguimiento"></param>
    /// <returns></returns>
    private BitacoraInteroperabilidadAfiliacion RegistrarBitacoraAfiliacion(
        EntradaAfiliacionDirectorioDTO datosEntrada, 
        EstructuraRegistroDirectorio datosMaquetados, 
        Calendario fechaSistema, 
        string numeroSeguimiento,
        AfiliacionInteroperabilidadDetalle? afiliacionDetalle)
    {
        var estadoActual = afiliacionDetalle?.IndicadorEstadoAfiliado == General.Si
            ? DatosGeneralesInteroperabilidad.Afiliado : DatosGeneralesInteroperabilidad.Desafiliado;

        var bitacora = datosEntrada.CrearBitacoraAfiliacion(datosMaquetados, fechaSistema.FechaHoraSistema, numeroSeguimiento, estadoActual);
        _repositorioGeneral.Adicionar(bitacora);
        _repositorioGeneral.GuardarCambios();
        return bitacora;
    }

    /// <summary>
    /// Envia y Traduce la respuesta de la afiliación o desafiliación
    /// </summary>
    /// <param name="datosMaquetados"></param>
    /// <param name="numeroSeguimiento"></param>
    /// <returns></returns>
    private async Task<RespuestaRegistroDirectorioDTO> EnviarAfiliacionYObtenerResultado(EstructuraRegistroDirectorio datosMaquetados, string numeroSeguimiento)
    {
        var resultado = await _servicioAplicacionPeticion.EnviarAfiliacionDirectorio(datosMaquetados);
        return resultado.TraduccionRespuesta(numeroSeguimiento);
    }

    /// <summary>
    /// Actualizar el estado de bitacora de afiliación
    /// </summary>
    /// <param name="datosEntrada"></param>
    /// <param name="resultado"></param>
    /// <param name="bitacora"></param>
    /// <param name="fechaSistema"></param>
    private void ActualizarEstadoBitacora(
        EntradaAfiliacionDirectorioDTO datosEntrada, 
        RespuestaRegistroDirectorioDTO resultado, 
        BitacoraInteroperabilidadAfiliacion bitacora, 
        Calendario fechaSistema)
    {
        var estadoAfiliacion = ServicioDominioInteroperabilidad.ObtenerEstadoAfiliacionBitacora(
            datosEntrada.TipoInstruccion, resultado.Respuesta);

        bitacora.ActualizarBitacora(resultado.RazonRespuesta, fechaSistema.FechaHoraSistema,
            General.UsuarioPorDefectoInteroperabilidad, estadoAfiliacion);
        _repositorioGeneral.GuardarCambios();
    }

    /// <summary>
    /// Actualizar el estado de bitacora de afiliación
    /// </summary>
    /// <param name="bitacora"></param>
    /// <param name="fechaSistema"></param>
    /// <param name="afiliacionDetalle"></param>
    private void ActualizarEstadoBitacora(
        BitacoraInteroperabilidadAfiliacion bitacora,
        Calendario fechaSistema,
        AfiliacionInteroperabilidadDetalle? afiliacionDetalle,
        string codigoRespuesta)
    {
        var estadoActual = afiliacionDetalle?.IndicadorEstadoAfiliado == General.Si 
            ? DatosGeneralesInteroperabilidad.Afiliado : DatosGeneralesInteroperabilidad.Desafiliado;

        bitacora.ActualizarBitacora(codigoRespuesta,
            fechaSistema.FechaHoraSistema,
            General.UsuarioPorDefectoInteroperabilidad,
            estadoActual);

        _repositorioGeneral.GuardarCambios();
    }

    /// <summary>
    /// Construye respuesta de exito
    /// </summary>
    /// <param name="resultado"></param>
    /// <returns></returns>
    private RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO> ConstruirRespuestaExitoso(
        RespuestaRegistroDirectorioDTO resultado)
    {
        return new RespuestasExtensiones<RespuestaRegistroDirectorioDTO>()
            .ConstruirRespuestaDatos(resultado.Respuesta, resultado.Respuesta, "Exitoso", resultado);
    }

    /// <summary>
    /// Construye respuesta de Error
    /// </summary>
    /// <param name="resultado"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    private RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO> ConstruirRespuestaError(
        RespuestaRegistroDirectorioDTO resultado, CodigoRespuesta error)
    {
        return new RespuestasExtensiones<RespuestaRegistroDirectorioDTO>()
            .ConstruirRespuestaDatos(resultado.Respuesta, error.Nombre, error.MensajeCliente, resultado);
    }
    #endregion Afiliacion de directorios CCE

    #region Consulta con número celular
    /// <summary>
    /// Obtiene datos del cliente receptor consultando a la CCE mediante el numero de celular
    /// </summary>
    /// <param name="datosConsulta">Datos para realizar la consulta</param>
    /// <returns>Datos del cliente recepetor</returns>
    public async Task<ResultadoConsultaCuentaInteroperabilidadDTO> ConsultaCuentaReceptorPorCelular(ConsultaCuentaCelularDTO datosConsulta)
    {
        try
        {
            var cuerpoConsulta = datosConsulta.AConsultaCuenta(_contextoAplicacion.IdTerminalOrigen);

            var consulta = await _servicioAplicacionTransaccionOperacion.ConsultaCuentaReceptorPorCCE(cuerpoConsulta);

            return consulta.AResultadoConsulta();
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        }
    }
    #endregion Consulta con numero celular

    #region Operaciones interoperabilidad

    /// <summary>
    /// Obtiene datos del cliente originante
    /// </summary>
    /// <param name="numeroCuenta">Numero de cuenta</param>
    /// <returns>Datos del cliente originante.</returns>
    public async Task<RespuetaConsultaCuentaDTO> ObtenerDatosClienteOriginante(string numeroCuenta)
    {
        var consulta = await _servicioAplicacionCliente.ObtenerDatosCuentaOrigen(numeroCuenta);

        return consulta.AResultadoConsultaOriginante();
    }

    /// <summary>
    /// Calcula los totales de la comision para la operacion
    /// </summary>
    /// <param name="datosCalculo">Datos para realizar el calculo</param>
    /// <returns>Retorna los datos del calculo de montos</returns>
    public async Task<ResultadoCalculoMonto> CalcularMontosTotales(CalculoComisionDTO datosCalculo)
    {
        try
        {
            var comision = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
            DatosGeneralesInteroperabilidad.Comision)) + 0.0m;

            datosCalculo.Comision.Porcentaje = 0;
            datosCalculo.Comision.Minimo = comision;
            datosCalculo.Comision.Maximo = comision;
            
            var comisionCalculada = await _servicioAplicacionTransaccionOperacion.CalcularMontosTotales(datosCalculo);

            return comisionCalculada.ControlMonto.AResultadoCalculoMonto();
        }
        catch (Exception e)
        {
            throw new ValidacionException(e.Message);
        }
        
    }
    /// <summary>
    /// Realiza la transferencia completa para interoperabilidad
    /// </summary>
    /// <param name="orden">Datos de la transferencia</param>
    /// <returns>Resultado de la transferencia</returns>
    public async Task<ResultadoTransferenciaCanalElectronico> RealizarTransferenciaInteroperabilidad(
        OrdenTransferenciaCanalElectronicoDTO orden)
    {
        await ValidarOperacion(orden.AValidarOperacion());

        var subCanal = ((int)CanalCCE.CanalInmediataEnum.SubCanalInteroperabilidad).ToString();

        var fechaSistema = _repositorioOperacion.ObtenerCalendarioCuentaEfectivo();

        var tipoDocumentoReceptor = _serivicioAplicacionParametro.ObtenertipoDocumentoTakana(
            orden.ResultadoConsultaCuenta.TipoDocumentoReceptor);

        var clienteBeneficiario = orden.ResultadoConsultaCuenta.AClienteExternoDTO(tipoDocumentoReceptor);

        var numeroLavado = _servicioAplicacionLavado.RegistrarNumeroLavado(
            orden.ResultadoConsultaCuenta.Comision.CodigoMoneda,
            fechaSistema.FechaHoraSistema, orden.ControlMonto.Monto,
            orden.ResultadoConsultaCuenta.TipoTransaccion,
            orden.ResultadoConsultaCuenta.CodigoCuentaInterbancariaDeudor,
            subCanal, orden.NumeroCuenta, clienteBeneficiario);

        var sesionUsuario = _contextoAplicacion.ASesionUsuarioCanalElectronico();

        var cuerpoTransferencia = orden.ARealizarTranferenciaCanalElectronico(sesionUsuario, numeroLavado, subCanal);

        var (operacion, asiento, transferencia, movimientoDiario) = await _servicioAplicacionTransaccionOperacion
            .RealizarTransferenciaSalienteCCE(cuerpoTransferencia, true);

        await _servicioAplicacionNotificaciones.GenerarOperacionNotificada
            (movimientoDiario!.NumeroMovimiento.ToString(), movimientoDiario.SubTipoTransaccionMovimiento, movimientoDiario.FechaMovimiento);

        var cuerpoOrden = operacion.ADatosOrdenTransferenciaInteroperabilidad(
            orden, ConceptoCobroCCE.CodigoConceptoOtro, sesionUsuario);

        await _servicioAplicacionTransaccionOperacion.EnviarOrdenTransferenciaCCE(cuerpoOrden, fechaSistema, asiento, transferencia);

        await _repositorioRedis.EliminarAsync(cuerpoOrden.KeyIntentosBarrido);

        await _servicioAplicacionCliente.EnviarNotificacionAntifraudeUNIBANCA(cuerpoOrden.NumeroMovimiento, NotificacionUnibancaDTO.NotificacionTransferenciaCanalElectronico);

        await EnviarNotificacionCliente(cuerpoTransferencia, movimientoDiario);

        return operacion.AResultadoOperacionTransferencia(fechaSistema.FechaHoraSistema);
    }

    private async Task EnviarNotificacionCliente(RealizarTransferenciaInmediataDTO cuerpoTransferencia, MovimientoDiario movimientoDiario)
    {
        var afiliacionDetalleExistente = _repositorioOperacion.ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(x =>
            x.NumeroCelular == movimientoDiario.Cuenta.Cliente.NumeroTelefonoSecundario
                && x.CodigoCuentaInterbancario == movimientoDiario.Cuenta.CodigoCuentaInterbancario
                && x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado).FirstOrDefault();

        if (afiliacionDetalleExistente?.IndicadorEnviarNotificacion == General.Si)
            await _servicioAplicacionNotificaciones.EnviarNotificacionCuentaEfectivo(movimientoDiario);
    }

    /// <summary>
    /// Valida la operacion con los parametros de interoperabilidad obtenidos
    /// </summary>
    /// <param name="datosValidar">Datos a validar</param>
    public async Task ValidarOperacion(ValidarOperacionDTO datosValidar)
    {
        try
        {
            decimal montoSumaActual = 0.0m;

            var limiteMaximo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MaximoOperacion)) + 0.0m;
            var limiteMinimo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MinimoOperacion)) + 0.0m;

            ServicioDominioInteroperabilidad.ValidarMonto(limiteMaximo, limiteMinimo, datosValidar.MontoOperacion);

            var montoMaximoDia = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MontoLimiteDia)) + 0.0m;

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var transaccion = _repositorioOperacion
                .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(x => 
                    x.FechaOperacion.Date == fechaSistema.FechaHoraSistema.Date && 
                    x.IndicadorEstadoOperacion == General.Finalizado &&
                    x.CodigoCanal == DatosGeneralesInteroperabilidad.CanalInteroperabilidad &&
                    x.CodigoCuentaInterbancariaOriginante == datosValidar.CodigoCCIOriginante && 
                    x.IndicadorTransaccion == General.Originante)
                .ToList();

            if (transaccion != null) montoSumaActual = transaccion.Sum(x => x.MontoTransferencia);

            ServicioDominioInteroperabilidad.ValidarMontoMaximoTransaccionDia(montoMaximoDia, montoSumaActual,
                datosValidar.MontoOperacion);

            var cantidadTransaccionesDia = int.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MaximoOperacionesDia));

            ServicioDominioInteroperabilidad.ValidarCantidadTransaccionesDia(cantidadTransaccionesDia,
                transaccion.Count);
        }
        catch (Exception e)
        {
            throw new ValidacionException(e.Message);
        }
    }

    #endregion Operaciones interoperabilidad

    #region Operacion por Pagos QR
    /// <summary>
    /// Genera la autorizacion token para el uso de los servicios QR
    /// </summary>
    /// <returns>Token de autorizacion</returns>
    public async Task<string> GenerarAutorizacionToken()
    {
        try
        {
            var estructura = InteroperabilidadExtension.AGenerarToken();
            var resultadoToken = await _servicioAplicacionPeticion.EnviarGenerarToken(estructura);

            if (resultadoToken.header.codReturn != DatosValoresFijos.RespuestaExitosaQR)
                throw new Exception("Generacion Token rechazado: " + resultadoToken.header.txtReturn);

            return resultadoToken.token;
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        }
    
    }
    /// <summary>
    /// Genera el token QR para Directorio CCE
    /// </summary>
    /// <param name="generar">Datos de generacion QR</param>
    /// <returns>QR generado</returns>
    public async Task<RespuestaGenerarQR> GenerarQR(GenerarQRDTO generar)
    {
        try
        {
            var token = await GenerarAutorizacionToken();

            var estructura = InteroperabilidadExtension.AGenerarQR(generar);

            var resultadoToken = await _servicioAplicacionPeticion.EnviarGenerarQr(estructura, token);

            if (resultadoToken.header.codReturn != DatosValoresFijos.RespuestaExitosaQR)
            {
                var error = _serivicioAplicacionParametro.ObtenerErrorLocal(resultadoToken.header.txtReturn);
                throw new ValidacionException("Generacion QR rechazado: " + error.Nombre);
            }

            return resultadoToken.ARespuestaGeneracionQR();
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        }
    }
    /// <summary>
    /// Lee el QR con el hash
    /// </summary>
    /// <param name="datosLectura">Datos de lectura QR</param>
    /// <returns>Datos del cliente que genero el QR</returns>
    public async Task<RespuestaConsultaDatosQRDTO> LeerQR(LeerQRDTO datosLectura)
    {
        try
        {
            var token = await GenerarAutorizacionToken();

            var estructura = datosLectura.ALeerQR();

            var resultadoToken = await _servicioAplicacionPeticion.EnviarLecturaQr(estructura, token);

            if (resultadoToken.header.codReturn != DatosGeneralesInteroperabilidad.Exitoso)
            {
                var codigoError = _serivicioAplicacionParametro.ObtenerErrorLocal(resultadoToken.header.txtReturn);
                throw new Exception("Lectura de QR rechazado: " + codigoError.Nombre);
            }
               
            return resultadoToken.ARespuestaConsultaDatosQR();
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        } 
    }
    /// <summary>
    /// Obtiene los datos de un QR con el identificador
    /// </summary>
    /// <param name="datos">Datos para obtener</param>
    /// <returns>Respuesta de la consulta de datos</returns>
    public async Task<RespuestaConsultaDatosQRDTO> ObtenerDatosQR(ObtenerDatosQRDTO datos)
    {
        try
        {
            var token = await GenerarAutorizacionToken();

            var estructura = datos.AObtenerDatosQR();

            var resultadoToken = await _servicioAplicacionPeticion.ProcesarPeticionObtenerDatosQr(estructura, token);

            if (resultadoToken.header.codReturn != DatosGeneralesInteroperabilidad.Exitoso)
            {
                var codigoError = _serivicioAplicacionParametro.ObtenerErrorLocal(resultadoToken.header.txtReturn);
                throw new Exception("Obtener datos de QR rechazado: " + codigoError.Nombre);
            }

            return resultadoToken.ARespuestaConsultaDatosQR();
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        }
    }
    /// <summary>
    /// Obtiene los datos de la cuenta receptora mediante QR
    /// </summary>
    /// <param name="datosConsulta">datos necesarios para la consulta</param>
    /// <returns>Datos del cliente receptor</returns>
    public async Task<ResultadoConsultaCuentaInteroperabilidadDTO> ObtenerDatosCuentaReceptoraQR(
        ConsultarCuentaQRDTO datosConsulta)
    {
        bool consultaPorQr = true;

        var cuentaOriginante = await _servicioAplicacionCliente.ObtenerDatosCuentaOrigen(datosConsulta.NumeroCuentaOriginante);

        ServicioDominioInteroperabilidad.ValidarMonedaSoles(cuentaOriginante.CodigoMoneda,
            datosConsulta.CodigoMoneda);

        var directorios = _repositorioOperacion.ObtenerPorExpresionConLimite<DirectorioInteroperabilidad>(
            x=>x.CodigoDirectorio != DatosGeneralesInteroperabilidad.DirectorioCCE).ValidarEntidadTexto("Directorios");

        var (canal, tipoProxy, valorPorxy) = directorios.ToList().FiltroDirectorioDestino(
            datosConsulta.CodigoEntidadReceptora, datosConsulta.IdentificadorCuenta);

        if (!string.IsNullOrEmpty(tipoProxy))
            datosConsulta.IdentificadorCuenta = datosConsulta.CodigoEntidadReceptora;

        var cuerpoConsulta = datosConsulta.AObtenerDatosReceptorQR(
            cuentaOriginante, canal, tipoProxy, valorPorxy, _contextoAplicacion.IdTerminalOrigen);

        var consulta = await _servicioAplicacionTransaccionOperacion.ConsultaCuentaReceptorPorCCE(cuerpoConsulta, consultaPorQr);

        return consulta.AResultadoConsulta();
    }
    /// <summary>
    /// Consulta la cuenta del cliente receptor en una solo paso
    /// </summary>
    /// <param name="datosLectura">Datos necesarios para la consulta de cuenta</param>
    /// <returns>Datos completos del cliente recepor</returns>
    public async Task<RespuestaConsultaCompletaQR> ConsultarCuentaReceptorPorQR(
        ConsultaCuentaCompletaQRDTO datosLectura)
    {
        try
        {
            var lecturaQR = await LeerQR(datosLectura);

            var cuerpoConsulta = datosLectura.AConsultaCuentaQR(lecturaQR);
            var consulta = await ObtenerDatosCuentaReceptoraQR(cuerpoConsulta);

            var listaEntidades = await _servicioAplicacionCliente.ListarEntidadesFinancieras();

            var entidad = listaEntidades.FirstOrDefault(entidad =>
                entidad.CodigoEntidad == consulta.ResultadoConsultaCuenta.CodigoEntidadReceptora);

            if (entidad == null) throw new ValidacionException("Entidad receptora no registrada");

            var limiteMaximo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                     DatosGeneralesInteroperabilidad.MaximoOperacion));

            var limiteMontoMinimo = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                 DatosGeneralesInteroperabilidad.MinimoOperacion));

            var montoMaximoDia = decimal.Parse(_serivicioAplicacionParametro.obtenerParametrosConfiguracion(
                DatosGeneralesInteroperabilidad.MontoLimiteDia));

            return consulta.AConsultaCuentaCompletaQR(entidad.NombreEntidad, lecturaQR, limiteMaximo,
                limiteMontoMinimo, montoMaximoDia);
        }
        catch (Exception excepcion)
        {
            throw new ValidacionException(excepcion.Message);
        }
        
    }
    #endregion Operacion por Pagos QR
}
