using System.IO.Compression;
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
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MembershipRequest> MembershipRequests { get; set; }
 
        public MainContext(DbContextOptions<MainContext> options) : base(options) {
            ChangeTracker.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            List<PaymentType> PaymentTypes = new() {
                new PaymentType() {
                    Id = Guid.NewGuid(),
                    IndexNumber = 1,
                    Name = "Plan de pago 1",
                    Description = "Descripcion del plan de pago 1",
                    Amount = 0
                },
                new PaymentType() {
                    Id = Guid.NewGuid(),
                    IndexNumber = 2,
                    Name = "Plan de pago 2",
                    Description = "Descripcion del plan de pago 2",
                    Amount = 0
                },
                new PaymentType() {
                    Id = Guid.NewGuid(),
                    IndexNumber = 3,
                    Name = "Plan de pago 3",
                    Description = "Descripcion del plan de pago 3",
                    Amount = 0
                },
            };
            
            modelBuilder.Entity<Institution>(institution => {
                institution.HasKey(x => x.Id);
                institution.Property(x => x.Name);
                institution.Property(x => x.Address);
                institution.Property(x => x.PhoneNumber);
                institution.Property(x => x.IsActive);
                institution.Property(x => x.RegisteredDate);
                institution.Property(x => x.DataBaseConnectionName);
                institution.Property(x => x.WebSite);
            });

            modelBuilder.Entity<Register>(register => {
                register.HasKey(x => x.Id);
                register.Property(x => x.Email).IsRequired();
                register.Property(x => x.Password);
                register.Property(x => x.AuthenticationMethod);
                register.HasIndex(x => x.Email).IsUnique();
            });

            modelBuilder.Entity<OnlineStatus>(onlineStatus => {
                onlineStatus.HasKey(x => x.Id);
                onlineStatus.Property(x => x.LastConnection);
                onlineStatus.Property(x => x.Status);
            });

            modelBuilder.Entity<PaymentType>(paymentType => {
                paymentType.HasKey(x => x.Id);
                paymentType.Property(x => x.IndexNumber);
                paymentType.Property(x => x.Name);
                paymentType.Property(x => x.Description);
                paymentType.Property(x => x.Amount);
                paymentType.HasMany(pt => pt.Payments).WithOne(p => p.PaymentType).HasForeignKey(p => p.PaymentTypeId).IsRequired();
                paymentType.HasData(PaymentTypes);
            });

            modelBuilder.Entity<Payment>(payment => {
                payment.HasKey(x => x.Id);
                payment.Property(x => x.RegisterDate);
                payment.Property(x => x.EndDate);
                payment.HasOne(p => p.PaymentType).WithMany(pt => pt.Payments).IsRequired();
                payment.HasOne(p => p.User).WithOne(u => u.Payment).HasForeignKey<Payment>(p => p.UserId).IsRequired(false);
            });

            modelBuilder.Entity<User>(user => {
                user.HasKey(x => x.Id);
                user.Property(x => x.UserName).IsRequired().HasMaxLength(100);
                user.HasIndex(x => x.UserName).IsUnique();
                user.Property(x => x.Name).HasMaxLength(100);
                user.Property(x => x.LastName).HasMaxLength(100);
                user.Property(x => x.BirthDate);
                user.Property(x => x.Bio).HasMaxLength(1024);
                user.Property(x => x.Location);
                user.Property(x => x.Photo);
                user.HasOne(u => u.Register).WithOne(r => r.User).HasForeignKey<Register>(r => r.UserId);
                user.HasOne(u => u.Institution).WithMany(i => i.Users).HasForeignKey(u => u.InstitutionId);
                user.HasOne(u => u.OnlineStatus).WithOne(e => e.User).HasForeignKey<OnlineStatus>(e => e.UserId);
            });

            modelBuilder.Entity<MembershipRequest>(request => {
                request.HasKey(x => x.Id);
                request.Property(x => x.Author).IsRequired();
                request.Property(x => x.Message);
                request.Property(x => x.CreationDate);
                request.Property(x => x.IsAccepted);
                request.Property(x => x.InstitutionName).IsRequired();
                request.HasOne(x => x.ReceiverUser).WithMany(u => u.ReceivedMembershipRequests).HasForeignKey(x => x.ReceiverUserId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}