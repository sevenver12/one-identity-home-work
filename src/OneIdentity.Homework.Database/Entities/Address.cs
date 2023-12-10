namespace OneIdentity.Homework.Database.Entities;
public class Address
{
    public required string Street { get; set; }
    public required string Suite { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required Geo Geo { get; set; }
}
