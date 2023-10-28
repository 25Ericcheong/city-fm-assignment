using CityFm.Exceptions;
using CityFm.Models.Config;
using CityFm.Models.Static;
using CityFm.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));
builder.Services
    .AddControllers(options => { options.Filters.Add(typeof(GeneralExceptionFilter), 1); })
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

builder.Services.AddHttpClient<ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("AllTheClouds", client =>
{
    client.BaseAddress = new Uri("https://alltheclouds.com.au/api/");
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
    client.DefaultRequestHeaders.Add("api-key", "API-UPEP55F2JPI3KHP");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(Policy.AllowCors, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
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