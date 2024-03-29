﻿namespace KeepAwakeTray6.Interfaces
{
    public interface ISettingsManager
    {
        /// <summary>
        /// The interval, in seconds, that inactivity should be checked for
        /// </summary>
        int InactivityCheckInterval { get; set; }

        /// <summary>
        /// The amount of time, in seconds, that a user can be inactive before
        /// the activity key is sent
        /// </summary>
        int InactivityInterval { get; set; }

        void SaveSettings(int inactivityCheckInterval, int inactivityInterval);
        void ResetSettingsToDefault();
    }
}
