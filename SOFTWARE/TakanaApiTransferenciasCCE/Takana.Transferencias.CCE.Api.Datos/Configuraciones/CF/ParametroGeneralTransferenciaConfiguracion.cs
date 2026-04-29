using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio ParametroGeneralTransferencia de la tabla CF_TIN_INMEDIATA_PARAMETROS_GENERALES
/// </summary>
public class ParametroGeneralTransferenciaConfiguracion : IEntityTypeConfiguration<ParametroGeneralTransferencia>
{
    public void Configure(EntityTypeBuilder<ParametroGeneralTransferencia> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_PARAMETROS_GENERALES", "CF");
        builder.HasKey(m => new { m.Id });
        
        builder.Property(p => p.Id).HasColumnName("ID_PARAMETRO");
        builder.Property(p => p.CodigoParametro).HasColumnName("COD_PARAMETRO");
        builder.Property(p => p.DescripcionParametro).HasColumnName("DES_PARAMETRO");
        builder.Property(p => p.ValorParametro).HasColumnName("VAL_PARAMETRO");
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(p => p.IndicadorOperacion).HasColumnName("IND_OPERACION");
        builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
    }    
}
