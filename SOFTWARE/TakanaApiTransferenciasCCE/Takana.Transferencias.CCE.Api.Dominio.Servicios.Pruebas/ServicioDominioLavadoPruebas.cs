using Microsoft.VisualStudio.TestTools.UnitTesting;
using Takana.Transferencias.CCE.Api.Common.Constantes;
using Takana.Transferencias.CCE.Api.Common.DTOs.CL;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.BA;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CC;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CF;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CG;
using Takana.Transferencias.CCE.Api.Dominio.Entidades.CL;

namespace Takana.Transferencias.CCE.Api.Dominio.Servicios.Pruebas
{
    [TestClass]
    public class ServicioDominioLavadoPruebas
    {
        #region Declaracion Variables
        private ServicioDominioLavado _servicioDominioLavado;
        private ClienteExternoDTO _datosCliente = new ClienteExternoDTO();
        #endregion

        #region Inicializaciones
        [TestInitialize]
        public void Inicializar()
        {
            _servicioDominioLavado = new ServicioDominioLavado();

            _datosCliente = new ClienteExternoDTO()
            {
                CodigoCliente = "CAJERROC",
                CodigoCuentaInterbancaria = "81300121110200937057",
                Nombres = "Luis torres huanca",
                NumeroDocumento = "73646901",
                CodigoTipoDocumento = "2",
                CodigoUsuario = "CAJERROC",
                TipoPersona = "2",
                Telefono = "999621321",
                TipoCliente = "EX"
            };
        }  

        #endregion

