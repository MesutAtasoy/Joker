FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/WebApplications/Joker.WebApp/Joker.WebApp.csproj"
COPY . .
WORKDIR "/src/src/WebApplications/Joker.WebApp"
RUN dotnet build "Joker.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Joker.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Joker.WebApp.dll"]
