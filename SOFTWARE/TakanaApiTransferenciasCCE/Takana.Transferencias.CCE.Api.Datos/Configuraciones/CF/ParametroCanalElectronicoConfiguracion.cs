using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase de mapea la entidad de tipo de cuenta grupo de la tabla CC_IND_TIP_CTA_GRUPO
    /// </summary>
    public class ParametroCanalElectronicoConfiguracion : IEntityTypeConfiguration<ParametroCanalElectronico>
    {
        public void Configure(EntityTypeBuilder<ParametroCanalElectronico> builder)
        {
            builder.ToTable("CF_PARAMETROS_ATM", "CF");
            builder.HasKey(p => new { p.CodigoParametro, p.CodigoMoneda, p.CodigoCanal});

            builder.Property(p => p.CodigoParametro).HasColumnName("COD_PARAMETRO");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.CodigoCanal).HasColumnName("IND_CANAL");
            builder.Property(p => p.ValorParametro).HasColumnName("VAL_PARAMETRO").HasPrecision(20, 2);
        }
    }
}