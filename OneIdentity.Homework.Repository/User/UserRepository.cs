using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver.Linq;
using OneIdentity.Homework.Database;
using OneIdentity.Homework.Repository.Extensions;
using OneIdentity.Homework.Repository.Extensions.Mapper;
using OneIdentity.Homework.Repository.Models;
using OneIdentity.Homework.Repository.Models.User;
namespace OneIdentity.Homework.Repository.Users;

public class UserRepository
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

    public async Task<PagedCollection<User>> GetAllUsers(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        return await _efContext.Users.ProjectToToDto().ToPagedCollectionAsync(pageSize, pageNumber, cancellationToken);
    }

    public async Task<User?> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _efContext.Users.ProjectToToDto().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        return (await _efContext.Users.Where(user => user.Id == id).ExecuteDeleteAsync(cancellationToken)) == 1;
    }

    public async Task<User> CreateUser(CreateUser user, CancellationToken cancellationToken = default)
    {
        var userTracker = _efContext.Users.Add(user.ToEntity(_timeProvider));
        await _efContext.SaveChangesAsync(cancellationToken);
        return userTracker.Entity.ToDto();
    }

    public async Task<User?> UpdateUser(Guid id, UpdateUser user, CancellationToken cancellationToken = default)
    {
        var userEntity = await _efContext.Users.FirstOrDefaultAsync(user => user.Id == id);

        if (userEntity == null)
        {
            _logger.LogWarning("User {UserId} wasn't found while attempting update", id);
            return null;
        }

        user.UpdateUserToUserEntity(userEntity, _timeProvider);
        await _efContext.SaveChangesAsync(cancellationToken);
        return userEntity.ToDto();
    }

}
