using Device.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Device.Infrastructure.Persistence.EntityConfigurations;

public class DeviceConfiguration : IEntityTypeConfiguration<Device.Domain.Entities.Device>
{
    public void Configure(EntityTypeBuilder<Device.Domain.Entities.Device> builder)
    {
        builder.ToTable("devices");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.Brand)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(d => d.State)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(d => d.CreatedAt)
            .IsRequired();

        builder.HasIndex(d => d.Brand);
        builder.HasIndex(d => d.State);
        builder.HasIndex(d => d.CreatedAt);
    }
}
