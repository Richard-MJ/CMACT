using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz;

public interface IServicioAplicacionInteroperabilidad
{
    /// <summary>
    /// Metodo que realiza el barrido de contactos para interoperabilidad
    /// </summary>
    /// <param name="datosEntrada">Datos de entrada de interoperabilidad</param>
    /// <returns>Datos de respuesta del barrido de contactos</returns>
    Task<ResultadoPrincipalBarridoDTO> BarridoContacto(
        EntradaBarridoDTO datosEntrada);
    /// <summary>
    /// Metodo que gestionar afilia un cliente en el directorio CCE (afilia, desafilia, modifica)
    /// </summary>
    /// <param name="datosEntrada">Datos para el registro</param>
    /// <param name="afiliacionDetalle">Datos para el registro</param>
    /// <returns>Datos de respuesta de la afiliacion</returns>
    Task<RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO>> GestionarAfiliacionDirectorioCCE(
        EntradaAfiliacionDirectorioDTO datosEntrada, AfiliacionInteroperabilidadDetalle? afiliacionDetalle);

    /// <summary>
    /// Obtiene datos del cliente originante
    /// </summary>
    /// <param name="numeroCuenta">Numero de cuenta</param>
    /// <returns>Datos del cliente originante.</returns>
    Task<RespuetaConsultaCuentaDTO> ObtenerDatosClienteOriginante(string numeroCuenta);

    /// <summary>
    /// Obtiene datos del cliente receptor consultando a la CCE mediante el numero de celular
    /// </summary>
    /// <param name="datosConsulta">Datos para realizar la consulta</param>
    /// <returns>Datos del cliente recepetor</returns>
    Task<ResultadoConsultaCuentaInteroperabilidadDTO> ConsultaCuentaReceptorPorCelular(ConsultaCuentaCelularDTO numeroCuenta);

    /// <summary>
    /// Calcula los totales de la comision para la operacion
    /// </summary>
    /// <param name="datosCalculo">Datos para realizar el calculo</param>
    /// <returns>Retorna los datos del calculo de montos</returns>
    Task<ResultadoCalculoMonto> CalcularMontosTotales(CalculoComisionDTO datosCalculo);

    /// <summary>
    /// Realiza la transferencia completa para interoperabilidad
    /// </summary>
    /// <param name="orden">Datos de la transferencia</param>
    /// <returns>Resultado de la transferencia</returns>
    Task<ResultadoTransferenciaCanalElectronico> RealizarTransferenciaInteroperabilidad(OrdenTransferenciaCanalElectronicoDTO datosTransferencia);

    /// <summary>
    /// Genera la autorizacion token para el uso de los servicios QR
    /// </summary>
    /// <returns>Token de autorizacion</returns>
    Task<string> GenerarAutorizacionToken();

    /// <summary>
    /// Genera el token QR para Directorio CCE
    /// </summary>
    /// <param name="generar">Datos de generacion QR</param>
    /// <returns>QR generado</returns>
    Task<RespuestaGenerarQR> GenerarQR(GenerarQRDTO generar);

    /// <summary>
    /// Lee el QR con el hash
    /// </summary>
    /// <param name="datosLectura">Datos de lectura QR</param>
    /// <returns>Datos del cliente que genero el QR</returns>
    Task<RespuestaConsultaDatosQRDTO> LeerQR(LeerQRDTO datosLectura);

    /// <summary>
    /// Obtiene los datos de un QR con el identificador
    /// </summary>
    /// <param name="datos">Datos para obtener</param>
    /// <returns>Respuesta de la consulta de datos</returns>
    Task<RespuestaConsultaDatosQRDTO> ObtenerDatosQR(ObtenerDatosQRDTO datos);

    /// <summary>
    /// Obtiene los datos de la cuenta receptora mediante QR
    /// </summary>
    /// <param name="datosConsulta">datos necesarios para la consulta</param>
    /// <returns>Datos del cliente receptor</returns>
    Task<ResultadoConsultaCuentaInteroperabilidadDTO> ObtenerDatosCuentaReceptoraQR(ConsultarCuentaQRDTO datosConsulta);

    /// <summary>
    /// Consulta la cuenta del cliente receptor en una solo paso
    /// </summary>
    /// <param name="datosLectura">Datos necesarios para la consulta de cuenta</param>
    /// <returns>Datos completos del cliente recepor</returns>
    Task<RespuestaConsultaCompletaQR> ConsultarCuentaReceptorPorQR(
        ConsultaCuentaCompletaQRDTO datosLectura);

}
