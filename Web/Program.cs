using Application.Interfaces.SU;
using Application.Services.SU;
using Infrastructure.Repositories.SU;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Lists = Application.Features.SU.User001.Lists;
using ListWins = Application.Features.SU.Win001.Lists;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var app = builder.Build();

app.UseCors("AllowAngularClient");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.MapControllers();

app.UseDefaultFiles();
app.MapFallbackToFile("index.html");
app.UseStaticFiles();
app.UseHttpsRedirection();

app.Run();
