using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA
{
    public class EntidadFinancieroInmediataPeriodoConfiguracion : IEntityTypeConfiguration<EntidadFinancieroInmediataPeriodo>
    {
        public void Configure(EntityTypeBuilder<EntidadFinancieroInmediataPeriodo> builder)
        {
            builder.ToTable("BA_ENTIDAD_FINANCIERA_CCE_INMEDIATA_PERIODO", "BA");
            builder.HasKey(m => new { m.NumeroPeriodo });

            builder.Property(p => p.NumeroPeriodo).HasColumnName("NUM_PERIODO");
            builder.Property(p => p.IdentificadorEntidad).HasColumnName("ID_ENTIDAD");
            builder.Property(p => p.CodigoEntidad).HasColumnName("COD_ENTIDAD");
            builder.Property(p => p.DescripcionPeriodo).HasColumnName("DES_PERIODO");
            builder.Property(p => p.HoraSingOff).HasColumnName("HOR_SING_OFF");
            builder.Property(p => p.HoraSingOn).HasColumnName("HOR_SING_ON");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.CodigoUsuarioModificacion).HasColumnName("COD_USUARIO_MOD");
            builder.Property(p => p.FechaModificacion).HasColumnName("FEC_MODIFICACION");
        }
    }
}
