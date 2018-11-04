using Hardcodet.Wpf.TaskbarNotification;
using KeepAwakeTray.Interfaces;
using KeepAwakeTray.Managers;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace KeepAwakeTray
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly CompositionManager container = new CompositionManager();

        private Mutex singleInstanceMutex;
        private TaskbarIcon notifyIcon;
        private IAppManager appManager;

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
            base.OnStartup(e);

            appManager = container.Resolve<IAppManager>();
            notifyIcon = (TaskbarIcon)FindResource("TheNotifyIcon");

            if (notifyIcon != null)
                notifyIcon.DataContext = appManager;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
