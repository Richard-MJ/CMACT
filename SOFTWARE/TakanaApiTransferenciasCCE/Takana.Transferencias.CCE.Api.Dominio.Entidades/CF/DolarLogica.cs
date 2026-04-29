using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class DolarLogica : MonedaLogica
{
    public DolarLogica(Moneda moneda) : base(moneda)
    {
    }
    /// <summary>
    /// Método de codigo de moneda opuesta
    /// </summary>
    /// <returns></returns>
    public override string CodigoMonedaOpuesta()
    {
        return ((int)MonedaCodigo.Soles).ToString();
    }
}