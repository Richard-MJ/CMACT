using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio CuentaEfectivo de la tabla CC_CUENTA_EFECTIVO
/// </summary>
public class CuentaEfectivoSueldoConfiguracion : IEntityTypeConfiguration<CuentaEfectivoSueldo>
{
    public void Configure(EntityTypeBuilder<CuentaEfectivoSueldo> builder)
    {
        builder.ToTable("CC_CUENTA_EFECTIVO_SUELDO", "CC");
        builder.HasKey(k => new { k.CodigoEmpresa, k.NumeroCuenta });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA");
        builder.Property(p => p.SaldoDisponibleRemu).HasColumnName("SAL_DISPONIBLE_REMU");
        builder.Property(p => p.SaldoDisponibleNoRemu).HasColumnName("SAL_DISPONIBLE_NOREMU");
        builder.Property(p => p.SaldoTransitoRemu).HasColumnName("SAL_TRANSITO_REMU");
        builder.Property(p => p.SaldoTransitoNoRemu).HasColumnName("SAL_TRANSITO_NOREMU");
        builder.Property(p => p.SaldoCongeladoRemu).HasColumnName("SAL_CONGELADO_REMU");
        builder.Property(p => p.SaldoCongeladoNoRemu).HasColumnName("SAL_CONGELADO_NOREMU");
    }
}
