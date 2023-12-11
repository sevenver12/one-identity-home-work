using DotNet.Testcontainers.Containers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Database.Entities;
using OneIdentity.Homework.ServiceDefaults;
using System.Net.Http.Json;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace OneIdentity.Homework.Api.Integration.Tests;

[UsesVerify]
public class UsersControllerTests : TestBed<Startup>, IAsyncLifetime
{
    private readonly IContainer _dbContainer;
    private readonly HomeWorkWebApplicationFactory<Program> _factory;
    private HttpClient? _client;
    public UsersControllerTests(ITestOutputHelper testOutputHelper, Startup fixture) : base(testOutputHelper, fixture)
    {
        _dbContainer = _fixture.GetServiceProvider(testOutputHelper).CreateAsyncScope()
            .ServiceProvider.GetKeyedService<IContainer>(TestConstants.DatabaseKey)!;

        _factory = _fixture.GetService<HomeWorkWebApplicationFactory<Program>>(testOutputHelper)!;
    }

    [Fact]
    public async Task GetPaged_EmptyDatabase_ShouldReturnEmpty()
    {
        // Arrange

        // Act
        var response = await _client!.GetAsync("/api/users");

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetPaged_ValidDataInDb_ShouldReturnUsers()
    {
        // Arrange
        await FillDatabaseAsync(GetTestUsers());

        // Act
        var response = await _client!.GetAsync("/api/users");

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetPaged_ValidDataInDbWithQuery_ShouldReturnUsers()
    {
        // Arrange
        await FillDatabaseAsync(GetTestUsers());

        // Act
        var response = await _client!.GetAsync("/api/users?PageSize=1&PageNumber=0");

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetById_ValidDataInDbWithQuery_ShouldReturnUser()
    {
        // Arrange
        var users = GetTestUsers();
        await FillDatabaseAsync(users);

        // Act
        var response = await _client!.GetAsync($"/api/users/{users.First().Id}/");

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetById_EmptyDb_ShouldReturnNotFound()
    {
        // Arrange

        // Act
        var response = await _client!.GetAsync($"/api/users/{Guid.NewGuid()}/");

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Post_EmptyDb_ShouldReturnCreatedUser()
    {
        // Arrange
        var updateUser = new Repository.Models.User.CreateUser
        {
            BirthDate = DateTimeOffset.Now,
            UserName = "name2",
            Email = "notjustanyemail@test.com",
            Name = "name2",
            Company = new Repository.Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cpt",
                Name = "Long Homework"
            },
            Address = new Repository.Models.User.Address
            {
                City = "Coty",
                Geo = new Repository.Models.User.Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                Street = "yes",
                Suite = "no",
                ZipCode = "maybe"
            },
            Phone = "5",
            Website = "5.com",
        };

        // Act
        var response = await _client!.PostAsJsonAsync($"/api/users/", updateUser);

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Put_EmptyDb_ShouldReturnNotFound()
    {
        // Arrange
        var updateUser = new Repository.Models.User.UpdateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Name = "name2",
            Company = new Repository.Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cpt",
                Name = "Long Homework"
            },
            Address = new Repository.Models.User.Address
            {
                City = "Coty",
                Geo = new Repository.Models.User.Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                Street = "yes",
                Suite = "no",
                ZipCode = "maybe"
            },
            Phone = "5",
            Website = "5.com",
        };

        // Act
        var response = await _client!.PutAsJsonAsync($"/api/users/{Guid.NewGuid()}/", updateUser);

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Put_UserInDb_ShouldReturnUpdatedUser()
    {
        // Arrange
        var users = GetTestUsers();
        await FillDatabaseAsync(users);

        var createUser = new Repository.Models.User.UpdateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Name = "name2",
            Company = new Repository.Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cpt",
                Name = "Long Homework"
            },
            Address = new Repository.Models.User.Address
            {
                City = "Coty",
                Geo = new Repository.Models.User.Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                Street = "yes",
                Suite = "no",
                ZipCode = "maybe"
            },
            Phone = "5",
            Website = "5.com",
        };

        // Act
        var response = await _client!.PutAsJsonAsync($"/api/users/{users.First().Id}/", createUser);

        // Assert
        await VerifyJson(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Delete_EmptyDb_ShouldReturnNotFound()
    {
        // Arrange

        // Act
        var response = await _client!.DeleteAsync($"/api/users/{Guid.NewGuid()}/");

        // Assert
        response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_UserInDb_ShouldReturnUpdatedUser()
    {
        // Arrange
        var users = GetTestUsers();
        await FillDatabaseAsync(users);

        // Act
        var response = await _client!.DeleteAsync($"/api/users/{users.First().Id}/");

        // Assert
        response.Should().BeSuccessful();
    }

    private async Task FillDatabaseAsync(List<User> users)
    {
        var efContext = _factory.Services.CreateAsyncScope().ServiceProvider.GetService<EfContext>()!;
        efContext.AddRange(users);
        await efContext.SaveChangesAsync();
        efContext.ChangeTracker.Clear();
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var port = _dbContainer.GetMappedPublicPort(_fixture.Configuration!.GetValue<int>(Constants.DatabaseContainerPortPath));
        _factory.ConfigureDbContainer(_dbContainer.Hostname, port);
        _client = _factory.CreateClient();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _dbContainer.DisposeAsync().AsTask();
    }

    private List<User> GetTestUsers() =>
    [
        new User()
        {
            BirthDate = DateTimeOffset.Now,
            Email = "someemail@test.com",
            UserName = "name",
            CreatedAt = DateTimeOffset.Now,
            Name= "name",
            Phone = "1234567890",
            Website = "www.something.com",
            Address = new Address
            {
                Geo = new Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                City = "1",
                Street = "2",
                Suite = "3",
                ZipCode = "4",
            },
            Company = new Company()
            {
                Bs = "slogan",
                CatchPhrase = "CatchPhrase",
                Name = "name",
            }
        },
        new User()
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Name = "name",
            UserName = "name2",
            CreatedAt = DateTimeOffset.Now,
            Phone = "1234567890",
            Website = "www.something.com",
            Address = new Address
            {
                Geo = new Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                City = "1",
                Street = "2",
                Suite = "3",
                ZipCode = "4",
            },
            Company = new Company()
            {
                Bs = "slogan",
                CatchPhrase = "CatchPhrase",
                Name = "name",
            }
        }
    ];
}