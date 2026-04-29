using AutorizadorCanales.Domain.Entidades.CF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;

public class AgenciaConfiguracion : IEntityTypeConfiguration<Agencia>
{
    public void Configure(EntityTypeBuilder<Agencia> builder)
    {
        ConfigurarAgencia(builder);
    }

    private static void ConfigurarAgencia(EntityTypeBuilder<Agencia> builder)
    {
        builder.ToTable("CF_AGENCIAS", "CF");
        builder.HasKey(m => new { m.CodigoEmpresa, m.CodigoAgencia });

        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");

        builder.Property(m => m.NombreAgencia).HasColumnName("DES_AGENCIA");
        builder.Property(m => m.Direccion).HasColumnName("DIR_FISICA");
        builder.Property(m => m.Estado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.CodigoUbigeo).HasColumnName("COD_UBIGEO");

        builder.Property(m => m.Activo).HasColumnName("IND_FUNCIONAMIENTO");
        builder.Property(m => m.NombreCiudad).HasColumnName("NOM_CIUDAD");
    }
}
