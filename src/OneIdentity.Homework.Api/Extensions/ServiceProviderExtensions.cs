using Microsoft.Extensions.ServiceDiscovery.Abstractions;
using Microsoft.Extensions.ServiceDiscovery.Http;
using Microsoft.Extensions.ServiceDiscovery;
using OneIdentity.Homework.ServiceDefaults;

namespace OneIdentity.Homework.Api.Extensions;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Resolves the mongodb url using ServiceDiscovery
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    /// This method is sadly neccesary as the preview mongodb service provider (or simply the mongo driver) doesn't support EndpointResolution added with 
    /// <see cref="HostingExtensions.AddServiceDiscovery(IServiceCollection)"/>
    /// </remarks>
    public static string ResolveMongoDbConnectionString(this IServiceProvider sp, WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("mongoDb")!;
        var timeProvider = sp.GetService<TimeProvider>() ?? TimeProvider.System;
        var selectorProvider = sp.GetRequiredService<IServiceEndPointSelectorProvider>();
        var resolverProvider = sp.GetRequiredService<ServiceEndPointResolverFactory>();
        var registry = new HttpServiceEndPointResolver(resolverProvider, selectorProvider, timeProvider);
        var resolvedEndpoint = registry.GetEndpointAsync(new HttpRequestMessage(HttpMethod.Get, $"http://{Constants.Mongo}"), default)
            .GetAwaiter().GetResult();
        var resolvedConnectionString = connString.Replace(Constants.Mongo, resolvedEndpoint.GetEndPointString());
        return resolvedConnectionString;
    }
}
