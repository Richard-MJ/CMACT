using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Notificaciones
{
    /// <summary>
    /// clase de notificaicones UNIBANCA
    /// </summary>
    public class NotificacionUnibancaDTO
    {
        public const string NotificacionTransferenciaVentanilla = "operacion_ventanilla";

        public const string NotificacionTransferenciaCanalElectronico = "transferencia_terceros_APP";

        public const string DescripcionTipoOperacion = "TipoOperacion";
    }
}
