using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio EntidadFinancieraPorTransferencia de la tabla CF_TIN_INMEDIATA_ENTIDAD_FINANCIERA_CCE_X_TRANSFERENCIA
/// </summary>
public class EntidadFinancieraPorEstadoSignConfiguracion: IEntityTypeConfiguration<EntidadFinancieraPorTransferencia>
{
    public void Configure(EntityTypeBuilder<EntidadFinancieraPorTransferencia> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_ENTIDAD_FINANCIERA_CCE_X_TRANSFERENCIA", "CF");
        builder.HasKey(m => new { m.Id });
        
        builder.Property(p => p.Id).HasColumnName("ID_ENTIDAD_X_TRANS");
        builder.Property(p => p.IdEntidad).HasColumnName("ID_ENTIDAD");
        builder.Property(p => p.IdentificadorTipoTransferencia).HasColumnName("ID_TIPO_TRANSFERENCIA");
        builder.Property(p => p.IndicadorParticipanteOriginante).HasColumnName("IND_PARTICIPA_ORIGINANTE");
        builder.Property(p => p.IndicadorParticipanteReceptor).HasColumnName("IND_PARTICIPA_RECEPTOR");

        builder.HasOne(c => c.TipoTransferencia).WithMany().HasForeignKey(c => new {c.IdentificadorTipoTransferencia});
    }
}
