using AutorizadorCanales.Aplication.Common.ServicioPinOperacionesApi;
using AutorizadorCanales.Aplication.Features.Autenticacion.Commands;
using AutorizadorCanales.Aplication.Features.Common;
using AutorizadorCanales.Aplication.Servicios;
using AutorizadorCanales.Aplication.Servicios.Afiliacion;
using AutorizadorCanales.Aplication.Servicios.Autenticacion;
using AutorizadorCanales.Aplication.Servicios.Sesion;
using AutorizadorCanales.Contracts.SG.Response;
using AutorizadorCanales.Core.Constantes;
using AutorizadorCanales.Core.DTO;
using AutorizadorCanales.Core.Utilitarios;
using AutorizadorCanales.Domain.Entidades;
using AutorizadorCanales.Domain.Entidades.CF;
using AutorizadorCanales.Domain.Entidades.CL;
using AutorizadorCanales.Domain.Entidades.SG;
using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Domain.Extensiones;
using AutorizadorCanales.Domain.Repositorios;
using AutorizadorCanales.Domain.Validaciones;
using AutorizadorCanales.Excepciones;
using AutorizadorCanales.Excepciones.Constantes;
using AutorizadorCanales.Logging.Interfaz;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Transactions;

namespace AutorizadorCanales.Infrastructure.Servicios;

public class ServicioAutenticacion : ServicioBase<ServicioAutenticacion>, IServicioAutenticacion
{
    private readonly IRepositorioLectura _repositorioLectura;
    private readonly IRepositorioEscritura _repositorioEscritura;
    private readonly IServicioGeneradorToken _servicioGeneradorToken;
    private readonly IServicioPinOperaciones _servicioPinOperaciones;
    private readonly IServicioSesionCanalElectronico _servicioSesionCanalElectronico;
    private readonly IServicioAfiliacion _servicioAfiliacion;
    private readonly IServicioAlertaInicioSesion _servicioAlertaInicioSesion;

    public ServicioAutenticacion(
        IRepositorioLectura repositorioLectura,
        IServicioGeneradorToken servicioGeneradorToken,
        IServicioPinOperaciones servicioPinOperaciones,
        IRepositorioEscritura repositorioEscritura,
        IServicioAfiliacion servicioAfiliacion,
        IServicioSesionCanalElectronico servicioSesionCanalElectronico,
        IServicioAlertaInicioSesion servicioAlertaInicioSesion,
        IContexto contexto,
        IBitacora<ServicioAutenticacion> bitacora) : base(contexto, bitacora)
    {
        _repositorioLectura = repositorioLectura;
        _servicioGeneradorToken = servicioGeneradorToken;
        _servicioPinOperaciones = servicioPinOperaciones;
        _repositorioEscritura = repositorioEscritura;
        _servicioAfiliacion = servicioAfiliacion;
        _servicioSesionCanalElectronico = servicioSesionCanalElectronico;
        _servicioAlertaInicioSesion = servicioAlertaInicioSesion;
    }

