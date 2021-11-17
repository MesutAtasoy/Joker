FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Favorite/Favorite.Api/Favorite.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Favorite/Favorite.Api"
RUN dotnet build "Favorite.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Favorite.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Favorite.Api.dll"]
