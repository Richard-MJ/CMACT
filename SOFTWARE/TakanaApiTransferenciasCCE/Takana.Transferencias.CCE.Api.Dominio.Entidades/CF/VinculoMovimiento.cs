using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF
{
    public class VinculoMovimiento
    {
        /// <summary>
        /// ID del vinculo del movimiento.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdVinculoMovimiento { get; private set; }
        /// <summary>
        /// Código del sistema afectado.
        /// </summary>
        public string CodigoSistema { get; private set; }
        /// <summary>
        /// Descripción del vinculo o motivo.
        /// </summary>
        public string Descripcion { get; private set; }
        /// <summary>
        /// Indicador de activo.
        /// </summary>
        public bool IndicadorActivo { get; private set; }
        /// <summary>
        /// Indicador de especificar detalle.
        /// </summary>
        public bool IndicadorEspecificar { get; private set; }
        /// <summary>
        /// Codigo de ususario modificador.
        /// </summary>
        public string CodigoUsuarioModificador { get; private set; }
        /// <summary>
        /// Fecha de modificación.
        /// </summary>
        public DateTime FechaModificacion { get; private set; }
    }
}
