using Hardcodet.Wpf.TaskbarNotification;
using KeepAwakeTray6.Interfaces;
using KeepAwakeTray6.Managers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace KeepAwakeTray6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        private Mutex? singleInstanceMutex;
        private TaskbarIcon? notifyIcon;
        private IAppManager? appManager;

        public App()
        {
            //
            // Only allow one instance of the app to run
            //
            var s = Assembly.GetExecutingAssembly().GetType().GUID.ToString();
            singleInstanceMutex = new Mutex(true, s, out var isNew);

            if (!isNew)
                Environment.Exit(0);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            notifyIcon = (TaskbarIcon)FindResource("TheNotifyIcon");

            serviceProvider = services.BuildServiceProvider();
            appManager = serviceProvider.GetService<IAppManager>();

            if (notifyIcon != null)
                notifyIcon.DataContext = appManager;
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAppManager, AppManager>();
            services.AddSingleton<IActivityMonitor, ActivityMonitor>();
            services.AddSingleton<ISettingsManager, SettingsManager>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon?.Dispose();
            singleInstanceMutex?.Dispose();
            base.OnExit(e);
        }
    }
}
