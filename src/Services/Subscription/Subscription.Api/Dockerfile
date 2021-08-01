FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Services/Subscription/Subscription.Api/Subscription.Api.csproj"
COPY . .
WORKDIR "/src/Subscription.Api"
RUN dotnet build "Subscription.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Subscription.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Subscription.Api.dll"]
