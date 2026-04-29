namespace PagareElectronico.Infraestructura.Logging
{
    /// <summary>
    /// Contrato para registrar eventos en bitácora.
    /// </summary>
    /// <typeparam name="T">Tipo asociado al origen del log.</typeparam>
    public interface IBitacora<T> where T : class
    {
        void Debug(string mensaje, params object[] argumentos);
        void Info(string mensaje, params object[] argumentos);
        void Trace(string mensaje, params object[] argumentos);
        void Warn(string mensaje, params object[] argumentos);
        void Error(string mensaje, params object[] argumentos);
        void Error(Exception excepcion, string mensaje, params object[] argumentos);
        void Fatal(string mensaje, params object[] argumentos);
        void Fatal(Exception excepcion, string mensaje, params object[] argumentos);
    }
}