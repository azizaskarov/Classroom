using Classroom.Dto.Context.Configurations;
using Classroom.Dto.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classroom.Dto.Context;

public class AppDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    { 
    }
    public DbSet<School> Schools { get; set; }
    public DbSet<UserSchool> UserSchools { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

       // new UserSchoolConfiguration().Configure(builder.Entity<UserSchool>());
       // builder.ApplyConfiguration(new UserSchoolConfiguration());

       builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<User>()
            .ToTable("users");

        builder.Entity<User>()
            .Property(user => user.FirstName)
            .HasColumnName("firstname")
            .HasMaxLength(50)
            .IsRequired();

        builder.Entity<User>()
            .Property(user => user.LastName)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Entity<User>()
            .Property(user => user.PhotoUrl)
            .HasColumnName("photo_url")
            .IsRequired(false);


    }
}