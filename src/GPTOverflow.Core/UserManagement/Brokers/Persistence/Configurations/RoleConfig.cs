using GPTOverflow.Core.UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GPTOverflow.Core.UserManagement.Brokers.Persistence.Configurations;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("role");
        builder.Property(x => x.Name).IsRequired().HasConversion<string>();
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder.HasMany(x => x.Permissions)
            .WithOne(x=>x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata.FindNavigation(nameof(Role.Permissions))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}