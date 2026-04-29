using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
public class PeriodoConfiguracion : IEntityTypeConfiguration<Periodo>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Periodo de la tabla CF_TIN_INMEDIATA_PERIODO
    /// </summary>
    public void Configure(EntityTypeBuilder<Periodo> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_PERIODO", "CF");
        builder.HasKey(m => new { m.Id });

        builder.Property(m => m.Id).HasColumnName("ID_PERIODO");
        builder.Property(g => g.Descripcion).HasColumnName("DES_PERIODO");
        builder.Property(g => g.HoraDesde).HasColumnName("HOR_DESDE");
        builder.Property(g => g.HoraHasta).HasColumnName("HOR_HASTA");
        builder.Property(g => g.ConsultaTiempoMinimo).HasColumnName("VAL_CONSULTA_TIEMPO_MINIMO");
        builder.Property(g => g.ConsultaTiempoMaximo).HasColumnName("VAL_CONSULTA_TIEMPO_MAXIMO");
        builder.Property(g => g.TransferenciaTiempoMinimo).HasColumnName("VAL_TRANSFERENCIA_TIEMPO_MINIMO");
        builder.Property(g => g.TransferenciaTiempoMaximo).HasColumnName("VAL_TRANSFERENCIA_TIEMPO_MAXIMO");        
        builder.Property(g => g.IndicadorEstado).HasColumnName("IND_ESTADO");
    }
}
