using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio CatalogoTransaccion de la tabla CF_CATAL_TRANSACCIONES
/// </summary>
public class CatalogoTransaccionConfiguracion : IEntityTypeConfiguration<CatalogoTransaccion>
{
    public void Configure(EntityTypeBuilder<CatalogoTransaccion> builder)
    {
        builder.ToTable("CF_CATAL_TRANSACCIONES", "CF");
        builder.HasKey(k => new { k.CodigoSistema, k.TipoTransaccion });

        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired(); ;
        builder.Property(p => p.TipoTransaccion).HasColumnName("TIP_TRANSACCION").IsRequired(); ;
        builder.Property(p => p.DescripcionTransaccion).HasColumnName("DES_TRANSACCION");
        builder.Property(p => p.IndicadorMovimiento).HasColumnName("IND_MOVIMIENTO");
        builder.Property(p => p.CodigoLavado).HasColumnName("COD_LAVADO");
        builder.Property(p => p.IndicadorRetiroDepositoCTS).HasColumnName("IND_RET_DEP_CTS");
        builder.Property(p => p.CodigoIdentificacionAsientoSunat).HasColumnName("COD_IDENTIFICACION_ASIENTO_SUNAT");
    }    
}