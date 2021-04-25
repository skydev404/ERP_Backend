# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ./BackEnd_ERP.sln ./

# Copy csproj and restore as distinct layers
COPY BackEnd_ERP/*.csproj ./
COPY Application/*.csproj ./
COPY Domain/*.csproj ./
COPY Infrastructure/*.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY ../engine/examples ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "API.dll"]