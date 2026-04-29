using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class AccesosPorSistemaConfiguracion : IEntityTypeConfiguration<AccesosPorSistema>
{
    public void Configure(EntityTypeBuilder<AccesosPorSistema> builder)
    {
        ConfigurarAccesosPorSistema(builder);
    }

    private static void ConfigurarAccesosPorSistema(EntityTypeBuilder<AccesosPorSistema> builder)
    {
        builder.ToTable("SG_ACCESOS_X_SISTEMAS", "SG");
        builder.HasKey(m => m.CodigoAcceso);
        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoAcceso).HasColumnName("COD_ACCESO");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
    }
}
