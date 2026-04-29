using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        ConfigurarUsuario(builder);
    }

    private static void ConfigurarUsuario(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("SG_USUARIOS", "SG");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoUsuario });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(p => p.IndicadorActivo).HasColumnName("IND_ACTIVO");
        builder.Property(p => p.Clave).HasColumnName("PALABRA_PASO");
        builder.Property(p => p.NombreUsuario).HasColumnName("NOM_USUARIO");
        builder.Property(p => p.CodigoPuesto).HasColumnName("COD_PUESTO");

        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.IndicadorHabilitado).HasColumnName("IND_HABILITADO");

        builder
            .HasMany(m => m.Accesos)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                right => right
                    .HasOne<AccesosPorSistema>()
                    .WithMany(),
                left => left
                    .HasOne<Usuario>()
                    .WithMany(),
                join => join
                    .ToTable("SG_USUARIOS_X_ACCESOS", "SG"));

        builder
            .HasMany(m => m.RolesAsignados)
            .WithOne(r => r.Usuario)
            .HasForeignKey(u => new { u.CodigoEmpresa, u.CodigoAgencia, u.CodigoUsuario });            

        builder
            .HasOne(f => f.Agencia)
            .WithMany()
            .HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoAgencia })
            .IsRequired();

        builder
            .HasOne(f => f.Cliente)
            .WithMany()
            .HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoCliente })
            .IsRequired();
    }
}
