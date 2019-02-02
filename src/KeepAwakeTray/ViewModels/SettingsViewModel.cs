using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using KeepAwakeTray.Interfaces;
using System;

namespace KeepAwakeTray.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsManager settingsManager;

        private bool isDirty;

        private string _inactivityCheckInterval;
        public string InactivityCheckInterval
        {
            get => _inactivityCheckInterval;
            set
            {
                if (value == _inactivityCheckInterval) return;

                _inactivityCheckInterval = value;
                isDirty = true;
            }
        }

        private string _inactivityInterval;
        public string InactivityInterval
        {
            get => _inactivityInterval;
            set
            {
                if (value == _inactivityInterval) return;

                _inactivityInterval = value;
                isDirty = true;
            }
        }

        public EventHandler CloseRequested = delegate { };

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }

        public SettingsViewModel(ISettingsManager manager)
        {
            settingsManager = manager ?? throw new ArgumentNullException(nameof(manager));

            InactivityCheckInterval = settingsManager.InactivityCheckInterval.ToString();
            InactivityInterval = settingsManager.InactivityInterval.ToString();

            SaveCommand = new RelayCommand(Save_Execute, () => isDirty);
            CloseCommand = new RelayCommand(Close_Execute);
        }

        private void Save_Execute()
        {
            settingsManager.SaveSettings(Convert.ToInt32(InactivityCheckInterval), Convert.ToInt32(InactivityInterval));
        }

        private void Close_Execute()
        {
            // TODO: Check for unsaved updates
            CloseRequested(this, EventArgs.Empty);
        }
    }
}
