namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
public class ParametroGeneralTransferencia
{
    #region Constantes
    /// <summary>
    /// Codigo de reintentos maximos
    /// </summary>
    public const string CodigoSistemaTINCCE = "IPS";
    /// <summary>
    /// Codigo de reintentos maximos
    /// </summary>
    public const string MaximoReintento = "MAX";
    /// <summary>
    /// Codigo de reintentos minimos
    /// </summary>
    public const string MinimoReintento = "MIN";
    /// <summary>
    /// Transaccion de completa
    /// </summary>
    public const string TransaccionCompleta = "S/N";
    /// <summary>
    /// Verificar revocacion de certificado de la CCE
    /// </summary>
    public const string RevocacionCertificadoCCE = "CERT_CCE";
    /// <summary>
    /// Codigo de tiempo de reintentos
    /// </summary>
    public const string TiempoReintento = "TIME";
    /// <summary>
    /// Codigo de entidad Caja Tacna 
    /// </summary>
    public const string CodigoEntidadOriginanteTakana = "813";
    /// <summary>
    /// Codigo de entidad Caja Tacna version CCE
    /// </summary>
    public const string CodigoEntidadOriginante = "0813";
    /// <summary>
    /// Codigo de estado de sistema CCE
    /// </summary>
    public const string EstadoSistema = "IPS";
    /// <summary>
    /// Monto minimo
    /// </summary>
    public const string MontoMinimo = "MIN";
    /// <summary>
    /// Hora para enviar los reportes
    /// </summary>
    public const string HoraEnvioReportes = "HOR_ENVIAR_REPORTES";
    /// <summary>
    /// Correo Electronico del administrador
    /// </summary>
    public const string CorreoElectronicoAdministrador = "EMAIL_ADMIN";
    /// <summary>
    /// Correo Electronico del administrador
    /// </summary>
    public const string CorreoElectronicoDestinariosERRFIRMA = "EMAIL_DESTINARIO_ERRFIRMA";
    /// <summary>
    /// Correos Electronicos de los destinarios de reportes
    /// </summary>
    public const string CorreoElectronicoDestinatariosReporte = "EMAIL_DESTINATARIO_REPORTE";
    /// <summary>
    /// Correos Electronicos de los destinarios de notificaciones
    /// </summary>
    public const string CorreoElectronicoDestinatariosNotificacion = "EMAIL_DESTINATARIO_NOTIFICACION";
    /// <summary>
    /// Codigo de maximo por transferencia de entrantes soles
    /// </summary>
    public const string MaximoPorTransferenciaEntranteSoles = "MAX_TRANS_ENTRANTE_SOLES";
    /// <summary>
    /// Codigo de transferencia por dia de entrante - monto total
    /// </summary>
    public const string MaximoPorDiaTransferenciaEntranteSoles = "MAX_TRANS_DIA_ENTRANTE_SOLES";

    #endregion

    #region Propiedades

    /// <summary>
    /// Identificador del parametro
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// Codigo del parametro
    /// </summary>
    public string CodigoParametro { get; private set; }
    
    /// <summary>
    /// Nombre o descripcion del parametroA
    /// </summary>
    public string DescripcionParametro { get; private set; }
    /// <summary>
    /// Valor del parametro
    /// </summary>
    public string ValorParametro { get; private set; }
    /// <summary>
    /// IndicadorEstado
    /// </summary>
    public char IndicadorEstado { get; private set; }
    /// <summary>
    /// Indica la operacion de transferencia inmediata u Interoperabilidad
    /// </summary>
    public char IndicadorOperacion { get; private set; }
    /// <summary>
    /// Codigo Moneda
    /// </summary>
    public string? CodigoMoneda { get; private set; }

    #endregion Propiedades

    #region Métodos
    /// <summary>
    /// Método que actualiza el valor del parametro
    /// </summary>
    /// <param name="valor"></param>
    public void ActualizarValorParametro(string valor)
    {
        ValorParametro = valor;
    }
    #endregion
}
