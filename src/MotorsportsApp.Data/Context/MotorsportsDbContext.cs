using Microsoft.EntityFrameworkCore;
using MotorsportsApp.Models.Domain;

namespace MotorsportsApp.Data.Context;

public class MotorsportsDbContext : DbContext
{
    public MotorsportsDbContext(DbContextOptions<MotorsportsDbContext> options) 
        : base(options)
    {
    }

    public DbSet<Driver> Drivers { get; set; } = null!;
    public DbSet<Constructor> Constructors { get; set; } = null!;
    public DbSet<Circuit> Circuits { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Timing> Timings { get; set; } = null!;
    public DbSet<LapData> LapData { get; set; } = null!;
    public DbSet<Weather> Weather { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Driver
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId);
            entity.Property(e => e.Code).HasMaxLength(3);
        });

        // Configure Constructor
        modelBuilder.Entity<Constructor>(entity =>
        {
            entity.HasKey(e => e.ConstructorId);
        });

        // Configure Circuit
        modelBuilder.Entity<Circuit>(entity =>
        {
            entity.HasKey(e => e.CircuitId);
        });

        // Configure Session
        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId);
            entity.HasOne(e => e.Circuit);
        });

        // Configure Timing
        modelBuilder.Entity<Timing>(entity =>
        {
            entity.HasKey(e => e.TimingId);
            entity.HasIndex(e => new { e.SessionId, e.DriverId, e.Timestamp });
        });

        // Configure LapData
        modelBuilder.Entity<LapData>(entity =>
        {
            entity.HasKey(e => e.LapDataId);
            entity.HasIndex(e => new { e.SessionId, e.DriverId, e.LapNumber });
        });

        // Configure Weather
        modelBuilder.Entity<Weather>(entity =>
        {
            entity.HasKey(e => e.WeatherId);
            entity.HasIndex(e => new { e.SessionId, e.Timestamp });
        });
    }
}
