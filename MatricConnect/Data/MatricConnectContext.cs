using MatricConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace MatricConnect.Data;

public class MatricConnectContext : DbContext
{
    public MatricConnectContext(DbContextOptions<MatricConnectContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<University> Universities { get; set; }

    public DbSet<Programme> Programmes { get; set; }

    public DbSet<SavedProgramme> SavedProgrammes { get; set; }
}