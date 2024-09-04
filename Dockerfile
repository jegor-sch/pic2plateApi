FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

# Add a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY pic2plateApi/pic2plateApi.csproj pic2plateApi/
RUN dotnet restore pic2plateApi/pic2plateApi.csproj
COPY . .
WORKDIR /src/pic2plateApi
RUN dotnet build pic2plateApi.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish pic2plateApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/ .

ENTRYPOINT ["dotnet", "pic2plateApi.dll"]
