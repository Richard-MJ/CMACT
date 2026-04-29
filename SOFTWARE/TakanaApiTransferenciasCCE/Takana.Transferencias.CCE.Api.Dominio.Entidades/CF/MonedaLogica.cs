using Takana.Transferencias.CCE.Api.Common.Excepciones;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public abstract class MonedaLogica
{
    /// <summary>
    /// Moenda
    /// </summary>
    public Moneda Moneda { get; private set; }

    protected MonedaLogica(Moneda moneda)
    {
        Moneda = moneda;
    }

    /// <summary>
    /// Codigo de moneda opuesta
    /// </summary>
    /// <returns></returns>
    public abstract string CodigoMonedaOpuesta();

    /// <summary>
    /// Crear moneda logica
    /// </summary>
    /// <param name="moneda"></param>
    /// <returns></returns>
    /// <exception cref="ValidacionException"></exception>
    public static MonedaLogica Crear(Moneda moneda)
    {
        var codigo = (MonedaCodigo)Enum.Parse(typeof(MonedaCodigo), moneda.CodigoMoneda);
        switch (codigo)
        {
            case MonedaCodigo.Soles:
                return new SolLogica(moneda);
            case MonedaCodigo.Dolares:
                return new DolarLogica(moneda);
            default:
                throw new ValidacionException("Código de moneda " + moneda.CodigoMoneda + " no válida.");
        }
    }
}