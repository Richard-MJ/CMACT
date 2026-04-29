namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA
{
    /// <summary>
    /// Clase que representa a la entidad de dominio Oficina
    /// </summary>
    public class OficinaCCE
    {

        #region Constantes
        /// <summary>
        /// Codigo oficina de lima
        /// </summary>
        public const string CodigoOficinaLima = "15";
        #endregion Constantes

        #region Propiedades
        /// <summary>
        /// Id de la entidad financiera
        /// </summary>
        public int IDEntidadFinanciera { get; private set; }
        /// <summary>
        /// Id de la plaza
        /// </summary>
        public int IDPlaza { get; private set; }
        /// <summary>
        /// Id de la oficina CCE
        /// </summary>
        public int IDOficina { get; private set; }
        /// <summary>
        /// Código de la oficina 
        /// </summary>
        public virtual string CodigoOficina { get; private set; }
        /// <summary>
        /// Indicador del estado de la oficina
        /// </summary>
        public virtual string EstadoOficina { get; private set; }
        /// <summary>
        /// Código de ubigeo de referencia de la oficia
        /// </summary>
        public virtual string? CodigoUbigeoReferencia { get; private set; }
        /// <summary>
        /// Propiedad virtual de la plaza CCE
        /// </summary>
        public virtual PlazaCCE PlazaCCE { get; private set; }
        /// <summary>
        /// Propiedad virtual de la entidad financiera CCE
        /// </summary>
        public virtual EntidadFinancieraDiferida EntidadFinancieraCCE { get; private set; }

        /// <summary>
        /// Método que valida si la oficina es de lima
        /// </summary>
        /// <returns></returns>
        public bool EsOficinaDeLima()
        {
            return CodigoUbigeoReferencia.Substring(0, 2) == CodigoOficinaLima;
        }
        #endregion Propiedades
    }
}
