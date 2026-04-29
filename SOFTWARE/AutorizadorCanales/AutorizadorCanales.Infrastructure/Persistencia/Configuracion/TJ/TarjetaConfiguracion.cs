using AutorizadorCanales.Domain.Entidades.TJ;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.TJ;

public class TarjetaConfiguracion : IEntityTypeConfiguration<Tarjeta>
{
    public void Configure(EntityTypeBuilder<Tarjeta> builder)
    {
        ConfigurarTarjeta(builder);
    }

    private void ConfigurarTarjeta(EntityTypeBuilder<Tarjeta> builder)
    {
        builder.ToTable("TJ_TARJETAS", "TJ");

        builder.HasKey(m => new { m.NumeroTarjeta });

        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA").IsRequired()
            .HasPrecision(16,0);

        builder.Property(m => m.CodigoAgencia).HasColumnName("COD_AGENCIA");
        builder.Property(m => m.CodigoCliente).HasColumnName("COD_CLIENTE");
        builder.Property(m => m.FechaEmision).HasColumnName("FEC_EMISION");
        builder.Property(m => m.FechaVencimiento).HasColumnName("FEC_VENCIMIENTO");
        builder.Property(m => m.NumeroPvv1).HasColumnName("NUM_PVV1");
        builder.Property(m => m.NumeroPvv2).HasColumnName("NUM_PVV2");
        builder.Property(m => m.NumeroPvvHomebanking1).HasColumnName("NUM_PVV1_HB");
        builder.Property(m => m.NumeroPvvHomebanking2).HasColumnName("NUM_PVV2_HB");
        builder.Property(m => m.NumeroPvvHomebankingAnterior1).HasColumnName("NUM_PVV1_ANT_HB");
        builder.Property(m => m.NumeroPvvHomebankingAnterior2).HasColumnName("NUM_PVV2_ANT_HB");
        builder.Property(m => m.CodigoUsuarioRecepcion).HasColumnName("COD_USUARIO_RECEPCION");
        builder.Property(m => m.IndicadorAfiliadoHomeBanking).HasColumnName("IND_AFILIADO_HB");
        builder.Property(m => m.FechaAfiliacionHomeBanking).HasColumnName("FEC_AFILIADO_HB");
        builder.Property(m => m.FechaActivacion).HasColumnName("FEC_ACTIVACION");
        builder.Property(m => m.FechaUltimoCambioPassHomebanking).HasColumnName("FEC_ULT_CAMBIO_PASS_HB");
        builder.Property(m => m.CodigoMotivoAnulacion).HasColumnName("COD_MOTIVO_ANULACION");
        builder.Property(m => m.DescripcionAnulacion).HasColumnName("DES_ANULACION");
        builder.Property(m => m.FechaAnulacion).HasColumnName("FEC_ANULACION");
        builder.Property(m => m.CodigoUsuarioAnulacion).HasColumnName("COD_USUARIO_ANULACION");
        builder.Property(m => m.NumeroComprobante).HasColumnName("NUM_COMPROBANTE");
        builder.Property(m => m.CodigoUsuarioDerivacion).HasColumnName("COD_USUARIO_DERIVACION");
        builder.Property(m => m.FechaDerivacion).HasColumnName("FEC_DERIVACION");
        builder.Property(m => m.FechaRecepcion).HasColumnName("FEC_RECEPCION");

        builder.Property(m => m.CodigoLote).HasColumnName("COD_LOTE");
        builder.Property(m => m.CodigoEstadoTarjeta).HasColumnName("COD_ESTADO_TAR");
        builder.Property(m => m.TipoEstadoTarjeta).HasColumnName("TIP_ESTADO_TAR");
        builder.Property(m => m.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
        builder.Property(m => m.FechaServidor).HasColumnName("FEC_SERVIDOR");
        builder.Property(m => m.CodigoTipoTarjeta).HasColumnName("COD_TIPO_TARJETA");
        builder.Property(m => m.CodigoAgenciaDerivacion).HasColumnName("COD_AGENCIA_DERIVACION");
        builder.Property(m => m.IndicadorChip).HasColumnName("IND_CHIP");
        builder.Property(m => m.CodigoEmpresa).HasColumnName("COD_EMPRESA");
        builder.Property(m => m.IndicadorAfiliadoAppMovil).HasColumnName("IND_AFILIADO_APP");
        builder.Property(m => m.IndicadorContactless).HasColumnName("IND_CONTACTLESS");
        builder.Property(m => m.IndicadorMicroPagos).HasColumnName("IND_MICROPAGOS").IsRequired();

        builder.HasOne(t => t.Duenio)
            .WithMany()
            .HasForeignKey(t => new { t.CodigoEmpresa, t.CodigoCliente })
            .IsRequired(false);

        builder.HasMany(a => a.AfiliacionesCanalElectronico)
            .WithOne(e=> e.Tarjeta)
            .HasForeignKey(a => a.NumeroTarjeta)
            .IsRequired();
    }
}
