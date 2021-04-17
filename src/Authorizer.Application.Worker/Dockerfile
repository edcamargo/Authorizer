#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Authorizer.Application.Worker/Authorizer.Application.Worker.csproj", "src/Authorizer.Application.Worker/"]
COPY ["src/Authorizer.InfraStructure.Data/Authorizer.InfraStructure.Data.csproj", "src/Authorizer.InfraStructure.Data/"]
COPY ["src/Authorizer.Domain/Authorizer.Domain.csproj", "src/Authorizer.Domain/"]
COPY ["src/Authorizer.InfraStructure.Ioc/Authorizer.InfraStructure.Ioc.csproj", "src/Authorizer.InfraStructure.Ioc/"]
RUN dotnet restore "src/Authorizer.Application.Worker/Authorizer.Application.Worker.csproj"
COPY . .
WORKDIR "/src/src/Authorizer.Application.Worker"
RUN dotnet build "Authorizer.Application.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authorizer.Application.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorizer.Application.Worker.dll"]