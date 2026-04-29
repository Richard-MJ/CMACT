using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio TransaccionOrdenTransferenciaInmediata 
    /// de la tabla CC_TIN_INMEDIATA_TRANSACCION_ORDEN_TRANSFERENCIA
    /// </summary>
    public class TransaccionOrdenTransferenciaInmediataConfiguracion : IEntityTypeConfiguration<TransaccionOrdenTransferenciaInmediata>
    {
        public void Configure(EntityTypeBuilder<TransaccionOrdenTransferenciaInmediata> builder)
        {
            builder.ToTable("CC_TIN_INMEDIATA_TRANSACCION_ORDEN_TRANSFERENCIA", "CC");
            builder.HasKey(m => new { m.IdTransaccion });
            
            builder.Property(p => p.IdTransaccion).HasColumnName("ID_TRANSACCION");
            builder.Property(p => p.IndicadorTransaccion).HasColumnName("IND_TRANSACCION");
            builder.Property(p => p.IdentificadorInstruccion).HasColumnName("ID_INSTRUCCION");
            builder.Property(p => p.NumeroTransferencia).HasColumnName("NUM_TRANSFERENCIA");
            builder.Property(p => p.NumeroLavado).HasColumnName("NUM_LAVADO");
            builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO");
            builder.Property(p => p.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO");
            builder.Property(p => p.FechaOperacion).HasColumnName("FEC_OPERACION_CCE");
            builder.Property(p => p.FechaRespuesta).HasColumnName("FEC_RESPUESTA_CCE");
            builder.Property(p => p.FechaLiquidacion).HasColumnName("FEC_LIQUIDACION");
            builder.Property(p => p.EntidadOriginante).HasColumnName("COD_ENTIDAD_ORIGINANTE");
            builder.Property(p => p.EntidadReceptor).HasColumnName("COD_ENTIDAD_RECEPTOR");
            builder.Property(p => p.CodigoCuentaInterbancariaOriginante).HasColumnName("COD_CUENTA_INTERBANCARIA_ORIGINANTE");
            builder.Property(p => p.TipoPersonaOriginante).HasColumnName("TIP_PERSONA_ORIGINANTE");
            builder.Property(p => p.TipoDocumentoIdentidadOriginante).HasColumnName("TIP_DOCUMENTO_IDENTIDAD_ORIGINANTE");
            builder.Property(p => p.NumeroDocumentoIdentidadOriginante).HasColumnName("NUM_IDENTIDAD_ORIGINANTE");
            builder.Property(p => p.NombreOriginante).HasColumnName("NOM_ORIGINANTE");
            builder.Property(p => p.TelefonoOriginante).HasColumnName("TEL_ORIGINANTE");
            builder.Property(p => p.CodigoCuentaInterbancariaReceptor).HasColumnName("COD_CUENTA_INTERBANCARIA_RECEPTOR");
            builder.Property(p => p.TipoDocumentoIdentidadReceptor).HasColumnName("TIP_DOCUMENTO_IDENTIDAD_RECEPTOR");
            builder.Property(p => p.NumeroDocumentoIdentidadReceptor).HasColumnName("NUM_IDENTIDAD_RECEPTOR");
            builder.Property(p => p.NombreReceptor).HasColumnName("NOM_RECEPTOR");
            builder.Property(p => p.TelefonoReceptor).HasColumnName("TEL_RECEPTOR");
            builder.Property(p => p.TarjetaCreditoReceptor).HasColumnName("COD_TARJETA_CREDITO_RECEPTOR");
            builder.Property(p => p.CodigoPlaza).HasColumnName("COD_PLAZA");
            builder.Property(p => p.CodigoTrace).HasColumnName("COD_TRACE");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.CodigoTarifa).HasColumnName("COD_TARIFA");
            builder.Property(p => p.CodigoCanal).HasColumnName("COD_CANAL");
            builder.Property(p => p.CodigoTitular).HasColumnName("COD_TITULAR");
            builder.Property(p => p.MontoTransferencia).HasColumnName("MON_TRANSFERENCIA");
            builder.Property(p => p.MontoComision).HasColumnName("MON_COMISION");
            builder.Property(p => p.MontoITF).HasColumnName("MON_ITF");
            builder.Property(p => p.MontoLiquidacionInterbancaria).HasColumnName("MON_LIQUIDACION");
            builder.Property(p => p.TipoTransferencia).HasColumnName("TIP_TRANSFERENCIA");
            builder.Property(p => p.NumeroReintentoSolicitud).HasColumnName("NUM_REINTENTO_SOLICITUD");            
            builder.Property(p => p.IndicadorEstadoOperacion).HasColumnName("IND_ESTADO_OPERACION");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
            builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
            builder.Property(p => p.CodigoUsuarioConciliacion).HasColumnName("COD_USUARIO_CONCILIACION");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");

            builder.HasOne(c => c.CanalCCE).WithMany().HasForeignKey(c => c.CodigoCanal);
            builder.HasOne(c => c.AsientoContable).WithMany().HasForeignKey(c => c.NumeroAsiento);
            builder.HasOne(c => c.Transferencia).WithMany().HasForeignKey(c => c.NumeroTransferencia);
            builder.HasOne(c => c.EntidadFinancieraOriginante).WithMany().HasForeignKey(c => c.EntidadOriginante);
        }   
    }
}