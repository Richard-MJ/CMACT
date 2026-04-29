using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Common.DTOs
{
    public abstract record CanalDTO
    {
        /// <summary>
        /// Codigo del canal
        /// </summary>
        public string Canal { get; set; }

        /// <summary>
        /// Propiedad calculada del canal
        /// </summary>
        public bool EsMensajeCliente =>
            Canal == ((int)CanalEnum.Interoperabilidad).ToString() ||
            Canal == ((int)CanalEnum.AppMovil).ToString() ||
            Canal == ((int)CanalEnum.HomeBanking).ToString();
    }
}
