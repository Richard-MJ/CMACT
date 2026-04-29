using System.Globalization;
using Microsoft.Extensions.Options;
using PagareElectronico.Application.DTOs.Requests;
using PagareElectronico.Infrastructure.Configuration;
using PagareElectronico.Infrastructure.Integrations.Cavali.Contracts.Requests;

namespace PagareElectronico.Infrastructure.Integrations.Cavali.Mappers
{
    /// <summary>
    /// Mapea la solicitud interna de registro y anotación al contrato annotation-account de CAVALI.
    /// </summary>
    public sealed class AnnotationAccountRequestMapper
    {
        private readonly CavaliApiOptions _options;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AnnotationAccountRequestMapper"/>.
        /// </summary>
        /// <param name="options">Opciones de configuración de CAVALI.</param>
        public AnnotationAccountRequestMapper(IOptions<CavaliApiOptions> options)
        {
            _options = options.Value;
        }

        /// <summary>
        /// Mapea la solicitud interna al request externo de CAVALI.
        /// </summary>
        /// <param name="source">Solicitud interna.</param>
        /// <returns>Solicitud externa para CAVALI.</returns>
        public AnnotationAccountRequest Map(DtoSolicitudRegistrarAnotacionPagare source)
        {
            return new AnnotationAccountRequest
            {
                PromissoryNoteKey = new AnnotationPromissoryNoteKey
                {
                    ParticipantCode = _options.ParticipantCode,
                    Banking = _options.BankCode,
                    UniqueCode = source.CodigoUnico,
                    Product = _options.ProductCode,
                    CreditNumber = source.NumeroCredito
                },
                SignatureFile = new AnnotationSignatureFile
                {
                    Type = source.CodigoTipoFirma,
                    Signature = source.FirmaBase64
                },
                PromissoryNoteDetail = new AnnotationPromissoryNoteDetail
                {
                    ConditionJustSign = 1,
                    OwnerDocumentNumber = string.Empty,
                    IssuedDate = source.DetallePagare.FechaEmision.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    IssuedPlace = source.DetallePagare.LugarEmision,
                    Special = !string.IsNullOrEmpty(source.DetallePagare.DescripcionClausulasEspeciales) ? 1 : 2,
                    ExpirationDate = source.DetallePagare.FechaVencimiento.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    Amount = source.DetallePagare.Monto.ToString("0.00", CultureInfo.InvariantCulture),
                    Currency = source.DetallePagare.CodigoMoneda,
                    CompensatoryInterestAmount = "0",
                    PeriodOne = string.Empty,
                    CompensatoryInterestArrears = "0",
                    PeriodTwo = string.Empty,
                    InterestArrears = "0",
                    PeriodThree = string.Empty,
                    SpecialClauses = source.DetallePagare.DescripcionClausulasEspeciales ?? string.Empty,
                    AutomaticSendInfConstancy = 0,
                    ClientDetail = MapearCliente(source.DetallePagare),
                    AdditionalField2 = string.IsNullOrWhiteSpace(source.DetallePagare.CampoAdicional2)
                        ? null : source.DetallePagare.CampoAdicional2
                }
            };
        }

        /// <summary>
        /// Mapea la información interna del cliente al nodo clientDetail de CAVALI.
        /// </summary>
        /// <param name="detallePagare">Detalle interno del pagaré.</param>
        /// <returns>Detalle del cliente para CAVALI.</returns>
        private static AnnotationClientDetail MapearCliente(DtoDetallePagareSolicitud detallePagare)
        {
            return new AnnotationClientDetail
            {
                ClientName = detallePagare.Cliente.NombreCliente,
                DocumentType = detallePagare.Cliente.CodigoTipoDocumento,
                DocumentNumber = detallePagare.Cliente.NumeroDocumento,
                EmailClient = detallePagare.Cliente.CorreoCliente,
                Domicile = string.IsNullOrWhiteSpace(detallePagare.Cliente.Domicilio)
                    ? detallePagare.Cliente.Domicilio : detallePagare.Cliente.Domicilio.Length > 100
                        ? detallePagare.Cliente.Domicilio.Substring(0, 100)
                        : detallePagare.Cliente.Domicilio,
                CivilStatus = detallePagare.Cliente.CodigoEstadoCivil,
                Spouse = detallePagare.Conyuge is null
                    ? null
                    : new AnnotationSpouse
                    {
                        SpouseName = detallePagare.Conyuge.NombreConyuge,
                        SpouseDocumentNumber = detallePagare.Conyuge.NumeroDocumentoConyuge,
                        SpouseEmail = detallePagare.Conyuge.CorreoConyuge
                    },
                LegalRepresentative = detallePagare.RepresentantesLegales is null || detallePagare.RepresentantesLegales.Count == 0
                    ? null
                    : detallePagare.RepresentantesLegales
                        .Select(x => new AnnotationLegalRepresentative
                        {
                            LegalRepresentativeName = x.NombreRepresentanteLegal,
                            DocumentNumber = x.NumeroDocumento,
                            EmailLegalRepresentative = x.CorreoRepresentanteLegal
                        })
                        .ToList(),
                Guarantee = detallePagare.Garantias is null || detallePagare.Garantias.Count == 0
                    ? null
                    : detallePagare.Garantias
                        .Select(x => new AnnotationGuarantee
                        {
                            GuaranteeType = x.CodigoTipoGarantia,
                            BusinessName = x.RazonSocial,
                            DocumentNumber = x.NumeroDocumento,
                            CivilStatus = x.CodigoEstadoCivil,
                            Domicile = string.IsNullOrWhiteSpace(x.Domicilio)
                                ? x.Domicilio : x.Domicilio.Length > 100
                                    ? x.Domicilio.Substring(0, 100)
                                    : x.Domicilio,
                            EmailGuarantee = x.CorreoGarantia,
                            GuaranteeLegalRepresentative = x.RepresentantesLegales is null || x.RepresentantesLegales.Count == 0
                                ? null
                                : x.RepresentantesLegales
                                    .Select(y => new AnnotationGuaranteeLegalRepresentative
                                    {
                                        LegalRepresentativeName = y.NombreRepresentanteLegal,
                                        DocumentNumber = y.NumeroDocumento,
                                        EmailLegalRepresentative = y.CorreoRepresentanteLegal
                                    })
                                    .ToList()
                        })
                        .ToList()
            };
        }
    }
}