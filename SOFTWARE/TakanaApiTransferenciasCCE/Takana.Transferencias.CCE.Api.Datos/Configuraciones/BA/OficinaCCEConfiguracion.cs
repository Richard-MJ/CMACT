using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio OficinaCCE de la tabla BA_OFICINA_FINANCIERA_CCE
    /// </summary>
    public class OficinaCCEConfiguracion : IEntityTypeConfiguration<OficinaCCE>
    {
        public void Configure(EntityTypeBuilder<OficinaCCE> builder)
        {
            builder.ToTable("BA_OFICINA_FINANCIERA_CCE", "BA");
            builder.HasKey(k => new { k.IDEntidadFinanciera, k.IDPlaza, k.IDOficina });
            builder.Property(p => p.IDEntidadFinanciera).HasColumnName("ID_ENTIDAD").IsRequired();
            builder.Property(p => p.IDPlaza).HasColumnName("ID_PLAZA").IsRequired();
            builder.Property(p => p.IDOficina).HasColumnName("ID_OFICINA").IsRequired();
            builder.Property(p => p.CodigoOficina).HasColumnName("COD_OFICINA_CCE").IsRequired();
            builder.Property(p => p.EstadoOficina).HasColumnName("IND_ESTADO").IsRequired();
            builder.Property(p => p.CodigoUbigeoReferencia).HasColumnName("REF_CODIGO_UBIGEO").IsRequired();

            builder.HasOne(m => m.EntidadFinancieraCCE).WithMany(d => d.Oficinas).HasForeignKey(f => f.IDEntidadFinanciera);
            builder.HasOne(m => m.PlazaCCE).WithMany(d => d.Oficinas).HasForeignKey(f => f.IDPlaza);
        }
    }
}
