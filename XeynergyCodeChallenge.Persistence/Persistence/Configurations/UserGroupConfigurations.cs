using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XeynergyCodeChallenge.Domain.Entities;

namespace XeynergyCodeChallenge.Infrastructure.Persistence.Configurations
{
    internal class UserGroupConfigurations : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.Property(p => p.GroupName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
