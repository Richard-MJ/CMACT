using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Agencia con la entidad BA_ENTIDAD_FINANCIERA_CCE_INMEDIATA
    /// </summary>
    public class EstadoInmediataConfiguracion : IEntityTypeConfiguration<EstadoInmediata>
    {
        public void Configure(EntityTypeBuilder<EstadoInmediata> builder)
        {
            builder.ToTable("CF_TIN_INMEDIATA_ESTADOS", "CF");
            builder.HasKey(m => new { m.Codigo });
            
            builder.Property(p => p.Codigo).HasColumnName("COD_ESTADO");
            builder.Property(p => p.Descripcion).HasColumnName("DES_ESTADO");
        }
    }
}