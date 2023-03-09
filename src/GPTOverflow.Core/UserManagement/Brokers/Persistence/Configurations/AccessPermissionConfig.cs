using GPTOverflow.Core.CrossCuttingConcerns.Persistence.Configurations;
using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Configurations;

public class AccessPermissionConfig : AuditableEntityConfiguration<AccessPermission>
{
    public override void Configure(EntityTypeBuilder<AccessPermission> builder)
    {
        base.Configure(builder);
        builder.ToTable("access_permission");
        builder.Property(x => x.Name).IsRequired().HasConversion<string>().HasMaxLength(100);
        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(150);
        builder.HasIndex(x => x.Name).IsUnique();
        
    }
}