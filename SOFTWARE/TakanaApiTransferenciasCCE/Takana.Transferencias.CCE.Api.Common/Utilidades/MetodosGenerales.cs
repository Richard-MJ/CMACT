namespace Takana.Transferencias.CCE.Api.Common.Utilidades
{
    /// <summary>
    /// Representan los metodos genericos utilizados por toda la aplicacion.
    /// </summary>
    public static class MetodosGenerales
    {
        /// <summary>
        /// Asigna un tipo a una variable generica.
        /// </summary>
        /// <param name="input">Variable de entrada.</param>
        /// <typeparam name="T">Tipo a asignar.</typeparam>
        /// <returns>Variable tipificada.</returns>
        public static T? ConvertirObjetoATipoVariable<T>(object input)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T));
            }
            catch (Exception excepcion)
            {
                Console.WriteLine($"Error al convertir el valor '{input}' a {typeof(T)}. {excepcion.Message}");
                return default;
            }
        }
        
        /// <summary>
        /// Método que obtiene el indicador de ITF
        /// </summary>
        /// <param name="NumeroDocumentoOriginante"></param>
        /// <param name="CodigoTipoDocumentoOriginante"></param>
        /// <param name="NumeroDocumentoReceptor"></param>
        /// <param name="CodigoTipoDocumentoReceptor"></param>
        /// <returns></returns>
        public static string ObtenerIndicadorITF(
            string NumeroDocumentoOriginante,
            string CodigoTipoDocumentoOriginante,
            string NumeroDocumentoReceptor,
            string? CodigoTipoDocumentoReceptor)
        {
            var CodigoITF = "O";
            
            if(NumeroDocumentoReceptor.Equals(NumeroDocumentoOriginante) 
            && CodigoTipoDocumentoReceptor.Equals(CodigoTipoDocumentoOriginante))
                CodigoITF = "M";
                
            return CodigoITF;
        }
    }
}
