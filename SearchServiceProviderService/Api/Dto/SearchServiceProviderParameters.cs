namespace SearchServiceProviderService.Api.Dto;

public class SearchServiceProviderParameters
{
    public string ServiceName { get; set; } = string.Empty;

    public double Latitude { get; set; }

    public double Longitude { get; set; }
}