        #region Lavado Transferencias Entrantes
        /// <summary>
        /// Prueba Unitaria que genera un registro de lavado de menor cuantia exitoso
        /// </summary>
        [TestMethod]
        public void GenerarLavadoMenorCuantiaTransferenciaInmediataEntrante()
        {
            var cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(cliente, "F");

            var personaFisica = new PersonaFisica();
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoCliente))?.SetValue(personaFisica, "CAJERROC");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.IndicadorSexo))?.SetValue(personaFisica, "M");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoEstadoCivil))?.SetValue(personaFisica, "01");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.Cliente))?.SetValue(personaFisica, cliente);

            typeof(Cliente).GetProperty(nameof(Cliente.PersonaFisica))?.SetValue(cliente, personaFisica);

            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");

            var tipoDocumento = new Entidades.CL.TipoDocumento();
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.CodigoTipoDocumento))?.SetValue(tipoDocumento, "1");
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.CodigoTipoDocumentoInmediataCce))?.SetValue(tipoDocumento, "2");
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.IndicadorPersonaNatural))?.SetValue(tipoDocumento, General.Si);

            var documentoClienteOriginante = new List<DocumentoCliente>();
            var documento = new DocumentoCliente();
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.CodigoTipoDocumento))?.SetValue(documento, "1");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.NumeroDocumento))?.SetValue(documento, "73646962");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.Cliente))?.SetValue(documento, cliente);
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.TipoDocumento))?.SetValue(documento, tipoDocumento);
            documentoClienteOriginante.Add(documento);

            typeof(Cliente).GetProperty(nameof(Cliente.Documentos))?.SetValue(cliente, documentoClienteOriginante);

            var datosTransferencia = new Transferencia();
            var datosAsientoContable = new AsientoContable();
            var datosEntidadFinanciera = new EntidadFinancieraInmediata();

            var menorCuantia = _servicioDominioLavado.RegistrarLavadoMenorCuantiaTransferenciaEntrante(
                subTipoTransaccion, datosTransferencia, datosAsientoContable, cliente, _datosCliente, datosEntidadFinanciera, 12345678);

            Assert.IsNotNull(menorCuantia);
        }

        /// <summary>
        /// Prueba Unitaria que genera un registro de lavado de operacion unica exitoso
        /// </summary>
        [TestMethod]
        public void GenerarLavadoOperacionUnicaTransferenciaEntrante()
        {
            var cliente = new Cliente();
            typeof(Cliente).GetProperty(nameof(Cliente.CodigoCliente))?.SetValue(cliente, "1");
            typeof(Cliente).GetProperty(nameof(Cliente.NombreCliente))?.SetValue(cliente, "USUARIO PRUEBA");
            typeof(Cliente).GetProperty(nameof(Cliente.TipoPersona))?.SetValue(cliente, "F");

            var personaFisica = new PersonaFisica();
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoCliente))?.SetValue(personaFisica, "CAJERROC");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoNombre))?.SetValue(personaFisica, "Pepe");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.PrimerApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.SegundoApellido))?.SetValue(personaFisica, "Ramirez");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.IndicadorSexo))?.SetValue(personaFisica, "M");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.CodigoEstadoCivil))?.SetValue(personaFisica, "01");
            typeof(PersonaFisica).GetProperty(nameof(PersonaFisica.Cliente))?.SetValue(personaFisica, cliente);

            typeof(Cliente).GetProperty(nameof(Cliente.PersonaFisica))?.SetValue(cliente, personaFisica);

            var subTipoTransaccion = new SubTipoTransaccion();
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSistema))?.SetValue(subTipoTransaccion, "CC");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoSubTipoTransaccion))?.SetValue(subTipoTransaccion, "12");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.CodigoTipoTransaccion))?.SetValue(subTipoTransaccion, "41");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.DescripcionSubTransaccion))?.SetValue(subTipoTransaccion, "PRUEBA");
            typeof(SubTipoTransaccion).GetProperty(nameof(SubTipoTransaccion.AplicaContabilizadon))?.SetValue(subTipoTransaccion, "S");

            var tipoDocumento = new Entidades.CL.TipoDocumento();
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.CodigoTipoDocumento))?.SetValue(tipoDocumento, "1");
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.CodigoTipoDocumentoInmediataCce))?.SetValue(tipoDocumento, "2");
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.IndicadorPrioridadPersonaNatural))?.SetValue(tipoDocumento, 1);
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.IndicadorPrioridadPersonaJuridica))?.SetValue(tipoDocumento, 2);
            typeof(Entidades.CL.TipoDocumento).GetProperty(nameof(Entidades.CL.TipoDocumento.IndicadorPersonaNatural))?.SetValue(tipoDocumento, General.Si);

            var documentoClienteOriginante = new List<DocumentoCliente>();
            var documento = new DocumentoCliente();
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.CodigoTipoDocumento))?.SetValue(documento, "1");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.NumeroDocumento))?.SetValue(documento, "73646962");
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.Cliente))?.SetValue(documento, cliente);
            typeof(DocumentoCliente).GetProperty(nameof(DocumentoCliente.TipoDocumento))?.SetValue(documento, tipoDocumento);
            documentoClienteOriginante.Add(documento);

            var tipoDireccion = new TipoDireccion();
            typeof(TipoDireccion).GetProperty(nameof(TipoDireccion.IndicadorPrioridadNatural))?.SetValue(tipoDireccion, 1);
            typeof(TipoDireccion).GetProperty(nameof(TipoDireccion.IndicadorPrioridadJuridica))?.SetValue(tipoDireccion, 2);

            var direccionClienteOriginante = new List<DireccionCliente>();
            var direccion = new DireccionCliente();
            typeof(DireccionCliente).GetProperty(nameof(DireccionCliente.Cliente))?.SetValue(direccion, cliente);
            typeof(DireccionCliente).GetProperty(nameof(DireccionCliente.TipoDireccion))?.SetValue(direccion, tipoDireccion);
            direccionClienteOriginante.Add(direccion);

            typeof(Cliente).GetProperty(nameof(Cliente.Documentos))?.SetValue(cliente, documentoClienteOriginante);
            typeof(Cliente).GetProperty(nameof(Cliente.Direcciones))?.SetValue(cliente, direccionClienteOriginante);

            var datosTransferencia = new Transferencia();
            var datosAsientoContable = new AsientoContable();
            var datosEntidadFinanciera = new EntidadFinancieraInmediata();

            var operacionUnica = _servicioDominioLavado.RegistrarLavadoOperacionUnicaTransferenciaEntrante(
                subTipoTransaccion, datosTransferencia, datosAsientoContable, cliente, _datosCliente, DateTime.Now, datosEntidadFinanciera, 12345678);

            Assert.IsNotNull(operacionUnica);
        }
        #endregion

    }
}

