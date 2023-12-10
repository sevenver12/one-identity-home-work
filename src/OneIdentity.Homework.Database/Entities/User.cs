using System.Diagnostics.CodeAnalysis;

namespace OneIdentity.Homework.Database.Entities;
public class User
{
    public User()
    {
        
    }

    /// <summary>
    /// Id of the user
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the user
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// User name of the user
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Email of the user
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Birth Date of the user
    /// </summary>
    public required DateTimeOffset BirthDate { get; set; }

    /// <summary>
    /// Website of the user
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Phone number of the user
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// When the user was created
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// When the user was Updated
    /// </summary>
    public DateTimeOffset? UpdatedAt { get; set; }
    
    /// <summary>
    /// Company of the user
    /// </summary>
    public required Company Company { get; set; }

    /// <summary>
    /// Address of the user
    /// </summary>
    public required Address Address { get; set; }

}
