using IDPA_Hauptprojekt_2024.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IDPA_Hauptprojekt_2024_Migration.Database
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlite("Data Source=IDPA_Hauptprojekt_2024.db");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
