using edu_institutional_management.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace edu_institutional_management.Server;

public class ApplicationContext : DbContext {
    public ApplicationContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>(role => {
            role.HasKey(t => t.Id);
            role.Property(t => t.Name).IsRequired();
        });

        modelBuilder.Entity<Permission>(permission => {
            permission.HasKey(t => t.Id);
            permission.Property(t => t.Name).IsRequired();
            permission.Property(t => t.Description);
        });

        modelBuilder.Entity<Student>(student => {
            student.HasKey(t => t.Id);
        });
    }
}