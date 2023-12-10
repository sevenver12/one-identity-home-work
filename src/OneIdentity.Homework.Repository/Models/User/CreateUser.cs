namespace OneIdentity.Homework.Repository.Models.User;
public class CreateUser
{

    /// <summary>
    /// Id of the user
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// User name of the user
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Password of the user
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Email of the user
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Birth Date of the user
    /// </summary>
    public required DateTimeOffset BirthDate { get; set; }

    /// <summary>
    /// Phone number of the user
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// An optional Nickname
    /// </summary>
    public string? Nickname { get; set; }
}
