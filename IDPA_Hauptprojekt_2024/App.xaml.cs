using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024.Database.Logic;
using IDPA_Hauptprojekt_2024.LocigClass;

namespace IDPA_Hauptprojekt_2024
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite("Data Source=IDPA_Hauptprojekt_2024.db"));
            services.AddSingleton<DatabaseHandleClass>();
            services.AddSingleton<FilterAlgorithm>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate(); // This ensures that migrations are applied on startup
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var filterAlgorithm = _serviceProvider.GetRequiredService<FilterAlgorithm>();
            mainWindow.SetFilterAlgorithm(filterAlgorithm);
            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}