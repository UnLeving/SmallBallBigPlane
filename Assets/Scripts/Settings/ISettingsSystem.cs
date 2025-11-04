using HelpersAndExtensions.SaveSystem;

namespace SmallBallBigPlane
{
    public interface ISettingsSystem
    {
        bool LimitTo60Fps { get; set; }
        bool SoundEnabled { get; set; }
        bool ConsoleEnabled { get; set; }
        bool StatsEnabled { get; set; }
        void ApplyLimitFps();
        void ApplySound();
        void ApplyConsole();
        void ApplyStats();

        SettingsData ToData();
        void FromData(SettingsData data);
    }
}


