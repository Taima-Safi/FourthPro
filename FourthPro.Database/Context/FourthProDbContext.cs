using FourthPro.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Database.Context;

public class FourthProDbContext : DbContext
{
    public FourthProDbContext(DbContextOptions<FourthProDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder) // for relations
    {
        base.OnModelCreating(builder);
    }
    public DbSet<UserModel> User { get; set; }
}
