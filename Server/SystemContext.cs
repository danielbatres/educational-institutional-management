using Microsoft.EntityFrameworkCore;
using edu_institutional_management.Server.Models;

namespace edu_institutional_management.Server;

public class SystemContext : DbContext {
    public SystemContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>(role => {
            role.ToTable("Role");
            role.HasKey(t => t.Id);
            role.Property(t => t.Name).IsRequired();
        });

        modelBuilder.Entity<Permission>(permission => {
            permission.ToTable("Permission");
            permission.HasKey(t => t.Id);
        });

        modelBuilder.Entity<Student>(student => {
            student.ToTable("Student");
            student.HasKey(t => t.Id);
        });
    }
}