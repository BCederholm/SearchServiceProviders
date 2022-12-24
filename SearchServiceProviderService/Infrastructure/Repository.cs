using System.Text.Json;
using SearchServiceProviderService.Domain.Interfaces;

namespace SearchServiceProviderService.Infrastructure;

public class InfrastructureRepository : IInfrastructureRepository
{
    public List<Domain.Models.ServiceProvider> ReadServiceProviders()
    {
        List<Domain.Models.ServiceProvider> result = new List<Domain.Models.ServiceProvider>();

        try
        {
            using (var sr = new StreamReader("data.json"))
            {
                var json = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(json))
                {
                    var deserialized = JsonSerializer.Deserialize<List<Domain.Models.ServiceProvider>>(json);
                    if (deserialized != null)
                    {
                        result = deserialized;
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("data.json could not be found or read:");
            Console.WriteLine(e.Message);
        }

        return result;
    }
}
