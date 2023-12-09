using Microsoft.Extensions.DependencyInjection;
using OneIdentity.Homework.Repository.Abstraction;

namespace OneIdentity.Homework.Repository.Extensions;
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the neccesary services to the service collection to use <see cref="OneIdentity.Homework.Repository"/>
    /// </summary>
    /// <param name="services">Service collection to be extended</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