    /// <summary>
    /// Autentica al cliente
    /// </summary>
    /// <param name="datos">Datos para generar el acceso de cliente</param>
    /// <returns>Retorna los datos de acceso</returns>
    public async Task<JsonObject> AutenticarCliente(AutenticarClienteCommand datos)
    {
        var tarjeta = await ObtenerTarjetaAsociada(datos.NumeroTarjeta);

        try
        {
            var tipoDocumento = await _repositorioLectura.ObtenerPorCodigoAsync<TipoDocumento>
            (EntidadEmpresa.EMPRESA, datos.IdTipoDocumento);

            if (!tipoDocumento.IndicadorHomeBankingAppCanales)
                throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();

            if (tipoDocumento.EsTipoDocumentoIdentidad && tipoDocumento.Mascara.Length != datos.NumeroDocumento.Trim().Length)
                throw new ExcepcionAUsuario("06", $"El número de caracteres del {tipoDocumento.DescripcionTipoDocumento} es incorrecto.");

            var documentoCliente = tarjeta.Duenio!.ObtenerTipoDocumento(tipoDocumento.CodigoTipoDocumento);

            if (documentoCliente.NumeroDocumento != datos.NumeroDocumento)
                throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();

            var parametrosCanalesGeneral =
                await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<ParametroCanalElectronicoGeneral>(p => p.IndicadorEstado);

            var numeroDiasVencimiento = parametrosCanalesGeneral.FirstOrDefault
             (p => p.IdParametroCanalElectronico == (int)ModeloParametroCanalElectronicoGeneral.NUMERO_DIAS_VENCIMIENTO);

            if (datos.IdTipoOperacionCanalElectronico == TipoOperacionLogin.AFILIACION)
                await _servicioAfiliacion.AfiliarCanalesElectronicos(
                    idAudiencia: datos.SistemaCliente.IdAudiencia,
                    passwordCajero: datos.PasswordPrimario!,
                    passwordInternet: datos.Password,
                    numeroDiasVencimiento: int.Parse(numeroDiasVencimiento!.ValorParametro!),
                    tarjeta: tarjeta);

            tarjeta.ValidarTarjetaInicioSesion(Contexto.IndicadorCanal);

            var clientesApiPorCliente = await _repositorioEscritura
                .ObtenerPorExpresionConLimiteAsync<ClienteApi>(c => c.CodigoCliente == tarjeta.CodigoCliente
                && (c.IndicadorEstado == ClienteApi.AFILIADO || c.IndicadorEstado == ClienteApi.BLOQUEADO));

            var clientesApiPorAudiciencia = clientesApiPorCliente
                   .Where(c => c.IdSistemaCliente == datos.SistemaCliente.IdAudiencia && c.NumeroTarjeta == tarjeta.NumeroTarjeta)
                   .ToList();

            if (clientesApiPorAudiciencia.Count > 1)
                throw new ExcepcionAUsuario("06", "Error al recuperar datos de cliente.");

            var clienteApiOtraAudiencia = clientesApiPorCliente
                .Where(c => c.IdSistemaCliente != datos.SistemaCliente.IdAudiencia
                    && c.SistemaCliente.IndicadorBloqueoCanalElectronico == EstadoEntidad.SI)
                .OrderByDescending(c => c.FechaPrimerIntentoFallido)
                .FirstOrDefault();

            ClienteApi clienteApi;

            if (clientesApiPorAudiciencia.Count == 0)
            {
                clienteApi = ClienteApi
                    .Crear(datos.SistemaCliente.IdAudiencia, tarjeta);
                if (clienteApiOtraAudiencia != null
                    && clienteApiOtraAudiencia.NumeroIntentosFallidos > 0)
                {
                    clienteApi.ModificarDatosDeIntentosFallidos(clienteApiOtraAudiencia);
                }
                await _repositorioEscritura.AdicionarAsync(clienteApi);
            }
            else
                clienteApi = clientesApiPorAudiciencia.Single();

            var parametrosCanalElectronico =
               await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<ParametroCanalElectronico>(p =>
                   p.CodigoSubCanal == Contexto.IndicadorSubCanal && p.CodigoCanal == Contexto.IndicadorCanal && p.CodigoMoneda == Moneda.SOLES);

            var numeroHorasBloqueo = (int)parametrosCanalElectronico.First
                (x => x.CodigoParametro == ParametroCanalElectronico.TIEMPO_BLOQUEO).ValorParametro;
            var segundosRangoIntentosFallidos = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.TIEMPO_INGRESO_CLAVE).ValorParametro;
            var maximoIntentosFallidos = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.NUMERO_INTENTOS_CLAVE).ValorParametro;

            var dispositivoCorrespondiente = tarjeta.DispositivoCorrespondiente(Utils.ObtenerGuidsPorCanal(Contexto.IndicadorCanal, datos.Guids));

            if (datos.IdTipoOperacionCanalElectronico == TipoOperacionLogin.LOGIN_BIOMETRIA)
            {
                if (dispositivoCorrespondiente == null)
                    throw new ExcepcionAUsuario("06", "Dispositivo no registrado");

                await LoginClienteBiometria(datos,
                    clienteApi,
                    tarjeta,
                    dispositivoCorrespondiente,
                    numeroHorasBloqueo);
            }
            else
                await AutenticarClienteClaveInternet(clienteApi, tarjeta, datos.Password,
                    numeroHorasBloqueo, segundosRangoIntentosFallidos, maximoIntentosFallidos);

