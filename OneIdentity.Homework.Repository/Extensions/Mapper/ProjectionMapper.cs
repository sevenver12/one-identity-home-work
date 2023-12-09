using Riok.Mapperly.Abstractions;

namespace OneIdentity.Homework.Repository.Extensions.Mapper;

[Mapper]
public static partial class ProjectionMapper
{
    public static partial IQueryable<Models.User.User> ProjectToToDto(this IQueryable<Database.Entities.User> user);
}
