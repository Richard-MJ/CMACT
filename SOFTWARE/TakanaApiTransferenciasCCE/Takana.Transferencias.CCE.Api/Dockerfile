#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-amd64 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 443

# Se agrega TLSv1 y se reduce el nivel de seguridad de Dbian para permitir la conenxión con la BD SQL Server
#RUN sed -i "s/MinProtocol = TLSv1.2/MinProtocol = TLSv1/" "/etc/ssl/openssl.cnf"
#RUN sed -i "s/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g" "/etc/ssl/openssl.cnf"
# 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Takana.Transferencias.CCE.Api/Takana.Transferencias.CCE.Api.csproj", "Takana.Transferencias.CCE.Api/"]
COPY ["Takana.Transferencia.CCE.Api.Loggin.Nlog/Takana.Transferencias.CCE.Api.Loggin.Nlog.csproj", "Takana.Transferencia.CCE.Api.Loggin.Nlog/"]
COPY ["Takana.Transferencias.CCE.Api.Loggin.Interfaz/Takana.Transferencias.CCE.Api.Loggin.Interfaz.csproj", "Takana.Transferencias.CCE.Api.Loggin.Interfaz/"]
COPY ["Takana.Transferencias.CCE.Api.Common.Interfaces/Takana.Transferencias.CCE.Api.Common.Interfaces.csproj", "Takana.Transferencias.CCE.Api.Common.Interfaces/"]
COPY ["Takana.Transferencias.CCE.Api.Common/Takana.Transferencias.CCE.Api.Common.csproj", "Takana.Transferencias.CCE.Api.Common/"]
COPY ["Takana.Transferencias.CCE.Api.Dominio.Entidades/Takana.Transferencias.CCE.Api.Dominio.Entidades.csproj", "Takana.Transferencias.CCE.Api.Dominio.Entidades/"]
COPY ["Takana.Transferencias.CCE.Api.Infraestructura.Contenedor/Takana.Transferencias.CCE.Api.Infraestructura.Contenedor.csproj", "Takana.Transferencias.CCE.Api.Infraestructura.Contenedor/"]
COPY ["Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion/Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion.csproj", "Takana.Transferencias.CCE.Api.Aplicacion.Servicios.Implementacion/"]
COPY ["Takana.Transferencias.CCE.Api.Dominio.Servicios/Takana.Transferencias.CCE.Api.Dominio.Servicios.csproj", "Takana.Transferencias.CCE.Api.Dominio.Servicios/"]
COPY ["Takana.Transferencias.CCE.Api.Datos/Takana.Transferencias.CCE.Api.Datos.csproj", "Takana.Transferencias.CCE.Api.Datos/"]
RUN dotnet restore "./Takana.Transferencias.CCE.Api/Takana.Transferencias.CCE.Api.csproj"
COPY . .
WORKDIR "/src/Takana.Transferencias.CCE.Api"
RUN dotnet build "./Takana.Transferencias.CCE.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Takana.Transferencias.CCE.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV TZ America/Lima
ENTRYPOINT ["dotnet", "Takana.Transferencias.CCE.Api.dll"]