using KeepAwakeTray.Interfaces;
using Newtonsoft.Json;
using System;
using System.IO;

namespace KeepAwakeTray.Managers
{
    public class SettingsManager : ISettingsManager
    {
        private const int InactivityCheckIntervalDefault = 30;
        private const int InactivityIntervalDefault = 180;

        private readonly Settings settings = new Settings();

        private readonly string fileName;

        public int InactivityCheckInterval
        {
            get => settings.InactivityCheckInterval;
            set => settings.InactivityCheckInterval = value > 0 ? value : 1;
        }

        public int InactivityInterval
        {
            get => settings.InactivityInterval;
            set => settings.InactivityInterval = value > 0 ? value : 1;
        }

        public SettingsManager()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "KeepAwakeTray");
            Directory.CreateDirectory(path);
            fileName = Path.Combine(path, "settings.json");

            if (File.Exists(fileName))
            {
                try
                {
                    using (var file = File.OpenText(fileName))
                    {
                        var ser = new JsonSerializer();
                        settings = (Settings)ser.Deserialize(file, typeof(Settings));
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
