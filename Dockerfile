FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS http://*:5000
EXPOSE 5000

# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
ARG Configuration=Release
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development
COPY *.sln ./
COPY BackEnd_ERP/API.csproj BackEnd_ERP/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/
RUN dotnet restore
COPY . .
WORKDIR /app/BackEnd_ERP
RUN echo $($Configuration)
RUN dotnet build -c $Configuration -o /app

FROM build-env AS publish
ARG Configuration=Release
RUN dotnet publish -c $Configuration -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "API.dll"]