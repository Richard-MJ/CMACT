using System.Text;
using System.Transactions;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios
{
    /// <summary>
    /// Clase de capa Aplicacion para el control de servicio de las afialiaciones
    /// </summary>
    public class ServicioAplicacionAfiliacion : ServicioBase, IServicioAplicacionAfiliacion
    {
        private readonly IRepositorioGeneral _repositorioGeneral;
        private readonly IRepositorioOperacion _repositorioOperacion;
        private readonly IServicioAplicacionColas _servicioAplicacionColas;
        private readonly IServicioDominioAfiliacion _servicioDominioAfiliacion;
        private readonly IServicioAplicacionProducto _servicioAplicacionProducto;
        private readonly IServicioAplicacionInteroperabilidad _servicioAplicacionInteroperabilidad;
        private readonly IServicioAplicacionNotificaciones _servicioAplicacionNotificaciones;

        public ServicioAplicacionAfiliacion(
            IContextoAplicacion contexto,
            IRepositorioGeneral repositorioGeneral,
            IRepositorioOperacion repositorioOperaciones,
            IServicioDominioAfiliacion servicioDominioAfiliacion,
            IServicioAplicacionProducto servicioAplicacionProducto,
            IServicioAplicacionInteroperabilidad servicioAplicacionInteroperabilidad,
            IServicioAplicacionColas aplicacionServicioColas,
            IServicioAplicacionNotificaciones servicioAplicacionNotificaciones) : base(contexto)
        {
            _repositorioGeneral = repositorioGeneral;
            _repositorioOperacion = repositorioOperaciones;
            _servicioAplicacionColas = aplicacionServicioColas;
            _servicioDominioAfiliacion = servicioDominioAfiliacion;
            _servicioAplicacionProducto = servicioAplicacionProducto;
            _servicioAplicacionInteroperabilidad = servicioAplicacionInteroperabilidad;
            _servicioAplicacionNotificaciones = servicioAplicacionNotificaciones;
        }
        /// <summary>
        /// Metodo que afilia servicios
        /// </summary>
        /// <param name="numeroTarjeta">numero tarjeta</param>
        /// <param name="numeroCuenta">numeor de cuenta</param>
        /// <param name="codigoServicio">codigo del servicio</param>
        /// <returns>DTO de la afiliacion</returns>
        public async Task<(AfiliacionServicioDTO, TarjetaMovimiento)> AfiliacionServicioInterno(
           string numeroTarjeta,
           string numeroCuenta,
           string codigoServicio)
        {
            var tarjetaDeCliente = _servicioAplicacionProducto.ObtenerClienteAPartirDeTarjeta(numeroTarjeta);

            var numeroAfiliacion = await _repositorioGeneral.ObtenerNumeroSerieNoBloqueanteAsync("%", Cliente.EsquemaCliente,
                Afiliado.CodigoNumeroAfiliacion, 1);

            var cuentaEfectivo = await _repositorioOperacion.ObtenerPorCodigoAsync<CuentaEfectivo>(
                Empresa.CodigoPrincipal, numeroCuenta);

            var usuario = await _repositorioOperacion.ObtenerPorCodigoAsync<Usuario>(
                Empresa.CodigoPrincipal, Agencia.Principal, General.UsuarioPorDefectoInteroperabilidad);

            var fechaHoraSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var tarjetaMovimiento = await _servicioAplicacionNotificaciones.GenerarNotificacionTarjeta(
                tarjetaDeCliente,
                ((int)CatalogoTransaccionEnum.CodigoTransaccionAfiliacion).ToString(),
                ((int)SubTipoTransaccionEnum.AfiliacionBilleteraVirtual).ToString(),
                fechaHoraSistema.FechaHoraSistema);

            var afiliacion = _servicioDominioAfiliacion.AfiliacionServicio(
               numeroAfiliacion, cuentaEfectivo, tarjetaDeCliente, usuario, codigoServicio, fechaHoraSistema.FechaHoraSistema, tarjetaMovimiento.IdMovimento);

            await _repositorioOperacion.AdicionarAsync(afiliacion);

            return new(afiliacion.AdatosAfiliacion(
                cuentaEfectivo, tarjetaDeCliente, fechaHoraSistema.FechaHoraSistema, numeroAfiliacion), tarjetaMovimiento);
        }

        /// <summary>
        /// Metodo que desafilia de un servicio
        /// </summary>
        /// <param name="numeroCuenta">numero de cuenta</param>
        /// <param name="parametroEmpresa">parametro de la empresa</param>
        /// <returns>Retorna DTO de la desafiliacion</returns>
        public async Task<(AfiliacionServicioDTO, TarjetaMovimiento)> DesafiliacionAServicioInterno(
            string numeroCuenta,
            ParametroPorEmpresa parametroEmpresa)
        {
            var cuentaEfectivo = await _repositorioOperacion
                .ObtenerPorCodigoAsync<CuentaEfectivo>(Empresa.CodigoPrincipal, numeroCuenta);

            var afiliaciones = cuentaEfectivo.Cliente.Afiliaciones
                .Where(x => x.IndicadorActivo == General.Activo)
                .ToList();

            var usuario = await _repositorioOperacion.ObtenerPorCodigoAsync<Usuario>(
                Empresa.CodigoPrincipal, Agencia.Principal, General.UsuarioPorDefectoInteroperabilidad);

            var fechaHoraSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var cuentasHistoricasAfiliadas = _servicioDominioAfiliacion.DesafiliarServicio(
                afiliaciones,
                parametroEmpresa,
                usuario,
                fechaHoraSistema.FechaHoraSistema);

            var tarjetaDeCliente = await _repositorioOperacion.ObtenerPorCodigoAsync<Tarjeta>
                (cuentasHistoricasAfiliadas.AfiliadoServicio.Afiliado.NumeroTarjeta);

            var tarjetaMovimiento = await _servicioAplicacionNotificaciones.GenerarNotificacionTarjeta(
                tarjetaDeCliente,
                ((int)CatalogoTransaccionEnum.CodigoTransaccionAfiliacion).ToString(),
                ((int)SubTipoTransaccionEnum.DesafiliacionBilleteraVirtual).ToString(),
                fechaHoraSistema.FechaHoraSistema,
                cuentasHistoricasAfiliadas.AfiliadoServicio.Afiliado.NumeroMovimiento);

            _repositorioOperacion.Adicionar(cuentasHistoricasAfiliadas);

            return new(cuentasHistoricasAfiliadas.AdatosAfiliacion(cuentaEfectivo.Cliente, fechaHoraSistema.FechaHoraSistema), tarjetaMovimiento);
        }

        /// <summary>
        /// Metodo que afilia a un cliente al serviciode interoperabilidad
        /// </summary>
        /// <param name="datosAfiliacion">Datos para afiliacion</param>
        /// <returns>True si se afilio correctamente</returns>
        public async Task<RespuestaAfiliacionCCEDTO> AfiliacionDirectorioCCE(
            EntradaAfiliacionDirectorioDTO datosAfiliacion)
        {
            var afiliacionInteroperabilidad = _repositorioOperacion
                .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidad>(x =>
                    x.CodigoCuentaInterbancario == datosAfiliacion.CodigoCuentaInterbancario)
                .FirstOrDefault();

            _servicioDominioAfiliacion.ValidarAfiliacionInteroperabilidad(afiliacionInteroperabilidad?.Detalles);

            var afiliadoFinal = afiliacionInteroperabilidad?.Detalles.FirstOrDefault(
                x => x.NumeroCelular == datosAfiliacion.NumeroCelular);

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var parametroInteroperabilidad = _repositorioOperacion.ObtenerPorCodigo<ParametroPorEmpresa>(Empresa.CodigoPrincipal,
                Sistema.CuentaEfectivo, DatosGeneralesInteroperabilidad.CodigoServicioInteroperabilidad);

            var (afiliacionInterno, tarjetaMovimiento) = await AfiliacionServicioInterno(
                datosAfiliacion.NumeroTarjeta,
                datosAfiliacion.NumeroCuentaAfiliada!,
                parametroInteroperabilidad.ValorParametro);

            datosAfiliacion.NumeroAfiliacion = afiliacionInterno.NumeroAfiliacion;

            var resultado = await _servicioAplicacionInteroperabilidad.GestionarAfiliacionDirectorioCCE(datosAfiliacion, afiliadoFinal);

            try
            {
                _servicioDominioAfiliacion.VerificarRespuestaAfiliacionCCE(resultado);

                var cuerpoGenerarQR = afiliacionInterno.AGenerarQRParaAfiliacion(datosAfiliacion.CodigoCuentaInterbancario);
                var datosQR = await _servicioAplicacionInteroperabilidad.GenerarQR(cuerpoGenerarQR);

                if (afiliacionInteroperabilidad == null)
                {
                    var afiliacionCabeceraNueva = datosAfiliacion.CrearCabeceraAfiliacion(fechaSistema.FechaHoraSistema);
                    _repositorioOperacion.Adicionar(afiliacionCabeceraNueva);
                }

                var afiliacionDetalleNueva = _servicioDominioAfiliacion.CrearDetalleAfiliacion(
                    afiliadoFinal,
                    datosAfiliacion,
                    datosQR,
                    resultado.Datos.NumeroSeguimiento,
                    fechaSistema.FechaHoraSistema,
                    datosAfiliacion.NotificarOperacionesRecibidas ?? true,
                    datosAfiliacion.NotificarOperacionesEnviadas ?? true);

                var tarjetaMovimientoConfiguracion = await _servicioAplicacionNotificaciones.GenerarNotificacionTarjeta(
                    tarjetaMovimiento.Tarjeta,
                    ((int)CatalogoTransaccionEnum.CodigoTransaccionAfiliacion).ToString(),
                    ((int)SubTipoTransaccionEnum.ConfiguracionNotificacionInteroperatibilidad).ToString(),
                    (afiliacionDetalleNueva.FechaModifico ?? afiliacionDetalleNueva.FechaRegistro).GetValueOrDefault(),
                    afiliacionDetalleNueva.NumeroAfiliacion,
                    $"R:{afiliacionDetalleNueva.IndicadoRecibirNotificacion}-E:{afiliacionDetalleNueva.IndicadorEnviarNotificacion}");

                if (afiliadoFinal == null) _repositorioOperacion.Adicionar(afiliacionDetalleNueva);

                await _repositorioOperacion.GuardarCambiosAsync();

                await _servicioAplicacionNotificaciones.EnviarNotificacionTarjeta(tarjetaMovimiento);

                return AfiliacionExtension.ARespuestaAfiliacion(fechaSistema.FechaHoraSistema, datosQR.CadenaQR);
            }
            catch (Exception)
            {
                if(resultado.Datos.Respuesta == DatosGeneralesInteroperabilidad.Aceptado)
                {
                    datosAfiliacion.TipoInstruccion = DatosGeneralesInteroperabilidad.TipoInstruccionEliminacion;
                    await _servicioAplicacionInteroperabilidad.GestionarAfiliacionDirectorioCCE(datosAfiliacion, afiliadoFinal);
                }
                throw new ValidacionException("Ocurrio un error al realizar la afiliación con la CCE");
            }

        }

        /// <summary>
        /// Metodo para desafiliacion de interoperabilidad
        /// </summary>
        /// <param name="datosDesafiliacion"> datos para afiliacion</param>
        /// <returns>True si la afiliacion es correcta</returns>
        public async Task<RespuestaAfiliacionCCEDTO> DesafiliacionDirectorioCCE(EntradaAfiliacionDirectorioDTO datosDesafiliacion)
        {
            var afiliacionDetalle = _repositorioOperacion
                .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(x =>
                    x.NumeroCelular == datosDesafiliacion.NumeroCelular
                    && x.CodigoCuentaInterbancario == datosDesafiliacion.CodigoCuentaInterbancario
                    && x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado)
                .FirstOrDefault();

            if (afiliacionDetalle == null) throw new ValidacionException("Este número no está afiliado a interoperabilidad");

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            var resultado = await _servicioAplicacionInteroperabilidad.GestionarAfiliacionDirectorioCCE(datosDesafiliacion, afiliacionDetalle);

            try
            {
                _servicioDominioAfiliacion.VerificarRespuestaAfiliacionCCE(resultado);

                await DesafiliarServicioInteroperabilidad(afiliacionDetalle, datosDesafiliacion.TipoInstruccion, fechaSistema.FechaHoraSistema,
                    datosDesafiliacion.Canal, datosDesafiliacion.NumeroCuentaAfiliada, datosDesafiliacion.NumeroCelular);

                return AfiliacionExtension.ARespuestaDesafiliacion(fechaSistema.FechaHoraSistema);
            }
            catch (ValidacionException excepcion)
            {
                throw new ValidacionException("Se realizo la desafiliacion en el directorio, pero no internamente:" + excepcion.Message);
            }
        }

        /// <summary>
        /// Método que te desafilia del servicio de Interoperabilidad
        /// </summary>
        /// <param name="afiliacionDetalle"></param>
        /// <param name="tipoInstruccion"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="canal"></param>
        /// <param name="numeroCuenta"></param>
        /// <param name="numeroCelular"></param>
        /// <returns></returns>
        private async Task DesafiliarServicioInteroperabilidad(
            AfiliacionInteroperabilidadDetalle afiliacionDetalle,
            string tipoInstruccion, DateTime fechaSistema,
            string canal, string numeroCuenta,
            string numeroCelular)
        {
            var parametroInteroperabilidad = _repositorioOperacion.ObtenerPorCodigo<ParametroPorEmpresa>(
                Empresa.CodigoPrincipal, Sistema.CuentaEfectivo, DatosGeneralesInteroperabilidad.CodigoServicioInteroperabilidad);

            _servicioDominioAfiliacion.DesafiliarInteroperabilidadCCE(afiliacionDetalle, tipoInstruccion, fechaSistema, canal);

            var (desafiliacionInterno, tarjetaMovimiento) = await DesafiliacionAServicioInterno
                (afiliacionDetalle.afiliacion.NumeroCuenta, parametroInteroperabilidad);

            var tarjetaMovimientoConfiguracion = await _servicioAplicacionNotificaciones.GenerarNotificacionTarjeta(
                 tarjetaMovimiento.Tarjeta,
                 ((int)CatalogoTransaccionEnum.CodigoTransaccionAfiliacion).ToString(),
                 ((int)SubTipoTransaccionEnum.ConfiguracionNotificacionInteroperatibilidad).ToString(),
                 (afiliacionDetalle.FechaModifico ?? afiliacionDetalle.FechaRegistro).GetValueOrDefault(),
                 afiliacionDetalle.NumeroAfiliacion,
                 "Desafiliación interoperatibilidad");

            await _repositorioOperacion.GuardarCambiosAsync();

            await _servicioAplicacionNotificaciones.EnviarNotificacionTarjeta(tarjetaMovimiento);
        }

        /// <summary>
        /// Registra los datos procesados en la bitácora de afiliación a partir del contenido en formato <c>byte[]</c>.
        /// </summary>
        /// <param name="contenido">Arreglo de bytes que contiene los datos en texto plano, separados por saltos de línea.</param>
        /// <returns>Una tarea asincrónica que representa la operación de registro.</returns>
        public async Task RegistrarDatosProcesadosBitacoraAfiliacion(byte[] contenido)
        {
            var texto = Encoding.UTF8.GetString(contenido);
            var lineas = texto.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var datos = lineas
                .Where(EsLineaDeRegistroValida)
                .Select(ConstruirArchivoDirectorioDto)
                .Where(dto => dto != null)
                .ToList();

            var fechaSistema = _repositorioGeneral.ObtenerCalendarioCuentaEfectivo();

            using (var transaction = new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                try
                {
                    foreach (var dato in datos)
                    {
                        var numeroSinPrefijo = dato!.NumeroCelular.Substring(3);

                        var afiliacionDetalle = _repositorioOperacion
                            .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(x =>
                                x.CodigoCuentaInterbancario == dato!.CodigoCuentaInterbancario &&
                                x.NumeroCelular == numeroSinPrefijo)
                            .First();

                        var numeroSeguimiento = dato!.CodigoReferencia
                            .Substring(dato.CodigoReferencia.Length - 6);

                        RegistrarBitacoraAfiliacion(afiliacionDetalle, dato!, fechaSistema.FechaHoraSistema, numeroSeguimiento);

                        if (dato!.TipoInstruccion == DatosValoresFijos.NuevoRegistro)
                        {
                            if (afiliacionDetalle != null)
                            {
                                afiliacionDetalle.Afiliar(fechaSistema.FechaHoraSistema, afiliacionDetalle.NumeroAfiliacion, afiliacionDetalle.Canal);
                                afiliacionDetalle.AgregarNumeroSeguimiento(numeroSeguimiento);
                            }
                        }
                        else
                        {
                            await DesafiliarServicioInteroperabilidad(afiliacionDetalle, dato.TipoInstruccion, fechaSistema.FechaHoraSistema,
                                afiliacionDetalle.Canal, afiliacionDetalle.afiliacion.NumeroCuenta, numeroSinPrefijo);
                        }
                    }

                    transaction.Complete();
                }
                catch (Exception excepcion)
                {
                    throw new Exception(excepcion.Message, excepcion.InnerException);
                }
            }
        }

        /// <summary>
        /// Crea y Guarda la bitacora de afiliación o desafiliación
        /// </summary>
        /// <param name="afiliacionDetalle"></param>
        /// <param name="datoArchivo"></param>
        /// <param name="fechaSistema"></param>
        private void RegistrarBitacoraAfiliacion(
            AfiliacionInteroperabilidadDetalle afiliacionDetalle,
            ArchivoDirectorioDTO datoArchivo,
            DateTime fechaSistema,
            string numeroSeguimiento)
        {
            var estadoActual = afiliacionDetalle.IndicadorEstadoAfiliado == General.Si
                ? DatosGeneralesInteroperabilidad.Afiliado : DatosGeneralesInteroperabilidad.Desafiliado;

            var bitacora = BitacoraInteroperabilidadAfiliacion.Crear(
                 afiliacionDetalle.CodigoCuentaInterbancario,
                 afiliacionDetalle.NumeroCelular,
                 datoArchivo.CodigoReferencia,
                 numeroSeguimiento,
                 datoArchivo.TipoInstruccion,
                 fechaSistema,
                 General.UsuarioPorDefectoInteroperabilidad,
                 afiliacionDetalle.Canal,
                 estadoActual,
                 DatosGeneralesInteroperabilidad.Aceptado);
            _repositorioOperacion.Adicionar(bitacora);
        }

        /// <summary>
        /// Verifica si una línea de texto representa un tipo de operación válida (nuevo, eliminar o modificar).
        /// </summary>
        /// <param name="linea">Línea de texto a evaluar.</param>
        /// <returns>True si la línea comienza con un código de operación válido; de lo contrario, false.</returns>
        private bool EsLineaDeRegistroValida(string linea)
        {
            return linea.StartsWith(DatosValoresFijos.NuevoRegistro) ||
                   linea.StartsWith(DatosValoresFijos.EliminarRegistro) ||
                   linea.StartsWith(DatosValoresFijos.ModificarRegistro);
        }

        /// <summary>
        /// Construye un objeto <see cref="ArchivoDirectorioDTO"/> a partir de una línea de texto separada por comas.
        /// </summary>
        /// <param name="linea">Línea de texto con los datos separados por comas.</param>
        /// <returns>Instancia de <see cref="ArchivoDirectorioDTO"/> si los datos son válidos; de lo contrario, null.</returns>
        private ArchivoDirectorioDTO? ConstruirArchivoDirectorioDto(string linea)
        {
            var partes = linea.Split(',');
            if (partes.Length < 4) return null;

            return new ArchivoDirectorioDTO
            {
                TipoInstruccion = partes[0],
                CodigoReferencia = partes[1],
                CodigoCuentaInterbancario = partes[2],
                NumeroCelular = partes[3]
            };
        }

        #region Enviar Correo
        /// <summary>
        /// Envía un correo electrónico relacionado con la afiliación basado en los datos proporcionados.
        /// </summary>
        /// <param name="afiliacion">Datos internos del servicio de afiliación.</param>
        /// <param name="numeroCuentaAfiliada">Información de cuenta afiliada.</param>
        /// <param name="numeroCelular">Información de la numero de Celular.</param>
        /// <param name="fechaSistema">La fecha y hora actuales del sistema.</param>
        /// <param name="operacion">El tipo de operación que se está realizando (por ejemplo, alta, baja, actualización).</param>
        /// <param name="asunto">El asunto del correo electrónico.</param>
        /// <returns>Una tarea que representa la operación asincrónica de enviar el correo.</returns>

        public async Task EnviarCorreoAfiliacion(
            AfiliacionServicioDTO afiliacion,
            string numeroCuentaAfiliada,
            string numeroCelular,
            DateTime fechaSistema,
            string operacion,
            string asunto)
        {
            var correoRemitente = _repositorioOperacion.ObtenerPorCodigo<ParametroPorEmpresa>
                (Empresa.CodigoPrincipal, Sistema.CuentaEfectivo, General.EmailCanales);

            var cuerpoCorreo = afiliacion.ACorreAfiliacionInteroperabilidad(fechaSistema, numeroCelular,
                numeroCuentaAfiliada, _contextoAplicacion, correoRemitente.ValorParametro, operacion, asunto.ToUpper(), asunto);

            await _servicioAplicacionColas.EnviarCorreoAsync(CorreoGeneralDTO.CodigoAfiliacionInteroperabilidad, cuerpoCorreo);
        }
        #endregion
    }
}
