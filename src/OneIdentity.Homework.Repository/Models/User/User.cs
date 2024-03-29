﻿namespace OneIdentity.Homework.Repository.Models.User;
public class User
{
    /// <summary>
    /// Id of the user
    /// </summary>
    public Guid Id { get; set; }

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
    public Company? Company { get; set; }

    /// <summary>
    /// Address of the user
    /// </summary>
    public Address? Address { get; set; }
}
