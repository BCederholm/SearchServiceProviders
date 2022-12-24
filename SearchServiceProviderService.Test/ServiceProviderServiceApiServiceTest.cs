using Moq;
using SearchServiceProviderService.Api.Services;
using SearchServiceProviderService.Domain.Interfaces;

namespace SearchServiceProviderService.Test;

[TestClass]
public class ServiceProviderServiceApiServiceTest
{
    private readonly Mock<IInfrastructureRepository> _infrastructureRepository;
    private ServiceProviderService _sut;

    public ServiceProviderServiceApiServiceTest()
    {
        _infrastructureRepository = new Mock<IInfrastructureRepository>();
        _sut = new ServiceProviderService(_infrastructureRepository.Object);
    }

    [TestMethod]
    [DataRow(59.334591, 18.063240, "massage", 9, "2km", 100)]
    [DataRow(59.334591, 18.063240, "thai", 1, "1.7km", 36)]
    public void SearchServiceProviders_DefaultScenario_Success(double targetLatitude, double targetLongitude, string filter, int expectedServiceProviders, string expectedDistanceOnFirstServiceProvider, int expectedScoreOnFirstServiceProvider)
    {
        // Arrange
        var fakeReadServiceProviders = ServiceProvidersTestData();
        var targetPosition = new Domain.Models.Position() { Latitude = targetLatitude, Longitude = targetLongitude };

        // Act
        _infrastructureRepository.Setup(p => p.ReadServiceProviders()).Returns(fakeReadServiceProviders);
        var response = _sut.SearchServiceProviders(filter, targetPosition);

        // Assert
        Assert.AreEqual(expectedServiceProviders, response.Results.Count);
        Assert.AreEqual(expectedDistanceOnFirstServiceProvider, response.Results[0].Distance);
        Assert.AreEqual(expectedScoreOnFirstServiceProvider, response.Results[0].Score);
    }

    private List<Domain.Models.ServiceProvider> ServiceProvidersTestData()
    {
        Domain.Models.ServiceProvider sp1 = new Domain.Models.ServiceProvider { Id = 1, Name = "Massage", Position = new Domain.Models.Position() { Latitude = 59.3166428, Longitude = 18.0561182999999 } };
        Domain.Models.ServiceProvider sp2 = new Domain.Models.ServiceProvider { Id = 2, Name = "Salongens massage", Position = new Domain.Models.Position() { Latitude = 59.3320299, Longitude = 18.023149800000056 } };
        Domain.Models.ServiceProvider sp3 = new Domain.Models.ServiceProvider { Id = 3, Name = "Massör", Position = new Domain.Models.Position() { Latitude = 59.315887, Longitude = 18.081163800000013 } };
        Domain.Models.ServiceProvider sp4 = new Domain.Models.ServiceProvider { Id = 4, Name = "Svensk massage", Position = new Domain.Models.Position() { Latitude = 59.3433317, Longitude = 18.090476800000033 } };
        Domain.Models.ServiceProvider sp5 = new Domain.Models.ServiceProvider { Id = 5, Name = "Thaimassage", Position = new Domain.Models.Position() { Latitude = 59.31952889999999, Longitude = 18.062400900000057 } };
        Domain.Models.ServiceProvider sp6 = new Domain.Models.ServiceProvider { Id = 6, Name = "LPG-massage", Position = new Domain.Models.Position() { Latitude = 59.34411099999999, Longitude = 18.049118499999963 } };
        Domain.Models.ServiceProvider sp7 = new Domain.Models.ServiceProvider { Id = 7, Name = "Massage 30 min", Position = new Domain.Models.Position() { Latitude = 59.44411099999999, Longitude = 18.049118499999963 } };
        Domain.Models.ServiceProvider sp8 = new Domain.Models.ServiceProvider { Id = 8, Name = "Ansiktsmassage", Position = new Domain.Models.Position() { Latitude = 59.44411099999999, Longitude = 18.149118499999963 } };
        Domain.Models.ServiceProvider sp9 = new Domain.Models.ServiceProvider { Id = 9, Name = "Massage", Position = new Domain.Models.Position() { Latitude = 59.40411099999999, Longitude = 18.109118499999963 } };
        Domain.Models.ServiceProvider sp10 = new Domain.Models.ServiceProvider { Id = 10, Name = "Härlig massage", Position = new Domain.Models.Position() { Latitude = 59.40211099999999, Longitude = 18.105118499999963 } };

        var respons = new List<Domain.Models.ServiceProvider> { sp1, sp2, sp3, sp4, sp5, sp6, sp7, sp8, sp9, sp10 };

        return respons;
    }
}