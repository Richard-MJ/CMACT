using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio CuentaAfiliada de la tabla CC_AFILIACION_SERVICIOS_CUENTAS
    /// </summary>
    public class EstadoCuentaConfiguracion : IEntityTypeConfiguration<EstadoCuenta>
    {
        public void Configure(EntityTypeBuilder<EstadoCuenta> builder)
        {
            builder.ToTable("CC_ESTADOS_CUENTA", "CC");
            builder.HasKey(m => m.CodigoEstado);

            builder.Property(m => m.CodigoEstado).HasColumnName("COD_ESTADO");
            builder.Property(m => m.DescripcionEstado).HasColumnName("DES_ESTADO");
            builder.Property(m => m.IndicadorEstadoVigente).HasColumnName("EST_VIGENTE");
        }
    }
}
