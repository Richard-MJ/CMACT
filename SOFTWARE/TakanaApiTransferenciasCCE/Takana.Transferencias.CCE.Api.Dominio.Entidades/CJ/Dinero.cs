using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CJ;

/// <summary>
/// Clase de dominio que representa el detalle de la operación de dinero
/// </summary>
public class Dinero
{
    /// <summary>
    /// Propiedad que indica la moneda de origen del dinero
    /// </summary>
    public Moneda Moneda { get; private set; }
    /// <summary>
    /// Propiedad que indica el monto del dinero
    /// </summary>
    public decimal Monto { get; private set; }
    /// <summary>
    /// Propiedad que indica el tipo de cambio que se utiliza al realizar la conversión
    /// </summary>
    public decimal TasaCambioAplicada { get; private set; }
    /// <summary>
    /// Clase de dinero
    /// </summary>
    /// <param name="moneda"></param>
    /// <param name="monto"></param>
    private Dinero(Moneda moneda, decimal monto)
    {
        Moneda = moneda;
        Monto = monto;
        TasaCambioAplicada = 1;
    }

    /// <summary>
    /// Clase de crear dinero
    /// </summary>
    /// <param name="moneda"></param>
    /// <param name="monto"></param>
    /// <returns></returns>
    public static Dinero Crear(Moneda moneda, decimal monto)
    {
        return new Dinero(moneda, monto);
    }

    /// <summary>
    /// Método de convertir a tasa de cambio
    /// </summary>
    /// <param name="moneda"></param>
    /// <param name="tasaCambio"></param>
    /// <returns></returns>
    public Dinero ConvertirATasaCambio(Moneda moneda, ITasaCambio tasaCambio)
    {
        var dineroConvertido = Crear(Moneda, Monto);

        if (Moneda.CodigoMoneda != moneda.CodigoMoneda)
        {
            if (Moneda.CodigoMoneda == ((int)MonedaCodigo.Dolares).ToString())
            {
                dineroConvertido = Crear(moneda,
                    (Monto * tasaCambio.ValorVenta).Redondear(AsientoContableDetalle.DecimalesPorDefecto));
                dineroConvertido.TasaCambioAplicada = tasaCambio.ValorVenta;
            }
            else
            {
                if (Moneda.CodigoMoneda == ((int)MonedaCodigo.Soles).ToString())
                {
                    dineroConvertido = Crear(moneda,
                        (Monto / tasaCambio.ValorCompra).Redondear(AsientoContableDetalle.DecimalesPorDefecto));
                    dineroConvertido.TasaCambioAplicada = tasaCambio.ValorCompra;
                }
            }
        }
        return dineroConvertido;
    }
}

