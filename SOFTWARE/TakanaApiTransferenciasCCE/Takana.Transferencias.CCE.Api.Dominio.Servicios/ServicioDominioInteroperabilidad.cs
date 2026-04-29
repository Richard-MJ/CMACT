using System.Text.RegularExpressions;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Dominio.Servicios.Extensiones.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios
{
    /// <summary>
    /// Clase de dominio que se encarga de la interoperababilidad
    /// </summary>
    public static class ServicioDominioInteroperabilidad
    {
        #region Metodos de maquetacion
        /// <summary>
        /// Metodo encargadado de maquetar la el formato para directorio
        /// </summary>
        /// <param name="datosEntrada">Datos de entrada</param>
        /// <param name="fechaSistema">Fecha del sistema</param>
        /// <param name="numeroSeguimiento">Numeor de seguimiento</param>
        /// <returns>Retorna estructura de registro de directorio</returns>
        public static EstructuraRegistroDirectorio MaquetarDatos(
             this EntradaAfiliacionDirectorioDTO datosEntrada,
             Calendario fechaSistema,
             string numeroSeguimiento)
        {
            try
            {
                var idTramoMensajeDocumentoId = "";

                var fechaTramo = fechaSistema.FechaFormato + fechaSistema.HoraFormato;

                var tramoComun = fechaTramo
                    + datosEntrada.CodigoEntidadOriginante
                    + DatosValoresFijos.RequerimientoRegistro
                    + numeroSeguimiento;

                var idTramoMensajeCabecera = DatosValoresFijos.ValorIdCabeceraRegistro + tramoComun;
                var fechaHoraCreacion = fechaSistema.FechaInteroperabilidadFormato;

                if (datosEntrada.TipoInstruccion != DatosValoresFijos.EliminarRegistro && datosEntrada.CodigoCuentaInterbancario == null)
                    throw new Exception("Para este tipo de instruccion el CCI es obligatorio");

                var idTramoMensajeDocumento = DatosValoresFijos.ValorIdDocumentoRegistro + tramoComun;
                var fechaHoraCreacionDocumento = fechaSistema.FechaInteroperabilidadFormato;

                idTramoMensajeDocumentoId = DatosValoresFijos.ValorIdDocumentoIdRegistro
                    + fechaTramo
                    + datosEntrada.CodigoEntidadOriginante
                    + numeroSeguimiento;

                return RegistroDirectorioExtensiones.ArmarDatos(
                    datosEntrada,
                    idTramoMensajeCabecera,
                    fechaHoraCreacion,
                    idTramoMensajeDocumentoId,
                    idTramoMensajeDocumento,
                    fechaHoraCreacionDocumento
                );
            }
            catch (Exception)
            {
                throw new ValidacionException("Fallo al maquetar datos Afiliacion");
            }

        }
        /// <summary>
        /// Metodo que se encarga de maquetar la estructura de la CCE para barrido de contactos
        /// </summary>
        /// <param name="datosEntrada">Datos ede entrada</param>
        /// <param name="fechaSistema">fecha del sistema</param>
        /// <param name="numeroSeguimiento">Numero de seguimiento</param>
        /// <returns>Retorna datos maquetados para la CCE</returns>
        public static EstructuraBarridoContacto MaquetarDatos(
            this List<ContactosBarrido> datosEntrada,
            Calendario fechaSistema,
            string numeroSeguimiento)
        {
            var fechaCreacion = fechaSistema.FechaInteroperabilidadFormato;
            var idInstruccion = fechaSistema.FechaFormato
                + fechaSistema.HoraFormato
                + ParametroGeneralTransferencia.CodigoEntidadOriginante
                + DatosValoresFijos.ValorCuentaVerificacion
                + DatosValoresFijos.ValorRequest
                + DatosGeneralesInteroperabilidad.CanalInteroperabilidad
                + numeroSeguimiento;

            var listaCelulares = new Proxy[datosEntrada.Count];

            var contador = 0;

            foreach (var celular in datosEntrada)
            {
                listaCelulares[contador] = new Proxy() { Type = DatosValoresFijos.TypeBarrido, Value = celular.NumeroCelular };
                contador++;
            }

            return BarridoContactoExtensiones.ArmarDatos(
                fechaCreacion,
                idInstruccion,
                listaCelulares);
        }

        #endregion Metodos de maquetacion

        /// <summary>
        /// Filtra la respuesta del barrido de contactos
        /// </summary>
        /// <param name="resultadoBarridoCCE">Respuesta completa de la CCE</param>
        /// <returns>Retorna la respuesta de la CCE filtrada</returns>
        public static List<ResultadoBarridoDTO> FiltrarRespuesta(
            this Directories[] resultadoBarridoCCE,
            List<EntidadFinancieraTinDTO> listaEntidades,
            List<DirectorioInteroperabilidad> directorios)
        {
            if (resultadoBarridoCCE == null)
                throw new ValidacionException("No está afiliado a un directorio");

            var resultadoEnviarLista = new List<ResultadoBarridoDTO>();
            var entidadPorCodigo = listaEntidades.ToDictionary(e => e.CodigoEntidad);

            foreach (var directorio in resultadoBarridoCCE)
            {
                if (directorio.Proxy == null) continue;

                foreach (var item in directorio.Proxy)
                {
                    if (item.Present != "true") continue;

                    var resultadoEnviar = new ResultadoBarridoDTO { NumeroCelular = item.Value };
                    var entidadesReceptor = new List<EntidadesReceptorAfiliado>();

                    if (item.BankCode == null)
                    {
                        entidadesReceptor.Add(new EntidadesReceptorAfiliado
                        {
                            CodigoEntidad = directorio.Directory,
                            NombreEntidad = ObtenerNombreDirectorio(directorio.Directory, directorios)
                        });
                    }
                    else
                    {
                        foreach (var codigo in item.BankCode)
                        {
                            entidadesReceptor.Add(new EntidadesReceptorAfiliado
                            {
                                CodigoEntidad = codigo,
                                NombreEntidad = entidadPorCodigo.TryGetValue(codigo, out var entidad)
                                    ? entidad.NombreEntidad : codigo
                            });
                        }
                    }

                    resultadoEnviar.EntidadesReceptor.AddRange(entidadesReceptor);
                    resultadoEnviarLista.Add(resultadoEnviar);
                }
            }

            if (!resultadoEnviarLista.Any())
                resultadoEnviarLista.Add(new ResultadoBarridoDTO());

            return resultadoEnviarLista;
        }

        /// <summary>
        /// Valida y convierte numeros de celular a validos
        /// </summary>
        /// <param name="celulares">Numeros de celular</param>
        /// <returns>Retorna celulares validos y filtrados</returns>
        public static List<ContactosBarrido> ValidarNumerosCelulares(
            this List<ContactosBarrido> celulares)
        {
            if (celulares.Count == 0) throw new ValidacionException("No hay ningun celular para el barrido de contactos");

            Regex peruCelularRegex = new Regex(@"^\+519[0-8]{8}$");
            Regex nueveDigitosRegex = new Regex(@"^9[0-9]{8}$");

            for (int i = celulares.Count - 1; i >= 0; i--)
            {
                var celular = celulares[i];
                var celularLimpio = celular.NumeroCelular.Replace(" ", "").Replace("-", "");

                if (peruCelularRegex.IsMatch(celularLimpio))
                    continue;

                if (nueveDigitosRegex.IsMatch(celularLimpio))
                {
                    celular.NumeroCelular = DatosGeneralesInteroperabilidad.PrefijoPeruano + celularLimpio;
                    continue;
                }
                celulares.RemoveAt(i);
            }

            if (celulares.Count == 0) throw new ValidacionException("Número inválido");

            return celulares;
        }

        /// <summary>
        /// Valida si el monto de la operación está dentro del rango especificado por el monto máximo y mínimo.
        /// </summary>
        /// <param name="montoMaximo">Monto máximo permitido para la operación.</param>
        /// <param name="montoMinimo">Monto mínimo permitido para la operación.</param>
        /// <param name="montoOperacion">Monto de la operación a validar.</param>
        public static void ValidarMonto(decimal montoMaximo, decimal montoMinimo, decimal montoOperacion)
        {
            if (montoOperacion < montoMinimo || montoOperacion > montoMaximo)
                throw new ValidacionException("Monto fuera de los limites");
        }

        /// <summary>
        /// Valida si el monto de la operación no excede el límite máximo diario establecido,
        /// considerando el monto acumulado actual y el monto de la operación.
        /// </summary>
        /// <param name="montoMaximoDia">Monto máximo permitido para transacciones en un día.</param>
        /// <param name="montoSumadoActual">Monto acumulado actual de transacciones realizadas en el día.</param>
        /// <param name="montoOperacion">Monto de la operación a validar.</param>
        public static void ValidarMontoMaximoTransaccionDia(decimal montoMaximoDia, decimal montoSumadoActual, decimal montoOperacion)
        {
            var montoTotal = montoSumadoActual + montoOperacion;
            if (montoTotal > montoMaximoDia)
                throw new ValidacionException("Estas superando el monto maximo permitido por dia.");
        }

        /// <summary>
        /// Valida si la cantidad de transacciones realizadas en el día no excede el límite máximo establecido.
        /// </summary>
        /// <param name="cantidadMaximoDia">Cantidad máxima permitida de transacciones en un día.</param>
        /// <param name="cantidadSumadoActual">Cantidad actual de transacciones realizadas en el día.</param>
        public static void ValidarCantidadTransaccionesDia(decimal cantidadMaximoDia, decimal cantidadSumadoActual)
        {
            if (ValidarCantidadIntentoGeneral(cantidadMaximoDia, cantidadSumadoActual))
                throw new ValidacionException("Has superado la cantidad de transacciones por dia ");
        }

        /// <summary>
        /// Valida si la cantidad de barridos realizados en el día no excede el límite máximo establecido.
        /// </summary>
        /// <param name="cantidadMaximoDia">Cantidad máxima permitida de barridos en un día.</param>
        /// <param name="cantidadSumadoActual">Cantidad actual de barridos realizados en el día.</param>
        public static void ValidarCantidadBarridoDia(decimal cantidadMaximoDia, decimal cantidadSumadoActual)
        {
            if (ValidarCantidadIntentoGeneral(cantidadMaximoDia, cantidadSumadoActual))
                throw new ValidacionException("Has superado la cantidad de transacciones por dia ");
        }

        /// <summary>
        /// Valida si la cantidad de intentos generales realizados en el día no excede el límite máximo establecido.
        /// </summary>
        /// <param name="cantidadMaximoDia">Cantidad máxima permitida de intentos generales en un día.</param>
        /// <param name="cantidadSumadoActual">Cantidad actual de intentos generales realizados en el día.</param>
        /// <returns>true si la cantidad de intentos generales es válida; de lo contrario, false.</returns>
        public static bool ValidarCantidadIntentoGeneral(decimal cantidadMaximoDia, decimal cantidadSumadoActual)
        {
            if (cantidadSumadoActual >= cantidadMaximoDia)
                return true;

            return false;
        }

        /// <summary>
        /// Valida si la cantidad de intentos de barrido realizados no excede el límite máximo establecido.
        /// </summary>
        /// <param name="cantidadMaximoDia">Cantidad máxima permitida de intentos de barrido.</param>
        /// <returns>True si la cantidad de intentos no supera el limite de intentos</returns>
        public static bool ValidarCantidadIntentosBarrido(int cantidadMaximoDia,List<BitacoraInteroperabilidadBarrido> bitacoraBarrido)
        {
            bool hayIntentosFallidos = bitacoraBarrido.Any(x => x.ResultadoAceptado == BitacoraInteroperabilidadBarrido.NoHayResultado);
            bool hayIntentoExitoso = bitacoraBarrido.Any(x => x.ResultadoAceptado == BitacoraInteroperabilidadBarrido.SiHayResultado);

            if (hayIntentosFallidos && !hayIntentoExitoso && bitacoraBarrido.Count >= cantidadMaximoDia)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Obtiene el estado de afiliación desde la bitácora basado en el tipo de instrucción y el código de respuesta.
        /// </summary>
        /// <param name="tipoInstruccion">Tipo de instrucción relacionada con la afiliación.</param>
        /// <param name="codigoRespuesta">Código de respuesta asociado a la operación.</param>
        /// <returns>Estado de afiliación obtenido desde la bitácora.</returns>
        public static string ObtenerEstadoAfiliacionBitacora(string tipoInstruccion, string codigoRespuesta)
        {

            if (codigoRespuesta == DatosGeneralesInteroperabilidad.Aceptado)
            {
                if (tipoInstruccion == DatosValoresFijos.NuevoRegistro)
                    return BitacoraInteroperabilidadAfiliacion.EstadoAfiliado;
                else
                    return BitacoraInteroperabilidadAfiliacion.EstadoDesafiliado;
            }
            else
            {
                if (tipoInstruccion == DatosValoresFijos.NuevoRegistro)
                    return BitacoraInteroperabilidadAfiliacion.EstadoDesafiliado;
                else
                    return BitacoraInteroperabilidadAfiliacion.EstadoAfiliado;
            }
        }

        /// <summary>
        /// Realiza un filtrado en una lista de directorios de interoperabilidad para encontrar coincidencias basadas 
        /// en el código de entidad y el identificador de cuenta.
        /// </summary>
        /// <param name="directorios">Lista de directorios de interoperabilidad a filtrar.</param>
        /// <param name="codigoEntidadDirectorioReceptor">Código de entidad del directorio receptor a buscar.</param>
        /// <param name="identificadorCuenta">Identificador de cuenta a buscar en los directorios.</param>
        /// <returns>
        /// Tupla que contiene el código CCI, el código servicio usado y el código tipo QR encontrados en los directorios que 
        /// coinciden con los criterios de búsqueda.
        /// </returns>
        public  static (string,string,string) FiltroDirectorioDestino(
           this List<DirectorioInteroperabilidad> directorios,
           string codigoEntidadDirectorioReceptor,
           string identificadorCuenta)
        {
            string canal = ""; string tipoProxy = ""; string valorPorxy = "";
            var directorio = directorios.FirstOrDefault(x => x.CodigoDirectorio == General.Cero + codigoEntidadDirectorioReceptor);

            if (directorio != null)
            {
                tipoProxy = DatosValoresFijos.TipoProxy;
                valorPorxy = identificadorCuenta;
                canal = DatosGeneralesInteroperabilidad.CanalInteroperabilidad;
            }
            else
                canal = DatosGenerales.CanalBancaMovil;

            return (canal, tipoProxy, valorPorxy);
        }

        /// <summary>
        /// Obtiene el nombre del directorio de interoperabilidad que corresponde al código especificado.
        /// </summary>
        /// <param name="codigo">Código del directorio cuyo nombre se desea obtener.</param>
        /// <param name="directorios">Lista de directorios de interoperabilidad donde buscar el nombre.</param>
        /// <returns>Nombre del directorio de interoperabilidad correspondiente al código proporcionado.</returns>
        public static string ObtenerNombreDirectorio(string codigo, List<DirectorioInteroperabilidad> directorios)
        {
            var directorio = directorios.FirstOrDefault(x => x.CodigoDirectorio == codigo);

            if (directorio != null)
                return directorio.NombreDirectorio;

            return null;
        }

        /// <summary>
        /// Valida si ambas monedas están denominadas en soles (código de moneda "PEN").
        /// </summary>
        /// <param name="codigoMonedaOriginante">Código de moneda del origen de la transacción.</param>
        /// <param name="codigoMonedaConsulta">Código de moneda de consulta.</param>
        public static void ValidarMonedaSoles(string codigoMonedaOriginante, string codigoMonedaConsulta)
        {
            var moneda = codigoMonedaOriginante == DatosGenerales.CodigoMonedaSoles
            ? DatosGenerales.CodigoMonedaSolesCCE : DatosGenerales.CodigoMonedaDolaresCCE;

            if (moneda != codigoMonedaConsulta)
                throw new ValidacionException("No se puede enviar a un tipo de moneda diferente.");
        }
    }
}
