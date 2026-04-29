using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;

public class AliasProductoClienteConfiguracion : IEntityTypeConfiguration<AliasProductoCliente>
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio Calendario de la tabla CF_CALENDARIOS
    /// </summary>
    public void Configure(EntityTypeBuilder<AliasProductoCliente> builder)
    {
        builder.ToTable("CF_ALIAS_PRODUCTO_CLIENTE", "CF");
        builder.HasKey(k => new { k.NumeroProducto, k.CodigoSistema});

        builder.Property(p => p.NumeroAlias).HasColumnName("NUM_ALIAS");
        builder.Property(p => p.NombreAlias).HasColumnName("NOM_ALIAS").HasMaxLength(35);
        builder.Property(p => p.NumeroProducto).HasColumnName("NUM_PRODUCTO").HasMaxLength(20);
        builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA").HasMaxLength(15);
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").HasMaxLength(1);
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO");
    }
}
