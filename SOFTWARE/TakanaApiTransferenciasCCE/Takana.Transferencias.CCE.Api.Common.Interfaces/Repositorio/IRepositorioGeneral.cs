using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

namespace Takana.Transferencias.CCE.Api.Common.Interfaz
{
    public interface IRepositorioGeneral : IRepositorioReportes, IRepositorioLectura, IRepositorioEscritura
    {
        /// <summary>
        /// Obtiene datos del cliente por Codigo de Cuenta Interbancaria
        /// </summary>
        /// <param name="codigoCuentaInterbancario">Codigo de Cuenta Bancaria del Cliente</param>
        /// <returns>Retorna un diccionario de datos</returns>
        ClienteReceptorDTO ObtenerDatosClientePorCodigoCuentaInterbancaria(string codigoCuentaInterbancario);
        /// <summary>
        /// Se obtiene el número de inicial de una secuencia de números de series de sistema.
        /// </summary>
        /// <param name="codigoAgencia">Código de agencia de la serie de sistema, "%" obtiene la serie por empresa de sistema.</param>
        /// <param name="codigoSistema">Código de sistema de la series de sistema</param>
        /// <param name="codigoSerie">Código de la serie de sistema.</param>
        /// <param name="cantidadSeries">Cantidad de número de la secuencia de números de serie.</param>
        /// <returns>Primero número de la serie de sistema.</returns>
        int ObtenerNumeroSerieNoBloqueante(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries);

        /// <summary>
        /// Se obtiene el número de inicial de una secuencia de números de series de sistema.
        /// </summary>
        /// <param name="codigoAgencia">Código de agencia de la serie de sistema, "%" obtiene la serie por empresa de sistema.</param>
        /// <param name="codigoSistema">Código de sistema de la series de sistema</param>
        /// <param name="codigoSerie">Código de la serie de sistema.</param>
        /// <param name="cantidadSeries">Cantidad de número de la secuencia de números de serie.</param>
        /// <returns>Primero número de la serie de sistema.</returns>
        Task<int> ObtenerNumeroSerieNoBloqueanteAsync(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries);

        /// <summary>
        /// Método que retorna el valor de un parámetro por Empresa
        /// </summary>
        /// <param name="codigoSistema">Código del sistema</param>
        /// <param name="codigoParametro">Código del parámetro</param>
        /// <returns>Valor del parámetro</returns>
        string ObtenerValorParametroPorEmpresa(string codigoSistema, string codigoParametro);
        /// <summary>
        /// Método para obtener el valor de un índice de cálculo
        /// </summary>
        /// <param name="codigoIndice">Código de Índice</param>
        /// <param name="fechaReferencia">Fecha de referencia</param>
        /// <returns></returns>
        decimal ObtenerValorPorIndice(string codigoIndice, DateTime fechaReferencia);
        /// <summary>
        /// Método para obtener el valor de un índice de cálculo
        /// </summary>
        /// <returns>Moneda local.</returns>
        Moneda ObtenerMonedaLocal();
        /// <summary>
        /// Procedimiento almancenado que obtiene  el detalle de la transferencia inmediata para correo electronico
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <exception cref="Exception"></exception>
        CorreoTransferenciaInmediataDTO ObtenerDetalleTransferenciaInmediataPorNumeroMovimiento(int numeroMovimiento);
    }
}
