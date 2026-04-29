namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica
{
    /// <summary>
    /// Clase que valida el CCI
    /// </summary>
    public class CodigoCuentaInterbancario
    {
        #region Propiedades
        /// <summary>
        /// Codigo de la entidad
        /// </summary>
        public string CodigoEntidad { get; private set; }
        /// <summary>
        /// Codigo de la oficina
        /// </summary>
        public string CodigoOficina { get; private set; }
        /// <summary>
        /// Numero de la cuenta
        /// </summary>
        public string NumeroCuenta { get; private set; }
        /// <summary>
        /// Digito validor de l entidad
        /// </summary>
        public string DigitoValidadorEntidadOficina { get; private set; }
        /// <summary>
        /// Digito validado de la cuenta
        /// </summary>
        public string DigitoValidadorCuenta { get; private set; }

        #endregion

        #region 
        /// <summary>
        /// Valida el codigo de cuenta inter
        /// </summary>
        /// <param name="entidad">Entidad</param>
        /// <param name="oficina">Oficina</param>
        /// <param name="cuenta">Cuenta</param>
        private CodigoCuentaInterbancario(string entidad, string oficina, string cuenta)
        {
            var cantidadValida = 3;
            var codigoOficinaValido = 12;
            var modulo = 10;
            if (string.IsNullOrEmpty(entidad)
                || entidad.Trim().Length != cantidadValida
                || !entidad.All(char.IsDigit))
                throw new Exception("El Código de Entidad ingresado es invalido.");

            if (string.IsNullOrEmpty(oficina)
                || oficina.Trim().Length != cantidadValida
                || !oficina.All(char.IsDigit))
                throw new Exception("El Código de Oficina ingresado es invalido.");

            if (string.IsNullOrEmpty(cuenta)
                || cuenta.Trim().Length != codigoOficinaValido
                || !cuenta.All(char.IsDigit))
                throw new Exception("El Código de Oficina ingresado es invalido.");

            CodigoEntidad = entidad;
            CodigoOficina = oficina;
            NumeroCuenta = cuenta;
            DigitoValidadorEntidadOficina = ObtenerDigitoValidador(entidad + oficina, modulo).ToString();
            DigitoValidadorCuenta = ObtenerDigitoValidador(cuenta, modulo).ToString();

        }

        #endregion

        #region Métodos
        /// <summary>
        /// Genera el codigo de cuenta interbancario valido
        /// </summary>
        /// <param name="cci">codigo de cuenta interbancario</param>
        /// <returns>Retorna el codigo de cuenta generado</returns>
        public static CodigoCuentaInterbancario Generar(string cci)
        {
            var cantidadDigitosCCI = 18;
            if (string.IsNullOrEmpty(cci)
                || cci.Trim().Length != cantidadDigitosCCI
                || !cci.All(char.IsDigit))
                throw new Exception("El CCI ingresado es invalido.");
            return new CodigoCuentaInterbancario(cci.Substring(0, 3), cci.Substring(3, 3), cci.Substring(6, 12));
        }
        /// <summary>
        /// Obitene el digito validador del CCI
        /// </summary>
        /// <param name="valor">Valor</param>
        /// <param name="modulo">Modulo</param>
        /// <returns>Retorna el digito validor</returns>
        private int ObtenerDigitoValidador(string valor, int modulo)
        {
            int total = 0;
            int valorResultante = 0;
            int valorValidoResultante = 9;
            char[] arrreglo = valor.ToCharArray();
            for (int i = arrreglo.Length - 1; i > -1; i--)
            {
                if (i % 2 != 0)
                {
                    valorResultante = int.Parse(arrreglo[i].ToString()) * 2;
                    if (valorResultante > valorValidoResultante)
                        total += valorResultante.ToString().Sum(d => int.Parse(d.ToString()));
                    else
                        total += valorResultante;
                }
                else
                    total += int.Parse(arrreglo[i].ToString());
            }
            return total % modulo == 0 ? 0 : modulo - total % modulo;
        }

        #endregion

    }
}
