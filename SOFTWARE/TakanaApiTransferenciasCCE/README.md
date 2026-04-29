# ApiTransferenciasCCE – API REST en .NET 8
`ApiTransferenciasCCE` es una API REST desarrollada con **.NET 8**, diseñada para gestionar operaciones de 
**transferencias interbancarias inmediatas**. Esta API se ejecuta sobre **IIS (Internet Information Services)** 
y forma parte del ecosistema de soluciones tecnológicas de CMAC Tacna.

---

## 🚀 Características principales

* Desarrollado con .NET 8 (LTS)
* Arquitectura orientada a API REST
* Despliegue en entorno IIS sobre Windows Server
* Bitacorización completa de eventos y transacciones
* Seguridad mediante certificados TLS

---

## ⚙️ Requisitos del entorno

* **.NET 8 Runtime & Hosting Bundle** instalado en el servidor
* **IIS** habilitado con soporte para aplicaciones ASP.NET Core
* Acceso a certificados TLS y de firma digital proporcionados por CMAC
* Acceso de red a los servicios de la CCE desde el servidor donde se aloja esta API
* 
---

## 🔧 Ejecución en Local (Modo Desarrollo)

1. Abrir el proyecto en **Visual Studio** con .NET 8 instalado.
2. Ejecutar con el perfil `IIS Express` o `Kestrel` definido en `launchSettings.json`.
3. Las variables de entorno están configuradas en `launchSettings.json`.
4. Es **obligatorio tener instalado** el certificado de firma digital de la CCE (`signing.agregador.cce.com.pe`) para compilar correctamente.
5. El proyecto puede usarse con `F5` directamente desde Visual Studio, sin necesidad de publicar ni configurar IIS.

---

## 🔨 Compilación y despliegue en IIS

1. Publicar el proyecto con:

   ```bash
   dotnet publish -c Release -o ./publicacion
   ```
2. Copiar los archivos publicados al directorio del sitio web en IIS.
3. Configurar el `appsettings.config` con las variables de entorno, el Application Pool (con usuario autorizado) y los bindings HTTPS.
4. Instalar el certificado TLS correspondiente (`cmactacna.com.pe_2024.pfx`) y registrar el hash en TFS.
5. Instalar el certificado de firma digital de la CCE (`signing.agregador.cce.com.pe`), es necesario para la compilación.
6. Verificar los permisos del usuario del Application Pool:
   * Acceso a los certificados
   * Acceso a la carpeta de logs

---

## 📁 Estructura del proyecto

* `/Controllers`: Endpoints expuestos por la API
* `/Services`: Lógica de negocio
* `/Models`: Modelos de datos
* `/Configurations`: Archivos de configuración y valores por entorno
* `/Logs`: Carpeta de almacenamiento de logs de operación

---

## 📝 Bitacorización

El sistema implementa un mecanismo de bitacorización que registra:

* Solicitudes entrantes
* Excepciones controladas
* Estados de procesamiento de transferencias

Los registros pueden consultarse en la tabla:

```
[CC].[CC_BITACORAS_TRANSFERENCIAS_INMEDIATAS]
[CC].[CC_TIN_INMEDIATA_TRANSACCION_ORDEN_TRANSFERENCIA]
```

o directamente desde la interfaz del módulo **Tesorería Web**.

---

## 📦 Documentación complementaria y recursos

* Documentación general del proyecto:

  * `\\srvnashp01\desarrollo_documentos$\Documentos\Transferencias_Inmediatas`
* Manual de Certificados:

  * `\\srvnashp01\desarrollo_documentos$\Documentos\Transferencias_Inmediatas\CERTIFICADOS`
* Manual del PinVerify:

  * `\\srvnashp01\desarrollo_documentos$\Documentos\Transferencias_Inmediatas\PINVERIFY`
* Pruebas Postman:

  * `\\srvnashp01\desarrollo_documentos$\Documentos\Transferencias_Inmediatas\POSTMAN`

---

## 📋 Instrucciones resumidas de despliegue

1. Desplegar los servicios de Api `ApiTransferenciasCCE`, `ApiGateWayTransferenciasCCE` y `PinOperacionesApi`.
2. Instalar y configurar certificados TLS y de firma digital.
3. Reemplazar el hash del certificado TLS en las variables de despliegue del TFS.
4. Verificar permisos del usuario de Application Pool.
5. Ejecutar `reiniciar_pool.bat` para reiniciar correctamente los sitios.
6. Verificar red hacia servicios de la CCE.
7. Validar con `PinVerifyChecker.exe` que `PinVerify` funcione correctamente.

---

## 🧪 Requisitos de modulos adicionales para ejecución de pruebas

Para ejecutar correctamente las pruebas, los usuarios deben tener habilitados los siguientes servicios:

* Tesorería Web
* Operaciones Web
* Takana Cliente
* SAF2000
* APP Móvil Canales
* Servidor de Reportes – CMAC Tacna S.A.

---

## 📑 Reportes relacionados a Transferencias Inmediatas

* **ATE-CC-010**
* **ATE-CC-011**
* **ATE-CC-012**
* **ACA-CC-006**
* **ACA-CL-04**