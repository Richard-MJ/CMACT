
using System.Globalization;
using System.Text;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Utilidades;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using static Takana.Transferencias.CCE.Api.Dominio.Entidades.CF.Moneda;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Logica
{
    public class AdministradorComprobantes
    {

        /// <summary>
        /// Nombre de la impresora
        /// </summary>
        private readonly string _nombreImpresora;
        /// <summary>
        /// Anchi
        /// </summary>
        private int _ancho;
        /// <summary>
        /// Linea de separacion
        /// </summary>
        private string _lineaSeparacion;
        /// <summary>
        /// Linea vacia
        /// </summary>
        private string _lineaVacia;
        /// <summary>
        /// Nombre de Empresa
        /// </summary>
        private string _nombreEmpresa = "CAJA TACNA";
        private string _rucEmpresa = "RUC Nro 20130098488";
        private string _direccionEmpresa = "CALLE FRANCISCO LAZO 297, TACNA-TACNA";
        /// <summary>
        /// Define el formato de impresion
        /// </summary>
        /// <param name="nombreImpresora">Nombre de la impresora</param>
        public AdministradorComprobantes(string nombreImpresora)
        {
            _nombreImpresora = nombreImpresora;
            if (_nombreImpresora.ToUpper().Contains("EPSON_TMU"))
                _ancho = 40;
            else
                _ancho = 50;
            _lineaSeparacion = new string('-', _ancho);
            _lineaVacia = new string(' ', _ancho);
        }
        /// <summary>
        /// Mapea los datos de transferencia a un texto de impresion
        /// </summary>
        /// <param name="transferencia"></param>
        /// <param name="montos"></param>
        /// <returns></returns>
        public string ATextoInmediatasCCE(Transferencia transferencia, RealizarTransferenciaInmediataDTO montos)
        {
            string[] lineas;
            string titulo = "COMPROBANTE DE TRANSFERENCIA";
            string titulo2 = "INTERBANCARIA INMEDIATA - CCE";
            titulo = titulo.CentrarTextoVoucher(_ancho, _lineaVacia);
            titulo2 = titulo2.CentrarTextoVoucher(_ancho, _lineaVacia);
            _nombreEmpresa = _nombreEmpresa.CentrarTextoVoucher(_ancho, _lineaVacia);
            _rucEmpresa = _rucEmpresa.CentrarTextoVoucher(_ancho, _lineaVacia);
            _direccionEmpresa = _direccionEmpresa.CentrarTextoVoucher(_ancho, _lineaVacia);

            decimal montoComision = montos.ControlMonto.TotalComision; 
            decimal montoTotal = transferencia.MontoTransferencia + montoComision + montos.ControlMonto.Itf;
            string cuenta = string.Empty;
            if (transferencia.DetallesSalientes.First().CodigoCuentaInterbancario.Length == 16)
                cuenta = new StringBuilder(transferencia.DetallesSalientes.First().CodigoCuentaInterbancario).Insert(12, "-").Insert(8, "-").Insert(4, "-").ToString();
            else if (transferencia.DetallesSalientes.First().CodigoCuentaInterbancario.Length == 20)
                cuenta = new StringBuilder(transferencia.DetallesSalientes.First().CodigoCuentaInterbancario).Insert(18, "-").Insert(6, "-").Insert(3, "-").ToString();
            var nombreEntidad = transferencia.DetallesSalientes.First().EntidadDestino.NombreEntidad;
            var monedaSoles = ((int)MonedaCodigo.Soles).ToString();
            lineas = new string[38];
            lineas[0] = _nombreEmpresa;
            lineas[1] = _rucEmpresa;
            lineas[2] = _direccionEmpresa;
            lineas[3] = " ";
            lineas[4] = titulo;
            lineas[5] = titulo2;
            lineas[6] = " ";
            lineas[7] = "NRO. COMPROBANTE: " + transferencia.NumeroMovimiento;
            lineas[8] = "OPERACIÓN:" + (transferencia.CodigoTipoTransferencia == TipoTransferencia.CodigoTransferenciaOrdinaria ? "TRANSFERENCIA ORDINARIA" : "PAGO DE TARJETA DE CREDITO");
            lineas[9] = _lineaSeparacion;
            lineas[10] = "NRO. CUENTA:";
            lineas[11] = transferencia.NumeroCuenta!;
            lineas[12] = "TITULAR:";
            lineas[13] = transferencia.CuentaOrigen.Cliente.NombreCliente.ToUpper();
            lineas[14] = _lineaSeparacion;
            lineas[15] = "ENTIDAD FINANCIERA:";
            lineas[16] = nombreEntidad.ToUpper();
            lineas[17] = (transferencia.CodigoTipoTransferencia == TipoTransferencia.CodigoPagoTarjeta ? "NRO. TARJETA:" : "NRO. CCI:");
            lineas[18] = cuenta;
            lineas[19] = "BENEFICIARIO:";
            lineas[20] = transferencia.DetallesSalientes.First().Beneficiario.ToUpper();
            lineas[21] = _lineaSeparacion;
            lineas[22] = "IMPORTE : " + (transferencia.CuentaOrigen.Moneda.Moneda_ML) + string.Format(CultureInfo.InvariantCulture, "{0,15:N2}", transferencia.MontoTransferencia);
            lineas[23] = "COMISION: " + (transferencia.CuentaOrigen.Moneda.Moneda_ML) + string.Format(CultureInfo.InvariantCulture, "{0,15:N2}", montoComision);
            lineas[24] = "ITF     : " + (transferencia.CuentaOrigen.Moneda.Moneda_ML) + string.Format(CultureInfo.InvariantCulture, "{0,15:N2}", montos.ControlMonto.Itf);
            lineas[25] = "TOTAL   : " + (transferencia.CuentaOrigen.Moneda.Moneda_ML) + string.Format(CultureInfo.InvariantCulture, "{0,15:N2}", montoTotal);
            lineas[26] = _lineaSeparacion;
            lineas[27] = "AGENCIA : " + transferencia.Agencia.NombreAgencia;
            lineas[28] = "USUARIO : " + transferencia.CodigoUsuario;
            lineas[29] = "FECHA Y HORA: " + transferencia.FechaTransferencia.ToString("dd/MM/yyyy HH:mm:ss");
            lineas[30] = _lineaSeparacion;
            lineas[31] = " ";
            lineas[32] = "FIRMA: ";
            lineas[33] = " ";
            lineas[34] = " ";
            lineas[35] = " ";
            lineas[36] = "NRO. DOI: "; 
            lineas[37] = " ";
            return ArregloCadenasACadena(lineas);
        }
        /// <summary>
        /// Crea el arreglo de la cadena de impresion
        /// </summary>
        /// <param name="lineas">Lineas de codigo</param>
        /// <param name="retornoDeCarro">retorno de carro</param>
        /// <param name="generarNuevaLinea">Genera una nueva linea</param>
        /// <returns>Retorna el arrelgo</returns>
        private string ArregloCadenasACadena(string[] lineas, bool retornoDeCarro = true, bool generarNuevaLinea = true)
        {
            string comprobante = string.Empty;
            if (lineas != null)
            {
                if (_nombreImpresora.ToUpper().Contains("EPSON_TMU"))
                {
                    for (int i = 0; i < lineas.Length; i++)
                        if (!string.IsNullOrEmpty(lineas[i]))
                            comprobante += (lineas[i] + _lineaVacia).Substring(0, _ancho) +
                                (generarNuevaLinea ? "\n" : string.Empty) +
                                (retornoDeCarro ? "\r" : string.Empty);
                }
                else
                {
                    string ls_separador = new string(' ', 16);
                    for (int i = 0; i < lineas.Length; i++)
                        if (!string.IsNullOrEmpty(lineas[i]))
                            comprobante += (lineas[i] + _lineaVacia).Substring(0, _ancho) + ls_separador + (lineas[i] + _lineaVacia).Substring(0, _ancho) + '\n';
                }
            }
            return comprobante;
        }
    }
}
