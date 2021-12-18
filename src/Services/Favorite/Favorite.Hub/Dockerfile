FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Favorite/Favorite.Hub/Favorite.Hub.csproj", "Favorite.Hub/"]
RUN dotnet restore "src/Services/Favorite/Favorite.Hub/Favorite.Hub.csproj"
COPY . .
WORKDIR "/src/Favorite.Hub"
RUN dotnet build "Favorite.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Favorite.Hub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Favorite.Hub.dll"]
