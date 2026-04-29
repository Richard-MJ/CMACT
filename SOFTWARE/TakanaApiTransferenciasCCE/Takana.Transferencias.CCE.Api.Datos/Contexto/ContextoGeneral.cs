using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CG;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.SG;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA;

namespace Takana.Transferencias.CCE.Api.Datos.Contexto
{
    /// <summary>
    /// Contexto Genera para las interacciones con la CCE
    /// </summary>
    public class ContextoGeneral : ContextoBase<ContextoGeneral>
    {
        /// <summary>
        /// Datos de paraametros por empresa
        /// </summary>
        public DbSet<ParametroPorEmpresa> ParametrosPorEmpresa { get; set; }
        /// <summary>
        /// Datos de la moneda
        /// </summary>
        public DbSet<Moneda> Monedas { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="baseDatosConexion"></param>
        public ContextoGeneral(
            IBitacora<ContextoGeneral> bitacora,
            IConfiguracionBaseDatosSAF baseDatosConexion) : base(bitacora, baseDatosConexion)
        {
        }

        /// <summary>
        /// Metodo encargado de crear los modelos 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            modelBuilder.ApplyConfiguration(new SistemaClienteConfiguracion());

            #region Configuración - BA

            modelBuilder.ApplyConfiguration(new EntidadFinancieraInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new EntidadFinancieroInmediataPeriodoConfiguracion());

            #endregion Configuración - BA

            #region Configuración - CF
            modelBuilder.ApplyConfiguration(new CatalogoTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new SubTipoTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new MonedaConfiguracion());
            modelBuilder.ApplyConfiguration(new AgenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new CalendarioConfiguracion());
            modelBuilder.ApplyConfiguration(new CanalConfiguracion());
            modelBuilder.ApplyConfiguration(new CanalPorSubTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new CodigoRespuestaConfiguracion());
            modelBuilder.ApplyConfiguration(new EstadoInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new TiposTransferenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroPorEmpresaConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroPorAgenciaConfiguracion());           
            modelBuilder.ApplyConfiguration(new SeriesPorEmpresaConfiguracion());
            modelBuilder.ApplyConfiguration(new EntidadFinancieraPorEstadoSignConfiguracion());
            modelBuilder.ApplyConfiguration(new LimiteTransferenciaInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroGeneralTransferenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new PeriodoConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoReporteConfiguracion());
            #endregion Configuración - CF

            #region Configuracion - PA
            modelBuilder.ApplyConfiguration(new TipoCambioActualConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoCambioHistoricoConfiguracion());
            modelBuilder.ApplyConfiguration(new CuentaContableConfiguracion());
            #endregion

            #region Configuracion - CL
            modelBuilder.ApplyConfiguration(new TipoDocumentoConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliacionInteroperabilidadDetalleConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliacionInteroperabilidadConfiguracion());
            #endregion Configuracion - CL

            #region Configuracion - CC            
            modelBuilder.ApplyConfiguration(new TipoTramaConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraTransferenciaInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new MensajeNotificacionTransferenciaInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraInteroperabilidadAfiliacionConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraInteroperabilidadConfiguracion());
            modelBuilder.ApplyConfiguration(new DirectorioInteroperabilidadConfiguracion());
            modelBuilder.ApplyConfiguration(new ReporteConfiguracion());
            #endregion Configuracion - CC

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).ToTable(tb => tb.UseSqlOutputClause(false));
            }

            base.OnModelCreating(modelBuilder);
        }

        public Task<int> EjecutarComandoSQL(string comandoSql, params object[] parametros)
        {
            return this.Database.ExecuteSqlRawAsync(comandoSql, parametros);
        }
    }
}
