using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones.Interoperabilidad;
/// <summary>
/// Clase extension para barrido de contactos de interoperabilidad
/// </summary>
public static class BarridoContactoExtensiones
{
    /// <summary>
    /// Maqueta los datos a una estructura requerida por la CCE
    /// </summary>
    /// <param name="datosEntrada">Datos de entrada</param>
    /// <param name="fechaCreacion">fecha de creacion</param>
    /// <param name="idInstruccion">Identificador de instruccion</param>
    /// <param name="listaCelulares">Lista de celulares</param>
    /// <returns>Retorna estructura del mensaje</returns>
    public static EstructuraBarridoContacto ArmarDatos(
        string fechaCreacion,
        string idInstruccion,
        Proxy[] listaCelulares
        )
    {
        AppHdr appHdr = new AppHdr()
        {
            Fr = new Fr
            {
                FIId = new FIId
                {
                    FinInstnId = new FinInstnId
                    {
                        Othr = new Othr() { Id = ParametroGeneralTransferencia.CodigoEntidadOriginante }
                    }
                }
            },
            To = new To
            {
                FIId = new FIId
                {
                    FinInstnId = new FinInstnId
                    {
                        Othr = new Othr() { Id = DatosValoresFijos.IdBarrido }
                    }
                }
            },
            BizMsgIdr = DatosValoresFijos.Barrido,
            MsgDefIdr = DatosValoresFijos.MsgDefIdrBarrido,
            CreDt = fechaCreacion
        };

        Document document = new Document
        {
            PrxyLookUp = new PrxyLookUp
            {
                GrpHdr = new GrpHdr
                {
                    MsgId = DatosValoresFijos.Barrido,
                    CreDtTm = fechaCreacion,
                    MsgSndr = new MsgSndr
                    {
                        Agt = new Agt
                        {
                            FinInstnId = new FinInstnId
                            {
                                Othr = new Othr { Id = ParametroGeneralTransferencia.CodigoEntidadOriginante }
                            }
                        }
                    }
                },
                LookUp = new LookUp
                {
                    PrxyOnly = new PrxyOnly
                    {
                        LkUpTp = DatosValoresFijos.LkUpTpBarrido,
                        Id = idInstruccion,
                        PrxyRtrvl = new PrxyRtrvl
                        {
                            Tp = DatosValoresFijos.TpBarrido,
                            Val = DatosValoresFijos.ValorDummy
                        }
                    }
                },
                SplmtryData = new SplmtryData
                {
                    Envlp = new Envlp
                    {
                        Proxy = listaCelulares
                    }
                }
            }
        };

        return new EstructuraBarridoContacto()
        {
            BusMsg = new BusMsg
            {
                AppHdr = appHdr,
                Document = document
            }
        };
    }
    /// <summary>
    /// Metodo que traduce la respuesta
    /// </summary>
    /// <param name="resultadoCce">Resultado de la CCE</param>
    /// <returns>Campos traducidos para uso interno</returns>
    public static RespuestaBarridoDTO TraduccionRespuesta(
        this EstructuraBarridoContacto resultadoCce,
        string numeroSeguimiento)
    {
        var respuesta = resultadoCce.BusMsg.Document.PrxyLookUpRspn.LkUpRspn.RegnRspn.PrxRspnSts;
        return new RespuestaBarridoDTO
        {
            CodigoRespuesta = respuesta,
            RazonRespuesta = respuesta == DatosGeneralesInteroperabilidad.Rechazado
                ? resultadoCce.BusMsg.Document.PrxyLookUpRspn.LkUpRspn.RegnRspn?.StsRsnInf?.Prtry : respuesta,
            NumeroSeguimiento = numeroSeguimiento,
            Directorios = respuesta == DatosGeneralesInteroperabilidad.Aceptado
                ? resultadoCce.BusMsg.Document.SplmtryData?.Envlp?.Directories : null
        };
    }

}

