using SearchServiceProviderService.Api.Dto;
using SearchServiceProviderService.Api.Services;
using SearchServiceProviderService.Domain.Interfaces;
using SearchServiceProviderService.Domain.Models;
using SearchServiceProviderService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IInfrastructureRepository, InfrastructureRepository>();
builder.Services.AddScoped<IServiceProviderService, ServiceProviderService>();

var app = builder.Build();

app.MapGet("/", () => "Try me: http://localhost:5085/SearchServiceProviders?ServiceName=massage&Latitude=59.334591&Longitude=18.063240");

app.MapGet("/SearchServiceProviders", ([AsParameters] SearchServiceProviderParameters criteria, IServiceProviderService serviceProviderService) =>
{
    var response = serviceProviderService.SearchServiceProviders(criteria.ServiceName, new Position() { Latitude = criteria.Latitude, Longitude = criteria.Longitude });
    return response == null ? Results.NotFound() : Results.Ok(response);
});

app.Run();