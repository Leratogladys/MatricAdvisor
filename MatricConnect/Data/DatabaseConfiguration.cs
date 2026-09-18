using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatricConnect.Data
{
    public class DatabaseConfiguration
    {
        public static string GetDatabasePath()
        {
            string localAppData = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);

            string databaseFolder = Path.Combine(localAppData, "MatricConnect");

            Directory.CreateDirectory(databaseFolder);

            return Path.Combine(databaseFolder, "MatricConnect.db");
        }

        public static string GetConnectionString()
        {
            return $"Data Source={GetDatabasePath()}";
        }
    }
}
