using Consul;

namespace Order.Infrastructure.ServiceDiscovery;

public class ConsulService(IConsulClient consulClient) : IServiceDiscoveryService
{
    public async Task<Uri> GetServiceUri(string serviceName, string requestUrl)
    {
        var allRegisteredService = await consulClient.Agent.Services();

        var registeredServices = allRegisteredService.Response?
            .Where(s => s.Value.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase))
            .Select(s => s.Value)
            .ToList();

        var service = registeredServices!.First();

        Console.WriteLine(service.Address);
        Console.WriteLine(service.Port);
        Console.WriteLine(requestUrl);

        // localhost, Port: 5002, requestUrl api/customers/1239126793612
        var uri = new Uri($"http://{service.Address}:{service.Port}{requestUrl}");

        return uri;
    }
}