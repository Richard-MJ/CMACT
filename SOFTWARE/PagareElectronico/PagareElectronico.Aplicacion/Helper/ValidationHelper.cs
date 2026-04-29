using System.Text.RegularExpressions;
using PagareElectronico.Application.Exceptions;
using PagareElectronico.Application.DTOs.Requests;

namespace PagareElectronico.Aplicacion.Helper
{
    internal static class ValidationHelper
    {
        /// <summary>
        /// Valida la solicitud de registro y anotación.
        /// </summary>
        /// <param name="request">Solicitud a validar.</param>
        /// <summary>
        /// Valida la solicitud de registro y anotación.
        /// </summary>
        /// <param name="request">Solicitud a validar.</param>
        public static void ValidarSolicitudRegistro(this DtoSolicitudRegistrarAnotacionPagare request)
        {
            if (request is null)
                throw new ValidationException("40001", "La solicitud es obligatoria.");

            if (string.IsNullOrWhiteSpace(request.CodigoUnico))
                throw new ValidationException("40003", "El código único es obligatorio.");

            UtilsHelper.ValidarLongitudMaxima(request.CodigoUnico, 20, "40022", "El código único no debe exceder 20 caracteres.");
            UtilsHelper.ValidarAlfanumericoConGuion(request.CodigoUnico, "40023", "El código único solo permite letras, números y guion.");

            if (!string.IsNullOrWhiteSpace(request.NumeroCredito))
            {
                UtilsHelper.ValidarLongitudMaxima(request.NumeroCredito, 20, "40024", "El número de crédito no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(request.NumeroCredito, "40025", "El número de crédito solo permite letras, números y guion.");
            }

            if (request.CodigoTipoFirma != 1 && request.CodigoTipoFirma != 2)
                throw new ValidationException("40005", "El tipo de firma debe ser 1 (firma con certificado digital) o 2 (firma electrónica).");

            if (string.IsNullOrWhiteSpace(request.FirmaBase64))
                throw new ValidationException("40006", "La firma en Base64 es obligatoria.");

            if (!UtilsHelper.EsBase64Valido(request.FirmaBase64))
                throw new ValidationException("40026", "La firma en Base64 no tiene un formato válido.");

            if (request.DetallePagare is null)
                throw new ValidationException("40007", "El detalle del pagaré es obligatorio.");

            var detalle = request.DetallePagare;

            if (!string.IsNullOrWhiteSpace(detalle.NumeroDocumentoTitular))
                UtilsHelper.ValidarLongitudMaxima(detalle.NumeroDocumentoTitular, 100, "40027", "El número de documento del titular no debe exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(detalle.LugarEmision))
                UtilsHelper.ValidarLongitudMaxima(detalle.LugarEmision, 50, "40028", "El lugar de emisión no debe exceder 50 caracteres.");

            if (detalle.FechaEmision != default &&
                detalle.FechaVencimiento != default &&
                detalle.FechaVencimiento < detalle.FechaEmision)
            {
                throw new ValidationException("40029", "La fecha de vencimiento no puede ser menor a la fecha de emisión.");
            }

            if (detalle.Monto < 0)
                throw new ValidationException("40030", "El monto no puede ser menor a cero.");

            if (detalle.Monto > 0 && !UtilsHelper.TienePrecision(detalle.Monto, 10, 2))
                throw new ValidationException("40031", "El monto debe tener como máximo 10 dígitos y 2 decimales.");

            if (detalle.CodigoMoneda != 0 && detalle.CodigoMoneda != 1 && detalle.CodigoMoneda != 2)
                throw new ValidationException("40032", "La moneda debe ser 1 (S/.) o 2 (US$).");

            if (!string.IsNullOrWhiteSpace(detalle.DescripcionClausulasEspeciales))
                UtilsHelper.ValidarLongitudMaxima(detalle.DescripcionClausulasEspeciales, 3500, "40033", "Las cláusulas especiales no deben exceder 3500 caracteres.");

            if (!string.IsNullOrWhiteSpace(detalle.CampoAdicional2))
                UtilsHelper.ValidarLongitudMaxima(detalle.CampoAdicional2, 100, "40034", "El campo adicional 2 no debe exceder 100 caracteres.");

            if (detalle.Cliente is null)
                throw new ValidationException("40012", "La información del cliente es obligatoria.");

            ValidarCliente(detalle.Cliente);

            var tipoDocumentoCliente = detalle.Cliente.CodigoTipoDocumento;
            var numeroDocumentoCliente = detalle.Cliente.NumeroDocumento?.Trim() ?? string.Empty;
            var esClienteRuc = tipoDocumentoCliente == 2;

            if (esClienteRuc)
            {
                if (detalle.Conyuge != null)
                    throw new ValidationException("40035", "Un cliente con documento RUC no debe enviar datos de cónyuge.");

                if (detalle.RepresentantesLegales is null || detalle.RepresentantesLegales.Count == 0)
                    throw new ValidationException("40036", "Un cliente con documento RUC debe enviar al menos un representante legal.");

                if (detalle.RepresentantesLegales.Count > 3)
                    throw new ValidationException("40037", "Solo se permiten hasta 3 representantes legales del cliente.");

                ValidarRepresentantesLegalesCliente(detalle.RepresentantesLegales);
            }
            else
            {
                if (detalle.RepresentantesLegales != null && detalle.RepresentantesLegales.Count > 0)
                    throw new ValidationException("40038", "Solo los clientes con tipo de documento RUC pueden enviar representantes legales.");

                if (detalle.Conyuge != null)
                    ValidarConyuge(detalle.Conyuge);
            }

            ValidarGarantias(detalle.Garantias);
        }

        /// <summary>
        /// Valida la información del cliente.
        /// </summary>
        private static void ValidarCliente(DtoClienteSolicitud cliente)
        {
            if (!string.IsNullOrWhiteSpace(cliente.NombreCliente))
                UtilsHelper.ValidarLongitudMaxima(cliente.NombreCliente, 100, "40039", "El nombre del cliente no debe exceder 100 caracteres.");

            if (cliente.CodigoTipoDocumento != 0 &&
                cliente.CodigoTipoDocumento != 1 &&
                cliente.CodigoTipoDocumento != 2 &&
                cliente.CodigoTipoDocumento != 3 &&
                cliente.CodigoTipoDocumento != 4)
            {
                throw new ValidationException("40040", "El tipo de documento del cliente debe ser 1 (DNI), 2 (RUC), 3 (Carnet de Extranjería) o 4 (Pasaporte).");
            }

            if (cliente.CodigoTipoDocumento > 0 && string.IsNullOrWhiteSpace(cliente.NumeroDocumento))
                throw new ValidationException("40041", "Debe enviar el número de documento del cliente cuando se informa el tipo de documento.");

            if (!string.IsNullOrWhiteSpace(cliente.NumeroDocumento) && cliente.CodigoTipoDocumento <= 0)
                throw new ValidationException("40042", "Debe enviar el tipo de documento del cliente cuando se informa el número de documento.");

            if (!string.IsNullOrWhiteSpace(cliente.NumeroDocumento))
                ValidarNumeroDocumentoCliente(cliente.CodigoTipoDocumento, cliente.NumeroDocumento);

            if (!string.IsNullOrWhiteSpace(cliente.CorreoCliente))
            {
                UtilsHelper.ValidarLongitudMaxima(cliente.CorreoCliente, 255, "40043", "El correo del cliente no debe exceder 255 caracteres.");
                UtilsHelper.ValidarCorreo(cliente.CorreoCliente, "40044", "El correo del cliente no tiene un formato válido.");
            }

            if (cliente.CodigoEstadoCivil != 0 &&
                cliente.CodigoEstadoCivil != 1 &&
                cliente.CodigoEstadoCivil != 2 &&
                cliente.CodigoEstadoCivil != 3 &&
                cliente.CodigoEstadoCivil != 4)
            {
                throw new ValidationException("40046", "El estado civil del cliente debe ser 1 (Soltero), 2 (Casado), 3 (Divorciado) o 4 (Viudo).");
            }
        }

        /// <summary>
        /// Valida la información del cónyuge.
        /// </summary>
        private static void ValidarConyuge(DtoConyugeSolicitud conyuge)
        {
            if (!string.IsNullOrWhiteSpace(conyuge.NombreConyuge))
                UtilsHelper.ValidarLongitudMaxima(conyuge.NombreConyuge, 100, "40047", "El nombre del cónyuge no debe exceder 100 caracteres.");

            if (!string.IsNullOrWhiteSpace(conyuge.NumeroDocumentoConyuge))
            {
                UtilsHelper.ValidarLongitudMaxima(conyuge.NumeroDocumentoConyuge, 20, "40048", "El número de documento del cónyuge no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(conyuge.NumeroDocumentoConyuge, "40049", "El número de documento del cónyuge solo permite letras, números y guion.");
            }

            if (!string.IsNullOrWhiteSpace(conyuge.CorreoConyuge))
            {
                UtilsHelper.ValidarLongitudMaxima(conyuge.CorreoConyuge, 255, "40050", "El correo del cónyuge no debe exceder 255 caracteres.");
                UtilsHelper.ValidarCorreo(conyuge.CorreoConyuge, "40051", "El correo del cónyuge no tiene un formato válido.");
            }
        }

        /// <summary>
        /// Valida los representantes legales del cliente.
        /// </summary>
        private static void ValidarRepresentantesLegalesCliente(IList<DtoRepresentanteLegalSolicitud> representantes)
        {
            foreach (var representante in representantes)
            {
                if (string.IsNullOrWhiteSpace(representante.NombreRepresentanteLegal))
                    throw new ValidationException("40052", "El nombre del representante legal es obligatorio.");

                UtilsHelper.ValidarLongitudMaxima(representante.NombreRepresentanteLegal, 100, "40053", "El nombre del representante legal no debe exceder 100 caracteres.");

                if (string.IsNullOrWhiteSpace(representante.NumeroDocumento))
                    throw new ValidationException("40054", "El número de documento del representante legal es obligatorio.");

                UtilsHelper.ValidarLongitudMaxima(representante.NumeroDocumento, 20, "40055", "El número de documento del representante legal no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(representante.NumeroDocumento, "40056", "El número de documento del representante legal solo permite letras, números y guion.");

                if (!string.IsNullOrWhiteSpace(representante.CorreoRepresentanteLegal))
                {
                    UtilsHelper.ValidarLongitudMaxima(representante.CorreoRepresentanteLegal, 255, "40057", "El correo del representante legal no debe exceder 255 caracteres.");
                    UtilsHelper.ValidarCorreo(representante.CorreoRepresentanteLegal, "40058", "El correo del representante legal no tiene un formato válido.");
                }
            }
        }

        /// <summary>
        /// Valida la lista de garantías.
        /// </summary>
        private static void ValidarGarantias(IList<DtoGarantiaSolicitud> garantias)
        {
            if (garantias is null || garantias.Count == 0)
                return;

            if (garantias.Count > 6)
                throw new ValidationException("40059", "Solo se permiten hasta 6 garantías.");

            foreach (var garantia in garantias)
            {
                if (garantia.CodigoTipoGarantia != 1 && garantia.CodigoTipoGarantia != 2)
                    throw new ValidationException("40060", "El tipo de garantía debe ser 1 (Aval) o 2 (Fiador).");

                if (string.IsNullOrWhiteSpace(garantia.RazonSocial))
                    throw new ValidationException("40061", "La razón social o nombre de la garantía es obligatoria.");

                UtilsHelper.ValidarLongitudMaxima(garantia.RazonSocial, 100, "40062", "La razón social o nombre de la garantía no debe exceder 100 caracteres.");

                if (string.IsNullOrWhiteSpace(garantia.NumeroDocumento))
                    throw new ValidationException("40063", "El número de documento de la garantía es obligatorio.");

                UtilsHelper.ValidarLongitudMaxima(garantia.NumeroDocumento, 20, "40064", "El número de documento de la garantía no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(garantia.NumeroDocumento, "40065", "El número de documento de la garantía solo permite letras, números y guion.");

                if (garantia.CodigoEstadoCivil != 0 &&
                    garantia.CodigoEstadoCivil != 1 &&
                    garantia.CodigoEstadoCivil != 2 &&
                    garantia.CodigoEstadoCivil != 3 &&
                    garantia.CodigoEstadoCivil != 4)
                {
                    throw new ValidationException("40066", "El estado civil de la garantía debe ser 1 (Soltero), 2 (Casado), 3 (Divorciado) o 4 (Viudo).");
                }

                if (!string.IsNullOrWhiteSpace(garantia.CorreoGarantia))
                {
                    UtilsHelper.ValidarLongitudMaxima(garantia.CorreoGarantia, 255, "40068", "El correo de la garantía no debe exceder 255 caracteres.");
                    UtilsHelper.ValidarCorreo(garantia.CorreoGarantia, "40069", "El correo de la garantía no tiene un formato válido.");
                }

                var representantes = garantia.RepresentantesLegales ?? new List<DtoRepresentanteLegalGarantiaSolicitud>();

                if (representantes.Count > 3)
                    throw new ValidationException("40070", "Solo se permiten hasta 3 representantes legales por garantía.");

                if (UtilsHelper.EsRuc(garantia.NumeroDocumento) && representantes.Count == 0)
                    throw new ValidationException("40071", "Un garante con RUC debe enviar al menos un representante legal.");

                ValidarRepresentantesLegalesGarantia(representantes);
            }
        }

        /// <summary>
        /// Valida los representantes legales de la garantía.
        /// </summary>
        private static void ValidarRepresentantesLegalesGarantia(IList<DtoRepresentanteLegalGarantiaSolicitud> representantes)
        {
            foreach (var representante in representantes)
            {
                if (string.IsNullOrWhiteSpace(representante.NombreRepresentanteLegal))
                    throw new ValidationException("40072", "El nombre del representante legal de la garantía es obligatorio.");

                UtilsHelper.ValidarLongitudMaxima(representante.NombreRepresentanteLegal, 100, "40073", "El nombre del representante legal de la garantía no debe exceder 100 caracteres.");

                if (string.IsNullOrWhiteSpace(representante.NumeroDocumento))
                    throw new ValidationException("40074", "El número de documento del representante legal de la garantía es obligatorio.");

                UtilsHelper.ValidarLongitudMaxima(representante.NumeroDocumento, 20, "40075", "El número de documento del representante legal de la garantía no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(representante.NumeroDocumento, "40076", "El número de documento del representante legal de la garantía solo permite letras, números y guion.");

                if (!string.IsNullOrWhiteSpace(representante.CorreoRepresentanteLegal))
                {
                    UtilsHelper.ValidarLongitudMaxima(representante.CorreoRepresentanteLegal, 255, "40077", "El correo del representante legal de la garantía no debe exceder 255 caracteres.");
                    UtilsHelper.ValidarCorreo(representante.CorreoRepresentanteLegal, "40078", "El correo del representante legal de la garantía no tiene un formato válido.");
                }
            }
        }

        /// <summary>
        /// Valida el número de documento del cliente según el tipo.
        /// </summary>
        private static void ValidarNumeroDocumentoCliente(int tipoDocumento, string numeroDocumento)
        {
            numeroDocumento = numeroDocumento.Trim();

            switch (tipoDocumento)
            {
                case 1:
                    if (!Regex.IsMatch(numeroDocumento, @"^\d{8}$"))
                        throw new ValidationException("40079", "El número de documento del cliente para DNI debe tener exactamente 8 dígitos.");
                    break;

                case 2:
                    if (!Regex.IsMatch(numeroDocumento, @"^\d{11}$"))
                        throw new ValidationException("40080", "El número de documento del cliente para RUC debe tener exactamente 11 dígitos.");
                    break;

                case 3:
                case 4:
                    if (numeroDocumento.Length < 8 || numeroDocumento.Length > 20)
                        throw new ValidationException("40081", "El número de documento del cliente para Carnet de Extranjería o Pasaporte debe tener entre 8 y 20 caracteres.");

                    UtilsHelper.ValidarAlfanumericoConGuion(numeroDocumento, "40082", "El número de documento del cliente solo permite letras, números y guion.");
                    break;

                default:
                    throw new ValidationException("40083", "El tipo de documento del cliente no es válido.");
            }
        }

        /// <summary>
        /// Valida la solicitud de cancelación.
        /// </summary>
        /// <param name="request">Solicitud a validar.</param>
        public static void ValidarSolicitudCancelacion(this DtoSolicitudCancelarPagare request)
        {
            if (request is null)
                throw new ValidationException("40001", "La solicitud es obligatoria.");

            if (request.Pagares is null || request.Pagares.Count == 0)
                throw new ValidationException("40018", "Debe enviar al menos un pagaré para cancelar.");

            foreach (var pagare in request.Pagares)
            {
                ValidarIdentificadorPagare(pagare);

                if (pagare.FechaCancelacion == default)
                    throw new ValidationException("40019", "La fecha de cancelación es obligatoria.");
            }
        }

        /// <summary>
        /// Valida la solicitud de retiro.
        /// </summary>
        /// <param name="request">Solicitud a validar.</param>
        public static void ValidarSolicitudRetiro(this DtoSolicitudEliminarPagare request)
        {
            if (request is null)
                throw new ValidationException("40001", "La solicitud es obligatoria.");

            if (request.Pagares is null || request.Pagares.Count == 0)
                throw new ValidationException("40020", "Debe enviar al menos un pagaré para retirar.");

            foreach (var pagare in request.Pagares)
                ValidarIdentificadorPagare(pagare);
        }

        /// <summary>
        /// Valida la solicitud de reversión de cancelación.
        /// </summary>
        /// <param name="request">Solicitud a validar.</param>
        public static void ValidarSolicitudReversion(this DtoSolicitudRevertirCancelacionPagare request)
        {
            if (request is null)
                throw new ValidationException("40001", "La solicitud es obligatoria.");

            if (request.Pagares is null || request.Pagares.Count == 0)
                throw new ValidationException("40021", "Debe enviar al menos un pagaré para revertir.");

            foreach (var pagare in request.Pagares)
                ValidarIdentificadorPagare(pagare);
        }

        /// <summary>
        /// Valida los datos básicos de identificación de un pagaré.
        /// </summary>
        /// <param name="pagare">Pagaré a validar.</param>
        private static void ValidarIdentificadorPagare(DtoPagareIdentificadorSolicitud pagare)
        {
            if (pagare is null)
                throw new ValidationException("40001", "La solicitud es obligatoria.");

            if (string.IsNullOrWhiteSpace(pagare.CodigoUnico))
                throw new ValidationException("40003", "El código único es obligatorio.");

            UtilsHelper.ValidarLongitudMaxima(pagare.CodigoUnico, 20, "40022", "El código único no debe exceder 20 caracteres.");
            UtilsHelper.ValidarAlfanumericoConGuion(pagare.CodigoUnico, "40023", "El código único solo permite letras, números y guion.");

            if (!string.IsNullOrWhiteSpace(pagare.NumeroCredito))
            {
                UtilsHelper.ValidarLongitudMaxima(pagare.NumeroCredito, 20, "40024", "El número de crédito no debe exceder 20 caracteres.");
                UtilsHelper.ValidarAlfanumericoConGuion(pagare.NumeroCredito, "40025", "El número de crédito solo permite letras, números y guion.");
            }
        }
    }
}
