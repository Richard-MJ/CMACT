using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase de mapea la entidad de tipo de cuenta grupo de la tabla CC_IND_TIP_CTA_GRUPO
    /// </summary>
    public class TipoTramaConfiguracion : IEntityTypeConfiguration<TipoTrama>
    {
        public void Configure(EntityTypeBuilder<TipoTrama> builder)
        {
            builder.ToTable("CC_TIPO_TRAMA_TRANSFERENCIA_INMEDIATA", "CC");
            builder.HasKey(m =>  m.Id);

            builder.Property(m => m.Id).HasColumnName("ID_TIPO_TRAMA");
            builder.Property(m => m.Descripcion).HasColumnName("DES_TRAMA");
            builder.Property(m => m.Tipo).HasColumnName("TIP_TRAMA");
        }
    }
}
