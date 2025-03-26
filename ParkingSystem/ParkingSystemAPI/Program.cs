using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using AutoMapper;
using ParkingSystemLibrary.Data;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Services;
using ParkingSystemAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ParkingLot parkingLot = new();
builder.Services.AddSingleton(parkingLot);

builder.Services.AddScoped<VehicleService>();

builder.Services.AddScoped<ParkingLotService>();

builder.Services.AddScoped<VehicleApiService>();

builder.Services.AddScoped<ParkingLotApiService>();

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useInMemory)
{
    builder.Services.AddDbContext<VehicleDb>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
// else
// {
//     builder.Services.AddDbContext<VehicleDb>(options =>
//         options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
// }

builder.Services.AddControllers().AddNewtonsoftJson(option => 
{
    option.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
