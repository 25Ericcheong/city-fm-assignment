using System.Net.Mime;
using CityFm.Exceptions;
using CityFm.Models.Config;
using CityFm.Models.Static;
using CityFm.Models.Static.Http;
using CityFm.Services;

var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection(AppSettingKeys.Settings).Get<AppSettings>();

if (settings is null) throw new NullReferenceException("AppSettings cannot be found");

builder.Services
    .AddControllers(options => { options.Filters.Add(typeof(GeneralExceptionFilter), 1); })
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });


builder.Services.AddHttpClient(ClientKeys.AllTheCloudsProductFxRate, client =>
{
    client.BaseAddress = new Uri(ExternalUri.AllTheClouds);
    client.DefaultRequestHeaders.Add("accept", MediaTypeNames.Application.Json);
    client.DefaultRequestHeaders.Add("api-key", settings.AllTheClouds.ApiKey);
});

builder.Services.AddHttpClient(ClientKeys.AllTheCloudsOrder, client =>
{
    client.BaseAddress = new Uri(ExternalUri.AllTheClouds);
    client.DefaultRequestHeaders.Add("accept", MediaTypeNames.Application.Json);
    client.DefaultRequestHeaders.Add("Content-Type", MediaTypeNames.Application.Json);
    client.DefaultRequestHeaders.Add("api-key", settings.AllTheClouds.ApiKey);
});

builder.Services.AddTransient<IProductsService, ProductsService>();
builder.Services.AddTransient<IFxRatesService, FxRatesService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(Policy.AllowCors, policy =>
    {
        policy.WithOrigins(settings.Urls.ClientUrl)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy.AllowCors);

app.UseAuthorization();

app.MapControllers();

app.Run();