            var (usuarioLogueado, dispositivo) = await CrearUsuarioLogueado(datos.IdTipoOperacionCanalElectronico,
              clienteApiOtraAudiencia,
              clienteApi,
              parametrosCanalesGeneral,
              tarjeta,
              dispositivoCorrespondiente);

            if (usuarioLogueado == null)
                throw new ExcepcionAUsuario("06", "Error al validar usuario");

            var traceSeisUltimosDigitos = "000000" + datos.IdTrama;
            traceSeisUltimosDigitos = traceSeisUltimosDigitos.Substring(traceSeisUltimosDigitos.Length - 6);

            var (tokenAcceso, tokenGuid) = _servicioGeneradorToken.GenerarToken(
                autenticarCommand: datos,
                usuario: usuarioLogueado,
                roles: Utilitarios.ObtenerRolesFinales(),
                idSesion: traceSeisUltimosDigitos,
                sistemaCliente: datos.SistemaCliente);

            Bitacora.Trace("Generando token refresco para usuario API " + datos.NumeroTarjeta + " en audiencia "
                + datos.SistemaCliente.IdAudiencia + ".");

            var tokenRefresco = await _servicioGeneradorToken.GenerarTokenRefresco(new GenerarTokenRefrescoDto
                (datos.SistemaCliente.IdAudiencia,
                usuarioLogueado.IdClienteApi,
                usuarioLogueado.CodigoUsuario,
                tokenAcceso,
                datos.SistemaCliente.IndicadorCanal,
                TokenRefresco.TIPO_AUTENTICACION_PASSWORD));

            await _servicioSesionCanalElectronico.CrearSesionCanalElectronico
               (usuarioLogueado.IdRegistroDispositivoNuevo != null, dispositivo, tokenGuid);

            var tiempoMaximoInactividad = (int)parametrosCanalElectronico.First
                (p => p.CodigoParametro == ParametroCanalElectronico.TIEMPO_INACTIVIDAD).ValorParametro;

