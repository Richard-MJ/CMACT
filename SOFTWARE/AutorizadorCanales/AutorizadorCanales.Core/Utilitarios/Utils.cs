using AutorizadorCanales.Core.Constantes;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AutorizadorCanales.Core.Utilitarios;

public static class Utils
{
    /// <summary>
    /// Hashea string
    /// </summary>
    /// <param name="texto">valor a hashear</param>
    /// <returns></returns>
    public static string HashString(this string texto)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
            byte[] hashBytes = sha256.ComputeHash(textoBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    /// <summary>
    /// Enmascara el núermo de tarjeta
    /// </summary>
    /// <param name="numeroTarjeta">Número de tarjeta</param>
    /// <returns></returns>
    public static string EnmascararTarjeta(this string numeroTarjeta)
    {
        return string.Concat(new string('*', numeroTarjeta.ToString().Length - 4), (numeroTarjeta.ToString().Substring(numeroTarjeta.ToString().Length - 4)));
    }

    /// <summary>
    /// Obtiene los guids dependiendo del canal
    /// </summary>
    /// <param name="canal"></param>
    /// <param name="guids"></param>
    /// <returns></returns>
    public static List<string> ObtenerGuidsPorCanal(string canal, string? guids)
    {
        if (canal == CanalElectronicoConstante.APP_CMACT)
            return string.IsNullOrEmpty(guids)
                ? new List<string>()
                : JsonSerializer.Deserialize<List<string>>(guids)!;
        if (canal == CanalElectronicoConstante.HOME_BANKING || canal == CanalElectronicoConstante.BANKING_EMPRESARIAL)
            return string.IsNullOrEmpty(guids) || guids == "undefined" || guids == "0"
                ? new List<string>()
                : new List<string>(guids!.Split(','));

        return new List<string>();
    }
}
