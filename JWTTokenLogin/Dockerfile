FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["JWTTokenLogin/JWTTokenLogin.csproj", "JWTTokenLogin/"]
RUN dotnet restore "JWTTokenLogin/JWTTokenLogin.csproj"
COPY . .
WORKDIR "/src/JWTTokenLogin"
RUN dotnet build "JWTTokenLogin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "JWTTokenLogin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "JWTTokenLogin.dll"]