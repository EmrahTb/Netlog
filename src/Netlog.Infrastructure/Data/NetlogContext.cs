using Netlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Netlog.Infrastructure.Data
{
    public class NetlogContext : DbContext
    {
        public NetlogContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<MaintenanceHistory> MaintenanceHistorys { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }
        public DbSet<PictureGroup> PictureGroups { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(ConfigureUser);
            builder.Entity<Maintenance>(ConfigureMaintenance);
            builder.Entity<MaintenanceHistory>(ConfigureMaintenanceHistory);
            builder.Entity<ActionType>(ConfigureActionType);
            builder.Entity<PictureGroup>(ConfigurePictureGroup);
            builder.Entity<Status>(ConfigureStatus);
            builder.Entity<Vehicle>(ConfigureVehicle);
            builder.Entity<VehicleType>(ConfigureVehicleType);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureMaintenance(EntityTypeBuilder<Maintenance> builder)
        {
            builder.ToTable("Maintenance");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureMaintenanceHistory(EntityTypeBuilder<MaintenanceHistory> builder)
        {
            builder.ToTable("MaintenanceHistory");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureActionType(EntityTypeBuilder<ActionType> builder)
        {
            builder.ToTable("ActionType");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigurePictureGroup(EntityTypeBuilder<PictureGroup> builder)
        {
            builder.ToTable("PictureGroup");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureStatus(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureVehicle(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }
        private void ConfigureVehicleType(EntityTypeBuilder<VehicleType> builder)
        {
            builder.ToTable("VehicleType");

            builder.HasKey(ci => ci.ID);

            builder.Property(ci => ci.ID)
               .ForSqlServerUseSequenceHiLo("Netlog_type_hilo")
               .IsRequired();
        }

    }
}
