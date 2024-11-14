using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024;

namespace IDPA_Hauptprojekt_2024_Migration
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
            services.AddSingleton<MainWindow>(); // Register MainWindow or other services if needed
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.Migrate(); // This ensures that migrations are applied on startup
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
