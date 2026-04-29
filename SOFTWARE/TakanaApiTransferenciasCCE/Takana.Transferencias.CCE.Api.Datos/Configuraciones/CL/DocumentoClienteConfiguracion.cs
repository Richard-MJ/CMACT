using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio DocumentoCliente de la tabla CL_ID_CLIENTES
/// </summary>
public class DocumentoClienteConfiguracion : IEntityTypeConfiguration<DocumentoCliente>
{
   public void Configure(EntityTypeBuilder<DocumentoCliente> builder)
    {
        builder.ToTable("CL_ID_CLIENTES", "CL");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoCliente, k.CodigoTipoDocumento });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE").IsRequired();
        builder.Property(p => p.CodigoTipoDocumento).HasColumnName("COD_TIPO_ID").IsRequired();
        builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_ID").IsRequired();
        builder.Property(p => p.FechaVencimiento).HasColumnName("FEC_VENCIM");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");

        builder.HasOne(m => m.Cliente).WithMany(d => d.Documentos).HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoCliente });
    }     
}
