using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio
{
    public interface IServicioDominioAfiliacion
    {
        /// <summary>
        /// Metodo que afilia a servicios
        /// </summary>
        /// <param name="numeroAfiliacion"> Numero de afiliacion</param>
        /// <param name="cuentaEfectivo">Cuenta Efectivo</param>
        /// <param name="tarjetaDeCliente">Tarjeta de credito del cliente</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="codigoServicio">Codigo del servicio</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <returns>Retorna datos del afiliado</returns>
        Afiliado AfiliacionServicio(
            int numeroAfiliacion,
            CuentaEfectivo cuentaEfectivo,
            Tarjeta tarjetaDeCliente,
            Usuario usuario,
            string codigoServicio,
            DateTime fechaSistema,
            decimal numeroMovimiento = 0);
        /// <summary>
        /// Desafilia del servicio de interoperabilidad
        /// </summary>
        /// <param name="afiliacionExistente">Afiliacion existente</param>
        /// <param name="tipoInstruccion">Tipo de instruccion</param>
        void DesafiliarInteroperabilidadCCE(
            AfiliacionInteroperabilidadDetalle afiliacionExistente,
            string tipoInstruccion,
            DateTime fecha,
            string canal);
        /// <summary>
        /// Metodo que verifica la respuesta de la afiliacion de la CCE
        /// </summary>
        /// <param name="resultado">Resultado de la CCE</param>
        /// <param name="detalleAfiliacion">Detalle de la afiliacion</param>
        void VerificarRespuestaAfiliacionCCE(
            RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO> resultado);
        /// <summary>
        /// Metodo que desafilia un servicio
        /// </summary>
        /// <param name="afiliacionesExistentes">Afiliaciones existentes del cliente</param>
        /// <param name="parametroServicio">Pametro del servicio </param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Datos del desafiliado</returns>
        CuentaAfiliadaHistorica DesafiliarServicio(
            List<Afiliado> afiliacionesExistentes,
            ParametroPorEmpresa parametroCodigoCompraPorInternet,
            Usuario usuario,
            DateTime fechaSistema);
        /// <summary>
        /// Metodo que valida que tenga un numero tenga una afiliacion activa en interoperabilidad
        /// </summary>
        /// <param name="afiliacionDetalle"></param>
        void ValidarAfiliacionInteroperabilidad(List<AfiliacionInteroperabilidadDetalle>? afiliacionDetalle);
        /// <summary>
        /// Crea un nuevo detalle de afiliación para interoperabilidad utilizando los datos proporcionados.
        /// </summary>
        /// <param name="afiliacionDetalleExistente">Detalle existente de afiliación, puede ser nulo si es una nueva afiliación.</param>
        /// <param name="datosEntrada">Datos de entrada para el registro en el directorio.</param>
        /// <param name="datosQR">Datos generados del QR asociado a la afiliación.</param>
        /// <param name="numeroSeguimiento">Número de seguimiento asociado a la operación.</param>
        /// <param name="fecha">Fecha y hora de la operación de creación del detalle de afiliación.</param>
        /// <returns>Una nueva instancia de AfiliacionInteroperabilidadDetalle.</returns>
        AfiliacionInteroperabilidadDetalle CrearDetalleAfiliacion(
            AfiliacionInteroperabilidadDetalle? afiliacionDetalleExistente,
            EntradaAfiliacionDirectorioDTO datosEntrada,
            RespuestaGenerarQR datosQR,
            string? numeroSeguimiento,
            DateTime fecha,
            bool notificarOperacionesRecibidas,
            bool notificarOperacionesEnviadas);
    }
}
