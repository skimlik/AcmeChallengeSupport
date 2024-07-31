using Microsoft.EntityFrameworkCore;

namespace AcmeChallengeSupport.Host.Database;

public class DbUnit(DbContextOptions<DbUnit> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Filename=acme-challenges.db");
    }

    public DbSet<ChallengeMap> ChallengeMaps { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ChallengeMapConfiguration());
    }
}