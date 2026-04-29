using Takana.Transferencias.CCE.Api.Common;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.CC;
using Takana.Transferencias.CCE.Api.Common.DTOs.CF;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones
{
    /// <summary>
    /// Clase que mapea datos de la consulta cuenta
    /// </summary>
    public static class ConsultaCuentaExtensiones
    {
        /// <summary>
        /// Metodo que mapealos datos para la recepcion de la consulta de cuenta
        /// </summary>
        /// <param name="datosRecibidos">Datos recibidos de la CCE</param>
        /// <param name="datosCalculados">Datos calculados</param>
        /// <returns>Datos de la respuesta de la CCE</returns>
        public static EstructuraContenidoAV3 ArmarDatos(
            this ConsultaCuentaRecepcionEntradaDTO datosRecibidos,
            ConsultaCuentaRespuestaEntradaDTO datosCalculados)
        {
            return new EstructuraContenidoAV3(){
                AV3 = new ConsultaCuentaRespuestaEntradaDTO(){
                    debtorParticipantCode = datosRecibidos.debtorParticipantCode,
                    creditorParticipantCode = datosRecibidos.creditorParticipantCode,
                    creationDate = datosRecibidos.creationDate,
                    creationTime = datosRecibidos.creationTime,
                    trace = datosRecibidos.trace,
                    branchId = datosRecibidos.branchId,
                    debtorName = datosRecibidos.debtorName?.EsVacioTexto()?.QuitarDiacriticos(),
                    debtorId = datosRecibidos.debtorId,
                    debtorIdCode = datosRecibidos.debtorIdCode,
                    debtorPhoneNumber = datosRecibidos.debtorPhoneNumber?.EsVacioTexto(),
                    debtorAddressLine = datosRecibidos.debtorAddressLine?.EsVacioTexto(),
                    debtorMobileNumber = datosRecibidos.debtorMobileNumber?.EsVacioTexto(),
                    transactionType = datosRecibidos.transactionType,
                    channel = datosRecibidos.channel,
                    instructionId = datosRecibidos.instructionId,
                    creditorAddressLine = datosRecibidos.creditorAddressLine?.EsVacioTexto(),
                    creditorPhoneNumber = datosRecibidos.creditorPhoneNumber?.EsVacioTexto(),
                    creditorMobileNumber = datosRecibidos.creditorMobileNumber?.EsVacioTexto(),
                    creditorCCI = datosRecibidos.creditorCCI?.EsVacioTexto(),
                    creditorCreditCard = datosRecibidos.creditorCreditCard?.EsVacioTexto(),
                    currency = datosRecibidos.currency,
                    proxyValue = datosRecibidos.proxyValue?.EsVacioTexto(),
                    proxyType = datosRecibidos.proxyType?.EsVacioTexto(),                        
                    terminalId = datosRecibidos.terminalId.LimitarCaracteres(8), 
                    retrievalReferenteNumber = datosRecibidos.retrievalReferenteNumber,
                    responseCode = datosCalculados.responseCode,
                    reasonCode = datosCalculados.reasonCode?.EsVacioTexto(),
                    creditorName = datosCalculados.creditorName?.EsVacioTexto()?.QuitarDiacriticos(),
                    creditorId =  datosCalculados.creditorId?.EsVacioTexto(),
                    creditorIdCode =  datosCalculados.creditorIdCode?.EsVacioTexto(),
                    sameCustomerFlag =  datosCalculados.sameCustomerFlag?.EsVacioTexto(),
                }
            };        
        }

        /// <summary>
        /// Metodo que mapea los datos para la consulta cuenta de salida
        /// </summary>
        /// <param name="datos">Datos de la consulta cuenta</param>
        /// <param name="datosCanal">Datos el canal origen</param>
        /// <returns></returns>
        public static ConsultaCuentaSalidaDTO ArmarDatos(
            this ConsultaCuentaSalidaDTO datos,
            ConsultaCanalDTO datosCanal)
        {
            datos.terminalId = datosCanal.IdTerminal.Length > 6 ? datosCanal.IdTerminal.Substring(0,6) : datosCanal.IdTerminal;
            datos.debtorId = datosCanal.IdDeudor;
            datos.debtorName = datosCanal.NombreDeudor;
            datos.debtorTypeOfPerson = datosCanal.TipoPersonaDeudor; 
            datos.channel = datosCanal.Canal;
            datos.creditorCCI = datosCanal.AcreedorCCI;
            datos.creditorCreditCard = datosCanal.TarjetaCreditoAcreedor;
            datos.debtorParticipantCode = ParametroGeneralTransferencia.CodigoEntidadOriginante;
            return datos;
        }
        /// <summary>
        /// Traduccion de los campos recibidos de la CCE
        /// </summary>
        /// <param name="datos">Datos recibidos de la CCE</param>
        /// <returns>Consulta de cuenta traducido</returns>
        public static ConsultaCuentaRespuestaTraducidoDTO TraduccionConsultaCuenta(
            ConsultaCuentaRecepcionSalidaDTO datos)
        {
            return new ConsultaCuentaRespuestaTraducidoDTO
            {
                CodigoEntidadOriginante=datos.debtorParticipantCode,  
                CodigoEntidadReceptora=datos.creditorParticipantCode, 
                FechaCreacionTransaccion=datos.creationDate,       
                HoraCreacionTransaccion=datos.creationTime,
                IdentificadorTerminal=datos.terminalId,
                NumeroReferencia=datos.retrievalReferenteNumber,     
                Trace=datos.trace,     
                NombreDeudor=datos.debtorName ?? string.Empty,                   
                NumeroDocumentoDeudor=datos.debtorId,       
                TipoDocumentoDeudor=datos.debtorIdCode,
                TelefonoDeudor=datos.debtorPhoneNumber, 
                NumeroCelularDeudor=datos.debtorMobileNumber,        
                TipoTransaccion=datos.transactionType, 
                Canal=datos.channel, 
                IdentificadorTransaccion=datos.instructionId,
                NombreCompletoReceptor=datos.creditorName,               
                DireccionReceptor=datos.creditorAddressLine,                
                NumeroDocuementoReceptor=datos.creditorId,     
                TipoDocumentoReceptor=datos.creditorIdCode,
                TelefonoReceptor=datos.creditorPhoneNumber,      
                NumeroCelularReceptor=datos.creditorMobileNumber,        
                CodigoCuentaInterbancariaReceptor=datos.creditorCCI,                 
                CodigoTarjetaCreditoReceptor=datos.creditorCreditCard,               
                IndicadorITF=datos.sameCustomerFlag,
                CodigoMoneda=datos.currency,
                ValorProxy=datos.proxyValue,
                TipoProxy=datos.proxyType
            };
        }

        /// <summary>
        /// Extension para mapear el cuepro de la consulta cuenta del cliente originante
        /// </summary>
        /// <param name="cuentaEfectivo">Cuenta Efectivo del cliente</param>
        /// <param name="documento">Indicaro Remunerativo</param>
        /// <returns>Resultado de la cuenta cliente originante</returns>
        public static CuentaEfectivoDTO ACuentaEfectivoDTO(
            this CuentaEfectivo cuentaEfectivo,
            DocumentoCliente documento)
        {
            return new CuentaEfectivoDTO()
            {
                NumeroCuenta = cuentaEfectivo.NumeroCuenta,
                Titular = cuentaEfectivo.Cliente.NombreCliente,
                TipoProductoInterno = cuentaEfectivo.CodigoProducto
                    + " - " + cuentaEfectivo.Producto.NombreProducto,
                TipoCuentaTitular = cuentaEfectivo.IndicadorTipoAsociacion
                    + " - " + cuentaEfectivo.TipoCuentaGrupo.Descripcion,
                Moneda = cuentaEfectivo.CodigoMoneda + " - " + cuentaEfectivo.Moneda.NombreMoneda,
                ExoneradoImpuestos = cuentaEfectivo.EsExoneradaImpuestos,
                CodigoCliente = cuentaEfectivo.CodigoCliente,
                CodigoProducto = cuentaEfectivo.CodigoProducto,
                EsExoneradoCobroComisiones = cuentaEfectivo.EsExoneradaComisiones,
                CodigoMoneda = cuentaEfectivo.CodigoMoneda,
                SimboloMonedaProducto = cuentaEfectivo.SimboloMoneda!,
                CodigoCuentaInterbancario = cuentaEfectivo.CodigoCuentaInterbancario!,
                IndicadorTipoCuenta = cuentaEfectivo.IndicadorTipoAsociacion ?? string.Empty,
                Nombres = cuentaEfectivo.Cliente.Nombres,
                ApellidoMaterno = cuentaEfectivo.Cliente.ApellidoMaterno,
                ApellidoPaterno = cuentaEfectivo.Cliente.ApellidoPaterno,
                IndicadorTransferenciaCce = cuentaEfectivo.Caracteristicas.IndTransfCCETIN,
                SaldoDisponible = cuentaEfectivo.SaldoDisponible,
                NumeroDocumento = documento.NumeroDocumento,
                TipoDocumentoOriginante = new TipoDocumentoTinDTO()
                {
                    CodigoTipoDocumento = Convert.ToByte(cuentaEfectivo.Cliente.CodigoTipoDocumento),
                    EsTipoPersonaJuridica = cuentaEfectivo.Cliente.EsClienteJuridico
                },
                MontoMinimo = cuentaEfectivo.Caracteristicas.MontoMinimoSaldo,
                EsCuentaSueldo = cuentaEfectivo.EsCuentaSueldo,
                EsExoneradaITF = cuentaEfectivo.EsExoneradaITF,
                NombreProducto = cuentaEfectivo.Producto.NombreProducto,
                Alias = cuentaEfectivo.ConvertirAliasProductoPasivo()
            };
        }

        /// <summary>
        /// Mapea los datos de tipo documento CCE
        /// </summary>
        /// <param name="tiposDocumentoCce">tipos de documento</param>
        /// <returns>Lista de tipos de documentos</returns>
        public static List<TipoDocumentoTinDTO> DatosTipoDocumento(
            this List<TipoDocumento> tiposDocumentoCce)
        {
            return tiposDocumentoCce.Select(
                t => new TipoDocumentoTinDTO
                {
                    CodigoTipoDocumento = Convert.ToByte(t.CodigoTipoDocumento),
                    CodigoTipoDocumentoCCE = Convert.ToByte(t.CodigoTipoDocumentoInmediataCce),
                    CodigoTipoDocumentoCceTransferenciasInmediatas = t.CodigoTipoDocumentoInmediataCce,
                    DescripcionTipoDocumento = t.DescripcionTipoDocumento,
                    EsTipoPersonaJuridica = t.IndicadorPersona == Cliente.TipoPersonaJuridica,
                    LongitudDocumentoCCE = t.LongitudDocumentoCCE
                }).ToList();
        }

        /// <summary>
        /// Maquea el Vinculo y Motivo
        /// </summary>
        /// <param name="motivos"></param>
        /// <param name="vinculos"></param>
        /// <returns></returns>
        public static VinculosMotivosDTO AVinculoMotivo(
           this List<MotivoMovimiento> motivos,
           List<VinculoMovimiento> vinculos)
        {
            return new VinculosMotivosDTO()
            {
                Motivos = motivos.Select(s => new VinculoMotivoDTO()
                {
                    IdVinculoMotivo = s.IdMotivoMovimiento,
                    Especificar = s.IndicadorEspecificar,
                    Nombre = s.Descripcion
                }).ToList(),
                Vinculos = vinculos.Select(s => new VinculoMotivoDTO()
                {
                    IdVinculoMotivo = s.IdVinculoMovimiento,
                    Especificar = s.IndicadorEspecificar,
                    Nombre = s.Descripcion
                }).ToList()
            };
        }

        /// <summary>
        /// Maqueta los limites de transferencia
        /// </summary>
        /// <param name="limites"></param>
        /// <returns></returns>
        public static List<LimiteTransferenciaDTO> ALimitesTransferencias(
            this List<LimiteTransferenciaInmediata> limites)
        {
            return limites.Select(x => new LimiteTransferenciaDTO
            {
                CodigoTipoTransferencia = x.TipoTransferencia.Codigo,
                CodigoMoneda = x.CodigoMoneda,
                MontoMaximo = x.MontoLimiteMaximo,
                MontoMinimo = x.MontoLimiteMinimo
            }).ToList();
        }

        /// <summary>
        /// Método de convertir de alias de producto pasivo
        /// </summary>
        /// <param name="cuentaEfectivo"></param>
        /// <returns></returns>
        public static string ConvertirAliasProductoPasivo(this CuentaEfectivo cuentaEfectivo)
        {
            if (!string.IsNullOrEmpty(cuentaEfectivo.AliasProductoCliente?.NombreAlias))
                return cuentaEfectivo.AliasProductoCliente.NombreAlias;

            return $"{cuentaEfectivo.Producto.NombreProducto.ToUpper()} DE {cuentaEfectivo.Cliente.Nombres.PrimeraPalabra()}";
        }
    }
}