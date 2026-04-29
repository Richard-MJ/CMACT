using Microsoft.Extensions.DependencyInjection;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.Reporte;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Logica;

namespace Takana.Transferencias.CCE.Api.Infraestructura.Contenedor
{
    public static class ConfigurarPatronEstrategia
    {
        /// <summary>
        /// Método que agrega la configuracion de carga de los contexto de base de datos
        /// </summary>
        /// <param name="servicios"></param>
        /// <returns>Retorna la collecion del servicio</returns>
        public static IServiceCollection AddConfiguracionPatronKeyStrategy(this IServiceCollection servicios)
        {
            #region Patron de estrategia de Reportes
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, MantenimientoMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.MantenimientoMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, IncidenteMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.IncidenteMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ObjetivoTiempoRecuperacionMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ObjetivoTiempoRecuperacionMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDNoDisponibilidadDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDNoDisponibilidadDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDNoDisponibilidadMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDNoDisponibilidadMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDEfectividadBusquedaAliasDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDEfectividadBusquedaAliasDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDEfectividadBusquedaAliasMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDEfectividadBusquedaAliasMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDEfectividadTransferenciasDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDEfectividadTransferenciasDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDEfectividadTransferenciasMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDEfectividadTransferenciasMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDRendimientoBusquedaAliasDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDRendimientoBusquedaAliasDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDRendimientoBusquedaAliasMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDRendimientoBusquedaAliasMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDRendimientoTransferenciasDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDRendimientoTransferenciasDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ICDRendimientoTransferenciasMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.ICDRendimientoTransferenciasMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ResumenConsultaDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ResumenDeConsultasDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, ResumenTransaccionDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.ResumenDeTransaccionesDiario);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, VariacionUsuarioMontoTransferenciasMensualEstrategia>((int)GenerarReporteDTO.TipoReporte.VariacionUsuarioMontoTransferenciasMensual);
            servicios.AddKeyedScoped<IServicioGeneracionArchivoEstrategia, DirectorioInteroperabilidadDiarioEstrategia>((int)GenerarReporteDTO.TipoReporte.DirectorioInteroperabilidadDiario);
            #endregion

            #region Patron de estrategia de Tipo de Transferencias
            servicios.AddKeyedScoped<IServicioTipoTransferencia, TransferenciaOrdinariaEstrategia>(TipoTransferencia.CodigoTransferenciaOrdinaria);
            servicios.AddKeyedScoped<IServicioTipoTransferencia, PagoTarjetaCreditoEstrategia>(TipoTransferencia.CodigoPagoTarjeta);            
            #endregion

            return servicios;
        }
    }
}