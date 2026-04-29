using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class PersonaJuridicaConfiguracion : IEntityTypeConfiguration<PersonaJuridica>
{
    public void Configure(EntityTypeBuilder<PersonaJuridica> builder)
    {
        ConfigurarPersonaJuridica(builder);
    }

    private void ConfigurarPersonaJuridica(EntityTypeBuilder<PersonaJuridica> builder)
    {
        builder.ToTable("CL_PERSONAS_JURIDICAS", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente });
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(5);
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").HasMaxLength(15);
        builder.Property(p => p.CodigoSector).HasColumnName("COD_SECTOR").HasMaxLength(5);
        builder.Property(p => p.ClaseSociedad).HasColumnName("CLASE_SOCIEDAD").HasMaxLength(5);
        builder.Property(p => p.CodigoSubactividad).HasColumnName("COD_SUBACTIV").HasMaxLength(5);
        builder.Property(p => p.CodigoSubsubactividad).HasColumnName("COD_SUBSUBACTIV").HasMaxLength(5);
        builder.Property(p => p.IdSectorActividad).HasColumnName("ID_SECTOR_ACTIVIDAD");
        builder.Property(p => p.CodigoActividad).HasColumnName("COD_ACTIVIDAD").HasMaxLength(5);
        builder.Property(p => p.RazonSocial).HasColumnName("RAZON_SOCIAL").HasMaxLength(120);
        builder.Property(p => p.NombreComercial).HasColumnName("NOM_COMERCIAL").HasMaxLength(60);
        builder.Property(p => p.IndicadorObligado).HasColumnName("IND_OBLIGADO").HasMaxLength(5);

        builder.Ignore(p => p.Nombres);
        builder.Ignore(p => p.ApellidoPaterno);
        builder.Ignore(p => p.ApellidoMaterno);
        builder.Ignore(p => p.TipoCliente);

        builder.HasOne(f => f.Cliente)
            .WithOne(f => f.PersonaJuridica)
            .HasForeignKey<PersonaJuridica>(f => new { f.CodigoEmpresa, f.CodigoCliente })
            .IsRequired(false);
    }
}
