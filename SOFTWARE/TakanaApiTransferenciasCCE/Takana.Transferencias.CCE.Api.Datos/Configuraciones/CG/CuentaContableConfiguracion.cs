using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CG;
/// <summary>
/// Clase que representa el mapeo de la clase de Dominio CuentaContable de la tabla CG_CATALOGO_X_EMPRESA
/// </summary>
public class CuentaContableConfiguracion : IEntityTypeConfiguration<CuentaContable>
{
   public void Configure(EntityTypeBuilder<CuentaContable> builder)
    {
        builder.ToTable("CG_CATALOGO_X_EMPRESA", "CG");
        builder.HasKey(c => new { c.CodigoEmpresa, c.NumeroCuentaContable });

        builder.Property(c => c.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired().ValueGeneratedNever(); 
        builder.Property(c => c.NumeroCuentaContable).HasColumnName("CUENTA_CONTABLE").IsRequired();
        builder.Property(c => c.CodigoMoneda).HasColumnName("COD_MONEDA").IsRequired();
        builder.Property(c => c.CodigoTipoCambio).HasColumnName("COD_TIP_CAMBIO").IsRequired();
        builder.Property(c => c.CodigoClaseTipoCambio).HasColumnName("CLASE_TIP_CAMBIO").IsRequired();
        builder.Property(c => c.ValorTasaCambio).HasColumnName("VAL_TIP_CAMBIO");
        builder.Property(c => c.IndicadorTipoCambio).HasColumnName("IND_TIP_CAMBIO");
        builder.Property(c => c.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(c => c.CodigoGrupo).HasColumnName("COD_GRUPO");
        builder.Property(c => c.DescripcionCuenta).HasColumnName("DES_CUENTA");
        builder.Property(c => c.CategoriaCuenta).HasColumnName("CATEG_CUENTA");
        builder.Property(c => c.IndicadorAcceso).HasColumnName("IND_ACCESO");
        builder.Property(c => c.IdCuenta).HasColumnName("ID_CUENTA");        
        builder.Property(c => c.CodigoTipoCuenta).HasColumnName("TIP_CUENTA");
    }     
}
