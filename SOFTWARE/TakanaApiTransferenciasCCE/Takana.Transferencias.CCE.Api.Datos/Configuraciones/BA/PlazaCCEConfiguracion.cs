using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio PlazaCCE de la tabla BA_PLAZA_FINANCIERA_CCE
    /// </summary>
    public class PlazaCCEConfiguracion : IEntityTypeConfiguration<PlazaCCE>
    {
        public void Configure(EntityTypeBuilder<PlazaCCE> builder)
        {
            builder.ToTable("BA_PLAZA_FINANCIERA_CCE", "BA");
            builder.HasKey(k => k.IDPlaza);

            builder.Property(p => p.IDPlaza).HasColumnName("ID_PLAZA").IsRequired();
            builder.Property(p => p.DescripcionPlaza).HasColumnName("DES_PLAZA").IsRequired();
            builder.Property(p => p.CodigoUbigeo).HasColumnName("COD_UBIGEO_COMPENSABLE").IsRequired();
            builder.Property(p => p.EstadoPlaza).HasColumnName("IND_ESTADO").IsRequired();
            builder.Property(p => p.EsPlazaExclusiva).HasColumnName("IND_PLAZA_EXCLUSIVA").IsRequired();
        }
    }
}
