using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff
{
    /// <summary>
    /// DTO que permite almacenar resultados o datos de la progrmacion SingOffOn
    /// </summary>
    public class SingOnOffProgramadoDTO
    {
        #region Constantes
        /// <summary>
        /// Descripción de la tarea programada sign on
        /// </summary>
        public const string TareaProgramadaSignOn = "tarea_programada_sign_on";
        /// <summary>
        /// Descripción de la tarea programada sign off
        /// </summary>
        public const string TareaProgramadaSignOff = "tarea_programada_sign_off";
        #endregion
        /// <summary>
        /// Numero de Periodo
        /// </summary>
        public long? NumeroPeriodo { get; set; }
        /// <summary>
        /// Indentificador de Entidad
        /// </summary>
        public int IdentificadorEntidad { get; set; }
        /// <summary>
        /// Codigo de Entidad
        /// </summary>
        public string CodigoEntidad { get; set; }
        /// <summary>
        /// Descripcion del periodo
        /// </summary>
        public string DescripcionPeriodo { get; set; }
        /// <summary>
        /// Hora del Sing On
        /// </summary>
        public TimeOnly HoraSingOn { get; set; }
        /// <summary>
        /// Hora del Sing Off
        /// </summary>
        public TimeOnly HoraSingOff { get; set; }
        /// <summary>
        /// Indicador de Estado
        /// </summary>
        public string IndicadorEstado { get; set; }
        /// <summary>
        /// Codigo de usuario de registro
        /// </summary>
        public string? CodigoUsuarioRegistro { get; set; }
        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime? FechaRegistro { get; set; }
        /// <summary>
        /// Codigo del usuario que realiza una modificacion
        /// </summary>
        public string? CodigoUsuarioModificacion { get; set; }
        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
    }
}
