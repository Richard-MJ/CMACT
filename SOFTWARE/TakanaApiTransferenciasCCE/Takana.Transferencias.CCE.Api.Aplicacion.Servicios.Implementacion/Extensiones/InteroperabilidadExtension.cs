using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaz;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones
{
    public static class InteroperabilidadExtension
    {
        /// <summary>
        /// Convierte datos de entrada de tipo ConsultaCuentaCelularDTO y CuentaEfectivoDTO
        /// en un objeto ConsultaCuentaOperacionDTO para operaciones de consulta de cuentas.
        /// </summary>
        /// <param name="datosEntrada">Datos de entrada desde ConsultaCuentaCelularDTO.</param>
        /// <returns>Objeto ConsultaCuentaOperacionDTO resultante de la conversión.</returns>
        public static ConsultaCuentaOperacionDTO AConsultaCuenta(
            this ConsultaCuentaCelularDTO datosEntrada,
            string idTerminal)
        {
            return new ConsultaCuentaOperacionDTO
            {
                NumeroCelularReceptor = datosEntrada.NumeroCelular,
                CuentaEfectivoDTO = datosEntrada.CuentaEfectivo!,
                TipoTransferencia = TipoTransferencia.CodigoTransferenciaOrdinaria,
                NumeroCuentaOTarjeta = datosEntrada.CodigoEntidad.Substring(1,3),
                Canal = DatosGeneralesInteroperabilidad.CanalInteroperabilidad,
                Usuario = new SesionUsuarioDTO { NombreEquipo = idTerminal }
            };
        }

        /// <summary>
        /// Obtiene la respuesta de la consulta para la cuenta de efectivo especificada.
        /// </summary>
        /// <param name="cuentaEfectivoDTO">Objeto que contiene los datos de la cuenta de efectivo.</param>
        public static ResultadoConsultaCuentaInteroperabilidadDTO AResultadoConsulta(
            this ResultadoConsultaCuentaCCE datosConsultaCuenta)
        {
            return new ResultadoConsultaCuentaInteroperabilidadDTO
            {
                ResultadoConsultaCuenta = datosConsultaCuenta
            };
        }

        /// <summary>
        /// Realiza el cálculo del monto resultante utilizando los datos de comisión y control de monto especificados.
        /// </summary>
        /// <param name="controlMonto">Control de monto utilizado para el cálculo.</param>
        /// <returns>Resultado del cálculo del monto.</returns>
        public static ResultadoCalculoMonto AResultadoCalculoMonto(
            this ControlMontoDTO controlMonto)
        {
            return new ResultadoCalculoMonto
            {
                ControlMonto = controlMonto
            };
        }

        /// <summary>
        /// Crea un objeto de sesión de usuario para interoperabilidad entre sistemas.
        /// </summary>
        /// <returns>Sesión de usuario para interoperabilidad.</returns>
        public static SesionUsuarioDTO ASesionUsuarioCanalElectronico(
            this IContextoAplicacion contextoAplicacion)
        {
            return  new SesionUsuarioDTO()
            {
                CodigoAgencia = contextoAplicacion.CodigoAgencia,
                CodigoUsuario = contextoAplicacion.CodigoUsuario,
                NombreEquipo = contextoAplicacion.IdTerminalOrigen,
                NombreImpresora = string.Empty
            };
        }

        /// <summary>
        /// Convierte los datos de la transferencia inmediata en un objeto ResultadoTransferenciaCelular.
        /// </summary>
        /// <param name="transferencia">Datos de la transferencia inmediata.</param>
        /// <param name="fecha">Fecha en la que se realiza la operación de transferencia.</param>
        /// <returns>Resultado de la operación de transferencia.</returns>
        public static ResultadoTransferenciaCanalElectronico AResultadoOperacionTransferencia(
            this ResultadoTransferenciaInmediataDTO transferencia,
            DateTime fecha)
        {
            return new ResultadoTransferenciaCanalElectronico()
            {
                NumeroOperacion = transferencia.NumeroOperacion,
                FechaOperacion = fecha
            };
        }
        /// <summary>
        /// Convierte los datos de la transferencia inmediata en un objeto RespuetaConsultaCuentaDTO.
        /// </summary>
        /// <param name="cuentaEfectivoDTO">cuentaEfectivo</param>
        /// <returns>Datos necesarios de la cuenta efectivo</returns>
        public static RespuetaConsultaCuentaDTO AResultadoConsultaOriginante(
            this CuentaEfectivoDTO cuentaEfectivoDTO)
        {
            return new RespuetaConsultaCuentaDTO
            {
                CuentaEfectivo = cuentaEfectivoDTO,
            };
        }

        /// <summary>
        /// Convierte los datos de realizar transferencia en un objeto ValidarOperacionDTO para validación.
        /// </summary>
        /// <param name="datos">Datos de realizar transferencia a validar.</param>
        /// <returns>Objeto ValidarOperacionDTO que contiene el resultado de la validación de la operación.</returns>
        public static ValidarOperacionDTO AValidarOperacion(
            this OrdenTransferenciaCanalElectronicoDTO datos)
        {
            return new ValidarOperacionDTO()
            {
                MontoOperacion = datos.ControlMonto.Monto,
                CodigoCCIOriginante = datos.ResultadoConsultaCuenta.CodigoCuentaInterbancariaDeudor
            };
        }

        /// <summary>
        /// Crea un objeto EstructuraObtenerToken para generar un token de interoperabilidad.
        /// </summary>
        /// <returns>Objeto EstructuraObtenerToken que representa el token generado.</returns>
        public static EstructuraObtenerToken AGenerarToken()
        {
            return new EstructuraObtenerToken
            {
                code = ParametroGeneralTransferencia.CodigoEntidadOriginante
            };
        }

        /// <summary>
        /// Convierte los datos de GenerarQRDTO en un objeto EstructuraGenerarQR para generar un código QR.
        /// </summary>
        /// <param name="generarQR">Datos necesarios para generar el código QR.</param>
        /// <returns>Objeto EstructuraGenerarQR que representa el código QR generado.</returns>
        public static EstructuraGenerarQR AGenerarQR(GenerarQRDTO generarQR)
        {
            return new EstructuraGenerarQR
            {
                header = new headerEnvio { user = ParametroGeneralTransferencia.CodigoEntidadOriginante },
                data = new data
                {
                    qrTipo = generarQR.TipoQr,
                    idCuenta = generarQR.CodigoCuentaInterbancario,
                    nombreComerciante = generarQR.NombreCliente,
                    moneda = generarQR.CodigoMoneda
                },
                type = generarQR.TipoGeneracionQR
            };
        }

        /// <summary>
        /// Convierte la estructura de respuesta de generación de QR en un objeto RespuestaGenerarQR 
        /// para manejar la respuesta de la generación del código QR.
        /// </summary>
        /// <param name="respuestaQR">Estructura de respuesta de generación de QR.</param>
        /// <returns>Respuesta de la generación del código QR.</returns>

        public static RespuestaGenerarQR ARespuestaGeneracionQR(
            this EstructuraGenerarQRRespuesta respuestaQR)
        {
            return new RespuestaGenerarQR
            {
                IdentificadorQR = respuestaQR.idQr,
                CadenaQR = respuestaQR.hash,
            };
        }

        /// <summary>
        /// Convierte los datos de LeerQRDTO en un objeto EstructuraLeerQR para leer los datos de un código QR.
        /// </summary>
        /// <param name="respuestaQR">Datos de respuesta de generación de QR.</param>
        /// <returns>Objeto EstructuraLeerQR que representa los datos leídos del código QR.</returns>
        public static EstructuraLeerQR ALeerQR(
            this LeerQRDTO respuestaQR)
        {
            return new EstructuraLeerQR
            {
                header = new headerEnvio { user=ParametroGeneralTransferencia.CodigoEntidadOriginante},
                hash = respuestaQR.CadenaHash
            };
        }

        /// <summary>
        /// Convierte la estructura de respuesta de consulta de datos de QR en un objeto RespuestaConsultaDatosQRDTO 
        /// para manejar la respuesta de la consulta de datos del código QR.
        /// </summary>
        /// <param name="respuestaQR">Estructura de respuesta de consulta de datos de QR.</param>
        /// <returns>Respuesta de la consulta de datos del código QR.</returns>
        public static RespuestaConsultaDatosQRDTO ARespuestaConsultaDatosQR(
            this EstructuraConsultaDatosQR respuestaQR)
        {
            return new RespuestaConsultaDatosQRDTO
            {
                CodigoEntidadReceptor = respuestaQR.qr.emisor,
                IdentificadorCuenta = respuestaQR.qr.idCuenta,
                IdentificadorQR =  respuestaQR.qr.idQr,
                CodigoMoneda = respuestaQR.qr.moneda,
                FechaRegistro = respuestaQR.qr.fechaRegistro,
                FechaVencimiento = respuestaQR.qr.fechaVencimiento,
                TipoQR = respuestaQR.qr.qrTipo
            };          
        }

        /// <summary>
        /// Convierte los datos de ObtenerDatosQRDTO en un objeto EstructuraObtenerDatosQR para obtener los datos de un código QR.
        /// </summary>
        /// <param name="respuestaQR">Datos de respuesta de consulta de datos de QR.</param>
        /// <returns>Objeto EstructuraObtenerDatosQR que representa los datos obtenidos del código QR.</returns>
        public static EstructuraObtenerDatosQR AObtenerDatosQR(
            this ObtenerDatosQRDTO respuestaQR)
        {
            return new EstructuraObtenerDatosQR
            {
                header =  new headerEnvio{ user = ParametroGeneralTransferencia.CodigoEntidadOriginante },
                idCuenta = respuestaQR.IdentificadorCuenta,
                idQr = respuestaQR.IdentificadorQR
            };
        }

        /// <summary>
        /// Convierte los datos en un objeto ConsultaCuentaOperacionDTO para obtener los datos del receptor de un código QR.
        /// </summary>
        /// <param name="datosEntrada">Datos de consulta de la cuenta QR.</param>
        /// <param name="cuentaEfectivo">Datos de la cuenta de efectivo relacionada.</param>
        /// <param name="canal">Canal de comunicación utilizado.</param>
        /// <param name="tipoProxy">Tipo de proxy utilizado.</param>
        /// <param name="valorProxy">Valor del proxy utilizado.</param>
        /// <returns>Datos del receptor obtenidos del código QR.</returns>
        public static ConsultaCuentaOperacionDTO AObtenerDatosReceptorQR(
            this ConsultarCuentaQRDTO datosEntrada,
            CuentaEfectivoDTO cuentaEfectivo,
            string canal,
            string tipoProxy,
            string valorProxy,
            string idTerminal)
        {
            return new ConsultaCuentaOperacionDTO
            {
                TipoProxy = tipoProxy,
                ValorProxy = valorProxy,
                CuentaEfectivoDTO = cuentaEfectivo,
                TipoTransferencia = TipoTransferencia.CodigoTransferenciaOrdinaria,
                NumeroCuentaOTarjeta = datosEntrada.IdentificadorCuenta,
                Canal = canal,
                Usuario = new SesionUsuarioDTO { NombreEquipo = idTerminal }
            };
        }

        /// <summary>
        /// Convierte los datos de ConsultaCuentaCompletaQRDTO y RespuestaConsultaDatosQRDTO en un objeto 
        /// ConsultarCuentaQRDTO para realizar una consulta de cuenta QR.
        /// </summary>
        /// <param name="datosLectura">Datos de lectura de la cuenta completa QR.</param>
        /// <param name="lecturaQR">Respuesta de la consulta de datos del código QR.</param>
        /// <returns>Datos convertidos para la consulta de cuenta QR.</returns>
        public static ConsultarCuentaQRDTO AConsultaCuentaQR(
            this ConsultaCuentaCompletaQRDTO datosLectura,
            RespuestaConsultaDatosQRDTO lecturaQR)
        {
            return new ConsultarCuentaQRDTO
            {
                NumeroCuentaOriginante = datosLectura.NumeroCuentaOriginante,
                IdentificadorQR = lecturaQR.IdentificadorQR,
                CodigoEntidadReceptora = lecturaQR.CodigoEntidadReceptor,
                IdentificadorCuenta = lecturaQR.IdentificadorCuenta,
                CodigoMoneda = lecturaQR.CodigoMoneda
            };
        }

        /// <summary>
        /// Convierte los datos en un objeto RespuestaConsultaCompletaQR para una consulta completa de cuenta QR.
        /// </summary>
        /// <param name="datosLectura">Datos de lectura de la consulta de cuenta interoperable.</param>
        /// <param name="nombreEntidadReceptora">Nombre de la entidad receptora del QR.</param>
        /// <param name="lecturaQR">Respuesta de la consulta de datos del código QR.</param>
        /// <param name="limiteMontoMaximo">Límite máximo de monto para la consulta.</param>
        /// <param name="limiteMontoMinimo">Límite mínimo de monto para la consulta.</param>
        /// <param name="montoMaximoDia">Monto máximo permitido por día para la consulta.</param>
        /// <returns>Consulta completa de cuenta QR.</returns>
        public static RespuestaConsultaCompletaQR AConsultaCuentaCompletaQR(
            this ResultadoConsultaCuentaInteroperabilidadDTO datosLectura,
            string nombreEntidadReceptora,
            RespuestaConsultaDatosQRDTO lecturaQR,
            decimal limiteMontoMaximo,
            decimal limiteMontoMinimo,
            decimal montoMaximoDia)
        {
            return new RespuestaConsultaCompletaQR
            {
                DatosConsulta = datosLectura,
                NombreEntidadReceptora = nombreEntidadReceptora,
                IdentificadorQR = lecturaQR.IdentificadorQR,
                LimiteMontoMaximo = limiteMontoMaximo,
                LimiteMontoMinimo = limiteMontoMinimo,
                MontoMaximoDia = montoMaximoDia
            };
        }

        /// <summary>
        /// Convierte los datos en un objeto GenerarQRDTO para generar un código QR de afiliación.
        /// </summary>
        /// <param name="afiliacionServicio">Datos de afiliación de servicio.</param>
        /// <param name="codigoCuentaInterbancario">Código de cuenta interbancario.</param>
        /// <returns>Datos convertidos para generar un código QR de afiliación.</returns>
        public static GenerarQRDTO AGenerarQRParaAfiliacion(
            this AfiliacionServicioDTO afiliacionServicio,
            string codigoCuentaInterbancario)
        {
            return new GenerarQRDTO
            {
                TipoQr = DatosValoresFijos.QrEstatico,
                CodigoCuentaInterbancario = codigoCuentaInterbancario,
                NombreCliente = afiliacionServicio.Nombre + afiliacionServicio.ApellidoPaterno?.Substring(0, 1),
                TipoGeneracionQR = DatosValoresFijos.tipoQrText,
                CodigoMoneda = afiliacionServicio.CodigoMonedaCuenta == DatosGenerales.CodigoMonedaSoles
                    ? DatosGenerales.CodigoMonedaSolesCCE : DatosGenerales.CodigoMonedaDolaresCCE,
            };
        }

        /// <summary>
        /// Convierte los datos en un objeto BitacoraInteroperabilidadAfiliacion para registrar una bitácora de afiliación.
        /// </summary>
        /// <param name="datosEntrada">Datos de entrada para el registro en directorio.</param>
        /// <param name="cuerpo">Estructura de registro en directorio.</param>
        /// <param name="fecha">Fecha de registro en la bitácora.</param>
        /// <param name="numeroSeguimiento">Número de seguimiento para la operación.</param>
        /// <returns>Datos convertidos para la bitácora de afiliación.</returns>
        public static BitacoraInteroperabilidadAfiliacion CrearBitacoraAfiliacion(
            this EntradaAfiliacionDirectorioDTO datosEntrada,
            EstructuraRegistroDirectorio cuerpo,
            DateTime fecha,
            string numeroSeguimiento,
            string estadoActual)
        {
            return BitacoraInteroperabilidadAfiliacion.Crear(
                 datosEntrada.CodigoCuentaInterbancario,
                 datosEntrada.NumeroCelular,
                 cuerpo.BusMsg.AppHdr.BizMsgIdr,
                 numeroSeguimiento,
                 datosEntrada.TipoInstruccion,
                 fecha,
                 General.UsuarioPorDefectoInteroperabilidad,
                 datosEntrada.Canal,
                 estadoActual);
        }

        /// <summary>
        /// Convierte los datos en un objeto BitacoraInteroperabilidadBarrido para registrar una bitácora de barrido.
        /// </summary>
        /// <param name="datosBarrido">Datos de barrido de contactos.</param>
        /// <param name="cuerpo">Estructura de barrido de contactos.</param>
        /// <param name="fecha">Fecha de registro en la bitácora.</param>
        /// <param name="numeroSeguimiento">Número de seguimiento para la operación.</param>
        /// <returns>Datos convertidos para la bitácora de barrido.</returns>
        public static BitacoraInteroperabilidadBarrido CrearBitacoraBarrido(
            this EntradaBarridoDTO datosBarrido,
            EstructuraBarridoContacto cuerpo,
            DateTime fecha,
            string numeroSeguimiento)
        {
           return BitacoraInteroperabilidadBarrido.Crear(
                datosBarrido.CodigoCCI,
                ParametroGeneralTransferencia.CodigoEntidadOriginante,
                datosBarrido.NumeroCelularOrigen,
                datosBarrido.ContactosBarrido.FirstOrDefault().NumeroCelular,
                numeroSeguimiento,
                cuerpo.BusMsg.Document.PrxyLookUp.LookUp.PrxyOnly.Id,
                fecha,
                General.UsuarioPorDefectoInteroperabilidad);
        }

        /// <summary>
        /// Convierte una lista filtrada aplicando los límites de monto especificados.
        /// </summary>
        /// <param name="resultadoFiltrado">Lista filtrada de resultados de barrido.</param>
        /// <param name="limiteMontoMaximo">Límite máximo de monto para el barrido.</param>
        /// <param name="limiteMontoMinimo">Límite mínimo de monto para el barrido.</param>
        /// <param name="montoMaximoDia">Monto máximo permitido por día para el barrido.</param>
        /// <returns>Datos convertidos del barrido principal.</returns>
        public static ResultadoPrincipalBarridoDTO ARespuestaBarrido(
            this List<ResultadoBarridoDTO> resultadoFiltrado,
            decimal limiteMontoMaximo,
            decimal limiteMontoMinimo,
            decimal montoMaximoDia)
        {
            return new ResultadoPrincipalBarridoDTO
            {
                LimiteMontoMaximo = limiteMontoMaximo + 0.0m,
                LimiteMontoMinimo = limiteMontoMinimo + 0.0m,
                MontoMaximoDia = montoMaximoDia + 0.0m,
                ResultadosBarrido = resultadoFiltrado
            };
        }

        /// <summary>
        /// Método que formatea de los archivos adjuntos
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static List<EstructuraArchivoDirectorioDTO> AdtoDirectorioArchivoMasivo(
            this List<BitacoraInteroperabilidadAfiliacion?> datos, DateTime fechaActual, int numeroSeguimiento)
        {
            return datos.ConvertAll(dato => new EstructuraArchivoDirectorioDTO()
            {
                CodigoAfiliacion = dato.CodigoTipoInstruccion,
                IdTrama = DatosValoresFijos.ValorIdCabeceraRegistro 
                    + fechaActual.ToString("yyyyMMddHHmmss")
                    + EntidadFinancieraInmediata.CodigoCajaTacna 
                    + DatosValoresFijos.RequerimientoRegistro 
                    + numeroSeguimiento++,
                CodigoCuentaInterbancaria = dato.CodigoCCI,
                NumeroCelular = DatosGeneralesInteroperabilidad.PrefijoPeruano 
                    + dato.NumeroCelular,
            });
        }

        /// <summary>
        /// Método de consulta de cuenta
        /// </summary>
        /// <param name="datos"></param>
        /// <returns></returns>
        public static ConsultaCuentaOperacionDTO AConsultaCuenta(
            this CuentaEfectivoDTO cuentaEfectivoDTO,
            string numeroCuentaOTarjeta, 
            string tipoTransferencia,
            string canal,
            string idTerminal)
        {
            return new ConsultaCuentaOperacionDTO
            {
                CuentaEfectivoDTO = cuentaEfectivoDTO,
                TipoTransferencia = tipoTransferencia,
                NumeroCuentaOTarjeta = numeroCuentaOTarjeta,
                Canal = canal,
                Usuario = new SesionUsuarioDTO { NombreEquipo = idTerminal }
            };
        }
    }
}
