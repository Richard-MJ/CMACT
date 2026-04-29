namespace Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;

public class CodigoRespuesta
{
    #region Constantes
    /// <summary>
    /// Respuesta Aceptada
    /// </summary>
    public const string Aceptada = "00";
    /// <summary>
    /// Respuesta Rechazada
    /// </summary>
    public const string Rechazada = "05";
    /// <summary>
    /// Tiempo de esperar respuesta CCE
    /// </summary>
    public const string TiempoEspera = "DS24";
    /// <summary>
    /// Tiempo de esperar respuesta CCE
    /// </summary>
    public const string ErrorCCE = "R";
    /// <summary>
    /// Respuesta No encontrada
    /// </summary>
    public const string NoEncontrada = "99";
    /// <summary>
    /// Respuesta Positiva Json(200)
    /// </summary>
    public const string RepuestaPositiva = "200";
    /// <summary>
    /// Respuesta Rechazada
    /// </summary>
    public const string AceptadoInteroperabilidad = "0";
    /// <summary>
    /// Indica que no existen ninguna error en la validacion
    /// </summary>
    public const string codigo0000 = "0000";
    /// <summary>
    /// Error CMAC en el barrido
    /// </summary>
    public const string ErrorEnBarrido = "ERRCMACBAR";
    /// <summary>
    /// Error afiliación con la CCE
    /// </summary>
    public const string ErrorAfiliacion = "ERRAFILI";
    /// <summary>
    /// Error desafiliación con la CCE
    /// </summary>
    public const string ErrorDesafiliacion = "ERRDESAFIL";

    #endregion Constantes  

    #region Propiedades
    /// <summary>
    /// Codigo
    /// </summary>
    public string Codigo { get; private set; }
    /// <summary>
    /// Nombre del codigo
    /// </summary>
	public string Nombre { get; private set; }
    /// <summary>
    /// Descripcion del codigo
    /// </summary>
    public string? Descripcion { get; private set; }
    /// <summary>
    /// Mensaje que se visualizara al cliente
    /// </summary>
    public string? MensajeCliente { get; private set; }
    /// <summary>
    /// Descripcion del estado de cuenta
    /// </summary>
    public string DescripcionEstadoCuenta { get; private set; }
    /// <summary>
    /// Tipo de codigo de respuesta
    /// </summary>
    public string TipoCodigoRespuesta { get; private set; }

    #endregion

    #region Metodos
    public void AgregarRespuesta(string codigo, string nombre, string descripcion)
    {
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
    }
    #endregion
}
