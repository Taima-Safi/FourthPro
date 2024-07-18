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
    public DbSet<ProjectModel> Project { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder) // for relations
    {
        base.OnModelCreating(builder);

        // Configure the relationship between UserModel and ProjectModel
        //builder.Entity<UserModel>()
        //    .HasOne(u => u.FourthProject)
        //    .WithMany(p => p.Users)
        //    .HasForeignKey(u => u.FourthProjectId)
        //    .OnDelete(DeleteBehavior.Restrict);

        //builder.Entity<UserModel>()
        //    .HasOne(u => u.FifthProject)
        //    .WithMany(p => p.Users)
        //    .HasForeignKey(u => u.FifthProjectId)
        //    .OnDelete(DeleteBehavior.Restrict);
    }
}