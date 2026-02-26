using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using LibraryApp.Data;

namespace LibraryApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                MessageBox.Show("OnStartup начал работу");

                var services = new ServiceCollection();
                services.AddDbContext<LibraryContext>(options =>
                    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryDb;Trusted_Connection=True;MultipleActiveResultSets=true;"));

                ServiceProvider = services.BuildServiceProvider();

                MessageBox.Show("DI контейнер создан");

                using (var scope = ServiceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                    db.Database.EnsureCreated();
                }

                MessageBox.Show("База проверена/создана");

                var mainWindow = new MainWindow();
                MessageBox.Show("MainWindow создан");
                mainWindow.Show();
                MessageBox.Show("MainWindow показано");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Критическая ошибка при запуске:\n{ex.Message}\n\nСтек:\n{ex.StackTrace}");
            }
        }
    }
}