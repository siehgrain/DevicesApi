using Devices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devices.Infrastructure.Persistence
{
    public class DevicesDbContext : DbContext
    {
        public DevicesDbContext(DbContextOptions<DevicesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Device> Devices => Set<Device>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.Brand)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(d => d.CreatedAt)
                    .IsRequired();

                entity.Property(d => d.State)
                    .IsRequired();
            });
        }
    }
}
