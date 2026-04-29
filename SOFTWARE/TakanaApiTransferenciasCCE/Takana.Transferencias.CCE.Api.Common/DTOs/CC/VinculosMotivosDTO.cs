namespace Takana.Transferencias.CCE.Api.Common.DTOs.CC
{
    /// <summary>
    /// DTO del vinculo y motivo
    /// </summary>
    public class VinculosMotivosDTO
    {
        /// <summary>
        /// Vinculos
        /// </summary>
        public IList<VinculoMotivoDTO> Vinculos { get; set; }
        /// <summary>
        /// Motivos
        /// </summary>
        public IList<VinculoMotivoDTO> Motivos { get; set; }
    }

    /// <summary>
    /// DTO de vinculos
    /// </summary>
    public class VinculoMotivoDTO
    {
        /// <summary>
        /// identificador de vinculo motivo
        /// </summary>
        public int IdVinculoMotivo { get; set; }
        /// <summary>
        /// Nombre de vinculo
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Especificado
        /// </summary>
        public bool Especificar { get; set; }
        /// <summary>
        /// Tipo de vinculo
        /// </summary>
        public string Tipo { get; set; }
    }
}
