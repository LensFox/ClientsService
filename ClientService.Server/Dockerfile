FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS base
WORKDIR /app

COPY ClientsService/ClientsService.Web ClientsService.Web
RUN dotnet restore ClientsService.Web/ClientsService.Web.csproj

COPY ClientsService/ClientsService.Web ClientsService.Web
COPY ClientsService/ClientsService.BLL ClientsService.BLL
COPY ClientsService/ClientsService.DDL ClientsService.DDL

RUN dotnet publish ClientsService.Web/ClientsService.Web.csproj -c Release -o out

FROM base As final
COPY --from=base app/out out

ENTRYPOINT [ "dotnet", "out/ClientsService.Web.dll" ]
