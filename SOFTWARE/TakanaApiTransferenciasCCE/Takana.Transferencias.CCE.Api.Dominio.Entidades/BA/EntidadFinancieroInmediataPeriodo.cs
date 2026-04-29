using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.BA
{
    /// <summary>
    /// Entidad que representa los periodos del Sing On / Sing Off
    /// </summary>
    public class EntidadFinancieroInmediataPeriodo
    {
        #region Propiedades
        /// <summary>
        /// Numero de Periodo
        /// </summary>
        public long NumeroPeriodo { get; private set; }
        /// <summary>
        /// Indentificador de Entidad
        /// </summary>
        public int IdentificadorEntidad { get; private set; }
        /// <summary>
        /// Codigo de Entidad
        /// </summary>
        public string CodigoEntidad { get; private set; }
        /// <summary>
        /// Descripcion del periodo
        /// </summary>
        public string DescripcionPeriodo { get; private set; }
        /// <summary>
        /// Hora del Sing On
        /// </summary>
        public TimeOnly HoraSingOn { get; private set; }
        /// <summary>
        /// Hora del Sing Off
        /// </summary>
        public TimeOnly HoraSingOff { get; private set; }
        /// <summary>
        /// Indicador de Estado
        /// </summary>
        public string IndicadorEstado { get; private set; }
        /// <summary>
        /// Codigo de usuario de registro
        /// </summary>
        public string CodigoUsuarioRegistro { get; private set; }
        /// <summary>
        /// Fecha de Registro
        /// </summary>
        public DateTime FechaRegistro { get; private set; }
        /// <summary>
        /// Codigo del usuario que realiza una modificacion
        /// </summary>
        public string CodigoUsuarioModificacion { get; private set; }
        /// <summary>
        /// Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; private set; }
        #endregion
        #region Metodos
        /// <summary>
        /// Crea una nueva instancia de <see cref="EntidadFinancieroInmediataPeriodo"/> con los datos
        /// del período de operación y valores iniciales de auditoría.
        /// </summary>
        /// <param name="descripcionPeriodo">Descripción del período.</param>
        /// <param name="horaSingOn">Hora de inicio del período (Sign On).</param>
        /// <param name="horaSingOff">Hora de fin del período (Sign Off).</param>
        /// <param name="indicadorEstado">Indicador de estado del período.</param>
        /// <param name="codigoUsuarioRegistro">Código del usuario que registra la información.</param>
        /// <param name="fechaRegistro">Fecha y hora del registro.</param>
        /// <returns>
        /// Una nueva instancia de <see cref="EntidadFinancieroInmediataPeriodo"/> inicializada.
        /// </returns>
        public static EntidadFinancieroInmediataPeriodo Crear(
           string descripcionPeriodo,
           TimeOnly horaSingOn,
           TimeOnly horaSingOff,
           string indicadorEstado,
           string codigoUsuarioRegistro,
           DateTime fechaRegistro
           )
        {
            return new EntidadFinancieroInmediataPeriodo()
            {
                IdentificadorEntidad = General.IndentificadorEntidad,
                CodigoEntidad = General.CodigoEntidad,
                DescripcionPeriodo = descripcionPeriodo,
                HoraSingOn = horaSingOn,
                HoraSingOff = horaSingOff,
                IndicadorEstado = indicadorEstado,
                CodigoUsuarioRegistro = codigoUsuarioRegistro,
                FechaRegistro = fechaRegistro,
                CodigoUsuarioModificacion = "Vacio",
                FechaModificacion = new DateTime(1990, 1, 1)
            };
        }
        /// <summary>
        /// Modifica la información del período, incluyendo horarios, descripción,
        /// estado y datos de auditoría de modificación.
        /// </summary>
        /// <param name="descripcionPeriodo">Nueva descripción del período.</param>
        /// <param name="horaSingOn">Nueva hora de inicio del período (Sign On).</param>
        /// <param name="horaSingOff">Nueva hora de fin del período (Sign Off).</param>
        /// <param name="indicadorEstado">Nuevo indicador de estado.</param>
        /// <param name="codigoUsuarioModificacion">Código del usuario que realiza la modificación.</param>
        /// <param name="fechaModificacion">Fecha y hora de la modificación.</param>
        public void ModificarPeriodoSing(
           string descripcionPeriodo,
           TimeOnly horaSingOn,
           TimeOnly horaSingOff,
           string indicadorEstado,
           string codigoUsuarioModificacion,
           DateTime fechaModificacion
        )
        {
            DescripcionPeriodo = descripcionPeriodo;
            HoraSingOn= horaSingOn;
            HoraSingOff= horaSingOff;
            IndicadorEstado= indicadorEstado;
            CodigoUsuarioModificacion = codigoUsuarioModificacion;
            FechaModificacion= fechaModificacion;
        }
        /// <summary>
        /// Modifica únicamente el estado del período y actualiza los datos
        /// de auditoría de la modificación.
        /// </summary>
        /// <param name="indicadorEstado">Nuevo indicador de estado.</param>
        /// <param name="codigoUsuarioModificacion">Código del usuario que realiza la modificación.</param>
        /// <param name="fechaModificacion">Fecha y hora de la modificación.</param>
        public void ModificarEstadoProgamadoSing(
            string indicadorEstado,
            string codigoUsuarioModificacion,
            DateTime fechaModificacion
            )
        {
            IndicadorEstado = indicadorEstado;
            CodigoUsuarioModificacion = codigoUsuarioModificacion;
            FechaModificacion = fechaModificacion;
        }
        #endregion
    }
}
