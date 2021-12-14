FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Notification/Notification.Hub/Notification.Hub.csproj"
COPY . .
WORKDIR "/src/src/Services/Notification/Notification.Hub"
RUN dotnet build "Notification.Hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Hub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Hub.dll"]
