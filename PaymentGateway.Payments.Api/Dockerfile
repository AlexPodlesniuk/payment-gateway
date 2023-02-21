FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PaymentGateway.Payments.Api/PaymentGateway.Payments.Api.csproj", "PaymentGateway.Payments.Api/"]
COPY ["PaymentGateway.BuildingBlocks.Api.Abstractions/PaymentGateway.BuildingBlocks.Api.Abstractions.csproj", "PaymentGateway.BuildingBlocks.Api.Abstractions/"]
COPY ["PaymentGateway.BuildingBlocks.Application/PaymentGateway.BuildingBlocks.Application.csproj", "PaymentGateway.BuildingBlocks.Application/"]
COPY ["PaymentGateway.BuildingBlocks.Persistence/PaymentGateway.BuildingBlocks.Persistence.csproj", "PaymentGateway.BuildingBlocks.Persistence/"]
COPY ["PaymentGateway.Payments.Application/PaymentGateway.Payments.Application.csproj", "PaymentGateway.Payments.Application/"]
COPY ["PaymentGateway.Acquisition.Contracts/PaymentGateway.Acquisition.Contracts.csproj", "PaymentGateway.Acquisition.Contracts/"]
COPY ["PaymentGateway.BuildingBlocks.Domain/PaymentGateway.BuildingBlocks.Domain.csproj", "PaymentGateway.BuildingBlocks.Domain/"]
COPY ["PaymentsGateway.BuildingBlocks.Messaging/PaymentsGateway.BuildingBlocks.Messaging.csproj", "PaymentsGateway.BuildingBlocks.Messaging/"]
COPY ["PaymentGateway.Payments.Domain/PaymentGateway.Payments.Domain.csproj", "PaymentGateway.Payments.Domain/"]
COPY ["PaymentGateway.Payments.Persistence.Mongo/PaymentGateway.Payments.Persistence.Mongo.csproj", "PaymentGateway.Payments.Persistence.Mongo/"]
COPY ["PaymentGateway.BuildingBlocks.Persistence.Mongo/PaymentGateway.BuildingBlocks.Persistence.Mongo.csproj", "PaymentGateway.BuildingBlocks.Persistence.Mongo/"]
RUN dotnet restore "PaymentGateway.Payments.Api/PaymentGateway.Payments.Api.csproj"
COPY . .
WORKDIR "/src/PaymentGateway.Payments.Api"
RUN dotnet build "PaymentGateway.Payments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PaymentGateway.Payments.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Payments.Api.dll"]
