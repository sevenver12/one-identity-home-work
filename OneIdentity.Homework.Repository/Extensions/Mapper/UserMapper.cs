using Riok.Mapperly.Abstractions;

namespace OneIdentity.Homework.Repository.Extensions.Mapper;

[Mapper]
public static partial class UserMapper
{
    public static Database.Entities.User ToEntity(this Models.User.CreateUser user, TimeProvider timeProvider)
    {
        var entity = user.ToEntity();
        entity.CreatedAt = timeProvider.GetUtcNow();
        return entity;
    }

    public static void UpdateUserToUserEntity(this Models.User.UpdateUser updateUser, Database.Entities.User user, TimeProvider timeProvider)
    {
        updateUser.UpdateUserToUserEntity(user);
        user.UpdatedDate = timeProvider.GetUtcNow();
    }

    public static partial Models.User.User ToDto(this Database.Entities.User user);

    private static partial Database.Entities.User ToEntity(this Models.User.CreateUser user);
    private static partial void UpdateUserToUserEntity(this Models.User.UpdateUser updateUser, Database.Entities.User user);
}
