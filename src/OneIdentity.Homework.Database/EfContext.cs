using Microsoft.EntityFrameworkCore;
using OneIdentity.Homework.Database.Entities;
using OneIdentity.Homework.Database.Interceptors;

namespace OneIdentity.Homework.Database;

public class EfContext : DbContext
{
    public EfContext() { }
    public EfContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.AddInterceptors(new MongoExceptionInterceptor(), new InMemoryExceptionInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
