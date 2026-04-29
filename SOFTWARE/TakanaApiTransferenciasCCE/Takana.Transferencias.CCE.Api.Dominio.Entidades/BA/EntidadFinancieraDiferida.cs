namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA
{
    /// <summary>
    /// Clase que representa a la entidad de dominio Entidad financiera
    /// </summary>
    public class EntidadFinancieraDiferida
    {
        #region Propiedades

        /// <summary>
        /// Identificador de la Entidad Financiera
        /// </summary>
        public int IDEntidadFinanciera { get; private set; }

        /// <summary>
        /// Código que identifica a la Entidad Financiera según CCE
        /// </summary>
        public virtual string CodigoEntidad { get; private set; }

        /// <summary>
        /// Descripción de la Entidad Financiera
        /// </summary>
        public string NombreEntidad { get; private set; }

        /// <summary>
        /// Indicador de entidades con cheque: SI (S); NO (N)
        /// </summary>
        public virtual string EstaActivaCheque { get; private set; }

        /// <summary>
        /// Código de oficina TIN 225
        /// </summary>
        public string? OficinaPagoTarjeta { get; private set; }

        /// <summary>
        /// Código SBS de la entidad
        /// </summary>
        public string? CodigoEntidadSbs { get; private set; }
        /// <summary>
        /// Lista de oficinas CCE
        /// </summary>
        public virtual ICollection<OficinaCCE> Oficinas { get; private set; }

        #endregion Propiedades
    }
}
