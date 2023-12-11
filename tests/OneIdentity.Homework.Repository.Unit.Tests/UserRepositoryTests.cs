using FluentAssertions;
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

    public UserRepositoryTests(ITestOutputHelper testOutputHelper, Startup fixture) : base(testOutputHelper, fixture)
    {
        _logger = _fixture.GetService<ILogger<UserRepository>>(testOutputHelper)!;
        _context = _fixture.GetService<EfContext>(_testOutputHelper)!;
    }

    [Fact]
    public void Constructor_NullLogger_ShouldThrow()
    {
        // Arrange

        // Act
        var act = () => new UserRepository(null, _context, _mockTimeProvider.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("logger");
    }

    [Fact]
    public void Constructor_NullContext_ShouldThrow()
    {
        // Arrange

        // Act
        var act = () => new UserRepository(_logger, null, _mockTimeProvider.Object);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("efContext");
    }

    [Fact]
    public void Constructor_NullTimeProvider_ShouldThrow()
    {
        // Arrange

        // Act
        var act = () => new UserRepository(_logger, _context, null);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("timeProvider");
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
        List<User> users = GetTestUsers();

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var result = await sut.GetPageOfUsersAsync(pageSize, pageNumber);

        //Assert
        await Verify(result);

    }

    [Fact]
    public async Task GetPageOfUsersAsync_ValidDataWith0PageSize_ShouldReturnWithUsers()
    {
        // Arrange
        var sut = CreateSut();
        var pageSize = 0;
        var pageNumber = 0;
        List<User> users = GetTestUsers();

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
        List<User> users = GetTestUsers();

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
        List<User> users = GetTestUsers();

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var result = await sut.DeleteUserAsync(users.First().Id);

        //Assert
        var usersInDb = await _context.Users.ToListAsync();
        await Verify(new { usersInDb, deletedUser = result });
    }

    [Fact]
    public async Task DeleteUserAsync_NoUserInDb_ShouldReturnFalse()
    {
        // Arrange
        var sut = CreateSut();
        // Act
        var result = await sut.DeleteUserAsync(Guid.NewGuid());

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
            UserName = "name2",
            Name = "name2",
            Company = new Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cpt",
                Name = "Long Homework"
            },
            Address = new Models.User.Address
            {
                City = "Coty",
                Geo = new Models.User.Geo
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
        List<User> users = GetTestUsers();

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        var updateUser = new Models.User.UpdateUser
        {
            BirthDate = DateTimeOffset.Now,
            Email = "notjustanyemail@test.com",
            Name = "name2",
            Address = new Models.User.Address
            {
                City = "Coty",
                Geo = new Models.User.Geo
                {
                    Lat = 1,
                    Lng = 2,
                },
                Street = "yes",
                Suite = "no",
                ZipCode = "maybe"
            },
            Company = new Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cp",
                Name = "name"
            }
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
            Name = "name2",
            Address = new Models.User.Address()
            {
                City = "City",
                Street = "strt",
                Suite = "no",
                ZipCode = "yes",
                Geo = new Models.User.Geo
                {
                    Lat = 5,
                    Lng = 5
                }
            },
            Company = new Models.User.Company
            {
                Bs = "bs",
                CatchPhrase = "cp",
                Name = "name"
            }

        };

        var updatedAt = new DateTimeOffset(2000, 1, 1, 1, 1, 1, TimeSpan.FromHours(2));
        _mockTimeProvider.Setup(s => s.GetUtcNow()).Returns(updatedAt);

        // Act
        var result = await sut.UpdateUserAsync(Guid.NewGuid(), updateUser);

        //Assert
        await Verify(result);
    }
}