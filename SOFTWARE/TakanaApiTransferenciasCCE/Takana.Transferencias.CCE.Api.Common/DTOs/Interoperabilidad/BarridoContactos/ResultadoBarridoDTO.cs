namespace Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad
{
    /// <summary>
    /// Rresultado principal de barrido de contactos
    /// </summary>
    public record ResultadoPrincipalBarridoDTO
    {
        /// <summary>
        /// Monto maximo por transaccion
        /// </summary>
        public decimal LimiteMontoMaximo { get; set; }
        /// <summary>
        /// Monto minimo por transaccion
        /// </summary>
        public decimal LimiteMontoMinimo { get; set; }
        /// <summary>
        /// Monto maximo sumado por dia
        /// </summary>
        public decimal MontoMaximoDia { get; set; }
        /// <summary>
        /// Resultado de barrido de contactos
        /// </summary>
        public List<ResultadoBarridoDTO> ResultadosBarrido { get; set; }
    }

    /// <summary>
    /// Resultado segcundario de barrido de contactos
    /// </summary>
    public class ResultadoBarridoDTO
    {
        /// <summary>
        /// Numeor de celular del cliente receptor
        /// </summary>
        public string NumeroCelular {  get; set; }
        /// <summary>
        /// Entidades o directorios a los que el cliente receptor esta afiliado
        /// </summary>
        public List<EntidadesReceptorAfiliado> EntidadesReceptor { get; set; } = new List<EntidadesReceptorAfiliado>();
    }
    /// <summary>
    /// Objeto de entidad receptora
    /// </summary>
    public class EntidadesReceptorAfiliado
    {
        /// <summary>
        /// Codigo de entidad
        /// </summary>
        public string CodigoEntidad { get; set; }
        /// <summary>
        /// Nombre de entidad
        /// </summary>
        public string NombreEntidad { get; set; }
    }
}
