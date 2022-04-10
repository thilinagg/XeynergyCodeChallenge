using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Infrastructure.Persistence.Configurations
{
    public class AccessRuleConfigurations : IEntityTypeConfiguration<AccessRule>
    {
        public void Configure(EntityTypeBuilder<AccessRule> builder)
        {
            builder.Property(p => p.RuleName)
                  .HasMaxLength(100)
                  .IsRequired();
        }
    }
}
