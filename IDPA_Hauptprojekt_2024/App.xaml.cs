using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using IDPA_Hauptprojekt_2024.Database;
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


        //DI-Container
        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlite("Data Source=IDPA_Hauptprojekt_2024.db"));
            services.AddTransient<MainWindow>();
            services.AddTransient<FilterAlgorithm>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var loadingScreen = new LoadingScreenWindow();
            loadingScreen.Show();

            // Data import and migration in background
            await Task.Run(() =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    context.Database.Migrate(); // Migration
                    var importer = new DataImporter(context);
                    importer.ImportData(); // Data import
                }
            });

            // Create MainWindow from DI-Container erstellen
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            loadingScreen.Close();

            base.OnStartup(e);
        }
    }
}