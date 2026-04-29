using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.TJ;

public class TarjetaMovimientoConfiguracion : IEntityTypeConfiguration<TarjetaMovimiento>
{
    public void Configure(EntityTypeBuilder<TarjetaMovimiento> builder)
    {
        builder.ToTable("TJ_MOVIMIENTOS", "TJ");

        builder.HasKey(m => m.IdMovimento);

        builder.Property(m => m.IdMovimento).HasColumnName("NUM_MOVIMIENTO").IsRequired();
        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(m => m.CodigoUsuario).HasColumnName("COD_USUARIO");
        builder.Property(m => m.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA").IsRequired();
        builder.Property(m => m.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
        builder.Property(m => m.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC");
        builder.Property(m => m.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO");
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.Observaciones).HasColumnName("OBS_MOVIMTO");
        builder.Property(m => m.IndicadorCanal).HasColumnName("IND_TTS");
        builder.Property(m => m.DescripcionObservacion).HasColumnName("DES_OBSERVACION");
        builder.Property(m => m.NumeroMovimientoFuente).HasColumnName("NUM_MOV_FUENTE");

        builder.Ignore(p => p.NumeroOperacion);
        builder.Ignore(p => p.CodigoSistema);
        builder.HasOne(tm => tm.Tarjeta)
            .WithMany()
            .HasForeignKey(tm => tm.NumeroTarjeta)
            .IsRequired();
    }
}
