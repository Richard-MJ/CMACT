using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase de mapea la entidad de tipo de cuenta grupo de la tabla CC_IND_TIP_CTA_GRUPO
    /// </summary>
    public class OperacionFrecuenteDetalleConfiguracion : IEntityTypeConfiguration<OperacionFrecuenteDetalle>
    {
        public void Configure(EntityTypeBuilder<OperacionFrecuenteDetalle> builder)
        {
            builder.ToTable("CC_OPERACION_FRECUENTE_DETALLE", "CC");
            builder.HasKey(m =>  m.NumeroDetalleOperacionFrecuente);

            builder.Property(o => o.NumeroDetalleOperacionFrecuente).HasColumnName("NUM_DET_OPE_FRECUENTE").IsRequired();
            builder.Property(o => o.NumeroOperacionFrecuente).HasColumnName("NUM_OPE_FRECUENTE").IsRequired();
            builder.Property(o => o.NumeroPropiedad).HasColumnName("NUM_PROPIEDAD").IsRequired();
            builder.Property(o => o.ValorPropiedad).HasColumnName("VAL_PROPIEDAD").IsRequired();
        }
    }
}
