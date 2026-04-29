using Swashbuckle.AspNetCore.Annotations;

namespace Takana.Transferencias.CCE.Api.Common.DTOs.Reporte
{
    public class GenerarReporteDTO
    {
        #region Constantes
        /// <summary>
        /// Constante de frecuencia diara del reporte
        /// </summary>
        public const string FrecuenciaDiaria = "D";
        /// <summary>
        /// Constante de frecuencia mensual del reporte
        /// </summary>
        public const string FrecuenciaMensual = "M";
        /// <summary>
        /// Abreviatura de Caja Tacna para la CCE
        /// </summary>
        public const string AbreviaturaCajaTacna = "CTAC";
        /// <summary>
        /// Es un valor por defecto cuando es reporte mensual
        /// </summary>
        public const string ValorDiaPorFrencuenciaMensual = "00";
        /// <summary>
        /// Descripcion de Rol de entidad Beneficiaria
        /// </summary>
        public const string DescripcionRolEntidadBeneficiaria = "ENTIDAD BENEFICIARIA";
        /// <summary>
        /// Descripción del Rol de Entidad Ordenante
        /// </summary>
        public const string DescripcionRolEntidadOrdenante = "ENTIDAD ORDENANTE";
        /// <summary>
        /// Descripción de No Disponibilidad
        /// </summary>
        public const string DescripcionNoDisponibilidad = "NO DISPONIBILIDAD";
        /// <summary>
        /// Descripción de Busqueda Alias
        /// </summary>
        public const string DescripcionBusquedaAlias = "BUSQUEDA ALIAS";
        /// <summary>
        /// Descripción de Efectividad
        /// </summary>
        public const string DescripcionEfectividad = "EFECTIVIDAD";
        /// <summary>
        /// Descripción de Rendimiento
        /// </summary>
        public const string DescripcionRendimiento = "RENDIMIENTO";
        /// <summary>
        /// Descripción de Percentil
        /// </summary>
        public const string DescripcionPercentil = "PERCENTIL";
        /// <summary>
        /// Descripción de Percentil
        /// </summary>
        public const string DescripcionTiempoMaximo = "TIEMPO MAXIMO";
        /// <summary>
        /// Descripción de Transferencia
        /// </summary>
        public const string DescripcionTransferencia = "TRANSFERENCIA";
        /// <summary>
        /// Descripción de la Tarea programa diaria de directorio
        /// </summary>
        public const string TareaProgramadaDiariaDirectorio = "tarea_programada_diaria_directorio";
        /// <summary>
        /// Descripción de la Tarea programa diaria de verificacion de la respuesta del directorio
        /// </summary>
        public const string TareaProgramadaDiariaVerificarDirectorio = "tarea_programada_diaria_verificar_respuesta_directorio";
        /// <summary>
        /// Descripción de la Tarea programa diaria de Reportes
        /// </summary>
        public const string TareaProgramadaDiariaReportes = "tarea_programada_diaria_reportes";
        /// <summary>
        /// Descripción de la Tarea programa mensuales de Reportes
        /// </summary>
        public const string TareaProgramadaMensualReportes = "tarea_programada_mensual_reportes";
        /// <summary>
        /// Constante Enum de tipo de reporte
        /// </summary>
        public enum TipoReporte
        {
            MantenimientoMensual = 1,
            IncidenteMensual = 2,
            ObjetivoTiempoRecuperacionMensual = 3,
            ICDNoDisponibilidadDiario = 4,
            ICDNoDisponibilidadMensual = 5,
            ICDEfectividadBusquedaAliasDiario = 6,
            ICDEfectividadBusquedaAliasMensual = 7,
            ICDEfectividadTransferenciasDiario = 8,
            ICDEfectividadTransferenciasMensual = 9,
            ICDRendimientoBusquedaAliasDiario = 10,
            ICDRendimientoBusquedaAliasMensual = 11,
            ICDRendimientoTransferenciasDiario = 12,
            ICDRendimientoTransferenciasMensual = 13,
            ResumenDeConsultasDiario = 14,
            ResumenDeTransaccionesDiario = 15,
            VariacionUsuarioMontoTransferenciasMensual = 16,
            DirectorioInteroperabilidadDiario = 17,
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Anio de la generación del reporte
        /// </summary>
        [SwaggerSchema("Anio de la generación del reporte")]
        public string Anio { get; set; }
        /// <summary>
        /// Mes que se generará el reporte
        /// </summary>
        [SwaggerSchema("Mes que se generará el reporte")]
        public string Mes { get; set; }
        /// <summary>
        /// Dia que se generará el reporte
        /// </summary>
        [SwaggerSchema("Dia que se generará el reporte")]
        public string Dia { get; set; }
        /// <summary>
        /// usuario que generará el reporte
        /// </summary>
        [SwaggerSchema("usuario que generará el reporte")]
        public string Usuario { get; set; }
        /// <summary>
        /// Id de tipo de reporte a Generar
        /// </summary>
        [SwaggerSchema("Id de tipo de reporte a Generar")]
        public int IdTipoReporte { get; set; }
        /// <summary>
        /// Formato de anio mas fecha
        /// </summary>
        public DateTime FechaReporte => new DateTime(int.Parse(Anio), int.Parse(Mes), int.Parse(Dia)).Date + DateTime.Now.TimeOfDay;
        /// <summary>
        /// Formato de anio mas fecha
        /// </summary>
        public string FormatoFecha => Mes + Anio;
        /// <summary>
        /// Formato de anio mas fecha con dia
        /// </summary>
        public string FormatoFechaConDia => Dia + Mes + Anio;
        /// <summary>
        /// Formato de la fecha del archivo carga masiva
        /// </summary>
        public string FormatoFechaCargaMasiva => Anio + Mes + Dia + DateTime.Now.ToString("HHmm");
        /// <summary>
        /// Nombre del archivo de Mantenimiento Mensual
        /// </summary>
        public string NombreArchivoMantenimientoMensual => $"{AbreviaturaCajaTacna}_MANT_{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de Incidente Mensual
        /// </summary>
        public string NombreArchivoIncidenteMensual => $"{AbreviaturaCajaTacna}_INCI_{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de Objetivo de tiempo de recuperación  Mensual
        /// </summary>
        public string NombreArchivoRTOMensual => $"{AbreviaturaCajaTacna}_RTO_{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de ICD no disponibilidad diario.
        /// </summary>
        public string NombreArchivoICDNoDisponibilidadDiario => $"{AbreviaturaCajaTacna}_NODISP_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de ICD no disponibilidad mensual.
        /// </summary>
        public string NombreArchivoICDNoDisponibilidadMensual => $"{AbreviaturaCajaTacna}_NODISP_{ValorDiaPorFrencuenciaMensual}{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de efectividad de busqueda de alias diario.
        /// </summary>
        public string NombreArchivoICDEfectividadBusquedaAliasDiario => $"{AbreviaturaCajaTacna}_EFECT_ALIAS_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de efectividad de busqueda de alias mensual.
        /// </summary>
        public string NombreArchivoICDEfectividadBusquedaAliasMensual => $"{AbreviaturaCajaTacna}_EFECT_ALIAS_{ValorDiaPorFrencuenciaMensual}{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de efectividad de transferencias diario.
        /// </summary>
        public string NombreArchivoICDEfectividadTransferenciasDiario => $"{AbreviaturaCajaTacna}_EFECT_TRANSF_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de efectividad de transferencias mensual.
        /// </summary>
        public string NombreArchivoICDEfectividadTransferenciasMensual => $"{AbreviaturaCajaTacna}_EFECT_TRANSF_{ValorDiaPorFrencuenciaMensual}{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de rendimiento de busqueda de alias diario.
        /// </summary>
        public string NombreArchivoICDRendimientoBusquedaAliasDiario => $"{AbreviaturaCajaTacna}_REND_ALIAS_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de rendimiento de busqueda de alias mensual.
        /// </summary>
        public string NombreArchivoICDRendimientoBusquedaAliasMensual => $"{AbreviaturaCajaTacna}_REND_ALIAS_{ValorDiaPorFrencuenciaMensual}{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de rendimiento de transferencias diario.
        /// </summary>
        public string NombreArchivoICDRendimientoTransferenciasDiario => $"{AbreviaturaCajaTacna}_REND_TRANSF_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de ICD de rendimiento de transferencias mensual.
        /// </summary>
        public string NombreArchivoICDRendimientoTransferenciasMensual => $"{AbreviaturaCajaTacna}_REND_TRANSF_{ValorDiaPorFrencuenciaMensual}{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de Resumen de Consulta Diario
        /// </summary>
        public string NombreArchivoResumenConsultaDiario => $"{AbreviaturaCajaTacna}_CONSULTA_BUSQUEDA_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de Resumen de Transaccion Diario
        /// </summary>
        public string NombreArchivoResumenTransaccionDiario => $"{AbreviaturaCajaTacna}_TRANSF_REPORTE_RES_{FormatoFechaConDia}.csv";
        /// <summary>
        /// Nombre del archivo de Variación de usuarios y montos de transferencias Mensual
        /// </summary>
        public string NombreArchivoCNTUsuarioMensual => $"{AbreviaturaCajaTacna}_CNT_USUARIO_REPORTE_{FormatoFecha}.csv";
        /// <summary>
        /// Nombre del archivo de Variación de usuarios y montos de transferencias Mensual
        /// </summary>
        public string NombreArchivoCargaMasivaDirectorio => $"DIRECTORIO{FormatoFechaCargaMasiva}.csv";
        #endregion
    }
}