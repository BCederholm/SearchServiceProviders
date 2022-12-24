using SearchServiceProviderService.Api.Dto;
using SearchServiceProviderService.Domain.Models;

namespace SearchServiceProviderService.Api.Services;

public interface IServiceProviderService
{
    SearchServiceProvidersResponse SearchServiceProviders(string filter, Position referencePosition);
}