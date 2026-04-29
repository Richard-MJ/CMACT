using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CG;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio AsientoContableDetalle de la tabla CG_ASTO_DETALLE
/// </summary>
public class AsientoContableDetalleConfiguracion : IEntityTypeConfiguration<AsientoContableDetalle>
{
   public void Configure(EntityTypeBuilder<AsientoContableDetalle> builder)
    {
        builder.ToTable("CG_ASTO_DETALLE", "CG");
        builder.HasKey(k => new { k.NumeroAsiento, k.NumeroLinea });

        builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO").IsRequired().ValueGeneratedNever();
        builder.Property(p => p.NumeroLinea).HasColumnName("NUM_LINEA").IsRequired();
        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(p => p.NumeroCuentaContable).HasColumnName("CUENTA_CONTABLE");
        builder.Property(p => p.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO");
        builder.Property(p => p.Debito).HasColumnName("DEBITO");
        builder.Property(p => p.Credito).HasColumnName("CREDITO");
        builder.Property(p => p.Debito_Cta).HasColumnName("DEBITO_CTA");
        builder.Property(p => p.Credito_Cta).HasColumnName("CREDITO_CTA");
        builder.Property(p => p.DetalleAsiento).HasColumnName("DETALLE");
        builder.Property(p => p.TipoCambioBase).HasColumnName("TIP_CAM_BASE").HasPrecision(8, 4);
        builder.Property(p => p.TipoCambioCuenta).HasColumnName("TIP_CAM_CTA").HasPrecision(8, 4);
        builder.Property(p => p.Referencia).HasColumnName("REFERENCIA");
        builder.Property(p => p.CodigoUnidadEjecutora).HasColumnName("COD_UNIDAD");

        builder.HasOne(m => m.Asiento).WithMany(d => d.Detalles).HasForeignKey(f => new { f.NumeroAsiento });
        builder.HasOne(m => m.CuentaContable).WithMany().HasForeignKey(f => new { f.CodigoEmpresa, f.NumeroCuentaContable });
    }     
}
