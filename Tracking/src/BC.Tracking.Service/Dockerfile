#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG CTGMUSEPF_ARTIFACTS_PAT
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/BC.Tracking.Service/BC.Tracking.Service.csproj", "src/BC.Tracking.Service/"]
COPY ["src/BC.Tracking.Domain/BC.Tracking.Domain.csproj", "src/BC.Tracking.Domain/"]
COPY ["tests/BC.Tracking.Domain.Tests/BC.Tracking.Domain.Tests.csproj", "src/BC.Tracking.Domain.Tests/"]
RUN dotnet restore "src/BC.Tracking.Service/BC.Tracking.Service.csproj"
RUN dotnet restore "src/BC.Tracking.Domain.Tests/BC.Tracking.Domain.Tests.csproj"
COPY . .
WORKDIR "/src/src/BC.Tracking.Service"
RUN dotnet build "BC.Tracking.Service.csproj" -c Release -o /app/build

WORKDIR "/src/src/BC.Tracking.Domain.Tests"
RUN dotnet build "BC.Tracking.Domain.Tests.csproj" -c Release -o /app/build
RUN dotnet test "BC.Tracking.Domain.Tests.csproj" --logger "trx;LogFileName=BC.Tracking.trx"

WORKDIR "/src/src/BC.Tracking.Service"
FROM build AS publish
RUN dotnet publish "BC.Tracking.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Connexity.BC.Tracking.Service.dll"]