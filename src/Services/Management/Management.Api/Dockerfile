#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["src/Services/Management/Management.Api/Management.Api.csproj", "src/Services/Management/Management.Api/"]
COPY ["src/Services/Management/Management.Application/Management.Application.csproj", "src/Services/Management/Management.Application/"]
COPY ["src/Services/Management/Management.Core/Management.Core.csproj", "src/Services/Management/Management.Core/"]
COPY ["src/Submodules/Joker.Packages/Joker.Repositories/Joker.Repositories.csproj", "src/Submodules/Joker.Packages/Joker.Repositories/"]
COPY ["src/Services/Management/Management.Infrastructure/Management.Infrastructure.csproj", "src/Services/Management/Management.Infrastructure/"]
COPY ["src/Submodules/Joker.Packages/Joker.EntityFrameworkCore/Joker.EntityFrameworkCore.csproj", "src/Submodules/Joker.Packages/Joker.EntityFrameworkCore/"]
COPY ["src/Submodules/Joker.Packages/Joker.Exceptions/Joker.Exceptions.csproj", "src/Submodules/Joker.Packages/Joker.Exceptions/"]
COPY ["src/Submodules/Joker.Packages/Joker.Mvc/Joker.Mvc.csproj", "src/Submodules/Joker.Packages/Joker.Mvc/"]
COPY ["src/Submodules/Joker.Packages/Joker.Response/Joker.Response.csproj", "src/Submodules/Joker.Packages/Joker.Response/"]
COPY ["src/Submodules/Joker.Packages/Joker.EntityFrameworkCore.Migration/Joker.EntityFrameworkCore.Migration.csproj", "src/Submodules/Joker.Packages/Joker.EntityFrameworkCore.Migration/"]
RUN dotnet restore "src/Services/Management/Management.Api/Management.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Management/Management.Api"
RUN dotnet build "Management.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Management.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Api.dll"]
