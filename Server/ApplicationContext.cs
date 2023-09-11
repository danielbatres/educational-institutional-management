using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class ApplicationContext : DbContext {
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Student> Students { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base (options) {
        ChangeTracker.LazyLoadingEnabled = true;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
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
            rolePermission.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        });

        modelBuilder.Entity<UserRole>(userRole => {
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
            userRole.HasOne(ur => ur.Role).WithMany().HasForeignKey(ur => ur.RoleId);
        });

        modelBuilder.Entity<Student>(student => {
            student.HasKey(t => t.Id);
        });

        
    }
}