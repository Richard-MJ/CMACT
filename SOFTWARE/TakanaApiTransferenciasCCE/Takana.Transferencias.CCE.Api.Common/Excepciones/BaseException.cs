using System.Runtime.Serialization;

namespace Takana.Transferencias.CCE.Api.Common.Excepciones
{
    /// <summary>
    /// Representa una excepcion controlada por el sistema.
    /// </summary>
    public class BaseException : Exception
    {
        public const string DescripcionExcepcionGeneral = "En este momento no podemos atenderlo";
        public const string DescripcionExcepcionCCI = "El CCI ingresado es incorrecto";
        /// <summary>
        /// Codigo del error.
        /// </summary>
        public string CodigoError { get; }
        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="codigo">Codigo del error.</param>
        /// <param name="message">Descripcion del error.</param>
        protected BaseException(string codigo, string message) : base(message)
        {
            CodigoError = codigo;
        }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="codigo">Codigo del error.</param>
        /// <param name="message">Descripcion del error.</param>
        /// <param name="innerException">Excepcion interna.</param>
        protected BaseException(string codigo, string message, Exception innerException) : base(message,
            innerException)
        {
            CodigoError = codigo;
        }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="info">Informacion de serializacion.</param>
        /// <param name="context">Contexto de la ejecucion.</param>
        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
