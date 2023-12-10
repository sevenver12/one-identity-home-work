﻿using OneIdentity.Homework.Database.Entities;

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
    public string? Phone { get; set; }

    /// <summary>
    /// An optional Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Website of the user
    /// </summary>
    public string? Website { get; set; }

    /// <summary>
    /// Company of the user
    /// </summary>
    public Company? Company { get; set; }

    /// <summary>
    /// Address of the user
    /// </summary>
    public Address? Address { get; set; }
}
