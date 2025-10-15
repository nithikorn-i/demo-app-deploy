using Application.Interfaces.SU;
using Application.Services.SU;
using Infrastructure.Repositories.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Lists = Application.Features.SU.User001.Lists;
using ListWins = Application.Features.SU.Win001.Lists;

Console.WriteLine("🚀 Starting .NET application setup...");;

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

Console.WriteLine($"🌍 Environment: {builder.Environment.EnvironmentName}");

var app = builder.Build();

// ✅ Log that app has been built
Console.WriteLine("✅ Application build completed.");

app.UseCors("AllowAngularClient");

// Swagger setup (only in Development)
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("🧩 Development mode detected: Enabling Swagger UI...");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // ⚠️ Disable HTTPS redirection in container (no cert)
    Console.WriteLine("⚠️ Non-development mode detected: Skipping HTTPS redirection (no cert).");
    // ❌ อย่าใช้ app.UseHttpsRedirection(); ใน container ถ้าไม่มี cert
}

app.UseAuthentication();
app.MapControllers();

Console.WriteLine("App is use");

app.UseDefaultFiles();
app.MapFallbackToFile("index.html");
app.UseStaticFiles();

Console.WriteLine("✅ App is running and ready to accept requests...");

app.Run();
