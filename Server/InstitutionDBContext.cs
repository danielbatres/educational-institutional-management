using edu_institutional_management.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class InstitutionDBContext : DbContext {
    public InstitutionDBContext(DbContextOptions<InstitutionDBContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Institution>(institution => {
            institution.ToTable("Institution");
            institution.HasKey(x => x.Id);
            institution.Property(x => x.Name);
            institution.Property(x => x.Address);
            institution.Property(x => x.PhoneNumber);
        });

        modelBuilder.Entity<User>(user => {
            user.ToTable("User");
            user.HasKey(x => x.Id);
            user.Property(x => x.Name);
            user.Property(x => x.LastName);
            user.Property(x => x.BirthDate);
        });

        modelBuilder.Entity<Register>(register => {
            register.ToTable("Register");
            register.HasKey(x => x.Id);
            register.Property(x => x.Email);
            register.Property(x => x.Password);
            register.Property(x => x.AuthenticationMethod);
        });

        modelBuilder.Entity<OnlineStatus>(onlineStatus => {
            onlineStatus.ToTable("OnlineStatus");
            onlineStatus.HasKey(x => x.Id);
            onlineStatus.Property(x => x.LastConnection);
            onlineStatus.Property(x => x.Status);
        });
    }
}