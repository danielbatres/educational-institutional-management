using edu_institutional_management.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class MainContext : DbContext {
    public DbSet<UserDto> Users { get; set; }
    public DbSet<InstitutionDto> Institutions { get; set; }
    public DbSet<RegisterDto> Registers { get; set; }
    public DbSet<OnlineStatusDto> OnlineStatuses { get; set; }

    public MainContext(DbContextOptions<MainContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        Guid idFirstInstitution = Guid.NewGuid();
       
        List<InstitutionDto> InstitutionsInit = new() {
            new InstitutionDto() {
                Id = idFirstInstitution,
                Name = "Institution 1",
                Address = "local",
                PhoneNumber = "222333"
            },
            new InstitutionDto() {
                Id = Guid.NewGuid(),
                Name = "Institution 2",
                Address = "local",
                PhoneNumber = "34663"
            }
        };

        modelBuilder.Entity<InstitutionDto>(institution => {
            institution.HasKey(x => x.Id);
            institution.Property(x => x.Name);
            institution.Property(x => x.Address);
            institution.Property(x => x.PhoneNumber);
            institution.HasMany(x => x.Users).WithOne(user => user.Institution).HasForeignKey(user => user.InstitutionId);
            institution.HasData(InstitutionsInit);
        });

        modelBuilder.Entity<RegisterDto>(register => {
            register.HasKey(x => x.Id);
            register.Property(x => x.Email);
            register.Property(x => x.Password);
            register.Property(x => x.AuthenticationMethod);
        });

        modelBuilder.Entity<OnlineStatusDto>(onlineStatus => {
            onlineStatus.HasKey(x => x.Id);
            onlineStatus.Property(x => x.LastConnection);
            onlineStatus.Property(x => x.Status);
        });

        List<UserDto> UsersInit = new() {
            new UserDto() {
                Id = Guid.NewGuid(),
                Name = "First user",
                LastName = "Last name",
                Age = 25,
                BirthDate = DateTime.Now,
                Bio = "Bio",
                InstitutionId = idFirstInstitution,
                PhoneNumber = ""
            }
        };

        modelBuilder.Entity<UserDto>(user => {
            user.HasKey(x => x.Id);
            user.Property(x => x.Name).HasMaxLength(100);
            user.Property(x => x.LastName);
            user.Property(x => x.BirthDate);
            user.HasOne(x => x.Register).WithOne(register => register.User).HasForeignKey<RegisterDto>(register => register.UserId);
            user.HasOne(x => x.OnlineStatus).WithOne(online => online.User).HasForeignKey<OnlineStatusDto>(online => online.UserId);
            user.HasData(UsersInit);
        });
    }
}