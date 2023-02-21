using PaymentGateway.Acquisition.Application;
using PaymentGateway.Acquisition.Persistence.Mongo;
using PaymentGateway.BuildingBlocks.Api.Abstractions;
using PaymentsGateway.BuildingBlocks.Messaging;

const string EndpointName = "Acquisition";
var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppSettings();
builder.Host.UseMessaging(EndpointName);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();

var app = builder.Build();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();