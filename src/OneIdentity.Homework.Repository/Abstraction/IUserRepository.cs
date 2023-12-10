using OneIdentity.Homework.Repository.Models;
using OneIdentity.Homework.Repository.Models.User;

namespace OneIdentity.Homework.Repository.Abstraction;

/// <summary>
/// Crud repository for the <see cref="Database.Entities.User"/>
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Creates an user in the database
    /// </summary>
    /// <param name="user">User to be created</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>The created user</returns>
    /// <remarks>Returns null when there was a duplicate Id provided</remarks>
    Task<User?> CreateUserAsync(CreateUser user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an user with the given id
    /// </summary>
    /// <param name="id">Given id of the user</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>Result of the delete operation</returns>
    Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Queries a page of users
    /// </summary>
    /// <param name="pageSize">Number of elements in a page</param>
    /// <param name="pageNumber">The current page to be returned</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>A page of users</returns>
    Task<PagedCollection<User>> GetPageOfUsersAsync(int pageSize, int pageNumber, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Queries an user by provided id
    /// </summary>
    /// <param name="id">The provided id</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>The requested user</returns>
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Updates an user by the given id and update object
    /// </summary>
    /// <param name="id">Given id</param>
    /// <param name="user">Update object</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>The updated user</returns>
    /// <remarks>Returns null when user was not found</remarks>
    Task<User?> UpdateUserAsync(int id, UpdateUser user, CancellationToken cancellationToken = default);
}