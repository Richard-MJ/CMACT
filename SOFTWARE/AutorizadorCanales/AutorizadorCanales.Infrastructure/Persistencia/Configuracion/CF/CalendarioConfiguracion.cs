using AutorizadorCanales.Domain.Entidades.CF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class CalendarioConfiguracion : IEntityTypeConfiguration<Calendario>
{
    public void Configure(EntityTypeBuilder<Calendario> builder)
    {
        ConfigurarCalendario(builder);
    }

    private static void ConfigurarCalendario(EntityTypeBuilder<Calendario> builder)
    {
        builder.ToTable("CF_CALENDARIOS","CF");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoSistema });
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();

        builder.Property(p => p.FechaSistema).HasColumnName("FEC_HOY").IsRequired();
    }
}
