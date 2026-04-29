using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.SG
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio SistemaCliente de la tabla SG_API_AUDIENCIA
    /// </summary>
    public class SistemaClienteConfiguracion : IEntityTypeConfiguration<SistemaCliente>
    {
        public void Configure(EntityTypeBuilder<SistemaCliente> builder)
        {
            builder.ToTable("SG_API_AUDIENCIA", "SG");
            builder.HasKey(m => new { m.Id });

            builder.Property(m => m.Id).HasColumnName("ID");
            builder.Property(m => m.IdSecreto).HasColumnName("ID_SECRETA_64");
            builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(m => m.DireccionOrigenPermitido).HasColumnName("DIR_ORIGEN_PERMITIDO");
        }
    }
}
