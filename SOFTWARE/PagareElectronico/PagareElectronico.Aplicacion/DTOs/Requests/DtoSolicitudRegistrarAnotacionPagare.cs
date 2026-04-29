namespace PagareElectronico.Application.DTOs.Requests
{
    /// <summary>
    /// Representa la solicitud interna para registrar y anotar un pagaré.
    /// </summary>
    public sealed class DtoSolicitudRegistrarAnotacionPagare : DtoPagareIdentificadorSolicitud
    {
        /// <summary>
        /// Obtiene o establece el tipo de firma requerido por CAVALI.
        /// </summary>
        public int CodigoTipoFirma { get; set; }

        /// <summary>
        /// Obtiene o establece el contenido de la firma en formato Base64.
        /// </summary>
        public string FirmaBase64 { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el detalle del pagaré.
        /// </summary>
        public DtoDetallePagareSolicitud DetallePagare { get; set; } = new();
    }

    /// <summary>
    /// Representa el detalle interno del pagaré.
    /// </summary>
    public sealed class DtoDetallePagareSolicitud
    {
        /// <summary>
        /// Obtiene o establece el número de documento del titular del pagaré.
        /// </summary>
        public string NumeroDocumentoTitular { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la fecha de emisión del pagaré.
        /// </summary>
        public DateOnly FechaEmision { get; set; }

        /// <summary>
        /// Obtiene o establece el lugar de emisión del pagaré.
        /// </summary>
        public string LugarEmision { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la fecha de vencimiento del pagaré.
        /// </summary>
        public DateOnly FechaVencimiento { get; set; }

        /// <summary>
        /// Obtiene o establece el monto principal del pagaré.
        /// </summary>
        public decimal Monto { get; set; }

        /// <summary>
        /// Obtiene o establece el código de moneda del pagaré.
        /// </summary>
        public int CodigoMoneda { get; set; }

        /// <summary>
        /// Obtiene o establece las cláusulas especiales del pagaré.
        /// </summary>
        public string? DescripcionClausulasEspeciales { get; set; }

        /// <summary>
        /// Obtiene o establece la información del cliente.
        /// </summary>
        public DtoClienteSolicitud Cliente { get; set; } = new();

        /// <summary>
        /// Obtiene o establece la información del cónyuge.
        /// </summary>
        public DtoConyugeSolicitud? Conyuge { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de representantes legales.
        /// </summary>
        public List<DtoRepresentanteLegalSolicitud>? RepresentantesLegales { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de garantías.
        /// </summary>
        public List<DtoGarantiaSolicitud>? Garantias { get; set; }

        /// <summary>
        /// Obtiene o establece el campo adicional 2.
        /// </summary>
        public string? CampoAdicional2 { get; set; }
    }

    /// <summary>
    /// Representa la información interna del cliente.
    /// </summary>
    public sealed class DtoClienteSolicitud
    {
        /// <summary>
        /// Obtiene o establece el nombre completo o razón social del cliente.
        /// </summary>
        public string NombreCliente { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el tipo de documento del cliente.
        /// </summary>
        public int CodigoTipoDocumento { get; set; }

        /// <summary>
        /// Obtiene o establece el número de documento del cliente.
        /// </summary>
        public string NumeroDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del cliente.
        /// </summary>
        public string CorreoCliente { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el domicilio del cliente.
        /// </summary>
        public string Domicilio { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el estado civil del cliente.
        /// </summary>
        public int CodigoEstadoCivil { get; set; }
    }

    /// <summary>
    /// Representa la información interna del cónyuge.
    /// </summary>
    public sealed class DtoConyugeSolicitud
    {
        /// <summary>
        /// Obtiene o establece el nombre completo del cónyuge.
        /// </summary>
        public string NombreConyuge { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento del cónyuge.
        /// </summary>
        public string NumeroDocumentoConyuge { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del cónyuge.
        /// </summary>
        public string CorreoConyuge { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa un representante legal interno.
    /// </summary>
    public sealed class DtoRepresentanteLegalSolicitud : DtoRepresentanteLegalBaseSolicitud
    {
    }

    /// <summary>
    /// Representa una garantía interna.
    /// </summary>
    public sealed class DtoGarantiaSolicitud
    {
        /// <summary>
        /// Obtiene o establece el tipo de garantía.
        /// </summary>
        public int CodigoTipoGarantia { get; set; }

        /// <summary>
        /// Obtiene o establece la razón social de la garantía.
        /// </summary>
        public string RazonSocial { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento de la garantía.
        /// </summary>
        public string NumeroDocumento { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el estado civil de la garantía.
        /// </summary>
        public int CodigoEstadoCivil { get; set; }

        /// <summary>
        /// Obtiene o establece el domicilio de la garantía.
        /// </summary>
        public string Domicilio { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico de la garantía.
        /// </summary>
        public string CorreoGarantia { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece los representantes legales de la garantía.
        /// </summary>
        public List<DtoRepresentanteLegalGarantiaSolicitud>? RepresentantesLegales { get; set; }
    }

    /// <summary>
    /// Representa un representante legal de una garantía.
    /// </summary>
    public sealed class DtoRepresentanteLegalGarantiaSolicitud : DtoRepresentanteLegalBaseSolicitud
    {
    }
}