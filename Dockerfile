# Use the official .NET runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /pic2plateApi

# Copy the csproj file and restore the project dependencies
COPY pic2plateApi.csproj ./
RUN dotnet restore "./pic2plateApi.csproj"

# Copy the rest of the project files and build the application
COPY . .
RUN dotnet build "./pic2plateApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "./pic2plateApi.csproj" -c Release -o /app/publish

# Create the final image to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pic2plateApi.dll"]
