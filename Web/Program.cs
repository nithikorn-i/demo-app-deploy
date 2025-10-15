using Application.Interfaces.SU;
using Application.Services.SU;
using Infrastructure.Repositories.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Lists = Application.Features.SU.User001.Lists;
using ListWins = Application.Features.SU.Win001.Lists;

Console.WriteLine("App is running...");

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine("App is AllowAngularClient");

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
Console.WriteLine("App is Set Services");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWinRepository, WinRepository>();
builder.Services.AddScoped<IWinService, WinService>();
builder.Services.AddScoped<Lists>();
builder.Services.AddScoped<ListWins>();

Console.WriteLine("App is builder..");

var app = builder.Build();

app.UseCors("AllowAngularClient");

Console.WriteLine("App is UseSwagger");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.MapControllers();

Console.WriteLine("App is use");

app.UseDefaultFiles();
app.MapFallbackToFile("index.html");
app.UseStaticFiles();

app.Run();
