using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class SolLogica : MonedaLogica
{
    /// <summary>
    /// Clase de sol logico
    /// </summary>
    /// <param name="moneda"></param>
    public SolLogica(Moneda moneda) : base(moneda)
    {
    }

    /// <summary>
    /// Método de codigo de moneda opuesta
    /// </summary>
    /// <returns></returns>
    public override string CodigoMonedaOpuesta()
    {
        return ((int)MonedaCodigo.Dolares).ToString();
    }
}