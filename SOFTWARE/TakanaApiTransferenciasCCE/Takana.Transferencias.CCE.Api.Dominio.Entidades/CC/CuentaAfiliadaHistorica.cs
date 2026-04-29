using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.SG;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class CuentaAfiliadaHistorica
    {
        /// <summary>
        /// Codigo empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Numero afiliado
        /// </summary>
        public int NumeroAfiliado { get; private set; }
        /// <summary>
        /// Coidgo de servicio
        /// </summary>
        public short CodigoServicio { get; private set; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Codigo de agencia Cuenta
        /// </summary>
        public string CodigoAgenciaCuenta { get; private set; }
        /// <summary>
        /// Fecha de desafiliacion
        /// </summary>
        public DateTime FechaDesafiliacion { get; private set; }
        /// <summary>
        /// Codigo de usuario desafiliacion
        /// </summary>
        public string CodigoUsuarioDesafiliacion { get; private set; }
        /// <summary>
        /// Codigo de Agencia desafiliacion
        /// </summary>
        public string CodigoAgenciaDesafiliacion { get; private set; }
        /// <summary>
        /// Afiliaion servicio
        /// </summary>
        private AfiliadoServicio _afiliadoServicio;
        /// <summary>
        /// Afiliacoin servicio publico
        /// </summary>
        public virtual AfiliadoServicio AfiliadoServicio
        {
            get { return _afiliadoServicio; }
            set
            {
                _afiliadoServicio = value;
                NumeroAfiliado = value.NumeroAfiliado;
                CodigoServicio = value.CodigoServicio;
            }
        }
        /// <summary>
        /// Cuenta efectivo
        /// </summary>
        virtual public CuentaEfectivo Cuenta { get; private set; }

        /// <summary>
        /// Cuenta la Cuenta Afiliacion Historica
        /// </summary>
        /// <param name="cuentaAfiliada">Cuenta afiliada</param>
        /// <param name="usuario">usuario</param>
        /// <param name="fechaSistema">fecha de evento</param>
        /// <returns>Retorna historica</returns>
        public static CuentaAfiliadaHistorica Crear(CuentaAfiliada cuentaAfiliada,
            Usuario usuario, DateTime fechaSistema)
        {
            return new CuentaAfiliadaHistorica()
            {
                CodigoEmpresa = Empresa.CodigoPrincipal,
                AfiliadoServicio = cuentaAfiliada.AfiliadoServicio,
                Cuenta = cuentaAfiliada.Cuenta,
                CodigoUsuarioDesafiliacion = usuario.CodigoUsuario,
                CodigoAgenciaDesafiliacion = usuario.CodigoAgencia,
                CodigoAgenciaCuenta = cuentaAfiliada.CodigoAgenciaCuenta,
                FechaDesafiliacion = fechaSistema
            };
        }
    }
}
