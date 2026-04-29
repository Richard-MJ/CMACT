using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    /// <summary>
    /// Clase de dominio que repreenta a la entidad Cuenta afiliada
    /// </summary>
    public class CuentaAfiliada
    {
        /// <summary>
        /// Codigo de empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Numero de afiliado
        /// </summary>
        public int NumeroAfiliado { get; private set; }
        /// <summary>
        /// Codigo de servicio
        /// </summary>
        public short CodigoServicio { get; private set; }
        /// <summary>
        /// Numero de cuenta
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Codigo de agencia cuenta
        /// </summary>
        public string CodigoAgenciaCuenta { get; private set; }
        /// <summary>
        /// Afiliacion
        /// </summary>
        private AfiliadoServicio _afiliadoServicio;
        /// <summary>
        /// Afiliacion a servicio
        /// </summary>
        virtual public AfiliadoServicio AfiliadoServicio
        {
            get { return _afiliadoServicio; }
            set
            {
                _afiliadoServicio = value;
                if (value == null) return;
                NumeroAfiliado = value.NumeroAfiliado;
                CodigoServicio = value.CodigoServicio;
            }
        }
        /// <summary>
        /// Cuenta efectivo relacionada
        /// </summary>
        virtual public CuentaEfectivo Cuenta { get; private set; }

        /// <summary>
        /// Crea y agrega el servicio
        /// </summary>
        /// <param name="servicio">Servicio</param>
        /// <param name="cuenta">cuenta afiliada</param>
        /// <returns></returns>
        public static CuentaAfiliada CrearYAgregarAlServicio(AfiliadoServicio servicio, CuentaEfectivo cuenta)
        {
            CuentaAfiliada cuentaAfiliada = new CuentaAfiliada()
            {
                AfiliadoServicio = servicio,
                Cuenta = cuenta,
                CodigoEmpresa = Empresa.CodigoPrincipal,
                CodigoAgenciaCuenta = cuenta.CodigoAgencia
            };

            servicio.CuentasAfiliadas.Add(cuentaAfiliada);

            return cuentaAfiliada;
        }
    }

}
