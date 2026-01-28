using Devices.Application.Interfaces;
using Devices.Application.Services;
using Devices.Infrastructure.Persistence;
using Devices.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<DevicesDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// DI
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<CreateDeviceService>();
builder.Services.AddScoped<GetDevicesService>();
builder.Services.AddHealthChecks();

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.UseAuthorization();
app.MapControllers();
app.Run();
