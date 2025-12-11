using UnityEngine;
using HelpersAndExtensions.SaveSystem;
using System;

namespace SmallBallBigPlane
{
    [System.Serializable]
    public class SettingsData : ISavable
    {
        public string Id { get; set; }
        public bool LimitTo60Fps;
        public bool SoundEnabled;
        public bool ConsoleEnabled;
        public bool StatsEnabled;
    }
    
    public class SettingsSystem
    {
        public event Action<SettingsData> OnSettingsChanged;

        public string Id { get; set; } = "Settings";
        public SettingsData data;
        private readonly SaveLoadSystem _saveLoadSystem;
        
        public SettingsSystem(SaveLoadSystem saveLoadSystem)
        {
            this._saveLoadSystem = saveLoadSystem;
        }

        private void ApplyAll()
        {
            ApplyLimitFps();
            ApplySound();
            OnSettingsChanged?.Invoke(data);
        }
        
        public void Initialize()
        {
            this.data = _saveLoadSystem.GameData.SettingsData;
            this.data.Id = Id;
            
            FromData(data);
        }
        
        public void SaveData()
        {
            if (data == null) return;

            var currentData = ToData();
            
            data.LimitTo60Fps = currentData.LimitTo60Fps;
            data.SoundEnabled = currentData.SoundEnabled;
            data.ConsoleEnabled = currentData.ConsoleEnabled;
            data.StatsEnabled = currentData.StatsEnabled;
        }

        public SettingsData ToData()
        {
            return new SettingsData
            {
                LimitTo60Fps = data.LimitTo60Fps,
                SoundEnabled = data.SoundEnabled,
                ConsoleEnabled = data.ConsoleEnabled,
                StatsEnabled = data.StatsEnabled
            };
        }

        public void FromData(SettingsData data)
        {
            if (data == null) return;
            
            this.data.LimitTo60Fps = data.LimitTo60Fps;
            this.data.SoundEnabled = data.SoundEnabled;
            this.data.ConsoleEnabled = data.ConsoleEnabled;
            this.data.StatsEnabled = data.StatsEnabled;
            
            ApplyAll();
        }

        public void ApplyLimitFps()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = this.data.LimitTo60Fps ? 60 : 30;
        }

        public void ApplySound()
        {
            AudioListener.volume = this.data.SoundEnabled ? 1f : 0f;
        }

        public void ApplyConsole()
        {
            OnSettingsChanged?.Invoke(data);
        }

        public void ApplyStats()
        {
            OnSettingsChanged?.Invoke(data);
        }
    }
}


