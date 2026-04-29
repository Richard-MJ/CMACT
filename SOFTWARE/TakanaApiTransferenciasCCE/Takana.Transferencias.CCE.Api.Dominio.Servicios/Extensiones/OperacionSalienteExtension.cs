using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{
    public static class OperacionSalienteExtension
    {
        /// <summary>
        /// Extesion que mapea el cuerpo de consulta cuenta por cci
        /// </summary>
        /// <param name="consultaCuenta"></param>
        /// <returns>Retorna datos de consulta cuenta</returns>
        public static List<TipoTransferenciaDTO> TipoTransferenciasCce(
            this List<TipoTransferencia> datos)
        {
            var tipoTransferencia = new List<TipoTransferenciaDTO>();
            return datos.ConvertAll(c => new TipoTransferenciaDTO()
            {
                CodigoTipoTransferencia = c.Codigo,
                DescripcionTipoTransferencia = c.Descripcion,
                EstadoTipoTransferencia = c.IndicadorEstado
            });
        }

        /// <summary>
        /// Lista las entidades de la CCE para transferencias inmediatas
        /// </summary>
        /// <param name="entidades">lista de entidades</param>
        /// <returns>retorna datos de la entidad</returns>
        public static List<EntidadFinancieraTinDTO> AEntidadFinancietaTin(
            this List<EntidadFinancieraInmediata> entidades)
        {
            return entidades.Select(
               x => new EntidadFinancieraTinDTO()
               {
                   IdEntidad = x.IdentificadorEntidad,
                   CodigoEstadoSign = x.CodigoEstadoSign,
                   CodigoEntidad = x.CodigoEntidad,
                   NombreEntidad = x.NombreEntidad,
                   OficinaPagoTarjeta = x.OficinaPagoTarjeta ?? string.Empty
               }).ToList();
        }

        /// <summary>
        /// Obtiene la comision de la transferencia
        /// </summary>
        /// <param name="comision">Comision de la transfernecia</param>
        /// <returns>Datos de la comision</returns>
        public static ComisionDTO ADatosComision(
             this ComisionCCE comision)
        {
            return new ComisionDTO
            {
                IdTipoTransferencia = comision.IdTipoTransferencia,
                CodigoComision = comision.CodigoComision,
                CodigoMoneda = comision.CodigoMoneda,
                CodigoAplicacionTarifa = comision.CodigoAplicacionTarifa,
                Porcentaje = comision.Porcentaje,
                Minimo = comision.Minimo,
                Maximo = comision.Maximo,
                IndicadorPorcentaje = comision.IndicadorPorcentaje,
                IndicadorFijo = comision.IndicadorFijo,
                PorcentajeCCE = comision.PorcentajeCCE,
                MinimoCCE = comision.MinimoCCE,
                MaximoCCE = comision.MaximoCCE
            };
        }
        /// <summary>
        /// Mapea de detalle de transferencia DTO a Entidad de detalle de transferencia
        /// </summary>
        /// <param name="detalle">Detakke de datis</param>
        /// <param name="transferencia">Datos de transferencia</param>
        /// <param name="entidadDetino">Entidad Destino</param>
        /// <param name="numeroDetalle">Numero de detalle</param>
        /// <returns>Retorna los datos del detalle de transfernecia</returns>
        public static TransferenciaDetalleSalienteCCE ADetalleTransferencia(
            this RealizarTransferenciaInmediataDTO detalle,
            Transferencia transferencia,
            EntidadFinancieraDiferida entidadDetino,
            int numeroDetalle)
        {
            return TransferenciaDetalleSalienteCCE.Crear(
                transferencia,
                numeroDetalle,
                detalle.CodigoCuentaTransaccionReceptor,
                entidadDetino.IDEntidadFinanciera,
                entidadDetino,
                detalle.CodigoTipoDocumentoReceptor,
                detalle.NumeroDocumentoReceptor,
                detalle.Beneficiario,
                detalle.MismoTitularEnDestino
                    ? ((int)RealizarTransferenciaInmediataDTO.NumeroTitular.Si).ToString()
                    : ((int)RealizarTransferenciaInmediataDTO.NumeroTitular.No).ToString(),
                detalle.ControlMonto.Monto,
                detalle.CodigoTarifarioComision,
                detalle.ControlMonto.TotalComision
            );
        }
        /// <summary>
        /// Mape de la entidad de Concepto de cobro a Conceptor de cobro
        /// </summary>
        /// <param name="concepto">Concepto de cobro</param>
        /// <returns>Lista de conceptos de cobro</returns>
        public static List<ConceptoCobroDTO> AConceptoCobroDTO(
            this List<ConceptoCobroCCE> concepto)
        {
            return concepto.Select(
               e => new ConceptoCobroDTO()
               {
                   IdConcepto = e.IdConcepto,
                   Codigo = e.Codigo,
                   Descripcion = e.Descripcion,
                   IndicadorHabilitado = e.IndicadorHabilitado
               }).ToList();
        }
    }
}
