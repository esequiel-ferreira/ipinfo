/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.ApiControllers;
using IPInfo.Controllers;
using IPInfo.Entities.Repository;
using IPInfo.Entities.Service;
using Microsoft.Extensions.Caching.Memory;

public class InvalidateCacheService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IIPAddressRepository _ipAddressRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IIP2CountryService _ip2CountryService;
    private readonly IMemoryCache _cache;

    public InvalidateCacheService(ILogger logger, IIPAddressRepository ipAddressRepository, ICountryRepository countryRepository, IIP2CountryService ip2CountryService, IMemoryCache cache)
    {
        _logger = logger;
        _ipAddressRepository = ipAddressRepository;
        _countryRepository = countryRepository;
        _ip2CountryService = ip2CountryService;
        _cache = cache;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Invalidate Cache Service stated.");
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            IP2CountryJob ip2CountryJob = new IP2CountryJob(_logger, _ipAddressRepository, _countryRepository, _ip2CountryService, _cache);
            await ip2CountryJob.ExecuteAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message); 
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Invalidate Cache Service stopped.");
    }

}