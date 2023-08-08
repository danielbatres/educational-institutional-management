using edu_institutional_management.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class CentralContext : DbContext {
    public DbSet<User> Users { get; set; }
    public DbSet<Institution> Institutions { get; set; }
    public DbSet<Register> Registers { get; set; }
    public DbSet<OnlineStatus> OnlineStatuses { get; set; }

    public CentralContext(DbContextOptions<CentralContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        List<Institution> InstitutionsInit = new List<Institution>();

        InstitutionsInit.Add(new Institution() {
            Id = Guid.NewGuid(),
            Name = "Institution 1",
            Address = "local",
            PhoneNumber = "222333"
        });

        InstitutionsInit.Add(new Institution() {
            Id = Guid.NewGuid(),
            Name = "Institution 2",
            Address = "local",
            PhoneNumber = "34663"
        });

        modelBuilder.Entity<Institution>(institution => {
            institution.HasKey(x => x.Id);
            institution.Property(x => x.Name);
            institution.Property(x => x.Address);
            institution.Property(x => x.PhoneNumber);
            institution.HasMany(x => x.Users).WithOne(user => user.Institution).HasForeignKey(user => user.InstitutionId);
            institution.HasData(InstitutionsInit);
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

        List<User> UsersInit = new List<User>();

        UsersInit.Add(new User() {
            Id = Guid.NewGuid(),
            Name = "First user",
            LastName = "Last name",
            Age = 25,
            BirthDate = DateTime.Now,
        });

        modelBuilder.Entity<User>(user => {
            user.HasKey(x => x.Id);
            user.Property(x => x.Name).HasMaxLength(100);
            user.Property(x => x.LastName);
            user.Property(x => x.BirthDate);
            user.HasOne(x => x.Register).WithOne(register => register.User).HasForeignKey<Register>(register => register.UserId);
            user.HasOne(x => x.OnlineStatus).WithOne(online => online.User).HasForeignKey<OnlineStatus>(online => online.UserId);
        });
    }
}