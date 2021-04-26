FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
ARG Configuration=Release
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln ./
COPY BackEnd_ERP/API.csproj BackEnd_ERP/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
RUN dotnet restore BackEnd_ERP/API.csproj

# copy and build app and libraries
COPY BackEnd_ERP/ BackEnd_ERP/
COPY Application/ Application/
COPY Domain/ Domain/
COPY Infrastructure/ Infrastructure/
WORKDIR /app/BackEnd_ERP
RUN dotnet build -c $Configuration --no-restore -o /app

FROM build-env AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.dll"]