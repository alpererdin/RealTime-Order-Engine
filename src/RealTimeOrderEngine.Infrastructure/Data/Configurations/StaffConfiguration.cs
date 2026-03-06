using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealTimeOrderEngine.Domain.Entities;

namespace RealTimeOrderEngine.Infrastructure.Data.Configurations;

public class StaffConfiguration : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
        builder.Property(s => s.PinCode).IsRequired().HasMaxLength(10);
        builder.HasIndex(s => s.PinCode).IsUnique();
    }
}