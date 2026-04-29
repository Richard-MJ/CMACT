namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public abstract class BitacoraInteroperabilidad
    {
        /// <summary>
        /// Código de respuesta relacionado con la operación segun la CCE
        /// </summary>
        private string? _codigoRespuesta;

        /// <summary>
        /// Fecha y hora de creación de la operación.
        /// </summary>
        private DateTime _fechaCreacion;

        /// <summary>
        /// Fecha y hora de la respuesta de la operación, puede ser nulo si aún no hay respuesta.
        /// </summary>
        private DateTime? _fechaRespuesta;

        /// <summary>
        /// Código del usuario que realizó el registro inicial.
        /// </summary>
        private string? _codigoUsuarioRegistro;

        /// <summary>
        /// Código del usuario que realizó la última modificación.
        /// </summary>
        private string? _codigoUsuarioModifico;

        /// <summary>
        /// Fecha y hora de registro inicial.
        /// </summary>
        private DateTime _fechaRegistro;

        /// <summary>
        /// Fecha y hora de la última modificación.
        /// </summary>
        private DateTime? _fechaModifico;

        /// <summary>
        /// Código de respuesta relacionado con la operación.
        /// </summary>
        public string? CodigoRespuesta
        {
            get { return _codigoRespuesta; }
            internal set { _codigoRespuesta = value; }
        }

        /// <summary>
        /// Fecha y hora de creación de la operación.
        /// </summary>
        public DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        /// <summary>
        /// Fecha y hora de la respuesta de la operación, puede ser nulo si aún no hay respuesta.
        /// </summary>
        public DateTime? FechaRespuesta
        {
            get { return _fechaRespuesta; }
            internal set { _fechaRespuesta = value; }
        }

        /// <summary>
        /// Código del usuario que realizó el registro inicial.
        /// </summary>
        public string? CodigoUsuarioRegistro
        {
            get { return _codigoUsuarioRegistro; }
            set { _codigoUsuarioRegistro = value; }
        }

        /// <summary>
        /// Código del usuario que realizó la última modificación.
        /// </summary>
        public string? CodigoUsuarioModifico
        {
            get { return _codigoUsuarioModifico; }
            set { _codigoUsuarioModifico = value; }
        }

        /// <summary>
        /// Fecha y hora de registro inicial.
        /// </summary>
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }

        /// <summary>
        /// Fecha y hora de la última modificación.
        /// </summary>
        public DateTime? FechaModifico
        {
            get { return _fechaModifico; }
            set { _fechaModifico = value; }
        }
    }
}
