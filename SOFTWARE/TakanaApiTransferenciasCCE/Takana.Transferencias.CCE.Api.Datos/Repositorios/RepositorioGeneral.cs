using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Takana.Transferencias.CCE.Api.Datos.Contexto;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades;
using Takana.Transferencias.CCE.Api.Common.DTOs.Email;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Common.ConsultasCuentas;

namespace Takana.Transferencias.CCE.Api.Datos.Repositorios
{
    /// <summary>
    /// Clase que se utiliza como repositorio de metodos para el contexto de general
    /// </summary>
    public class RepositorioGeneral : RepositorioReportes, IRepositorioGeneral
    {
        public RepositorioGeneral(ContextoGeneral contextoGeneral) : base(contextoGeneral)
        {
        }

        /// <summary>
        /// Metodo para buscar el registro de una tabla con una llave
        /// </summary>
        /// <typeparam name="T"> Clase</typeparam>
        /// <param name="llaves">Llavs para buscar</param>
        /// <returns>Resultado de busqueda</returns>
        public T ObtenerPorCodigo<T>(params object[] llaves) where T : class
        {
            var entidad = _contextoGeneral.Establecer<T>().FindAsync(llaves).Result;

            if (entidad == null) throw new EntidadNoExisteException(typeof(T), llaves);

            return entidad;
        }        
        
