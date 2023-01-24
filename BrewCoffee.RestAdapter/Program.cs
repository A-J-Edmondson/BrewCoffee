using BrewCoffee.RestAdapter.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

var app = builder.Build();

app.Configure();

await app.RunAsync();