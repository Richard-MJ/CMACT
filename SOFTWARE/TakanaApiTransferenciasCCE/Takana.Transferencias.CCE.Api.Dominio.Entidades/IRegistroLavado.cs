namespace Takana.Transferencias.CCE.Api.Dominio.Entidades
{
    /// <summary>
    /// Interfaz de dominio de registro lavado
    /// </summary>
    public interface IRegistroLavado
    {
        /// <summary>
        /// Indica que es lavado unico
        /// </summary>
        public const string Unico = "U";
        /// <summary>
        /// Indica que es lavado de menor cuantia
        /// </summary>
        public const string MenorCuantia = "M";
        /// <summary>
        /// Indicador de estado del lavado.
        /// </summary>
        string IndicadorEstado { get; }

        /// <summary>
        /// Método para anular el lavado de activo.
        /// </summary>
        void InaplicarLavado();
        /// <summary>
        /// Indica el tipo de lavado si es Unico (U) o Menor Cuantía (M)
        /// </summary>
        string IndicadorTipoLavado { get; }

        /// <summary>
        /// Número del Lavado
        /// </summary>
        int NumeroOperacionLavado { get; }

        /// <summary>
        /// Método para completar el detalle registro de lavado
        /// </summary>
        /// <param name="origen">Lista de movimientos origen, utilizando en operaciones con movimientos mixtos
        /// por cuentas sueldo (remunerativo, no remunerativo).</param>
        /// <param name="destino">Movimiento destino</param>
        /// <param name="codigoCanal">Codigo de canal</param>
        void CompletarDatosDetalle(IList<IOperacionLavado> origen, IOperacionLavado destino, string codigoCanal);

        /// <summary>
        /// Agrega un interviniente.
        /// </summary>
        /// <param name="interviniente"></param>
        IRegistroLavado AdicionarInterviniente(ILavadoInterviniente interviniente);
    }
}
