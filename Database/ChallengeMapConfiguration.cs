using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcmeChallengeSupport.Host.Database;

public class ChallengeMapConfiguration : IEntityTypeConfiguration<ChallengeMap>
{
    public void Configure(EntityTypeBuilder<ChallengeMap> builder)
    {
        builder.ToTable("AcmeChallengeMap");
        builder.HasKey(k => k.Key);
        builder.Property(p => p.Key).HasColumnName("Key").IsRequired();
        builder.Property(p => p.Value).HasColumnName("Value").IsRequired();
    }
}