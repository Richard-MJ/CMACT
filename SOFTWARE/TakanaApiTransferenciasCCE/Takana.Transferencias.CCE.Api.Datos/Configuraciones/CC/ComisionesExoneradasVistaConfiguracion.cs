using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ComisionesExoneradasVistaConfiguracion : IEntityTypeConfiguration<ComisionesExoneradasVista>
    {
        public void Configure(EntityTypeBuilder<ComisionesExoneradasVista> builder)
        {
            builder.ToTable("VW_CC_COMISIONES_EXONERADAS", "CC");
            builder.HasKey(k => new { k.CodigoComisionExonerada });

            builder.Property(p => p.CodigoComisionExonerada).HasColumnName("ID_COMISION_EXON");
            builder.Property(p => p.CodigoComision).HasColumnName("COD_COMISION");
            builder.Property(p => p.NumeroMovimientoOrigen).HasColumnName("NUM_MOVIMIENTO_ORIGEN");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO");
            builder.Property(p => p.CodigoEstadoMovimiento).HasColumnName("EST_MOVIMIENTO");

            builder.HasOne(m => m.CuentaEfectivo)
              .WithMany(d => d.ComisionesExoneradas)
              .HasForeignKey(f => new { f.CodigoEmpresa, f.NumeroCuenta });
        }
    }
}
