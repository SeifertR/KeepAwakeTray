using GalaSoft.MvvmLight.CommandWpf;
using KeepAwakeTray.Interfaces;
using System;
using System.Diagnostics;
using System.Windows;

namespace KeepAwakeTray
{
    public class AppManager : IAppManager
    {
        private readonly IActivityMonitor monitor;
        private readonly ISettingsManager settings;

        public RelayCommand ActivateCommand { get; }
        public RelayCommand DeactivateCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }
        public RelayCommand ExitCommand { get; }

        public AppManager(IActivityMonitor activityMonitor, ISettingsManager settingsManager)
        {
            monitor = activityMonitor ?? throw new ArgumentNullException(nameof(activityMonitor));
            settings = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));

            ActivateCommand = new RelayCommand(() => monitor.Activate());
            DeactivateCommand = new RelayCommand(() => monitor.Deactivate());
            ShowSettingsCommand = new RelayCommand(() => Debug.WriteLine("Show settings not implemented"));
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }
    }
}
