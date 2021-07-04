#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Services/Location/Location.Api/Location.Api.csproj", "src/Services/Location/Location.Api/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mvc/Joker.Mvc.csproj", "src/Submodules/Joker.Packages/Joker.Mvc/"]
COPY ["src/Submodules/Joker.Packages/Joker.Exceptions/Joker.Exceptions.csproj", "src/Submodules/Joker.Packages/Joker.Exceptions/"]
COPY ["src/Submodules/Joker.Packages/Joker.Response/Joker.Response.csproj", "src/Submodules/Joker.Packages/Joker.Response/"]
COPY ["src/Services/Location/Location.Application/Location.Application.csproj", "src/Services/Location/Location.Application/"]
COPY ["src/Services/Location/Location.Core/Location.Core.csproj", "src/Services/Location/Location.Core/"]
COPY ["src/Submodules/Joker.Packages/Joker.Repositories/Joker.Repositories.csproj", "src/Submodules/Joker.Packages/Joker.Repositories/"]
COPY ["src/Services/Location/Location.Infrastructure/Location.Infrastructure.csproj", "src/Services/Location/Location.Infrastructure/"]
COPY ["src/Submodules/Joker.Packages/Joker.EntityFrameworkCore/Joker.EntityFrameworkCore.csproj", "src/Submodules/Joker.Packages/Joker.EntityFrameworkCore/"]
COPY ["src/Submodules/Joker.Packages/Joker.EntityFrameworkCore.Migration/Joker.EntityFrameworkCore.Migration.csproj", "src/Submodules/Joker.Packages/Joker.EntityFrameworkCore.Migration/"]
RUN dotnet restore "src/Services/Location/Location.Api/Location.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Location/Location.Api"
RUN dotnet build "Location.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Location.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Location.Api.dll"]
