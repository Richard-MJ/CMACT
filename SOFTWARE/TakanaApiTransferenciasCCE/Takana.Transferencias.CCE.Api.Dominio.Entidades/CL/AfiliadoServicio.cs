using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL
{
    /// <summary>
    /// Clase de dominio encargada de la afiliacion de un servicio
    /// </summary>
    public class AfiliadoServicio
    {
        /// <summary>
        /// Indicador de  desafiliado
        /// </summary>
        public const string IndicadorDesafiliado = "D";
        /// <summary>
        /// Numero de afiliado
        /// </summary>
        public int NumeroAfiliado { get; private set; }
        /// <summary>
        /// Codigo de servicio
        /// </summary>
        public short CodigoServicio { get; private set; }
        /// <summary>
        /// Indicador de estado de la afiliacion
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Fecha de la afiliacion
        /// </summary>
        public DateTime? FechaDesafiliacion { get; private set; }
        /// <summary>
        /// Codigo de agencia de la afiliacion
        /// </summary>
        public string? CodigoAgenciaDesafiliacion { get; private set; }
        /// <summary>
        /// Codigo de usuario de la afiliacion
        /// </summary>
        public string? CodigoUsuarioDesafiliacion { get; private set; }
        /// <summary>
        /// Fecha en la que se realiza la modificación de la afiliación
        /// </summary>
        public DateTime? FechaModificacion { get; private set; }

        /// <summary>
        /// Fecha en la que se realiza la afiliación
        /// </summary>
        public DateTime? FechaAfiliacion { get; private set; }
        /// <summary>
        /// Datos de la cuenta afialda
        /// </summary>
        virtual public ICollection<CuentaAfiliada> CuentasAfiliadas { get; private set; }
        /// <summary>
        /// Afiliado
        /// </summary>
        private Afiliado _afiliado;
        /// <summary>
        /// Entidad de afiliado
        /// </summary>
        virtual public Afiliado Afiliado
        {
            get
            {
                return _afiliado;
            }
            set
            {
                _afiliado = value;
                NumeroAfiliado = value.NumeroAfiliado;
            }
        }
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public AfiliadoServicio()
        {
            CuentasAfiliadas = new List<CuentaAfiliada>();
        }

        #region Metodos
        /// <summary>
        /// Metodo que crea una afiliacion de servicio
        /// </summary>
        /// <param name="afiliado">Afiliado</param>
        /// <param name="codigoServicio"> Codigo del servicio</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <returns>AfiladoServicio</returns>
        public static AfiliadoServicio Crear(Afiliado afiliado, short codigoServicio,
            DateTime fechaSistema)
        {
            return new AfiliadoServicio()
            {
                Afiliado = afiliado,
                CodigoServicio = codigoServicio,
                IndicadorEstado = Afiliado.EstadoAfiliado,
                FechaAfiliacion = fechaSistema
            };
        }
        /// <summary>
        /// Método que desafilia un servicio y remueve las cuentas afiliadas
        /// </summary>
        /// <param name="usuario">usuario que realiza la acción</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        public void DesafiliarServicioYRemoverCuentasAfiliadas(
            Usuario usuario,
            DateTime fechaSistema)
        {
            IndicadorEstado = IndicadorDesafiliado;
            FechaDesafiliacion = fechaSistema;
            CodigoAgenciaDesafiliacion = usuario.CodigoAgencia;
            CodigoUsuarioDesafiliacion = usuario.CodigoUsuario;
            FechaModificacion = fechaSistema;
            CuentasAfiliadas.Clear();
        }
        #endregion Metodos
    }
}
