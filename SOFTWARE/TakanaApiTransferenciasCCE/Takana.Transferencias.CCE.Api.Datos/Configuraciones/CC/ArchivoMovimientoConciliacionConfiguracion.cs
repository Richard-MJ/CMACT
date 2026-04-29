using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ArchivoMovimientoConciliacion con la tabla CC_TIN_INMEDIATA_MOVIMIENTO_ARCHIVO_CONCILIACION
    /// </summary>
    public class ArchivoMovimientoConciliacionConfiguracion : IEntityTypeConfiguration<ArchivoMovimientoConciliacion>
    {
        public void Configure(EntityTypeBuilder<ArchivoMovimientoConciliacion> builder)
        {
            builder.ToTable("CC_TIN_INMEDIATA_MOVIMIENTO_ARCHIVO_CONCILIACION", "CC");
            builder.HasKey(m => new { m.IdArchivoMovimiento });

            builder.Property(m => m.IdArchivoMovimiento).HasColumnName("ID_MOVIMIENTO_ARCHIVO");
            builder.Property(m => m.IdArchivoLote).HasColumnName("ID_LOTE_ARCHIVO");
            builder.Property(m => m.CodigoCuentaOrigen).HasColumnName("COD_CUENTA_INTERBANCARIA_ORIGEN");
            builder.Property(m => m.CodigoCuentaReceptor).HasColumnName("COD_CUENTA_TARJETA_RECEPTOR");
            builder.Property(m => m.EntidadReceptor).HasColumnName("COD_ENTIDAD_RECEPTOR");
            builder.Property(m => m.Monto).HasColumnName("MON_TRANSFERENCIA");
            builder.Property(m => m.MontoComision).HasColumnName("MON_COMISION");
            builder.Property(m => m.SignoComision).HasColumnName("SIG_COMISION");
            builder.Property(m => m.TipoTransferencia).HasColumnName("TIP_TRANSFERENCIA");
            builder.Property(m => m.FechaHora).HasColumnName("FEC_OPERACION");
            builder.Property(m => m.Canal).HasColumnName("COD_CANAL");
            builder.Property(m => m.CodigoProceso).HasColumnName("COD_PROCESO");
            builder.Property(m => m.ReferenciaTransferencia).HasColumnName("ID_REFERENCIA_TRANSFERENCIA");
            builder.Property(m => m.IdentificadorTransaccion).HasColumnName("ID_TRANSACCION");
            builder.Property(m => m.Estado).HasColumnName("IND_ESTADO_OPERACION");
            builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        }
    }
}