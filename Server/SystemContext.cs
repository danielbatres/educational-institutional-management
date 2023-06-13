using Microsoft.EntityFrameworkCore;
using edu_institutional_management.Server.Models;

namespace edu_institutional_management.Server;

public class SystemContext : DbContext {
    public SystemContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>(role => {
            role.HasKey(t => t.Id);
            role.Property(t => t.Name).IsRequired();
        });

        modelBuilder.Entity<Permission>(permission => {
            permission.HasKey(t => t.Id);
        });

        modelBuilder.Entity<Student>(student => {
            student.HasKey(t => t.Id);
        });
    }
}