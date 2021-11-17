FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Identity/Joker.Identity/Joker.Identity.csproj"
COPY . .
WORKDIR "/src/src/Services/Identity/Joker.Identity"
RUN dotnet build "Joker.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Joker.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Joker.Identity.dll"]