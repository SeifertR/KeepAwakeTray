using KeepAwakeTray.Interfaces;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;

namespace KeepAwakeTray
{
    public class ActivityMonitor : IActivityMonitor, IDisposable
    {
        private readonly ISettingsManager settings;

        //
        // PInvoke signatures and structure can be found here: https://www.pinvoke.net
        //
        [StructLayout(LayoutKind.Sequential)]
        private struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));

            [MarshalAs(UnmanagedType.U4)]
            public uint cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        //
        // Virtual key codes can be found here: https://docs.microsoft.com/en-us/windows/desktop/inputdev/virtual-key-codes
        //
        private static byte VK_SHIFT = 0x10;
        //
        // Flag values can be found here: https://msdn.microsoft.com/en-us/library/windows/desktop/ms646271(v=vs.85).aspx
        //
        private static uint KEYEVENTF_KEYUP = 0x0002;

        private Timer activityTimer;

        public bool IsActive { get; private set; } = true;


        public ActivityMonitor(ISettingsManager settingsManager)
        {
            settings = settingsManager ?? throw new ArgumentNullException(nameof(settingsManager));

            activityTimer = new Timer { Interval = settings.InactivityCheckInterval * 1000 };
            activityTimer.Elapsed += ActivityTimer_Elapsed;
            activityTimer.Start();
        }

        private void ActivityTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            activityTimer.Enabled = false;
            //
            // If we've been idle for more than settings.InactivityInterval seconds, simulate pressing
            // the shift key to reset the idle time.
            //
            if (GetLastInputTime() > settings.InactivityInterval)
            {
                keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
                Debug.WriteLine(DateTime.Now.ToString("mm:ss") + " Fired");
            }
            else
                Debug.WriteLine(DateTime.Now.ToString("mm:ss") + " Not fired");

            activityTimer.Interval = settings.InactivityCheckInterval * 1000;
            activityTimer.Enabled = true;
        }

        private static uint GetLastInputTime()
        {
            uint idleTime = 0;
            var lastInputInfo = new LASTINPUTINFO();
            lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
            lastInputInfo.dwTime = 0;

            var envTicks = (uint)Environment.TickCount;

            if (!GetLastInputInfo(ref lastInputInfo))
                return ((idleTime > 0) ? (idleTime / 1000) : 0);

            var lastInputTick = lastInputInfo.dwTime;
            idleTime = envTicks - lastInputTick;

            return ((idleTime > 0) ? (idleTime / 1000) : 0);
        }

        public void Activate()
        {
            activityTimer.Start();
            IsActive = true;
        }

        public void Deactivate()
        {
            activityTimer.Stop();
            IsActive = false;
        }

        public void Dispose()
        {
            activityTimer.Stop();
            activityTimer.Dispose();
        }
    }
}
