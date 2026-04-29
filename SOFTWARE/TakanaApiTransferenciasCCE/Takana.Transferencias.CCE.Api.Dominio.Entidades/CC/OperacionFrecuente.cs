using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio de tipo de cuenta grupo
    /// </summary>
    public class OperacionFrecuente
    {
        /// <summary>
        /// Identificador numerico de la operación frecuente
        /// </summary>
        public int NumeroOperacionFrecuente { get; private set; }
        /// <summary>
        /// Identificador numerico del tipo de operación frecuente
        /// </summary>
        public int NumeroTipoOperacionFrecuente { get; private set; }
        /// <summary>
        /// Número de la cuenta de origen
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Número de la cuenta de origen
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Codigo del sistema al que pertenece
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Nombre que toma la operacion frecuente
        /// </summary>
        public string NombreOperacionFrecuente { get; private set; }
        /// <summary>
        /// Pista de auditoria que indica el estado
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Pista de auditoria que indica el codigo del usuario
        /// </summary>
        public string CodigoUsuario { get; private set; }
        /// <summary>
        /// Pista de auditoria que indica la fecha de registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Colección que contiene el detalle de la operaciones frecuentes
        /// </summary>
        public virtual ICollection<OperacionFrecuenteDetalle> OperacionesFrecuenteDetalle { get; set; }

        /// <summary>
        /// Metodo estatico que crea una entidad
        /// </summary>
        public static OperacionFrecuente RegistrarOperacionFrecuente(
            string numeroCuenta, 
            string nombreOperacionFrecuente,
            string codigoUsuario, 
            DateTime fechaRegistro, 
            int tipoOperacion, 
            List<OperacionFrecuenteDetalle> detalle)
        {

            return new OperacionFrecuente
            {
                CodigoSistema = Sistema.CuentaEfectivo,
                NumeroTipoOperacionFrecuente = tipoOperacion,
                NumeroCuenta = numeroCuenta,
                NombreOperacionFrecuente = nombreOperacionFrecuente,
                IndicadorEstado = General.Activo,
                CodigoUsuario = codigoUsuario,
                FechaRegistro = fechaRegistro,
                OperacionesFrecuenteDetalle = detalle,
                CodigoEmpresa = Empresa.CodigoPrincipal
            };
        }
    }
}
