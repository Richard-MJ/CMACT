using AutorizadorCanales.Domain.Entidades.SG;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;

public class TokenRefrescoConfiguracion : IEntityTypeConfiguration<TokenRefresco>
{
    public void Configure(EntityTypeBuilder<TokenRefresco> builder)
    {
        ConfigurarTokenRefresco(builder);
    }

    private void ConfigurarTokenRefresco(EntityTypeBuilder<TokenRefresco> builder)
    {
        builder.ToTable("SG_API_TOKENS_REFRESCO", "SG");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID").IsRequired().ValueGeneratedOnAdd();
        builder.Property(t => t.IdSecreto).HasColumnName("ID_SECRETA").HasMaxLength(128);
        builder.Property(t => t.IdSistemaCliente).HasColumnName("ID_AUDIENCIA").HasMaxLength(32);
        builder.Property(t => t.IdClienteApi).HasColumnName("ID_CLIENTE_API");
        builder.Property(t => t.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);
        builder.Property(t => t.FechaEmision).HasColumnName("FEC_EMISION");
        builder.Property(t => t.FechaExpiracion).HasColumnName("FEC_EXPIRACION");
        builder.Property(t => t.TicketProtegido).HasColumnName("DES_TICKET_PROT")
                .HasMaxLength(Int32.MaxValue);

        builder.Property(t => t.IdTipoAutenticacion).HasColumnName("ID_TIPO_AUTH");
        builder.Property(t => t.IdDispositivoAutenticacion).HasColumnName("ID_TERMINAL_AUTH").HasMaxLength(60);

        builder.Ignore(t => t.IdVisual);

        builder
            .HasOne(tp => tp.Audiencia)
            .WithMany()
            .HasForeignKey(tp => tp.IdSistemaCliente)
            .IsRequired();        
        builder
            .HasOne(tp => tp.ClienteApi)
            .WithMany()
            .HasForeignKey(tp => tp.IdClienteApi)
            .IsRequired();
    }
}
