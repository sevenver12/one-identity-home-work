using Riok.Mapperly.Abstractions;

namespace OneIdentity.Homework.Repository.Extensions.Mapper;

/// <summary>
/// Source generated mapper
/// </summary>
[Mapper]
public static partial class ProjectionMapper
{
    //Projections are not supported in EF mongodb driver sadly
    public static partial IQueryable<Models.User.User> ProjectToToDto(this IQueryable<Database.Entities.User> user);
}
