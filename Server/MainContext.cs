using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server
{
    public class MainContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<OnlineStatus> OnlineStatuses { get; set; }

        public MainContext(DbContextOptions<MainContext> options) : base(options) {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Institution>(institution => {
                institution.HasKey(x => x.Id);
                institution.Property(x => x.Name);
                institution.Property(x => x.Address);
                institution.Property(x => x.PhoneNumber);
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

            modelBuilder.Entity<User>(user => {
                user.HasKey(x => x.Id);
                user.Property(x => x.Name).HasMaxLength(100);
                user.Property(x => x.LastName);
                user.Property(x => x.BirthDate);
                user.HasOne(u => u.Register).WithOne(r => r.User).HasForeignKey<Register>(r => r.UserId);
                user.HasOne(u => u.Institution).WithMany(i => i.Users).HasForeignKey(u => u.InstitutionId);
                user.HasOne(u => u.OnlineStatus).WithOne(e => e.User).HasForeignKey<OnlineStatus>(e => e.UserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}