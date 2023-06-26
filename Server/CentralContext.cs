using edu_institutional_management.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class CentralContext : DbContext {
    public DbSet<User> Users { get; set; }
    
    public CentralContext(DbContextOptions<CentralContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Institution>(institution => {
            institution.HasKey(x => x.Id);
            institution.Property(x => x.Name);
            institution.Property(x => x.Address);
            institution.Property(x => x.PhoneNumber);
        });

        modelBuilder.Entity<User>(user => {
            user.HasKey(x => x.Id);
            user.Property(x => x.Name).HasMaxLength(100);
            user.Property(x => x.LastName);
            user.Property(x => x.BirthDate);
            user.HasOne(x => x.Register).WithOne(register => register.User).HasForeignKey<Register>(register => register.UserId);
        });

        modelBuilder.Entity<Register>(register => {
            register.HasKey(x => x.Id);
            register.Property(x => x.Email);
            register.Property(x => x.Password);
            register.Property(x => x.AuthenticationMethod);
        });

        modelBuilder.Entity<OnlineStatus>(onlineStatus => {
            onlineStatus.HasKey(x => x.Id);
            onlineStatus.Property(x => x.LastConnection);
            onlineStatus.Property(x => x.Status);
        });
    }
}