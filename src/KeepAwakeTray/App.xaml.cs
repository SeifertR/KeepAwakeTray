using Hardcodet.Wpf.TaskbarNotification;
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
        private Mutex singleInstanceMutex;
        private AppManager appManager;
        private TaskbarIcon notifyIcon;

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

            appManager = new AppManager();
            notifyIcon = (TaskbarIcon)FindResource("TheNotifyIcon");

            if (notifyIcon != null)
                notifyIcon.DataContext = appManager;

            appManager.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon.Dispose();
            base.OnExit(e);
        }
    }
}
