namespace Order.Infrastructure.ServiceDiscovery;

public interface IServiceDiscoveryService
{
    Task<Uri> GetServiceUri(string serviceName, string requestUrl);
}