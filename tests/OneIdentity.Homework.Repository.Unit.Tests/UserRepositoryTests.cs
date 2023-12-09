
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.EntityFrameworkCore;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Database.Entities;
using System.Runtime.CompilerServices;
using Xunit;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;

namespace OneIdentity.Homework.Repository.Unit.Tests;

[UsesVerify]
public class UserRepositoryTests : TestBed<Startup>
{
    private readonly ILogger<UserRepository> _logger;
    private readonly Mock<EfContext> _mockContext = new();
    private readonly Mock<TimeProvider> _mockTimeProvider = new();

    public UserRepositoryTests(ITestOutputHelper testOutputHelper, Startup fixture) : base(testOutputHelper, fixture)
    {
        _logger = _fixture.GetService<ILogger<UserRepository>>(testOutputHelper)!;
    }

    public UserRepository CreateSut()
    {
        return new UserRepository(_logger, _mockContext.Object, _mockTimeProvider.Object);
    }

    [Fact]
    public async Task GetPageOfUsersAsync_ValidData_ShouldReturnWithUsers()
    {
        // Arrange
        var sut = CreateSut();
        var pageSize = 2;
        var pageNumber = 0;
        List<User> users =
        [
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "someemail@test.com",
                Password = "password",
                UserName = "name",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name",
            },
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "notjustanyemail@test.com",
                Password = "password",
                UserName = "name2",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name2",
            },

        ];

        _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

        // Act
        var result = await sut.GetPageOfUsersAsync(pageSize, pageNumber);

        //Assert
        await Verify(result);

    }

    [Fact]
    public async Task GetPageOfUsersAsync_NoUsersProvided_ShouldReturnEmpty()
    {
        // Arrange
        var sut = CreateSut();
        var pageSize = 2;
        var pageNumber = 0;
        List<User> users = [];

        _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

        // Act
        var result = await sut.GetPageOfUsersAsync(pageSize, pageNumber);

        //Assert
        await Verify(result);
    }

    [Fact]
    public async Task GetUserByIdAsync_NoUsersProvided_ShouldReturnEmpty()
    {
        // Arrange
        var sut = CreateSut();
        var id = Guid.NewGuid();
        List<User> users = [];

        _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

        // Act
        var result = await sut.GetUserByIdAsync(id);

        //Assert
        await Verify();
    }

    [Fact]
    public async Task GetUserByIdAsync_ValidData_ShouldReturnWithUser()
    {
        // Arrange
        var sut = CreateSut();
        List<User> users =
        [
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "someemail@test.com",
                Password = "password",
                UserName = "name",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name",
            },
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "notjustanyemail@test.com",
                Password = "password",
                UserName = "name2",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name2",
            },

        ];

        _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

        // Act
        var result = await sut.GetUserByIdAsync(users.First().Id);

        //Assert
        await Verify(result);
    }

    [Fact]
    public async Task DeleteUserAsync_ValidData_ShouldReturnWithUser()
    {
        // Arrange
        var sut = CreateSut();
        List<User> users =
        [
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "someemail@test.com",
                Password = "password",
                UserName = "name",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name",
            },
            new User()
            {
                BirthDate = DateTimeOffset.Now,
                Email = "notjustanyemail@test.com",
                Password = "password",
                UserName = "name2",
                CreatedAt = DateTimeOffset.Now,
                Nickname = "name2",
            },

        ];

        _mockContext.Setup(x => x.Users).ReturnsDbSet(users);

        // Act
        var result = await sut.DeleteUserAsync(users.First().Id);

        //Assert
        await Verify(result);
    }
}