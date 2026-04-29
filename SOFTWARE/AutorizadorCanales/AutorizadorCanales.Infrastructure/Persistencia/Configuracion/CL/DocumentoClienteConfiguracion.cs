using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class DocumentoClienteConfiguracion : IEntityTypeConfiguration<DocumentoCliente>
{
    public void Configure(EntityTypeBuilder<DocumentoCliente> builder)
    {
        ConfigurarDocumentoCliente(builder);
    }

    private void ConfigurarDocumentoCliente(EntityTypeBuilder<DocumentoCliente> builder)
    {
        builder.ToTable("CL_ID_CLIENTES", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente, k.CodigoTipoDocumento });
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
        builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_ID").IsRequired();

        builder.Property(p => p.FechaVencimiento).HasColumnName("FEC_VENCIM");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");

        builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_ID").IsRequired();

        builder.HasOne(m => m.Cliente)
            .WithMany(d => d.Documentos)
            .HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoCliente });
    }
}
