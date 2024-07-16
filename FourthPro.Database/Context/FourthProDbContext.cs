using FourthPro.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace FourthPro.Database.Context;

public class FourthProDbContext : DbContext
{
    public FourthProDbContext()
    {

    }
    public FourthProDbContext(DbContextOptions<FourthProDbContext> options) : base(options)
    {
    }
    public DbSet<UserModel> User { get; set; }
    public DbSet<DoctorModel> Doctor { get; set; }
    public DbSet<DepartmentModel> Department { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder) // for relations
    {
        base.OnModelCreating(builder);
    }
}