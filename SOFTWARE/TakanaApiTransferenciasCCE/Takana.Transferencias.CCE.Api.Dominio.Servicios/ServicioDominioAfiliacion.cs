using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Dominio;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Clase de dominio que controla la afiliacion
    /// </summary>
    public class ServicioDominioAfiliacion : IServicioDominioAfiliacion
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
        public Afiliado AfiliacionServicio(
           int numeroAfiliacion,
           CuentaEfectivo cuentaEfectivo,
           Tarjeta tarjetaDeCliente,
           Usuario usuario,
           string codigoServicio,
           DateTime fechaSistema, 
           decimal numeroMovimiento = 0)
        {
            Afiliado afiliado = Afiliado.Crear(
                numeroAfiliacion,
                fechaSistema,
                usuario,
                tarjetaDeCliente,
                numeroMovimiento);

            AfiliadoServicio servicioAfiliacionComprasPorInternet = AfiliadoServicio.Crear(
                afiliado,
                Convert.ToInt16(codigoServicio),
                fechaSistema);
            CuentaAfiliada.CrearYAgregarAlServicio(servicioAfiliacionComprasPorInternet, cuentaEfectivo);
            afiliado.AgregarServicioAfiliacion(servicioAfiliacionComprasPorInternet);
            return afiliado;
        }
        /// <summary>
        /// Metodo que desafilia un servicio
        /// </summary>
        /// <param name="afiliacionesExistentes">Afiliaciones existentes del cliente</param>
        /// <param name="parametroServicio">Pametro del servicio </param>
        /// <param name="usuario">Usuario del sistema</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>Datos del desafiliado</returns>
        public CuentaAfiliadaHistorica DesafiliarServicio(
            List<Afiliado> afiliacionesExistentes,
            ParametroPorEmpresa parametroServicio,
            Usuario usuario,
            DateTime fechaSistema)
        {
            var servicioAfiliado = ObtenerServicio(
                afiliacionesExistentes,
                parametroServicio);

            var cuentaHistorica = CuentaAfiliadaHistorica.Crear(
                servicioAfiliado.CuentasAfiliadas.FirstOrDefault(),
                usuario,
                fechaSistema);

            servicioAfiliado.DesafiliarServicioYRemoverCuentasAfiliadas(
                usuario,
                fechaSistema);

            return cuentaHistorica;
        }
        /// <summary>
        /// Obtiene el servicio por parametro
        /// </summary>
        /// <param name="afiliacionesExistentes">Afiliacion existente del cliente</param>
        /// <param name="parametroCodigoServicio">Codigo del servicio</param>
        /// <returns>Retorna datos del servicio</returns>
        public AfiliadoServicio ObtenerServicio(
           List<Afiliado> afiliacionesExistentes,
           ParametroPorEmpresa parametroCodigoServicio)
        {
            var listaServicios = ObtenerServiciosAfiliacionActivosDesdeListaAfiliacionesActivas(
                afiliacionesExistentes,
                Convert.ToInt16(parametroCodigoServicio.ValorParametro));

            var servicio = listaServicios.FirstOrDefault();
            if (servicio == null)
                throw new ValidacionException("El cliente no esta afiliado al servicio");
            return servicio;
        }

        /// <summary>
        /// Obtiene Servicios de afiliacion desde lista de afiliaciones activas
        /// </summary>
        /// <param name="afiliacionesExistentes"> afiliaciones existentes</param>
        /// <param name="codigoServicio">Codigo de servicio</param>
        /// <returns>Lista dde servicio al que esta afiliado el cliente</returns>
        public List<AfiliadoServicio> ObtenerServiciosAfiliacionActivosDesdeListaAfiliacionesActivas(
           List<Afiliado> afiliacionesExistentes,
           short codigoServicio)
        {
            afiliacionesExistentes = afiliacionesExistentes.Where(a => a.IndicadorActivo == General.Activo).ToList();

            List<AfiliadoServicio> listaServicios = new List<AfiliadoServicio>();
            afiliacionesExistentes.ForEach(a => listaServicios.AddRange(a.ServiciosAfiliado));
            listaServicios = listaServicios.Where(s => s.IndicadorEstado == General.Activo
                && s.CodigoServicio == codigoServicio).ToList();
            return listaServicios;
        }

        /// <summary>
        /// Desafilia del servicio de interoperabilidad
        /// </summary>
        /// <param name="afiliacionExistente"></param>
        /// <param name="tipoInstruccion"></param>
        /// <param name="fecha"></param>
        /// <param name="canal"></param>
        /// <exception cref="ValidacionException"></exception>
        public void DesafiliarInteroperabilidadCCE(
            AfiliacionInteroperabilidadDetalle afiliacionExistente,
            string tipoInstruccion,
            DateTime fecha,
            string canal)
        {
            if (tipoInstruccion == DatosValoresFijos.EliminarRegistro)
            {
                if (afiliacionExistente == null)
                    throw new ValidacionException("No se ha encontrado una afiliacion existente para desafiliar");
                afiliacionExistente.Desafiliar(fecha, canal);
                afiliacionExistente.ActualizarNotificacion(false, false);
            }
        }

        /// <summary>
        /// Metodo que verifica la respuesta de la afiliacion de la CCE
        /// </summary>
        /// <param name="resultado">Resultado de la CCE</param>
        public void VerificarRespuestaAfiliacionCCE(
            RespuestaSalidaDTO<RespuestaRegistroDirectorioDTO> resultado)
        {
            if (resultado.Datos.Respuesta == DatosGeneralesInteroperabilidad.Rechazado)
                throw new ValidacionException(resultado.RazonExtra ?? resultado.Razon);
        }
        /// <summary>
        /// Metodo que valida que tenga un numero tenga una afiliacion activa en interoperabilidad
        /// </summary>
        /// <param name="afiliacionDetalle">datos del detalle de afiliacion</param>
        public void ValidarAfiliacionInteroperabilidad(List<AfiliacionInteroperabilidadDetalle>? afiliacionDetalle)
        {
            var afiliadoFinal = afiliacionDetalle?.FirstOrDefault(x => 
                x.IndicadorEstadoAfiliado == AfiliacionInteroperabilidadDetalle.Afiliado);
            
            if (afiliadoFinal != null)
                throw new ValidacionException("Este número tiene una afiliación a interoperabilidad activa");
        }

        /// <summary>
        /// Crea un nuevo detalle de afiliación para interoperabilidad utilizando los datos proporcionados.
        /// </summary>
        /// <param name="afiliacionDetalleExistente">Detalle existente de afiliación, puede ser nulo si es una nueva afiliación.</param>
        /// <param name="datosAfiliar">Datos de entrada para el registro en el directorio.</param>
        /// <param name="datosQR">Datos generados del QR asociado a la afiliación.</param>
        /// <param name="numeroSeguimiento">Número de seguimiento asociado a la operación.</param>
        /// <param name="fecha">Fecha y hora de la operación de creación del detalle de afiliación.</param>
        /// <returns>Una nueva instancia de AfiliacionInteroperabilidadDetalle.</returns>
        public AfiliacionInteroperabilidadDetalle CrearDetalleAfiliacion(
            AfiliacionInteroperabilidadDetalle? afiliacionDetalleExistente,
            EntradaAfiliacionDirectorioDTO datosAfiliar,
            RespuestaGenerarQR datosQR,
            string? numeroSeguimiento,
            DateTime fecha,
            bool notificarOperacionesRecibidas,
            bool notificarOperacionesEnviadas)
        {
            if (afiliacionDetalleExistente != null)
            {
                afiliacionDetalleExistente.Afiliar(fecha, datosAfiliar.NumeroAfiliacion, datosAfiliar.Canal);
                afiliacionDetalleExistente.AgregarNumeroSeguimiento(numeroSeguimiento);
                afiliacionDetalleExistente.AgregarQR(datosQR.IdentificadorQR, datosQR.CadenaQR);
                afiliacionDetalleExistente.ActualizarNotificacion(notificarOperacionesRecibidas, notificarOperacionesEnviadas);
                return afiliacionDetalleExistente;
            }
            else
            {
                var AfiliacionDetalleNueva = AfiliacionExtension.CrearAfiliacionDetalle(datosAfiliar,fecha);
                AfiliacionDetalleNueva.Afiliar(fecha, datosAfiliar.NumeroAfiliacion,datosAfiliar.Canal);
                AfiliacionDetalleNueva.AgregarQR(datosQR.IdentificadorQR, datosQR.CadenaQR);
                AfiliacionDetalleNueva.AgregarNumeroSeguimiento(numeroSeguimiento);
                AfiliacionDetalleNueva.ActualizarNotificacion(notificarOperacionesRecibidas, notificarOperacionesEnviadas);
                return AfiliacionDetalleNueva;
            }
        }
    }
}
