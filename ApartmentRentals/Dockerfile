# Stage 1: Runtime base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 2: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Копируем файлы решений и проектов для восстановления зависимостей
COPY ["ApartmentRentals.sln", "./"]
COPY ["ApartmentRentals.WebAPI/ApartmentRentals.WebAPI.csproj", "ApartmentRentals.WebAPI/"]
COPY ["ApartmentRentals.Data/ApartmentRentals.Data.csproj", "ApartmentRentals.Data/"]
# Добавляем проект тестов, чтобы sln-файл не ругался
COPY ["ApartmentRentals.Tests/ApartmentRentals.Tests.csproj", "ApartmentRentals.Tests/"]

# 2. Восстанавливаем все пакеты (включая prometheus-net)
# Важно: если вы добавили пакет в csproj, эта команда его скачает
RUN dotnet restore "ApartmentRentals.sln"

# 3. Копируем весь исходный код
COPY . .

# 4. Сборка и публикация
WORKDIR "/src/ApartmentRentals.WebAPI"
RUN dotnet publish "ApartmentRentals.WebAPI.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false

# Stage 3: Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Настройки среды
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "ApartmentRentals.WebAPI.dll"]