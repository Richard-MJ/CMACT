using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

/// <summary>
/// Clase que representa el mapeo de la clase de Dominio SubTipoTransaccion de la tabla CF_SUBTIP_TRANSAC
/// </summary>
public class SubTipoTransaccionConfiguracion : IEntityTypeConfiguration<SubTipoTransaccion>
{
    public void Configure(EntityTypeBuilder<SubTipoTransaccion> builder)
    {
        builder.ToTable("CF_SUBTIP_TRANSAC", "CF");
        builder.HasKey(k => new { k.CodigoEmpresa, k.CodigoSistema, k.CodigoTipoTransaccion, k.CodigoSubTipoTransaccion });

        builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
        builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION").IsRequired();
        builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC").IsRequired();
        builder.Property(p => p.DescripcionSubTransaccion).HasColumnName("DES_SUBTRANSAC").IsRequired();
        builder.Property(p => p.TipoOperacion).HasColumnName("COD_TIPO_OPERACION");
        builder.Property(p => p.AplicaContabilizadon).HasColumnName("IND_APLICA_ASIENTO");
        builder.Property(p => p.AplicaLavado).HasColumnName("IND_APLICA_LAVADO");
        builder.Property(p => p.IndicadorContablePrincipal).HasColumnName("IND_CONTA_PRINCIPAL");
        builder.Property(p => p.IndicadorComandoPrincipal).HasColumnName("IND_COMANDO_PRINCIPAL");
        builder.Property(p => p.CodigoFormaPagoLavado).HasColumnName("COD_FORMA_PAGO_LAVADO");
        builder.Property(p => p.IndicadorMovimientoLavando).HasColumnName("IND_MOVIMIENTO_LAVADO");
        builder.Property(p => p.DescripcionAuxiliar).HasColumnName("DES_AUXILIAR");

        builder.HasOne(m => m.Transaccion).WithMany().HasForeignKey(f => new { f.CodigoSistema, f.CodigoTipoTransaccion });
    }    
}