using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class UsuarioRolConfiguracion : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> builder)
    {
        ConfigurarRolSeguridad(builder);
    }

    private static void ConfigurarRolSeguridad(EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.ToTable("SG_USUARIOS_X_ROL", "SG");
        builder.HasKey(k => k.CodigoRol);
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired();
        builder.Property(p => p.CodigoRol).HasColumnName("COD_ROL").IsRequired();
    }
}
