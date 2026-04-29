using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.TJ
{
    /// <summary>
    /// Clase que representa a la entidad de dominio de una Tarjeta
    /// </summary>
    public class Tarjeta
    {
        public const string TarjetaActiva = "01";
        public const string TipoVisa = "3";

        /// <summary>
        /// Numero de tarjeta
        /// </summary>
        public decimal NumeroTarjeta { get; private set; }
        /// <summary>
        /// Codidog de agencia
        /// </summary>
        public string CodigoAgencia { get; private set; }
        /// <summary>
        /// Codigo del cliente
        /// </summary>
        public string CodigoCliente { get; private set; }
        /// <summary>
        /// Codigo de tipo de tarjeta
        /// </summary>
        public string CodigoTipoTarjeta { get; private set; }
        /// <summary>
        /// Codigo de estado de tarjeta
        /// </summary>
        public string CodigoEstadoTarjeta { get; private set; }
        /// <summary>
        /// Codigo de empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; }
        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public DateTime FechaVencimiento { get; private set; }
        /// <summary>
        /// Tipo de estado de tarjeta
        /// </summary>
        public string TipoEstadoTarjeta { get; private set; }
        /// <summary>
        /// Datos del dueño
        /// </summary>
        private Cliente _duenio;
        /// <summary>
        /// Datos del dueño
        /// </summary>
        virtual public Cliente Duenio
        {
            get { return _duenio; }
            set
            {
                _duenio = value;
                CodigoCliente = value.CodigoCliente;
            }
        }

        /// <summary>
        /// Método de verificar si la tarjeta esta vencida
        /// </summary>
        /// <param name="fechaSistema"></param>
        /// <returns></returns>
        public bool TarjetaVencida(DateTime fechaSistema)
        {
            return !(DateTime.Compare(fechaSistema.Date, FechaVencimiento.Date) <= 0);
        }

        /// <summary>
        /// Métdo que verifica si el tipo de estado de tarjeta esta activa
        /// </summary>
        /// <returns></returns>
        public bool Activa()
        {
            return TipoEstadoTarjeta == "TR" && CodigoEstadoTarjeta == "01";
        }
    }
}
