using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using OneIdentity.Homework.Database.Entities;

namespace OneIdentity.Homework.Database;

public class EfContext : DbContext
{
    public EfContext() { }
    public EfContext(DbContextOptions options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(u =>
        {
            u.OwnsOne(x => x.Address,c=>c.OwnsOne(address=>address.Geo));
            u.OwnsOne(x => x.Company).ToJson();
        });
        //modelBuilder.Entity<Address>(a=>
        //{
        //    a.OwnsOne(x => x.Geo).ToJson();
        //});
        //modelBuilder.Entity<Company>();
        //modelBuilder.Entity<Geo>();
        /// Can't really configure here and no way to tell ef how to generate Ids the recommended way in mongo is to use ObjectId or Guid so i chose Guid

    }
}
