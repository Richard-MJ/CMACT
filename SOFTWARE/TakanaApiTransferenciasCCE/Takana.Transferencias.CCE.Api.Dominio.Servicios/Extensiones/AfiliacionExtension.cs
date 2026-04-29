using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad.DatosRegistroDirectorio;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{
    /// <summary>
    /// Clase que mapea los datos de las afiliaciones
    /// </summary>
    public static class AfiliacionExtension
    {
        /// <summary>
        /// Mapea los datos a DTO de afiliacion Servicio
        /// </summary>
        /// <param name="afiliado"></param>
        /// <param name="cuentaEfectivo"></param>
        /// <param name="tarjeta"></param>
        /// <param name="fecha"></param>
        /// <param name="numeroAfiliacion"></param>
        /// <returns>Retorna DTO de afiliacion</returns>
        public static AfiliacionServicioDTO AdatosAfiliacion(
          this Afiliado afiliado,
          CuentaEfectivo cuentaEfectivo,
          Tarjeta tarjeta,
          DateTime fecha,
          int numeroAfiliacion)
        {
            return new AfiliacionServicioDTO()
            {
                FechaOperacion = fecha,
                CorreoElectronico = tarjeta.Duenio.DireccionCorreoElectronico,
                NombreCliente = tarjeta.Duenio.NombreCliente,
                Nombre = tarjeta.Duenio.Nombres,
                ApellidoPaterno = tarjeta.Duenio.ApellidoPaterno,
                ApellidoMaterno = tarjeta.Duenio.ApellidoMaterno,
                NombreProductoCuentaAfiliada = cuentaEfectivo.Producto.NombreProducto,
                CodigoMonedaCuenta = cuentaEfectivo.CodigoMoneda,
                NumeroAfiliacion = numeroAfiliacion
            };
        }

        /// <summary>
        /// Mapea los datos a DTO de afiliacion Servicio de cuenta Historica
        /// </summary>
        /// <param name="cuentasHistoricasAfiliadas">Cuenstas afiliadas historicas</param>
        /// <param name="tarjeta">Tarjeta del cliente</param>
        /// <param name="fecha">Fecha del sistema</param>
        /// <returns>Retorna DTO de al afiliacion</returns>
        public static AfiliacionServicioDTO AdatosAfiliacion(
            this CuentaAfiliadaHistorica cuentasHistoricasAfiliadas,
            Cliente cliente,
            DateTime fecha)
        {
            return new AfiliacionServicioDTO()
            {
                FechaOperacion = fecha,
                CorreoElectronico = cliente.DireccionCorreoElectronico,
                NombreCliente = cliente.NombreCliente,
                NumeroCuenta = cuentasHistoricasAfiliadas.NumeroCuenta,
                NombreProductoCuentaAfiliada = cuentasHistoricasAfiliadas.Cuenta.Producto.NombreProducto,
                CodigoMonedaCuenta = cuentasHistoricasAfiliadas.Cuenta.CodigoMoneda
            };
        }
        /// <summary>
        /// Mapea loa datos a Afiliacoin Interoperabilidad
        /// </summary>
        /// <param name="datos">datis de ¿l reg¿istro</param>
        /// <param name="fechaRegistro">Fecha del sistema</param>
        /// <returns>Retorna DTO de la afiliacion de interoperabildad</returns>
        public static AfiliacionInteroperabilidad CrearCabeceraAfiliacion(
          this EntradaAfiliacionDirectorioDTO datos, DateTime fechaRegistro)
        {
            return AfiliacionInteroperabilidad.Crear(
                datos.TipoOperacion,
                datos.CodigoCliente,
                datos.NumeroCuentaAfiliada,
                datos.CodigoCuentaInterbancario,
                General.UsuarioPorDefectoInteroperabilidad,
                fechaRegistro
            );
        }
        /// <summary>
        /// Mapea loa datos a Afiliacoin Interoperabilidad
        /// </summary>
        /// <param name="datos">datis de ¿l reg¿istro</param>
        /// <param name="fechaRegistro">Fecha del sistema</param>
        /// /// <param name="fechaRegistro">Codigo del tramo</param>
        /// <returns>Retorna DTO de la afiliacion de interoperabildad</returns>
        public static AfiliacionInteroperabilidadDetalle CrearAfiliacionDetalle(
            EntradaAfiliacionDirectorioDTO datosAfiliacion,
            DateTime fechaRegistro)
        {
            return AfiliacionInteroperabilidadDetalle.Crear(
                datosAfiliacion.NumeroAfiliacion,
                datosAfiliacion.CodigoCuentaInterbancario,
                datosAfiliacion.NumeroCelular,
                AfiliacionInteroperabilidadDetalle.Afiliado,
                fechaRegistro,
                General.UsuarioPorDefectoInteroperabilidad,
                fechaRegistro,
                datosAfiliacion.Canal
            );
        }
        /// <summary>
        /// Convierte la información de una afiliación en un objeto de respuesta de afiliacion
        /// </summary>
        /// <param name="fechaRegistro">La fecha de registro de la afiliación.</param>
        /// <param name="cadenaHash">Una cadena opcional que representa un hash asociado a la afiliación. Puede ser null.</param>
        /// <returns> Retorna la respuesta de la afiliacion</returns>
        public static RespuestaAfiliacionCCEDTO ARespuestaAfiliacion(
            DateTime fechaRegistro,
            string? cadenaHash)
        {
            return new RespuestaAfiliacionCCEDTO
            {
                FechaOperacion = fechaRegistro,
                CadenaHash = cadenaHash
            };
        }
        /// <summary>
        /// Convierte la información de una desafiliación en un objeto de respuesta de desafiliación.
        /// </summary>
        /// <param name="fechaRegistro">La fecha de registro de la desafiliación.</param>
        /// <returns>Retorna la respuesta de la desafiliación.</returns>
        public static RespuestaAfiliacionCCEDTO ARespuestaDesafiliacion(
            DateTime fechaRegistro)
        {
            return new RespuestaAfiliacionCCEDTO
            {
                FechaOperacion = fechaRegistro
            };
        }

        /// <summary>
        /// Método que mapea la información de una afiliación en un objeto de respuesta para interoperabilidad de correo.
        /// </summary>
        /// <param name="afiliacionServicioInterno"></param>
        /// <param name="fecha"></param>
        /// <param name="numeroCelular"></param>
        /// <param name="numeroCuentaAfiliada"></param>
        /// <param name="contexto"></param>
        /// <param name="correoRemitente"></param>
        /// <param name="descripcion"></param>
        /// <param name="temaMensaje"></param>
        /// <param name="servicio"></param>
        /// <returns></returns>
        public static CorreoGeneralDTO ACorreAfiliacionInteroperabilidad(
           this AfiliacionServicioDTO afiliacionServicioInterno,
           DateTime fecha,
           string numeroCelular,
           string numeroCuentaAfiliada,
           IContextoAplicacion contexto,
           string correoRemitente,
           string descripcion,
           string temaMensaje,
           string servicio)
        {
            return new CorreoGeneralDTO()
            {
                Celular = numeroCelular,
                DescripcionOperacion = descripcion,
                NombreCliente = afiliacionServicioInterno.NombreCliente,
                CorreoElectronicoRemitente = correoRemitente,
                CorreoElectronicoDestinatario = afiliacionServicioInterno.CorreoElectronico,
                FechaOperacion = fecha,
                TemaMensaje = temaMensaje,
                Servicio = servicio,
                Cuenta = afiliacionServicioInterno.NombreProductoCuentaAfiliada + "-" + numeroCuentaAfiliada,
                DireccionIP = contexto.IpAddress ?? "--",
                Modelo = contexto.ModeloDispositivo ?? "--",
                SistemaOperativo = contexto.SistemaOperativo ?? "--",
                Navegador = contexto.Navegador ?? "--",
            };
        }
    }
}
