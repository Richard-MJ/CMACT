using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
public class CanalConfiguracion: IEntityTypeConfiguration<CanalCCE>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Canal de la tabla CF_TIN_INMEDIATA_CANAL
    /// </summary>
    public void Configure(EntityTypeBuilder<CanalCCE> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_CANAL", "CF");
        builder.HasKey(m => m.CodigoCanalCCE);

        builder.Property(m => m.IdCanal).HasColumnName("ID_CANAL");
        builder.Property(m => m.CodigoCanalCCE).HasColumnName("COD_CANAL_CCE");
        builder.Property(m => m.DescripcionCanal).HasColumnName("DES_CANAL");
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
    }
}
