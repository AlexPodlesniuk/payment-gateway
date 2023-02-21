using PaymentGateway.BuildingBlocks.Api.Abstractions;
using PaymentGateway.Payments.Application;
using PaymentGateway.Payments.Persistence.Mongo;
using PaymentsGateway.BuildingBlocks.Messaging;

const string EndpointName = "Payments";
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings();
builder.Host.UseMessaging(EndpointName);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();