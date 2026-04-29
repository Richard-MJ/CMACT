using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Loggin.Interfaz;
using Takana.Transferencias.CCE.Api.Datos.Interfaces;

namespace Takana.Transferencias.CCE.Api.Datos.Contexto
{
    /// <summary>
    /// Clase base para los contextos 
    /// </summary>
    public class ContextoBase<TContexto> : DbContext, IContextoBase where TContexto : class
    {
        /// <summary>
        /// Instancia de la interfaz IBitacora
        /// </summary>
        private readonly IBitacora<TContexto> _bitacora; 
        /// <summary>
        /// Instacia de la base de datos conexión
        /// </summary>
        private readonly IConfiguracionBaseDatosSAF _baseDatosConexion;
        /// <summary>
        /// Constructor estatico, establece la inicialización de los elementos de la base de datos.
        /// </summary>
        public ContextoBase(
            IBitacora<TContexto> bitacora,
            IConfiguracionBaseDatosSAF baseDatosConexion)
        {
            _bitacora = bitacora;
            _baseDatosConexion = baseDatosConexion;
        }

        /// <summary>
        /// Configuracion de la cadena de conexion
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ObtenerCadenaConexion())
                .UseLazyLoadingProxies();
            optionsBuilder.EnableDetailedErrors(true);
        }

        /// <summary>
        /// Método para establecer la conexion con la base de datos
        /// </summary>
        /// <returns>Datos de la conexion</returns>
        private string ObtenerCadenaConexion()
        {
            var datosConexion = new SqlConnectionStringBuilder
            {
                DataSource = _baseDatosConexion.Servidor,
                InitialCatalog = _baseDatosConexion.Catalogo,
                ApplicationName = ConfigApi.CodigoBase,
                UserID = _baseDatosConexion.Usuario,
                Password = _baseDatosConexion.Password,
                IntegratedSecurity = _baseDatosConexion.SeguridadIntegrada,
                TrustServerCertificate = _baseDatosConexion.Certificado,
            };

            return datosConexion.ConnectionString;
        }

        /// <summary>
        /// Método que obtiene el conjunto de datos de una entidad.
        /// </summary>
        /// <typeparam name="T">Tipo de conjunto.</typeparam>
        /// <returns>Conjunto de datos de una entidad</returns>
        public DbSet<T> Establecer<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Método para guardar los cambios a la base de datos.
        /// </summary>
        public void GuardarCambios()
        {
            try
            {
                if (!ChangeTracker.HasChanges())
                    return;

                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entidadesInvolucradas = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + (entidad.Entity.GetType() + ","));

                _bitacora.Error(
                    "Error al guardar cambios en BBDD. {mensajeExcepcion}. Entidad(es): {entidadesInvolucradas}",
                    ex.Message, entidadesInvolucradas);

                throw new Exception("Error al guardar cambios en BBDD. Problemas de concurrencia.", ex);
            }
            catch (DbUpdateException ex)
            {
                var mensaje = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + ("Entidad de tipo " + entidad.Entity.GetType().Name +
                        " en estado " + entidad.State +
                        " tiene los siguientes errores de validación: "));

                throw new Exception("Error al guardar cambios en BBDD. " + mensaje, ex);
            }
        }
        
        /// <summary>
        /// Método para guardar los cambios a la base de datos.
        /// </summary>
        public async Task GuardarCambiosAsync()
        {
            try
            {
                if (!ChangeTracker.HasChanges())
                    return;

                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entidadesInvolucradas = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + (entidad.Entity.GetType() + ","));

                _bitacora.Error(
                    "Error al guardar cambios en BBDD. {mensajeExcepcion}. Entidad(es): {entidadesInvolucradas}",
                    ex.Message, entidadesInvolucradas);

                throw new Exception("Error al guardar cambios en BBDD. Problemas de concurrencia.", ex);
            }
            catch (DbUpdateException ex)
            {
                var mensaje = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + ("Entidad de tipo " + entidad.Entity.GetType().Name +
                        " en estado " + entidad.State +
                        " tiene los siguientes errores de validación: "));

                throw new Exception("Error al guardar cambios en BBDD. " + mensaje, ex);
            }
        }
    }
}
