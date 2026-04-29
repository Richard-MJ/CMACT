
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    /// <summary>
    /// Interfaz del servicio de dominio de lavado.
    /// </summary>
    public interface IServicioDominioLavado : IServicioBase
    {
        /// <summary>
        /// Método para realizar la obtención de un registro de lavado de operacion unica transferencia interbancaria inmediata entrante.
        /// </summary>
        /// <param name="transferencia">Objeto de la Transferencia.</param>
        /// <param name="asiento">Asiento generado.</param>
        /// <param name="clienteEmpresarial">Cliente empresarial.</param>
        /// <param name="clienteDestino">Cliente destino, puede ser null.</param>
        /// <param name="fechaSistema">Fecha Sistema.</param>
        /// <param name="banco">Entidad financiera.</param>
        /// <returns>Registro de Lavado.</returns>
        IRegistroLavado RegistrarLavadoOperacionUnicaTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion, Transferencia transferencia,
            AsientoContable asiento, Cliente clienteDestino, ClienteExternoDTO clienteOriginante,
            DateTime fechaSistema, EntidadFinancieraInmediata banco, int numeroOperacionUnica);

        /// <summary>
        /// Método para realizar la obtención de un registro de menor cuantia lavado transferencia interbancaria inmediata entrante.
        /// </summary>
        /// <param name="transferencia">Objeto de la Transferencia.</param>
        /// <param name="asiento">Asiento generado.</param>
        /// <param name="clienteEmpresarial">Cliente empresarial.</param>
        /// <param name="clienteDestino">Cliente destino, puede ser null.</param>
        /// <param name="banco">Entidad financiera.</param>
        /// <returns>Registro de Lavado.</returns>
        IRegistroLavado RegistrarLavadoMenorCuantiaTransferenciaEntrante(
            SubTipoTransaccion subTipoTransaccion, Transferencia transferencia,
            AsientoContable asiento, Cliente clienteDestino, ClienteExternoDTO clienteOriginante,
            EntidadFinancieraInmediata banco, int numeroMenorCuantia);

        /// <summary>
        /// Método para completar un lavado
        /// </summary>
        /// <param name="registroLavado"></param>
        /// <param name="operacionesLavado"></param>
        /// <param name="tiposTransaccionesOrigen"></param>
        void CompletarLavado(IRegistroLavado registroLavado, IList<IOperacionLavado> operacionesLavado,
           IList<TipoOperacionCanalOrigen> tiposTransaccionesOrigen);

        /// <summary>
        /// Método que anula el lavado activo
        /// </summary>
        /// <param name="lavado"></param>
        void AnularLavadoActivo(IRegistroLavado lavado);
    }
}
