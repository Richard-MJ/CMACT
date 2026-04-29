using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio TipoOperacionCanalOrigen de la tabla CF_TIP_OPERACION_ORIGEN_TTS
    /// </summary>
    public class TipoOperacionCanalOrigenConfiguracion : IEntityTypeConfiguration<TipoOperacionCanalOrigen>
    {
        public void Configure(EntityTypeBuilder<TipoOperacionCanalOrigen> builder)
        {
            builder.ToTable("CF_TIP_OPERACION_ORIGEN_TTS", "CF");
            builder.HasKey(k => new { k.IdTipoOperacion, k.CodigoCanal, k.CodigoSubCanal });

            builder.Property(p => p.IdOrigenLavado).HasColumnName("ID_ORIGEN_LAVADO").IsRequired();
            builder.Property(p => p.IdTipoOperacion).HasColumnName("ID_OPERACION");
            builder.Property(p => p.CodigoCanal).HasColumnName("COD_CANAL");
            builder.Property(p => p.CodigoSubCanal).HasColumnName("NUM_SUBCANAL");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSACCION");
            builder.Property(p => p.IntervinientePrincipal).HasColumnName("INTERVINIENTE_PRINCIPAL");
            
            builder.HasOne(p => p.SubTipoTransaccion).WithMany().HasForeignKey(p => new
            {
                p.CodigoEmpresa,
                p.CodigoSistema,
                p.CodigoTipoTransaccion,
                p.CodigoSubTipoTransaccion
            });
        }
    }
}
