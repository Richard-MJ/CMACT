using System.Text;

namespace Takana.Transferencias.CCE.Api.Common.Utilidades
{
    public class ShortGuid
    {
        #region Static

        /// <summary>
        /// A read-only instance of the ShortGuid class whose value
        /// is guaranteed to be all zeroes.
        /// </summary>
        public static readonly ShortGuid Empty = new ShortGuid(Guid.Empty);

        #endregion Static

        #region Fields

        private readonly Guid _guid;
        private readonly string _value;

        #endregion Fields

        #region Contructors

        /// <summary>
        /// Creates a ShortGuid from a base64 encoded string
        /// </summary>
        /// <param name="value">The encoded guid as a
        /// base64 string</param>
        public ShortGuid(string value)
        {
            _value = value;
            _guid = Decode(value);
        }

        /// <summary>
        /// Creates a ShortGuid from a Guid
        /// </summary>
        /// <param name="guid">The Guid to encode</param>
        public ShortGuid(Guid guid)
        {
            _value = Encode(guid);
            _guid = guid;
        }

        #endregion Contructors

        #region ToString

        /// <summary>
        /// Returns the base64 encoded guid as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _value;
        }

        #endregion ToString

        #region NewGuid

        /// <summary>
        /// Initialises a new instance of the ShortGuid class
        /// </summary>
        /// <returns></returns>
        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }

        #endregion NewGuid

        #region Encode

        /// <summary>
        /// Creates a new instance of a Guid using the string value,
        /// then returns the base64 encoded version of the Guid.
        /// </summary>
        /// <param name="value">An actual Guid string (i.e. not a ShortGuid)</param>
        /// <returns></returns>
        public static string Encode(string value)
        {
            Guid guid = new Guid(value);
            return Encode(guid);
        }

        /// <summary>
        /// Encodes the given Guid as a base64 string that is 22
        /// characters long.
        /// </summary>
        /// <param name="guid">The Guid to encode</param>
        /// <returns></returns>
        public static string Encode(Guid guid)
        {
            string encoded = Convert.ToBase64String(guid.ToByteArray());
            encoded = encoded
                .Replace("/", "_")
                .Replace("+", "-");
            return encoded.Substring(0, 22);
        }
        /// <summary>
        /// Codificado en Base 64
        /// <param name="input"></param>
        /// <returns>Retorna una cadena</returns>
        public static string Base64UrlEncode(string input)
        {
            return Base64UrlEncode(Encoding.UTF8.GetBytes(input));
        }

        /// <summary>
        /// Codificado en base 64 de byte a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }

        #endregion Encode

        #region Decode

        /// <summary>
        /// Decodes the given base64 string
        /// </summary>
        /// <param name="value">The base64 encoded string of a Guid</param>
        /// <returns>A new Guid</returns>
        public static Guid Decode(string value)
        {
            value = value
                .Replace("_", "/")
                .Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(value + "==");
            return new Guid(buffer);
        }

        /// <summary>
        /// Decodificar en Base 64 URL
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns>Retornar string convertido</returns>
        public static string Base64UrlDecode(string base64Url)
        {
            string paddedBase64 = base64Url.Replace('-', '+').Replace('_', '/');
            switch (base64Url.Length % 4)
            {
                case 2: paddedBase64 += "=="; break;
                case 3: paddedBase64 += "="; break;
            }

            byte[] byteArray = Convert.FromBase64String(paddedBase64);
            return Encoding.UTF8.GetString(byteArray);
        }

        /// <summary>
        /// Decodificar en Base 64 URL
        /// </summary>
        /// <param name="base64Url"></param>
        /// <returns>Retornar string convertido</returns>
        public static byte[] Base64UrlDecodeBytes(string input)
        {
            string output = input.Replace('-', '+').Replace('_', '/');

            switch (output.Length % 4)
            {
                case 2: output += "=="; break;
                case 3: output += "="; break;
            }

            return Convert.FromBase64String(output);
        }

        #endregion Decode

        #region Operators

        /// <summary>
        /// Implicitly converts the ShortGuid to it's string equivilent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator string(ShortGuid shortGuid)
        {
            return shortGuid._value;
        }

        /// <summary>
        /// Implicitly converts the ShortGuid to it's Guid equivilent
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator Guid(ShortGuid shortGuid)
        {
            return shortGuid._guid;
        }

        /// <summary>
        /// Implicitly converts the string to a ShortGuid
        /// </summary>
        /// <param name="shortGuid"></param>
        /// <returns></returns>
        public static implicit operator ShortGuid(string shortGuid)
        {
            return new ShortGuid(shortGuid);
        }

        /// <summary>
        /// Implicitly converts the Guid to a ShortGuid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static implicit operator ShortGuid(Guid guid)
        {
            return new ShortGuid(guid);
        }

        #endregion Operators
    }
}
