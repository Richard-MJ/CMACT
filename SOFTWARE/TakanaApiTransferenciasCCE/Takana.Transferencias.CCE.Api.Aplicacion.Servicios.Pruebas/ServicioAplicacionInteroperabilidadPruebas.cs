using Moq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.DTOs.BA;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Common.Interfaz;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;
using Takana.Transferencias.CCE.Api.Common.DTOs.Operaciones;
using Takana.Transferencias.CCE.Api.Common.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Interfaces.Aplicacion;
using Takana.Transferencias.CCE.Api.Common.DTOs.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Common.Constantes.Interoperabilidad;
using Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.Servicios;
using static Takana.Transferencias.CCE.Api.Common.Interoperatibilidad.EstructuraJsonInteroperatibilidad;

namespace Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Pruebas
{
    [TestClass]
    public class ServicioAplicacionInteroperabilidadPruebas
    {
        #region Declaraciones
        private Mock<IContextoAplicacion> _contexto;
        private Mock<IRepositorioRedis> _repositorioRedis;
        private Mock<IRepositorioGeneral> _repositorioGeneral;
        private Mock<IRepositorioOperacion> _repositorioOperaciones;
        private Mock<IServicioAplicacionParametroGeneral> _servicioAplicacionParametro;
        private Mock<IServicioAplicacionPeticion> _servicioAplicacionPeticion;
        private Mock<IServicioAplicacionTransaccionOperacion> _servicioAplicacionTransaccionOperacion;
        private Mock<IServicioAplicacionCliente> _servicioAplicacionCliente;
        private Mock<IServicioAplicacionLavado> _servicioAplicacionLavado;
        private Mock<IServicioAplicacionValidacion> _aplicacionValidacion;
        private Mock<IServicioAplicacionNotificaciones> _servicioAplicacionNotificaciones;

        private ServicioAplicacionInteroperabilidad _servicioAplicacionInteroperabilidad;
        #endregion Declaraciones

        private Calendario _calendario;

        #region Inicializar


