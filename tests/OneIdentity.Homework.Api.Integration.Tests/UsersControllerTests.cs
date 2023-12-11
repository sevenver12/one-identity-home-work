using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneIdentity.Homework.ServiceDefaults;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace OneIdentity.Homework.Api.Integration.Tests;

public class UsersControllerTests : TestBed<Startup>, IAsyncLifetime
{
    private readonly IContainer _dbContainer;
    private HomeWorkWebApplicationFactory<Program> _factory;

    public UsersControllerTests(ITestOutputHelper testOutputHelper, Startup fixture) : base(testOutputHelper, fixture)
    {
        _dbContainer = _fixture.GetServiceProvider(testOutputHelper).CreateAsyncScope()
            .ServiceProvider.GetKeyedService<IContainer>(TestConstants.DatabaseKey)!;

        _factory = _fixture.GetService<HomeWorkWebApplicationFactory<Program>>(testOutputHelper)!;

    }

    [Fact]
    public async void Test1()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/users");

        // Assert

    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var port = _dbContainer.GetMappedPublicPort(_fixture.Configuration!.GetValue<int>(Constants.DatabaseContainerPortPath));
        _factory.ConfigureDbContainer(_dbContainer.Hostname, port);
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.DisposeAsync().AsTask();
    }
}