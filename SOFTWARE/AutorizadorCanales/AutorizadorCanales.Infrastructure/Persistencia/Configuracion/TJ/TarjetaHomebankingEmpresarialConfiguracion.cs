using AutorizadorCanales.Domain.Entidades.TJ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.TJ;

public class TarjetaHomebankingEmpresarialConfiguracion : IEntityTypeConfiguration<TarjetaHomebankingEmpresarial>
{
    public void Configure(EntityTypeBuilder<TarjetaHomebankingEmpresarial> builder)
    {
        ConfigurarTarjetaHomebankingEmpresarial(builder);
    }

    private void ConfigurarTarjetaHomebankingEmpresarial(EntityTypeBuilder<TarjetaHomebankingEmpresarial> builder)
    {
        builder.ToTable("TJ_TARJETA_HB_EMPRESARIAL", "TJ");
        builder.HasKey(k => new { k.CodigoEmpresa, k.NumeroTarjeta });

        builder.Property(p => p.CodigoEmpresa)
            .HasColumnName("COD_EMPRESA")
            .HasMaxLength(5)
            .IsRequired();

        builder.Property(p => p.NumeroTarjeta)
            .HasColumnName("NUM_TARJETA")
            .HasPrecision(16, 0)
            .IsRequired();

        builder.Property(p => p.CodigoClienteEmpresarial)
            .HasColumnName("COD_CLIENTE_EMPRESARIAL")
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(p => p.NumeroAutorizacion)
            .HasColumnName("NUM_AUTORIZACION");

        builder.Property(p => p.IndicadorEstado)
            .HasColumnName("IND_ESTADO")
            .HasMaxLength(2);

        builder.Property(p => p.CodigoUsuarioRegistro)
            .HasColumnName("COD_USUARIO_REGISTRO")
            .HasMaxLength(15);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("FEC_CREACION");

        builder.Property(p => p.CodigoUsuarioModifico)
            .HasColumnName("COD_USUARIO_MODIFICO")
            .HasMaxLength(15);

        builder.Property(p => p.FechaModifico)
            .HasColumnName("FEC_MODIFICO");

        builder.HasOne(r => r.ClienteEmpresarial)
            .WithMany()
            .HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoClienteEmpresarial })
            .IsRequired();

        builder.HasOne(r => r.Tarjeta)
            .WithMany()
            .HasForeignKey(f => f.NumeroTarjeta)
            .IsRequired();
    }
}
