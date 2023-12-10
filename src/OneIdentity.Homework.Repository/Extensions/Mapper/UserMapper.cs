using Riok.Mapperly.Abstractions;

namespace OneIdentity.Homework.Repository.Extensions.Mapper;

/// <summary>
/// Source generated Mapper
/// </summary>
[Mapper]
public static partial class UserMapper
{
    /// <summary>
    /// Maps a <see cref="Models.User.CreateUser"/> to an <see cref="Database.Entities.User"/>
    /// </summary>
    /// <param name="user">Source User</param>
    /// <param name="timeProvider">Time provider to be used at the <see cref="Database.Entities.User.CreatedAt"/></param>
    /// <returns>Mapped Entity</returns>
    public static Database.Entities.User ToEntity(this Models.User.CreateUser user, TimeProvider timeProvider)
    {
        var entity = user.ToEntity();
        entity.CreatedAt = timeProvider.GetUtcNow();
        return entity;
    }

    /// <summary>
    /// MMaps the <paramref name="updateUser"/> to the <paramref name="user"/> to override it's properties
    /// </summary>
    /// <param name="updateUser">Source User</param>
    /// <param name="user">Target User</param>
    /// <param name="timeProvider">Time provider to be used at the <see cref="Database.Entities.User.UpdatedAt"/></param>
    public static void UpdateUserToUserEntity(this Models.User.UpdateUser updateUser, Database.Entities.User user, TimeProvider timeProvider)
    {
        updateUser.UpdateUserToUserEntity(user);
        user.UpdatedAt = timeProvider.GetUtcNow();
    }

    /// <summary>
    /// Maps an <see cref="Database.Entities.User"/> to an <see cref="Models.User.User"/>
    /// </summary>
    /// <param name="user">Source User</param>
    /// <returns>Mapped User</returns>
    public static partial Models.User.User ToDto(this Database.Entities.User user);

    /// <summary>
    /// Maps a collection of <see cref="Database.Entities.User"/> to a collection of <see cref="Models.User.User"/>
    /// </summary>
    /// <param name="user">Source Collection</param>
    /// <returns>Mapped Collection</returns>
    public static partial IEnumerable<Models.User.User> ToDto(this IEnumerable<Database.Entities.User> user);

    private static partial Database.Entities.User ToEntity(this Models.User.CreateUser user);
    private static partial void UpdateUserToUserEntity(this Models.User.UpdateUser updateUser, Database.Entities.User user);
}
