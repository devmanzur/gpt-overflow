using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Configurations;

public class ApplicationUserConfig : AuditableEntityConfiguration<ApplicationUser>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        base.Configure(builder);
        builder.ToTable("application_user");
        builder.Property(x => x.EmailAddress).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Status).IsRequired().HasConversion<string>();
        builder.Property(x => x.RoleId).IsRequired();
        builder.HasIndex(x => x.EmailAddress).IsUnique();
    }
}