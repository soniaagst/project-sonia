using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using ParkingSystemLibrary;
using ParkingSystemLibrary.Models;

ParkingLot parkingLot = new();
// ParkingSystemAntarmuka antarmuka = new(parkingLot);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(parkingLot);
// builder.Services.AddSingleton(antarmuka);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(option => 
{
    option.SerializerSettings.Converters.Add(new StringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

builder.Services.AddScoped<ParkingSystemAntarmuka>();

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
