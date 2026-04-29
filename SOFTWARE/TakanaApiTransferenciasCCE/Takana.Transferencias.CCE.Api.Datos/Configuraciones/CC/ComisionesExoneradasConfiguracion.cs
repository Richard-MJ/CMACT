using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio ConfiguracionComision de la tabla CC_CONFIGURACION_COMISION_TRANSACCION_ENCA
    /// </summary>
    internal class ComisionesExoneradasConfiguracion : IEntityTypeConfiguration<ComisionExonerada>
    {
        public void Configure(EntityTypeBuilder<ComisionExonerada> builder)
        {
            builder.ToTable("CC_COMISIONES_EXONERADAS", "CC");
            builder.HasKey(k => new { k.CodigoComisionExonerada });

            builder.Property(p => p.CodigoComisionExonerada).HasColumnName("ID_COMISION_EXON").ValueGeneratedOnAdd();
            builder.Property(p => p.CodigoComision).HasColumnName("COD_COMISION");
            builder.Property(p => p.NumeroMovimientoOrigen).HasColumnName("NUM_MOVIMIENTO_ORIGEN");
            builder.Property(p => p.CodigoEmpresa).HasColumnName("COD_EMPRESA");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION_ULT");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_ULT");
        }
    }
}
