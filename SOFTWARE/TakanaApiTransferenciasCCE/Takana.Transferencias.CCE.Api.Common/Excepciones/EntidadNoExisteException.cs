using System.Reflection;
using System.Runtime.Serialization;

namespace Takana.Transferencias.CCE.Api.Common.Excepciones
{
    public class EntidadNoExisteException : BaseException
    {
        /// <summary>
        /// Codigo del error.
        /// </summary>
        private const string Codigo = "02";

        /// <summary>
        /// Formato del mensaje del error.
        /// </summary>
        private const string Formato = "No se pudo recuperar la entidad {0} con clave: {1}.";

        /// <summary>
        /// Representa una excepcion cuando no se encuentra una entidiad.
        /// </summary>
        /// <param name="tipoEntidad">Tipo de la entidad.</param>
        /// <param name="propiedadesClave">Propiedades clave de la entidad.</param>
        public EntidadNoExisteException(MemberInfo tipoEntidad, IEnumerable<object> propiedadesClave) : base(Codigo,
            string.Format(Formato, tipoEntidad.Name, propiedadesClave.Aggregate((a, b) => a + "," + b)))
        {
        }

        /// <summary>
        /// Representa una excepcion cuando no se encuentra una entidiad.
        /// </summary>
        /// <param name="tipoEntidad">Tipo de la entidad.</param>
        /// <param name="propiedadesClave">Propiedades clave de la entidad.</param>
        /// <param name="innerException">Excepcion interna.</param>
        public EntidadNoExisteException(MemberInfo tipoEntidad, IEnumerable<object> propiedadesClave,
            Exception innerException) : base(Codigo,
            string.Format(Formato, tipoEntidad.Name, propiedadesClave.Aggregate((a, b) => a + "," + b)),
            innerException)
        {
        }

        /// <summary>
        /// Representa una excepcion controlada por el sistema.
        /// </summary>
        /// <param name="info">Informacion de serializacion.</param>
        /// <param name="context">Contexto de la ejecucion.</param>
        public EntidadNoExisteException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
