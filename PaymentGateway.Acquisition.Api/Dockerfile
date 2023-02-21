FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentGateway.Acquisition.Api/PaymentGateway.Acquisition.Api.csproj", "PaymentGateway.Acquisition.Api/"]
COPY ["PaymentGateway.BuildingBlocks.Api.Abstractions/PaymentGateway.BuildingBlocks.Api.Abstractions.csproj", "PaymentGateway.BuildingBlocks.Api.Abstractions/"]
COPY ["PaymentGateway.BuildingBlocks.Application/PaymentGateway.BuildingBlocks.Application.csproj", "PaymentGateway.BuildingBlocks.Application/"]
COPY ["PaymentGateway.BuildingBlocks.Persistence/PaymentGateway.BuildingBlocks.Persistence.csproj", "PaymentGateway.BuildingBlocks.Persistence/"]
COPY ["PaymentGateway.Acquisition.Application/PaymentGateway.Acquisition.Application.csproj", "PaymentGateway.Acquisition.Application/"]
COPY ["PaymentGateway.Acquisition.Domain/PaymentGateway.Acquisition.Domain.csproj", "PaymentGateway.Acquisition.Domain/"]
COPY ["PaymentGateway.BuildingBlocks.Domain/PaymentGateway.BuildingBlocks.Domain.csproj", "PaymentGateway.BuildingBlocks.Domain/"]
COPY ["PaymentGateway.Acquisition.Contracts/PaymentGateway.Acquisition.Contracts.csproj", "PaymentGateway.Acquisition.Contracts/"]
COPY ["PaymentsGateway.BuildingBlocks.Messaging/PaymentsGateway.BuildingBlocks.Messaging.csproj", "PaymentsGateway.BuildingBlocks.Messaging/"]
COPY ["PaymentGateway.BuildingBlocks.Persistence.Mongo/PaymentGateway.BuildingBlocks.Persistence.Mongo.csproj", "PaymentGateway.BuildingBlocks.Persistence.Mongo/"]
COPY ["PaymentGateway.Acquisition.Persistence.Mongo/PaymentGateway.Acquisition.Persistence.Mongo.csproj", "PaymentGateway.Acquisition.Persistence.Mongo/"]
RUN dotnet restore "PaymentGateway.Acquisition.Api/PaymentGateway.Acquisition.Api.csproj"
COPY . .
WORKDIR "/src/PaymentGateway.Acquisition.Api"
RUN dotnet build "PaymentGateway.Acquisition.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.Acquisition.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Acquisition.Api.dll"]
