# Etapa 1: Compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Prueba.Api/Prueba.Api.csproj"
RUN dotnet publish "Prueba.Api/Prueba.Api.csproj" -c Release -o /app/publish

# Etapa 2: Ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Prueba.Api.dll"]
