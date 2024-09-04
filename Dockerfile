# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK as a build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["pic2plateApi.csproj", "./"]
RUN dotnet restore "./pic2plateApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "pic2plateApi.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "pic2plateApi.csproj" -c Release -o /app/publish

# Build the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pic2plateApi.dll"]
