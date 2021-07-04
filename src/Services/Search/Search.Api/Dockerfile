#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Search/Search.Api/Search.Api.csproj", "src/Services/Search/Search.Api/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mvc/Joker.Mvc.csproj", "src/Submodules/Joker.Packages/Joker.Mvc/"]
COPY ["src/Submodules/Joker.Packages/Joker.Exceptions/Joker.Exceptions.csproj", "src/Submodules/Joker.Packages/Joker.Exceptions/"]
COPY ["src/Submodules/Joker.Packages/Joker.Response/Joker.Response.csproj", "src/Submodules/Joker.Packages/Joker.Response/"]
COPY ["src/Services/Search/Search.Core/Search.Core.csproj", "src/Services/Search/Search.Core/"]
COPY ["src/Submodules/Joker.Packages/Joker.ElasticSearch/Joker.ElasticSearch.csproj", "src/Submodules/Joker.Packages/Joker.ElasticSearch/"]
COPY ["src/Services/Search/Search.Application/Search.Application.csproj", "src/Services/Search/Search.Application/"]
COPY ["src/Submodules/Joker.Packages/Joker.CAP/Joker.CAP.csproj", "src/Submodules/Joker.Packages/Joker.CAP/"]
COPY ["src/Submodules/Joker.Packages/Joker.Extensions/Joker.Extensions.csproj", "src/Submodules/Joker.Packages/Joker.Extensions/"]
COPY ["src/Submodules/Joker.Packages/Joker.Domain/Joker.Domain.csproj", "src/Submodules/Joker.Packages/Joker.Domain/"]
COPY ["src/Submodules/Joker.Packages/Joker.EventBus/Joker.EventBus.csproj", "src/Submodules/Joker.Packages/Joker.EventBus/"]
RUN dotnet restore "src/Services/Search/Search.Api/Search.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Search/Search.Api"
RUN dotnet build "Search.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Search.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Search.Api.dll"]
