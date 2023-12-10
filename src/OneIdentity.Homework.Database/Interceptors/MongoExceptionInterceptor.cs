using Microsoft.EntityFrameworkCore.Diagnostics;

namespace OneIdentity.Homework.Database.Interceptors;

/// <summary>
/// Processes mongodb exceptions to a unified exception to be used
/// </summary>
public class MongoExceptionInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        if (eventData.Exception.GetBaseException() is MongoDB.Driver.MongoBulkWriteException mongoEx)
        {
            if (mongoEx.WriteErrors.Count == 1)
            {
                var firstWriteError = mongoEx.WriteErrors.First();

                throw firstWriteError switch
                {
                    { Category: MongoDB.Driver.ServerErrorCategory.DuplicateKey } =>
                        new UniqueConstraintViolationException("Unique constraint violation", mongoEx),
                    _ => mongoEx
                };
            }
        }

        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}