            var autenticacion = new AutenticacionResponse(
                tokenAcceso,
                AutenticacionConstante.TIPO_BEARER,
                (int)TimeSpan.FromMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).TotalSeconds,
                (int)tiempoMaximoInactividad,
                tokenRefresco,
                datos.SistemaCliente.IdAudiencia,
                usuarioLogueado.DispositivoAutorizado.ToString().ToLower(),
                usuarioLogueado.CodigoUsuario,
                usuarioLogueado.CodigoUsuario,
                traceSeisUltimosDigitos,
                Contexto.FechaSistema.ToUniversalTime().ToString(),
                Contexto.FechaSistema.AddMinutes(_servicioGeneradorToken.ObtenerMinutosVidaToken()).ToUniversalTime().ToString());

            var autenticacionObject = JsonNode.Parse
                (JsonSerializer.Serialize(autenticacion))!.AsObject();

            if (usuarioLogueado.IdRegistroDispositivoNuevo != null)
                autenticacionObject.Add("newGuid", usuarioLogueado.IdRegistroDispositivoNuevo);
            else
            {
                await ValidarEnviarAlertaInicioSesion(datos, EstadoEntidad.SI, dispositivoCorrespondiente?.DispositivoId);
            }

            return autenticacionObject;
        }
        catch (ExcepcionAUsuario ex)
        {
            await ValidarEnviarAlertaInicioSesion(datos, EstadoEntidad.NO);
            throw;
        }
    }

    private async Task<Tarjeta> ObtenerTarjetaAsociada(string numeroTarjeta)
    {
        var tarjeta = await _repositorioEscritura.ObtenerTarjetaPorCodigoAsync(decimal.Parse(numeroTarjeta));
        if (tarjeta?.Duenio is null) throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion();

        return tarjeta;
    }

    private async Task ValidarEnviarAlertaInicioSesion(AutenticarClienteCommand datos, string estadoInicioSesion, string? idDispositivo = null)
    {
        if (datos.IdTipoOperacionCanalElectronico != TipoOperacionLogin.AFILIACION)
        {
            await _servicioAlertaInicioSesion.RegistrarAlertaInicioSesion(estadoInicioSesion, datos.NumeroTarjeta, idDispositivo);
        }
    }

    /// <summary>
    /// Crea el dto de usuario logueado
    /// </summary>
    /// <param name="idTipoOperacionCanalElectronico">id tipo de operacion</param>
    /// <param name="clienteApiOtraAudiencia">ciente api de otra audiencia</param>
    /// <param name="clienteApi">cliente api</param>
    /// <param name="parametrosCanalesGeneral">parámetros canales general</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    /// <param name="dispositivoCanalElectronico">Lista de dispositivos</param>
    /// <returns>Dto usuario logueado</returns>
    private async Task<(UsuarioLogueadoDto, DispositivoCanalElectronico?)> CrearUsuarioLogueado(
        int idTipoOperacionCanalElectronico,
        ClienteApi clienteApiOtraAudiencia,
        ClienteApi clienteApi,
        List<ParametroCanalElectronicoGeneral> parametrosCanalesGeneral,
        Tarjeta tarjeta,
        DispositivoCanalElectronico? dispositivoCanalElectronico)
    {
        if (clienteApiOtraAudiencia != null)
            clienteApiOtraAudiencia.ModificarDatosDeIntentosFallidos(clienteApi);

        string indicadorAplicarVigenciaClaveInternet = parametrosCanalesGeneral
            .FirstOrDefault(p => p.IdParametroCanalElectronico == (int)ModeloParametroCanalElectronicoGeneral.INDICADOR_VIGENCIA_CLAVE)!.ValorParametro!;

        int numeroDiasVencimientoAfiliacion = int.Parse(parametrosCanalesGeneral
            .FirstOrDefault(p => p.IdParametroCanalElectronico == (int)ModeloParametroCanalElectronicoGeneral.NUMERO_DIAS_VENCIMIENTO)!.ValorParametro!);

        int numeroDiasTranscurridos = tarjeta.AfiliacionCanalElectronico!
            .FechaAfiliacionPrincipal
            .ObtenerDiasTranscurridos(Contexto.FechaSistema);

        TarjetaHomebankingEmpresarial tarjetaEmpresarial = null;
        if (Contexto.EsCanalEmpresarial())
            tarjetaEmpresarial = await _repositorioEscritura.ObtenerPorCodigoAsync<TarjetaHomebankingEmpresarial>
                (EntidadEmpresa.EMPRESA, tarjeta.NumeroTarjeta);

        if (indicadorAplicarVigenciaClaveInternet.Equals(EstadoEntidad.SI)
                && numeroDiasTranscurridos > numeroDiasVencimientoAfiliacion
                && tarjeta.AfiliacionCanalElectronico.FechaCaducidadClaveInternet.Date < Contexto.FechaSistema)
        {
            using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                tarjeta.AfiliacionCanalElectronico
                    .ActualizarVigenciaDeClaveInternet(Contexto.CodigoUsuario, Contexto.IdTerminal, Contexto.FechaSistema);

                if (Contexto.EsCanalEmpresarial())
                {
                    tarjetaEmpresarial!.CambiarEstadoRegistrado();
                }
                else
                {
                    tarjeta.IndicadorAfiliadoHomeBanking = EstadoEntidad.NO;
                    tarjeta.IndicadorAfiliadoAppMovil = EstadoEntidad.NO;
                }

                await _repositorioEscritura.GuardarCambiosAsync();
                transaccion.Complete();
            }

            throw new ExcepcionAUsuario("06", "Su clave de internet ha caducado, favor de volver a generarla.");
        }

        if (idTipoOperacionCanalElectronico == TipoOperacionLogin.LOGIN_BIOMETRIA)
        {
            if (!tarjeta.AfiliacionCanalElectronico.EsTarjetaAfiliada)
            {
                using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    tarjeta.AfiliacionCanalElectronico
                        .DesactivarPrimerFactorAutenticacion(Contexto.CodigoUsuario, Contexto.IdTerminal);
                    if (Contexto.EsCanalEmpresarial())
                    {
                        tarjetaEmpresarial!.CambiarEstadoRegistrado();
                    }
                    else
                    {
                        tarjeta.IndicadorAfiliadoHomeBanking = EstadoEntidad.NO;
                        tarjeta.IndicadorAfiliadoAppMovil = EstadoEntidad.NO;
                    }
                    await _repositorioEscritura.GuardarCambiosAsync();
                    transaccion.Complete();
                }

                throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos por SMS.");
            }
        }
        else
        {
            var tipoOperacionCanalElectronico = await _repositorioLectura.ObtenerPorCodigoAsync<TipoOperacionCanalElectronico>
            (idTipoOperacionCanalElectronico);

            if (tipoOperacionCanalElectronico.EsTipoOperacionLogin && !tarjeta.AfiliacionCanalElectronico.EsTarjetaAfiliada)
            {
                using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    tarjeta.AfiliacionCanalElectronico
                        .DesactivarPrimerFactorAutenticacion(Contexto.CodigoUsuario, Contexto.IdTerminal);
                    tarjeta.IndicadorAfiliadoHomeBanking = EstadoEntidad.NO;
                    tarjeta.IndicadorAfiliadoAppMovil = EstadoEntidad.NO;
                    await _repositorioEscritura.GuardarCambiosAsync();
                    transaccion.Complete();
                }

                throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos.");
            }
            else if (tipoOperacionCanalElectronico.EsTipoOperacionAfiliacion)
            {
                if (tarjeta.AfiliacionCanalElectronico.IndicadorAfiliacionPrincipal
                    && !tarjeta.AfiliacionCanalElectronico.EsAfiliacionSms)
                {
                    tarjeta.AfiliacionCanalElectronico
                        .ActualizarConfirmacionAutenticacion(clienteApi, Contexto.IdTerminal);
                }
                else
                {
                    throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos por SMS. Vuelva a ingresar su datos.");
                }
            }
        }

        using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (Contexto.EsCanalEmpresarial())
            {
                tarjetaEmpresarial!.CambiarEstadoAfiliado();
            }
            else
            {
                tarjeta.IndicadorAfiliadoHomeBanking = EstadoEntidad.SI;
                tarjeta.IndicadorAfiliadoAppMovil = EstadoEntidad.SI;
            }

            await _repositorioEscritura.GuardarCambiosAsync();
            transaccion.Complete();
        }

        if (clienteApi.NumeroIntentosFallidos > 0)
        {
            Bitacora.Info("Intento fallido en el login del canal electrónico",
            new Dictionary<string, object>
            {
                        {"idCliente", clienteApi.SistemaCliente.Id},
                        {"numeroTarjeta", tarjeta.NumeroTarjeta}
                });
            var mensajeExcepcion = ConstMensajeError.MensajeErrorAfiliacionInicioSesion +
                " " + clienteApi.DescripcionMotivoFallo;

            throw ExcepcionAUsuario.ExcepcionAfiliacionInicioSesion("06", mensajeExcepcion);
        }

        var clienteNatural = tarjeta.Duenio!.PersonaFisica;

        if (idTipoOperacionCanalElectronico == TipoOperacionLogin.LOGIN_BIOMETRIA)
            return (new UsuarioLogueadoDto
            {
                IdClienteApi = clienteApi.Id,
                CodigoUsuario = clienteApi.IdVisual,
                PrimerNombreUsuario = clienteNatural == null
                   ? tarjeta.Duenio.NombreCliente : clienteNatural.PrimerNombre,
                SegundoNombreUsuario = clienteNatural == null
                   ? tarjeta.Duenio.NombreCliente : clienteNatural.SegundoNombre,
                ApellidoUsuario = clienteNatural == null
                   ? "" : clienteNatural.PrimerApellido,
                NumeroTarjeta = tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture),
                AfiliacionSmsConfirmado = tarjeta.AfiliacionCanalElectronico!.EsTarjetaAfiliada,
                LoginSmsConfirmado = true,
            }, dispositivoCanalElectronico!);

        DispositivoCanalElectronico? nuevoDispositivoCanalElectronico = null;

        if (idTipoOperacionCanalElectronico == TipoOperacionLogin.LOGIN)
        {
            if (dispositivoCanalElectronico == null)
            {
                nuevoDispositivoCanalElectronico = DispositivoCanalElectronico.Generar(
                                                        Guid.NewGuid().ToString(),
                                                        tarjeta,
                                                        clienteApi.SistemaCliente.IndicadorCanal,
                                                        Contexto.FechaSistema);
                using (var transaccion = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _repositorioEscritura.AdicionarAsync(nuevoDispositivoCanalElectronico);
                    await _repositorioEscritura.GuardarCambiosAsync();
                    transaccion.Complete();
                }

            }
        }

        return (new UsuarioLogueadoDto
        {
            IdClienteApi = clienteApi.Id,
            CodigoUsuario = clienteApi.IdVisual,
            PrimerNombreUsuario = clienteNatural == null
                ? tarjeta.Duenio.NombreCliente : clienteNatural.PrimerNombre,
            SegundoNombreUsuario = clienteNatural == null
                ? tarjeta.Duenio.NombreCliente : clienteNatural.SegundoNombre,
            ApellidoUsuario = clienteNatural == null
                ? "" : clienteNatural.PrimerApellido,
            NumeroTarjeta = tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture),
            AfiliacionSmsConfirmado = tarjeta.AfiliacionCanalElectronico.EsTarjetaAfiliada,
            LoginSmsConfirmado = dispositivoCanalElectronico != null,
            IdRegistroDispositivoNuevo = nuevoDispositivoCanalElectronico?.DispositivoId
        }, dispositivoCanalElectronico ?? nuevoDispositivoCanalElectronico);
    }

    /// <summary>
    /// Método que login cliente con clave biométrica
    /// </summary>
    /// <param name="clienteApi">Entidad cliente api</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    /// <param name="tipoDocumento">Entidad tipo de documento</param>
    /// <param name="dispositivosUids">Lista dispositivos</param>
    /// <param name="numeroHorasBloqueo">Número de horas bloqueo</param>
    private async Task LoginClienteBiometria(AutenticarClienteCommand datos,
        ClienteApi clienteApi,
        Tarjeta tarjeta,
        DispositivoCanalElectronico dispositivoCanalElectronico,
        int numeroHorasBloqueo)
    {
        Bitacora.Info("Login por canal electrónico - biometrìa",
            new Dictionary<string, object>
            {
                    {"idCliente", clienteApi.SistemaCliente.Id},
                    {"numeroTarjeta", tarjeta.NumeroTarjeta}
            });

        var tarjetaTexto = tarjeta.NumeroTarjeta.ToString();
        var afiliacionesTokenDigital = await _repositorioLectura.ObtenerPorExpresionConLimiteAsync<AfiliacionTokenDigital>
            (x => x.NumeroTarjeta == tarjetaTexto && (x.EstadoDispositivo == EstadoEntidad.ACTIVO || x.EstadoDispositivo == EstadoEntidad.INACTIVO));
        var afiliacionTokenDigital = afiliacionesTokenDigital.OrderByDescending(x => x.IdAfilacion).FirstOrDefault()
            ?? throw new ExcepcionAUsuario("06", "Debe afiliarse al servicio de token digital");
        var afiliacionBiometrica = afiliacionTokenDigital.AfiliacionesBiometricasActivas();

        if (!afiliacionBiometrica.Any(x => x.EsDispositivoRegistrado(dispositivoCanalElectronico.DispositivoId)))
            throw new ExcepcionAUsuario("06", "Este dispositivo no está afiliado al servicio de biometría");

        AutenticarClienteClaveBiometrica(
            clienteApi,
            tarjeta,
            datos.Password.HashString(),
            afiliacionBiometrica,
            numeroHorasBloqueo);
    }

    /// <summary>
    /// Autenticar por medio de clave de internet
    /// </summary>
    /// <param name="clienteApi">Entidad Cliente APi</param>
    /// <param name="tarjeta">Entidad tarjeta</param>
    /// <param name="tipoDocumento">Entidad tipo documento</param>
    /// <param name="numeroDocumento">Número de documento</param>
    /// <param name="pinBlocks">Pin blocks</param>
    /// <param name="numeroHorasBloqueo">Número de horas de bloqueo</param>
    /// <param name="segundosRangoIntentosFallidos">Segundos rango intentos fallidos</param>
    /// <param name="maximoIntentosFallidos">Máximo de intentos fallidos</param>
    /// <returns></returns>
    private async Task AutenticarClienteClaveInternet(
        ClienteApi clienteApi,
        Tarjeta tarjeta,
        string password,
        int numeroHorasBloqueo,
        int segundosRangoIntentosFallidos,
        int maximoIntentosFallidos)
    {
        try
        {
            string numeroTarjeta = tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture);
            var pinBlocks = await _servicioPinOperaciones.ObtenerPinBlock(password.Trim(), numeroTarjeta);

            if (tarjeta.CodigoEstadoTarjeta == "03")
                throw new ExcepcionAUsuario("06", "Tarjeta anulada.");
            else if (tarjeta.TarjetaVencida(Contexto.FechaSistema))
                throw new ExcepcionAUsuario("06", "Tarjeta vencida.");
            else if (tarjeta.CodigoTipoTarjeta == "3" && Contexto.IndicadorCanal == CanalElectronicoConstante.BANKING_EMPRESARIAL)
                throw new ExcepcionAUsuario("06", "Tipo de tarjeta no permitido.");
            else if (tarjeta.CodigoTipoTarjeta == "4" && Contexto.IndicadorCanal != CanalElectronicoConstante.BANKING_EMPRESARIAL)
                throw new ExcepcionAUsuario("06", "Tipo de tarjeta no permitido.");
            else if (clienteApi.EsClienteBloqueado && Contexto.FechaSistema < clienteApi.FechaFinBloqueo)
                throw new ExcepcionAUsuario("06", "¡Lo sentimos! Tu acceso se encuentra " +
                    "restringido por " + numeroHorasBloqueo + " horas ya que has excedido el número máximo " +
                    "de intentos");

            await _servicioPinOperaciones.ValidarClave(numeroTarjeta, pinBlocks.Item1, tarjeta.NumeroPvvHomebanking1!);
            await _servicioPinOperaciones.ValidarClave(numeroTarjeta, pinBlocks.Item2, tarjeta.NumeroPvvHomebanking2!);

            clienteApi
                .RegistrarIngresoClaveValida(Contexto.FechaSistema)
                .ValidarEstado();
        }
        catch (ExcepcionAUsuario ex)
        {
            if (ex.CodigoError == "55")
            {
                clienteApi.RegistrarIngresoClaveInvalida(Contexto.FechaSistema,
                segundosRangoIntentosFallidos,
                maximoIntentosFallidos, numeroHorasBloqueo,
                Contexto.IndicadorCanal)
                .ActualizarMensaje(maximoIntentosFallidos);
            }
            else
                throw;
        }

    }

    /// <summary>
    /// Autentica por medio de la clave biométrica
    /// </summary>
    /// <param name="clienteApi">Entidad ClienteApi</param>
    /// <param name="tarjeta">Entidad Tarjeta</param>
    /// <param name="tipoDocumento">Entidad Tipo Documento</param>
    /// <param name="numeroDocumento">Número documento</param>
    /// <param name="password">Password</param>
    /// <param name="afiliacionBiometrica">Afiliaciones biométricas</param>
    /// <param name="numeroHorasBloqueo">Npumero de horas de bloqueo</param>
    /// <returns></returns>
    private void AutenticarClienteClaveBiometrica(
        ClienteApi clienteApi,
        Tarjeta tarjeta,
        string password,
        IEnumerable<AfiliacionBiometrica> afiliacionBiometrica,
        int numeroHorasBloqueo)
    {
        string numeroTarjeta = tarjeta.NumeroTarjeta.ToString(CultureInfo.InvariantCulture);

        if (tarjeta.CodigoEstadoTarjeta == "03")
            throw new ExcepcionAUsuario("06", "Tarjeta anulada.");
        else if (tarjeta.TarjetaVencida(Contexto.FechaSistema))
            throw new ExcepcionAUsuario("06", "Tarjeta vencida.");
        else if (tarjeta.CodigoTipoTarjeta == "4")
            throw new ExcepcionAUsuario("06", "Tipo de tarjeta no permitido.");
        else if (tarjeta.IndicadorAfiliadoHomeBanking != "S")
            throw new ExcepcionAUsuario("06", "Cliente no afiliado a los Canales Electrónicos.");
        else if (clienteApi.EsClienteBloqueado
            && Contexto.FechaSistema < clienteApi.FechaFinBloqueo)
        {
            throw new ExcepcionAUsuario("06", "¡Lo sentimos! Tu acceso se encuentra " +
                "restringido por " + numeroHorasBloqueo + " horas ya que has excedido el número máximo " +
                "de intentos");
        }

        if (!afiliacionBiometrica.Any(x => x.EsClaveValida(password)))
            throw new ExcepcionAUsuario("06", "Datos incorrectos, vuelva a intentar.");

        clienteApi
            .RegistrarIngresoClaveValida(Contexto.FechaSistema)
            .ValidarEstado();
    }
}