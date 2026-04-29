using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Transferencia de la tabla CC_TRANSFERENCIAS_ENCABEZADO
    /// </summary>
    public class TransferenciaConfiguracion : IEntityTypeConfiguration<Transferencia>
    {
        public void Configure(EntityTypeBuilder<Transferencia> builder)
        {
            builder.ToTable("CC_TRANSFERENCIAS_ENCABEZADO", "CC");
            builder.HasKey(k => k.NumeroTransferencia);

            builder.Property(p => p.NumeroTransferencia).HasColumnName("NUM_TRANSFERENCIA").IsRequired().ValueGeneratedNever();
            builder.Property(p => p.CodigoTipoTransferencia).HasColumnName("TIP_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.FechaTransferencia).HasColumnName("FEC_SISTEMA").IsRequired();
            builder.Property(p => p.CodigoOrigen).HasColumnName("COD_ORIGEN").IsRequired();
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(p => p.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA").IsRequired();
            builder.Property(p => p.MontoTransferencia).HasColumnName("MON_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.EstadoTransferencia).HasColumnName("IND_ESTADO").IsRequired();
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired();
            builder.Property(p => p.CodigoEnte).HasColumnName("COD_ENTE");
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOCUMENTO");
            builder.Property(p => p.DetalleSumillaTransferencia).HasColumnName("DETALLE_TRANSFERENCIA").HasMaxLength(150);
            builder.Property(p => p.NumeroMovimientoFuente).HasColumnName("CC_NUM_MOV_FUENTE");
            builder.Property(p => p.Canal).HasColumnName("IND_CANAL");

            builder.HasOne(c => c.CuentaOrigen).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.NumeroCuenta });
            builder.HasOne(c => c.Agencia).WithMany().HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoAgencia });
            builder.HasMany(p => p.DetallesEntrantes).WithOne(d => d.Transferencia).HasForeignKey(c => new { c.NumeroTransferencia, c.CodigoCuentaInterbancario });
            builder.HasMany(p => p.DetallesSalientes).WithOne(d => d.Transferencia).HasForeignKey(c => new { c.NumeroTransferencia, c.NumeroDetalle });
        }
    }
}
