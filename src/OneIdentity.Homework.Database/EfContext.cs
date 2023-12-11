using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using OneIdentity.Homework.Database.Entities;

namespace OneIdentity.Homework.Database;

public class EfContext : DbContext
{
    public EfContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(u =>
        {
            u.OwnsOne(x => x.Address,c=>c.OwnsOne(address=>address.Geo));
            u.OwnsOne(x => x.Company).ToJson();
        });

        /// Can't really configure here and no way to tell ef how to generate Ids the recommended way in mongo is to use ObjectId or Guid so i chose Guid

    }
}
