using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Extensiones
{
    public static class NotificacionExtension
    {
        /// <summary>
        /// Método que mapea el formato para correo de reportes
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="correoRemitente"></param>
        /// <param name="correoDestinatario"></param>
        /// <param name="mensajeNotificacion"></param>
        /// <returns></returns>
        public static CorreoNotificacionDTO AdtoFormatoNotificacionCorreo(
            this IContextoAplicacion contexto,
            DateTime fechaSistema,
            string correoRemitente,
            string correoDestinatario,
            string temaMensaje,
            MensajeNotificacionTransferenciaInmediata mensajeNotificacion)
        {
            return new CorreoNotificacionDTO()
            {
                CorreoElectronicoRemitente = correoRemitente,
                CorreoElectronicoDestinatario = correoDestinatario,
                FechaOperacion = fechaSistema,
                TemaMensaje = temaMensaje.ToUpper(),
                DireccionIP = contexto.IpAddress ?? "--",
                Modelo = contexto.ModeloDispositivo ?? "--",
                SistemaOperativo = contexto.SistemaOperativo ?? "--",
                Navegador = contexto.Navegador ?? "--",
                IdentificadorMensaje = mensajeNotificacion.IdentificadorMensaje,
                IdentificadorTrama = mensajeNotificacion.IdentificadorTrama,
                CodigoMensaje = mensajeNotificacion.CodigoMensaje,
                FechaMensaje = mensajeNotificacion.FechaMensaje,
                FechaConciliacion = mensajeNotificacion.FechaConciliacion,
                FechaNuevaLiquidacion = mensajeNotificacion.FechaNuevaLiquidacion,
                FechaAnteriorLiquidacion = mensajeNotificacion.FechaAnteriorLiquidacion,
                DescripcionMensajeIdentificacion = mensajeNotificacion.DescripcionMensajeIdentificacion,
                ClaseMensaje = mensajeNotificacion.ClaseMensaje,
                CodigoMoneda = string.IsNullOrEmpty(mensajeNotificacion.CodigoMoneda) 
                    ? mensajeNotificacion.CodigoMoneda 
                    : mensajeNotificacion.CodigoMoneda  == ((int)Moneda.MonedaCodigo.SolesCCE).ToString() 
                    ? Moneda.Soles : Moneda.Dolares,
                CodigoEntidad = mensajeNotificacion.CodigoEntidad,
                NombreEntidad = mensajeNotificacion.NombreEntidad,
                SaldoBalance = mensajeNotificacion.SaldoBalance,
                SaldoMinimo = mensajeNotificacion.SaldoMinimo,
                SaldoNormal = mensajeNotificacion.SaldoNormal,
                SaldoUmbral = mensajeNotificacion.SaldoUmbral,
                SaldoNuevo = mensajeNotificacion.SaldoNuevo,
                SaldoAnterior = mensajeNotificacion.SaldoAnterior,
                DescripcionMensaje = mensajeNotificacion.DescripcionMensaje,
                EstadoNuevoLiquidacion = mensajeNotificacion.EstadoNuevoLiquidacion,
                EstadoAnteriorLiquidacion = mensajeNotificacion.EstadoAnteriorLiquidacion,
                NumeroCreditosRecibidasAceptadas = mensajeNotificacion.NumeroCreditosRecibidasAceptadas,
                TotalCreditosRecibidasAceptadas = mensajeNotificacion.TotalCreditosRecibidasAceptadas,
                NumeroCreditosRecibidasRechazadas = mensajeNotificacion.NumeroCreditosRecibidasRechazadas,
                TotalCreditosRecibidasRechazadas = mensajeNotificacion.TotalCreditosRecibidasRechazadas,
                NumeroCreditosEnviadasAceptadas = mensajeNotificacion.NumeroCreditosEnviadasAceptadas,
                TotalCreditosEnviadasAceptadas = mensajeNotificacion.TotalCreditosEnviadasAceptadas,
                NumeroCreditosEnviadasRechazadas = mensajeNotificacion.NumeroCreditosEnviadasRechazadas,
                TotalCreditosEnviadasRechazadas = mensajeNotificacion.TotalCreditosEnviadasRechazadas,
                ValorConciliacion = mensajeNotificacion.ValorConciliacion,
                NumeroAnteriorConciliacionRealizada = mensajeNotificacion.NumeroAnteriorConciliacionRealizada,
                ValorBrutoRealizada = mensajeNotificacion.ValorBrutoRealizada,
                NumeroAnteriorConciliacionReduccion = mensajeNotificacion.NumeroAnteriorConciliacionReduccion,
                ValorBrutoReduccion = mensajeNotificacion.ValorBrutoReduccion,
                EstadoMensaje = mensajeNotificacion.EstadoMensaje,
                CodigoSistema = mensajeNotificacion.CodigoSistema,
                IndicadorEstado = mensajeNotificacion.IndicadorEstado,
                CodigoUsuarioRegistro = mensajeNotificacion.CodigoUsuarioRegistro,
                CodigoUsuarioModifico = mensajeNotificacion.CodigoUsuarioModifico,
                FechaRegistro = mensajeNotificacion.FechaRegistro,
                FechaModifico = mensajeNotificacion.FechaModifico
            };
        }
    }
}
