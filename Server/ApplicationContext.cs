using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class ApplicationContext : DbContext {
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Appearance> Appearances { get; set; }
    public DbSet<Settings> Settings { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<ActivityLog> Activities { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) {
        ChangeTracker.LazyLoadingEnabled = true;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        List<Appearance> AppearanceInit = new() {
            new() {
                Id = 1,
                Theme = AppereanceSelection.LightTheme
            },
            new() {
                Id = 2,
                Theme = AppereanceSelection.DarkTheme
            }
        };

        modelBuilder.Entity<Role>(role => {
            role.HasKey(t => t.Id);
            role.Property(t => t.Name).IsRequired();
            role.Property(r => r.Description);
            role.Property(r => r.RoleColor);
            role.Property(r => r.MembersCount);
            role.Property(r => r.PermissionsCount);
            role.HasMany(r => r.Permissions).WithOne(rp => rp.Role).HasForeignKey(rp => rp.RoleId);
        });

        modelBuilder.Entity<Permission>(permission => {
            permission.HasKey(t => t.Id);
            permission.Property(t => t.Name).IsRequired();
            permission.Property(t => t.Description);
            permission.HasMany(p => p.RolePermissions).WithOne(rp => rp.Permission).HasForeignKey(rp => rp.PermissionId);
        });

        modelBuilder.Entity<RolePermission>(rolePermission => {
            rolePermission.HasKey(rp => rp.Id);
            rolePermission.Property(rp => rp.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<UserRole>(userRole => {
            userRole.HasKey(ur => ur.Id);
            userRole.Property(ur => ur.Id).ValueGeneratedOnAdd();
            userRole.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
        });

        modelBuilder.Entity<Appearance>(appearance => {
            appearance.HasKey(a => a.Id);
            appearance.Property(a => a.Id).ValueGeneratedOnAdd();
            appearance.Property(a => a.Theme);
            appearance.HasData(AppearanceInit);
        });

        modelBuilder.Entity<Settings>(settings => {
            settings.HasKey(s => s.Id);
            settings.Property(s => s.UserId);
            settings.HasOne(s => s.Appearance).WithMany(a => a.Settings).HasForeignKey(s => s.AppearanceId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ActivityLog>(activity => {
            activity.HasKey(a => a.Id);
            activity.Property(a => a.Id).ValueGeneratedOnAdd();
            activity.Property(a => a.ActionType);
            activity.Property(a => a.Author);
            activity.Property(a => a.Message);
            activity.Property(a => a.UserName);
            activity.Property(a => a.Date);
        });

        modelBuilder.Entity<Notification>(notification => {
            notification.HasKey(n => n.Id);
            notification.Property(n => n.Id).ValueGeneratedOnAdd();
            notification.Property(n => n.Title).IsRequired();
            notification.Property(n => n.Message).IsRequired();
            notification.Property(n => n.CreationDate);
            notification.Property(n => n.Read);
            notification.Property(n => n.UserId);
        });

        modelBuilder.Entity<Student>(student => {
            student.HasKey(t => t.Id);
        });
    }
}