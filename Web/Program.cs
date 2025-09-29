using Application.Features.SU.Oat001;
using Application.Features.SU.User001;
using Application.Interfaces.SU;
using Application.Services.SU;
using Infrastructure.Repositories.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Serilog;
using Serilog.Formatting.Json;
using Web.LoggingMiddleware;

var builder = WebApplication.CreateBuilder(args);

// อ่านค่า ApiBaseUrl จาก config
var apiBaseUrl = builder.Configuration.GetValue<string>("ApiBaseUrl");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .Enrich.WithThreadId()
    .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
        requestUri: "http://172.16.0.137:5000",
        textFormatter: new JsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200","http://web-ss.myns","http://web-dev.myns")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserSerice, UserService>();

builder.Services.AddScoped<IOatReposttory, OatRepository>();

builder.Services.AddScoped<IOatSerice, OatService>();

builder.Services.AddScoped<ListOat>();

builder.Services.AddScoped<Lists>();


var app = builder.Build();
app.UseCors("AllowAngularClient");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/env-check", () => new {
    Env = builder.Environment.EnvironmentName,
    ApiBaseUrl = apiBaseUrl
});
app.UseRouting();
app.UseMiddleware<StructuredLoggingMiddleware>();
app.UseStaticFiles();
app.UseDefaultFiles();
app.MapFallbackToFile("index.html");
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
app.Run();

// app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast =  Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
