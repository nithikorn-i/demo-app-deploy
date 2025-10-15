using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add basic logs
Console.WriteLine("🚀 Starting .NET application setup...");

// Add services to the container
builder.Services.AddControllers();

// Add Swagger only for Development
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine($"🌍 Environment: {builder.Environment.EnvironmentName}");

var app = builder.Build();

// ✅ Log that app has been built
Console.WriteLine("✅ Application build completed.");

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

// ✅ Basic middleware
app.UseRouting();
app.UseAuthorization();

// ✅ Map endpoints
app.MapControllers();

// ✅ Add a clear startup log message
Console.WriteLine("✅ App is running and ready to accept requests...");

// Start the application
app.Run();
