﻿using Microsoft.EntityFrameworkCore;
using OneIdentity.Homework.Database.Models;

namespace OneIdentity.Homework.Database;

public class EfContext : DbContext
{
    public EfContext(){}
    public EfContext(DbContextOptions options) : base(options){}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}