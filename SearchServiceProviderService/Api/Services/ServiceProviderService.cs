using System.Globalization;
using SearchServiceProviderService.Api.Dto;
using SearchServiceProviderService.Domain.Interfaces;
using SearchServiceProviderService.Domain.Models;

namespace SearchServiceProviderService.Api.Services;

public class ServiceProviderService : IServiceProviderService
{

    public ServiceProviderService(IInfrastructureRepository infrastructureRepository)
    {
        InfrastructureRepository = infrastructureRepository;
    }

    IInfrastructureRepository InfrastructureRepository { get; set; }

    public SearchServiceProvidersResponse SearchServiceProviders(string filter, Position referencePosition)
    {
        var response = new SearchServiceProvidersResponse();

        var allServiceProviders = InfrastructureRepository.ReadServiceProviders();
        var filteredServiceProviders = allServiceProviders.Where<Domain.Models.ServiceProvider>(sp => FilterServiceProviderNameWithIgnoreCase(sp.Name, filter)).ToList();

        foreach (Domain.Models.ServiceProvider sp in filteredServiceProviders)
        {
            var spr = new SearchServiceProviderResponse(sp);

            spr.Distance = $"{CalculateGelocationDistanceInKilometers(sp.Position!, referencePosition).ToString(CultureInfo.InvariantCulture)}km";
            spr.Score = CalculateFilterMatchInPercentage(sp.Name, filter);

            response.Results.Add(spr);
        }

        response.TotalDocuments = filteredServiceProviders.Count() * NumberOfDocumentsPerServiceProvider();
        response.TotalHits = filteredServiceProviders.Count();

        return response;
    }

    private int NumberOfDocumentsPerServiceProvider()
    {
        Type t = typeof(SearchServiceProviderResponse);
        var numberOfDocumentsPerServiceProvider = t.GetProperties().Count();
        
        return numberOfDocumentsPerServiceProvider;
    }

    private bool FilterServiceProviderNameWithIgnoreCase(string serviceProviderName, string filter)
    {
        return serviceProviderName.ToLower().Contains(filter.ToLower());
    }

    private int CalculateFilterMatchInPercentage(string serviceProviderName, string filter)
    {
        var serviceProviderNameLength = serviceProviderName.Length;
        var filterLength = filter.Length;
        var matchRatio = (decimal)filterLength / (decimal)serviceProviderNameLength;

        return (int)(matchRatio * 100);
    }

    private double CalculateGelocationDistanceInKilometers(Position serviceProviderPosition, Position targetPosition)
    {
        // CREDIT: https://stackoverflow.com/questions/60700865/find-distance-between-2-coordinates-in-net-core

        var d1 = serviceProviderPosition.Latitude * (Math.PI / 180.0);
        var num1 = serviceProviderPosition.Longitude * (Math.PI / 180.0);
        var d2 = targetPosition.Latitude * (Math.PI / 180.0);
        var num2 = targetPosition.Longitude * (Math.PI / 180.0) - num1;
        var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
        var result = 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));

        return (double)Math.Round((int)result / (decimal)1000, 1);
    }

}