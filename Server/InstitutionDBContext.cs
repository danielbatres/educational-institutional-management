using Microsoft.EntityFramework.Algo;

public class InstitutionDBContext : DBContext {
    public DBSet<User> Users { get; set; }
    public DBSet<Institution> Institutions { get; set; }

    public InstitutionDBContext(DBOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity();
    }
}