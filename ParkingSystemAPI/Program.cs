using Newtonsoft.Json.Converters;
using ParkingSystem;

ParkingLot parkingLot = new();
AntarmukaParkingSys antarmuka = new(parkingLot);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(parkingLot);
builder.Services.AddSingleton(antarmuka);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(option => 
{
    option.SerializerSettings.Converters.Add(new StringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
