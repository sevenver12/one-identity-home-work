using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.Linq;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Repository.Abstraction;
using OneIdentity.Homework.Repository.Extensions;
using OneIdentity.Homework.Repository.Extensions.Mapper;
using OneIdentity.Homework.Repository.Models;
using OneIdentity.Homework.Repository.Models.User;
using System.Diagnostics;
namespace OneIdentity.Homework.Repository;

/// <inheritdoc/>
public class UserRepository : IUserRepository
{
    private readonly ILogger<UserRepository> _logger;
    private readonly EfContext _efContext;
    private readonly TimeProvider _timeProvider;

    public UserRepository(ILogger<UserRepository> logger, EfContext efContext, TimeProvider timeProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _efContext = efContext ?? throw new ArgumentNullException(nameof(efContext));
        _timeProvider = timeProvider;
    }

    ///<inheritdoc/>
    public async Task<PagedCollection<User>> GetPageOfUsersAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        var users = await _efContext.Users.ApplyPaging(pageSize, pageNumber).ToListAsync(cancellationToken);
        return users.ToDto().ToPagedCollection(pageNumber);
    }

    ///<inheritdoc/>
    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _efContext.Users.FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        if (user is null)
        {
            return null;
        }
        return user.ToDto();
    }

    ///<inheritdoc/>
    public async Task<bool> DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await _efContext.Users.FirstOrDefaultAsync(usr => usr.Id == id);

        if (user == null)
        {
            return false;
        }
        _efContext.Users.Remove(user);
        await _efContext.SaveChangesAsync();
        //TODO: this should return 1 stating how many rows were affected,
        //but the mongodb driver is just not working yet so a query is needed here temporarily and above
        var deletedUser = await _efContext.Users.FirstOrDefaultAsync(usr => usr.Id == id);
        return deletedUser == null;

    }

    ///<inheritdoc/>
    public async Task<User?> CreateUserAsync(CreateUser user, CancellationToken cancellationToken = default)
    {
        try
        {
            var userTracker = _efContext.Users.Add(user.ToEntity(_timeProvider));
            await _efContext.SaveChangesAsync(cancellationToken);
            return userTracker.Entity.ToDto();
        }
        catch (UniqueConstraintViolationException e)
        {
            _logger.LogInformation(e, "User {UserId} couldn't be created because an user exists with the same Id already", user.Id);
            return null;
        }
        throw new UnreachableException();
    }

    ///<inheritdoc/>
    public async Task<User?> UpdateUserAsync(Guid id, UpdateUser user, CancellationToken cancellationToken = default)
    {
        var userEntity = await _efContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (userEntity is null)
        {
            _logger.LogInformation("User {UserId} wasn't found while attempting update", id);
            return null;
        }

        user.UpdateUserToUserEntity(userEntity, _timeProvider);
        await _efContext.SaveChangesAsync(cancellationToken);
        return userEntity.ToDto();
    }

}
