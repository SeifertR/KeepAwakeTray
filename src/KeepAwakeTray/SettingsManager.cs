using KeepAwakeTray.Interfaces;

namespace KeepAwakeTray
{
    public class SettingsManager : ISettingsManager
    {
        public int InactivityCheckInterval { get; set; } = 30;

        public int InactivityInterval { get; set; } = 180;
    }
}
