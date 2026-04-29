using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.Excepciones;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioInteroperabilidadPrueba
    {
        private Calendario _calendario;
        private DirectorioInteroperabilidad _directorioCCE;

        private string _numeroSeguimiento = "00123";
        [TestInitialize]
        public void Inicializar()
        {

            _calendario = new Calendario();
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoEmpresa))?
                .SetValue(_calendario, "1");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoAgencia))?
                .SetValue(_calendario, "01");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.CodigoSistema))?
                .SetValue(_calendario, "CC");
            typeof(Calendario)?
                .GetProperty(nameof(Calendario.FechaSistema))?
                .SetValue(_calendario, DateTime.Now);
        }

        public EntradaAfiliacionDirectorioDTO AEntradaRegistro()
        {
            return new EntradaAfiliacionDirectorioDTO
            {
                CodigoEntidadOriginante = "0813",
                TipoInstruccion = "NEWR",
                NumeroCelular = "987655555",
                TipoOperacion = "19",
                CodigoCliente = "114019106666",
                NumeroCuentaAfiliada = "001211101939525",
                CodigoCuentaInterbancario = "81300121110193955555",
                IndicadorModificarNumero =  "N",
                NumeroAntiguo = "",
                NumeroTarjeta = "4772000011111111"
            };
        }

        public List<ContactosBarrido> AListaContactos()
        {
            return new List<ContactosBarrido>
            {
                new ContactosBarrido
                {
                    NumeroCelular = "998744633",
                    NombreAlias = "NEWR",
                }
                
            };
        }

        public Directories[] ADirectoriosAfiliacion()
        {
            return new Directories[]
            {
                new Directories
                {
                      Directory = "901",
                    Proxy = new Proxy[]
                    {
                        new Proxy
                        {
                            Present = "true",
                            BankCode = new List<string> { "0002", "0813" }
                        }
                    }
                }
              
            };
        }

        public List<EntidadFinancieraTinDTO> AListaEntidades()
        {
            return new List<EntidadFinancieraTinDTO>
            {
                new EntidadFinancieraTinDTO
                {
                    CodigoEntidad = "0002",
                    NombreEntidad = "BCP",
                }

            };
        }

        #region Maquetacion
        [TestMethod]
        public void MaquetarDatosRegistroDirectorio()
        {
            var datosEntrada = AEntradaRegistro();
            var resultado = ServicioDominioInteroperabilidad.MaquetarDatos(datosEntrada,
                _calendario, _numeroSeguimiento);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void MaquetarDatosBarridoContactos()
        {
            var datosEntrada = AListaContactos();
            var resultado = ServicioDominioInteroperabilidad.MaquetarDatos(datosEntrada,
                _calendario, _numeroSeguimiento);

            Assert.IsNotNull(resultado);
        }
        #endregion

        #region Barrido de contactos
        [TestMethod]
        public void FiltrarRespuestaBarridoExitoso()
        {

            var listaEntidades = AListaEntidades();
            var listaDirectorios = new List<DirectorioInteroperabilidad>();
            var directoriosAfiliados = ADirectoriosAfiliacion();

            _directorioCCE = new DirectorioInteroperabilidad();
            typeof(Cliente).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.SetValue(_directorioCCE, "0903");
            typeof(Cliente).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.SetValue(_directorioCCE, "BCP");

            listaDirectorios.Add(_directorioCCE);

            var resultado = ServicioDominioInteroperabilidad.FiltrarRespuesta(directoriosAfiliados,
                listaEntidades, listaDirectorios);

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void ValidarNumerosCelulares()
        {
            var datosEntrada = AListaContactos();

            var resultado = ServicioDominioInteroperabilidad.ValidarNumerosCelulares(datosEntrada);

            Assert.IsNotNull(resultado);
        }
        #endregion Barrido de contactos

        #region Validacion de Transferencia
        [TestMethod]
        public void ValidarMontoDebeLanzarExcepcionCuandoMontoFueraDeLimites()
        {
            decimal montoMaximo = 1000m;
            decimal montoMinimo = 100m;
            decimal montoOperacion = 1200m; 

            Assert.ThrowsException<ValidacionException>(() =>
            {
                ServicioDominioInteroperabilidad.ValidarMonto(montoMaximo, montoMinimo, montoOperacion);
            });
        }

        [TestMethod]
        public void ValidarMontoMaximoTransaccionDiaDebeLanzarExcepcionCuandoMontoSuperado()
        {
            decimal montoMaximoDia = 1000m;
            decimal montoSumadoActual = 800m;
            decimal montoOperacion = 300m; 

            Assert.ThrowsException<ValidacionException>(() =>
            {
                ServicioDominioInteroperabilidad.ValidarMontoMaximoTransaccionDia(montoMaximoDia, montoSumadoActual, 
                    montoOperacion);
            });
        }

        [TestMethod]
        public void ValidarCantidadTransaccionesDiaDebeLanzarExcepcionCuandoCantidadSuperada()
        {
            decimal cantidadMaximoDia = 5;
            decimal cantidadSumadoActual = 6;

            Assert.ThrowsException<ValidacionException>(() =>
            {
                ServicioDominioInteroperabilidad.ValidarCantidadTransaccionesDia(cantidadMaximoDia, cantidadSumadoActual);
            });
        }

        [TestMethod]
        public void ValidarCantidadBarridoDiaDebeLanzarExcepcionCuandoCantidadSuperada()
        {
            decimal cantidadMaximoDia = 5;
            decimal cantidadSumadoActual = 6; 

            Assert.ThrowsException<ValidacionException>(() =>
            {
                ServicioDominioInteroperabilidad.ValidarCantidadBarridoDia(cantidadMaximoDia, cantidadSumadoActual);
            });
        }

        [TestMethod]
        public void ValidarCantidadIntentoGeneralDebeDevolverTrueCuandoCantidadSuperada()
        {
            decimal cantidadMaximoDia = 5;
            decimal cantidadSumadoActual = 4; 

            bool resultado = ServicioDominioInteroperabilidad.ValidarCantidadIntentoGeneral(
                cantidadMaximoDia, cantidadSumadoActual);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void ValidarCantidadIntentosNoSuperaMaximoParametrizado()
        {
            int cantidadMaximoDia = 5;
            int cantidadIntentosActual = 6;

            
            var bitacora = new BitacoraInteroperabilidadBarrido();
            typeof(BitacoraInteroperabilidadBarrido)?.GetProperty(nameof(BitacoraInteroperabilidadBarrido.CodigoCCI))?
            .SetValue(bitacora, DateTime.Now.ToString());
            typeof(BitacoraInteroperabilidadBarrido)?.GetProperty(nameof(BitacoraInteroperabilidadBarrido.NumeroCelularOrigen))?
            .SetValue(bitacora, "987654321");
            typeof(BitacoraInteroperabilidadBarrido)?.GetProperty(nameof(BitacoraInteroperabilidadBarrido.IdentificadorInstruccion))?
            .SetValue(bitacora, "identificador123456");

            typeof(BitacoraInteroperabilidadBarrido)?.GetProperty(nameof(BitacoraInteroperabilidadBarrido.ResultadoAceptado))?
            .SetValue(bitacora, "N");


            var bitacoras = new List<BitacoraInteroperabilidadBarrido>();
            bitacoras.Add(bitacora);

            var resultadoValidacion =  ServicioDominioInteroperabilidad.ValidarCantidadIntentosBarrido(
                    cantidadMaximoDia, bitacoras);
            Assert.IsTrue(resultadoValidacion);
        }

        [TestMethod]
        public void ObtenerEstadoAfiliacionBitacoraDebeRetornarEstadoAfiliadoCuandoCodigoAceptadoYTipoNuevoRegistro()
        {
            string tipoInstruccion = DatosValoresFijos.NuevoRegistro;
            string codigoRespuesta = DatosGeneralesInteroperabilidad.Aceptado;

            string estado = ServicioDominioInteroperabilidad.ObtenerEstadoAfiliacionBitacora(tipoInstruccion, codigoRespuesta);

            Assert.AreEqual(BitacoraInteroperabilidadAfiliacion.EstadoAfiliado, estado);
        }

        [TestMethod]
        public void FiltroDirectorioDestinoDebeRetornarValoresCorrectosCuandoDirectorioExiste()
        {
            var directorios = new List<DirectorioInteroperabilidad>();
            string codigoEntidadDirectorioReceptor = "901";
            string identificadorCuenta = "8DS8K8GF8GF8FS";
            _directorioCCE = new DirectorioInteroperabilidad();
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.SetValue(_directorioCCE, "0901");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.SetValue(_directorioCCE, "YAPE");
            directorios.Add(_directorioCCE);

            var resultado = ServicioDominioInteroperabilidad.FiltroDirectorioDestino(directorios,codigoEntidadDirectorioReceptor, identificadorCuenta);

            Assert.AreEqual(DatosGeneralesInteroperabilidad.CanalInteroperabilidad, resultado.Item1);
            Assert.AreEqual(DatosValoresFijos.TipoProxy, resultado.Item2);
            Assert.AreEqual(identificadorCuenta, resultado.Item3);
        }

        [TestMethod]
        public void ObtenerNombreDirectorioDebeRetornarNombreCuandoDirectorioExiste()
        {
            var directorios = new List<DirectorioInteroperabilidad>();

            _directorioCCE = new DirectorioInteroperabilidad();
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.SetValue(_directorioCCE, "0901");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.SetValue(_directorioCCE, "YAPE");
            directorios.Add(_directorioCCE);
            string codigo = "0901";

            var resultado = ServicioDominioInteroperabilidad.ObtenerNombreDirectorio(codigo, directorios);

            Assert.AreEqual("YAPE", resultado);
        }

        [TestMethod]
        public void ValidarMonedaSolesDebeLanzarExcepcionCuandoMonedaConsultaNoEsIgual()
        {
            string codigoMonedaOriginante = DatosGenerales.CodigoMonedaSoles;
            string codigoMonedaConsulta = DatosGenerales.CodigoMonedaDolaresCCE;

            Assert.ThrowsException<ValidacionException>(() =>
            {
                ServicioDominioInteroperabilidad.ValidarMonedaSoles(codigoMonedaOriginante, codigoMonedaConsulta);
            });
        }

        #endregion Validacion de Transferencia
    }
}
