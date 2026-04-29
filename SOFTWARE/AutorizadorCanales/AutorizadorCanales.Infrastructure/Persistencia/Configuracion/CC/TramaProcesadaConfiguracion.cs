using AutorizadorCanales.Domain.Entidades.CC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CC;

public class TramaProcesadaConfiguracion : IEntityTypeConfiguration<TramaProcesada>
{
    public void Configure(EntityTypeBuilder<TramaProcesada> builder)
    {
        ConfigurarTramaProcesada(builder);
    }

    private void ConfigurarTramaProcesada(EntityTypeBuilder<TramaProcesada> builder)
    {
        builder.ToTable("CC_TRAMAS_PROCESADAS", "CC");
        builder.HasKey(c => c.Id);

        builder.Property(p => p.Id).HasColumnName("ID_TRAMA").IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.IdCanal).HasColumnName("IND_CANAL").IsRequired().HasMaxLength(5);
        builder.Property(p => p.CodigoTipoTrama).HasColumnName("TIP_TRAMA").IsRequired().HasMaxLength(4);
        builder.Property(p => p.NumeroTarjeta).HasColumnName("NUM_TARJETA").IsRequired().HasMaxLength(16);
        builder.Property(p => p.CodigoProceso).HasColumnName("COD_PROCESO").IsRequired().HasMaxLength(6);
        builder.Property(p => p.CodigoMonedaIso).HasColumnName("COD_MONEDA_ISO").IsRequired().HasMaxLength(3);
        builder.Property(p => p.CadenaMontoOperacion).HasColumnName("MON_OPER").IsRequired().HasMaxLength(12);
        builder.Property(p => p.CodigoNumeroTrace).HasColumnName("NUM_TRACE").IsRequired().HasMaxLength(6);
        builder.Property(p => p.CodigoFechaHora).HasColumnName("FEC_HORA").IsRequired().HasMaxLength(6);
        builder.Property(p => p.CodigoFechaDia).HasColumnName("FEC_DIA").IsRequired().HasMaxLength(4);
        builder.Property(p => p.CodigoBinAdquiriente).HasColumnName("BIN_ADQUI").IsRequired().HasMaxLength(11);
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired().HasMaxLength(5);
        builder.Property(p => p.IdMovimientoTts).HasColumnName("ID_MOV_TTS").IsRequired();
        builder.Property(p => p.CodigoRespuesta).HasColumnName("COD_RESP").IsRequired().HasMaxLength(2);
        builder.Property(p => p.FechaSistema).HasColumnName("FEC_SISTEMA").IsRequired();
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired();
        builder.Property(p => p.FechaModificado).HasColumnName("FEC_MODIFICADO").IsRequired();
        builder.Property(p => p.CodigoSistemaFuente).HasColumnName("COD_SISTEMA_FUENTE").IsRequired().HasMaxLength(5);

        builder.Property(p => p.IdTerminal).HasColumnName("ID_TERMINAL").IsRequired().HasMaxLength(24);
        builder.Property(p => p.NumeroProductoOrigen).HasColumnName("NRO_PRODUCTO_ORIGEN").HasMaxLength(28);
        builder.Property(p => p.NumeroProductoDestino).HasColumnName("NRO_PRODUCTO_DESTINO").HasMaxLength(28);
        builder.Property(p => p.CodigoNumeroAutorizacion).HasColumnName("NUM_AUTORIZA").IsRequired().HasMaxLength(6);
        builder.Property(p => p.CodigoMensajeUno).HasColumnName("COD_MENSAJE1").IsRequired().HasMaxLength(16);
        builder.Property(p => p.CodigoMensajeDos).HasColumnName("COD_MENSAJE2").IsRequired().HasMaxLength(20);
        builder.Property(p => p.IndicadorConciliado).HasColumnName("IND_CONCILIADO").HasMaxLength(1);
        builder.Property(p => p.NumeroAsientoCompensa).HasColumnName("NUM_ASIENTO_COMPENSA");
    }
}
