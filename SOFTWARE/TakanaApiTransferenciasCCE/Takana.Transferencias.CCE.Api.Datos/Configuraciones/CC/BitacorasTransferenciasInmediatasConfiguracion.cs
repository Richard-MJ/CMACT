using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;

namespace Takana.Transferencias.CCE.Api.Datos.Configuraciones.CC
{
    /// <summary>
    /// Clase que representa el mapeo de la clase de Dominio BitacoraTransferenciaInmediata con la tabla CC_BITACORAS_TRANSFERENCIAS_INMEDIATAS
    /// </summary>
    public class BitacoraTransferenciaInmediataConfiguracion : IEntityTypeConfiguration<BitacoraTransferenciaInmediata>
    {
        public void Configure(EntityTypeBuilder<BitacoraTransferenciaInmediata> builder)
        {
            builder.ToTable("CC_BITACORAS_TRANSFERENCIAS_INMEDIATAS", "CC");
            builder.HasKey(m => new { m.NumeroBitacora });
            
            builder.Property(p => p.NumeroBitacora).HasColumnName("NUM_BITACORA");
            builder.Property(p => p.IndicadorBitacora).HasColumnName("IND_BITACORA");
            builder.Property(p => p.IdentificadorTrama).HasColumnName("ID_TIPO_TRAMA");
            builder.Property(p => p.TipoTransferencia).HasColumnName("TIP_TRANSFERENCIA");
            builder.Property(p => p.CodigoEntidadOriginante).HasColumnName("COD_ENTIDAD_ORIGINANTE");
            builder.Property(p => p.CodigoEntidadReceptor).HasColumnName("COD_ENTIDAD_RECEPTOR");
            builder.Property(p => p.FechaBitacoraOperacion).HasColumnName("FEC_BITACORA_OPERACION_CCE");
            builder.Property(p => p.FechaBitacoraRespuesta).HasColumnName("FEC_BITACORA_RESPUESTA_CCE");
            builder.Property(p => p.FechaLiquidacion).HasColumnName("FEC_LIQUIDACION");
            builder.Property(p => p.NumeroTrace).HasColumnName("NUM_TRACE");
            builder.Property(p => p.CodigoCuentaInterbancariaOriginante).HasColumnName("COD_CUENTA_INTERBANCARIA_ORIGINANTE");
            builder.Property(p => p.NombreOriginante).HasColumnName("NOM_ORIGINANTE");
            builder.Property(p => p.TipoPersonaOriginante).HasColumnName("TIP_PERSONA_ORIGINANTE");
            builder.Property(p => p.NumeroDocumentoOriginante).HasColumnName("NUM_DOCUMENTO_ORIGINANTE");
            builder.Property(p => p.TipoDocumentoOriginante).HasColumnName("TIP_DOCUMENTO_ORIGINANTE");
            builder.Property(p => p.TelefonoOriginante).HasColumnName("TEL_ORIGINANTE");
            builder.Property(p => p.DireccionOriginante).HasColumnName("DES_DIRECCION_ORIGINANTE");
            builder.Property(p => p.CelularOriginante).HasColumnName("TEL_CELULAR_ORIGINANTE");
            builder.Property(p => p.CodigoCuentaInterbancariaReceptor).HasColumnName("COD_CUENTA_INTERBANCARIA_RECEPTOR");
            builder.Property(p => p.NombreReceptor).HasColumnName("NOM_RECEPTOR");
            builder.Property(p => p.NumeroDocumentoReceptor).HasColumnName("NUM_DOCUMENTO_RECEPTOR");
            builder.Property(p => p.TipoDocumentoReceptor).HasColumnName("TIP_DOCUMENTO_RECEPTOR");
            builder.Property(p => p.DireccionReceptor).HasColumnName("DES_DIRECCION_RECEPTOR");
            builder.Property(p => p.TelefonoReceptor).HasColumnName("TEL_RECEPTOR");
            builder.Property(p => p.CelularReceptor).HasColumnName("TEL_CELULAR_RECEPTOR");
            builder.Property(p => p.NumeroTarjetaReceptor).HasColumnName("COD_NUMERO_TARJETA");
            builder.Property(p => p.IdentificadorReferenciaTransaccion).HasColumnName("ID_REFERENCIA_TRANSACCION");
            builder.Property(p => p.ReferenciaTransaccion).HasColumnName("DES_REFERENCIA_TRANSACCION");
            builder.Property(p => p.CodigoNumeroReferencia).HasColumnName("COD_NUMERO_REFERENCIA");
            builder.Property(p => p.CodigoRespuesta).HasColumnName("COD_RESPUESTA");
            builder.Property(p => p.RazonRespuesta).HasColumnName("COD_RAZON");
            builder.Property(p => p.MensajeReenvio).HasColumnName("COD_MENSAJE_REENVIO");
            builder.Property(p => p.CodigoMoneda).HasColumnName("COD_MONEDA");
            builder.Property(p => p.ValorProxy).HasColumnName("DES_VAL_PROXY");
            builder.Property(p => p.TipoProxy).HasColumnName("DES_TIP_PROXY");
            builder.Property(p => p.MontoImporte).HasColumnName("MON_TRANSFERENCIA");
            builder.Property(p => p.MontoComision).HasColumnName("MON_COMISION");
            builder.Property(p => p.MontoLiquidacion).HasColumnName("MON_LIQUIDACION_INTERBANCARIA");
            builder.Property(p => p.MontoSalarioBruto).HasColumnName("SAL_BRUTO_CANTIDAD");
            builder.Property(p => p.IndicadorITF).HasColumnName("IND_ITF");
            builder.Property(p => p.CodigoTarifa).HasColumnName("COD_TARIFA");
            builder.Property(p => p.CodigoCriterioAplicacion).HasColumnName("COD_CRITERIO_APLICACION");
            builder.Property(p => p.CodigoCanal).HasColumnName("COD_CANAL");
            builder.Property(p => p.CodigoProposito).HasColumnName("COD_PROPOSITO");
            builder.Property(p => p.DescripcionInformacionEstructurada).HasColumnName("DES_INFORMACION_ESTRUCTURADA");
            builder.Property(p => p.CodigoTerminal).HasColumnName("COD_TERMINAL");
            builder.Property(p => p.IndicadorPagoSalario).HasColumnName("IND_PAGO_SALARIO");
            builder.Property(p => p.MesPago).HasColumnName("MES_PAGO");
            builder.Property(p => p.AnioPago).HasColumnName("ANIO_PAGO");
            builder.Property(p => p.IdentificadorInstruccion).HasColumnName("ID_INSTRUCCION");
            builder.Property(p => p.IdentificadorRama).HasColumnName("ID_RAMA");
            builder.Property(p => p.CodigoSistema).HasColumnName("COD_SISTEMA");
            builder.Property(p => p.DescripcionMensajeOriginal).HasColumnName("DES_MENSAJE_ORIGINAL");
            builder.Property(p => p.DescripcionDefinicionMensaje).HasColumnName("DES_DEFINICION_MENSAJE");
            builder.Property(p => p.IndicadorConsultaQR).HasColumnName("IND_CONSULTA_QR");
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO");
            builder.Property(p => p.CodigoUsuarioRegistro).HasColumnName("COD_USUARIO_REGISTRO");
            builder.Property(p => p.CodigoUsuarioModifico).HasColumnName("COD_USUARIO_MODIFICO");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO");
            builder.Property(p => p.FechaModifico).HasColumnName("FEC_MODIFICO");

            builder.HasOne(m => m.TipoTrama).WithMany().HasForeignKey(m => m.IdentificadorTrama);
        }
    }
}