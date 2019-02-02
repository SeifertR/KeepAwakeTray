using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using KeepAwakeTray.Interfaces;
using System;
using System.Windows;
using KeepAwakeTray.ViewModels;
using KeepAwakeTray.Views;

namespace KeepAwakeTray.Managers
{
    public class AppManager : ViewModelBase, IAppManager
    {
        private readonly IActivityMonitor monitor;
        private readonly ISettingsManager settingsManager;

        private bool showingSettings;

        public bool IsActive => monitor.IsActive;

        public RelayCommand ActivateCommand { get; }
        public RelayCommand DeactivateCommand { get; }
        public RelayCommand ShowSettingsCommand { get; }
        public RelayCommand ExitCommand { get; }

        public AppManager(IActivityMonitor activityMonitor, ISettingsManager manager)
        {
            monitor = activityMonitor ?? throw new ArgumentNullException(nameof(activityMonitor));
            settingsManager = manager ?? throw new ArgumentNullException(nameof(manager));

            ActivateCommand = new RelayCommand(() =>
            {
                monitor.Activate();
                RaisePropertyChanged(nameof(IsActive));
            }, () => !monitor.IsActive);

            DeactivateCommand = new RelayCommand(() =>
            {
                monitor.Deactivate();
                RaisePropertyChanged(nameof(IsActive));
            }, () => monitor.IsActive);

            ShowSettingsCommand = new RelayCommand(ShowSettings_Execute, () => !showingSettings);
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }

        private void ShowSettings_Execute()
        {
            showingSettings = true;

            var viewModel = new SettingsViewModel(settingsManager);
            var view = new SettingsView(viewModel) {Topmost = true};
            view.Closed += SettingsView_Closed;
            view.Show();
            view.Activate();
        }

        private void SettingsView_Closed(object sender, EventArgs e)
        {
            ((SettingsView) sender).Closed -= SettingsView_Closed;
            showingSettings = false;
        }
    }
}
