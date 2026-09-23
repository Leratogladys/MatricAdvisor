// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides Entity Framework Core with a design-time
//                   Matric Connect database context for migration commands.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MatricConnect.Data
{
    public class MatricConnectContextFactory
        : IDesignTimeDbContextFactory<MatricConnectContext>
    {
        public MatricConnectContext CreateDbContext(string[] args)
        {
            //
            // Name              : MatricConnectContext
            //                     CreateDbContext(string[] args)
            // Purpose           : Creates and configures a Matric Connect
            //                     database context for Entity Framework Core
            //                     design-time operations.
            // Re-use            : GetConnectionString(),
            //                     MatricConnectContext()
            // Method Parameters : string[] args
            //                     - design-time arguments supplied by
            //                       Entity Framework Core; currently unused
            // Output Type       : MatricConnectContext
            //                     - configured Matric Connect database context
            //

            var optionsBuilder =
                new DbContextOptionsBuilder<MatricConnectContext>();

            optionsBuilder.UseSqlite(
                DatabaseConfiguration.GetConnectionString());

            return new MatricConnectContext(
                optionsBuilder.Options);
        } // end method
    } // end class MatricConnectContextFactory
} // end namespace MatricConnect.Data