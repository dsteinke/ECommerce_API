FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
EXPOSE 3000

ENV ASPNETCORE_URLS=http://+:3000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ECommerce_API/ECommerce.API.csproj", "ECommerce.API/"]
COPY ["ECommerce_API.Application/ECommerce.Application.csproj", "ECommerce.Application/"]
COPY ["ECommerce_API.Core/ECommerce.Domain.csproj", "ECommerce.Domain/"]
COPY ["ECommerce_API.Infrastructure/ECommerce.Infrastructure.csproj", "ECommerce.Infrastructure/"]
COPY ["ECommerce_API.Test/ECommerce.Test.csproj", "ECommerce.Test/"]
RUN dotnet restore "ECommerce.API/ECommerce.API.csproj"

COPY . .

WORKDIR /src/ECommerce_API
RUN dotnet build "ECommerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

COPY ECommerce_API/ecommerce_demo.db ./ecommerce_demo.db

ENTRYPOINT [ "dotnet", "ECommerce.API.dll" ]