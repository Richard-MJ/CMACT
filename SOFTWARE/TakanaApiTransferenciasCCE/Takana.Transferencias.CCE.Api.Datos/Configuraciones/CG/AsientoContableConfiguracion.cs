using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CG;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio AsientoContable de la tabla CG_ASTO_RESUMEN
/// </summary>
public class AsientoContableConfiguracion : IEntityTypeConfiguration<AsientoContable>
{
   public void Configure(EntityTypeBuilder<AsientoContable> builder)
    {
        builder.ToTable("CG_ASTO_RESUMEN", "CG");
        builder.HasKey(k => new { k.NumeroAsiento });

        builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO").IsRequired().ValueGeneratedNever();
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
        builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC");
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(p => p.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO");
        builder.Property(p => p.DetalleAsiento).HasColumnName("DES_ASIENTO");
        builder.Property(p => p.EstadoAsiento).HasColumnName("EST_ASIENTO");
        builder.Property(p => p.FechaAsiento).HasColumnName("FEC_ASIENTO");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
        builder.Property(p => p.IndLiquidacion).HasColumnName("IND_LIQUIDACION");
    }     
}
