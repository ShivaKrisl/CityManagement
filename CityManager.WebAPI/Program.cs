using Asp.Versioning;
using CitiesManager.Core.Domain.Repository_Interfaces;
using CitiesManager.Core.Services;
using CitiesManager.Core.Services_Interfaces;
using CityManager.Infrastructure;
using CityManager.Infrastructure.Repository_Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
    options.Filters.Add(new ConsumesAttribute("application/json"));
}).AddXmlSerializerFormatters(); // only controllers


var apiVersioningBuilder = builder.Services.AddApiVersioning(config =>
{
    config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Reads version number from request url at "apiVersion" constraint

    //config.ApiVersionReader = new QueryStringApiVersionReader(); //Reads version number from request query string called "api-version". Eg: api-version=1.0

    //config.ApiVersionReader = new HeaderApiVersionReader("api-version"); //Reads version number from request header called "api-version". Eg: api-version: 1.0

    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
});

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
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ApiDocumentation.xml"));
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "City Web API", Version = "v1" });

    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "City Web API", Version = "v2" });
    options.DocInclusionPredicate((version, apiDescription) =>
    {
        return apiDescription.GroupName == version;
    });


});

apiVersioningBuilder.AddApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV"; //v1
    options.SubstituteApiVersionInUrl = true;
});

// CORS for Angular 
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()) // Client app urls from appsettings.json
               //.AllowAnyHeader()
               //.AllowAnyMethod();
               .WithHeaders("Authorization", "origin", "accept", "content-type")
               .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHsts();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
});

app.UseRouting();

app.UseCors(); // between UseRouting and UseAuthorization

app.UseAuthorization();

app.MapControllers();

app.Run();
