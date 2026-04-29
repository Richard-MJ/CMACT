using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio Moneda de la tabla CF_MONEDAS
/// </summary>
public class MonedaConfiguracion : IEntityTypeConfiguration<Moneda>
{
    public void Configure(EntityTypeBuilder<Moneda> builder)
    {
        builder.ToTable("CF_MONEDAS", "CF");
        builder.HasKey(m => new { m.CodigoMoneda });

        builder.Property(m => m.CodigoMoneda).HasColumnName("COD_MONEDA");
        builder.Property(m => m.NombreMoneda).HasColumnName("DES_MONEDA");
        builder.Property(m => m.AbreviaturaMoneda).HasColumnName("ABR_MONEDA");
        builder.Property(m => m.CodigoSigla).HasColumnName("COD_SIGLA");
        builder.Property(m => m.Moneda_ML).HasColumnName("MONEDA_ML");
        builder.Property(m => m.CodigoMonedaIso).HasColumnName("COD_MONEDA_ISO");
        builder.Property(m => m.NombreSimple).HasColumnName("NOM_SIMPLE");
        builder.Property(m => m.CodigoRetencionSunat).HasColumnName("COD_RETENCION_SUNAT");
        builder.Property(m => m.CodigoMonedaPagoEfectivo).HasColumnName("MONEDA_PAGOEFECTIVO");
    }
}
