# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CarShop.API/CarShop.API.csproj", "CarShop.API/"]
RUN dotnet restore "CarShop.API/CarShop.API.csproj"
COPY . .
WORKDIR "/src/CarShop.API"
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarShop.API.dll"]
