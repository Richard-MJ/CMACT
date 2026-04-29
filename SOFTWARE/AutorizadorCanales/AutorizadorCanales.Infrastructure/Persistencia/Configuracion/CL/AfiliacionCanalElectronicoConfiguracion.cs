using AutorizadorCanales.Domain.Entidades.CL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;

public class AfiliacionCanalElectronicoConfiguracion : IEntityTypeConfiguration<AfiliacionCanalElectronico>
{
    public void Configure(EntityTypeBuilder<AfiliacionCanalElectronico> builder)
    {
        ConfigurarAfiliacionCanalElectronico(builder);
    }

    private static void ConfigurarAfiliacionCanalElectronico(EntityTypeBuilder<AfiliacionCanalElectronico> builder)
    {
        builder.ToTable("CL_AFILIACIONES_CANALES_ELECTRONICOS", "CL");
        builder.HasKey(m => new { m.IdAfiliacionCanalElectronico });

        builder.Property(m => m.IdAfiliacionCanalElectronico)
            .HasColumnName("ID_AFILIACIONES_CAN_ELEC")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(m => m.IdVerificacion).HasColumnName("ID_VERIFICACION");
        builder.Property(m => m.ObservacionAfiliaciones).HasColumnName("OBSERVACION_AFILIACIONES");
        builder.Property(m => m.IdDispositivoAutenticacion).HasColumnName("ID_TERMINAL_AUTH");
        builder.Property(m => m.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
        builder.Property(m => m.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_MODIFICACION");

        builder.Property(m => m.NumeroTarjeta).HasColumnName("NUM_TARJETA").HasPrecision(16, 0).IsRequired();
        builder.Property(m => m.FechaAfiliacionPrincipal).HasColumnName("FEC_AFILIACION_PRINCIPAL");
        builder.Property(m => m.IndicadorAfiliacionPrincipal).HasColumnName("IND_AFILIACION_PRINCIPAL");
        builder.Property(m => m.FechaConfirmacionAfiliacion).HasColumnName("FEC_CONFIRMACION_AFILIACION").IsRequired().HasColumnType("datetime");
        builder.Property(m => m.IndicadorConfirmacionAfiliacion).HasColumnName("IND_CONFIRMACION_AFILIACION");
        builder.Property(m => m.FechaAfiliacionSms).HasColumnName("FEC_AFILIACION_SMS").IsRequired().HasColumnType("datetime");
        builder.Property(m => m.IndicadorAfiliacionSms).HasColumnName("IND_AFILIACION_SMS");
        builder.Property(m => m.FechaConfirmacionSms).HasColumnName("FEC_CONFIRMACION_SMS").IsRequired().HasColumnType("datetime");
        builder.Property(m => m.IndicadorConfirmacionSms).HasColumnName("IND_CONFIRMACION_SMS");
        builder.Property(m => m.FechaCambioClaveTarjeta).HasColumnName("FEC_CAMBIO_CLAVE_TARJETA").IsRequired().HasColumnType("datetime");
        builder.Property(m => m.IndicadorCambioClaveTarjeta).HasColumnName("IND_CAMBIO_CLAVE_TARJETA");
        builder.Property(m => m.FechaCambioClaveInternet).HasColumnName("FEC_CAMBIO_CLAVE_INTERNET").IsRequired().HasColumnType("datetime");
        builder.Property(m => m.IndicadorCambioClaveInternet).HasColumnName("IND_CAMBIO_CLAVE_INTERNET");
        builder.Property(m => m.NumeroIntentosClaveInternet).HasColumnName("NUM_INTENTOS_CLAVE_INTERNET");
        builder.Property(m => m.FechaCaducidadClaveInternet).HasColumnName("FEC_CADUCIDAD_AFIL_PRINCIPAL");
        builder.Property(m => m.IdApiUsuario).HasColumnName("ID_API_USUARIO");
        builder.Property(m => m.IndicadorActivo).HasColumnName("IND_ACTIVO");
        builder.Property(m => m.IndicadorVencido).HasColumnName("IND_VENCIDO");
        builder.Property(m => m.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired();
        builder.Property(m => m.FechaModificacion).HasColumnName("FEC_MODIFICACION").IsRequired();
        builder.Property(m => m.ModeloDispositivo).HasColumnName("MODELO_DISPOSITIVO").IsRequired();
        builder.Property(m => m.DireccionIp).HasColumnName("DIRECCION_IP").IsRequired();
        builder.Property(m => m.Navegador).HasColumnName("NAVEGADOR").IsRequired();
        builder.Property(m => m.SistemaOperativo).HasColumnName("SIST_OPERATIVO").IsRequired();
    }
}
