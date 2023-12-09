namespace OneIdentity.Homework.Repository.Models.User;
public class UpdateUser
{

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
