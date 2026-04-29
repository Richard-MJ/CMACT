using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class ParametroPorAgencia : Empresa
    {
        /// <summary>
        /// Código de la agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Código del sistema
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Código del parámetro
        /// </summary>
        public string CodigoParametro { get; private set; }
        /// <summary>
        /// valor de parametro
        /// </summary>
        public string ValorParametro { get; private set; }
    }
}
