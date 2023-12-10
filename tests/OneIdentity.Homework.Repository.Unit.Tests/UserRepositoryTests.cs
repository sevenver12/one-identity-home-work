using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Database.Entities;
using Xunit.Abstractions;
using Xunit.Microsoft.DependencyInjection.Abstracts;
namespace OneIdentity.Homework.Repository.Unit.Tests;

[UsesVerify]
public class UserRepositoryTests : TestBed<Startup>
{
    private readonly ILogger<UserRepository> _logger;
    private readonly EfContext _context;
    private readonly Mock<TimeProvider> _mockTimeProvider = new();

    public UserRepositoryTests(ITestOutputHelper testOutputHelper, Startup fixture) : base(testOutputHelper, fixture)
    {
        _logger = _fixture.GetService<ILogger<UserRepository>>(testOutputHelper)!;
        _context = _fixture.GetService<EfContext>(_testOutputHelper)!;
    }

    public UserRepository CreateSut()
    {
        return new UserRepository(_logger, _context, _mockTimeProvider.Object);
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

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

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

        // Act
        var result = await sut.GetUserByIdAsync(id);

        //Assert
        await Verify(result);
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

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var result = await sut.GetUserByIdAsync(users.First().Id);

        //Assert
        await Verify(result);
    }

    [Fact]
    public async Task DeleteUserAsync_ValidData_ShouldReturnTrue()
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

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var result = await sut.DeleteUserAsync(users.First().Id);

        //Assert
        await Verify(result);
    }

    [Fact]
    public async Task CreateUserAsync_DuplicateUser_ShouldReturnNull()
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

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var createUser = new Models.User.CreateUser
        {
            Id = users.First().Id,
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Password = "password",
            UserName = "name2",
            Nickname = "name2",
        };

        // Act
        var result = await sut.CreateUserAsync(createUser);

        //Assert
        await Verify(result);
    }

    [Fact]
    public async Task CreateUserAsync_ValidUser_ShouldReturnCreatedUser()
    {
        // Arrange
        var sut = CreateSut();

        var createUser = new Models.User.CreateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Password = "password",
            UserName = "name2",
            Nickname = "name2",
        };
        var createdAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(2));
        _mockTimeProvider.Setup(s => s.GetUtcNow()).Returns(createdAt);

        // Act
        var result = await sut.CreateUserAsync(createUser);

        //Assert
        await Verify(result).AddNamedDateTimeOffset(createdAt, nameof(User.CreatedAt));
    }

    [Fact]
    public async Task UpdateUserAsync_UserInDb_ShouldReturnUpdatedUser()
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

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var updateUser = new Models.User.UpdateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Nickname = "name2",
        };

        var updatedAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(2));
        _mockTimeProvider.Setup(s => s.GetUtcNow()).Returns(updatedAt);

        // Act
        var result = await sut.UpdateUserAsync(users.First().Id, updateUser);

        //Assert
        var usersInDb = await _context.Users.ToListAsync();
        await Verify(new { usersInDb, updateResult = result }).AddNamedDateTimeOffset(updatedAt, nameof(User.UpdatedAt));
    }

    [Fact]
    public async Task UpdateUserAsync_UserNotInDb_ShouldReturnNull()
    {
        // Arrange
        var sut = CreateSut();

        var updateUser = new Models.User.UpdateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Nickname = "name2",
        };

        var updatedAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(2));
        _mockTimeProvider.Setup(s => s.GetUtcNow()).Returns(updatedAt);

        // Act
        var result = await sut.UpdateUserAsync(Guid.NewGuid(), updateUser);

        //Assert
        await Verify(result);
    }
}