using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConceptoCobroCce de la tabla CF_CONCEPTO_COBRO_CCE
    /// </summary>
    public class ConceptoCobroCCEConfiguracion : IEntityTypeConfiguration<ConceptoCobroCCE>
    {
        public void Configure(EntityTypeBuilder<ConceptoCobroCCE> builder)
        {
            builder.ToTable("CF_CONCEPTO_COBRO_CCE", "CF");
            builder.HasKey(m => new { m.IdConcepto });

            builder.Property(m => m.IdConcepto).HasColumnName("ID_CONCEPTO");
            builder.Property(m => m.Codigo).HasColumnName("COD_CONCEPTO");
            builder.Property(m => m.Descripcion).HasColumnName("DES_CONCEPTO");
            builder.Property(m => m.IndicadorHabilitado).HasColumnName("IND_HABILITADO");
        }
    }

}
