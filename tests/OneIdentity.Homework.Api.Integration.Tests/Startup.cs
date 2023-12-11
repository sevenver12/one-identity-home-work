using DotNet.Testcontainers.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OneIdentity.Homework.ServiceDefaults;
using Xunit.Microsoft.DependencyInjection;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace OneIdentity.Homework.Api.Integration.Tests;
public class Startup : TestBedFixture
{

    protected override void AddServices(IServiceCollection services, IConfiguration? configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.TryAddKeyedTransient(TestConstants.DatabaseKey, (sp, key) =>
        {
            var db = new ContainerBuilder().WithImage($"{configuration[Constants.DatabaseImagePath]}:{configuration[Constants.DatabaseTagPath]}")
                                               .WithPortBinding(configuration.GetValue<int>(Constants.DatabaseContainerPortPath), true);
            db = db.WithEnvironment(configuration.GetSection(Constants.DatabaseEnvsPath).GetChildren().ToDictionary(o => o.Key, o => o.Value));
            return db.Build();
        });

        services.TryAddTransient<HomeWorkWebApplicationFactory<Program>>();
    }

    protected override ValueTask DisposeAsyncCore()
        => new();

    protected override IEnumerable<TestAppSettings> GetTestAppSettings()
    {
        yield return new() { Filename = "appsettings.integration.json", IsOptional = false };
    }
}
