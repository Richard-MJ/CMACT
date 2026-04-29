using Takana.Transferencias.CCE.Api.Common.Constantes;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

/// <summary>
/// Clase de dominio que representa una operación de Menor Cuantia
/// </summary>
public class MenorCuantiaEncabezado : IRegistroLavado
{ 
    #region Constantes
    /// <summary>
    /// Menor cuantia activa
    /// </summary>
    public const string EstadoActivo = "A";
    /// <summary>
    /// Codigo de serie lavado
    /// </summary>
    public const string CodigoSerieLavado = "OP_CUANTIA";
    #endregion  Constantes

    #region Propiedades

    /// <summary>
    /// Representa el Número de Operación
    /// </summary>
    public int NumeroLavado { get; private set; }
    /// <summary>
    /// Representa el código de la agencia de la operación
    /// </summary>
    public string CodigoAgencia { get; private set; }
    /// <summary>
    /// Representa el código de la modalidad de la operación
    /// </summary>
    public int CodigoModalidad { get; private set; }
    /// <summary>
    /// Representa el código de la sub modalidad de la operación
    /// </summary>
    public int CodigoSubModalidad { get; private set; }
    /// <summary>
    /// Representa la fecha de registro de la operación
    /// </summary>
    public DateTime FechaRegistro { get; private set; }
    /// <summary>
    /// Indica el estado de la operación
    /// </summary>
    public string IndicadorEstado { get; private set; }
    /// <summary>
    /// Representa el código del usuario de la operación
    /// </summary>
    public string CodigoUsuario { get; private set; }
    /// <summary>
    /// Representa los detalles de la operación
    /// </summary>
    public virtual ICollection<MenorCuantiaDetalle> Detalles { get; private set; }
    /// <summary>
    /// Representa los intervinientes de la operación
    /// </summary>
    public virtual ICollection<MenorCuantiaInterviniente> Intervinientes { get; private set; }
    /// <summary>
    /// Indicador Forma Pago
    /// </summary>
    public int IndicadorFormaPago { get; private set; }
    /// <summary>
    /// Representa el código ubigeo
    /// </summary>
    public string CodigoUbigeo { get; private set; }
    #endregion Propiedades

    #region Constructor

    /// <summary>
    /// Constructor privado
    /// </summary>
    public MenorCuantiaEncabezado()
    {
        Detalles = new List<MenorCuantiaDetalle>();
        Intervinientes = new List<MenorCuantiaInterviniente>();
    }

    #endregion Constructor

    #region Metodos
    /// <summary>
    /// Método que crea una menor cuantia encabezado
    /// </summary>
    /// <param name="numeroOperacion"></param>
    /// <param name="codigoAgencia"></param>
    /// <param name="codigoModalidad"></param>
    /// <param name="codigoSubModalidad"></param>
    /// <param name="fechaRegistro"></param>
    /// <param name="indicadorEstado"></param>
    /// <param name="codigoUsuario"></param>
    /// <returns>Retorna datos de menor cuantia</returns>
    public static MenorCuantiaEncabezado Crear(
        int numeroOperacion
        ,string codigoAgencia
        ,int codigoModalidad
        ,int codigoSubModalidad
        ,DateTime fechaRegistro
        ,string indicadorEstado
        ,string codigoUsuario
    )
    {
        return new MenorCuantiaEncabezado()
        {
            NumeroLavado = numeroOperacion,
            CodigoAgencia = codigoAgencia,
            CodigoModalidad = codigoModalidad,
            CodigoSubModalidad = codigoSubModalidad,
            FechaRegistro = fechaRegistro,
            IndicadorFormaPago = Int32.Parse(General.FormaPagoCuentaCorriente),
            IndicadorEstado = indicadorEstado,
            CodigoUsuario = codigoUsuario
        };
    }

    /// <summary>
    /// Adiciona un detalle a la operación de menor cuantia
    /// </summary>
    /// <param name="detalle">Instancia de la clase MenorCuantiaDetalle</param>
    public void AdicionarDetalle(MenorCuantiaDetalle detalle)
    {
        Detalles.Add(detalle);
    }

    /// <summary>
    /// Adiciona un intervniente a la operación de menor cuantia
    /// </summary>
    /// <param name="interviniente">Instancia de la clase MenorCuantiaInterviniente</param>
    public void AdicionarInterviniente(MenorCuantiaInterviniente interviniente)
    {
        Intervinientes.Add(interviniente);
    }

    #region Implementación de IRegistroLavado
    /// <summary>
    /// Método que inaplica el lavado
    /// </summary>
    public void InaplicarLavado()
    {
        IndicadorEstado = General.No;
    }
    /// <summary>
    /// Método que completa datos del detalle
    /// </summary>
    /// <param name="origen"></param>
    /// <param name="destino"></param>
    /// <param name="codigoCanal"></param>
    public void CompletarDatosDetalle(IList<IOperacionLavado> origen, IOperacionLavado destino
        , string codigoCanal)
    {
        IndicadorEstado = General.Activo;
        AdicionarDetalle(MenorCuantiaDetalle.RegistarDetalle(NumeroLavado, origen, destino));
    }
    /// <summary>
    /// Indicador de tipo lavado
    /// </summary>
    public string IndicadorTipoLavado => IRegistroLavado.MenorCuantia;
    /// <summary>
    /// Numero de operacion de lavado
    /// </summary>
    public int NumeroOperacionLavado => NumeroLavado;
    /// <summary>
    /// Método de adicionar interviniente
    /// </summary>
    /// <param name="interviniente"></param>
    /// <returns></returns>
    public IRegistroLavado AdicionarInterviniente(ILavadoInterviniente interviniente)
    {
        if (interviniente is MenorCuantiaInterviniente)
            Intervinientes.Add(interviniente as MenorCuantiaInterviniente);
        return this;
    }

    #endregion Implementación de IRegistroLavado

    #endregion Metodos
}