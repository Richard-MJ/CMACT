using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio MenorCuantiaDetalle de la tabla CJ_MENOR_CUANTIA_DETA
    /// </summary>
    public class MenorCuantiaDetalleConfiguracion : IEntityTypeConfiguration<MenorCuantiaDetalle>
    {
        public void Configure(EntityTypeBuilder<MenorCuantiaDetalle> builder)
        {
            builder.ToTable("CJ_MENOR_CUANTIA_DETA", "CJ");
            builder.HasKey(k => new { k.IdentificadorMenorCuantiaDetalle });

            builder.Property(p => p.IdentificadorMenorCuantiaDetalle).HasColumnName("ID_MENOR_CUANTIA_DETA").IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NumeroOperacion).HasColumnName("NUM_OPERACION");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO");
            builder.Property(p => p.MontoMovimiento).HasColumnName("MON_MOVIMIENTO");
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUB_TIP_TRANSAC");
            builder.Property(p => p.IndicadorMovimiento).HasColumnName("IND_MOVIMIENTO");
            builder.Property(p => p.FechaOperacion).HasColumnName("FEC_OPERACION");
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.CodigoFormaPagoCJ).HasColumnName("COD_FORMA_PAGO_CJ");
            builder.Property(p => p.NumeroSecuenciaDocumento).HasColumnName("NUM_SECUENCIA_DOC");
            builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.NumeroMovimiento2).HasColumnName("NUM_MOVIMIENTO_2");
            builder.Property(p => p.NumeroCuenta2).HasColumnName("NUM_CUENTA_2");
            builder.Property(p => p.CodigoEntidadSBS).HasColumnName("COD_ENTIDAD_SBS");

            builder.HasOne(m => m.Encabezado).WithMany(d => d.Detalles).HasForeignKey(f => f.NumeroOperacion);
        }
    }
}
