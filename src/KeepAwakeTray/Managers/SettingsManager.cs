using GalaSoft.MvvmLight;
using KeepAwakeTray.Interfaces;
using KeepAwakeTray.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace KeepAwakeTray.Managers
{
    public class SettingsManager : ViewModelBase, ISettingsManager
    {
        private const int InactivityCheckIntervalDefault = 30;
        private const int InactivityIntervalDefault = 180;

        private readonly SettingsModel settingsModel = new SettingsModel();

        private readonly string settingsPath;
        private readonly string fileName;

        public int InactivityCheckInterval
        {
            get => settingsModel.InactivityCheckInterval;
            set => settingsModel.InactivityCheckInterval = value > 0 ? value : 1;
        }

        public int InactivityInterval
        {
            get => settingsModel.InactivityInterval;
            set => settingsModel.InactivityInterval = value > 0 ? value : 1;
        }

        public SettingsManager()
        {
            settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeepAwakeTray");
            fileName = Path.Combine(settingsPath, "settings.json");

            if (File.Exists(fileName))
            {
                try
                {
                    using (var file = File.OpenText(fileName))
                    {
                        var ser = new JsonSerializer();
                        settingsModel = (SettingsModel)ser.Deserialize(file, typeof(SettingsModel));
                    }
                }
                catch
                {
                    ResetSettingsToDefault();
                }
            }
            else
                ResetSettingsToDefault();
        }

        public void SaveSettings(int inactivityCheckInterval, int inactivityInterval)
        {
            Directory.CreateDirectory(settingsPath);
            InactivityCheckInterval = inactivityCheckInterval;
            InactivityInterval = inactivityInterval;
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public void ResetSettingsToDefault()
        {
            SaveSettings(InactivityCheckIntervalDefault, InactivityIntervalDefault);
        }
    }
}
