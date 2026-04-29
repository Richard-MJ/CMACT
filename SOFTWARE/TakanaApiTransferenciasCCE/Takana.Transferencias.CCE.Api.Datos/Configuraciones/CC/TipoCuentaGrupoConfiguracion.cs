using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase de mapea la entidad de tipo de cuenta grupo de la tabla CC_IND_TIP_CTA_GRUPO
    /// </summary>
    public class TipoCuentaGrupoConfiguracion : IEntityTypeConfiguration<TipoCuentaGrupo>
    {
        public void Configure(EntityTypeBuilder<TipoCuentaGrupo> builder)
        {
            builder.ToTable("CC_IND_TIP_CTA_GRUPO", "CC");
            builder.HasKey(m =>  m.IndicadorTipoCuenta);

            builder.Property(m => m.IndicadorTipoCuenta).HasColumnName("IND_TIP_CUENTA").IsRequired();
            builder.Property(m => m.Descripcion).HasColumnName("DES_TIP_CUENTA");
        }
    }
}
