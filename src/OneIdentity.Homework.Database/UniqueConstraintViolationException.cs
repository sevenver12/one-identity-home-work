using Microsoft.EntityFrameworkCore;

namespace OneIdentity.Homework.Database;

/// <summary>
/// Unified Constraint validation exception for EF
/// </summary>
public class UniqueConstraintViolationException : DbUpdateException
{
    public UniqueConstraintViolationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
