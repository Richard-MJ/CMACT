using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio OperacionUnicaLavado de la tabla CJ_OPERACION_UNICA_LAVADO
    /// </summary>
    public class OperacionUnicaLavadoConfiguracion : IEntityTypeConfiguration<OperacionUnicaLavado>
    {
        public void Configure(EntityTypeBuilder<OperacionUnicaLavado> builder)
        {
            builder.ToTable("CJ_OPERACION_UNICA_LAVADO", "CJ");
            builder.HasKey(k => new { k.NumeroLavado });

            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.NumeroLavado).HasColumnName("NUM_MOVIMIENTO").IsRequired();
            builder.Property(p => p.CodigoAgencia).HasColumnName("COD_AGENCIA");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.CodigoTipoTransaccion).HasColumnName("TIP_TRANSACCION");
            builder.Property(p => p.NumeroSecuenciaDocumento).HasColumnName("NUM_SECUENCIA_DOC");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.NumeroCuenta).HasColumnName("NUM_CUENTA");
            builder.Property(p => p.TipoLavado).HasColumnName("TIP_LAVADO");
            builder.Property(p => p.IndicadorMovimiento).HasColumnName("IND_MOVIMIENTO");
            builder.Property(p => p.MontoMovimiento).HasColumnName("MTO_MOVIMIENTO");
            builder.Property(p => p.FechaOperacion).HasColumnName("FEC_OPERACION");
            builder.Property(p => p.IndicadorFormaPago).HasColumnName("IND_FORMA_PAGO");
            builder.Property(p => p.DescripcionFondo).HasColumnName("DES_FONDO");
            builder.Property(p => p.NumeroCuentaDestino).HasColumnName("NUM_CUENTA_DESTINO");
            builder.Property(p => p.CodigoModalidad).HasColumnName("COD_MODALIDAD");
            builder.Property(p => p.CodigoSubModalidad).HasColumnName("COD_SUB_MODALIDAD");
            builder.Property(p => p.NumeroAsiento).HasColumnName("NUM_ASIENTO");
            builder.Property(p => p.CodigoFormaPagoCJ).HasColumnName("COD_FORMA_PAGO_CJ");
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.FechaServidor).HasColumnName("FEC_SERVIDOR");
            builder.Property(p => p.NumeroMovimtoSistema).HasColumnName("NUM_MOVIMTO_SISTEMA");
            builder.Property(p => p.IndicadorCanal).HasColumnName("IND_CANAL");
            builder.Property(p => p.CodigoSubTipoTransaccion).HasColumnName("SUB_TIP_TRANSAC");
            builder.Property(p => p.CodigoEntidadSBS).HasColumnName("COD_ENTIDAD_SBS");
            builder.Property(p => p.IndicadorPorRegularizar).HasColumnName("IND_POR_REGULARIZAR");

            builder.HasMany(m => m.Detalle).WithOne().HasForeignKey(f => new { f.NumeroMovimientoLavado });
        }
    }
}
