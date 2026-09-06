using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MatricConnect.Data;

public class MatricConnectContextFactory
    : IDesignTimeDbContextFactory<MatricConnectContext>
{
    public MatricConnectContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MatricConnectContext>();

        optionsBuilder.UseSqlite("Data Source=MatricConnect.db");

        return new MatricConnectContext(optionsBuilder.Options);
    }
}