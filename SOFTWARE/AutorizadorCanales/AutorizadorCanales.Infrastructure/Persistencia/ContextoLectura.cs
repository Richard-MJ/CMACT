using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CC;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CF;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.CL;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.SG;
using AutorizadorCanales.Infrastructure.Persistencia.Configuracion.TJ;
using Microsoft.EntityFrameworkCore;

namespace AutorizadorCanales.Infrastructure.Persistencia;

public class ContextoLectura : DbContext
{
    public ContextoLectura(DbContextOptions<ContextoLectura> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region CC
        modelBuilder.ApplyConfiguration(new TramaProcesadaConfiguracion());
        #endregion

        #region CF
        modelBuilder.ApplyConfiguration(new AgenciaConfiguracion());
        modelBuilder.ApplyConfiguration(new CalendarioConfiguracion());
        modelBuilder.ApplyConfiguration(new CanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new ParametroCanalElectronicoGeneralConfiguracion());
        modelBuilder.ApplyConfiguration(new ParametroCanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new SubTipoTransaccionConfiguracion());
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
        modelBuilder.ApplyConfiguration(new TipoOperacionCanalElectronicoConfiguracion());
        modelBuilder.ApplyConfiguration(new TipoBiometriaConfiguracion());
        modelBuilder.ApplyConfiguration(new AfiliacionBiometricaConfiguracion());
        #endregion

        #region SG
        modelBuilder.ApplyConfiguration(new AudienciaConfiguracion());
        modelBuilder.ApplyConfiguration(new UsuarioRolConfiguracion());
        modelBuilder.ApplyConfiguration(new AccesosPorSistemaConfiguracion());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        modelBuilder.ApplyConfiguration(new TokenRefrescoConfiguracion());
        modelBuilder.ApplyConfiguration(new AlertaInicioSesionConfiguracion());
        modelBuilder.ApplyConfiguration(new AfiliacionTokenDigitalConfiguracion());
        modelBuilder.ApplyConfiguration(new SesionCanalElectronicoConfiguracion());
        #endregion

        #region TJ
        modelBuilder.ApplyConfiguration(new TarjetaConfiguracion());
        modelBuilder.ApplyConfiguration(new TarjetaHomebankingEmpresarialConfiguracion());
        modelBuilder.ApplyConfiguration(new TarjetaMovimientoConfiguracion());
        #endregion TJ

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

    public Task<int> EjecutarComandoSQL(string comandoSql, params object[] parametros)
    {
        return this.Database.ExecuteSqlRawAsync(comandoSql, parametros);
    }
}
