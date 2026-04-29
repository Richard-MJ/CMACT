using AutorizadorCanales.Domain.Entidades.CF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class SubTipoTransaccionConfiguracion : IEntityTypeConfiguration<SubTipoTransaccion>
{
    public void Configure(EntityTypeBuilder<SubTipoTransaccion> builder)
    {
        ConfigurarSubTipoTransaccion(builder);
    }

    private static void ConfigurarSubTipoTransaccion(EntityTypeBuilder<SubTipoTransaccion> builder)
    {
        builder.ToTable("CF_SUBTIP_TRANSAC", "CF");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoSistema, k.CodigoTipoTransaccion, k.CodigoSubTipoTransaccion });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired().HasMaxLength(5);
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired().HasMaxLength(2);
        builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION").IsRequired().HasMaxLength(5);
        builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC").IsRequired().HasMaxLength(5);
        builder.Property(p => p.DescripcionSubTransaccion).HasColumnName("DES_SUBTRANSAC").IsRequired().HasMaxLength(60);
    }
}
