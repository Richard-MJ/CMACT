using System.Text.Json;
using System.Text.Json.Serialization;

namespace Takana.Transferencias.CCE.Api.Common.Utilidades;

/// <summary>
/// Clase que convierte un campo de json de un tipo a otro
/// </summary>
/// <typeparam name="T">Tipo de datos a convertir</typeparam>
public class ConvertirdorJsonAtipo<T> : JsonConverter<T>
{
    /// <summary>
    /// Metodo que leer el tipo de dato a convertir
    /// </summary>
    /// <param name="leido">Json a convertir</param>
    /// <param name="tipoAconvertir">tipo a convertir</param>
    /// <param name="opcion">Opciones a convertir</param>
    /// <returns>Tipo de dato convertido</returns>
    public override T Read(ref Utf8JsonReader leido, Type tipoAconvertir, JsonSerializerOptions opcion)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Decimal:
                return LeerDecimal(ref leido);
            case TypeCode.Int32:
                return LeerInt(ref leido);
            case TypeCode.Int64:
                return LeerInt64(ref leido);
            default:
                throw new JsonException($"El convertidor no soporta este tipo: {tipoAconvertir}");
        }
    }
    /// <summary>
    /// Convierte de un decimal a un valor
    /// </summary>
    /// <param name="leido">Json a convertir</param>
    /// <param name="valor">tipo de dato</param>
    /// <param name="opciones">opcion de tipo</param> 
    public override void Write(Utf8JsonWriter leido, T valor, JsonSerializerOptions opciones)
    {
        leido.WriteNumberValue(Convert.ToDecimal(valor));
    }

    /// <summary>
    /// Metodo que convierte un string a un decimal
    /// </summary>
    /// <param name="leido">Json convertir</param>
    /// <returns>Valor convertido</returns> 
    private T LeerDecimal(ref Utf8JsonReader leido)
    {
        if (leido.TokenType == JsonTokenType.String)
        {
            if (decimal.TryParse(leido.GetString(), out decimal resultado))
            {
                return (T)Convert.ChangeType(resultado, typeof(T));
            }
        }

        return (T)Convert.ChangeType(leido.GetDecimal(), typeof(T));
    }
    /// <summary>
    /// Metodo que convierte un string a un entero
    /// </summary>
    /// <param name="leido">Json convertir</param>
    /// <returns>Valor convertido</returns>
    private T LeerInt(ref Utf8JsonReader leido)
    {
        if (leido.TokenType == JsonTokenType.String)
        {
            if (int.TryParse(leido.GetString(), out int result))
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        return (T)Convert.ChangeType(leido.GetInt32(), typeof(T));
    }

    /// <summary>
    /// Metodo que convierte un string a un entero
    /// </summary>
    /// <param name="leido">Json convertir</param>
    /// <returns>Valor convertido</returns>
    private T LeerInt64(ref Utf8JsonReader leido)
    {
        if (leido.TokenType == JsonTokenType.String)
        {
            if (int.TryParse(leido.GetString(), out int result))
            {
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        return (T)Convert.ChangeType(leido.GetInt64(), typeof(T));
    }
}