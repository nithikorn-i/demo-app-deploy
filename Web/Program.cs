using Application.Interfaces.SU;
using Application.Services.SU;
using Infrastructure.Repositories.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Lists = Application.Features.SU.User001.Lists;
using ListWins = Application.Features.SU.Win001.Lists;

Console.WriteLine("🚀 Starting .NET application setup...");

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

Console.WriteLine("✅ Application build completed.");

// 🧱 Middleware pipeline
app.UseCors("AllowAngularClient");

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("🧩 Development mode detected: Enabling Swagger UI...");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // ❌ อย่า redirect https ใน container ถ้าไม่มี cert
    Console.WriteLine("⚠️ Non-development mode detected: Skipping HTTPS redirection (no cert).");
}

// 🔐 Authentication / Authorization
app.UseAuthentication();
app.UseAuthorization();

// 🌐 Static files (Angular frontend)
app.UseDefaultFiles();
app.UseStaticFiles();

// 🧭 Routing
app.MapControllers();
app.MapFallbackToFile("index.html"); // Angular routes fallback

Console.WriteLine("✅ App is running and ready to accept requests...");

// 🏁 Start the app — block main thread here
app.Run();
