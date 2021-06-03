FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ClientsService/ClientsService.Web ClientsService.Web
RUN dotnet restore ClientsService.Web/ClientsService.Web.csproj

COPY ClientsService/ClientsService.Web ClientsService.Web
COPY ClientsService/ClientsService.BLL ClientsService.BLL
COPY ClientsService/ClientsService.DDL ClientsService.DDL

RUN dotnet build ClientsService.Web/ClientsService.Web.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ClientsService.Web/ClientsService.Web.csproj -c Release -o /app/publish

FROM base As final
WORKDIR /app
COPY --from=publish app/publish .

EXPOSE 80
ENTRYPOINT [ "dotnet", "ClientsService.Web.dll" ]
