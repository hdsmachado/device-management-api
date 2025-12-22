using Device.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Device.Infrastructure.Persistence;

public class DeviceDbContext : DbContext
{
    public DeviceDbContext(DbContextOptions<DeviceDbContext> options)
        : base(options)
    {
    }

    public DbSet<Device.Domain.Entities.Device> Devices => Set<Device.Domain.Entities.Device>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DeviceDbContext).Assembly);
    }
}