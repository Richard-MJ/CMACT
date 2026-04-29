using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio TipoCambioHistorico de la tabla PA_TIP_CAM_HISTORICO
/// </summary>
public class TipoCambioHistoricoConfiguracion : IEntityTypeConfiguration<TipoCambioHistorico>
{
    public void Configure(EntityTypeBuilder<TipoCambioHistorico> builder)
    {
        builder.ToTable("PA_TIP_CAM_HISTORICO", "PA");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoMoneda, k.CodigoTipoCambio, k.FechaTipoCambio });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.FechaTipoCambio).HasColumnName("FEC_TIP_CAMBIO").IsRequired();
        builder.Property(p => p.ValorVenta).HasColumnName("VAL_VENTA").IsRequired();
        builder.Property(p => p.ValorCompra).HasColumnName("VAL_COMPRA").IsRequired();
        builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA").IsRequired();
        builder.Property(p => p.CodigoTipoCambio).HasColumnName("COD_TIP_CAMBIO").IsRequired();
    }     
}
