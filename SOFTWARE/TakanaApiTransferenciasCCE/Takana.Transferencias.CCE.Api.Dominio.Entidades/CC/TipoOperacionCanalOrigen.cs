using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class TipoOperacionCanalOrigen : Empresa
    {

        #region Constantes
        public const short Transaccion = 11;
        #endregion Constantes

        /// <summary>
        /// ID de una transaccion origen por operación, autogenerado.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrigenLavado { get; private set; }

        /// <summary>
        /// ID del tipo de operación que se esta realizando.
        /// </summary>
        public short IdTipoOperacion { get; private set; }

        /// <summary>
        /// Código del canal de origen de la operación.
        /// </summary>
        public string CodigoCanal { get; private set; }

        /// <summary>
        /// Código del sub canal de origen de la operación.
        /// </summary>
        public byte CodigoSubCanal { get; private set; }

        /// <summary>
        /// Propiedad que representa el código del sistema
        /// </summary>
        public string CodigoSistema { get; private set; }

        /// <summary>
        /// Propiedad que representa el Código de tipo de transacción
        /// </summary>
        public string CodigoTipoTransaccion { get; private set; }

        /// <summary>
        /// Propiedad que representa el Código de tipo de sub transacción
        /// </summary>
        public string CodigoSubTipoTransaccion { get; private set; }

        /// <summary>
        /// INDICADOR DE USO DE INTERVINIENTE PRINCIPAL
        /// </summary>
        public byte IntervinientePrincipal { get; private set; }

        public bool UsarIntervinientePrincipal
        {
            get
            {
                return IntervinientePrincipal == 1;
            }
        }
        /// <summary>
        /// Subtipo de transacción que se toma como principal en un tipo de operación determinado para un
        /// sub canal de origen.
        /// </summary>
        public virtual SubTipoTransaccion SubTipoTransaccion { get; private set; }
    }
}
