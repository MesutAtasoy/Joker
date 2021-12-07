FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/Aggregators/Aggregator.StoreFront.Api/Aggregator.StoreFront.Api.csproj"
COPY . .
WORKDIR "/src/src/Aggregators/Aggregator.StoreFront.Api"
RUN dotnet build "Aggregator.StoreFront.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aggregator.StoreFront.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aggregator.StoreFront.Api.dll"]
