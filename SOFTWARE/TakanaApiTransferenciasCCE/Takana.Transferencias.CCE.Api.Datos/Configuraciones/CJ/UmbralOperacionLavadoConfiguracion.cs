using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio UmbralOperacionLavado de la tabla CJ_UMBRAL_X_TIPO_OPERACION_X_AGENCIA
    /// </summary>
    public class UmbralOperacionLavadoConfiguracion : IEntityTypeConfiguration<UmbralOperacionLavado>
    {
        public void Configure(EntityTypeBuilder<UmbralOperacionLavado> builder)
        {
            builder.ToTable("CJ_UMBRAL_X_TIPO_OPERACION_X_AGENCIA", "CJ");
            builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoAgencia, k.CodigoTipoOperacion });

            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").IsRequired();
            builder.Property(p => p.CodigoTipoOperacion).HasColumnName("COD_TIPO_OPERACION").IsRequired();
            builder.Property(p => p.MontoLimite).HasColumnName("MON_OPERACION").IsRequired();
            builder.Property(p => p.EstaActivo).HasColumnName("IND_ACTIVO").IsRequired();
        }
    }
}
