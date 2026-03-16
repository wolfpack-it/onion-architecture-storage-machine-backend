using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using Contoso.StorageMachine;
using Contoso.StorageMachine.Stock;
using Contoso.StorageMachine.Repacking;

var builder = WebApplication.CreateBuilder(args);

// Dependency injection: data access implementations
builder.Services.AddSingleton<IStockRepository, StockRepository>();
builder.Services.AddSingleton<IBinTreeRepository, RepackingRepository>();

// API documentation: OpenAPI + Scalar UI
builder.Services.AddSwaggerDocumentation();

// JSON serialization: camelCase property names
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services
    .AddHsts(options => options.MaxAge = TimeSpan.FromDays(180))
    .AddAuthorization()
    .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

app.UseHsts();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

// A basic example of handling a GET request
app.MapGet("/hello", () => Results.Text("Storage machine is running"));

// See the following example on how to process a POST request, decode JSON and return different responses
PostExample.Map(app);

// Dispatching and handling of Stock component requests
StockEndpoints.Map(app);

// Dispatching and handling of Repacking component requests
RepackingEndpoints.Map(app);

app.Run();
