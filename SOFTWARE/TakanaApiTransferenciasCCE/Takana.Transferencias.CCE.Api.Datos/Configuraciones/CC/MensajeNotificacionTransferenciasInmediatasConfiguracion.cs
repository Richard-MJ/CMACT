using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio MensajeNotificacionTransferenciaInmediata 
    /// de la CC_TIN_INMEDIATA_MENSAJE_NOTIFICACION CC_FIRMAS_CLIENTE
    /// </summary>
    public class MensajeNotificacionTransferenciaInmediataConfiguracion : IEntityTypeConfiguration<MensajeNotificacionTransferenciaInmediata>
    {
        public void Configure(EntityTypeBuilder<MensajeNotificacionTransferenciaInmediata> builder)
        {
            builder.ToTable("CC_TIN_INMEDIATA_MENSAJE_NOTIFICACION", "CC");
            builder.HasKey(m => new { m.IdentificadorMensaje });
            
            builder.Property(p => p.IdentificadorMensaje).HasColumnName("ID_MENSAJE");
            builder.Property(p => p.IdentificadorTrama).HasColumnName("ID_TIPO_TRAMA");
            builder.Property(p => p.CodigoMensaje).HasColumnName("COD_MENSAJE");
            builder.Property(p => p.FechaMensaje).HasColumnName("FEC_MENSAJE");
            builder.Property(p => p.FechaConciliacion).HasColumnName("FEC_CONCILIACION");
            builder.Property(p => p.FechaNuevaLiquidacion).HasColumnName("FEC_NUEVA_LIQUIDACION");
            builder.Property(p => p.FechaAnteriorLiquidacion).HasColumnName("FEC_ANTERIOR_LIQUIDACION");
            builder.Property(p => p.DescripcionMensajeIdentificacion).HasColumnName("DES_MENSAJE_IDENTIFICACION");
            builder.Property(p => p.ClaseMensaje).HasColumnName("CLA_MENSAJE");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.CodigoEntidad).HasColumnName("COD_ENTIDAD_CCE");
            builder.Property(p => p.NombreEntidad).HasColumnName("NOM_ENTIDAD_CCE");
            builder.Property(p => p.SaldoBalance).HasColumnName("SAL_BALANCE");
            builder.Property(p => p.SaldoMinimo).HasColumnName("SAL_MINIMO");
            builder.Property(p => p.SaldoNormal).HasColumnName("SAL_NORMAL");
            builder.Property(p => p.SaldoUmbral).HasColumnName("SAL_UMBRAL");
            builder.Property(p => p.SaldoNuevo).HasColumnName("SAL_NUEVO");
            builder.Property(p => p.SaldoAnterior).HasColumnName("SAL_ANTERIOR");
            builder.Property(p => p.DescripcionMensaje).HasColumnName("DES_MENSAJE");
            builder.Property(p => p.EstadoNuevoLiquidacion).HasColumnName("EST_NUEVO_LIQUIDACION");
            builder.Property(p => p.EstadoAnteriorLiquidacion).HasColumnName("EST_ANTERIOR_LIQUIDACION");
            builder.Property(p => p.NumeroCreditosRecibidasAceptadas).HasColumnName("NUM_CREDITOS_RECIBIDAS_ACEPTADAS");
            builder.Property(p => p.TotalCreditosRecibidasAceptadas).HasColumnName("VAL_TOTAL_CREDITOS_RECIBIDAS_ACEPTADAS");
            builder.Property(p => p.NumeroCreditosRecibidasRechazadas).HasColumnName("NUM_CREDITOS_RECIBIDAS_RECHAZADAS");
            builder.Property(p => p.TotalCreditosRecibidasRechazadas).HasColumnName("VAL_TOTAL_CREDITOS_RECIBIDAS_RECHAZADAS");
            builder.Property(p => p.NumeroCreditosEnviadasAceptadas).HasColumnName("NUM_CREDITOS_ENVIADAS_ACEPTADAS");
            builder.Property(p => p.TotalCreditosEnviadasAceptadas).HasColumnName("VAL_TOTAL_CREDITOS_ENVIADAS_ACEPTADAS");
            builder.Property(p => p.NumeroCreditosEnviadasRechazadas).HasColumnName("NUM_CREDITOS_ENVIADAS_RECHAZADAS");
            builder.Property(p => p.TotalCreditosEnviadasRechazadas).HasColumnName("VAL_TOTAL_CREDITOS_ENVIADAS_RECHAZADAS");
            builder.Property(p => p.ValorConciliacion).HasColumnName("VAL_CONCILIACION");
            builder.Property(p => p.NumeroAnteriorConciliacionRealizada).HasColumnName("NUM_ANTERIOR_CONCILIACION_REALIZADA");
            builder.Property(p => p.ValorBrutoRealizada).HasColumnName("VAL_BRUTO_REALIZADA");
            builder.Property(p => p.NumeroAnteriorConciliacionReduccion).HasColumnName("NUM_ANTERIOR_CONCILIACION_REDUCCION");
            builder.Property(p => p.ValorBrutoReduccion).HasColumnName("VAL_BRUTO_REDUCCION");
            builder.Property(p => p.EstadoMensaje).HasColumnName("EST_MENSAJE");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
            builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");
        }
    }
}