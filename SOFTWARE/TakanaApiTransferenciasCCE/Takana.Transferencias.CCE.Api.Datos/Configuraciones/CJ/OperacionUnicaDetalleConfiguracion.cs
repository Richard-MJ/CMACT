using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ
{    /// <summary>
     /// Clase que representa el mapeo de la clase de Dominio OperacionUnicaDetalle de la tabla CJ_OPERACION_UNICA_DETALLE
     /// </summary>
    public class OperacionUnicaDetalleConfiguracion : IEntityTypeConfiguration<OperacionUnicaDetalle>
    {
        public void Configure(EntityTypeBuilder<OperacionUnicaDetalle> builder)
        {
            builder.ToTable("CJ_OPERACION_UNICA_DETALLE", "CJ");
            builder.HasKey(k => new { k.NumeroSecuencia });

            builder.Property(p => p.NumeroSecuencia).HasColumnName("NUM_SECUENCIA");
            builder.Property(p => p.NumeroMovimientoLavado).HasColumnName("NUM_MOVIMIENTO_LAV");
            builder.Property(p => p.TipoCliente).HasColumnName("TIP_CLIENTE");
            builder.Property(p => p.ApellidoPaterno).HasColumnName("APELLIDO_PATERNO");
            builder.Property(p => p.ApellidoMaterno).HasColumnName("APELLIDO_MATERNO");
            builder.Property(p => p.Nombres).HasColumnName("NOMBRES");
            builder.Property(p => p.TipoInterviniente).HasColumnName("TIPO_INTERVINIENTE");
            builder.Property(p => p.CodigoCliente).HasColumnName("COD_CLIENTE");
            builder.Property(p => p.TipoDocumento).HasColumnName("TIP_DOC");
            builder.Property(p => p.NumeroDocumento).HasColumnName("NUM_DOC");
            builder.Property(p => p.TipoPersona).HasColumnName("TIP_PERSONA");
            builder.Property(p => p.NumeroRuc).HasColumnName("NUM_RUC");
            builder.Property(p => p.FechaNacimiento).HasColumnName("FEC_NACIMIENTO");
            builder.Property(p => p.Nacionalidad).HasColumnName("NACIONALIDAD");
            builder.Property(p => p.CodigoResidencia).HasColumnName("COD_RESIDENCIA");
            builder.Property(p => p.DetalleDireccion).HasColumnName("DET_DIRECCION");
            builder.Property(p => p.Telefono).HasColumnName("TELEFONO");
            builder.Property(p => p.CodigoOcupacion).HasColumnName("COD_OCUPACION");
            builder.Property(p => p.CodigoActividad).HasColumnName("COD_ACTIVIDAD");
            builder.Property(p => p.CodigoSubactividad).HasColumnName("COD_SUBACTIV");
            builder.Property(p => p.CodigoCargo).HasColumnName("COD_CARGO");
            builder.Property(p => p.CodigoDepartamento).HasColumnName("COD_DEPA");
            builder.Property(p => p.CodigoProvincia).HasColumnName("COD_PROV");
            builder.Property(p => p.CodigoDistrito).HasColumnName("COD_DIST");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaServidor).HasColumnName("FEC_SERVIDOR");
            builder.Property(p => p.CodigoPaisResidencia).HasColumnName("COD_PAIS_RESIDENCIA");
        }
    }
}
