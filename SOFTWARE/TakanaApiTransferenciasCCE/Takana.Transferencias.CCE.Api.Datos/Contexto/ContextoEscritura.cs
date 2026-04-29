using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Contexto
{
    /// <summary>
    /// Contexto Genera para las interacciones con la Bitacora de la CCE
    /// </summary>
    public class ContextoEscritura : ContextoBase<ContextoEscritura>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bitacora"></param>
        /// <param name="baseDatosConexion"></param>
        public ContextoEscritura(
            IBitacora<ContextoEscritura> bitacora,
            IConfiguracionBaseDatosSAF baseDatosConexion) : base(bitacora, baseDatosConexion)
        {
        }

        /// <summary>
        /// Metodo encargado de crear los modelos 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Configuracion - CC            
            modelBuilder.ApplyConfiguration(new BitacoraTransferenciaInmediataConfiguracion());
            #endregion Configuracion - CC

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).ToTable(tb => tb.UseSqlOutputClause(false));
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
