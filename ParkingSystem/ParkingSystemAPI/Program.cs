using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json.Converters;
using ParkingSystemLibrary.Data;
using ParkingSystemLibrary.Models;
using ParkingSystemLibrary.Services;
using ParkingSystemAPI.Services;
using ParkingSystemLibrary.Repositories;
using System.Text;
using FluentValidation.AspNetCore;
using ParkingSystemAPI.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ParkingLot parkingLot = new();
builder.Services.AddSingleton(parkingLot);

builder.Services.AddScoped<VehicleRepository>();

builder.Services.AddScoped<VehicleService>();

builder.Services.AddScoped<ParkingLotService>();

builder.Services.AddScoped<VehicleApiService>();

builder.Services.AddScoped<ParkingLotApiService>();

var useInMemory = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useInMemory)
{
    builder.Services.AddDbContext<ParkingDb>(options =>
        options.UseInMemoryDatabase("TestDb"));
}
else
{
    builder.Services.AddDbContext<ParkingDb>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}

builder.Services.AddControllers().AddNewtonsoftJson(option => 
{
    option.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterVehicleValidator>();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var securityKey = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(securityKey),
//             ValidateIssuer = true,
//             ValidIssuer = jwtSettings["Issuer"],
//             ValidateAudience = true,
//             ValidAudience = jwtSettings["Audience"],
//             ValidateLifetime = true,
//             ClockSkew = TimeSpan.Zero
//         };
//     });

// builder.Services.AddAuthorization();

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

app.UseAuthentication();

// app.UseAuthorization();

app.MapControllers();

app.Run();
