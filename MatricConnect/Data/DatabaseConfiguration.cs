// Programmer name : Lerato Molefe
// Project name    : Matric Connect
// Purpose         : Provides the SQLite database path and connection
//                   string used by Matric Connect.

namespace MatricConnect.Data
{
    public class DatabaseConfiguration
    {
        public static string GetDatabasePath()
        {
            //
            // Name              : string GetDatabasePath()
            // Purpose           : Creates the Matric Connect application
            //                     data folder when necessary and returns
            //                     the full SQLite database file path.
            // Re-use            : None
            // Method Parameters : None
            // Output Type       : string
            //                     - full path to the Matric Connect
            //                       SQLite database file
            //

            string localAppData = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);

            string databaseFolder = Path.Combine(
                localAppData,
                "MatricConnect");

            Directory.CreateDirectory(databaseFolder);

            return Path.Combine(
                databaseFolder,
                "MatricConnect.db");
        } // end method

        public static string GetConnectionString()
        {
            //
            // Name              : string GetConnectionString()
            // Purpose           : Creates the SQLite connection string
            //                     using the configured database path.
            // Re-use            : GetDatabasePath()
            // Method Parameters : None
            // Output Type       : string
            //                     - SQLite database connection string
            //

            return $"Data Source={GetDatabasePath()}";
        } // end method
    } // end class DatabaseConfiguration
} // end namespace MatricConnect.Data