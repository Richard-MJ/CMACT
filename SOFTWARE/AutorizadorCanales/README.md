# AutorizadorCanales

# Especificaciones

| Característica           | Descripción          |
| ------------------------ | -------------------- |
| Plataforma de desarrollo | `Visual Studio 2022` |
| Framework                | `.NET Core 6.0`      |
| Lenguaje                 | C#                   |
| Proyectos de la solución | `AutorizadorCanales.Api`         |

Para utilizar los comandos `dotnet` se requiere de la instalación del entorno de ejecución de [.NET Core](https://dotnet.microsoft.com/es-es/download/dotnet/6.0).

# Configuración AutorizadorCanales
sss
## Variables de entorno

| Característica               | Ejemplo                                  | Descripción                                                       |
| ---------------------------- | ---------------------------------------- | ----------------------------------------------------------------- |
| ServidorBD                   | SRV-DBDEV-01                             | Dirección IP del servidor donde se encuentra la base de datos     |
| CatalogoBD                   | SAF2000_TACNA_DEV                        | Nombre de la base de datos                                        |
| UsuarioBD	                   | usuario_sql_01                           | Nombre de Usuario sql                                             |
| PasswordBD                   | mc57eE1                                  | Contraseña de Usuario sql                                         |
| SeguridadIntegrada           | true                                     | Indicador de seguridad integrada                                  |
| PinOperacionesApi            | https://SRV-APPDES06:8295/api            | Url del api pin operaciones gateway IIS                           |
| ServidorColasIp              | 192.168.112.14                           | Direccion IP del servidor de colas RabbitMQ                       |
| ServidorColasUsername        | usuario                                  | Nombre de usuario credencial para RabbitMQ                        |
| ServidorColasPassword        | ficticio                                 | Contraseña de usuario credencial para RabbitMQ                    |
| ServidorApiSeguridad         | SRV-APPDES02                             | Nombre del servidor donde está ubicado el api seguridad           |
| PuertoApiSeguridad           | 9000                                     | Puerto donde se despliega el servicio Api.Seguridad               |
| BitacoraServidor             | http://192.168.70.210:9090/api/bitacoras | Url del servicio de bitácoras                                     |
| BitacoraFileActiva           | true                                     | Indicador para activar escritura de bitacora por archivo          |
| BitacoraWsActiva             | true                                     | Indicador para activar escritura de bitacora por web service      |

## Despliegue

### Configuración en Pool

| Característica        | Valor                     |
| --------------------- | ------------------------- |
| Name                  | `AutorizadorCanales`    |
| .NET Version          | `No Managed Code`         |
| Managed Pipeline Mode | `Integrated`              |
| Identity              | `ApplicationPoolIdentity` |
       