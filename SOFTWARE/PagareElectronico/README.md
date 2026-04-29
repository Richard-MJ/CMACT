# PagareElectronico.Api – API REST en .NET 8 para integración con CAVALI

`PagareElectronico.Api` es una API REST desarrollada con **.NET 8**, cuya finalidad es **recibir, validar, registrar y transformar** la información relacionada al proceso de **Pagaré Electrónico**, para integrarla con los servicios externos de **CAVALI**.

La API actúa como una **pasarela de integración**, desacoplando los sistemas internos de la institución del contrato técnico exigido por el proveedor externo. De esta manera, centraliza la validación, trazabilidad, manejo de errores, persistencia del proceso y consumo de servicios.

---

## 🚀 Características principales

- Desarrollado con **.NET 8**
- Arquitectura orientada a **API REST**
- Integración con servicios externos de **CAVALI**
- Seguridad adicional mediante **ApiKey**
- Manejo centralizado de errores
- Preparado para despliegue en **IIS** sobre Windows Server

---

## 🎯 Propósito del proyecto

`PagareElectronico.Api` permite:

- Recibir solicitudes del sistema consumidor
- Validar la información de entrada
- Mapear la estructura interna al formato requerido por **CAVALI**
- Obtener el token de autenticación del proveedor
- Consumir los endpoints externos de Pagaré Electrónico
- Actualizar el estado del proceso
- Retornar una respuesta controlada y trazable al sistema consumidor

En términos funcionales, esta API constituye una **capa intermedia de integración** entre los sistemas internos de créditos/pagarés y la plataforma externa de **CAVALI**.

---

## ⚙️ Requisitos del entorno

Para la ejecución y despliegue del proyecto se requiere:

- **.NET 8 SDK / Runtime**
- **IIS** habilitado con soporte para ASP.NET Core
- Acceso de red hacia los servicios de autenticación y negocio de **CAVALI**
- Variables de entorno correctamente configuradas
- Permisos de escritura sobre carpetas de logs, si aplica

---

## 🔧 Ejecución en local (modo desarrollo)

1. Abrir la solución en **Visual Studio 2022** o superior.
2. Verificar que el proyecto esté configurado con **.NET 8**.
3. Configurar las variables necesarias en:
   - `appsettings.json`
   - `appsettings.Development.json`
   - o variables de entorno del sistema
4. Validar conectividad hacia:
   - `AuthUrl` de CAVALI
   - `BaseUrl` de CAVALI
5. Ejecutar el proyecto con el perfil `IIS Express` o `Kestrel`.

---

## 🔨 Compilación y despliegue en IIS

1. Publicar el proyecto con el siguiente comando:

   ```bash
   dotnet publish -c Release -o ./publicacion
   ```

2. Copiar los archivos publicados al servidor donde se encuentra el sitio en IIS.
3. Crear o configurar el sitio/aplicación en IIS.
4. Asignar un **Application Pool** compatible con ASP.NET Core.
5. Configurar las variables de entorno del ambiente correspondiente.
6. Verificar:
   - conectividad hacia los servicios de **CAVALI**
   - permisos del usuario del Application Pool
   - acceso a rutas de logging, si aplica

---

## 🧱 Arquitectura general

El proyecto está planteado bajo una separación de responsabilidades por capas:

- **API**: expone los endpoints HTTP y gestiona la entrada/salida de solicitudes
- **Aplicación**: orquesta los casos de uso y el flujo del proceso
- **Infraestructura**: implementa persistencia, clientes HTTP, logging e integraciones

### Flujo general de operación

1. El sistema consumidor envía una solicitud a `PagareElectronico.Api`.
2. La API valida la información recibida.
3. Se obtiene un token de autenticación desde **CAVALI**.
4. La información interna se transforma al contrato esperado por el proveedor.
5. Se consume el servicio externo correspondiente.
6. Se actualiza el estado del proceso.
7. Se devuelve una respuesta controlada al sistema consumidor.

---

## 🗄️ Tablas principales

La API realiza el seguimiento del proceso mediante las siguientes tablas del esquema `PR`:

```sql
SELECT * FROM [PR].[PR_ESTADOS_PAGARE_ELECTRONICO]
GO

SELECT * FROM [PR].[PR_CREDITOS_PAGARE_ELECTRONICO]
GO
```
---

## 🔐 Variables de entorno

Para el correcto funcionamiento de la integración con **CAVALI**, se deben configurar las siguientes variables:

| Variable | Valor | Descripción |
|---|---|---|
| `AuthUrl` | `https://api.qae.cavali.com.pe/auth/token` | URL utilizada para obtener el token de acceso mediante autenticación OAuth 2.0. |
| `BaseUrl` | `https://api.pagares.qae.cvl-release.com/` | URL base de los servicios de Pagaré Electrónico expuestos por CAVALI. |
| `ParticipantCode` | `447` | Código del participante asignado para identificar a la institución dentro de la integración con CAVALI. |
| `BankCode` | `4` | Código de la entidad bancaria utilizado dentro de las operaciones enviadas al proveedor. |
| `ProductCode` | `41` | Código identificador del producto asociado al proceso de Pagaré Electrónico. |
| `ClientId` | `l648ps0ra189uj509fj7bd9r1` | Identificador único del cliente utilizado para solicitar el token de autenticación. |
| `ClientSecret` | `48uiqm48foq1sdrj171u1r2oavfi98lh7g229s5njgbs2s00a81` | Secreto asociado al cliente para el proceso de autenticación. Debe tratarse como dato sensible. |
| `ApiKey` | `zgKgImDdor8wEYxaoGRa72DuazkbcE9j4TsM4R8Y` | Llave de acceso requerida por la API de CAVALI para consumir sus endpoints protegidos. Debe tratarse como dato sensible. |
| `TimeoutSeconds` | `30` | Tiempo máximo de espera, en segundos, para las peticiones HTTP realizadas hacia los servicios externos. |

### Ejemplo referencial de configuración

```json
{
  "Cavali": {
    "AuthUrl": "https://api.qae.cavali.com.pe/auth/token",
    "BaseUrl": "https://api.pagares.qae.cvl-release.com/",
    "ParticipantCode": "447",
    "BankCode": "4",
    "ProductCode": "41",
    "ClientId": "l648ps0ra189uj509fj7bd9r1",
    "ClientSecret": "48uiqm48foq1sdrj171u1r2oavfi98lh7g229s5njgbs2s00a81",
    "ApiKey": "zgKgImDdor8wEYxaoGRa72DuazkbcE9j4TsM4R8Y",
    "TimeoutSeconds": 30
  }
}
```

---

## 🔑 Seguridad

La integración considera los siguientes mecanismos de seguridad:

- Uso de `ClientId` y `ClientSecret`
- Envío de `ApiKey` en las solicitudes hacia el proveedor
- Uso recomendado de **HTTPS** en todos los ambientes
- Resguardo adecuado de secretos de configuración

> **Importante:** `ClientSecret` y `ApiKey` son datos sensibles y deben protegerse adecuadamente en los ambientes correspondientes.

---

## 📦 Documentación complementaria

La documentación funcional y técnica relacionada al proyecto se encuentra en la siguiente ruta:

```text
\\srvnashp01\desarrollo_documentos$\CAVALI Pagaré Electonico
```

En esta ubicación se pueden mantener documentos como:

- contratos de integración
- colecciones Postman
- flujos funcionales
- evidencias de pruebas
- documentación técnica y operativa

---
