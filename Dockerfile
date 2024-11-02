# Вибір базового образу для збірки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Копіюємо csproj і відновлюємо залежності
COPY ["WebAndroidServer/WebSimba.csproj", "WebAndroidServer/"]
RUN dotnet restore "WebAndroidServer/WebSimba.csproj"

# Копіюємо інші файли і будуємо програму
COPY . .
WORKDIR /source/WebAndroidServer
RUN dotnet publish -c Release -o /app

# Етап створення кінцевого образу
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

# Запускаємо програму
ENTRYPOINT ["dotnet", "WebAndroidServer.dll"]
