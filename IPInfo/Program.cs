/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api    
*/

global using Microsoft.EntityFrameworkCore;
using IPInfo;
using IPInfo.DataAccess.EntityFramework;
using IPInfo.DataAccess.EntityFramework.Repository;
using IPInfo.Entities.Report;
using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// use DataContext Factory
IPInfoAbstractDataContext context = DataContextFactory.CreateIPInfoDataContext();

// Register  dependencies
builder.Services.AddScoped<IIPAddressRepository>(sp =>
{
    return new IPAddressEntityFrameworkRepository(context);
});

builder.Services.AddScoped<ICountryRepository>(sp =>
{ 
    return new CountryEntityFrameworkRepository(context);
});
 
builder.Services.AddScoped<IIP2CountryService>(sp =>
{ 
    return IP2CountryFactory.CreateIP2CountryService();
});
 
builder.Services.AddScoped<IReport>(sp =>
{ 
    return context;
});
 

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMemoryCache();

// Invalidate Cache service
builder.Services.AddHostedService<InvalidateCacheService>();

IPInfoAbstractDataContext contextInvalidateCacheService = DataContextFactory.CreateIPInfoDataContext();
builder.Services.AddSingleton<IIPAddressRepository, IPAddressEntityFrameworkRepository>(s =>
{ 
    return new IPAddressEntityFrameworkRepository(contextInvalidateCacheService);
});

builder.Services.AddSingleton<ICountryRepository, CountryEntityFrameworkRepository>(s =>
{ 
    return new CountryEntityFrameworkRepository(contextInvalidateCacheService);
});

builder.Services.AddSingleton<IIP2CountryService>(s =>
{
    return IP2CountryFactory.CreateIP2CountryService();
});

// Logging  
builder.Services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("Logging"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
