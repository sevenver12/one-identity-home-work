using Microsoft.EntityFrameworkCore;
using OneIdentity.Homework.Database.Entities;

namespace OneIdentity.Homework.Database;

public class EfContext : DbContext
{
    public EfContext(){}
    public EfContext(DbContextOptions options) : base(options){}

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
