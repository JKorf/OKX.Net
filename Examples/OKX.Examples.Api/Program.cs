using OKX.Net.Interfaces.Clients;
using Microsoft.AspNetCore.Mvc;
using OKX.Net.Objects;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add the OKX services
builder.Services.AddOKX();

// OR to provide API credentials for accessing private endpoints, or setting other options:
/*
builder.Services.AddOKX(options =>
{    
   options.ApiCredentials = new ApiCredentials("<APIKEY>", "<APISECRET>", "<PASS>");
   options.Rest.RequestTimeout = TimeSpan.FromSeconds(5);
});
*/

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Map the endpoints and inject the OKX rest client
app.MapGet("/{Symbol}", async ([FromServices] IOKXRestClient client, string symbol) =>
{
    var result = await client.UnifiedApi.ExchangeData.GetTickerAsync(symbol);
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.MapGet("/Balances", async ([FromServices] IOKXRestClient client) =>
{
    var result = await client.UnifiedApi.Account.GetAccountBalanceAsync();
    return result.Success
        ? Results.Ok(result.Data)
        : Results.Problem(result.Error?.Message, statusCode: 502);
})
.WithOpenApi();

app.Run();
