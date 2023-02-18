using Coffee.Services;
using Coffee.Services.Interfaces;
using Business.Middlewares;
using Business.ServiceResult.Interfaces;
using Business.ServiceResult;
using Business.Coffee.Http.Interfaces;
using Business.Coffee.Http;
using Coffee.HttpServices.Interfaces;
using Coffee.HttpServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICoffeeService, CoffeeService>();
builder.Services.AddSingleton<ICounterService, CounterService>();
builder.Services.AddScoped<IHttpWeatherService, HttpWeatherService>();
builder.Services.AddScoped<IServiceResultFactory, ServiceResultFactory>();
builder.Services.AddHttpClient<IHttpWeatherClient, HttpWeatherClient>();



var app = builder.Build();

var _config = builder.Configuration;

app.UseMiddleware<Http418HandlerMiddleware>
    (_config.GetValue<int>("Http418_Month"),
    _config.GetValue<int>("Http418_Day"));

app.MapControllers();

app.Run();
