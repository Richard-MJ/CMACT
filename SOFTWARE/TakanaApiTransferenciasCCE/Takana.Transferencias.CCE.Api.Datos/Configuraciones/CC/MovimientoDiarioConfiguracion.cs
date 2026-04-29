using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio MovimientoDiario de la tabla CC_MOVIMTO_DIARIO
    /// </summary>
    public class MovimientoDiarioConfiguracion : IEntityTypeConfiguration<MovimientoDiario>
    {
        public void Configure(EntityTypeBuilder<MovimientoDiario> builder)
        {
            builder.ToTable("CC_MOVIMTO_DIARIO", "CC");
            builder.HasKey(k => k.NumeroMovimiento);

            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired().HasMaxLength(5);
            builder.Property(p => p.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired().HasPrecision(10, 0);
            builder.Property(p => p.NumeroMovimientoFuente).HasColumnName("NUM_MOV_FUENTE").HasPrecision(10, 0);
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA").HasMaxLength(15);
            builder.Property(p => p.FechaMovimiento).HasColumnName("FEC_MOVIMIENTO");
            builder.Property(p => p.EstadoMovimiento).HasColumnName("EST_MOVIMIENTO").HasMaxLength(2);
            builder.Property(p => p.MontoMovimiento).HasColumnName("MON_MOVIMIENTO").HasPrecision(18, 2);
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").HasMaxLength(2);
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION").HasMaxLength(5);
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUBTIP_TRANSAC").HasMaxLength(5);
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").HasMaxLength(15);
            builder.Property(p => p.CodigoProducto).HasColumnName("COD_PRODUCTO").HasMaxLength(5);
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOCUMENTO").HasPrecision(20, 0);
            builder.Property(p => p.IndAplicaCargo).HasColumnName("IND_APL_CARGO").HasMaxLength(2);
            builder.Property(p => p.DescripcionMovimiento).HasColumnName("DES_MOVIMIENTO").HasMaxLength(120);
            builder.Property(p => p.SistemaFuente).HasColumnName("SISTEMA_FUENTE").HasMaxLength(3);
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA").HasMaxLength(5);
            builder.Property(p => p.IndConsolidado).HasColumnName("IND_CONSOLIDADO").HasMaxLength(2);
            builder.Property(p => p.MontoDisponible).HasColumnName("MON_DISPONIBLE").HasPrecision(18, 2);
            builder.Property(p => p.MontoIntangible).HasColumnName("MON_INTANGIBLE").HasPrecision(18, 2);
            builder.Property(p => p.IndAfectaITF).HasColumnName("IND_AFECTA_ITF").HasMaxLength(2);
            builder.Property(p => p.IndDepAutomatico).HasColumnName("IND_DEP_AUTOMATICO").HasMaxLength(2);
            builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO").HasPrecision(10, 0);
            builder.Property(p => p.IndOrigenDestino).HasColumnName("IND_ORIGEN_DESTINO").HasMaxLength(2);
            builder.Property(p => p.FechaServidor).HasColumnName("FEC_SERVIDOR");
            builder.Property(p => p.IndicadorRemunerativo).HasColumnName("IND_REMUNERATIVO").HasMaxLength(2);

            builder.Ignore(p => p.NumeroAsientoContable);
            builder.Ignore(p => p.DescripcionAsientoMovimientoContable);
            builder.Ignore(p => p.EsPrincipal);
            builder.Ignore(p => p.TipoCuentaContable);
            builder.Ignore(p => p.CuentaContable);
            builder.Ignore(p => p.MontoMovimientoContable);
            builder.Ignore(p => p.ReferenciaMovimientoContable);
            builder.Ignore(p => p.CodigoUnidadEjecutora);
            builder.Ignore(p => p.EsTransaccionITF);
            builder.Ignore(p => p.MontoItf);
            builder.Ignore(p => p.MontoImpuestoRecibo);
            builder.Ignore(p => p.MontoComisionRecibo);
            builder.Ignore(p => p.MontoRedondeoOperacion);
            builder.Ignore(p => p.CodigoCuentaPuente);
            builder.Ignore(p => p.TasaCambioLocal);
            builder.Ignore(p => p.TasaCambioCuenta);           
            builder.Ignore(p => p.AgenciaOperacion);

            builder.HasOne(m => m.Cuenta).WithMany(d => d.MovimientosDiarios).HasForeignKey(f => new { f.CodigoEmpresa, f.NumeroCuenta });
            builder.HasOne(m => m.Transaccion).WithMany().HasForeignKey(m => new { m.CodigoSistema, m.CodigoTipoTransaccion });
            builder.HasOne(m => m.SubTipoTransaccionMovimiento).WithMany().HasForeignKey(f => new { f.CodigoEmpresa, f.CodigoSistema, f.CodigoTipoTransaccion, f.CodigoSubTipoTransaccion });
        }
    }
}