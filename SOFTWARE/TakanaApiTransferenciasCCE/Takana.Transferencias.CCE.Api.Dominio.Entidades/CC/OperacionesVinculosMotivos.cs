using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.PA;

namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CC
{
    public class OperacionesVinculosMotivos
    {
        /// <summary>
        /// Número de identidad de operación.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOperacionVinculoMotivo { get; private set; }

        /// <summary>
        /// Número de operación.
        /// </summary>
        public int NumeroOperacion { get; private set; }

        /// <summary>
        /// Código del sistema que se afecta.
        /// </summary>
        public string CodigoSistema { get; private set; }

        /// <summary>
        /// Código de la agencia.
        /// </summary>
        public string CodigoAgencia { get; private set; }

        /// <summary>
        /// Id de identidad del vinculo.
        /// </summary>
        public int IdVinculoMovimiento { get; private set; }

        /// <summary>
        /// Descripción del vinculo en caso de especificar.
        /// </summary>
        public string EspecificarDetalleVinculo { get; private set; }

        /// <summary>
        /// Id de identidad del motivo.
        /// </summary>
        public int IdMotivoMovimiento { get; private set; }

        /// <summary>
        /// Descripción del motivo en caso de especificar.
        /// </summary>
        public string EspecificarDetalleMotivo { get; private set; }

        /// <summary>
        /// Nacionalidad del beneficiario
        /// </summary>
        public string IdNacionalidad { get; private set; }

        /// <summary>
        /// Fecha de ingreso de la operación.
        /// </summary>
        public DateTime FechaRegistro { get; private set; }

        /// <summary>
        /// Vinculo de Movimiento.
        /// </summary>
        public virtual VinculoMovimiento VinculoMovimiento { get; private set; }

        /// <summary>
        /// Motivo de Movimiento.
        /// </summary>
        public virtual MotivoMovimiento MotivoMovimiento { get; private set; }

        /// <summary>
        /// Instancia de la entidad Nacion
        /// </summary>
        public virtual Nacion Nacion { get; private set; }
        /// <summary>
        /// Genera una nueva entidad de la operacion de vinculos y motivos.
        /// </summary>
        /// <param name="operacion">Operacoin principal</param>
        /// <param name="IdVinculoMovimiento">Identificador de vinculo</param>
        /// <param name="IdMotivoMovimiento">Identificador de motivo</param>
        /// <param name="vinculoEspecificado">Descripcion vinculo</param>
        /// <param name="motivoEspecificado">Descripcion motivo</param>
        /// <returns>OperacionesVinculosMotivos</returns>
        public static OperacionesVinculosMotivos Generar(
            ITransaccion operacion,
            int IdVinculoMovimiento,
            int IdMotivoMovimiento,
            string vinculoEspecificado,
            string motivoEspecificado)
        {
            return new OperacionesVinculosMotivos()
            {
                NumeroOperacion = operacion.NumeroOperacion,
                IdVinculoMovimiento = IdVinculoMovimiento,
                EspecificarDetalleVinculo = vinculoEspecificado,
                IdMotivoMovimiento = IdMotivoMovimiento,
                EspecificarDetalleMotivo = motivoEspecificado,
                FechaRegistro = operacion.FechaOperacion,
                CodigoSistema = ((IOperacionLavado)operacion).CodigoSistema,
                CodigoAgencia = ((IOperacionLavado)operacion).CodigoAgencia
            };
        }
    }
}
