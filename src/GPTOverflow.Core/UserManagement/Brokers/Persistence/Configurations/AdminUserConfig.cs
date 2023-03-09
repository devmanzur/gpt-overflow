using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Configurations;

public class AdminUserConfig : AuditableEntityConfiguration<AdminUser>
{
    public override void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        base.Configure(builder);
        builder.ToTable("admin_user");
        builder.Property(x => x.EmailAddress).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.HasIndex(x => x.EmailAddress).IsUnique();
    }
}