FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copia os csproj
COPY ["Devices.Api/Devices.Api.csproj", "Devices.Api/"]
COPY ["Devices.Application/Devices.Application.csproj", "Devices.Application/"]
COPY ["Devices.Domain/Devices.Domain.csproj", "Devices.Domain/"]
COPY ["Devices.Infrastructure/Devices.Infrastructure.csproj", "Devices.Infrastructure/"]

# restore
RUN dotnet restore "Devices.Api/Devices.Api.csproj"

# copia tudo
COPY . .
WORKDIR "/src/Devices.Api"

# publish
RUN dotnet publish "Devices.Api.csproj" -c Release -o /app/publish

# imagem final (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# cria a pasta onde ficará o SQLite
RUN mkdir -p /app/data

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:7217
ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 7217

ENTRYPOINT ["dotnet", "Devices.Api.dll"]
