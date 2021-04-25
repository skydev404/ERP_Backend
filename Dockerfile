# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./BackEnd_ERP.sln ./NuGet.config  ./

# Copy csproj and restore as distinct layers
COPY *.csproj ./BackEnd_ERP
COPY *.csproj ./Application
COPY *.csproj ./Domain
COPY *.csproj ./Infrastructure
RUN dotnet restore

# Copy everything else and build
COPY ../engine/examples ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]