using AutorizadorCanales.Domain.Entidades.CL;

namespace AutorizadorCanales.Domain.Entidades.TJ
{
    /// <summary>
    /// Entidad de la tarjeta empresarial
    /// </summary>
    public class TarjetaHomebankingEmpresarial
    {
        /// <summary>
        /// Código de la empresa
        /// </summary>
        public string CodigoEmpresa { get; private set; } = null!;

        /// <summary>
        /// Número de la tarjeta
        /// </summary>
        public decimal NumeroTarjeta { get; private set; }

        /// <summary>
        /// Código del cliente empresarial
        /// </summary>
        public string? CodigoClienteEmpresarial { get; private set; } = null!;

        /// <summary>
        /// Número de autorización
        /// </summary>
        public int? NumeroAutorizacion { get; private set; }

        /// <summary>
        /// Indicador de estado
        /// </summary>
        public string? IndicadorEstado { get; private set; } = null!;

        public string? CodigoUsuarioRegistro { get; private set; } = null!;
        public DateTime? FechaCreacion { get; private set; }
        public string? CodigoUsuarioModifico { get; private set; } = null!;
        public DateTime? FechaModifico { get; private set; }

        public virtual Cliente ClienteEmpresarial { get; private set; } = null!;
        public virtual Tarjeta Tarjeta { get; private set; } = null!;

        public const string INDICADOR_AFILIADO = "A";
        public const string INDICADOR_REGISTRADO = "R";
        public const string INDICADOR_INACTIVO = "I";

        public Cliente Duenio => ClienteEmpresarial;

        public void ActivarAfiliacion(string codigoUsuario, DateTime fechaModificacion)
        {
            IndicadorEstado = INDICADOR_AFILIADO;
            CodigoUsuarioModifico = codigoUsuario;
            FechaModifico = fechaModificacion;
        }

        public void DesactivarAfiliacion(string codigoUsuario, DateTime fechaModificacion)
        {
            IndicadorEstado = INDICADOR_INACTIVO;
            CodigoUsuarioModifico = codigoUsuario;
            FechaModifico = fechaModificacion;
        }

        public bool NoRegistraTarjetaEmpresarial() =>
            IndicadorEstado != INDICADOR_AFILIADO;

        public void CambiarEstadoRegistrado()
        {
            IndicadorEstado = INDICADOR_REGISTRADO;
        }        
        
        public void CambiarEstadoAfiliado()
        {
            IndicadorEstado = INDICADOR_AFILIADO;
        }
    }
}
