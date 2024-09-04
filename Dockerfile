FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore --configfile NuGet.config

ARG BUILD_VERSION="1"
RUN dotnet publish pic2plateApi/pic2plateApi.csproj -c Release -o /app/publish -p:Version=$BUILD_VERSION

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish/ .

ENTRYPOINT ["dotnet", "pic2plateApi.dll"]
