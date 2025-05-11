using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.Services;
using CitiesManager.Core.Services_Interfaces;
using CityManager.Infrastructure;
using CityManager.Infrastructure.Repository_Classes;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); // only controllers

builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
builder.Services.AddScoped<ICitiesAdder, CitiesAdderService>();
builder.Services.AddScoped<ICitiesGetterService, CitiesGetterService>();
builder.Services.AddScoped<ICitiesUpdateService, CitiesUpdateService>();
builder.Services.AddScoped<ICitiesDeleteService, CitiesDeleteService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
