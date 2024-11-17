using MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Customer;
using MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Deposit;
using MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Transfer;
using MiniKPayDotNetCore.CustomerMinimalApi.Controllers.Withdraw;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseTransferServiceEndpoint();
app.UseWithdrawServiceEndpoint();
app.UseDepositServiceEndpoint();
app.UseHttpsRedirection();

app.UseCustomersServiceEndpoint();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