        /// <summary>
        /// Metodo para buscar el registro de una tabla con una llave
        /// </summary>
        /// <typeparam name="T"> Clase</typeparam>
        /// <param name="llaves">Llavs para buscar</param>
        /// <returns>Resultado de busqueda</returns>
        public async Task<T> ObtenerPorCodigoAsync<T>(params object[] llaves) where T : class
        {
            var entidad = await _contextoGeneral.Establecer<T>().FindAsync(llaves);

            if (entidad == null) throw new EntidadNoExisteException(typeof(T), llaves);

            return entidad;
        }
        /// <summary>
        /// Metodo que busca un registro en una tabla con un filtro en especifico
        /// </summary>
        /// <typeparam name="T">Clase</typeparam>
        /// <param name="filtro">Filtro</param>
        /// <param name="incluir">Inlcuir</param>
        /// <param name="limite">Limite</param>
        /// <returns>Retorna resultado de busqueda con filtros</returns>
        public IList<T> ObtenerPorExpresionConLimite<T>(Expression<Func<T, bool>> filtro = null,
            string incluir = null,
            byte limite = 0) where T : class
        {
            try
            {
                if (filtro != null)
                {
                    if (limite == 0)
                        return _contextoGeneral.Establecer<T>().Where(filtro).ToListAsync().Result;
                    else
                        return _contextoGeneral.Establecer<T>().Where(filtro).Take(limite).ToListAsync().Result;
                }
                else
                {
                    return _contextoGeneral.Establecer<T>().ToListAsync().Result;
                }
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }

        /// <summary>
        /// Lista los registro d euna tabla
        /// </summary>
        /// <typeparam name="T">Clase</typeparam>
        /// <returns>Retorna la lista de los registros</returns>
        public IQueryable<T> Listar<T>() where T : class
        {
            return _contextoGeneral.Establecer<T>();
        }

        /// <summary>
        /// Método que adiciona una entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objeto"></param>
        public void Adicionar<T>(T objeto) where T : class
        {
            _contextoGeneral.Establecer<T>().AddAsync(objeto);
        }

        /// <summary>
        /// Método que guarda cambios en la BBDD
        /// </summary>
        public void GuardarCambios()
        {
            _contextoGeneral.GuardarCambios();
        }

        /// <summary>
        /// Procedimiento almancenado que obtiene datos del cliente por Codigo de Cuenta Interbancaria
        /// </summary>
        /// <param name="codigoCuentaInterbancario">Codigo de Cuenta Bancaria del Cliente</param>
        /// <returns>Retorna un diccionario de datos</returns>
        public ClienteReceptorDTO ObtenerDatosClientePorCodigoCuentaInterbancaria(
            string codigoCuentaInterbancario)
        {
            var consultaCuenta = new ClienteReceptorDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_OBTENER_DATO_CLIENTE_POR_CTA_INTERBANCARIA";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AVCH_COD_CTA_INTERBANCARIA", SqlDbType.VarChar))
                        .Value = codigoCuentaInterbancario;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                consultaCuenta.CodigoCuentaInterbancaria = GetString(reader, "NUM_CUENTA_CCI");
                                consultaCuenta.NombreCliente = GetString(reader, "NOM_CLIENTE");
                                consultaCuenta.NumeroDocumento = GetString(reader, "NUM_ID");
                                consultaCuenta.TipoDocumento = GetString(reader, "COD_TIPO_ID");
                                consultaCuenta.TipoDocumentoCCE = GetString(reader, "COD_TIPO_ID_CCE_INMEDIATA");
                                consultaCuenta.Direccion = GetString(reader, "DET_DIRECCION");
                                consultaCuenta.NumeroTelefono = GetString(reader, "TEL_PRINCIPAL");
                                consultaCuenta.NumeroCelular = GetString(reader, "TEL_SECUNDARIO");
                                consultaCuenta.EstadoCuenta = GetString(reader, "IND_ESTADO");
                                consultaCuenta.CodigoMoneda = GetString(reader, "COD_MONEDA");
                                consultaCuenta.CodigoMonedaISO = GetString(reader, "COD_MONEDA_ISO");
                                consultaCuenta.IndicadorCuentaValida = GetString(reader, "IND_TRANSF_CCE_TIN");
                            }
                        }
                    }
                    loSqlConnection.Close();
                }

                return consultaCuenta;
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }

        /// <summary>
        /// Se obtiene el número de inicial de una secuencia de números de series de sistema.
        /// </summary>
        /// <param name="codigoAgencia">Código de agencia de la serie de sistema, "%" obtiene la serie por empresa de sistema.</param>
        /// <param name="codigoSistema">Código de sistema de la series de sistema</param>
        /// <param name="codigoSerie">Código de la serie de sistema.</param>
        /// <param name="cantidadSeries">Cantidad de número de la secuencia de números de serie.</param>
        /// <returns>Primero número de la serie de sistema.</returns>

        public int ObtenerNumeroSerieNoBloqueante(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries)
        {
            SqlParameter resultado = new SqlParameter
            {
                ParameterName = "@decVALOR",
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Decimal,
                Value = 0
            };
            SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
            loSqlConnection.Open();
            using (SqlTransaction loSqlTransaction = loSqlConnection.BeginTransaction())
            {
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    try
                    {
                        loSqlCommand.Connection = loSqlConnection;
                        loSqlCommand.Transaction = loSqlTransaction;
                        loSqlCommand.CommandText = "CF.USP_CF_OBTENER_SIGUIENTE_SERIE";
                        loSqlCommand.CommandType = CommandType.StoredProcedure;
                        loSqlCommand.Parameters.Add(new SqlParameter("@vchAGENCIA", SqlDbType.VarChar)).Value = codigoAgencia;
                        loSqlCommand.Parameters.Add(new SqlParameter("@vchSISTEMA", SqlDbType.VarChar)).Value = codigoSistema;
                        loSqlCommand.Parameters.Add(new SqlParameter("@vchSERIE", SqlDbType.VarChar)).Value = codigoSerie;
                        loSqlCommand.Parameters.Add(new SqlParameter("@decINCREMENTO", SqlDbType.Int)).Value = cantidadSeries;
                        loSqlCommand.Parameters.Add(resultado);
                        loSqlCommand.ExecuteNonQuery();
                        loSqlTransaction.Commit();
                    }
                    catch (Exception excepcion)
                    {
                        loSqlTransaction.Rollback();
                        throw new ApplicationException(string.Format("Se genero un error al ejecutar el procedimiento almacenado.") + excepcion);
                    }

                }
            }
            loSqlConnection.Close();

            return Convert.ToInt32(resultado.Value);
        }

        /// <summary>
        /// Se obtiene el número de inicial de una secuencia de números de series de sistema.
        /// </summary>
        /// <param name="codigoAgencia">Código de agencia de la serie de sistema, "%" obtiene la serie por empresa de sistema.</param>
        /// <param name="codigoSistema">Código de sistema de la series de sistema</param>
        /// <param name="codigoSerie">Código de la serie de sistema.</param>
        /// <param name="cantidadSeries">Cantidad de número de la secuencia de números de serie.</param>
        /// <returns>Primero número de la serie de sistema.</returns>

        public async Task<int> ObtenerNumeroSerieNoBloqueanteAsync(string codigoAgencia, string codigoSistema, string codigoSerie, int cantidadSeries)
        {
            SqlParameter resultado = new SqlParameter
            {
                ParameterName = "@decVALOR",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Decimal,
                Value = 0
            };
            await _contextoGeneral.EjecutarComandoSQL(
                "CF.USP_CF_OBTENER_SIGUIENTE_SERIE @vchAGENCIA, @vchSISTEMA, @vchSERIE, @decINCREMENTO, @decVALOR OUTPUT"
                , new SqlParameter("@vchAGENCIA", codigoAgencia)
                , new SqlParameter("@vchSISTEMA", codigoSistema)
                , new SqlParameter("@vchSERIE", codigoSerie)
                , new SqlParameter("@decINCREMENTO", cantidadSeries)
                , resultado
                );
            return Convert.ToInt32(resultado.Value);
        }

        /// <summary>
        /// Metodo que obtiene valores por parametro
        /// </summary>
        /// <param name="codigoSistema">codigo sistema</param>
        /// <param name="codigoParametro">codigo de sistema</param>
        /// <returns>Retorna el valor del parametro por empresa</returns>
        public string ObtenerValorParametroPorEmpresa(string codigoSistema, string codigoParametro)
        {
            return _contextoGeneral.ParametrosPorEmpresa.Find(Empresa.CodigoPrincipal, codigoSistema, codigoParametro)!.ValorParametro;
        }
        /// <summary>
        /// Metodo que obtiene valo de indice de la tabla de indices
        /// </summary>
        /// <param name="codigoIndice">codigo de indice</param>
        /// <param name="fechaReferencia">fecha de referencia</param>
        /// <returns>El valor de indice</returns>
        public decimal ObtenerValorPorIndice(string codigoIndice, DateTime fechaReferencia)
        {
            decimal resultado = -1;
            SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
            loSqlConnection.Open();
            using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
            {
                loSqlCommand.Connection = loSqlConnection;
                loSqlCommand.CommandText = "SELECT TOP 1 VAL_INDICE FROM PA.PA_DETA_INDICES WHERE COD_INDICE = @CodigoIndice AND FEC_INDICE <= @FechaReferencia " +
                 "ORDER BY FEC_INDICE DESC";
                loSqlCommand.Parameters.Add(new SqlParameter("@CodigoIndice", SqlDbType.VarChar))
                    .Value = codigoIndice;
                loSqlCommand.Parameters.Add(new SqlParameter("@FechaReferencia", SqlDbType.DateTime))
                    .Value = fechaReferencia;
                using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado = reader.GetDecimal(reader.GetOrdinal("VAL_INDICE"));

                        }
                    }
                }
                loSqlConnection.Close();
            }
            return resultado;
        }
        /// <summary>
        /// Metodo que obtiene la moneda local
        /// </summary>
        /// <returns>Retorna los datos de la moneda local</returns>
        public Moneda ObtenerMonedaLocal()
        {
            var resultado = string.Empty;
            SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
            loSqlConnection.Open();
            using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
            {
                loSqlCommand.Connection = loSqlConnection;
                loSqlCommand.CommandText = "SELECT COD_MON_ORIGEN FROM CF.CF_EMPRESAS WHERE COD_EMPRESA = @CodigoEmpresa";
                loSqlCommand.Parameters.Add(new SqlParameter("@CodigoEmpresa", SqlDbType.VarChar))
                    .Value = Empresa.CodigoPrincipal;
                using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            resultado = reader.GetString(reader.GetOrdinal("COD_MON_ORIGEN"));

                        }
                    }
                }
                loSqlConnection.Close();
            }

            return _contextoGeneral.Monedas.Find(resultado);
        }

        /// <summary>
        /// Método de obtener la fecha de cuenta efectivo
        /// </summary>
        /// <returns>Retorna los datos de la fecha de cuenta efectivo</returns>
        public Calendario ObtenerCalendarioCuentaEfectivo()
        {
            return ObtenerPorCodigo<Calendario>(Empresa.CodigoPrincipal, Agencia.Principal, Sistema.CuentaEfectivo);
        }
        /// <summary>
        /// Método para obtener uno o nulo
        /// </summary>
        /// <returns>Entidad Consultada</returns>
        public T ObtenerUnoONulo<T>(Expression<Func<T, bool>> filtro,
            string incluir = null) where T : class
        {
            try
            {
                return _contextoGeneral.Establecer<T>().FirstOrDefault(filtro);
            }
            catch (Exception excepcion)
            {
                throw new InvalidOperationException(excepcion.Message);
            }
        }
        /// <summary>
        /// Procedimiento almancenado que obtiene  el detalle de la transferencia inmediata para correo electronico
        /// </summary>
        /// <param name="numeroMovimiento"></param>
        /// <exception cref="Exception"></exception>
        public CorreoTransferenciaInmediataDTO ObtenerDetalleTransferenciaInmediataPorNumeroMovimiento(int numeroMovimiento)
        {
            var datosCorreo = new CorreoTransferenciaInmediataDTO();
            try
            {
                SqlConnection loSqlConnection = (SqlConnection)(_contextoGeneral.Database.GetDbConnection());
                loSqlConnection.Open();
                using (SqlCommand loSqlCommand = loSqlConnection.CreateCommand())
                {
                    loSqlCommand.Connection = loSqlConnection;
                    loSqlCommand.CommandText = "CC.USP_CC_OBTENER_DETALLE_TRANSFERENCIA_INMEDIATA_POR_NUM_MOVIMIENTO";
                    loSqlCommand.CommandType = CommandType.StoredProcedure;
                    loSqlCommand.Parameters.Add(new SqlParameter("@AI_NUM_MOVIMIENTO", SqlDbType.Int)).Value = numeroMovimiento;
                    using (SqlDataReader reader = loSqlCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                datosCorreo.IndicadorTransaccion = GetString(reader, "IND_TRANSACCION");
                                datosCorreo.NombreClienteOrigen = GetString(reader, "NOM_ORIGINANTE");
                                datosCorreo.NumeroCuentaOrigen = GetString(reader, "NUM_CUENTA");
                                datosCorreo.MontoTransferencia = reader.GetDecimal(reader.GetOrdinal("MON_TRANSFERENCIA"));
                                datosCorreo.MontoComision = reader.GetDecimal(reader.GetOrdinal("MON_COMISION"));
                                datosCorreo.MontoItf = reader.GetDecimal(reader.GetOrdinal("MON_ITF"));
                                datosCorreo.CodigoCanal = GetString(reader, "COD_CANAL");
                                datosCorreo.SimboloMoneda = GetString(reader, "COD_SIMBOLO_MONEDA");
                                datosCorreo.DescripcionMoneda = GetString(reader, "DES_MONEDA");
                                datosCorreo.NombreClienteDestino = GetString(reader, "NOM_RECEPTOR");
                                datosCorreo.NumeroTarjetaDestino = GetString(reader, "COD_TARJETA_CREDITO_RECEPTOR");
                                datosCorreo.CuentaInterbancariaDestino = GetString(reader, "COD_CUENTA_INTERBANCARIA_RECEPTOR");
                                datosCorreo.CuentaInterbancariaOrigen = GetString(reader, "COD_CUENTA_INTERBANCARIA_ORIGINANTE");
                                datosCorreo.NombreBancoOrigen = GetString(reader, "DES_ENTIDAD_ORIGEN");
                                datosCorreo.NombreBancoDestino = GetString(reader, "DES_ENTIDAD_RECEPTOR");
                                datosCorreo.TipoTransferencia = GetString(reader, "TIP_TRANSFERENCIA");
                                datosCorreo.TipoDocumentoDestino = GetString(reader, "TIP_DOCUMENTO_RECEPTOR");
                                datosCorreo.NumeroDocumentoDestino = GetString(reader, "NUM_DOCUMENTO_RECEPTOR");
                                datosCorreo.NumeroOperacion = reader.GetInt32(reader.GetOrdinal("NUM_MOVIMIENTO"));
                                datosCorreo.CorreoElectronicoDestinatario = GetString(reader, "DES_EMAIL");
                                datosCorreo.CelularDestino = GetString(reader, "NUM_CELULAR_RECEPTOR");
                                datosCorreo.CelularOrigen = GetString(reader, "NUM_CELULAR_ORIGINANTE");
                            }
                        }
                    }
                    loSqlConnection.Close();
                }
                return datosCorreo;
            }
            catch (Exception excepcion)
            {
                throw new Exception(excepcion.Message);
            }
        }
    }
}