namespace SearchServiceProviderService.Domain.Interfaces;

public interface IInfrastructureRepository
{

    public List<Domain.Models.ServiceProvider> ReadServiceProviders();

}