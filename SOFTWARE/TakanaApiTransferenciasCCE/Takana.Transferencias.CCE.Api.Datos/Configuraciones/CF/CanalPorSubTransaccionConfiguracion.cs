using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

public class CanalPorSubTransaccionConfiguracion : IEntityTypeConfiguration<CanalPorSubTransaccionCCE>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Canal de la tabla CF_TIN_INMEDIATA_CANAL
    /// </summary>
    public void Configure(EntityTypeBuilder<CanalPorSubTransaccionCCE> builder)
    {
        builder.ToTable("CF_TIN_INMEDIATA_CANAL_X_SUB_TRANSACCION", "CF");
        builder.HasKey(m => new { m.IdCanalPorSubTransaccion });

        builder.Property(m => m.IdCanalPorSubTransaccion).HasColumnName("ID_CANAL_X_SUB_TRANSACCION");
        builder.Property(m => m.CodigoCanalCCE).HasColumnName("COD_CANAL_CCE");
        builder.Property(m => m.IndicadorTipo).HasColumnName("IND_TIPO");
        builder.Property(m => m.CodigoCanal).HasColumnName("COD_CANAL_CMACT");
        builder.Property(m => m.NumeroSubCanal).HasColumnName("NUM_SUBCANAL_CMACT");
        builder.Property(m => m.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
        builder.Property(m => m.CodigoSubTipoTransaccion).HasColumnName("SUB_TIP_TRANSACCION");
        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.CodigoSistema).HasColumnName("COD_SISTEMA");
        builder.Property(m => m.IndicadorEstado).HasColumnName("IND_ESTADO");
        builder.Property(m => m.CodigoUsuarioTransaccion).HasColumnName("COD_USUARIO_TRANSACCION");

        builder.HasOne(m => m.CanalCCE).WithMany(p => p.CanalesPorSubTransaciones).HasForeignKey(f => f.CodigoCanalCCE );
        builder.HasOne(c => c.SubTipoTransaccion).WithMany()
            .HasForeignKey(c => new { c.CodigoEmpresa, c.CodigoSistema, c.CodigoTipoTransaccion, c.CodigoSubTipoTransaccion });
    }
}