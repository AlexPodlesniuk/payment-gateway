using Bank.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/payments", (PaymentInfo payment) =>
{
    var localPaymentId = Guid.NewGuid().ToString();
    var response = new
    {
        Id = localPaymentId,
        Status = 200
    };
    
    return Results.Created($"/payments/{localPaymentId}", response);
});

app.Run();