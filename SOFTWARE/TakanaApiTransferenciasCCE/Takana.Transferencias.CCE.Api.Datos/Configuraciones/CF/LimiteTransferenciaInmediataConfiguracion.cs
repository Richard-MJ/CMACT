using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio LimiteTransferenciaInmediata con la entidad CF_TIN_INMEDIATA_LIMITE_TRANSFERENCIA
    /// </summary>
    public class LimiteTransferenciaInmediataConfiguracion : IEntityTypeConfiguration<LimiteTransferenciaInmediata>
    {
        public void Configure(EntityTypeBuilder<LimiteTransferenciaInmediata> builder)
        {           
            builder.ToTable("CF_TIN_INMEDIATA_LIMITE_TRANSFERENCIA", "CF");
            builder.HasKey(k => k.IdLimiteTransferencia);

            builder.Property(p => p.IdLimiteTransferencia).HasColumnName("ID_LIMITE_TRANSFERENCIA").IsRequired();
            builder.Property(p => p.CodigoCanal).HasColumnName("IND_CANAL");
            builder.Property(p => p.IdTipoTransferencia).HasColumnName("ID_TIPO_TRANSFERENCIA");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.MontoLimiteMinimo).HasColumnName("MON_MIN");
            builder.Property(p => p.MontoLimiteMaximo).HasColumnName("MON_MAX");
            builder.Property(p => p.EstadoLimite).HasColumnName("IND_ESTADO");

            builder.HasOne(c => c.TipoTransferencia).WithMany().HasForeignKey(c => new {c.IdTipoTransferencia});
        }
    }
}