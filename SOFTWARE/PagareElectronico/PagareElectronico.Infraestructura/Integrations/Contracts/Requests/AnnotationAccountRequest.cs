using System.Text.Json.Serialization;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests.Common;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests
{
    /// <summary>
    /// Representa la solicitud que CAVALI espera para annotation-account.
    /// </summary>
    public sealed class AnnotationAccountRequest
    {
        /// <summary>
        /// Obtiene o establece la llave del pagaré.
        /// </summary>
        public AnnotationPromissoryNoteKey PromissoryNoteKey { get; set; } = new();

        /// <summary>
        /// Obtiene o establece el archivo de firma.
        /// </summary>
        public AnnotationSignatureFile SignatureFile { get; set; } = new();

        /// <summary>
        /// Obtiene o establece el detalle del pagaré.
        /// </summary>
        public AnnotationPromissoryNoteDetail PromissoryNoteDetail { get; set; } = new();
    }

    /// <summary>
    /// Representa la llave del pagaré para annotation-account.
    /// </summary>
    public sealed class AnnotationPromissoryNoteKey
    {
        /// <summary>
        /// Obtiene o establece el código de participante.
        /// </summary>
        public int ParticipantCode { get; set; }

        /// <summary>
        /// Obtiene o establece el código de banca.
        /// </summary>
        public int Banking { get; set; }

        /// <summary>
        /// Obtiene o establece el código único.
        /// </summary>
        public string UniqueCode { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el código de producto.
        /// </summary>
        public int Product { get; set; }

        /// <summary>
        /// Obtiene o establece el número de crédito.
        /// </summary>
        public string CreditNumber { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa el archivo de firma enviado a CAVALI.
    /// </summary>
    public sealed class AnnotationSignatureFile
    {
        /// <summary>
        /// Obtiene o establece el tipo de firma.
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Obtiene o establece el contenido de la firma.
        /// </summary>
        public string Signature { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa el detalle del pagaré enviado a CAVALI.
    /// </summary>
    public sealed class AnnotationPromissoryNoteDetail
    {
        /// <summary>
        /// Obtiene o establece el indicador conditionJustSign.
        /// </summary>
        public int ConditionJustSign { get; set; }

        /// <summary>
        /// Obtiene o establece el número de documento del titular.
        /// </summary>
        public string? OwnerDocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la fecha de emisión en formato yyyy-MM-dd.
        /// </summary>
        public string IssuedDate { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el lugar de emisión.
        /// </summary>
        public string IssuedPlace { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el indicador special.
        /// </summary>
        public int Special { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de vencimiento en formato yyyy-MM-dd.
        /// </summary>
        public string ExpirationDate { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el monto.
        /// </summary>
        public string Amount { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el código de moneda.
        /// </summary>
        public int Currency { get; set; }

        /// <summary>
        /// Obtiene o establece el importe del interés compensatorio.
        /// </summary>
        public string CompensatoryInterestAmount { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el primer período.
        /// </summary>
        public string PeriodOne { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el importe del interés compensatorio en mora.
        /// </summary>
        public string CompensatoryInterestArrears { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el segundo período.
        /// </summary>
        public string PeriodTwo { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el importe del interés moratorio.
        /// </summary>
        public string InterestArrears { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el tercer período.
        /// </summary>
        public string PeriodThree { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece las cláusulas especiales.
        /// </summary>
        public string SpecialClauses { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el indicador de envío automático de constancia informativa.
        /// </summary>
        public int AutomaticSendInfConstancy { get; set; }

        /// <summary>
        /// Obtiene o establece la información del cliente.
        /// </summary>
        public AnnotationClientDetail ClientDetail { get; set; } = new();

        /// <summary>
        /// Obtiene o establece el campo adicional 2.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalField2 { get; set; }
    }

    /// <summary>
    /// Representa la información del cliente en annotation-account.
    /// </summary>
    public sealed class AnnotationClientDetail
    {
        /// <summary>
        /// Obtiene o establece el nombre del cliente.
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el tipo de documento.
        /// </summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// Obtiene o establece el número de documento.
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del cliente.
        /// </summary>
        public string EmailClient { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el domicilio.
        /// </summary>
        public string Domicile { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el estado civil.
        /// </summary>
        public int CivilStatus { get; set; }

        /// <summary>
        /// Obtiene o establece la información del cónyuge.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AnnotationSpouse? Spouse { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de representantes legales.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnnotationLegalRepresentative>? LegalRepresentative { get; set; }

        /// <summary>
        /// Obtiene o establece la lista de garantías.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnnotationGuarantee>? Guarantee { get; set; }
    }

    /// <summary>
    /// Representa la información del cónyuge.
    /// </summary>
    public sealed class AnnotationSpouse
    {
        /// <summary>
        /// Obtiene o establece el nombre del cónyuge.
        /// </summary>
        public string SpouseName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento del cónyuge.
        /// </summary>
        public string SpouseDocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico del cónyuge.
        /// </summary>
        public string SpouseEmail { get; set; } = string.Empty;
    }

    /// <summary>
    /// Representa un representante legal.
    /// </summary>
    public sealed class AnnotationLegalRepresentative : ExternalLegalRepresentativeBase
    {
    }

    /// <summary>
    /// Representa una garantía.
    /// </summary>
    public sealed class AnnotationGuarantee
    {
        /// <summary>
        /// Obtiene o establece el tipo de garantía.
        /// </summary>
        public int GuaranteeType { get; set; }

        /// <summary>
        /// Obtiene o establece la razón social.
        /// </summary>
        public string BusinessName { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el número de documento.
        /// </summary>
        public string DocumentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el estado civil.
        /// </summary>
        public int CivilStatus { get; set; }

        /// <summary>
        /// Obtiene o establece el domicilio.
        /// </summary>
        public string Domicile { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece el correo electrónico de la garantía.
        /// </summary>
        public string EmailGuarantee { get; set; } = string.Empty;

        /// <summary>
        /// Obtiene o establece la lista de representantes legales de la garantía.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<AnnotationGuaranteeLegalRepresentative>? GuaranteeLegalRepresentative { get; set; }
    }

    /// <summary>
    /// Representa un representante legal de una garantía.
    /// </summary>
    public sealed class AnnotationGuaranteeLegalRepresentative : ExternalLegalRepresentativeBase
    {
    }
}