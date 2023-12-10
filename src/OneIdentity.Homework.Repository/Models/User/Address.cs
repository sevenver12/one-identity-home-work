namespace OneIdentity.Homework.Repository.Models.User;
public class Address
{
    /// <summary>
    /// Street of the address
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Suite of the address
    /// </summary>
    public required string Suite { get; set; }

    /// <summary>
    /// City of the address
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Zip code of the address
    /// </summary>
    public required string ZipCode { get; set; }

    /// <summary>
    /// Geo data of the address
    /// </summary>
    public required Geo Geo { get; set; }
}
