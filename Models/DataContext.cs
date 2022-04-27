using Microsoft.EntityFrameworkCore;

namespace WebLab5.Models;

public sealed class DataContext : DbContext
{
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Lab> Labs { get; set; }
    public DbSet<Patient> Patients { get; set; }

    public DataContext(DbContextOptions options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DataContext()
    {
    }
}