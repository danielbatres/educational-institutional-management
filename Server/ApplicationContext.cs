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
    public DbSet<ActivityLog> Activities { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<FieldInformation> FieldsInformation { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentRegister> StudentRegisters { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<RatingsList> RatingsLists { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectCourse> SubjectCourses { get; set; }
    public DbSet<CourseSchedule> CourseSchedules { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<AttendanceSchedule> AttendanceSchedules { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

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
            role.HasMany(r => r.RolePermissions).WithOne(rp => rp.Role).HasForeignKey(rp => rp.RoleId);
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

        modelBuilder.Entity<SubjectCourse>(subjectCourse => {
            subjectCourse.HasKey(s => s.Id);
            subjectCourse.HasMany(sc => sc.RatingsLists).WithOne(rl => rl.SubjectCourse).HasForeignKey(rl => rl.SubjectCourseId);
            subjectCourse.HasMany(sc => sc.CourseSchedules).WithOne(cs => cs.SubjectCourse).HasForeignKey(cs => cs.SubjectCourseId);
        });

        modelBuilder.Entity<CourseSchedule>(courseSchedule => {
            courseSchedule.HasKey(cs => cs.Id);
            courseSchedule.Property(cs => cs.DayOfWeek).IsRequired();
            courseSchedule.Property(cs => cs.StartTime).IsRequired();
            courseSchedule.Property(cs => cs.EndTime).IsRequired();
        });

        modelBuilder.Entity<Attendance>(attendance => {
            attendance.HasKey(a => a.Id);
            attendance.Property(a => a.Date).IsRequired();
            attendance.Property(a => a.IsPresent).IsRequired();
        });

        modelBuilder.Entity<AttendanceSchedule>(attendanceSchedule => {
            attendanceSchedule.HasKey(ts => ts.Id);
            attendanceSchedule.Property(ts => ts.DayOfWeek).IsRequired();
            attendanceSchedule.Property(ts => ts.StartTime).IsRequired();
            attendanceSchedule.Property(ts => ts.EndTime).IsRequired();
        });

        modelBuilder.Entity<Course>(course => {
            course.HasKey(c => c.Id);
            course.Property(c => c.Guide);
            course.Property(c => c.Name);
            course.Property(c => c.Acronym);
            course.Property(c => c.StudentsCount);
            course.HasMany(c => c.Students).WithOne(c => c.Course).HasForeignKey(s => s.CourseId);
            course.HasMany(c => c.AttendanceSchedules).WithOne(c => c.Course).HasForeignKey(asch => asch.CourseId);
        });

        modelBuilder.Entity<Student>(student => {
            student.HasKey(t => t.Id);
            student.Property(s => s.Name);
            student.Property(s => s.LastName);
            student.Property(s => s.Gender);
            student.Property(s => s.BirthDate);
            student.Property(s => s.PhoneNumber);
            student.Property(s => s.Photo);
            student.Property(s => s.UniqueIdentifier);
            student.HasIndex(s => s.UniqueIdentifier).IsUnique();
            student.HasOne(s => s.StudentRegister).WithOne(sr => sr.Student).HasForeignKey<StudentRegister>(sr => sr.StudentId);
            student.HasMany(s => s.FieldsInformation).WithOne(fi => fi.Student).HasForeignKey(fi => fi.StudentId);
            student.HasMany(s => s.Attendances).WithOne(a => a.Student).HasForeignKey(a => a.StudentId);
        });

        modelBuilder.Entity<StudentRegister>(studentRegister => {
            studentRegister.HasKey(sr => sr.Id);
            studentRegister.Property(sr => sr.Email);
            studentRegister.HasIndex(sr => sr.Email).IsUnique();
            studentRegister.Property(sr => sr.Password);
            studentRegister.Property(sr => sr.CreatedAt);
        });

        modelBuilder.Entity<Subject>(subject => {
            subject.HasKey(s => s.Id);
            subject.Property(s => s.Name);
            subject.HasMany(s => s.SubjectCourses).WithOne(sc => sc.Subject).HasForeignKey(sc => sc.SubjectId);
        });

        modelBuilder.Entity<RatingsList>(ratingsList => {
            ratingsList.HasKey(rl => rl.Id);
            ratingsList.Property(rl => rl.ListName);
            ratingsList.Property(rl => rl.Description);
            ratingsList.HasMany(rl => rl.Ratings).WithOne(r => r.RatingsList).HasForeignKey(r => r.RatingsListId);
        });

        modelBuilder.Entity<Rating>(rating => {
            rating.HasKey(r => r.Id);
            rating.Property(r => r.Id).ValueGeneratedOnAdd();
            rating.Property(r => r.StudentId);
            rating.Property(r => r.RatingValue);
        });

        modelBuilder.Entity<Category>(category => {
            category.HasKey(c => c.Id);
            category.Property(c => c.Name);
            category.HasMany(c => c.Fields).WithOne(f => f.Category).HasForeignKey(f => f.CategoryId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<Field>(field => {
            field.HasKey(f => f.Id);
            field.Property(f => f.Name);
            field.Property(f => f.IsRequired);
            field.HasMany(f => f.FieldsInformation).WithOne(fi => fi.Field).HasForeignKey(fi => fi.FieldId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<FieldInformation>(fieldInformation => {
            fieldInformation.HasKey(fi => fi.Id);
            fieldInformation.Property(fi => fi.Information);
        });

        modelBuilder.Entity<ChatMessage>(chat => {
            chat.HasKey(c => c.Id);
            chat.Property(c => c.Id).ValueGeneratedOnAdd();
            chat.Property(c => c.Message);
            chat.Property(c => c.TimeStamp);
            chat.Property(c => c.UserId);
        });
    }
}