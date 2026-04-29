using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takana.Transferencias.CCE.Api.Common.DTOs.SignOnOff;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica.Programacion_Sing_On_Off
{
    public class LogicaGestionarProgramacionSing
    {
        /// <summary>
        /// Gestiona el período de Sign On / Sign Off programado.
        /// Si no existe una entidad previa, crea un nuevo período con la información proporcionada;
        /// de lo contrario, actualiza el período existente.
        /// </summary>
        /// <param name="entidadPeriodoSing">
        /// Entidad del período existente. Si es <c>null</c>, se creará una nueva instancia.
        /// </param>
        /// <param name="periodoProgramado">
        /// Objeto DTO que contiene la información del período programado.
        /// </param>
        /// <param name="codigoUsuario">
        /// Código del usuario que realiza la operación (creación o modificación).
        /// </param>
        /// <param name="fechaActual">
        /// Fecha y hora de la operación.
        /// </param>
        /// <returns>
        /// La entidad creada cuando el período no existe; 
        /// de lo contrario, <c>null</c> cuando solo se realiza una modificación.
        /// </returns>
        public static EntidadFinancieroInmediataPeriodo GestionarPeriodoSing(
            EntidadFinancieroInmediataPeriodo entidadPeriodoSing,
            SingOnOffProgramadoDTO periodoProgramado,
            string codigoUsuario,
            DateTime fechaActual
            )
        {
            var resultados = new EntidadFinancieroInmediataPeriodo();

            if (entidadPeriodoSing == null)
            {
                entidadPeriodoSing = EntidadFinancieroInmediataPeriodo.Crear(
                    periodoProgramado.DescripcionPeriodo,
                    periodoProgramado.HoraSingOn,
                    periodoProgramado.HoraSingOff,
                    periodoProgramado.IndicadorEstado,
                    codigoUsuario,
                    fechaActual
                );
                resultados = entidadPeriodoSing;
            }
            else
            {
                entidadPeriodoSing.ModificarPeriodoSing(
                   periodoProgramado.DescripcionPeriodo,
                   periodoProgramado.HoraSingOn,
                   periodoProgramado.HoraSingOff,
                   periodoProgramado.IndicadorEstado,
                   codigoUsuario,
                   fechaActual
                );
                resultados = null;
            }

            return resultados;
        }

    }
}
