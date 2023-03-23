#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/CMH.MobileHomeTracker.Api/CMH.MobileHomeTracker.Api.csproj", "Api/CMH.MobileHomeTracker.Api/"]
COPY ["Api/CMH.MobileHomeTracker.Dto/CMH.MobileHomeTracker.Dto.csproj", "Api/CMH.MobileHomeTracker.Dto/"]
COPY ["Infrastructure/CMH.MobileHomeTracker.Infrastructure/CMH.MobileHomeTracker.Infrastructure.csproj", "Infrastructure/CMH.MobileHomeTracker.Infrastructure/"]
COPY ["Data/CMH.MobileHomeTracker.Data.DB2/CMH.MobileHomeTracker.Data.DB2.csproj", "Data/CMH.MobileHomeTracker.Data.DB2/"]
COPY ["Core/CMH.MobileHomeTracker.Domain/CMH.MobileHomeTracker.Domain.csproj", "Core/CMH.MobileHomeTracker.Domain/"]
COPY ["Adapters/CMH.MobileHomeTracker.Adapters.Twilio/CMH.MobileHomeTracker.Adapters.Twilio.csproj", "Adapters/CMH.MobileHomeTracker.Adapters.Twilio/"]
COPY ["Data/CMH.MobileHomeTracker.Data.SqlServer/CMH.MobileHomeTracker.Data.SqlServer.csproj", "Data/CMH.MobileHomeTracker.Data.SqlServer/"]
RUN dotnet restore "Api/CMH.MobileHomeTracker.Api/CMH.MobileHomeTracker.Api.csproj"
COPY . .
WORKDIR "/src/Api/CMH.MobileHomeTracker.Api"
RUN dotnet build "CMH.MobileHomeTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CMH.MobileHomeTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CMH.MobileHomeTracker.Api.dll"]