        [TestInitialize]
        public void Inicializar()
        {
            _contexto  = new Mock<IContextoAplicacion>();
            _repositorioGeneral = new Mock<IRepositorioGeneral>();
            _repositorioRedis = new Mock<IRepositorioRedis>();
            _repositorioOperaciones = new Mock<IRepositorioOperacion>();
            _servicioAplicacionParametro = new Mock<IServicioAplicacionParametroGeneral>();
            _servicioAplicacionPeticion = new Mock<IServicioAplicacionPeticion>();
            _servicioAplicacionTransaccionOperacion = new Mock<IServicioAplicacionTransaccionOperacion>();
            _servicioAplicacionCliente = new Mock<IServicioAplicacionCliente>();
            _servicioAplicacionLavado = new Mock<IServicioAplicacionLavado>();
            _aplicacionValidacion = new Mock<IServicioAplicacionValidacion>();
            _servicioAplicacionNotificaciones = new Mock<IServicioAplicacionNotificaciones>();

            _servicioAplicacionInteroperabilidad = new ServicioAplicacionInteroperabilidad(
                _contexto.Object,
                _repositorioGeneral.Object,
                _repositorioRedis.Object,
                _repositorioOperaciones.Object,
                _servicioAplicacionLavado.Object,
                _aplicacionValidacion.Object,
                _servicioAplicacionCliente.Object,
                _servicioAplicacionPeticion.Object,
                _servicioAplicacionParametro.Object,
                _servicioAplicacionTransaccionOperacion.Object,
                _servicioAplicacionNotificaciones.Object
                );

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
        #endregion

        #region Barrido Contactos

        [TestMethod]
        public void BarridoContactosExitoso()
        {
            var datosBarridoEntrada = AEntradaBarrido();
            var directorios = ADirectoriosCCE();
            var entidades = AEntidadesCCE();

            var limiteMaximo = "10";
            var limiteMontoMinimo = "1";

            var montoMaximoDia = "10000";

            string intentosMaximos = "5";
            var bitacorasBarrido = new List<BitacoraInteroperabilidadBarrido>();
            var afiliaciones = new List<AfiliacionInteroperabilidadDetalle>();

            var bitacoraBarrido = new BitacoraInteroperabilidadBarrido();
            typeof(BitacoraInteroperabilidadBarrido).GetProperty(nameof(BitacoraInteroperabilidadBarrido.NumeroBitacora))?
                .SetValue(bitacoraBarrido, 1);

            bitacorasBarrido.Add(bitacoraBarrido);

            var afiliacion = new AfiliacionInteroperabilidadDetalle();
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.NumeroCelular))?
                .SetValue(afiliacion, "960378410");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.IndicadorEstadoAfiliado))?
                .SetValue(afiliacion, "S");
            typeof(AfiliacionInteroperabilidadDetalle).GetProperty(nameof(AfiliacionInteroperabilidadDetalle.FechaBloqueo))?
                .SetValue(afiliacion, DateTime.Now);
            afiliaciones.Add(afiliacion);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<BitacoraInteroperabilidadBarrido>(
                    It.IsAny<Expression<Func<BitacoraInteroperabilidadBarrido, bool>>>(), null!, 0))
                .Returns(bitacorasBarrido);

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(
                    It.IsAny<string>()
                )).Returns(intentosMaximos);

            _repositorioGeneral.Setup(x => x
               .ObtenerPorExpresionConLimite<DirectorioInteroperabilidad>(
                   It.IsAny<Expression<Func<DirectorioInteroperabilidad, bool>>>(), null!, 0))
               .Returns(directorios);

            _repositorioGeneral.Setup(x => x
               .ObtenerPorExpresionConLimite<AfiliacionInteroperabilidadDetalle>(
                   It.IsAny<Expression<Func<AfiliacionInteroperabilidadDetalle, bool>>>(), null!, 0))
               .Returns(afiliaciones);

            _servicioAplicacionCliente.Setup(x => x
                .ListarEntidadesFinancieras()).ReturnsAsync(entidades);

            BarridoContactosCCEExitoso(datosBarridoEntrada, directorios, entidades);

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(
                    It.IsAny<string>())).Returns(limiteMaximo);

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(
                    It.IsAny<string>())).Returns(limiteMontoMinimo);

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(
                    It.IsAny<string>())).Returns(montoMaximoDia);

            var resultado = _servicioAplicacionInteroperabilidad.BarridoContacto(datosBarridoEntrada).Result;

            Assert.IsNotNull(resultado.LimiteMontoMaximo);
            Assert.IsNotNull(resultado.LimiteMontoMinimo);
            Assert.IsNotNull(resultado.MontoMaximoDia);
            Assert.IsNotNull(resultado.ResultadosBarrido);
        }

        public void BarridoContactosCCEExitoso(
            EntradaBarridoDTO datosEntrada, List<DirectorioInteroperabilidad> directoriosCCE, 
            List<EntidadFinancieraTinDTO> entidades)
        {

            var numeroSeguimiento = "123";
            var estructura = AEstructuraBarrido();

            _servicioAplicacionParametro.Setup(x => x
                .ObtenerNumeroSeguimiento(It.IsAny<string>())).Returns(numeroSeguimiento);

            _servicioAplicacionPeticion.Setup(x => x
                .EnviarBarridoContacto(
                    It.IsAny<EstructuraBarridoContacto>()
                )).ReturnsAsync(estructura);

            var resultadoBarrido = _servicioAplicacionInteroperabilidad.BarrerContactosCCE(
                datosEntrada, _calendario, directoriosCCE, entidades, new AfiliacionInteroperabilidadDetalle()).Result;

            Assert.IsNotNull(resultadoBarrido.FirstOrDefault()?.EntidadesReceptor);
        }

        #endregion Barrido Contactos 

        #region Afiliacion
        [TestMethod]
        public void AfiliarDirectorioCceExitoso()
        {
            var numeroSeguimiento = "123";
            var datosEntrada = AAfiliacionDirectorio();
            var datosRespuesta = AEstructuraRegistroDirectorio();

            _servicioAplicacionParametro.Setup(x => x
                .ObtenerNumeroSeguimiento(It.IsAny<string>())).Returns(numeroSeguimiento);

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            _servicioAplicacionPeticion.Setup(x => x
                .EnviarAfiliacionDirectorio(It.IsAny<EstructuraRegistroDirectorio>())).ReturnsAsync(datosRespuesta);

            var resultado = _servicioAplicacionInteroperabilidad.GestionarAfiliacionDirectorioCCE(datosEntrada, null).Result;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(DatosGeneralesInteroperabilidad.Aceptado, resultado.Codigo);
            Assert.AreEqual(numeroSeguimiento,resultado.Datos.NumeroSeguimiento);
        }
        #endregion Afiliacion

        #region Consulta con numero celular
        [TestMethod]
        public void ConsultaCuentaCelularExitoso()
        {

            var consultaCuentaCelular = new ConsultaCuentaCelularDTO{
                NumeroCelular = "987654321",
                CodigoEntidad = "0002",
                CuentaEfectivo = new CuentaEfectivoDTO { NumeroCuenta = "12345653"}
            };

            _servicioAplicacionCliente.Setup(x => x
                .ObtenerDatosCuentaOrigen(It.IsAny<string>()));

            _servicioAplicacionTransaccionOperacion.Setup(x => x
                .ConsultaCuentaReceptorPorCCE(It.IsAny<ConsultaCuentaOperacionDTO>(), true)).ReturnsAsync(new ResultadoConsultaCuentaCCE());

            var resultado = _servicioAplicacionInteroperabilidad.ConsultaCuentaReceptorPorCelular(consultaCuentaCelular).Result;

            Assert.IsNotNull(resultado);
        }
        #endregion Consulta con numero celular

        #region Operaciones
        [TestMethod]
        public void CalcularTotalesExitoso()
        {
            var comisionInteroperabilidad = "0";
            var configuracionComision = AComisionDTO();
            var comisionEsperada = 0.0m;

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(
                    It.IsAny<string>()
                )).Returns(comisionInteroperabilidad);

            _servicioAplicacionTransaccionOperacion.Setup(x => x
                .CalcularMontosTotales(It.IsAny<CalculoComisionDTO>())).ReturnsAsync(configuracionComision);

            var resultado = _servicioAplicacionInteroperabilidad.CalcularMontosTotales(configuracionComision).Result;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(comisionEsperada,resultado.ControlMonto.TotalComision);
        }
        [TestMethod]
        public async Task ValidarOperacionExitosa()
        {
            var limiteMaximo = "2000";
            var limiteMinimo = "1";
            var montoMaximoDia = "1500";
            var cantidadTransaccionesDia = "8";

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MaximoOperacion)).Returns(() => (limiteMaximo).ToString());

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MinimoOperacion)).Returns(() => (limiteMinimo).ToString());

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MontoLimiteDia)).Returns(() => (montoMaximoDia).ToString());

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MaximoOperacionesDia)).Returns(() => (cantidadTransaccionesDia).ToString());

            _repositorioGeneral.Setup(x => x
                .ObtenerCalendarioCuentaEfectivo()).Returns(_calendario);

            var transferenciasLista = new List<TransaccionOrdenTransferenciaInmediata>();
            var transferencia = new TransaccionOrdenTransferenciaInmediata();
            typeof(TransaccionOrdenTransferenciaInmediata).GetProperty(nameof(TransaccionOrdenTransferenciaInmediata.MontoTransferencia))?.
                SetValue(transferencia, 150m);

            transferenciasLista.Add(transferencia);

            _repositorioOperaciones.Setup(x => x
               .ObtenerPorExpresionConLimite<TransaccionOrdenTransferenciaInmediata>(
                   It.IsAny<Expression<Func<TransaccionOrdenTransferenciaInmediata, bool>>>(), null!, 0))
               .Returns(transferenciasLista);

            var datosValidar = new ValidarOperacionDTO
            {
                MontoOperacion = 500,
                CodigoCCIOriginante = "081399999999999999999"
            };
            try
            {
                await _servicioAplicacionInteroperabilidad.ValidarOperacion(datosValidar);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        #endregion Operaciones

        #region Pagos QR
        [TestMethod]
        public void GenerarTokenAutorizacionExitosa()
        {
            var tokenEsperado = "atkfgadhgdsgds3g5sad5dsaf5sadf5sadf3sa5fdsafsaf5dafs3afd53sadf";
            var resultadoToken = new EstructuraObtenerTokenRespuesta
            {
                header = Header(),
                token = "atkfgadhgdsgds3g5sad5dsaf5sadf5sadf3sa5fdsafsaf5dafs3afd53sadf"
            };

            _servicioAplicacionPeticion.Setup(x => x
                .EnviarGenerarToken(
                    It.IsAny<EstructuraObtenerToken>()
                )).ReturnsAsync(resultadoToken);

            var resultado = _servicioAplicacionInteroperabilidad.GenerarAutorizacionToken().Result;

            Assert.IsNotNull(resultado);
            Assert.AreEqual(tokenEsperado,resultado);

        }

        [TestMethod]
        public void GenerarQRExitoso()
        {
            var datosEntaraGenerar = AEntradaGenerarQR();

            var resultadoGenerar = new EstructuraGenerarQRRespuesta
            {
                header = Header(),
                idQr = "ABCDF12354",
                hash = "dsgsadgasgdmmgdomgddgomgdomdsaogsdfsdññokfdsañokafsokf"
                
            };
            _servicioAplicacionPeticion.Setup(x => x
               .EnviarGenerarQr(
                   It.IsAny<EstructuraGenerarQR>(),
                   It.IsAny<string>()
               )).ReturnsAsync(resultadoGenerar);

            GenerarTokenAutorizacionExitosa();

            var resultado = _servicioAplicacionInteroperabilidad.GenerarQR(datosEntaraGenerar).Result;

            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.IdentificadorQR);
            Assert.IsNotNull(resultado.CadenaQR);
        }

        [TestMethod]
        public void LeerQRExitoso()
        {
            var leerQR = new LeerQRDTO{ CadenaHash = "dsgsadgasgdmmgdomgddgomgdomdsaogsdfsdññokfdsañokafsokf" };

            var resultadoLecturaQR = new EstructuraConsultaDatosQR
            {
                header = Header(),
                qr = new qr
                {
                    emisor = "002",
                    idCuenta = "1234235235",
                    idQr = "A23232323",
                    moneda =  "604",
                    fechaRegistro = _calendario.FechaSistema.ToString(),
                    fechaVencimiento = _calendario.FechaSistema.ToString(),
                    qrTipo = "11"
                }

            };

            _servicioAplicacionPeticion.Setup(x => x
               .EnviarLecturaQr(
                   It.IsAny<EstructuraLeerQR>(),
                   It.IsAny<string>()
               )).ReturnsAsync(resultadoLecturaQR);

            GenerarTokenAutorizacionExitosa();

            var resultado = _servicioAplicacionInteroperabilidad.LeerQR(leerQR).Result;

            Assert.IsNotNull(resultado);
            Assert.IsNotNull(resultado.IdentificadorQR);
            Assert.IsNotNull(resultado.CodigoMoneda);
            Assert.IsNotNull(resultado.IdentificadorCuenta);
        }


        [TestMethod]
        public void ObtenerDatosCuentaReceptoraQRExitoso()
        {
            var cuentaOriginante = ObtenerCuentaEfectivoDTO();
            var directoriosCCE = ADirectoriosCCE();
            var respuestaConsulta = AConsultarCuentaQR();

            _servicioAplicacionCliente.Setup(x => x
               .ObtenerDatosCuentaOrigen(
                   It.IsAny<string>()
               )).ReturnsAsync(cuentaOriginante);

            _repositorioOperaciones.Setup(x => x
                .ObtenerPorExpresionConLimite<DirectorioInteroperabilidad>(
                    It.IsAny<Expression<Func<DirectorioInteroperabilidad, bool>>>(), null!, 0))
                .Returns(directoriosCCE);

            _servicioAplicacionTransaccionOperacion.Setup(x => x
                .ConsultaCuentaReceptorPorCCE(
                    It.IsAny<ConsultaCuentaOperacionDTO>(), true
                )).ReturnsAsync(new ResultadoConsultaCuentaCCE { CodigoEntidadReceptora = "0002"});

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MaximoOperacion)).Returns(() => (10000).ToString());

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MinimoOperacion)).Returns(() => (1).ToString());

            var resultado = _servicioAplicacionInteroperabilidad.ObtenerDatosCuentaReceptoraQR(respuestaConsulta).Result;

            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void ConsultaCuentaCompletaQR()
        {
            var entidades = AEntidadesCCE();
            var consultaCuenta = new ConsultaCuentaCompletaQRDTO
            {
                NumeroCuentaOriginante = "1241254215",
                CadenaHash = "dsgkdsldslfspldgfsdgpldsags"
            };

            _servicioAplicacionCliente.Setup(x => x
               .ListarEntidadesFinancieras()).ReturnsAsync(entidades);

            LeerQRExitoso();
            ObtenerDatosCuentaReceptoraQRExitoso();

            _servicioAplicacionParametro.Setup(x => x
               .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MaximoOperacion)).Returns(() => (10000).ToString());

            _servicioAplicacionParametro.Setup(x => x
                .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MinimoOperacion)).Returns(() => (1).ToString());

            _servicioAplicacionParametro.Setup(x => x
               .obtenerParametrosConfiguracion(DatosGeneralesInteroperabilidad.MontoLimiteDia)).Returns(() => (15000).ToString());

            var resultado = _servicioAplicacionInteroperabilidad.ConsultarCuentaReceptorPorQR(consultaCuenta).Result;

            Assert.IsNotNull(resultado);
        }
        #endregion Pagos QR

        #region Obtener datos prueba

        public ConsultarCuentaQRDTO AConsultarCuentaQR()
        {
            return new ConsultarCuentaQRDTO()
            {
                NumeroCuentaOriginante = "012645351651",
                IdentificadorQR = "ID32GFDG32",
                CodigoEntidadReceptora = "0002",
                IdentificadorCuenta = "08139876543213654",
                CodigoMoneda = "604",
            };
        }

        public CuentaEfectivoDTO ObtenerCuentaEfectivoDTO()
        {
            return new CuentaEfectivoDTO()
            {
                NumeroDocumento = "12365",
                Titular = "Titular",
                TipoDocumentoOriginante = new TipoDocumentoTinDTO
                { CodigoTipoDocumento = 1, EsTipoPersonaJuridica = true },
                CodigoMoneda = "1",
                NumeroCuenta = "123456789",
                CodigoCuentaInterbancario = "81323500423525284326",
                IndicadorTipoCuenta = "I",
                IndicadorTransferenciaCce = "S",
                SaldoDisponible = 10000
            };
        }

        public headerRespuesta Header()
        {
            return new headerRespuesta
            {
                codReturn = DatosValoresFijos.RespuestaExitosaQR
            };
        }

        public GenerarQRDTO AEntradaGenerarQR()
        {
            return new GenerarQRDTO
            {
                TipoQr = "11",
                CodigoCuentaInterbancario = "08139999999999",
                NombreCliente = "luis",
                TipoGeneracionQR = "TEXT",
                CodigoMoneda = "1"
            };
        }
        public EntradaBarridoDTO AEntradaBarrido()
        {
            return new EntradaBarridoDTO
            {
                CodigoCCI = "000265432119876543217",
                NumeroCelularOrigen = "987564321",

                ContactosBarrido = new List<ContactosBarrido>
                {
                    new ContactosBarrido
                    {
                        NumeroCelular = "987654321"
                    }
                }
            };
        }
        public EstructuraBarridoContacto AEstructuraBarrido()
        {
            return new EstructuraBarridoContacto
            {
                BusMsg = ABusMSG()
            };
        }

        public BusMsg ABusMSG()
        {

            var regnRspn = new RegnRspn
            {
                PrxRspnSts = "ACTC",
                StsRsnInf = new StsRsnInf { Prtry = "DS24" }
            };
            return new BusMsg
            {
                Document = new Document
                {
                    PrxyLookUpRspn = new PrxyLookUpRspn
                    {
                        LkUpRspn = new LkUpRspn
                        {
                            RegnRspn = regnRspn
                        }

                    },
                    SplmtryData = new SplmtryData
                    {
                        Envlp = new Envlp
                        {
                            Directories = ADirectorios()
                        }
                    },
                    PrxyRegnRspn = new PrxyRegnRspn
                    {
                        RegnRspn = regnRspn
                    }
                }
            };
        }

        public Directories[] ADirectorios()
        {
            return new Directories[]
            {
                new Directories
                {
                     Directory = "0903",
                Proxy = new Proxy[]
                {
                    new Proxy
                    {
                        Type = "M",
                        Value = "900000001",
                        Present = "true",
                        BankCode = new List<string>
                        {
                            "0002"
                        }
                    }
                }
                }

            };
        }

        public EntradaAfiliacionDirectorioDTO AAfiliacionDirectorio()
        {
            return new EntradaAfiliacionDirectorioDTO
            {
                CodigoEntidadOriginante = "0813",
                TipoInstruccion = "NEWR",
                NumeroCelular = "987655555",
                TipoOperacion = "19",
                CodigoCliente = "114019106641",
                NumeroCuentaAfiliada = "001211101939525",
                CodigoCuentaInterbancario = "81300121110193955555",
                IndicadorModificarNumero = "N",
                NumeroAntiguo = "",
                NumeroTarjeta = "4772000011900238"
            };
        }
        public List<DirectorioInteroperabilidad> ADirectoriosCCE()
        {
            var directorios = new List<DirectorioInteroperabilidad>();
            var directorio = new DirectorioInteroperabilidad();

            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.
                SetValue(directorio, "0901");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.
                SetValue(directorio, "YAPE");
            directorios.Add(directorio);
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.
                SetValue(directorio, "0902");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.
                SetValue(directorio, "PLIN");
            directorios.Add(directorio);
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.
                SetValue(directorio, "0903");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.
               SetValue(directorio, "DIRECTORIO CCE");
            directorios.Add(directorio);
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.CodigoDirectorio))?.
                SetValue(directorio, "0904");
            typeof(DirectorioInteroperabilidad).GetProperty(nameof(DirectorioInteroperabilidad.NombreDirectorio))?.
               SetValue(directorio, "BIM");
            directorios.Add(directorio);

            return directorios;
        }

        public List<EntidadFinancieraTinDTO> AEntidadesCCE()
        {
            return new List<EntidadFinancieraTinDTO>
            {
                new EntidadFinancieraTinDTO
                {
                    CodigoEstadoSign = "123",
                    CodigoEntidad = "0002",
                    NombreEntidad = "BCP",
                }
            };
        }

        public EstructuraRegistroDirectorio AEstructuraRegistroDirectorio()
        {
            return new EstructuraRegistroDirectorio
            {
                BusMsg = ABusMSG()
            };
        }

        public CalculoComisionDTO AComisionDTO()
        {
            return new CalculoComisionDTO
            {
                MontoOperacion = 100,
                ControlMonto = ObtenerControlMonto(),
                Comision = ObtenerComision(),
                SaldoActual = 2000,
                MontoMinimoCuenta = 10,
                MismoTitular = "M"
            };
        }
        public ControlMontoDTO ObtenerControlMonto()
        {
            return new ControlMontoDTO()
            {
                CodigoMoneda = "1",
                CodigoMonedaOrigen = "1",
                Monto = 0,
                MontoComisionEntidad = 0,
                MontoComisionCce = 0,
                Itf = 0,
                TotalComision = 0,
                Total = 0
            };
        }

        public ComisionDTO ObtenerComision()
        {
            return new ComisionDTO()
            {
                Id = 1,
                IdTipoTransferencia = 2,
                CodigoComision = "0101",
                CodigoMoneda = "1",
                CodigoAplicacionTarifa = "M",
                Porcentaje = 0.5M,
                Minimo = 2,
                Maximo = 5,
                IndicadorPorcentaje = "S",
                IndicadorFijo = "N",
                PorcentajeCCE = 0.5M,
                MinimoCCE = 2,
                MaximoCCE = 3
            };
        }
        #endregion Obtener datos prueba
    }
}
