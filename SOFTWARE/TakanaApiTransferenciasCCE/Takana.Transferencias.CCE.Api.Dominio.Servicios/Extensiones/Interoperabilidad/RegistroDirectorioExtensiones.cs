using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones.Interoperabilidad;

public static class RegistroDirectorioExtensiones
{
    /// <summary>
    /// Maqueta los datos a la estructura requerida por la CCE
    /// </summary>
    /// <param name="datosEntrada">Datos de entrada</param>
    /// <param name="idTramoMensajeCabecera">Identificador del mensaje de cabecera</param>
    /// <param name="fechaHoraCreacion">fecha del sistema</param>
    /// <param name="idTramoMensajeDocumentoId">Identificador del mensaje de documento</param>
    /// <param name="idTramoMensajeDocumento">Identificador del mensaje de documento Id</param>
    /// <param name="fechaHoraCreacionDocumento">Fecha del sistema</param>
    /// <returns>Datos maquetados</returns>
    public static EstructuraRegistroDirectorio ArmarDatos(
            EntradaAfiliacionDirectorioDTO datosEntrada,
            string idTramoMensajeCabecera,
            string fechaHoraCreacion,
            string idTramoMensajeDocumentoId,
            string idTramoMensajeDocumento,
            string fechaHoraCreacionDocumento)
    {
        AppHdr appHdr = new AppHdr
        {
            Fr = new Fr
            {
                FIId = new FIId
                {
                    FinInstnId = new FinInstnId
                    {
                        Othr = new Othr
                        {
                            Id = datosEntrada.CodigoEntidadOriginante == null
                                            ? ParametroGeneralTransferencia.CodigoEntidadOriginante
                                            : datosEntrada.CodigoEntidadOriginante
                        }
                    }
                }
            },
            To = new To
            {
                FIId = new FIId
                {
                    FinInstnId = new FinInstnId
                    {
                        Othr = new Othr { Id = DatosValoresFijos.IdRegistro }
                    }
                }
            },
            BizMsgIdr = idTramoMensajeCabecera,
            MsgDefIdr = DatosValoresFijos.MsgDefIdrRegistro,
            CreDt = fechaHoraCreacion,
        };

        Document document = new Document
        {
            PrxyRegn = new PrxyRegn
            {
                GrpHdr = new GrpHdr
                {
                    MsgId = idTramoMensajeDocumento,
                    CreDtTm = fechaHoraCreacionDocumento,
                    MsgSndr = new MsgSndr
                    {
                        Agt = new Agt
                        {
                            FinInstnId = new FinInstnId
                            {
                                Nm = datosEntrada.CodigoCuentaInterbancario,
                                Othr = new Othr
                                {
                                    
                                    Id = datosEntrada.CodigoEntidadOriginante == null
                                                    ? ParametroGeneralTransferencia.CodigoEntidadOriginante
                                                    : datosEntrada.CodigoEntidadOriginante
                                }
                            }
                        }
                    }
                },
                Regn = new Regn
                {
                    RegnTp = datosEntrada.TipoInstruccion,
                    Prxy = new Prxy
                    {
                        Tp = DatosValoresFijos.ValorIdDocumentoRegistro,
                        Val = "+51" + datosEntrada.NumeroCelular
                    },
                    PrxyRegn = new PrxyRegn
                    {
                        RegnId = idTramoMensajeDocumentoId,
                    },
                    RegnSubTp = "ACCT"
                }
            }
        };
        return new EstructuraRegistroDirectorio()
        {
            BusMsg = new BusMsg
            {
                AppHdr = appHdr,
                Document = document
            }
        };
    }
    /// <summary>
    /// Traduce la respuesta de la CCE a lo entendible para takana
    /// </summary>
    /// <param name="resultadoCce">Resultado de la CCE</param>
    /// <param name="numeroSeguimiento">Numero de seguimiento</param>
    /// <returns>Retorna datos traducoindos</returns>
    public static RespuestaRegistroDirectorioDTO TraduccionRespuesta(
        this EstructuraRegistroDirectorio resultadoCce,
        string numeroSeguimiento)
    {
        var codigoRespuesta = resultadoCce.BusMsg.Document.PrxyRegnRspn.RegnRspn.PrxRspnSts;
        return new RespuestaRegistroDirectorioDTO
        {
            Respuesta = resultadoCce.BusMsg.Document.PrxyRegnRspn.RegnRspn.PrxRspnSts,
            RazonRespuesta = resultadoCce.BusMsg.Document.PrxyRegnRspn.RegnRspn.PrxRspnSts == DatosGeneralesInteroperabilidad.Rechazado
                ? resultadoCce.BusMsg.Document.PrxyRegnRspn.RegnRspn.StsRsnInf.Prtry : codigoRespuesta,
            NumeroSeguimiento = numeroSeguimiento
        };
    }

}
