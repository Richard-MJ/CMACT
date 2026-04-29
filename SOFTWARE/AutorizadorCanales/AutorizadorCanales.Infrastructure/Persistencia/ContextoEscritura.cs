using AutorizadorCanales.Domain.Entidades.TJ;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CC;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.TJ;
using Microsoft.EntityFrameworkCore;

namespace AutorizadorCanales.Infrastructure.Persistencia;

public class ContextoEscritura : DbContext
{
    public DbSet<Tarjeta> Tarjeta { get; set; }

    public ContextoEscritura(DbContextOptions<ContextoEscritura> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region CC
        modelBuilder.ApplyConfiguration(new TramaProcesadaConfiguracion());
        #endregion

        #region CF
        modelBuilder.ApplyConfiguration(new AgenciaConfiguracion());
        modelBuilder.ApplyConfiguration(new CanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new OperacionNotificadaConfiguracion());
        #endregion

        #region CL
        modelBuilder.ApplyConfiguration(new DispositivoCanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new AfiliacionCanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new TipoDocumentoConfiguracion());
        modelBuilder.ApplyConfiguration(new PersonaFisicaConfiguracion());
        modelBuilder.ApplyConfiguration(new PersonaJuridicaConfiguracion());
        modelBuilder.ApplyConfiguration(new ClienteConfiguracion());
        modelBuilder.ApplyConfiguration(new ClienteApiConfiguracion());
        modelBuilder.ApplyConfiguration(new DocumentoClienteConfiguracion());
        #endregion

        #region SG
        modelBuilder.ApplyConfiguration(new AudienciaConfiguracion());
        modelBuilder.ApplyConfiguration(new UsuarioRolConfiguracion());
        modelBuilder.ApplyConfiguration(new AccesosPorSistemaConfiguracion());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        modelBuilder.ApplyConfiguration(new TokenRefrescoConfiguracion());
        modelBuilder.ApplyConfiguration(new AlertaInicioSesionConfiguracion());
        modelBuilder.ApplyConfiguration(new SesionCanalElectronicoConfiguracion());
        #endregion

        #region TJ
        modelBuilder.ApplyConfiguration(new TarjetaConfiguracion());
        modelBuilder.ApplyConfiguration(new TarjetaHomebankingEmpresarialConfiguracion());
        modelBuilder.ApplyConfiguration(new TarjetaMovimientoConfiguracion());
        #endregion TJ

        modelBuilder.ApplyHasTrigger();
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<T> Establecer<T>() where T : class
    {
        return this.Set<T>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        base.OnConfiguring(optionsBuilder);
    }
}
