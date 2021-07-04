#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Services/Campaign/Campaign.Api/Campaign.Api.csproj", "src/Services/Campaign/Campaign.Api/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mongo.Domain/Joker.Mongo.Domain.csproj", "src/Submodules/Joker.Packages/Joker.Mongo.Domain/"]
COPY ["src/Submodules/Joker.Packages/Joker.Exceptions/Joker.Exceptions.csproj", "src/Submodules/Joker.Packages/Joker.Exceptions/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mongo/Joker.Mongo.csproj", "src/Submodules/Joker.Packages/Joker.Mongo/"]
COPY ["src/Submodules/Joker.Packages/Joker.Repositories/Joker.Repositories.csproj", "src/Submodules/Joker.Packages/Joker.Repositories/"]
COPY ["src/Submodules/Joker.Packages/Joker.EventBus/Joker.EventBus.csproj", "src/Submodules/Joker.Packages/Joker.EventBus/"]
COPY ["src/Submodules/Joker.Packages/Joker.Domain/Joker.Domain.csproj", "src/Submodules/Joker.Packages/Joker.Domain/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mvc/Joker.Mvc.csproj", "src/Submodules/Joker.Packages/Joker.Mvc/"]
COPY ["src/Submodules/Joker.Packages/Joker.Response/Joker.Response.csproj", "src/Submodules/Joker.Packages/Joker.Response/"]
COPY ["src/Submodules/Joker.Packages/Joker.CAP/Joker.CAP.csproj", "src/Submodules/Joker.Packages/Joker.CAP/"]
COPY ["src/Submodules/Joker.Packages/Joker.Extensions/Joker.Extensions.csproj", "src/Submodules/Joker.Packages/Joker.Extensions/"]
COPY ["src/Services/Campaign/Campaign.Application/Campaign.Application.csproj", "src/Services/Campaign/Campaign.Application/"]
COPY ["src/Services/Campaign/Campaign.Domain/Campaign.Domain.csproj", "src/Services/Campaign/Campaign.Domain/"]
COPY ["src/Services/Campaign/Campaign.Infrastructure/Campaign.Infrastructure.csproj", "src/Services/Campaign/Campaign.Infrastructure/"]
RUN dotnet restore "src/Services/Campaign/Campaign.Api/Campaign.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Campaign/Campaign.Api"
RUN dotnet build "Campaign.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Campaign.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Campaign.Api.dll"]
