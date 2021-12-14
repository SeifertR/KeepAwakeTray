using System;
using System.Windows;
using KeepAwakeTray6.ViewModels;
using KeepAwakeTray6.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using KeepAwakeTray6.Interfaces;

namespace KeepAwakeTray6.Managers
{
    public class AppManager : ObservableRecipient, IAppManager
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
                OnPropertyChanged(nameof(IsActive));
            }, () => !monitor.IsActive);

            DeactivateCommand = new RelayCommand(() =>
            {
                monitor.Deactivate();
                OnPropertyChanged(nameof(IsActive));
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
