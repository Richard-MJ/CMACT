using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio CuentaAfiliada de la tabla CC_AFILIACION_SERVICIOS_CUENTAS
    /// </summary>
    public class LimitesOperacionesCuentaConfiguracion : IEntityTypeConfiguration<LimitesOperacionesCuenta>
    {
        public void Configure(EntityTypeBuilder<LimitesOperacionesCuenta> builder)
        {
            builder.ToTable("CC_LIMITES_OPERACIONES_CUENTA", "CC");
            builder.HasKey(m => new { m.IdLimite });

            builder.Property(p => p.IdLimite).HasColumnName("ID_LIMITE").ValueGeneratedNever();
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").HasMaxLength(1);
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA").HasMaxLength(15);
            builder.Property(p => p.IdTipoLimite).HasColumnName("ID_TIP_LIMITE");
            builder.Property(p => p.ValorLimite).HasColumnName("VAL_LIMITE");
            builder.Property(p => p.IndicadorCanal).HasColumnName("IND_CANAL").HasMaxLength(1);
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);

            builder.HasOne(f => f.Cuenta).WithMany(c => c.LimitesOperacionesCuenta).HasForeignKey(f => new { f.CodigoEmpresa, f.NumeroCuenta });
        }
    }
}
