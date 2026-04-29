using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CL
{
    /// <summary>
    /// Clase de dominio encargada del afiliado
    /// </summary>
    public class Afiliado
    {
        #region Constantes
        /// <summary>
        /// Codigo para numero de afiliacion
        /// </summary>
        public const string CodigoNumeroAfiliacion = "NUM_AFILIA";
        /// <summary>
        /// Estado afiliado
        /// </summary>
        public const string EstadoAfiliado = "A";
        /// <summary>
        /// Nivel de afiliacion
        /// </summary>
        public const string NivelAfiliado = "S";
        #endregion Constantes

        #region Propiedades
        /// <summary>
        /// Numero de afiliado
        /// </summary>
        public int NumeroAfiliado { get; private set; }
        /// <summary>
        /// Fecha de afiliacion
        /// </summary>
        public DateTime FechaAfiliacion { get; private set; }
        /// <summary>
        /// Nivel de afiliacion
        /// </summary>
        public string NivelAfiliacion { get; private set; }
        /// <summary>
        /// Codigo de empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Codigo de cliente
        /// </summary>
        public string CodigoCliente { get; private set; }
        /// <summary>
        /// Numero de tarjeta
        /// </summary>
        public decimal NumeroTarjeta { get; private set; }
        /// <summary>
        /// Codigo de agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Codigo de usuario
        /// </summary>
        public string CodigoUsuario { get; private set; }
        /// <summary>
        /// Fecha de registro
        /// </summary>
        public DateTime FechaRegistra { get; private set; }
        /// <summary>
        /// Indicador de activo
        /// </summary>
        public string IndicadorActivo { get; private set; }

        /// <summary>
        /// Número de movimiento
        /// </summary>
        public decimal NumeroMovimiento { get; set; }

        /// <summary>
        /// Propiedad que representa el listado de afiliaciones a servicios
        /// </summary>
        virtual public ICollection<AfiliadoServicio> ServiciosAfiliado { get; private set; } = new List<AfiliadoServicio>();
        /// <summary>
        /// Entidad cliente
        /// </summary>
        private Cliente _cliente;
        /// <summary>
        /// Entidad cliente
        /// </summary>
        virtual public Cliente Cliente
        {
            get { return _cliente; }
            set
            {
                _cliente = value;
                CodigoCliente = value.CodigoCliente;
            }
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Método de crea un afiliado
        /// </summary>
        /// <param name="numeroAfiliacion"></param>
        /// <param name="fechaSistema"></param>
        /// <param name="usuario"></param>
        /// <param name="tarjeta"></param>
        /// <returns>Retorna un afiliado</returns>
        public static Afiliado Crear(
            int numeroAfiliacion,
            DateTime fechaSistema,
            Usuario usuario,
            Tarjeta tarjeta,
            decimal numeroMovimiento = 0)
        {
            return new Afiliado()
            {
                Cliente = tarjeta.Duenio,
                CodigoAgencia = usuario.CodigoAgencia,
                CodigoEmpresa = Empresa.CodigoPrincipal,
                CodigoUsuario = usuario.CodigoUsuario,
                FechaAfiliacion = fechaSistema,
                FechaRegistra = fechaSistema,
                IndicadorActivo = EstadoAfiliado,
                NivelAfiliacion = NivelAfiliado,
                NumeroAfiliado = numeroAfiliacion,
                NumeroTarjeta = tarjeta.NumeroTarjeta,
                NumeroMovimiento = numeroMovimiento,
            };
        }

        /// <summary>
        /// Método que agrega servicios de afiliación
        /// </summary>
        /// <param name="servicioAfiliacion">servicio de afiliación</param>
        public void AgregarServicioAfiliacion(AfiliadoServicio servicioAfiliacion)
        {
            ServiciosAfiliado.Add(servicioAfiliacion);
        }
        #endregion Metodos
    }
}
