using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase de mapea la entidad de tipo de cuenta grupo de la tabla CC_IND_TIP_CTA_GRUPO
    /// </summary>
    public class OperacionFrecuenteConfiguracion : IEntityTypeConfiguration<OperacionFrecuente>
    {
        public void Configure(EntityTypeBuilder<OperacionFrecuente> builder)
        {
            builder.ToTable("CC_OPERACION_FRECUENTE", "CC");
            builder.HasKey(m =>  m.NumeroOperacionFrecuente);

            builder.Property(o => o.NumeroOperacionFrecuente).HasColumnName("NUM_OPE_FRECUENTE").IsRequired();
            builder.Property(o => o.CodigoEmpresa).HasColumnName("COD_EMPRESA").IsRequired();
            builder.Property(o => o.NumeroTipoOperacionFrecuente).HasColumnName("NUM_TIPO_OPE_FREC").IsRequired();
            builder.Property(o => o.NumeroCuenta).HasColumnName("NUM_CUENTA").IsRequired();
            builder.Property(o => o.CodigoSistema).HasColumnName("COD_SISTEMA").IsRequired();
            builder.Property(o => o.NombreOperacionFrecuente).HasColumnName("NOM_OPE_FRECUENTE").IsRequired();
            builder.Property(o => o.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired();
            builder.Property(o => o.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired();
            builder.Property(o => o.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired();

            builder.HasMany(o => o.OperacionesFrecuenteDetalle).WithOne().HasForeignKey(o => o.NumeroOperacionFrecuente);
        }
    }
}
