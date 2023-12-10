using Microsoft.EntityFrameworkCore.Diagnostics;
namespace OneIdentity.Homework.Database.Interceptors;

/// <summary>
/// Processes mongodb exceptions to a unified exception to be used
/// </summary>
public class InMemoryExceptionInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc/>
    public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        if (eventData.Exception.GetBaseException() is ArgumentException inMemoryException)
        {
            throw inMemoryException switch
            {
                { Message: var msg } when msg.StartsWith("An item with the same key has already been added") =>
                    new UniqueConstraintViolationException("Unique constraint violation", inMemoryException),
                _ => inMemoryException
            };

        }

        return base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}
