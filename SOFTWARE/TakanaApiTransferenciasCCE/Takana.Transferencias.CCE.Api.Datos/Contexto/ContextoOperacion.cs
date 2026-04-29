using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CF;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CL;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.SG;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.PA;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CG;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CJ;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.BA;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.TJ;

namespace Takana.Transferencias.CCE.Api.Datos.Contexto
{
    /// <summary>
    /// Clase de contexto de operacion para la operaciones internas de la caja
    /// </summary>
    public class ContextoOperacion : ContextoBase<ContextoOperacion>
    {
        public ContextoOperacion(
            IBitacora<ContextoOperacion> bitacora,
            IConfiguracionBaseDatosSAF baseDatosConexion) : base(bitacora, baseDatosConexion)
        {
        }
        /// <summary>
        /// Metodo encargado de crear los modelos del contexto
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuración - SG
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            #endregion Fin Configuración - SG

            #region Configuración - BA
            modelBuilder.ApplyConfiguration(new EntidadFinancieraInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new EntidadFinancieroInmediataPeriodoConfiguracion());
            modelBuilder.ApplyConfiguration(new EntidadFinancieraDiferidaConfiguracion());
            modelBuilder.ApplyConfiguration(new OficinaCCEConfiguracion());
            modelBuilder.ApplyConfiguration(new PlazaCCEConfiguracion());
            #endregion Fin Configuración - BA

            #region Configuracion - CF
            modelBuilder.ApplyConfiguration(new ParametroCanalElectronicoConfiguracion());
            modelBuilder.ApplyConfiguration(new AliasProductoClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new CatalogoTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new GrupoProductoConfiguracion());
            modelBuilder.ApplyConfiguration(new SubTipoTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new MonedaConfiguracion());
            modelBuilder.ApplyConfiguration(new CanalConfiguracion());
            modelBuilder.ApplyConfiguration(new CanalPorSubTransaccionConfiguracion());
            modelBuilder.ApplyConfiguration(new AgenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new CalendarioConfiguracion());
            modelBuilder.ApplyConfiguration(new EstadoInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroGeneralTransferenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroPorEmpresaConfiguracion());
            modelBuilder.ApplyConfiguration(new ParametroPorAgenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new MotivoMovimientoConfiguracion()); 
            modelBuilder.ApplyConfiguration(new VinculoMovimientoConfiguracion()); 
            modelBuilder.ApplyConfiguration(new ComisionCCEConfiguracion());
            modelBuilder.ApplyConfiguration(new TiposTransferenciaConfiguracion()); 
            modelBuilder.ApplyConfiguration(new LimiteTransferenciaInmediataConfiguracion()); 
            modelBuilder.ApplyConfiguration(new TipoOperacionCanalOrigenConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionNotificadaConfiguracion());
            #endregion

            #region Configuracion - CL
            modelBuilder.ApplyConfiguration(new ClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new PersonaFisicaConfiguracion());
            modelBuilder.ApplyConfiguration(new PersonaJuridicaConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoExoneradoConfiguracion());
            modelBuilder.ApplyConfiguration(new DocumentoClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new DireccionClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoDocumentoConfiguracion());
            modelBuilder.ApplyConfiguration(new TipoDireccionConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliadoConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliadoServicioConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliacionInteroperabilidadDetalleConfiguracion());
            modelBuilder.ApplyConfiguration(new AfiliacionInteroperabilidadConfiguracion());
            #endregion Fin ClieConfiguracionnte - CL

            #region Configuracion - CG
            modelBuilder.ApplyConfiguration(new CuentaContableConfiguracion());
            modelBuilder.ApplyConfiguration(new AsientoContableConfiguracion());
            modelBuilder.ApplyConfiguration(new AsientoContableDetalleConfiguracion());
            #endregion Fin Configuracion - CG

            #region Configuracion - CC
            modelBuilder.ApplyConfiguration(new TramaProcesadaConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionFrecuenteDetalleConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionFrecuenteConfiguracion());
            modelBuilder.ApplyConfiguration(new ArchivoMovimientoConciliacionConfiguracion());
            modelBuilder.ApplyConfiguration(new BitacoraTransferenciaInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new TransferenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new EstadoCuentaConfiguracion());            
            modelBuilder.ApplyConfiguration(new TransferenciaDetalleEntranteConfiguracion());
            modelBuilder.ApplyConfiguration(new TransferenciaDetalleSalienteConfiguracion());
            modelBuilder.ApplyConfiguration(new TransaccionOrdenTransferenciaInmediataConfiguracion());
            modelBuilder.ApplyConfiguration(new CuentaEfectivoSueldoConfiguracion());
            modelBuilder.ApplyConfiguration(new CuentaEfectivoConfiguracion());
            modelBuilder.ApplyConfiguration(new MovimientoInfoAdicionalConfiguracion());
            modelBuilder.ApplyConfiguration(new MovimientoDiarioConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());            
            modelBuilder.ApplyConfiguration(new ProductoCuentasCaracteristicasConfiguracion());
            modelBuilder.ApplyConfiguration(new FirmaClienteConfiguracion());
            modelBuilder.ApplyConfiguration(new LimitesOperacionesCuentaConfiguracion());    
            modelBuilder.ApplyConfiguration(new ConceptoCobroCCEConfiguracion()); 
            modelBuilder.ApplyConfiguration(new ConfiguracionComisionAgenciaConfiguracion());
            modelBuilder.ApplyConfiguration(new ConfiguracionComisionProductoConfiguracion());
            modelBuilder.ApplyConfiguration(new ConfiguracionComisionMonedaConfiguracion());           
            modelBuilder.ApplyConfiguration(new ConfiguracionComisionConfiguracion());
            modelBuilder.ApplyConfiguration(new ComisionesExoneradasConfiguracion());            
            modelBuilder.ApplyConfiguration(new ComisionesExoneradasVistaConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionesVicnculosMotivosConfiguracion()); 
            modelBuilder.ApplyConfiguration(new TransferenciaDetalleCCEConfiguracion());
            modelBuilder.ApplyConfiguration(new CuentaAfiliadaConfiguracion());
            modelBuilder.ApplyConfiguration(new CuentaAfiliadaHistoricaConfiguracion()); 
            modelBuilder.ApplyConfiguration(new TipoCuentaGrupoConfiguracion());
            modelBuilder.ApplyConfiguration(new DirectorioInteroperabilidadConfiguracion());
            #endregion Fin Configuracion - CC

            #region Configuracion - CJ
            modelBuilder.ApplyConfiguration(new MenorCuantiaEncabezadoConfiguracion());
            modelBuilder.ApplyConfiguration(new MenorCuantiaDetalleConfiguracion());
            modelBuilder.ApplyConfiguration(new MenorCuantiaIntervinienteConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionUnicaLavadoConfiguracion());
            modelBuilder.ApplyConfiguration(new OperacionUnicaDetalleConfiguracion());
            modelBuilder.ApplyConfiguration(new UmbralOperacionLavadoConfiguracion());
            #endregion Fin Configuracion - CJ

            #region Configuracion - PA
            modelBuilder.ApplyConfiguration(new ProvinciaConfiguracion());
            modelBuilder.ApplyConfiguration(new CantonesConfiguracion());
            modelBuilder.ApplyConfiguration(new NacionConfiguracion());
            #endregion Fin Configuracion - PA

            #region Configuracion - TJ
            modelBuilder.ApplyConfiguration(new TarjetaConfiguracion());
            modelBuilder.ApplyConfiguration(new TarjetaMovimientoConfiguracion());
            #endregion  Configuracion - TJ

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).ToTable(tb => tb.UseSqlOutputClause(false));
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
