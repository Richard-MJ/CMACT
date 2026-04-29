using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio MovimientoDiario de la tabla CC_MOVIMTO_DIARIO
    /// </summary>
    public class MovimientoInfoAdicionalConfiguracion : IEntityTypeConfiguration<MovimientoInfoAdicional>
    {
        public void Configure(EntityTypeBuilder<MovimientoInfoAdicional> builder)
        {
            builder.ToTable("CC_MOV_DIARIO_TTS", "CC");
            builder.HasKey(k => k.NumeroSecuencia);

            builder.Property(m => m.NumeroMovimiento).HasColumnName("NUM_MOVIMIENTO").IsRequired();
            builder.Property(m => m.NumeroSecuencia).HasColumnName("ID_MOVIMIENTO").IsRequired();
            builder.Property(m => m.CodigoCanal).HasColumnName("IND_CANAL").IsRequired().HasColumnType("char");
            builder.Property(m => m.NumeroTTS).HasColumnName("NUM_TRANS_TTS").IsRequired();
            builder.Property(m => m.NumeroCuenta).HasColumnName("NUM_CUENTA").IsRequired().HasMaxLength(15);
            builder.Property(m => m.PeriodoTransitoTTS).HasColumnName("PER_TRANS_TTS").IsRequired();
            builder.Property(m => m.ObservacionesTTS).HasColumnName("OBS_TRANSAC_TTS").HasMaxLength(250);
            builder.Property(m => m.FechaTransaccion).HasColumnName("FEC_TRANSACCION");
            builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA").HasMaxLength(20);
            builder.Property(m => m.CodigoSubCanal).HasColumnName("SUB_CANAL").HasColumnType("tinyint");
            builder.Property(m => m.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired().HasMaxLength(15);
            builder.Property(m => m.CodigoPaisOrigen).HasColumnName("COD_PAIS_ORIGEN").HasMaxLength(5);
            builder.Property(m => m.IdTransaccionExterno).HasColumnName("ID_TRANS_GN").HasMaxLength(16);
            builder.Property(m => m.IdTerminal).HasColumnName("ID_TERMINAL_ATM").HasMaxLength(16);
            builder.Property(m => m.CodigoSubTransaccion).HasColumnName("TIP_SUBTRANS").HasMaxLength(5);
            builder.Property(m => m.CodigoMotivoReversion).HasColumnName("COD_MOTIVO_REV_ATM").HasMaxLength(5);
        }
    }
}